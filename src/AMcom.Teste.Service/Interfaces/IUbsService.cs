using System.Collections.Generic;
using AMcom.Teste.Service.DTOs;

namespace AMcom.Teste.Service.Interfaces
{
    public interface IUbsService
    {
        List<UbsDTO> Buscar(CoordenadasDTO coordenadas, int quantidadeItens = 5);
    }
}