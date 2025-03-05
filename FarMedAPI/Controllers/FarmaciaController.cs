using Microsoft.AspNetCore.Mvc;
using FarMedAPI.Service;
using FarMedAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmaciaController : ControllerBase
    {
        private readonly IFarmaciaService _farmaciaService;

        public FarmaciaController(IFarmaciaService farmaciaService)
        {
            _farmaciaService = farmaciaService;
        }

        // Obtener todas las farmacias
        [HttpGet]
        public async Task<ActionResult<List<Farmacia>>> GetFarmacias()
        {
            var farmacias = await _farmaciaService.GetAllAsync();
            return Ok(farmacias);
        }

        // Obtener una farmacia por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Farmacia>> GetFarmacia(int id)
        {
            var farmacia = await _farmaciaService.GetByIdAsync(id);
            if (farmacia == null)
            {
                return NotFound();
            }
            return Ok(farmacia);
        }

        // Crear una nueva farmacia
        [HttpPost]
        public async Task<ActionResult<Farmacia>> CreateFarmacia(Farmacia farmacia)
        {
            await _farmaciaService.AddAsync(farmacia);
            return CreatedAtAction(nameof(GetFarmacia), new { id = farmacia.Id_Farmacia }, farmacia);
        }

        // Actualizar una farmacia existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFarmacia(int id, Farmacia updatedFarmacia)
        {
            var existingFarmacia = await _farmaciaService.GetByIdAsync(id);
            if (existingFarmacia == null)
            {
                return NotFound();
            }

            // Actualizar datos de la farmacia
            existingFarmacia.Nombre = updatedFarmacia.Nombre;
            existingFarmacia.Dirección = updatedFarmacia.Dirección;
            existingFarmacia.Teléfono = updatedFarmacia.Teléfono;
            existingFarmacia.Horario_Apertura = updatedFarmacia.Horario_Apertura;
            existingFarmacia.Horario_Cierre = updatedFarmacia.Horario_Cierre;

            await _farmaciaService.UpdateAsync(existingFarmacia);
            return NoContent();
        }

        // Eliminar una farmacia
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFarmacia(int id)
        {
            var farmacia = await _farmaciaService.GetByIdAsync(id);
            if (farmacia == null)
            {
                return NotFound();
            }
            await _farmaciaService.DeleteAsync(id);
            return NoContent();
        }
    }
}
