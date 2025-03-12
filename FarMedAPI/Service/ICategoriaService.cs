using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Services
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> GetAllAsync();
        Task AddAsync(Categoria categoria);
    }
}
