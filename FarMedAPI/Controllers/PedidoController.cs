using Microsoft.AspNetCore.Mvc;
using FarMedAPI.Service;
using FarMedAPI.Models;

namespace FarMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // Obtener todos los pedidos
        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> GetPedidos()
        {
            var pedidos = await _pedidoService.GetAllAsync();
            return Ok(pedidos);
        }

        // Obtener un pedido por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _pedidoService.GetByIdAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        // Crear un nuevo pedido
        [HttpPost]
        public async Task<ActionResult<Pedido>> CreatePedido(Pedido pedido)
        {
            await _pedidoService.AddAsync(pedido);
            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id_Pedido }, pedido);
        }

        // Actualizar un pedido existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedido(int id, Pedido updatedPedido)
        {
            var existingPedido = await _pedidoService.GetByIdAsync(id);
            if (existingPedido == null)
            {
                return NotFound();
            }

            // Actualizar datos del pedido
            existingPedido.Id_Cliente = updatedPedido.Id_Cliente;
            existingPedido.Id_Farmacia = updatedPedido.Id_Farmacia;
            existingPedido.Fecha_Pedido = updatedPedido.Fecha_Pedido;
            existingPedido.Estado = updatedPedido.Estado;
            existingPedido.Total = updatedPedido.Total;
            existingPedido.Dirección_Entrega = updatedPedido.Dirección_Entrega;

            await _pedidoService.UpdateAsync(existingPedido);
            return NoContent();
        }

        // Eliminar un pedido
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _pedidoService.GetByIdAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            await _pedidoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
