using FarMedAPI.Models;

namespace FarMedAPI.Service
{
    public interface IEmpleadoService
    {
        Task<List<Empleado>> GetAllAsync();
        Task<Empleado> GetByIdAsync(int id);
        Task AddAsync(Empleado empleado);
        Task UpdateAsync(Empleado empleado);
        Task DeleteAsync(int id);
    }
}
