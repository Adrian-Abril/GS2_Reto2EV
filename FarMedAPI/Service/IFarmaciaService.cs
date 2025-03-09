// Service/IFarmaciaService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Service
{
    public interface IFarmaciaService
    {
        Task<IEnumerable<Farmacia>> GetAllFarmaciasAsync();
        Task<Farmacia> GetFarmaciaByIdAsync(int id);
        Task<Farmacia> CreateFarmaciaAsync(Farmacia farmacia);
        Task<Farmacia> UpdateFarmaciaAsync(int id, Farmacia farmacia);
        Task<bool> DeleteFarmaciaAsync(int id);
    }
}