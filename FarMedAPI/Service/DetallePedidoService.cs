using FarMedAPI.Models;
using FarMedAPI.Repository;

namespace FarMedAPI.Service
{

public class DetallePedidoService : IDetallePedidoService
    {
        private readonly IDetallePedidoRepository _detallePedidoRepository;

        public DetallePedidoService(IDetallePedidoRepository detallePedidoRepository)
        {
            _detallePedidoRepository = detallePedidoRepository;
        }

        public async Task<IEnumerable<DetallePedido>> GetAll()
        {
            return await _detallePedidoRepository.GetAll();
        }

        public async Task<DetallePedido> GetById(int id)
        {
            return await _detallePedidoRepository.GetById(id);
        }

        public async Task<DetallePedido> Create(DetallePedido detallePedido)
        {
            return await _detallePedidoRepository.Create(detallePedido);
        }

        public async Task<bool> Update(DetallePedido detallePedido)
        {
            return await _detallePedidoRepository.Update(detallePedido);
        }

        public async Task<bool> Delete(int id)
        {
            return await _detallePedidoRepository.Delete(id);
        }
    }
}