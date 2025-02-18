using FarMedAPI.Repository;
using Microsoft.Data.SqlClient;
using FarMedAPI.Models;

namespace FarMedAPI.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            var clientes = new List<Cliente>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT id_cliente, nombre, email, contraseña, teléfono, tipo_usuario, dirección, fecha_registro FROM Clientes";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var cliente = new Cliente
                            {
                                Id_Cliente = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Email = reader.GetString(2),
                                Contraseña = reader.GetString(3),
                                Teléfono = reader.GetString(4),
                                Tipo_Usuario = reader.GetString(5),
                                Dirección = reader.GetString(6),
                                Fecha_Registro = reader.GetDateTime(7)
                            };

                            clientes.Add(cliente);
                        }
                    }
                }
            }
            return clientes;
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            Cliente cliente = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT id_cliente, nombre, email, contraseña, teléfono, tipo_usuario, dirección, fecha_registro FROM Clientes WHERE id_cliente = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            cliente = new Cliente
                            {
                                Id_Cliente = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Email = reader.GetString(2),
                                Contraseña = reader.GetString(3),
                                Teléfono = reader.GetString(4),
                                Tipo_Usuario = reader.GetString(5),
                                Dirección = reader.GetString(6),
                                Fecha_Registro = reader.GetDateTime(7)
                            };
                        }
                    }
                }
            }
            return cliente;
        }

        public async Task AddAsync(Cliente cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO Clientes (nombre, email, contraseña, teléfono, tipo_usuario, dirección, fecha_registro) 
                                 VALUES (@Nombre, @Email, @Contraseña, @Telefono, @TipoUsuario, @Direccion, GETDATE())";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Contraseña", cliente.Contraseña);
                    command.Parameters.AddWithValue("@Telefono", cliente.Teléfono);
                    command.Parameters.AddWithValue("@TipoUsuario", cliente.Tipo_Usuario);
                    command.Parameters.AddWithValue("@Direccion", cliente.Dirección);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE Clientes 
                                 SET nombre = @Nombre, email = @Email, contraseña = @Contraseña, 
                                     teléfono = @Telefono, tipo_usuario = @TipoUsuario, dirección = @Direccion 
                                 WHERE id_cliente = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", cliente.Id_Cliente);
                    command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Contraseña", cliente.Contraseña);
                    command.Parameters.AddWithValue("@Telefono", cliente.Teléfono);
                    command.Parameters.AddWithValue("@TipoUsuario", cliente.Tipo_Usuario);
                    command.Parameters.AddWithValue("@Direccion", cliente.Dirección);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Clientes WHERE id_cliente = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }
}