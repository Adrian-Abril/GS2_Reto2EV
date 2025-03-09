using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarMedAPI.Data;
using FarMedAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FarMedAPI.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly FarmaciaContext _context;

        public PedidoService(FarmaciaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> GetAllPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        public async Task<Pedido> GetPedidoById(int id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByCliente(int clienteId)
        {
            return await _context.Pedidos
                .Where(p => p.Id_Cliente == clienteId)
                .ToListAsync();
        }

        public async Task<Pedido> AddPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido> UpdatePedido(Pedido pedido)
        {
            _context.Entry(pedido).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<bool> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
                return false;

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}