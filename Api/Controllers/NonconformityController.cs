using System.Collections.Generic;
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
        private readonly NonconformityRepository _repository;

        public NonconformityController(NonconformityRepository repository, ILogger<NonconformityController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        //public async Task<ActionResult<List<Nonconformity>>> Get([FromServices] NonconformityContext context)
        public IEnumerable<Nonconformity> Get()
        {
            return _repository.GetAll();
            //var nonconformities = await context.Nonconformities.Include(x => x.Actions).ToListAsync();
            //return nonconformities;
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
