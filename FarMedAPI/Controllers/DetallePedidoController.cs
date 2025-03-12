using Microsoft.AspNetCore.Mvc;
using FarMedAPI.Services;
using FarMedAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("{pedidoId}")]
        public async Task<ActionResult<List<DetallePedido>>> GetDetallesByPedidoAsync(int pedidoId)
        {
            return Ok(await _detallePedidoService.GetByPedidoIdAsync(pedidoId));
        }
    }
}
