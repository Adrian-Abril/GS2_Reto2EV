using FarMedAPI.Models;
using FarMedAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarMedAPI.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<List<Producto>> GetAllAsync()
        {
            return await _productoRepository.GetAllAsync();
        }

        public async Task<Producto> GetByIdAsync(int id)
        {
            return await _productoRepository.GetByIdAsync(id);
        }

        public async Task<List<Producto>> GetByCategoriaIdAsync(int categoriaId)
        {
            if (categoriaId <= 0)
                throw new ArgumentException("El ID de la categoría debe ser mayor a 0.");

            return await _productoRepository.GetByCategoriaIdAsync(categoriaId) ?? new List<Producto>();
        }

        public async Task<List<Producto>> GetByLaboratorioIdAsync(int laboratorioId)
        {
            if (laboratorioId <= 0)
                throw new ArgumentException("El ID del laboratorio debe ser mayor a 0.");

            return await _productoRepository.GetByLaboratorioIdAsync(laboratorioId) ?? new List<Producto>();
        }

        public async Task AddAsync(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            await _productoRepository.AddAsync(producto);
        }

        public async Task UpdateAsync(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            await _productoRepository.UpdateAsync(producto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productoRepository.DeleteAsync(id);
        }
    }
}
