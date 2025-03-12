using FarMedAPI.Models;
using FarMedAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarMedAPI.Services
{
    public class LaboratorioService : ILaboratorioService
    {
        private readonly ILaboratorioRepository _laboratorioRepository;

        public LaboratorioService(ILaboratorioRepository laboratorioRepository)
        {
            _laboratorioRepository = laboratorioRepository;
        }

        public async Task<List<Laboratorio>> GetAllAsync()
        {
            return await _laboratorioRepository.GetAllAsync();
        }

        public async Task AddAsync(Laboratorio laboratorio)
        {
            await _laboratorioRepository.AddAsync(laboratorio);
        }
    }
}
