// Service/IProductoService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Service
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> GetAllProductosAsync();
        Task<Producto> GetProductoByIdAsync(int id);
        Task<IEnumerable<Producto>> GetProductosByCategoriaAsync(int categoriaId);
        Task<IEnumerable<Producto>> GetProductosByLaboratorioAsync(int laboratorioId);
        Task<Producto> CreateProductoAsync(Producto producto);
        Task<Producto> UpdateProductoAsync(int id, Producto producto);
        Task<bool> DeleteProductoAsync(int id);
    }
}