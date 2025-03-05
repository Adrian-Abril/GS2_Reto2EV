using Microsoft.AspNetCore.Mvc;
using FarMedAPI.Service;
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

        // Obtener todos los productos
        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetProductos()
        {
            var productos = await _productoService.GetAllAsync();
            return Ok(productos);
        }

        // Obtener un producto por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        // Crear un nuevo producto
        [HttpPost]
        public async Task<ActionResult<Producto>> CreateProducto(Producto producto)
        {
            await _productoService.AddAsync(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id_Producto }, producto);
        }

        // Actualizar un producto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, Producto updatedProducto)
        {
            var existingProducto = await _productoService.GetByIdAsync(id);
            if (existingProducto == null)
            {
                return NotFound();
            }

            // Actualizar datos del producto
            existingProducto.Id_Farmacia = updatedProducto.Id_Farmacia;
            existingProducto.Nombre = updatedProducto.Nombre;
            existingProducto.Descripción = updatedProducto.Descripción;
            existingProducto.Precio = updatedProducto.Precio;
            existingProducto.Stock = updatedProducto.Stock;

            await _productoService.UpdateAsync(existingProducto);
            return NoContent();
        }

        // Eliminar un producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            await _productoService.DeleteAsync(id);
            return NoContent();
        }
    }
}