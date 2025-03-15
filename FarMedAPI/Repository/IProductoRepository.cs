// Repository/IProductoRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Repository
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAllProductosAsync();
        Task<Producto> GetProductoByIdAsync(int id);
        Task<IEnumerable<Producto>> GetProductosByCategoriaAsync(int categoriaId);
        Task<IEnumerable<Producto>> GetProductosByLaboratorioAsync(int laboratorioId);
        Task<Producto> CreateProductoAsync(Producto producto);
        Task<Producto> UpdateProductoAsync(Producto producto);
        Task<bool> DeleteProductoAsync(int id);
        Task<bool> ProductoExistsAsync(int id);
    }
}