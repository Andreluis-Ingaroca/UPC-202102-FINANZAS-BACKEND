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
    public class CarterasController : ControllerBase
    {
        private readonly ICarteraService _carteraService;
        private readonly ILetraService _letraService;
        private readonly IOperacionCarteraService _operacionCarteraService;
        private readonly IMapper _mapper;

        public CarterasController(ICarteraService carteraService, IMapper mapper, ILetraService letraService, IOperacionCarteraService operacionCarteraService)
        {
            _carteraService = carteraService;
            _mapper = mapper;
            _letraService = letraService;
            _operacionCarteraService = operacionCarteraService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CarteraResource>), 200)]
        public async Task<IEnumerable<CarteraResource>> GetAllAsync()
        {
            var carteras = await _carteraService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Cartera>, IEnumerable<CarteraResource>>(carteras);
            return resources;
        }

        
        [HttpGet("{id}/letras")]
        [ProducesResponseType(typeof(IEnumerable<LetraResource>), 200)]
        public async Task<IEnumerable<LetraResource>> GetAllLetrasAsync(int id)
        {
            var letras = await _letraService.ListByCarteraId(id);
            var resources = _mapper.Map<IEnumerable<Letra>, IEnumerable<LetraResource>>(letras);
            return resources;
        }
        [HttpGet("{id}/operaciones")]
        [ProducesResponseType(typeof(IEnumerable<ToShowOperacionesResource>), 200)]
        public async Task<IEnumerable<ToShowOperacionesResource>> GetAllOperacionesAsync(int id)
        {
            var resources = await _carteraService.ListOperacionesByCarteraId(id);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CarteraResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _carteraService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var carteraResource = _mapper.Map<Cartera, CarteraResource>(result.Resource);
            return Ok(carteraResource);
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CarteraResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _carteraService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var carteraResource = _mapper.Map<Cartera, CarteraResource>(result.Resource);
            return Ok(carteraResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CarteraResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCarteraResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var cartera = _mapper.Map<SaveCarteraResource, Cartera>(resource);
            var result = await _carteraService.UpdateAsync(id,cartera);
            
            if (!result.Success)
                return BadRequest(result.Message);

            var carteraResource = _mapper.Map<Cartera, CarteraResource>(result.Resource);
            return Ok(carteraResource);
        }


        [HttpPost("{id}/letras")]
        [ProducesResponseType(typeof(LetraResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutLetraAsync(int id, [FromBody] SaveLetraResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var letra = _mapper.Map<SaveLetraResource, Letra>(resource);
            var result = await _letraService.SaveAsync(id, letra);

            if (!result.Success)
                return BadRequest(result.Message);

            var letraResource = _mapper.Map<Letra, LetraResource>(result.Resource);
            return Ok(letraResource);
        }
        
        [HttpGet("{id}/operacionCarteras")]
        [ProducesResponseType(typeof(IEnumerable<OperacionCarteraResource>), 200)]
        public async Task<IEnumerable<OperacionCarteraResource>> GetOperacionCarteraByCarteraIdAsync(int id)
        {
            var operacionCarteras = await _operacionCarteraService.ListByCarteraAsync(id);
            var resources = _mapper.Map<IEnumerable<OperacionCartera>, IEnumerable<OperacionCarteraResource>>(operacionCarteras);
            return resources;
        }

        [HttpGet("{id}/operacion/{operacionId}")]
        public async Task<IActionResult> GetOperacionCartera(int id, int operacionId)
        {
            var result = await _operacionCarteraService.GetOperacionCarteraAsync(operacionId,id);
            if (!result.Success)
                return BadRequest(result.Message);

            var operacionCarteraResource = _mapper.Map<OperacionCartera, OperacionCarteraResource>(result.Resource);
            return Ok(operacionCarteraResource);
        }

        [HttpPost("{id}/operacion/{operacionId}")]
        public async Task<IActionResult> AssignOperacionCartera(int id, int operacionId)
        {
            var result = await _operacionCarteraService.AssignOperacionCarteraAsync(operacionId,id);
            if (!result.Success)
                return BadRequest(result.Message);

            var operacionCarteraResource = _mapper.Map<OperacionCartera, OperacionCarteraResource>(result.Resource);
            return Ok(operacionCarteraResource);
        }

        [HttpDelete("{id}/operacion/{operacionId}")]
        public async Task<IActionResult> UnassignOperacionCartera(int id, int operacionId)
        {
            var result = await _operacionCarteraService.UnassignOperacionCarteraAsync(operacionId,id);
            if (!result.Success)
                return BadRequest(result.Message);

            var operacionCarteraResource = _mapper.Map<OperacionCartera, OperacionCarteraResource>(result.Resource);
            return Ok(operacionCarteraResource);
        }
    }
}
