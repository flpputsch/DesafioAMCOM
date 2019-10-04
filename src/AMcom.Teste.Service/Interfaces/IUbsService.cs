using System.Collections.Generic;
using AMcom.Teste.Service.DTOs;

namespace AMcom.Teste.Service.Interfaces
{
    public interface IUbsService
    {
        List<UbsDTO> Buscar(double latitude, double longitude, int quantidadeItens = 5);
    }
}