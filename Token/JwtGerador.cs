using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetKubernetes.Token
{
    public class JwtGerador : IJwtGerador
    {
        public string GerarToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("chave super secreta"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new SecurityTokenDescriptor(
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenGerado = tokenHandler.CreateToken(token);
            
            return tokenHandler.WriteToken(tokenGerado);
        }
    }
}