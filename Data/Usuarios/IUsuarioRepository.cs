using NetKubernetes.Dtos.UsuariosDtos;

namespace NetKubernetes.Data.Usuarios
{
    public interface IUsuarioRepository
    {
           Task<UsuarioResponseDto> GetUsuario();
           Task<UsuarioResponseDto> RegistroUsuario(UsuarioRegistroRequestDto request);
           Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request);                      
    }
}