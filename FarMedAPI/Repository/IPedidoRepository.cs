// Repository/IPedidoRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Repository
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> GetAllPedidosAsync();
        Task<Pedido> GetPedidoByIdAsync(int id);
        Task<IEnumerable<Pedido>> GetPedidosByClienteAsync(int clienteId);
        Task<IEnumerable<Pedido>> GetPedidosByFarmaciaAsync(int farmaciaId);
        Task<Pedido> CreatePedidoAsync(Pedido pedido);
        Task<Pedido> UpdatePedidoAsync(Pedido pedido);
        Task<bool> DeletePedidoAsync(int id);
        Task<bool> PedidoExistsAsync(int id);
    }
}