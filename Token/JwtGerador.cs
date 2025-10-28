using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NetKubernetes.Models;

namespace NetKubernetes.Token
{
    public class JwtGerador : IJwtGerador
    {
        public string GerarToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName!),
                new Claim("UserId", usuario.Id),
                new Claim("Email", usuario.Email!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MinhaChaveSuperSecreta"));

            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credenciais
            };  

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenGerado = tokenHandler.CreateToken(token);

            return tokenHandler.WriteToken(tokenGerado);         
        }
    }
}