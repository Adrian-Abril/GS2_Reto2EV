using FarMedAPI.Models;

namespace FarMedAPI.Service
{
    public interface IDetallePedidoService
    {
        Task<List<DetallePedido>> GetAllAsync();
        Task<DetallePedido> GetByIdAsync(int id);
        Task AddAsync(DetallePedido detallePedido);
        Task UpdateAsync(DetallePedido detallePedido);
        Task DeleteAsync(int id);
    }
}
