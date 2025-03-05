using FarMedAPI.Repository;
using FarMedAPI.Models;

namespace FarMedAPI.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly List<Pedido> _pedidos;

        public PedidoRepository()
        {
            _pedidos = new List<Pedido>
            {
                new Pedido { Id_Cliente = 1, Id_Farmacia = 1, Fecha_Pedido = DateTime.Now, Estado = "list", Total = 23, Dirección_Entrega = "calle inventada",},
                new Pedido { Id_Cliente = 2, Id_Farmacia = 2, Fecha_Pedido = DateTime.Now, Estado = "list", Total = 24, Dirección_Entrega = "calle inventada",}
            };
        }

        public Task<List<Pedido>> GetAllAsync()
        {
            return Task.FromResult(_pedidos);
        }

        public Task<Pedido> GetByIdAsync(int id)
        {
            var pedido = _pedidos.FirstOrDefault(c => c.Id_Pedido == id);
            return Task.FromResult(pedido);
        }

        public Task AddAsync(Pedido pedido)
        {
            pedido.Id_Cliente = _pedidos.Count + 1;
            _pedidos.Add(pedido);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Pedido pedido)
        {
            var existingPedido = _pedidos.FirstOrDefault(c => c.Id_Pedido == pedido.Id_Pedido);
            if (existingPedido != null)
            {
                existingPedido.Id_Cliente = pedido.Id_Cliente;
                existingPedido.Id_Farmacia = pedido.Id_Farmacia;
                existingPedido.Fecha_Pedido = pedido.Fecha_Pedido;
                existingPedido.Estado = pedido.Estado;
                existingPedido.Total = pedido.Total;
                existingPedido.Dirección_Entrega = pedido.Dirección_Entrega;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var pedido = _pedidos.FirstOrDefault(c => c.Id_Pedido == id);
            if (pedido != null)
            {
                _pedidos.Remove(pedido);
            }
            return Task.CompletedTask;
        }
    }
}

