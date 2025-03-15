// Repository/ICategoriaRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Repository
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAllCategoriasAsync();
        Task<Categoria> GetCategoriaByIdAsync(int id);
        Task<Categoria> CreateCategoriaAsync(Categoria categoria);
        Task<Categoria> UpdateCategoriaAsync(Categoria categoria);
        Task<bool> DeleteCategoriaAsync(int id);
        Task<bool> CategoriaExistsAsync(int id);
    }
}