using FarMedAPI.Repository;
using Microsoft.Data.SqlClient;
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

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT p.id_producto, p.nombre, p.descripcion, p.precio, p.stock, 
                                p.disponible, p.fecha_creacion, p.id_categoria, p.id_laboratorio 
                                FROM Productos p";
                
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var producto = new Producto
                            {
                                Id_Producto = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Precio = reader.GetDecimal(3),
                                Stock = reader.GetInt32(4),
                                Disponible = reader.GetBoolean(5),
                                Fecha_Creacion = reader.GetDateTime(6),
                                Id_Categoria = reader.GetInt32(7),
                                Id_Laboratorio = reader.IsDBNull(8) ? null : (int?)reader.GetInt32(8)
                            };

                            productos.Add(producto);
                        }
                    }
                }
            }
            return productos;
        }

        public async Task<Producto> GetByIdAsync(int id)
        {
            Producto producto = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT p.id_producto, p.nombre, p.descripcion, p.precio, p.stock, 
                                p.disponible, p.fecha_creacion, p.id_categoria, p.id_laboratorio 
                                FROM Productos p 
                                WHERE p.id_producto = @Id";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            producto = new Producto
                            {
                                Id_Producto = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Precio = reader.GetDecimal(3),
                                Stock = reader.GetInt32(4),
                                Disponible = reader.GetBoolean(5),
                                Fecha_Creacion = reader.GetDateTime(6),
                                Id_Categoria = reader.GetInt32(7),
                                Id_Laboratorio = reader.IsDBNull(8) ? null : (int?)reader.GetInt32(8)
                            };
                        }
                    }
                }
            }
            return producto;
        }

        public async Task<List<Producto>> GetByCategoriaAsync(int categoriaId)
        {
            var productos = new List<Producto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT p.id_producto, p.nombre, p.descripcion, p.precio, p.stock, 
                                p.disponible, p.fecha_creacion, p.id_categoria, p.id_laboratorio 
                                FROM Productos p 
                                WHERE p.id_categoria = @CategoriaId";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoriaId", categoriaId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var producto = new Producto
                            {
                                Id_Producto = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Precio = reader.GetDecimal(3),
                                Stock = reader.GetInt32(4),
                                Disponible = reader.GetBoolean(5),
                                Fecha_Creacion = reader.GetDateTime(6),
                                Id_Categoria = reader.GetInt32(7),
                                Id_Laboratorio = reader.IsDBNull(8) ? null : (int?)reader.GetInt32(8)
                            };

                            productos.Add(producto);
                        }
                    }
                }
            }
            return productos;
        }

        public async Task AddAsync(Producto producto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO Productos (nombre, descripcion, precio, stock, disponible, fecha_creacion, id_categoria, id_laboratorio) 
                                VALUES (@Nombre, @Descripcion, @Precio, @Stock, @Disponible, @FechaCreacion, @IdCategoria, @IdLaboratorio)";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", producto.Descripcion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Precio", producto.Precio);
                    command.Parameters.AddWithValue("@Stock", producto.Stock);
                    command.Parameters.AddWithValue("@Disponible", producto.Disponible);
                    command.Parameters.AddWithValue("@FechaCreacion", producto.Fecha_Creacion);
                    command.Parameters.AddWithValue("@IdCategoria", producto.Id_Categoria);
                    command.Parameters.AddWithValue("@IdLaboratorio", producto.Id_Laboratorio ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Producto producto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE Productos 
                                SET nombre = @Nombre, 
                                    descripcion = @Descripcion, 
                                    precio = @Precio, 
                                    stock = @Stock, 
                                    disponible = @Disponible, 
                                    id_categoria = @IdCategoria, 
                                    id_laboratorio = @IdLaboratorio 
                                WHERE id_producto = @Id";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", producto.Id_Producto);
                    command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", producto.Descripcion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Precio", producto.Precio);
                    command.Parameters.AddWithValue("@Stock", producto.Stock);
                    command.Parameters.AddWithValue("@Disponible", producto.Disponible);
                    command.Parameters.AddWithValue("@IdCategoria", producto.Id_Categoria);
                    command.Parameters.AddWithValue("@IdLaboratorio", producto.Id_Laboratorio ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Productos WHERE id_producto = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}