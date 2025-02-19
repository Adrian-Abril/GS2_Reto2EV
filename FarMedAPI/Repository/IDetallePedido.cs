using FarMedAPI.Models;

namespace FarMedAPI.Repository
{
    public interface IDetallePedidoRepository
    {
        Task<IEnumerable<DetallePedido>> GetAll();
        Task<DetallePedido> GetById(int id);
        Task<DetallePedido> Create(DetallePedido detallePedido);
        Task<bool> Update(DetallePedido detallePedido);
        Task<bool> Delete(int id);
    }
}
