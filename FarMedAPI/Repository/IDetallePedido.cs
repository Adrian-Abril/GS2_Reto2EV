using FarMedAPI.Models;

namespace FarMedAPI.Repository
{
    public interface IDetallePedidoRepository
    {
        Task<List<DetallePedido>> GetAllAsync();
        Task<DetallePedido> GetByIdAsync(int id);
        Task AddAsync(DetallePedido detallePedido);
        Task UpdateAsync(DetallePedido detallePedido);
        Task DeleteAsync(int id);
    }
}