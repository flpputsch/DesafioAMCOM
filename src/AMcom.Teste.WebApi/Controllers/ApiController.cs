using AMcom.Teste.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AMcom.Teste.WebApi.Controllers
{
    public class ApiController : ControllerBase
    {
        protected readonly IErroHandler ErroHandler;

        public ApiController(IErroHandler erroHandler)
        {
            ErroHandler = erroHandler;
        }

        protected IActionResult Response(object data = null)
        {
            if (ErroHandler.TemErro())
            {
                return BadRequest(ErroHandler.BuscarErros());
            }
            else
            {
                return Ok(data);
            }
        }
    }
}