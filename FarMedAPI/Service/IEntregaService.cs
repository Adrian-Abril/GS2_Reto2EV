// Service/IEntregaService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Service
{
    public interface IEntregaService
    {
        Task<IEnumerable<Entrega>> GetAllEntregasAsync();
        Task<Entrega> GetEntregaByIdAsync(int id);
        Task<Entrega> GetEntregaByPedidoIdAsync(int pedidoId);
        Task<Entrega> CreateEntregaAsync(Entrega entrega);
        Task<Entrega> UpdateEntregaAsync(int id, Entrega entrega);
        Task<bool> DeleteEntregaAsync(int id);
    }
}