using System;
using AMcom.Teste.DAL.Interface;
using AMcom.Teste.Service.DTOs;
using AMcom.Teste.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using GeoCoordinatePortable;

namespace AMcom.Teste.Service.Services
{
    public class UbsService : IUbsService
    {
        private readonly IUbsRepository _ubsRepository;
        public readonly IErroHandler _erroHandler;

        public UbsService(IUbsRepository ubsRepository, IErroHandler erroHandler)
        {
            _ubsRepository = ubsRepository;
            _erroHandler = erroHandler;
        }

        /// <summary>
        /// Busca os dados da Ubs com base na latitude e longitude e avaliação
        /// </summary>
        /// <param name="latitude">Longitude de pesquisa</param>
        /// <param name="longitude">Latitude de pesquisa</param>
        /// <param name="quantidadeItens">Quantidade de itens para buscar, com padrão 5</param>
        /// <returns>Dados das Ubs mais próximas das coordenadas informadas</returns>
        public List<UbsDTO> Buscar(double latitude, double longitude, int quantidadeItens = 5)
        {
            try
            {
                //Busca os dados e converte no Dto
                return _ubsRepository.Buscar(new GeoCoordinate(latitude, longitude), quantidadeItens)
                    .Select(x => (UbsDTO)x).ToList();
            }
            catch (ArgumentOutOfRangeException e)
            {
                //Caso a latitude e longitude seja inválido, da exceção e adiciona no handler
                _erroHandler.AdicionarErro($"{e.ParamName} informado é inválido!");
            }
            catch (Exception e)
            {
                //Outros erros adiciona no handler
                _erroHandler.AdicionarErro(e);
            }
            //Em caso de erro, retorna padrão
            return new List<UbsDTO>();
        }
    }
}