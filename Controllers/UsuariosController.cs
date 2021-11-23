using AutoMapper;
using Finanzas.Domain.Models;
using Finanzas.Domain.Services;
using Finanzas.Domain.Services.Communications;
using Finanzas.Extentions;
using Finanzas.Resources;
using Microsoft.AspNetCore.Authorization;
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
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPerfilService _perfilService;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioService usuarioService, IPerfilService perfilService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _perfilService = perfilService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsuarioResource>), 200)]
        public async Task<IEnumerable<UsuarioResource>> GetAllAsync()
        {
            var usuarios = await _usuarioService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioResource>>(usuarios);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _usuarioService.GetById(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var usuarioResource = _mapper.Map<Usuario, UsuarioResource>(result.Resource);
            return Ok(usuarioResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UsuarioResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveUsuarioResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var usuario = _mapper.Map<SaveUsuarioResource, Usuario>(resource);
            var result = await _usuarioService.UpdateAsync(id, usuario);

            if (!result.Success)
                return BadRequest(result.Message);

            var usuarioResource = _mapper.Map<Usuario, UsuarioResource>(result.Resource);
            return Ok(usuarioResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(UsuarioResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            var result = await _usuarioService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var usuarioResource = _mapper.Map<Usuario, UsuarioResource>(result.Resource);
            return Ok(usuarioResource);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveUsuarioResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var usuario = _mapper.Map<SaveUsuarioResource, Usuario>(resource);
            var result = await _usuarioService.SaveAsync(usuario);

            if (!result.Success)
                return BadRequest(result.Message);

            var usuarioResource = _mapper.Map<Usuario, UsuarioResource>(result.Resource);
            return Ok(usuarioResource);
        }

        [HttpPost("{id}/perfiles")]
        [ProducesResponseType(typeof(UsuarioResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostPerfilAsync(int id,[FromBody] SavePerfilResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var perfil = _mapper.Map<SavePerfilResource, Perfil>(resource);
            var result = await _perfilService.SaveAsync(id,perfil);

            if (!result.Success)
                return BadRequest(result.Message);

            var perfilResource = _mapper.Map<Perfil, PerfilResource>(result.Resource);
            return Ok(perfilResource);
        }

        [HttpGet("{id}/perfiles")]
        [ProducesResponseType(typeof(PerfilResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetPerfilByUsuarioIdAsync(int id)
        {
            var result = await _perfilService.GetByUserId(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var perfilResource = _mapper.Map<Perfil, PerfilResource>(result.Resource);
            return Ok(perfilResource);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            var response = await _usuarioService.Authenticate(request);

            if (response == null)
                return BadRequest(new { message = "Correo o contraseña invalidos" });

            return Ok(response);
        }


    }
}
