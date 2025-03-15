using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarMedAPI.Data;
using FarMedAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FarMedAPI.Repository
{
    public class PedidoRepository
    {
        private readonly FarmaciaContext _context;

        public PedidoRepository(FarmaciaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> GetAllPedidos()
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Empleado)
                .Include(p => p.Farmacia)
                .ToListAsync();
        }

        public async Task<Pedido> GetPedidoById(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Empleado)
                .Include(p => p.Farmacia)
                .FirstOrDefaultAsync(p => p.Id_Pedido == id);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByCliente(int clienteId)
        {
            return await _context.Pedidos
                .Where(p => p.Id_Cliente == clienteId)
                .Include(p => p.Cliente)
                .Include(p => p.Empleado)
                .Include(p => p.Farmacia)
                .ToListAsync();
        }

        public async Task<Pedido> CreatePedido(Pedido pedido)
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