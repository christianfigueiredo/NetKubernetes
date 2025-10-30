using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NetKubernetes.Dtos.UsuarioDtos;
using NetKubernetes.Models;
using NetKubernetes.Token;

namespace NetKubernetes.Data.Usuarios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IJwtGerador _jwtGerador;

        private readonly AppDbContext _context;
        private readonly IUsuarioSessao _usuarioSessao;

        public UsuarioRepository(UserManager<Usuario> userManager,
                                 SignInManager<Usuario> signInManager,
                                 IJwtGerador jwtGerador,
                                 AppDbContext context,
                                 IUsuarioSessao usuarioSessao)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGerador = jwtGerador;
            _context = context;
            _usuarioSessao = usuarioSessao;
        }

        private UsuarioResponseDto TransformerUserToUserDto(Usuario usuario)
        {
            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                UserName = usuario.UserName,
                Token = _jwtGerador.GerarToken(usuario)
            };
        }

        public async Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email!);
            await _signInManager.CheckPasswordSignInAsync(usuario!, request.Senha!, false);
            var usuarioDto = TransformerUserToUserDto(usuario!);
            return usuarioDto;
        }

        public async Task<UsuarioResponseDto> ObterUsuario()
        {
            var usuario = await _userManager.FindByNameAsync(_usuarioSessao.ObterUsuarioSessao());
            var usuarioDto = TransformerUserToUserDto(usuario!);
            return usuarioDto;
        }

        public async Task<UsuarioResponseDto> RegistrarUsuario(UsuarioRegistroRequestDto request)
        {
            var usuario = new Usuario
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                Telefone = request.Telefone,
                Email = request.Email,
                UserName = request.UserName
            };

            var resultado = await _userManager.CreateAsync(usuario, request.Senha!);

            if (resultado.Succeeded)
            {
                var usuarioDto = TransformerUserToUserDto(usuario);
                return usuarioDto;
            }

            throw new Exception("Erro ao registrar usuário");
        }
    }
}