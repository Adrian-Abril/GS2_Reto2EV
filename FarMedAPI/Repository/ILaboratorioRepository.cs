// Repository/ILaboratorioRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Repository
{
    public interface ILaboratorioRepository
    {
        Task<IEnumerable<Laboratorio>> GetAllLaboratoriosAsync();
        Task<Laboratorio> GetLaboratorioByIdAsync(int id);
        Task<Laboratorio> CreateLaboratorioAsync(Laboratorio laboratorio);
        Task<Laboratorio> UpdateLaboratorioAsync(Laboratorio laboratorio);
        Task<bool> DeleteLaboratorioAsync(int id);
        Task<bool> LaboratorioExistsAsync(int id);
    }
}