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

        /// <summary>
        /// Busca as 5 Ubs mais proximas com melhor nota
        /// </summary>
        /// <param name="latitude">Latitude de pesquisa</param>
        /// <param name="longitude">Longitude de pesquisa</param>
        /// <returns>Dados das Ubs mais proximas com melhor nota</returns>
        [HttpGet("latitude/{latitude:double}/longitude/{longitude:double}")]
        public IActionResult Get(double latitude, double longitude)
        {
            try
            {
                //Busca os dados com base na latitude e longitude
                var dados = _ubsService.Buscar(latitude, longitude);
                //Retorna os dados que vão ser mostrados caso não tenha erros na sessão
                return Response(dados);
            }
            catch (Exception e)
            {
                //Em caso de exceção, da um return que vai buscar os erros da sessão
                _erroHandler.AdicionarErro(e);
                return Response();
            }
        }
    }
}