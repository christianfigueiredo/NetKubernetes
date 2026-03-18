using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetKubernetes.Data.Imoveis
{
    public interface IImovelRepository 
    {
        Task<List<Imovel>> ObterImoveis();
        bool SaveChanges();
                
    }
}