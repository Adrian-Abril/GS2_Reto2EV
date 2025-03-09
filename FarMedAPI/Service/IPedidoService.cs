// Service/IPedidoService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Service
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetAllPedidosAsync();
        Task<Pedido> GetPedidoByIdAsync(int id);
        Task<IEnumerable<Pedido>> GetPedidosByClienteAsync(int clienteId);
        Task<IEnumerable<Pedido>> GetPedidosByFarmaciaAsync(int farmaciaId);
        Task<Pedido> CreatePedidoAsync(Pedido pedido);
        Task<Pedido> UpdatePedidoAsync(int id, Pedido pedido);
        Task<bool> DeletePedidoAsync(int id);
    }
}