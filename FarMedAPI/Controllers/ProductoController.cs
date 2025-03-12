using Microsoft.AspNetCore.Mvc;
using FarMedAPI.Services;
using FarMedAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetAllProductosAsync()
        {
            return Ok(await _productoService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProductoByIdAsync(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpGet("ByCategoria/{categoriaId}")]
        public async Task<ActionResult<List<Producto>>> GetProductosByCategoriaAsync(int categoriaId)
        {
            return Ok(await _productoService.GetByCategoriaIdAsync(categoriaId));
        }

        [HttpGet("ByLaboratorio/{laboratorioId}")]
        public async Task<ActionResult<List<Producto>>> GetProductosByLaboratorioAsync(int laboratorioId)
        {
            return Ok(await _productoService.GetByLaboratorioIdAsync(laboratorioId));
        }

        [HttpPost]
        public async Task<ActionResult> AddProductoAsync([FromBody] Producto producto)
        {
            await _productoService.AddAsync(producto);
            return CreatedAtAction(nameof(GetProductoByIdAsync), new { id = producto.ProductoId }, producto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductoAsync(int id, [FromBody] Producto producto)
        {
            producto.ProductoId = id;
            await _productoService.UpdateAsync(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductoAsync(int id)
        {
            if (!await _productoService.DeleteAsync(id))
                return NotFound();
            return NoContent();
        }
    }
}
