using Microsoft.AspNetCore.Mvc;
using FarMedAPI.Services;
using FarMedAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetAllUsuariosAsync()
        {
            return Ok(await _usuarioService.GetAllAsync());
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] Usuario usuario)
        {
            await _usuarioService.RegisterAsync(usuario);
            return CreatedAtAction(nameof(GetAllUsuariosAsync), new { id = usuario.UsuarioId }, usuario);
        }
    }
}
