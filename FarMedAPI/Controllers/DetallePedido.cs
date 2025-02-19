using Microsoft.AspNetCore.Mvc;
using FarMedAPI.Models;
using FarMedAPI.Service;

namespace FarMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidoController : ControllerBase
    {
        private readonly IDetallePedidoService _detallePedidoService;

        public DetallePedidoController(IDetallePedidoService detallePedidoService)
        {
            _detallePedidoService = detallePedidoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallePedido>>> GetDetallesPedido()
        {
            var detalles = await _detallePedidoService.GetAll();
            return Ok(detalles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePedido>> GetDetallePedido(int id)
        {
            var detalle = await _detallePedidoService.GetById(id);
            if (detalle == null)
            {
                return NotFound();
            }
            return Ok(detalle);
        }

        [HttpPost]
        public async Task<ActionResult<DetallePedido>> PostDetallePedido([FromBody] DetallePedido detallePedido)
        {
            var nuevoDetalle = await _detallePedidoService.Create(detallePedido);
            return CreatedAtAction(nameof(GetDetallePedido), new { id = nuevoDetalle.Id_Detalle }, nuevoDetalle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallePedido(int id, [FromBody] DetallePedido detallePedido)
        {
            if (id != detallePedido.Id_Detalle)
            {
                return BadRequest();
            }

            var updated = await _detallePedidoService.Update(detallePedido);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallePedido(int id)
        {
            var deleted = await _detallePedidoService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
