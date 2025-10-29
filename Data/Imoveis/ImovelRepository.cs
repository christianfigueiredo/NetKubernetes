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
            throw new NotImplementedException();
        }

        public IEnumerable<Imovel> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public Imovel? ObterImovelPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}