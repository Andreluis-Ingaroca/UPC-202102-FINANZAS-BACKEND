using AutoMapper;
using Finanzas.Domain.Models;
using Finanzas.Domain.Services;
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
    public class OperacionLetrasController : ControllerBase
    {
        private readonly IOperacionLetraService _operacionLetra;
        private readonly IMapper _mapper;

        public OperacionLetrasController(IOperacionLetraService operacionLetra, IMapper mapper)
        {
            _operacionLetra = operacionLetra;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OperacionLetraResource>), 200)]
        public async Task<IEnumerable<OperacionLetraResource>> GetAllAsync()
        {
            var opercionLetra= await _operacionLetra.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<OperacionLetra>, IEnumerable<OperacionLetraResource>>(opercionLetra);
            return resources;
        }

    }
}
