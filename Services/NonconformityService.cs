using System.Collections.Generic;
using System.Linq;
using NonconformityControl.Api.ViewModels;
using NonconformityControl.Infra.Repositories;

namespace NonconformityControl.Services
{
    public class NonconformityService
    {
        private readonly NonconformityRepository _nonconformityRepository;

        public NonconformityService(NonconformityRepository nonconformityRepository)
        {
            _nonconformityRepository = nonconformityRepository;
        }

        public IEnumerable<ListNonconformityViewModel> ListNonconformities()
        {
            return _nonconformityRepository.GetAll().Select(p => new ListNonconformityViewModel
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

    }
}