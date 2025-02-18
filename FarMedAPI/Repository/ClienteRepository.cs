


using FarMedAPI.Repository;
using FarMedAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarMedAPI.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly List<Cliente> _clientes;

        public ClienteRepository()
        {
            _clientes = new List<Cliente>
            {
                new Cliente { Id_Cliente = 1, Nombre = "Juan Pérez", Email = "juan@example.com", Contraseña = "1234", Teléfono = "123456789", Tipo_Usuario = "Admin", Dirección = "Calle 123", Fecha_Registro = DateTime.Now },
                new Cliente { Id_Cliente = 2, Nombre = "María López", Email = "maria@example.com", Contraseña = "abcd", Teléfono = "987654321", Tipo_Usuario = "User", Dirección = "Avenida 456", Fecha_Registro = DateTime.Now }
            };
        }

        public Task<List<Cliente>> GetAllAsync()
        {
            return Task.FromResult(_clientes);
        }

        public Task<Cliente> GetByIdAsync(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id_Cliente == id);
            return Task.FromResult(cliente);
        }

        public Task AddAsync(Cliente cliente)
        {
            cliente.Id_Cliente = _clientes.Count + 1;
            _clientes.Add(cliente);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Cliente cliente)
        {
            var existingCliente = _clientes.FirstOrDefault(c => c.Id_Cliente == cliente.Id_Cliente);
            if (existingCliente != null)
            {
                existingCliente.Nombre = cliente.Nombre;
                existingCliente.Email = cliente.Email;
                existingCliente.Contraseña = cliente.Contraseña;
                existingCliente.Teléfono = cliente.Teléfono;
                existingCliente.Tipo_Usuario = cliente.Tipo_Usuario;
                existingCliente.Dirección = cliente.Dirección;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id_Cliente == id);
            if (cliente != null)
            {
                _clientes.Remove(cliente);
            }
            return Task.CompletedTask;
        }
    }
}

