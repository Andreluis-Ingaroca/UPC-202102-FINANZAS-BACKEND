using AutoMapper;
using Finanzas.Domain.Models;
using Finanzas.Domain.Services;
using Finanzas.Extentions;
using Finanzas.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class PeriodosController : ControllerBase
    {
        private readonly IPeriodoService _periodoService;
        private readonly ITasaService _tasaService;
        private readonly IMapper _mapper;

        public PeriodosController(IPeriodoService periodoService, IMapper mapper, ITasaService tasaService)
        {
            _periodoService = periodoService;
            _mapper = mapper;
            _tasaService = tasaService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PeriodoResource>), 200)]
        public async Task<IEnumerable<PeriodoResource>> GetAllAsync()
        {
            var periodos = await _periodoService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Periodo>, IEnumerable<PeriodoResource>>(periodos);

            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PeriodoResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _periodoService.GetById(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var periodoResource = _mapper.Map<Periodo, PeriodoResource>(result.Resource);
            return Ok(periodoResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PeriodoResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SavePeriodoResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var periodo = _mapper.Map<SavePeriodoResource, Periodo>(resource);
            var result = await _periodoService.SaveAsync(periodo);

            if (!result.Success)
                return BadRequest(result.Message);

            var periodoResource = _mapper.Map<Periodo, PeriodoResource>(result.Resource);
            return Ok(periodoResource);

        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PeriodoResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id,[FromBody] SavePeriodoResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var periodo = _mapper.Map<SavePeriodoResource, Periodo>(resource);
            var result = await _periodoService.UpdateAsync(id,periodo);

            if (!result.Success)
                return BadRequest(result.Message);

            var periodoResource = _mapper.Map<Periodo, PeriodoResource>(result.Resource);
            return Ok(periodoResource);

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PeriodoResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _periodoService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var periodoResource = _mapper.Map<Periodo, PeriodoResource>(result.Resource);
            return Ok(periodoResource);

        }

        [HttpPost("{id}/periodos/{idC}/tasas")]
        [ProducesResponseType(typeof(PeriodoResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostTasaAsync(int id , int idC,[FromBody] SaveTasaResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var tasa = _mapper.Map<SaveTasaResource, Tasa>(resource);
            var result = await _tasaService.SaveAsync(id,idC,tasa);

            if (!result.Success)
                return BadRequest(result.Message);

            var tasaResource = _mapper.Map<Tasa, TasaResource>(result.Resource);
            return Ok(tasaResource);

        }
    }
}
