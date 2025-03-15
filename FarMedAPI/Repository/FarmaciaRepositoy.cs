using FarMedAPI.Repository;
using FarMedAPI.Models;

namespace FarMedAPI.Repositories
{
    public class FarmaciaRepository : IFarmaciaRepository
    {
        private readonly List<Farmacia> _farmacias;

        public FarmaciaRepository()
        {
            _farmacias = new List<Farmacia>
            {
                new Farmacia { Id_Farmacia = 1, Nombre = "Juan Pérez", Dirección = "juan@example.com", Teléfono = "1234", Horario_Apertura = "123456789", Horario_Cierre = "Lunes a Viernes / 14:00",},
                new Farmacia { Id_Farmacia = 2, Nombre = "María López", Dirección = "maria@example.com", Teléfono = "abcd", Horario_Apertura = "987654321", Horario_Cierre = "User",}
            };
        }

        public Task<List<Farmacia>> GetAllAsync()
        {
            return Task.FromResult(_farmacias);
        }

        public Task<Farmacia> GetByIdAsync(int id)
        {
            var farmacia = _farmacias.FirstOrDefault(c => c.Id_Farmacia == id);
            return Task.FromResult(farmacia);
        }

        public Task AddAsync(Farmacia farmacia)
        {
            farmacia.Id_Farmacia = _farmacias.Count + 1;
            _farmacias.Add(farmacia);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Farmacia farmacia)
        {
            var existingFarmacia = _farmacias.FirstOrDefault(c => c.Id_Farmacia == farmacia.Id_Farmacia);
            if (existingFarmacia != null)
            {
                existingFarmacia.Nombre = farmacia.Nombre;
                existingFarmacia.Dirección = farmacia.Dirección;
                existingFarmacia.Teléfono = farmacia.Teléfono;
                existingFarmacia.Horario_Apertura = farmacia.Horario_Apertura;
                existingFarmacia.Horario_Cierre = farmacia.Horario_Cierre;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var farmacia = _farmacias.FirstOrDefault(c => c.Id_Farmacia == id);
            if (farmacia != null)
            {
                _farmacias.Remove(farmacia);
            }
            return Task.CompletedTask;
        }
    }
}

