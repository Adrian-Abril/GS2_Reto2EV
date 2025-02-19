using Microsoft.AspNetCore.Mvc;
using FarMedAPI.Service;
using FarMedAPI.Models;

namespace FarMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidoController : ControllerBase
    {
        private readonly IDetallePedidoService _detallepedidoService;

        public DetallePedidoController(IDetallePedidoService detallepedidoService)
        {
            _detallepedidoService = detallepedidoService;
        }

        // Obtener todos los clientes
        [HttpGet]
        public async Task<ActionResult<List<DetallePedido>>> GetDetallePedidos()
        {
            var detallepedido = await _detallepedidoService.GetAllAsync();
            return Ok(detallepedido);
        }

        // Obtener un cliente por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePedido>> GetCliente(int id)
        {
            var detallepedido = await _detallepedidoService.GetByIdAsync(id);
            if (detallepedido == null)
            {
                return NotFound();
            }
            return Ok(detallepedido);
        }

        // Crear un nuevo cliente
        [HttpPost]
        public async Task<ActionResult<DetallePedido>> CreateDetallePedido(DetallePedido detallePedido)
        {
            await _detallepedidoService.AddAsync(detallePedido);
            return CreatedAtAction(nameof(GetCliente), new { id = detallePedido.Id_Detalle }, detallePedido);
        }

        // Actualizar un cliente existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDetallePedido(int id, DetallePedido updatedDetallePedido)
        {
            var existingDetallePedido = await _detallepedidoService.GetByIdAsync(id);
            if (existingDetallePedido == null)
            {
                return NotFound();
            }

            // Actualizar datos del cliente
            existingDetallePedido.Id_Pedido = updatedDetallePedido.Id_Pedido;
            existingDetallePedido.Id_Producto = updatedDetallePedido.Id_Producto;
            existingDetallePedido.Cantidad = updatedDetallePedido.Cantidad;
            existingDetallePedido.Precio_Unitario = updatedDetallePedido.Precio_Unitario;

            await _detallepedidoService.UpdateAsync(existingDetallePedido);
            return NoContent();
        }

        // Eliminar un cliente
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _detallepedidoService.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            await _detallepedidoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
