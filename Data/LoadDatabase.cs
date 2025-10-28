using Microsoft.AspNetCore.Identity;
using NetKubernetes.Models;

namespace NetKubernetes.Data
{
    public class LoadDatabase
    {

        public static async Task InserirDados(AppDbContext context, UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    Nome = "Christian",
                    Sobrenome = "Figueiredo",
                    Email = "t7A7o@example.com",
                    UserName = "christianfigueiredo",
                    Telefone = "11999999999"

                };
                await usuarioManager.CreateAsync(usuario, "Senha@123");
            }
            if (!context.Imoveis!.Any())
            {
                context.Imoveis!.AddRange(
                    new Imovel
                    {
                        Nome = "Casa",
                        Endereco = "Rua 1",
                        Preco = 250000,
                        DataCriacao = DateTime.Now
                    },

                    new Imovel
                    {
                        Nome = "Apartamento",
                        Endereco = "Rua 1",
                        Preco = 150000,
                        DataCriacao = DateTime.Now
                    }
                );
               
            }
             await context.SaveChangesAsync();
        }
    }
}
