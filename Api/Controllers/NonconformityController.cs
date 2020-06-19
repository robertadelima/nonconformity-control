using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NonconformityControl.Models;
using NonconformityControl.Infra.Repositories;

namespace NonconformityControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NonconformityController : ControllerBase
    {
        private readonly ILogger<NonconformityController> _logger;

        public NonconformityController(ILogger<NonconformityController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Nonconformity>>> Get([FromServices] NonconformityContext context)
        {
            var nonconformities = await context.Nonconformities.Include(x => x.Actions).ToListAsync();
            return nonconformities;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Nonconformity>> Post([FromServices] NonconformityContext context, [FromBody]Nonconformity model)
        {
            if(ModelState.IsValid) //validando o que está de required na entidade Nonconformity
            {
                context.Nonconformities.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState); 
            }
        }

    }
}
