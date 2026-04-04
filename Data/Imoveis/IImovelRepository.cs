using NetKubernetes.Models;

namespace NetKubernetes.Data.Imoveis
{
    public interface IImovelRepository
    {
            Task<IEnumerable<Imovel>> GetAllImoveisAsync();
            Task<Imovel?> GetImovelByIdAsync(int id);
            Task AddImovelAsync(Imovel imovel);
            Task UpdateImovelAsync(Imovel imovel);
            Task DeleteImovelAsync(int id);
            bool SaveChanges();
    }
}