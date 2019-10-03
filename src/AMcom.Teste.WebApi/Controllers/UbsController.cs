using System;
using AMcom.Teste.Service.DTOs;
using AMcom.Teste.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AMcom.Teste.WebApi.Controllers
{
    [Route("api/ubs")]
    public class UbsController : ApiController
    {
        private readonly IErroHandler _erroHandler;
        private readonly IUbsService _ubsService;

        public UbsController(IErroHandler erroHandler, IUbsService ubsService) : base(erroHandler)
        {
            _erroHandler = erroHandler;
            _ubsService = ubsService;
        }

        [HttpGet("latitude/{latitude:decimal}/longitude/{longitude:decimal}")]
        public IActionResult Get(decimal latitude, decimal longitude)
        {
            try
            {
                var dados = _ubsService.Buscar(new CoordenadasDTO(latitude, longitude));
                return Response(dados);
            }
            catch (Exception e)
            {
                _erroHandler.AdicionarErro(e);
                return Response();
            }
        }

        // Implemente um método que seja acessível por HTTP GET e retorne a lista das 5 UBSs
        // (Unidades Básicas de Saúde) mas próximas de um ponto (latitude e longitude) e ordenada
        // por avaliação (da melhor para a pior). O retorno deve ser no formato JSON.
    }
}