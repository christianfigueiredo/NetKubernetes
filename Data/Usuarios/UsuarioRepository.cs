using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Dtos.UsuarioDtos;
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
        private readonly DbContexto _context;
        private readonly IUsuarioSessao _usuarioSessao;
        public UsuarioRepository(DbContexto context, 
                                 UserManager<Usuario> userManager, 
                                 SignInManager<Usuario> signInManager, 
                                 IJwtGerador jwtGerador,
                                 IUsuarioSessao usuarioSessao)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGerador = jwtGerador;
            _usuarioSessao = usuarioSessao;
        }           
        private UsuarioResponseDto TransformUserToUserDto(Usuario usuario)
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
           var usuario = await _userManager.FindByNameAsync(_usuarioSessao.ObterUsuarioSessao());
           if (usuario is null)
           {
                throw new MiddlewareException(HttpStatusCode.Unauthorized, new { mensagem = "Usuário não encontrado" });
           }
           
           return TransformUserToUserDto(usuario!);
        
        }

        public async Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email!);

            if (usuario is null)
           {
                throw new MiddlewareException(HttpStatusCode.Unauthorized, new { mensagem = "Email não encontrado" });
           }

           var result = await _signInManager.CheckPasswordSignInAsync(usuario!, request.Senha!, false);
           if (!result.Succeeded)
           {
                return TransformUserToUserDto(usuario!);
           }

           throw new MiddlewareException(HttpStatusCode.Unauthorized, new { mensagem = "Credenciais incorretas" });         
           
        }

        public async Task<UsuarioResponseDto> RegistroUsuario(UsuarioRegistroRequestDto request)
        {
            var existeEmail = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
            if (existeEmail)
            {
                throw new MiddlewareException(HttpStatusCode.BadRequest, new { mensagem = "Email já cadastrado" });
            }
            
             var existeUsername = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
            if (existeUsername)
            {
                throw new MiddlewareException(HttpStatusCode.BadRequest, new { mensagem = "Nome de usuário já cadastrado" });
            }


            var usuario = new Usuario
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                Telefone = request.Telefone,
                UserName = request.UserName,
                Email = request.Email
            };

            await _userManager.CreateAsync(usuario!, request.Senha!);

            return TransformUserToUserDto(usuario!);
            
        }
    }
}