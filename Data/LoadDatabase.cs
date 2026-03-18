using Microsoft.AspNetCore.Identity;
using NetKubernetes.Models;

namespace NetKubernetes.Data;

    public class LoadDatabase 
    {
        public static async Task InsertData(AppDbContext context, UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    Nome = "Christian",
                    Sobrenome = "Santos",
                    Email = "HdV0M@example.com",
                    UserName = "Santos.Christian",
                    Telefone = "11999999999"
                };
                await usuarioManager.CreateAsync(usuario, "Admin123!");
            }
            if (!context.Imoveis!.Any())
            {
                context.Imoveis!.AddRange(
                    new Imovel
                    {
                        Nome = "Casa de Praia",
                        Descricao = "Casa de praia com vista para o mar",
                        Endereco = "Rua do Mar, 123",
                        Preco = 500000.00m,
                        Tipo = "Casa",
                        ImagemUrl = "https://example.com/casa-praia.jpg"
                    },
                    new Imovel
                    {
                        Nome = "Apartamento no Centro",
                        Descricao = "Apartamento moderno no centro da cidade",
                        Endereco = "Avenida Central, 456",
                        Preco = 300000.00m,
                        Tipo = "Apartamento",
                        ImagemUrl = "https://example.com/apartamento-centro.jpg"
                    }
                );
            }
            await context.SaveChangesAsync();
        }
        
    }
