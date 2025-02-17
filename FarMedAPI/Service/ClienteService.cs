using System.Collections.Generic;
using System.Threading.Tasks;
using FarMedAPI.Models;
using FarMedAPI.Repository;

namespace FarMedAPI.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _clienteRepository.GetAllAsync();
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await _clienteRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Cliente cliente)
        {
            await _clienteRepository.AddAsync(cliente);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task DeleteAsync(int id)
        {
            await _clienteRepository.DeleteAsync(id);
        }
    }
}
