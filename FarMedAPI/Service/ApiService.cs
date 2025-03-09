using System.Threading.Tasks;
using FarMedAPI.Models;

namespace FarMedAPI.Service
{
    // Esta interfaz no debe duplicar métodos ya definidos en otras interfaces
    public interface IApiService
    {
        // Métodos específicos que no existan en otras interfaces
        Task<string> GetApiVersion();
    }

    public class ApiService : IApiService
    {
        public Task<string> GetApiVersion()
        {
            return Task.FromResult("1.0.0");
        }
    }
}