using Microsoft.AspNetCore.Mvc;
using FarMedAPI.Services;
using FarMedAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> GetAllCategoriasAsync()
        {
            return Ok(await _categoriaService.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult> AddCategoriaAsync([FromBody] Categoria categoria)
        {
            await _categoriaService.AddAsync(categoria);
            return CreatedAtAction(nameof(GetAllCategoriasAsync), new { id = categoria.CategoriaId }, categoria);
        }
    }
}
