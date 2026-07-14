using System.Security.Claims;

namespace NetKubernetes.Token
{
    public class UsuarioSessao : IUsuarioSessao
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuarioSessao(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor  = httpContextAccessor;
        }

        public string ObterUsuarioSessao()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return userName ?? string.Empty;
        }
    }
}