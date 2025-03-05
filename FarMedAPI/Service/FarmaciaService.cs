using FarMedAPI.Models;
using FarMedAPI.Repository;

namespace FarMedAPI.Service
{
    public class FarmaciaService : IFarmaciaService
    {
        private readonly IFarmaciaRepository _farmaciaRepository;

        public FarmaciaService(IFarmaciaRepository farmaciaRepository)
        {
            _farmaciaRepository = farmaciaRepository;
        }

        public async Task<List<Farmacia>> GetAllAsync()
        {
            return await _farmaciaRepository.GetAllAsync();
        }

        public async Task<Farmacia> GetByIdAsync(int id)
        {
            return await _farmaciaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Farmacia farmacia)
        {
            await _farmaciaRepository.AddAsync(farmacia);
        }

        public async Task UpdateAsync(Farmacia farmacia)
        {
            await _farmaciaRepository.UpdateAsync(farmacia);
        }

        public async Task DeleteAsync(int id)
        {
            await _farmaciaRepository.DeleteAsync(id);
        }
    }
}
