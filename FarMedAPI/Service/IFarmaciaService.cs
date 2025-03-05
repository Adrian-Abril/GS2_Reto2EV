using FarMedAPI.Models;

namespace FarMedAPI.Service
{
    public interface IFarmaciaService
    {
        Task<List<Farmacia>> GetAllAsync();
        Task<Farmacia> GetByIdAsync(int id);
        Task AddAsync(Farmacia farmacia);
        Task UpdateAsync(Farmacia farmacia);
        Task DeleteAsync(int id);
    }
}
