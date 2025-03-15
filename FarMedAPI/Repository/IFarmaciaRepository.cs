// Repository/IFarmaciaRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Repository
{
    public interface IFarmaciaRepository
    {
        Task<IEnumerable<Farmacia>> GetAllFarmaciasAsync();
        Task<Farmacia> GetFarmaciaByIdAsync(int id);
        Task<Farmacia> CreateFarmaciaAsync(Farmacia farmacia);
        Task<Farmacia> UpdateFarmaciaAsync(Farmacia farmacia);
        Task<bool> DeleteFarmaciaAsync(int id);
        Task<bool> FarmaciaExistsAsync(int id);
    }
}