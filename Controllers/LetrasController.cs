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
    public class LetrasController : ControllerBase
    {
        private readonly ILetraService _letraService;
        private readonly IOperacionLetraService _operacionLetraService;
        private readonly IMapper _mapper;

        public LetrasController(ILetraService letraService, IMapper mapper, IOperacionLetraService operacionLetraService)
        {
            _letraService = letraService;
            _mapper = mapper;
            _operacionLetraService = operacionLetraService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LetraResource>), 200)]
        public async Task<IEnumerable<LetraResource>> GetAllAsync()
        {
            var letras = await _letraService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Letra>, IEnumerable<LetraResource>>(letras);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LetraResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _letraService.GetById(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var letraResource = _mapper.Map<Letra, LetraResource>(result.Resource);
            return Ok(letraResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(LetraResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _letraService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var letraResource = _mapper.Map<Letra, LetraResource>(result.Resource);
            return Ok(letraResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(LetraResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveLetraResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var letra = _mapper.Map<SaveLetraResource, Letra>(resource);
            var result = await _letraService.UpdateAsync(id,letra);
            if (!result.Success)
                return BadRequest(result.Message);

            var letraResource = _mapper.Map<Letra, LetraResource>(result.Resource);
            return Ok(letraResource);
        }

        [HttpGet("{id}/operacionLetras")]
        [ProducesResponseType(typeof(IEnumerable<OperacionLetraResource>), 200)]
        public async Task<IEnumerable<OperacionLetraResource>> GetAllOperacionLetraByOperacionIdAsync(int id)
        {
            var operacionLetra = await _operacionLetraService.ListByLetraIdAsync(id);
            var resources = _mapper.Map<IEnumerable<OperacionLetra>, IEnumerable<OperacionLetraResource>>(operacionLetra);
            return resources;
        }

        [HttpGet("{letraId}/operacion/{operacionId}")]
        public async Task<IActionResult> GetOperacionLetra(int letraId, int operacionId)
        {
            var result = await _operacionLetraService.GetByLetraIdAndOperacionAsync(letraId, operacionId);
            if (!result.Success)
                return BadRequest(result.Message);

            var operacionLetraResource = _mapper.Map<OperacionLetra, OperacionLetraResource>(result.Resource);
            return Ok(operacionLetraResource);
        }

        [HttpPost("{letraId}/operacion/{operacionId}")]
        public async Task<IActionResult> AssignOperacionLetra(int letraId, int operacionId)
        {
            var result = await _operacionLetraService.AssignOperacionLetraAsync(operacionId,letraId);
            if (!result.Success)
                return BadRequest(result.Message);

            var operacionLetraResource = _mapper.Map<OperacionLetra, OperacionLetraResource>(result.Resource);
            return Ok(operacionLetraResource);
        }

        [HttpDelete("{letraId}/operacion/{operacionId}")]
        public async Task<IActionResult> UnassignOperacionLetra(int letraId, int operacionId)
        {
            var result = await _operacionLetraService.UnassignOperacionLetraAsync(operacionId, letraId);
            if (!result.Success)
                return BadRequest(result.Message);

            var operacionLetraResource = _mapper.Map<OperacionLetra, OperacionLetraResource>(result.Resource);
            return Ok(operacionLetraResource);
        }


    }
}
