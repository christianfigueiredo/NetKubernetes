using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Models;

namespace NetKubernetes.Data
{
    public class DbContexto : IdentityDbContext<Usuario>
    {
        public DbContexto(DbContextOptions<DbContexto> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Imovel> Imoveis { get; set; }
    }
}