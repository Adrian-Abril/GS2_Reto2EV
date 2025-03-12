using Microsoft.AspNetCore.Mvc;
using FarMedAPI.Services;
using FarMedAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedidoByIdAsync(int id)
        {
            var pedido = await _pedidoService.GetByIdAsync(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePedidoAsync([FromBody] Pedido pedido)
        {
            await _pedidoService.CreateAsync(pedido);
            return CreatedAtAction(nameof(GetPedidoByIdAsync), new { id = pedido.PedidoId }, pedido);
        }
    }
}
