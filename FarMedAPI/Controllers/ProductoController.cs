using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;
using FarMedAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            try
            {
                var productos = await _productoService.GetAllProductosAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener los productos: {ex.Message}");
            }
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            try
            {
                var producto = await _productoService.GetProductoByIdAsync(id);

                if (producto == null)
                {
                    return NotFound($"No se encontró el producto con ID {id}");
                }

                return Ok(producto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener el producto: {ex.Message}");
            }
        }

        // GET: api/Producto/categoria/5
        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductosByCategoria(int categoriaId)
        {
            try
            {
                var productos = await _productoService.GetProductosByCategoriaAsync(categoriaId);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener los productos por categoría: {ex.Message}");
            }
        }

        // POST: api/Producto
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            try
            {
                await _productoService.CreateProductoAsync(producto);
                return CreatedAtAction(nameof(GetProducto), new { id = producto.Id_Producto }, producto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al crear el producto: {ex.Message}");
            }
        }

        // PUT: api/Producto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id_Producto)
            {
                return BadRequest("El ID del producto no coincide con el ID de la URL");
            }

            try
            {
                await _productoService.UpdateProductoAsync(producto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el producto: {ex.Message}");
            }
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                await _productoService.DeleteProductoAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar el producto: {ex.Message}");
            }
        }
    }
}