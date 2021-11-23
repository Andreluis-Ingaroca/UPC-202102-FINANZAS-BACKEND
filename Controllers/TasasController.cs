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
    public class TasasController : ControllerBase
    {
        private readonly ITasaService _tasaService;
        private readonly IOperacionService _operacionService;
        private readonly IMapper _mapper;

        public TasasController(ITasaService tasaService, IOperacionService operacionService, IMapper mapper)
        {
            _tasaService = tasaService;
            _operacionService = operacionService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TasaResource>), 200)]
        public async Task<IEnumerable<TasaResource>> GetAllAsync()
        {
            var tasas = await _tasaService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Tasa>, IEnumerable<TasaResource>>(tasas);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TasaResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _tasaService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var tasaResource = _mapper.Map<Tasa, TasaResource>(result.Resource);
            return Ok(tasaResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TasaResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTasaResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var tasa = _mapper.Map<SaveTasaResource, Tasa>(resource);
            var result = await _tasaService.UpdateAsync(id,tasa);

            if (!result.Success)
                return BadRequest(result.Message);

            var tasaResource = _mapper.Map<Tasa, TasaResource>(result.Resource);
            return Ok(tasaResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TasaResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _tasaService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var tasaResource = _mapper.Map<Tasa, TasaResource>(result.Resource);
            return Ok(tasaResource);
        }

        [HttpPost("{id}/operaciones")]
        [ProducesResponseType(typeof(OperacionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostOperacionAsync(int id, [FromBody] SaveOperacionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var operacion = _mapper.Map<SaveOperacionResource, Operacion>(resource);
            var result = await _operacionService.SaveAsync(id, operacion);

            if (!result.Success)
                return BadRequest(result.Message);

            var operacionResource = _mapper.Map<Operacion, OperacionResource>(result.Resource);
            return Ok(operacionResource);
        }


    }
}
