// Service/ILaboratorioService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Service
{
    public interface ILaboratorioService
    {
        Task<IEnumerable<Laboratorio>> GetAllLaboratoriosAsync();
        Task<Laboratorio> GetLaboratorioByIdAsync(int id);
        Task<Laboratorio> CreateLaboratorioAsync(Laboratorio laboratorio);
        Task<Laboratorio> UpdateLaboratorioAsync(int id, Laboratorio laboratorio);
        Task<bool> DeleteLaboratorioAsync(int id);
    }
}