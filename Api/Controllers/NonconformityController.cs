using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NonconformityControl.Models;
using NonconformityControl.Infra.Repositories;
using NonconformityControl.Api.ViewModels;

namespace NonconformityControl.Controllers
{
    [ApiController]
    [Route("nonconformities")]
    public class NonconformityController : ControllerBase
    {
        private readonly ILogger<NonconformityController> _logger;
        private readonly NonconformityRepository _repository;
        private readonly ActionRepository _actionRepository;

        public NonconformityController(NonconformityRepository repository, ILogger<NonconformityController> logger,
        ActionRepository actionRepository)
        {
            _repository = repository;
            _actionRepository = actionRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<ListNonconformityViewModel> Get()
        {
            return _repository.GetAll().Select(p => new ListNonconformityViewModel
            {
                Id = p.Id,
                Description = p.Description,
                Code = p.Code,
                Status = p.Status,
                Actions = p.Actions
            }).ToList();
        }

        [HttpPost]
        [Route("")]
        public string Post([FromBody]AddNonconformityViewModel model)
        {
            //model.Validate();
            Nonconformity nonconformity = new Nonconformity(model.Description);
            _repository.Add(nonconformity);
            return "Nonconformity successfully salved";
        }

        [HttpGet]
        [Route("{id}")]
        public Nonconformity GetById(int id)
        {
            return _repository.GetById(id);
        }

        [HttpPost]
        [Route("{id}/actions")]
        public string PostActions([FromBody]Action model)
        {
            _actionRepository.Add(model);
            return "Action successfully salved";
        }


    }
}
