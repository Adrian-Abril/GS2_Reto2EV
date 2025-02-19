using FarMedAPI.Models;
using FarMedAPI.Repository;

namespace FarMedAPI.Service
{
    public class DetallePedidoService : IDetallePedidoService
    {
        private readonly IDetallePedidoRepository _detallepedidoRepository;

        public DetallePedidoService(IDetallePedidoRepository detallepedidoRepository)
        {
            _detallepedidoRepository = detallepedidoRepository;
        }

        public async Task<List<DetallePedido>> GetAllAsync()
        {
            return await _detallepedidoRepository.GetAllAsync();
        }

        public async Task<DetallePedido> GetByIdAsync(int id)
        {
            return await _detallepedidoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(DetallePedido detallePedido)
        {
            await _detallepedidoRepository.AddAsync(detallePedido);
        }

        public async Task UpdateAsync(DetallePedido detallePedido)
        {
            await _detallepedidoRepository.UpdateAsync(detallePedido);
        }

        public async Task DeleteAsync(int id)
        {
            await _detallepedidoRepository.DeleteAsync(id);
        }
    }
}
