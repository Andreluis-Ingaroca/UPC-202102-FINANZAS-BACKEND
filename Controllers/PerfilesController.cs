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
    public class PerfilesController : ControllerBase
    {
        private readonly IPerfilService _perfilService;
        private readonly ICarteraService _carteraService;
        private readonly IMapper _mapper;

        public PerfilesController(IPerfilService perfilService, IMapper mapper, ICarteraService carteraService)
        {
            _perfilService = perfilService;
            _mapper = mapper;
            _carteraService = carteraService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PerfilResource>), 200)]
        public async Task<IEnumerable<PerfilResource>> GetAllAsync()
        {
            var perfiles = await _perfilService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Perfil>, IEnumerable<PerfilResource>>(perfiles);

            return resources;

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PerfilResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _perfilService.GetById(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var perfilResource = _mapper.Map<Perfil, PerfilResource>(result.Resource);

            return Ok(perfilResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PerfilResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _perfilService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var perfilResource = _mapper.Map<Perfil, PerfilResource>(result.Resource);

            return Ok(perfilResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PerfilResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SavePerfilResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var perfil = _mapper.Map<SavePerfilResource, Perfil>(resource);
            var result = await _perfilService.UpdateAsync(id, perfil);

            if (!result.Success)
                return BadRequest(result.Message);

            var perfilResource = _mapper.Map<Perfil, PerfilResource>(result.Resource);

            return Ok(perfilResource);
        }


        [HttpGet("{id}/carteras")]
        [ProducesResponseType(typeof(IEnumerable<CarteraResource>), 200)]
        public async Task<IEnumerable<CarteraResource>> GetAllCarterasAsync(int id)
        {
            var carteras = await _carteraService.ListByPerfilIdAsync(id);
            var resources = _mapper.Map<IEnumerable<Cartera>, IEnumerable<CarteraResource>>(carteras);

            return resources;

        }

        [HttpPost("{id}/carteras")]
        [ProducesResponseType(typeof(CarteraResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostCarteraAsync(int id, [FromBody] SaveCarteraResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var cartera = _mapper.Map<SaveCarteraResource, Cartera>(resource);
            var result = await _carteraService.SaveAsync(id, cartera);

            if (!result.Success)
                return BadRequest(result.Message);

            var carteraResource = _mapper.Map<Cartera, CarteraResource>(result.Resource);

            return Ok(carteraResource);
        }


    }
}
