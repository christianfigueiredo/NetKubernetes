using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Dtos.UsuariosDtos;
using NetKubernetes.Middleware;
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
                Telefone = usuario.Telefone,                
                UserName = usuario.UserName,
                Email = usuario.Email,
                Token = _jwtGerador.GerarToken(usuario)
            };            
        }
        public async Task<UsuarioResponseDto> GetUsuario()
        {
                var usuario = await _userManager.FindByNameAsync(_usuarioSessao.ObterUsuarioSessao()); //_userManager.FindByNameAsync(_usuarioSessao.ObterUsuarioSessao());
                if (usuario is null)
                    {
                        throw new MiddlewareException(HttpStatusCode.Unauthorized, 
                                                    new { Mensagem = "Usuário não encontrado" });
                    }

                return TransformerUserToUserDto(usuario!);
                   
        }

        public async Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email!);

             if (usuario is null)
                    {
                        throw new MiddlewareException(HttpStatusCode.Unauthorized, 
                                                    new { Mensagem = "Email do Usuário não encontrado" });
                    }

            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario!, request.Password!, false);
            if (!resultado.Succeeded)
                    {
                        throw new MiddlewareException(HttpStatusCode.Unauthorized, 
                                                    new { Mensagem = "Senha do Usuário incorreta" });
                    }

            return TransformerUserToUserDto(usuario!);
        }

        public async Task<UsuarioResponseDto> RegistroUsuario(UsuarioRegistroRequestDto request)
        {
            var existeEmail = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
            if (existeEmail)
            {
                throw new MiddlewareException(HttpStatusCode.BadRequest, 
                                            new { Mensagem = "Email do Usuário já existe" });
            }

            var existeUsername = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
            if (existeUsername)
            {
                throw new MiddlewareException(HttpStatusCode.BadRequest, 
                                            new { Mensagem = "UserName do Usuário já existe" });
            }

            var usuario = new Usuario
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                Telefone = request.Telefone,
                UserName = request.Email,
                Email = request.Email,             
            };

            var result = await _userManager.CreateAsync(usuario, request.Password!);

            if (!result.Succeeded)
            {
                throw new MiddlewareException(HttpStatusCode.BadRequest, 
                                            new { Mensagem = "Erro ao criar usuário" });
            }

            return TransformerUserToUserDto(usuario);
        }
    }
}