using Microsoft.AspNetCore.Mvc;
using FarMedAPI.Services;
using FarMedAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarMedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratorioController : ControllerBase
    {
        private readonly ILaboratorioService _laboratorioService;

        public LaboratorioController(ILaboratorioService laboratorioService)
        {
            _laboratorioService = laboratorioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Laboratorio>>> GetAllLaboratoriosAsync()
        {
            return Ok(await _laboratorioService.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult> AddLaboratorioAsync([FromBody] Laboratorio laboratorio)
        {
            await _laboratorioService.AddAsync(laboratorio);
            return CreatedAtAction(nameof(GetAllLaboratoriosAsync), new { id = laboratorio.LaboratorioId }, laboratorio);
        }
    }
}
