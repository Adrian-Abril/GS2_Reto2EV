using FarMedAPI.Models;

namespace FarMedAPI.Repository
{
    public interface IEntregaRepository
    {
        Task<List<Entrega>> GetAllAsync();
        Task<Entrega> GetByIdAsync(int id);
        Task AddAsync(Entrega entrega);
        Task UpdateAsync(Entrega entrega);
        Task DeleteAsync(int id);
    }
}
