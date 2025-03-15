// Repository/IEmpleadoRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Repository
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<Empleado>> GetAllEmpleadosAsync();
        Task<Empleado> GetEmpleadoByIdAsync(int id);
        Task<IEnumerable<Empleado>> GetEmpleadosByFarmaciaAsync(int farmaciaId);
        Task<Empleado> CreateEmpleadoAsync(Empleado empleado);
        Task<Empleado> UpdateEmpleadoAsync(Empleado empleado);
        Task<bool> DeleteEmpleadoAsync(int id);
        Task<bool> EmpleadoExistsAsync(int id);
    }
}