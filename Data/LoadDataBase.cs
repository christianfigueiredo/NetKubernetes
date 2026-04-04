using Microsoft.AspNetCore.Identity;
using NetKubernetes.Models;

namespace NetKubernetes.Data
{
    public class LoadDataBase
    {
        public static async Task InserirDados(AppDbContext context, UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usario = new Usuario
                {
                    Nome = "Christian",
                    Sobrenome = "Santos",
                    Email = "z5RbM@example.com",
                    UserName = "z5RbM@example.com",
                    Telefone = "11999999999"
                };
                await usuarioManager.CreateAsync(usario, "123456aA@");                    
               
            }
            if(!context.Imoveis!.Any())
            {
               context.Imoveis!.AddRange(
                    new Imovel
                    {
                        Nome = "Apartamento 1",
                        Descricao = "Apartamento com 2 quartos, sala, cozinha e banheiro.",
                        Endereco = "Rua A, 123",
                        Valor = 250000.00m,
                        DatadeCriacao = DateTime.Now,
                    },
                    new Imovel
                    {
                        Nome = "Casa 1",
                        Descricao = "Casa com 3 quartos, sala, cozinha, banheiro e garagem.",
                        Endereco = "Rua B, 456",
                        Valor = 500000.00m,
                        DatadeCriacao = DateTime.Now,
                    }               
               );
            }
           context.SaveChanges();
        }
    }
}