using FarMedAPI.Repository;
using FarMedAPI.Models;

namespace FarMedAPI.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly List<Producto> _productos;

        public ProductoRepository()
        {
            _productos = new List<Producto>
            {
                new Producto { Id_Farmacia = 1, Nombre = "asdada", Descripción = "jasdadasdasd", Precio = 22, Stock = 3,},
                new Producto { Id_Farmacia = 1, Nombre = "asdadad", Descripción = "asdasdadas", Precio = 21, Stock = 4,}
            };
        }

        public Task<List<Producto>> GetAllAsync()
        {
            return Task.FromResult(_productos);
        }

        public Task<Producto> GetByIdAsync(int id)
        {
            var producto = _productos.FirstOrDefault(c => c.Id_Producto == id);
            return Task.FromResult(producto);
        }

        public Task AddAsync(Producto producto)
        {
            producto.Id_Producto = _productos.Count + 1;
            _productos.Add(producto);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Producto producto)
        {
            var existingProducto = _productos.FirstOrDefault(c => c.Id_Producto == producto.Id_Producto);
            if (existingProducto != null)
            {
                existingProducto.Id_Farmacia = producto.Id_Farmacia;
                existingProducto.Nombre = producto.Nombre;
                existingProducto.Descripción = producto.Descripción;
                existingProducto.Precio = producto.Precio;
                existingProducto.Stock = producto.Stock;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var producto = _productos.FirstOrDefault(c => c.Id_Producto == id);
            if (producto != null)
            {
                _productos.Remove(producto);
            }
            return Task.CompletedTask;
        }
    }
}

