using Npgsql;
using FarMedAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarMedAPI.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString;

        public ProductoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Producto>> GetAllAsync()
        {
            var productos = new List<Producto>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT \"ProductoId\", \"Nombre\", \"Descripcion\", \"Precio\", \"Stock\", \"CategoriaId\", \"LaboratorioId\", \"RequiereReceta\" FROM \"Producto\"";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        productos.Add(MapProducto(reader));
                    }
                }
            }

            return productos;
        }

        public async Task<Producto> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT \"ProductoId\", \"Nombre\", \"Descripcion\", \"Precio\", \"Stock\", \"CategoriaId\", \"LaboratorioId\", \"RequiereReceta\" FROM \"Producto\" WHERE \"ProductoId\" = @Id";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return await reader.ReadAsync() ? MapProducto(reader) : null;
                    }
                }
            }
        }

        public async Task<List<Producto>> GetByCategoriaIdAsync(int categoriaId)
        {
            var productos = new List<Producto>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM \"Producto\" WHERE \"CategoriaId\" = @CategoriaId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoriaId", categoriaId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            productos.Add(MapProducto(reader));
                        }
                    }
                }
            }

            return productos;
        }

        public async Task<List<Producto>> GetByLaboratorioIdAsync(int laboratorioId)
        {
            var productos = new List<Producto>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM \"Producto\" WHERE \"LaboratorioId\" = @LaboratorioId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LaboratorioId", laboratorioId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            productos.Add(MapProducto(reader));
                        }
                    }
                }
            }

            return productos;
        }

        public async Task AddAsync(Producto producto)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO \"Producto\" (\"Nombre\", \"Descripcion\", \"Precio\", \"Stock\", \"CategoriaId\", \"LaboratorioId\", \"RequiereReceta\") VALUES (@Nombre, @Descripcion, @Precio, @Stock, @CategoriaId, @LaboratorioId, @RequiereReceta)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    SetProductoParameters(command, producto);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Producto producto)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE \"Producto\" SET \"Nombre\" = @Nombre, \"Descripcion\" = @Descripcion, \"Precio\" = @Precio, \"Stock\" = @Stock, \"CategoriaId\" = @CategoriaId, \"LaboratorioId\" = @LaboratorioId, \"RequiereReceta\" = @RequiereReceta WHERE \"ProductoId\" = @ProductoId";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    SetProductoParameters(command, producto);
                    command.Parameters.AddWithValue("@ProductoId", producto.ProductoId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM \"Producto\" WHERE \"ProductoId\" = @Id";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        private Producto MapProducto(NpgsqlDataReader reader)
        {
            return new Producto
            {
                ProductoId = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                Precio = reader.GetDecimal(3),
                Stock = reader.GetInt32(4),
                CategoriaId = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                LaboratorioId = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                RequiereReceta = reader.GetBoolean(7)
            };
        }

        private void SetProductoParameters(NpgsqlCommand command, Producto producto)
        {
            command.Parameters.AddWithValue("@Nombre", producto.Nombre);
            command.Parameters.AddWithValue("@Descripcion", (object?)producto.Descripcion ?? DBNull.Value);
            command.Parameters.AddWithValue("@Precio", producto.Precio);
            command.Parameters.AddWithValue("@Stock", producto.Stock);
            command.Parameters.AddWithValue("@CategoriaId", (object?)producto.CategoriaId ?? DBNull.Value);
            command.Parameters.AddWithValue("@LaboratorioId", (object?)producto.LaboratorioId ?? DBNull.Value);
            command.Parameters.AddWithValue("@RequiereReceta", producto.RequiereReceta);
        }
    }
}
