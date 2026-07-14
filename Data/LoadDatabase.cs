using Microsoft.AspNetCore.Identity;
using NetKubernetes.Models;

namespace NetKubernetes.Data
{
    public class LoadDatabase
    {
        public static async Task InserirDados(DbContexto context, UserManager<Usuario> usuarioManager)
        {

            if(!usuarioManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    Nome = "Christian",
                    Sobrenome = "Figueiredo",
                    Email = "chris@gnmail.com",
                    UserName = "chris.figueiredo",
                    Telefone = "123456789"
                };
                await usuarioManager.CreateAsync(usuario, "paSSword123$");
            }
            if(!context.Imoveis.Any())
            {
                context.Imoveis!.AddRange(
                    new Imovel { Nome = "Casa 1", Endereco = "Rua A, 123", Preco = 250000.00M, UrlImagem = "https://example.com/casa1.jpg", DataCriacao = DateTime.Now },
                    new Imovel { Nome = "Apartamento 2", Endereco = "Avenida B, 456", Preco = 150000.00M, UrlImagem = "https://example.com/apartamento2.jpg", DataCriacao = DateTime.Now },
                    new Imovel { Nome = "Casa 3", Endereco = "Rua C, 789", Preco = 300000.00M, UrlImagem = "https://example.com/casa3.jpg", DataCriacao = DateTime.Now }
                );
                await context.SaveChangesAsync();
            }
        }        
    }
}