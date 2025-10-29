using Microsoft.AspNetCore.Identity;
using NetKubernetes.Models;
using NetKubernetes.Token;

namespace NetKubernetes.Data.Imoveis
{
    
   
    public class ImovelRepository : IImovelRepository
    {
        private readonly AppDbContext _contexto;
        private readonly IUsuarioSessao _usuarioSessao;
        private readonly UserManager<Usuario> _userManager;
        public ImovelRepository(
            AppDbContext contexto,
            IUsuarioSessao usuarioSessao,
            UserManager<Usuario> userManager
            )
        
            {
                 _contexto = contexto;
                 _usuarioSessao = usuarioSessao;
                 _userManager = userManager;
                
            }
        public async Task AdicionarImovel(Imovel imovel)
        {
            var usuario = await _userManager.FindByNameAsync(_usuarioSessao.ObterUsuarioSessao());

            imovel.DataCriacao = DateTime.Now;
            imovel.UsuarioId = Guid.Parse(usuario!.Id);

            await _contexto.Imoveis!.AddAsync(imovel);
            
        }

        public void DeleteImovel(int id)
        {
            var imovel = _contexto.Imoveis!.FirstOrDefault(i => i.Id == id);

            _contexto.Imoveis!.Remove(imovel!);        


        }

        public IEnumerable<Imovel> ListarTodos()
        {
            
            return _contexto.Imoveis!.ToList();
        }

        public Imovel? ObterImovelPorId(int id)
        {
            return _contexto.Imoveis!.FirstOrDefault(i => i.Id == id);
        }

        public bool SaveChanges()
        {
            return _contexto.SaveChanges() >= 0;
        }

        
    }
}