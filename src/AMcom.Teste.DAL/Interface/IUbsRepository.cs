using System.Collections.Generic;
using AMcom.Teste.DAL.Model;
using GeoCoordinatePortable;

namespace AMcom.Teste.DAL.Interface
{
    public interface IUbsRepository
    {
        List<Ubs> Buscar(GeoCoordinate coordenadaBuscada, int quantidadeItens);
    }
}