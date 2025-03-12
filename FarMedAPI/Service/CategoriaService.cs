using FarMedAPI.Models;
using FarMedAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarMedAPI.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<List<Categoria>> GetAllAsync()
        {
            return await _categoriaRepository.GetAllAsync();
        }

        public async Task AddAsync(Categoria categoria)
        {
            await _categoriaRepository.AddAsync(categoria);
        }
    }
}
