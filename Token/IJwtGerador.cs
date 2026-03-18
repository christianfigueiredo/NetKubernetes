using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetKubernetes.Token
{
    public interface IJwtGerador
    {
        string GerarToken(Usuario usuario);
    }
}