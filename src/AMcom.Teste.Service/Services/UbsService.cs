using System.Collections.Generic;
using AMcom.Teste.DAL.Interface;
using AMcom.Teste.Service.DTOs;
using AMcom.Teste.Service.Interfaces;

namespace AMcom.Teste.Service.Services
{
    public class UbsService : IUbsService
    {
        private readonly IUbsRepository _ubsRepository;

        public UbsService(IUbsRepository ubsRepository)
        {
            _ubsRepository = ubsRepository;
        }

        // Implemente um método que retorne as 5 UBSs mais próximas de um ponto (latitude e longitude) que devem
        // ser passados como parâmetro para o método e retorne uma lista/coleção de objetos do tipo UbsDTO.
        // Esta lista deve estar ordenada pela avaliação (da melhor para a pior) de acordo com os dados que constam
        // no objeto retornado pela camada de acesso a dados (DAL).
        public List<UbsDTO> Buscar(CoordenadasDTO coordenadas, int quantidadeItens = 5)
        {
            var dados = _ubsRepository.Buscar(coordenadas.Latitude, coordenadas.Longitude, quantidadeItens);
            return new List<UbsDTO>();
        }
    }
}