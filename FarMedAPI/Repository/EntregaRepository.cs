using FarMedAPI.Repository;
using FarMedAPI.Models;

namespace FarMedAPI.Repositories
{
    public class EntregaRepository : IEntregaRepository
    {
        private readonly List<Entrega> _entregas;

        public EntregaRepository()
        {
            _entregas = new List<Entrega>
            {
                new Entrega { Id_Entrega = 1, Id_Pedido = 1, Fecha_Entrega = DateTime.Now, Estado = "Listo", Id_Empleado = 1,},
                new Entrega { Id_Entrega = 2, Id_Pedido = 2, Fecha_Entrega = DateTime.Now, Estado = "Cancelado", Id_Empleado = 2, }
            };
        }

        public Task<List<Entrega>> GetAllAsync()
        {
            return Task.FromResult(_entregas);
        }

        public Task<Entrega> GetByIdAsync(int id)
        {
            var entrega = _entregas.FirstOrDefault(c => c.Id_Entrega == id);
            return Task.FromResult(entrega);
        }

        public Task AddAsync(Entrega entrega)
        {
            entrega.Id_Entrega = _entregas.Count + 1;
            _entregas.Add(entrega);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Entrega entrega)
        {
            var existingEntrega = _entregas.FirstOrDefault(c => c.Id_Entrega == entrega.Id_Entrega);
            if (existingEntrega != null)
            {
                existingEntrega.Id_Pedido = entrega.Id_Pedido;
                existingEntrega.Fecha_Entrega = entrega.Fecha_Entrega;
                existingEntrega.Estado = entrega.Estado;
                existingEntrega.Id_Empleado = entrega.Id_Empleado;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var entrega = _entregas.FirstOrDefault(c => c.Id_Entrega == id);
            if (entrega != null)
            {
                _entregas.Remove(entrega);
            }
            return Task.CompletedTask;
        }
    }
}

