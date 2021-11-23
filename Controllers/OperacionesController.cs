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
    public class OperacionesController : ControllerBase
    {
        private readonly IOperacionService _operacionService;
        private readonly ICostosOperacionService _costosOperacionService;
        private readonly IOperacionLetraService _operacionLetraService;
        private readonly IMapper _mapper;

        public OperacionesController(IOperacionService operacionService, IMapper mapper, ICostosOperacionService costosOperacionService, IOperacionLetraService operacionLetraService)
        {
            _operacionService = operacionService;
            _mapper = mapper;
            _costosOperacionService = costosOperacionService;
            _operacionLetraService = operacionLetraService;
        }

        [HttpGet("{id}/results")]
        [ProducesResponseType(typeof(IEnumerable<ResultOperacionLetraResource>), 200)]
        public async Task<IEnumerable<ResultOperacionLetraResource>> GetAllOperacionesAsync(int id)
        {
            var resources = await _operacionLetraService.ListLetraOperacionesByCarteraId(id);
            return resources;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OperacionResource>), 200)]
        public async Task<IEnumerable<OperacionResource>> GetAllAsync()
        {
            var operaciones = await _operacionService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Operacion>, IEnumerable<OperacionResource>>(operaciones);
            return resources;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OperacionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _operacionService.GetById(id);

            if (!result.Success)
                return BadRequest(result.Message);


            var operacionResource = _mapper.Map<Operacion, OperacionResource>(result.Resource);
            return Ok(operacionResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(OperacionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _operacionService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var operacionResource = _mapper.Map<Operacion, OperacionResource>(result.Resource);
            return Ok(operacionResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OperacionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveOperacionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var operacion = _mapper.Map<SaveOperacionResource, Operacion>(resource);
            var result = await _operacionService.UpdateAsync(id,operacion);

            if (!result.Success)
                return BadRequest(result.Message);

            var operacionResource = _mapper.Map<Operacion, OperacionResource>(result.Resource);
            return Ok(operacionResource);
        }

        [HttpGet("{id}/costosOperacion")]
        [ProducesResponseType(typeof(IEnumerable<CostosOperacionResource>), 200)]
        public async Task<IEnumerable<CostosOperacionResource>> GetAllCostosOperacionByOperacionIdAsync(int id)
        {
            var costosOperacion = await _costosOperacionService.ListByOperacionIdAsync(id);
            var resources = _mapper.Map<IEnumerable<CostosOperacion>, IEnumerable<CostosOperacionResource>>(costosOperacion);
            return resources;
        }

        [HttpGet("{id}/operacionLetras")]
        [ProducesResponseType(typeof(IEnumerable<OperacionLetraResource>), 200)]
        public async Task<IEnumerable<OperacionLetraResource>> GetAllOperacionLetraByOperacionIdAsync(int id)
        {
            var operacionLetra = await _operacionLetraService.ListByOperacionIdAsync(id);
            var resources = _mapper.Map<IEnumerable<OperacionLetra>, IEnumerable<OperacionLetraResource>>(operacionLetra);
            return resources;
        }

        [HttpPost("{id}/costos/{costoId}")]
        [ProducesResponseType(typeof(IEnumerable<CostosOperacionResource>), 200)]
        public async Task<IActionResult> AssignCostoOperacionAsync(int id, int costoId, [FromBody] SaveCostosOperacionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var costosOperacion = _mapper.Map<SaveCostosOperacionResource, CostosOperacion>(resource);
            var result = await _costosOperacionService.AssignCostoOperacionAsync(costoId,id,costosOperacion);

            if (!result.Success)
                return BadRequest(result.Message);

            var costosOperacionResource = _mapper.Map<CostosOperacion, CostosOperacionResource>(result.Resource);
            return Ok(costosOperacionResource);
        }

        [HttpDelete("{id}/costos/{costoId}")]
        [ProducesResponseType(typeof(IEnumerable<CostosOperacionResource>), 200)]
        public async Task<IActionResult> UnassignCostoOperacionAsync(int id, int costoId)
        {
            var result = await _costosOperacionService.UnassignCostoOperacionAsync(costoId,id);

            if (!result.Success)
                return BadRequest(result.Message);

            var costosOperacionResource = _mapper.Map<CostosOperacion, CostosOperacionResource>(result.Resource);
            return Ok(costosOperacionResource);
        }


    }
}