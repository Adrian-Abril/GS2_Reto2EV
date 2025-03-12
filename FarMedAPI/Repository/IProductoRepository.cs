using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Repositories
{
    public interface IProductoRepository
    {
        Task<List<Producto>> GetAllAsync();
        Task<Producto> GetByIdAsync(int id);
        Task<List<Producto>> GetByCategoriaIdAsync(int categoriaId);
        Task<List<Producto>> GetByLaboratorioIdAsync(int laboratorioId);
        Task AddAsync(Producto producto);
        Task UpdateAsync(Producto producto);
        Task<bool> DeleteAsync(int id);
    }
}
