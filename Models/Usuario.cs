using Microsoft.AspNetCore.Identity;

namespace NetKubernetes.Models
{
    public class Usuario : IdentityUser
    {
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Telefone { get; set; }
    }
}