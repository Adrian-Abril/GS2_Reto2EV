using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Service;
using FarMedAPI.Models;

namespace FarMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // Obtener todos los clientes
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetClientes()
        {
            var clientes = await _clienteService.GetAllAsync();
            return Ok(clientes);
        }

        // Obtener un cliente por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        // Crear un nuevo cliente
        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
        {
            await _clienteService.AddAsync(cliente);
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id_Cliente }, cliente);
        }

        // Actualizar un cliente existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, Cliente updatedCliente)
        {
            var existingCliente = await _clienteService.GetByIdAsync(id);
            if (existingCliente == null)
            {
                return NotFound();
            }

            // Actualizar datos del cliente
            existingCliente.Nombre = updatedCliente.Nombre;
            existingCliente.Email = updatedCliente.Email;
            existingCliente.Contraseña = updatedCliente.Contraseña;
            existingCliente.Teléfono = updatedCliente.Teléfono;
            existingCliente.Tipo_Usuario = updatedCliente.Tipo_Usuario;
            existingCliente.Dirección = updatedCliente.Dirección;

            await _clienteService.UpdateAsync(existingCliente);
            return NoContent();
        }

        // Eliminar un cliente
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            await _clienteService.DeleteAsync(id);
            return NoContent();
        }
    }
}
