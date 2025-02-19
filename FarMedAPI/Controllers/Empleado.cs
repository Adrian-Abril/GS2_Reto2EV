using Microsoft.AspNetCore.Mvc;
using FarMedAPI.Service;
using FarMedAPI.Models;

namespace FarMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        // Obtener todos los clientes
        [HttpGet]
        public async Task<ActionResult<List<Empleado>>> GetEmpleados()
        {
            var empleados = await _empleadoService.GetAllAsync();
            return Ok(empleados);
        }

        // Obtener un cliente por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            var empleado = await _empleadoService.GetByIdAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return Ok(empleado);
        }

        // Crear un nuevo cliente
        [HttpPost]
        public async Task<ActionResult<Empleado>> CreateEmpleado(Empleado empleado)
        {
            await _empleadoService.AddAsync(empleado);
            return CreatedAtAction(nameof(GetEmpleado), new { id = empleado.Id_Empleado }, empleado);
        }

        // Actualizar un cliente existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpleado(int id, Empleado updatedEmpleado)
        {
            var existingEmpleado = await _empleadoService.GetByIdAsync(id);
            if (existingEmpleado == null)
            {
                return NotFound();
            }

            // Actualizar datos del cliente
            existingEmpleado.Nombre = updatedEmpleado.Id_Farmacia;
            existingEmpleado.Email = updatedEmpleado.Nombre;
            existingEmpleado.Contraseña = updatedEmpleado.Teléfono;
            existingEmpleado.Teléfono = updatedEmpleado.Rol;

            await _empleadoService.UpdateAsync(existingEmpleado);
            return NoContent();
        }

        // Eliminar un cliente
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var empleado = await _empleadoService.GetByIdAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            await _empleadoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
