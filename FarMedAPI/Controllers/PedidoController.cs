using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;
using FarMedAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FarMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoRepository _pedidoRepository;

        public PedidoController(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        // GET: api/Pedido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            var pedidos = await _pedidoRepository.GetAllPedidos();
            return Ok(pedidos);
        }

        // GET: api/Pedido/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _pedidoRepository.GetPedidoById(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // POST: api/Pedido
        [HttpPost]
        public async Task<ActionResult<Pedido>> CreatePedido(Pedido pedido)
        {
            var result = await _pedidoRepository.CreatePedido(pedido);
            return CreatedAtAction(nameof(GetPedido), new { id = result.Id_Pedido }, result);
        }

        // PUT: api/Pedido/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedido(int id, Pedido pedido)
        {
            if (id != pedido.Id_Pedido)
            {
                return BadRequest();
            }

            var result = await _pedidoRepository.UpdatePedido(pedido);
            return Ok(result);
        }

        // DELETE: api/Pedido/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var result = await _pedidoRepository.DeletePedido(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}