using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarMedAPI.Data;
using FarMedAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FarMedAPI.Service
{
    public class ProductoService : IProductoService
    {
        private readonly FarmaciaContext _context;

        public ProductoService(FarmaciaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetAllProductos()
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Laboratorio)
                .ToListAsync();
        }

        public async Task<Producto> GetProductoById(int id)
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Laboratorio)
                .FirstOrDefaultAsync(p => p.IdProducto == id);
        }

        public async Task<IEnumerable<Producto>> GetProductosByCategoria(int categoriaId)
        {
            return await _context.Productos
                .Where(p => p.IdCategoria == categoriaId)
                .Include(p => p.Categoria)
                .Include(p => p.Laboratorio)
                .ToListAsync();
        }

        public async Task<Producto> AddProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<Producto> UpdateProducto(Producto producto)
        {
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return false;

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}