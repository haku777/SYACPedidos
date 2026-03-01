using Microsoft.AspNetCore.Mvc;
using PedidosSYAC.Common.Dto.Estados;
using PedidosSYAC.Services.Services.Interfaces;

namespace PedidosSYAC.Controllers
{
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly IEstados _estados;
        public EstadosController(IEstados estados) { _estados = estados; }

        [HttpGet]
        [Route("GetEstados")]
        public Task<List<EstadosDto>> GetEstados()
        {
            var estados = _estados.Get();
            return estados;
        }

        [HttpGet]
        [Route("GetEstadosFiltered")]
        public async Task<List<EstadosSoloNombreDto>> GetAllByDto() { 
            var estados = await _estados.GetAllByDto();
            return estados;
        }



    }
}
