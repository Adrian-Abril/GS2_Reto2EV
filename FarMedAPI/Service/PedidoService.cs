using FarMedAPI.Models;
using FarMedAPI.Repository;

namespace FarMedAPI.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<List<Pedido>> GetAllAsync()
        {
            return await _pedidoRepository.GetAllAsync();
        }

        public async Task<Pedido> GetByIdAsync(int id)
        {
            return await _pedidoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Pedido pedido)
        {
            await _pedidoRepository.AddAsync(pedido);
        }

        public async Task UpdateAsync(Pedido pedido)
        {
            await _pedidoRepository.UpdateAsync(pedido);
        }

        public async Task DeleteAsync(int id)
        {
            await _pedidoRepository.DeleteAsync(id);
        }
    }
}
