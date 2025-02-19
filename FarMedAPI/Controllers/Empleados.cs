using Microsoft.AspNetCore.Mvc;
using FarMedAPI.Models;
using RestauranteAPI.Repositories;
using FarMedAPI.Service;

namespace FarMedAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadosService _empleadosService;
        public EmpleadosController(IEmpleadosService empleadosService)
        {
            _empleadosService = empleadosService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
            var empleado = await _empleadosService.GetAllAsync();
            return Ok(empleado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            var empleado == await _empleadosService.GetById(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return Ok(empleado);
        }

        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleado([FromBody] Empleado empleado)
        {
            var nuevoEmpleado = await _empleadosService.Create
        }   
    }
}