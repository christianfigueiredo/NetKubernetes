using NetKubernetes.Models;

namespace NetKubernetes.Token
{
    public interface IJwtGerador 
    {
        string GerarToken(Usuario usuario);
    }
}