using FarMedAPI.Repository;
using FarMedAPI.Models;

namespace FarMedAPI.Repositories
{
    public class DetallePedidoRepository : IDetallePedidoRepository
    {
        private readonly List<DetallePedido> _detallepedido;

        public DetallePedidoRepository()
        {
            _detallepedido = new List<DetallePedido>
            {
                 new DetallePedido { Id_Detalle = 1, Id_Pedido = 101, Id_Producto = 1001, Cantidad = 2, Precio_Unitario = 50.0m },
                new DetallePedido { Id_Detalle = 2, Id_Pedido = 102, Id_Producto = 1002, Cantidad = 1, Precio_Unitario = 75.0m }
            };
        }

        public Task<List<DetallePedido>> GetAllAsync()
        {
            return Task.FromResult(_detallepedido);
        }

        public Task<DetallePedido> GetByIdAsync(int id)
        {
            var detallePedido = _detallepedido.FirstOrDefault(c => c.Id_Detalle == id);
            return Task.FromResult(detallePedido);
        }

        public Task AddAsync(DetallePedido detallePedido)
        {
            detallePedido.Id_Detalle = _detallepedido.Count + 1;
            _detallepedido.Add(detallePedido);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(DetallePedido detallepedido)
        {
            var existingDetallePedido = _detallepedido.FirstOrDefault(c => c.Id_Detalle == detallepedido.Id_Detalle);
            if (existingDetallePedido != null)
            {
               existingDetallePedido.Id_Pedido = detallepedido.Id_Pedido;
                existingDetallePedido.Id_Producto = detallepedido.Id_Producto;
                existingDetallePedido.Cantidad = detallepedido.Cantidad;
                existingDetallePedido.Precio_Unitario = detallepedido.Precio_Unitario;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var detallepedido = _detallepedido.FirstOrDefault(c => c.Id_Detalle == id);
            if (detallepedido != null)
            {
                _detallepedido.Remove(detallepedido);
            }
            return Task.CompletedTask;
        }
    }
}

