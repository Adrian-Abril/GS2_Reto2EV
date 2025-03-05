using Microsoft.AspNetCore.Mvc;
using FarMedAPI.Service;
using FarMedAPI.Models;

namespace FarMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntregaController : ControllerBase
    {
        private readonly IEntregaService _entregaService;

        public EntregaController(IEntregaService entregaService)
        {
            _entregaService = entregaService;
        }

        // Obtener todas las entregas
        [HttpGet]
        public async Task<ActionResult<List<Entrega>>> GetEntregas()
        {
            var entregas = await _entregaService.GetAllAsync();
            return Ok(entregas);
        }

        // Obtener una entrega por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Entrega>> GetEntrega(int id)
        {
            var entrega = await _entregaService.GetByIdAsync(id);
            if (entrega == null)
            {
                return NotFound();
            }
            return Ok(entrega);
        }

        // Crear una nueva entrega
        [HttpPost]
        public async Task<ActionResult<Entrega>> CreateEntrega(Entrega entrega)
        {
            await _entregaService.AddAsync(entrega);
            return CreatedAtAction(nameof(GetEntrega), new { id = entrega.Id_Entrega }, entrega);
        }

        // Actualizar una entrega existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntrega(int id, Entrega updatedEntrega)
        {
            var existingEntrega = await _entregaService.GetByIdAsync(id);
            if (existingEntrega == null)
            {
                return NotFound();
            }

            // Actualizar datos de la entrega
            existingEntrega.Id_Pedido = updatedEntrega.Id_Pedido;
            existingEntrega.Fecha_Entrega = updatedEntrega.Fecha_Entrega;
            existingEntrega.Estado = updatedEntrega.Estado;
            existingEntrega.Id_Empleado = updatedEntrega.Id_Empleado;

            await _entregaService.UpdateAsync(existingEntrega);
            return NoContent();
        }

        // Eliminar una entrega
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrega(int id)
        {
            var entrega = await _entregaService.GetByIdAsync(id);
            if (entrega == null)
            {
                return NotFound();
            }
            await _entregaService.DeleteAsync(id);
            return NoContent();
        }
    }
}
