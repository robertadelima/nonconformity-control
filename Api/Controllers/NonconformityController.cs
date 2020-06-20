using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<Nonconformity> Get()
        {
            return _repository.GetAll();
        }

        [HttpPost]
        [Route("")]
        public string Post([FromBody]Nonconformity model)
        {
            _repository.Add(model);
            return "Nonconformity successfully salved";
        }

    }
}
