using NetKubernetes.Models;

namespace NetKubernetes.Data.Imoveis
{
    public interface IImovelRepository
    {
        bool SaveChanges();
        Task<IEnumerable<Imovel>> GetAllImoveisAsync();
        Imovel GetImovelById(int id);
        Task CreateImovelAsync(Imovel imovel);       
        Task DeleteImovelAsync(int id);
    }
}