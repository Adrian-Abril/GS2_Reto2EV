using Npgsql;
using FarMedAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarMedAPI.Repositories
{
    public class LaboratorioRepository : ILaboratorioRepository
    {
        private readonly string _connectionString;

        public LaboratorioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Laboratorio>> GetAllAsync()
        {
            var laboratorios = new List<Laboratorio>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT \"LaboratorioId\", \"Nombre\", \"Direccion\" FROM \"Laboratorio\"";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        laboratorios.Add(new Laboratorio
                        {
                            LaboratorioId = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Direccion = reader.IsDBNull(2) ? null : reader.GetString(2)
                        });
                    }
                }
            }

            return laboratorios;
        }

        public async Task AddAsync(Laboratorio laboratorio)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO \"Laboratorio\" (\"Nombre\", \"Direccion\") VALUES (@Nombre, @Direccion)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", laboratorio.Nombre);
                    command.Parameters.AddWithValue("@Direccion", (object?)laboratorio.Direccion ?? DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
