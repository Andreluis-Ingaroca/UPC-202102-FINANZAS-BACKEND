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
    public class CostosOperacionController : ControllerBase
    {
        private readonly ICostosOperacionService _costosOperacionService;
        private readonly IMapper _mapper;

        public CostosOperacionController(ICostosOperacionService costosOperacionService, IMapper mapper)
        {
            _costosOperacionService = costosOperacionService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CostosOperacionResource>), 200)]
        public async Task<IEnumerable<CostosOperacionResource>> GetAllAsync()
        {
            var costosOperacion = await _costosOperacionService.ListAsync();
            var resources = _mapper.Map<IEnumerable<CostosOperacion>, IEnumerable<CostosOperacionResource>>(costosOperacion);
            return resources;
        }


    }
}
