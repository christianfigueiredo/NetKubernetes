using NetKubernetes.Models;

namespace NetKubernetes.Data.Imoveis
{
    public interface IImovelRepository
    {
        bool SaveChanges();
        void AdicionarImovel(Imovel imovel);
        IEnumerable<Imovel> ListarTodos();
        Imovel? ObterImovelPorId(int id);
        void DeleteImovel(int id); 
    }
}