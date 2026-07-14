using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Models;
using NetKubernetes.Token;

namespace NetKubernetes.Data.Imoveis
{
    public class ImovelRepository : IImovelRepository
    {
        private readonly DbContexto _context;
        private readonly IUsuarioSessao _usuarioSessao; 
        private readonly UserManager<Usuario> _userManager;
        public ImovelRepository(DbContexto context, IUsuarioSessao usuarioSessao, UserManager<Usuario> userManager)
        {
            _context = context;
            _usuarioSessao = usuarioSessao;
            _userManager = userManager;
        }
        public async Task CreateImovelAsync(Imovel imovel)
        {
            var usuario = await _userManager.FindByNameAsync(_usuarioSessao.ObterUsuarioSessao());

            imovel.DataCriacao = DateTime.Now;
            imovel.UsuarioId = Guid.Parse(usuario!.Id);

            await _context.Imoveis.AddAsync(imovel);            
            
        }

        public async Task DeleteImovelAsync(int id)
        {
            var imovel = await _context.Imoveis.FindAsync(id);
            if (imovel != null)
            {
                _context.Imoveis.Remove(imovel);
            }           
        }

        public async Task<IEnumerable<Imovel>> GetAllImoveisAsync()
        {
            return await _context.Imoveis!.ToListAsync();
        }

        public Imovel GetImovelById(int id)
        {
            return _context.Imoveis!.FirstOrDefault(i => i.Id == id)!;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}