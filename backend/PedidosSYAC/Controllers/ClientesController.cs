using Microsoft.AspNetCore.Mvc;
using PedidosSYAC.Common.Dto.Clientes;
using PedidosSYAC.Services.Services.Interfaces;

namespace PedidosSYAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientes _cliente;
        public ClientesController(IClientes cliente) {
            _cliente = cliente;
        }

        [HttpGet]
        [Route("GetClientes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ClientesDto>>> GetClientes() {

            return await _cliente.Get();
        }

        [HttpGet]
        [Route("GetClienteByIdentificacion/{identificacion}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientesDto>> GetClienteByIdentificacion(string Identificacion)
        {
            if (string.IsNullOrEmpty(Identificacion))
                return NotFound();

            var result = await _cliente.GetByIdentificacion(Identificacion);

            if (result == null)
                return NotFound();

            return result;
        }

        [HttpPost]
        [Route("AddCliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientesDto>> AddCliente([FromBody] ClientesCreacionDto cliente)
        {

            if (cliente == null)
                return BadRequest();

            var result = await _cliente.AddCliente(cliente);

            if (result == null)
                return BadRequest();

            return result;
        }


        [HttpPut]
        [Route("UpdateCliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAutor([FromBody] ClientesActualizarDto cliente)
        {
            if (cliente == null)
                return NotFound();

            var result = await _cliente.GetByIdentificacion(cliente.Identificacion);

            if (result == null)
                return BadRequest();

            await _cliente.UpdateCliente(cliente);

            return NoContent();
        }


        [HttpDelete]
        [Route("DeleteCliente/{Identificacion}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCliente(string Identificacion)
        {
            if (string.IsNullOrEmpty(Identificacion))
                return BadRequest();

            int eliminado = await _cliente.DeleteClienteAsync(Identificacion);
            if (eliminado == 0)
                return NotFound();

            return Ok(new { message = "Cliente eliminado correctamente"});
        }
    }
}
