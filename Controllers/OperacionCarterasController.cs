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
    public class OperacionCarterasController : ControllerBase
    {
        private readonly IOperacionCarteraService _operacionCarteraService;
        private readonly IMapper _mapper;

        public OperacionCarterasController(IOperacionCarteraService operacionCarteraService, IMapper mapper)
        {
            _operacionCarteraService = operacionCarteraService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OperacionCarteraResource>), 200)]
        public async Task<IEnumerable<OperacionCarteraResource>> GetAllAsync()
        {
            var operacionCartera = await _operacionCarteraService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<OperacionCartera>, IEnumerable<OperacionCarteraResource>>(operacionCartera);
            return resources;
        }


    }
}
