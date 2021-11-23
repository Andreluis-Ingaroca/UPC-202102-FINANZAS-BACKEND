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
    public class CostosController : ControllerBase
    {
        private readonly ICostoService _costoService;
        private readonly IMapper _mapper;

        public CostosController(ICostoService costoService, IMapper mapper)
        {
            _costoService = costoService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CostoResource>), 200)]
        public async Task<IEnumerable<CostoResource>> GetAllAsync()
        {
            var costos = await _costoService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Costo>, IEnumerable<CostoResource>>(costos);

            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CostoResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _costoService.GetById(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var costosResource = _mapper.Map<Costo, CostoResource>(result.Resource);
            return Ok(costosResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CostoResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveCostoResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var costo = _mapper.Map<SaveCostoResource, Costo>(resource);
            var result = await _costoService.SaveAsync(costo);

            if (!result.Success)
                return BadRequest(result.Message);

            var costosResource = _mapper.Map<Costo, CostoResource>(result.Resource);
            return Ok(costosResource);


        }



        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CostoResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _costoService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var costosResource = _mapper.Map<Costo, CostoResource>(result.Resource);
            return Ok(costosResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CostoResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCostoResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var costo = _mapper.Map<SaveCostoResource, Costo>(resource);
            var result = await _costoService.UpdateAsync(id,costo);
            if (!result.Success)
                return BadRequest(result.Message);

            var costosResource = _mapper.Map<Costo, CostoResource>(result.Resource);
            return Ok(costosResource);
        }

    }
}
