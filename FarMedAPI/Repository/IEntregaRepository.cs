// Repository/IEntregaRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Repository
{
    public interface IEntregaRepository
    {
        Task<IEnumerable<Entrega>> GetAllEntregasAsync();
        Task<Entrega> GetEntregaByIdAsync(int id);
        Task<Entrega> GetEntregaByPedidoIdAsync(int pedidoId);
        Task<Entrega> CreateEntregaAsync(Entrega entrega);
        Task<Entrega> UpdateEntregaAsync(Entrega entrega);
        Task<bool> DeleteEntregaAsync(int id);
        Task<bool> EntregaExistsAsync(int id);
    }
}