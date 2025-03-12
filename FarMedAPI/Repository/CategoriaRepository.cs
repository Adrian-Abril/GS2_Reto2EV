using Npgsql;
using FarMedAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarMedAPI.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly string _connectionString;

        public CategoriaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Categoria>> GetAllAsync()
        {
            var categorias = new List<Categoria>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT \"CategoriaId\", \"Nombre\" FROM \"Categoria\"";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        categorias.Add(new Categoria
                        {
                            CategoriaId = reader.GetInt32(0),
                            Nombre = reader.GetString(1)
                        });
                    }
                }
            }

            return categorias;
        }

        public async Task AddAsync(Categoria categoria)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO \"Categoria\" (\"Nombre\") VALUES (@Nombre)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
