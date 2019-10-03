using System.Collections.Generic;
using AMcom.Teste.DAL.Model;

namespace AMcom.Teste.DAL.Interface
{
    public interface IUbsRepository
    {
        List<Ubs> Buscar(decimal coordenadasLatitude, decimal coordenadasLongitude, int quantidadeItens);
    }
}