using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NonconformityControl.Api.ViewModels;
using NonconformityControl.Services;

namespace NonconformityControl.Controllers
{
    [ApiController]
    [Route("nonconformities")]
    public class NonconformityController : ControllerBase
    {
        private readonly ILogger<NonconformityController> _logger;
        private readonly NonconformityService _nonconformityService;

        public NonconformityController(NonconformityService nonconformityService, ILogger<NonconformityController> logger)
        {
            _nonconformityService = nonconformityService;
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
            return _nonconformityService.ListNonconformities();
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
            var nonconformity = _nonconformityService.GetById(id);
            if(nonconformity == null)
            {
                return NotFound(new ResultViewModel(false, id, "Nonconformity does not exist!"));
            }
            return new ObjectResult(nonconformity); 
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
            var resultViewModel = _nonconformityService.AddNonconformity(request);
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
            var resultViewModel = _nonconformityService.RemoveNonconformity(id);
            if(!resultViewModel.Success)
            {
                return NotFound(resultViewModel);
            }
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
                return BadRequest();
            };
            var resultViewModel = _nonconformityService.AddAction(id, request);
            if(!resultViewModel.Success)
            {
                return NotFound(resultViewModel);
            }
            return new ObjectResult(resultViewModel);
        }

        /// <summary>
        /// Deletes an action
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actionId"></param>
        [HttpDelete]
        [Route("{id}/actions/{actionId}")]
        public IActionResult DeleteAction(int id, int actionId)
        {
            var resultViewModel = _nonconformityService.RemoveAction(id, actionId);
            if(!resultViewModel.Success)
            {
                return NotFound(resultViewModel);
            }
            return new ObjectResult(resultViewModel);
        }

        /// <summary>
        /// Evaluates a nonconformity as efficient
        /// </summary>
        [HttpPut]
        [Route("{id}/efficient")]
        public IActionResult PutEfficient(int id)
        {
            var resultViewModel = _nonconformityService.EvaluateAsEfficient(id);
            if(!resultViewModel.Success)
            {
                return NotFound(resultViewModel);
            }
            return new ObjectResult(resultViewModel);
        }

        /// <summary>
        /// Evaluates a nonconformity as inefficient
        /// </summary>
        [HttpPut]
        [Route("{id}/inefficient")]
        public IActionResult PutInefficient(int id)
        {
            var resultViewModel = _nonconformityService.EvaluateAsInefficient(id);
            if(!resultViewModel.Success)
            {
                return NotFound(resultViewModel);
            }
            return new ObjectResult(resultViewModel);
        }

        /// <summary>
        /// Lists Nonconformity Status Types
        /// </summary>
        [HttpGet]
        [Route("status")]
        public List<EnumViewModel> ListStatus()
        {
            return _nonconformityService.ListStatus();
        }

        /// <summary>
        /// Lists Nonconformity Evaluations Types
        /// </summary>
        [HttpGet]
        [Route("evaluation")]
        public List<EnumViewModel> ListEvaluation()
        {
            return _nonconformityService.ListEvaluation();
        }


    }
}
