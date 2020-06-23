using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NonconformityControl.Api.ViewModels;
using NonconformityControl.Infra.Repositories;
using NonconformityControl.Models;

namespace NonconformityControl.Services
{
    public class NonconformityService
    {
        private readonly NonconformityRepository _nonconformityRepository;
        private readonly ActionRepository _actionRepository;

        public NonconformityService(NonconformityRepository nonconformityRepository, ActionRepository actionRepository)
        {
            _nonconformityRepository = nonconformityRepository;
            _actionRepository = actionRepository;
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

        public ListNonconformityViewModel GetById(int id)
        {
            var nonconformity = _nonconformityRepository.GetById(id);
            if(nonconformity == null)
            {
                return new ListNonconformityViewModel();
            }
            var nonconformityViewModel = new ListNonconformityViewModel();
            nonconformityViewModel.Id = nonconformity.Id;
            nonconformityViewModel.Code = nonconformity.Code;
            nonconformityViewModel.Description = nonconformity.Description;
            nonconformityViewModel.Status = nonconformity.Status;
            nonconformityViewModel.Actions = nonconformity.Actions.Select(p => new ActionViewModel
            {
                Id = p.Id,
                Description = p.Description
            }).ToList();
            return nonconformityViewModel;
        }

        public ResultViewModel AddNonconformity(AddNonconformityViewModel request)
        {
            Nonconformity nonconformity = new Nonconformity(request.Description);
            nonconformity = _nonconformityRepository.Add(nonconformity);
            var resultViewModel = new ResultViewModel(true, nonconformity.Id, "Nonconformity successfully saved!");
            return resultViewModel;
        }

        public ResultViewModel RemoveNonconformity(int id)
        {
            var nonconformity = _nonconformityRepository.GetById(id);
            if(nonconformity == null)
            {
                return new ResultViewModel(false, nonconformity.Id, "Nonconformity does not exist!");
            }
            _nonconformityRepository.Remove(nonconformity);
            return new ResultViewModel(true, nonconformity.Id, "Nonconformity successfully removed!");
        }

        public ResultViewModel AddAction(int id, AddActionViewModel request)
        {
            var nonconformity = _nonconformityRepository.GetById(id);
            if(nonconformity == null)
            {
                return new ResultViewModel(false, nonconformity.Id, "Nonconformity does not exist!");
            }
            Action action = new Action(id, request.Description);
            _nonconformityRepository.AddActionToNonconformity(id, action);
            _actionRepository.Add(action);
            return new ResultViewModel(true, nonconformity.Id, "Action successfully added!");
        }

    }
}