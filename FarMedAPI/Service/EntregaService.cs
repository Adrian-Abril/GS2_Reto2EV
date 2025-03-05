using FarMedAPI.Models;
using FarMedAPI.Repository;

namespace FarMedAPI.Service
{
    public class EntregaService : IEntregaService
    {
        private readonly IEntregaRepository _entregaRepository;

        public EntregaService(IEntregaRepository entregaRepository)
        {
            _entregaRepository = entregaRepository;
        }

        public async Task<List<Entrega>> GetAllAsync()
        {
            return await _entregaRepository.GetAllAsync();
        }

        public async Task<Entrega> GetByIdAsync(int id)
        {
            return await _entregaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Entrega entrega)
        {
            await _entregaRepository.AddAsync(entrega);
        }

        public async Task UpdateAsync(Entrega entrega)
        {
            await _entregaRepository.UpdateAsync(entrega);
        }

        public async Task DeleteAsync(int id)
        {
            await _entregaRepository.DeleteAsync(id);
        }
    }
}
