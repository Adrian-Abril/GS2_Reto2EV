// Repository/IDetallePedidoRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Repository
{
    public interface IDetallePedidoRepository
    {
        Task<IEnumerable<DetallePedido>> GetAllDetallesPedidoAsync();
        Task<DetallePedido> GetDetallePedidoByIdAsync(int pedidoId, int productoId);
        Task<IEnumerable<DetallePedido>> GetDetallesByPedidoIdAsync(int pedidoId);
        Task<DetallePedido> CreateDetallePedidoAsync(DetallePedido detallePedido);
        Task<DetallePedido> UpdateDetallePedidoAsync(DetallePedido detallePedido);
        Task<bool> DeleteDetallePedidoAsync(int pedidoId, int productoId);
        Task<bool> DetallePedidoExistsAsync(int pedidoId, int productoId);
    }
}
