using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Models;
using NetKubernetes.Token;

namespace NetKubernetes.Data.Imoveis
{
    public class ImovelRepository : IImovelRepository
    {
        private readonly AppDbContext _context;
        private readonly IUsuarioSessao _usuarioSessao;
        private readonly UserManager<Usuario> _userManager;

        public ImovelRepository(AppDbContext context, IUsuarioSessao usuarioSessao, 
                                UserManager<Usuario> userManager)
        {
            _context = context;
            _usuarioSessao = usuarioSessao;
            _userManager = userManager;
        }
        public async Task AddImovelAsync(Imovel imovel)
        {
            var usuario = await _userManager.FindByNameAsync(_usuarioSessao.ObterUsuarioSessao());

            imovel.DatadeCriacao = DateTime.Now;
            imovel.UsuarioId = Guid.Parse(usuario!.Id);     

            _context.Imoveis!.Add(imovel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteImovelAsync(int id)
        {
            var imovel = await _context.Imoveis!.FirstOrDefaultAsync(i => i.Id == id);
            if (imovel != null)
            {
                _context.Imoveis!.Remove(imovel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Imovel>> GetAllImoveisAsync()
        {
            return await _context.Imoveis!.ToListAsync();
        }

        public async Task<Imovel?> GetImovelByIdAsync(int id)
        {
            return await _context.Imoveis!.FirstOrDefaultAsync(i => i.Id == id);
          
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public async Task UpdateImovelAsync(Imovel imovel)
        {
            _context.Imoveis!.Update(imovel);
            await _context.SaveChangesAsync();
        }
    }
}