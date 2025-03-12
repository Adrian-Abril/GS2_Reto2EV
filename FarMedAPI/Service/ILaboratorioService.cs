using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Services
{
    public interface ILaboratorioService
    {
        Task<List<Laboratorio>> GetAllAsync();
        Task AddAsync(Laboratorio laboratorio);
    }
}
