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
        public Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioResponseDto> ObterUsuario()
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioResponseDto> RegistrarUsuario(UsuarioRegistroRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}