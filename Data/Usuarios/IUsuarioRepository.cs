using NetKubernetes.Dtos.UsuarioDtos;
using NetKubernetes.Models;

namespace NetKubernetes.Data.Usuarios
{
    public interface IUsuarioRepository
    {
       
        Task<UsuarioResponseDto> ObterUsuario();
        Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request);
        Task<UsuarioResponseDto> RegistrarUsuario(UsuarioRegistroRequestDto request);
       
    }
}