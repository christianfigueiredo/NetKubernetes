using NetKubernetes.Models;
using NetKubernetes.Token;

namespace NetKubernetes.Data.Imoveis
{
    
   
    public class ImovelRepository : IImovelRepository
    {
        private readonly AppDbContext _contexto;
        private readonly IUsuarioSessao _usuarioSessao;
        public ImovelRepository(
            AppDbContext contexto,
            IUsuarioSessao usuarioSessao
            )
        
            {
                 _contexto = contexto;
                 _usuarioSessao = usuarioSessao;
                
            }
        public void AdicionarImovel(Imovel imovel)
        {
            imovel.DataCriacao = DateTime.Now;
            //imovel.UsuarioId = _usuarioSessao.ObterUsuarioSessao();
            
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