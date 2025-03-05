using FarMedAPI.Models;

namespace FarMedAPI.Service
{
    public interface IEntregaService
    {
        Task<List<Entrega>> GetAllAsync();
        Task<Entrega> GetByIdAsync(int id);
        Task AddAsync(Entrega entrega);
        Task UpdateAsync(Entrega entrega);
        Task DeleteAsync(int id);
    }
}
