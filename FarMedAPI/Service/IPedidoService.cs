using FarMedAPI.Models;

namespace FarMedAPI.Service
{
    public interface IPedidoService
    {
        Task<List<Pedido>> GetAllAsync();
        Task<Pedido> GetByIdAsync(int id);
        Task AddAsync(Pedido pedido);
        Task UpdateAsync(Pedido pedido);
        Task DeleteAsync(int id);
    }
}
