using FarMedAPI.Repository;
using FarMedAPI.Models;

namespace FarMedAPI.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly List<Empleado> _empleados;

        public EmpleadoRepository()
        {
            _empleados = new List<Empleado>
            {
                new Empleado { Id_Empleado = 1, Id_Farmacia = 1, Nombre = "juan@example.com", Teléfono = "123456789", Rol = "Repartidor"},
                new Empleado { Id_Empleado = 2, Id_Farmacia = 2, Nombre = "maria@example.com", Teléfono = "987654321", Rol = "Farmacéutico" }
            };
        }

        public Task<List<Empleado>> GetAllAsync()
        {
            return Task.FromResult(_empleados);
        }

        public Task<Empleado> GetByIdAsync(int id)
        {
            var empleado = _empleados.FirstOrDefault(c => c.Id_Empleado == id);
            return Task.FromResult(empleado);
        }

        public Task AddAsync(Empleado empleado)
        {
            empleado.Id_Empleado = _empleados.Count + 1;
            _empleados.Add(empleado);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Empleado empleado)
        {
            var existingEmpleado = _empleados.FirstOrDefault(c => c.Id_Empleado == empleado.Id_Empleado);
            if (existingEmpleado != null)
            {
                existingEmpleado.Id_Farmacia = empleado.Id_Farmacia;
                existingEmpleado.Nombre = empleado.Nombre;
                existingEmpleado.Teléfono = empleado.Teléfono;
                existingEmpleado.Rol = empleado.Rol;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var empleado = _empleados.FirstOrDefault(c => c.Id_Empleado == id);
            if (empleado != null)
            {
                _empleados.Remove(empleado);
            }
            return Task.CompletedTask;
        }
    }
}

