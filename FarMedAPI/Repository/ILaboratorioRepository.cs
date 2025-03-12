using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Repositories
{
    public interface ILaboratorioRepository
    {
        Task<List<Laboratorio>> GetAllAsync();
        Task AddAsync(Laboratorio laboratorio);
    }
}
