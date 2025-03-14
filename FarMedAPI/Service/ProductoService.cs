using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;
using FarMedAPI.Repository;

namespace FarMedAPI.Service
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<List<Producto>> GetAllProductosAsync()
        {
            return await _productoRepository.GetAllAsync();
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            return await _productoRepository.GetByIdAsync(id);
        }

        public async Task<List<Producto>> GetProductosByCategoriaAsync(int categoriaId)
        {
            return await _productoRepository.GetByCategoriaAsync(categoriaId);
        }

        public async Task CreateProductoAsync(Producto producto)
        {
            // Validar datos del producto
            if (string.IsNullOrEmpty(producto.Nombre))
                throw new ArgumentException("El nombre del producto es requerido");

            if (producto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero");

            if (producto.Stock < 0)
                throw new ArgumentException("El stock no puede ser negativo");

            if (producto.Id_Categoria <= 0)
                throw new ArgumentException("Se requiere una categoría válida");

            // Establecer la fecha de creación si no está establecida
            if (producto.Fecha_Creacion == DateTime.MinValue)
                producto.Fecha_Creacion = DateTime.Now;

            await _productoRepository.AddAsync(producto);
        }

        public async Task UpdateProductoAsync(Producto producto)
        {
            // Validar datos del producto
            if (string.IsNullOrEmpty(producto.Nombre))
                throw new ArgumentException("El nombre del producto es requerido");

            if (producto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero");

            if (producto.Stock < 0)
                throw new ArgumentException("El stock no puede ser negativo");

            if (producto.Id_Categoria <= 0)
                throw new ArgumentException("Se requiere una categoría válida");

            // Verificar que el producto existe
            var existingProducto = await _productoRepository.GetByIdAsync(producto.Id_Producto);
            if (existingProducto == null)
                throw new ArgumentException($"No se encontró el producto con ID {producto.Id_Producto}");

            await _productoRepository.UpdateAsync(producto);
        }

        public async Task DeleteProductoAsync(int id)
        {
            // Verificar que el producto existe antes de eliminarlo
            var existingProducto = await _productoRepository.GetByIdAsync(id);
            if (existingProducto == null)
                throw new ArgumentException($"No se encontró el producto con ID {id}");

            await _productoRepository.DeleteAsync(id);
        }
    }
}