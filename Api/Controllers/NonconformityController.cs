using System.Linq;
using System.Collections.Generic;
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

        /// <summary>
        /// Returns all nonconformities 
        /// </summary>
        /// <returns> List of nonconformities </returns>
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
                Actions = p.Actions.Select(p => new ActionViewModel
                {
                    Description = p.Description,
                    Id = p.Id
                }).ToList()
            }).ToList();
        }

        /// <summary>
        /// Returns a nonconformity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Nonconformities </returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var nonconformity = _repository.GetById(id);
            if(nonconformity == null)
            {
                return NotFound();
            }
            
            return new ObjectResult(nonconformity); // TODO
        }

        /// <summary>
        /// Creates a nonconformity
        /// </summary>
        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody]AddNonconformityViewModel request)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            };
            Nonconformity nonconformity = new Nonconformity(request.Description);
            nonconformity = _repository.Add(nonconformity);
            var resultViewModel = new ResultViewModel(true, nonconformity.Id, "Nonconformity successfully saved!");
            
            return new ObjectResult(resultViewModel);
        }

        /// <summary>
        /// Deletes a nonconformity
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteNonconformity(int id)
        {
            var nonconformity = _repository.GetById(id);
            if(nonconformity == null)
            {
                return BadRequest("");
            }
            _repository.Remove(nonconformity);
            var resultViewModel = new ResultViewModel(true, nonconformity.Id, "Nonconformity successfully removed!");
            
            return new ObjectResult(resultViewModel);
        }

        /// <summary>
        /// Creates an action in a nonconformity
        /// </summary>
        [HttpPost]
        [Route("{id}/actions")]
        public IActionResult PostActions(int id, [FromBody]AddActionViewModel request)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            };
            var nonconformity = _repository.GetById(id);
            if(nonconformity == null)
            {
                return BadRequest("");
            }
            Action action = new Action(id, request.Description);
            _repository.AddActionToNonconformity(id, action);
            _actionRepository.Add(action);
            return Ok("Action successfully saved!");
        }

        /// <summary>
        /// Evaluates a nonconformity as efficient
        /// </summary>
        [HttpPut]
        [Route("{id}/efficient")]
        public IActionResult PutEfficient(int id)
        {
            var nonconformity = _repository.GetById(id);
            if(nonconformity == null)
            {
                return BadRequest();
            }
            _repository.UpdateAsEfficient(id);
            return Ok("Successfully set as efficient nonconformity!");
        }

        /// <summary>
        /// Evaluates a nonconformity as inefficient
        /// </summary>
        [HttpPut]
        [Route("{id}/inefficient")]
        public IActionResult PutInefficient(int id)
        {
            var nonconformity = _repository.GetById(id);
            if(nonconformity == null)
            {
                return BadRequest();
            }
            _repository.UpdateAsInefficient(id);
            var newNonconformity = new Nonconformity(nonconformity.Description, nonconformity.Version + 1);
            _repository.Add(newNonconformity);
            return Ok("Successfully set as inefficient nonconformity and new version of nonconformity created!");
        }

        /// <summary>
        /// Lists Nonconformity Status Types
        /// </summary>
        [HttpGet]
        [Route("status")]
        public List<EnumViewModel> ListStatus()
        {
            return System.Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>().Select(p => new EnumViewModel {
                Id = ((int)p),
                Description = p.ToString()
            }).ToList();
        }

        /// <summary>
        /// Lists Nonconformity Evaluations Types
        /// </summary>
        [HttpGet]
        [Route("evaluation")]
        public List<EnumViewModel> ListEvaluation()
        {
            return System.Enum.GetValues(typeof(EvaluationEnum)).Cast<EvaluationEnum>().Select(p => new EnumViewModel {
                Id = ((int)p),
                Description = p.ToString()
            }).ToList();
        }


    }
}
