using FarMedAPI.Models;
using FarMedAPI.Repository;

namespace FarMedAPI.Repositories
{
    public class DetallePedidoRepository : IDetallePedidoRepository
    {
        private readonly List<DetallePedido> _detallesPedido;

        public DetallePedidoRepository()
        {
            _detallesPedido = new List<DetallePedido>();
        }

        public Task<IEnumerable<DetallePedido>> GetAll()
        {
            return Task.FromResult<IEnumerable<DetallePedido>>(_detallesPedido);
        }

        public Task<DetallePedido> GetById(int id)
        {
            var detalle = _detallesPedido.FirstOrDefault(d => d.Id_Detalle == id);
            return Task.FromResult(detalle);
        }

        public Task<DetallePedido> Create(DetallePedido detallePedido)
        {
            detallePedido.Id_Detalle = _detallesPedido.Count + 1;
            _detallesPedido.Add(detallePedido);
            return Task.FromResult(detallePedido);
        }

        public Task<bool> Update(DetallePedido detallePedido)
        {
            var existingDetalle = _detallesPedido.FirstOrDefault(d => d.Id_Detalle == detallePedido.Id_Detalle);
            if (existingDetalle != null)
            {
                existingDetalle.Id_Pedido = detallePedido.Id_Pedido;
                existingDetalle.Id_Producto = detallePedido.Id_Producto;
                existingDetalle.Cantidad = detallePedido.Cantidad;
                existingDetalle.Precio_Unitario = detallePedido.Precio_Unitario;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> Delete(int id)
        {
            var detalle = _detallesPedido.FirstOrDefault(d => d.Id_Detalle == id);
            if (detalle != null)
            {
                _detallesPedido.Remove(detalle);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
