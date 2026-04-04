using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Identity;
using NetKubernetes.Dtos.UsuariosDtos;
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
                return TransformerUserToUserDto(usuario!);
                   
        }

        public async Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email!);
            await _signInManager.CheckPasswordSignInAsync(usuario!, request.Password!, false);
            return TransformerUserToUserDto(usuario!);
        }

        public async Task<UsuarioResponseDto> RegistroUsuario(UsuarioRegistroRequestDto request)
        {
            var usuario = new Usuario
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                Telefone = request.Telefone,
                UserName = request.Email,
                Email = request.Email,             
            };

            await _userManager.CreateAsync(usuario, request.Password!);

            return TransformerUserToUserDto(usuario);
        }
    }
}