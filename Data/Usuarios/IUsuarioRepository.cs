using NetKubernetes.Dtos.UsuariosDtos;
using NetKubernetes.Models;

namespace NetKubernetes.Data.Usuarios
{
    public interface IUsuarioRepository
    {
           Task<UsuarioResponseDto> GetUsuario();
           Task<UsuarioResponseDto> RegistroUsuario(UsuarioRegistroRequestDto request);
           Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request);                      
    }
}