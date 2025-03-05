using FarMedAPI.Models;

namespace FarMedAPI.Repository
{
    public interface IFarmaciaRepository
    {
        Task<List<Farmacia>> GetAllAsync();
        Task<Farmacia> GetByIdAsync(int id);
        Task AddAsync(Farmacia farmacia);
        Task UpdateAsync(Farmacia farmacia);
        Task DeleteAsync(int id);
    }
}
