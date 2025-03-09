// Service/IDetallePedidoService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Service
{
    public interface IDetallePedidoService
    {
        Task<IEnumerable<DetallePedido>> GetAllDetallesPedidoAsync();
        Task<DetallePedido> GetDetallePedidoByIdAsync(int pedidoId, int productoId);
        Task<IEnumerable<DetallePedido>> GetDetallesByPedidoIdAsync(int pedidoId);
        Task<DetallePedido> CreateDetallePedidoAsync(DetallePedido detallePedido);
        Task<DetallePedido> UpdateDetallePedidoAsync(int pedidoId, int productoId, DetallePedido detallePedido);
        Task<bool> DeleteDetallePedidoAsync(int pedidoId, int productoId);
    }
}