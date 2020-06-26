using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
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
                return null;
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
            ValidationResult validationResult = nonconformity.isValid();
            if(!validationResult.IsValid)
            {
                 return new ResultViewModel(false, nonconformity.Id, validationResult.ToString());
            }

            nonconformity = _nonconformityRepository.Add(nonconformity);
            nonconformity = UpdateCodeWhenAdd(nonconformity);

            var resultViewModel = new ResultViewModel(true, nonconformity.Id, "Nonconformity successfully saved!");
            return resultViewModel;
        }

        private Nonconformity UpdateCodeWhenAdd(Nonconformity nonconformity)
        {
            var code = string.Concat((System.DateTime.UtcNow.Year) + ":" + nonconformity.Id.ToString("D2") + ":" 
                + nonconformity.Version.ToString("D2")); 
            nonconformity.UpdateCode(code);
            _nonconformityRepository.Update(nonconformity);
            
            return nonconformity;
        }

        public ResultViewModel RemoveNonconformity(int id)
        {
            var nonconformity = _nonconformityRepository.GetById(id);
            if(nonconformity == null)
            {
                return new ResultViewModel(false, id, "Nonconformity does not exist!");
            }
            _nonconformityRepository.Remove(nonconformity);
            return new ResultViewModel(true, nonconformity.Id, "Nonconformity successfully removed!");
        }

        public ResultViewModel AddAction(int id, AddActionViewModel request)
        {
            var nonconformity = _nonconformityRepository.GetById(id);
            if(nonconformity == null)
            {
                return new ResultViewModel(false, id, "Nonconformity does not exist!");
            }
            Action action = new Action(id, request.Description);
            ValidationResult validationResult = action.isValid();
            if(!validationResult.IsValid)
            {
                 return new ResultViewModel(false, action.Id, validationResult.ToString());
            }
            _nonconformityRepository.AddActionToNonconformity(id, action);
            _actionRepository.Add(action);
            return new ResultViewModel(true, nonconformity.Id, "Action successfully added!");
        }

        public ResultViewModel RemoveAction(int id, int actionId)
        {
            var nonconformity = _nonconformityRepository.GetById(id);
            if(nonconformity == null)
            {
                return new ResultViewModel(false, id, "Nonconformity does not exist!");
            }
            var action = nonconformity?.Actions?.FirstOrDefault(p => p.Id == actionId);
            if(action == null)
            {
                return new ResultViewModel(false, actionId, "Nonconformity does not contain this action!");
            }
            _actionRepository.Remove(action);
            return new ResultViewModel(true, nonconformity.Id, "Action successfully removed!");
        }

        public ResultViewModel EvaluateAsEfficient(int id)
        {
            var nonconformity = _nonconformityRepository.GetById(id);
            if(nonconformity == null)
            {
                return new ResultViewModel(false, id, "Nonconformity does not exist!");
            }
            if(nonconformity.Status == StatusEnum.Inactive)
            {
                return new ResultViewModel(false, nonconformity.Id, "Can't evaluate inactive nonconformity!");
            }
            nonconformity.setAsEfficient();
            _nonconformityRepository.Update(nonconformity);
            return new ResultViewModel(true, nonconformity.Id, "Nonconformity successfully set as efficient!");
        }

        public ResultViewModel EvaluateAsInefficient(int id)
        {
            var nonconformity = _nonconformityRepository.GetById(id);
            if(nonconformity == null)
            {
                return new ResultViewModel(false, id, "Nonconformity does not exist!");
            }
            if(nonconformity.Status == StatusEnum.Inactive)
            {
                return new ResultViewModel(false, nonconformity.Id, "Can't evaluate inactive nonconformity!");
            }
            _nonconformityRepository.UpdateAsInefficient(id);

            var newNonconformity = new Nonconformity(nonconformity.Description, nonconformity.Version + 1);
            _nonconformityRepository.AddNewVersion(newNonconformity, nonconformity.Id);
            return new ResultViewModel(true, nonconformity.Id, 
                "Nonconformity successfully set as inefficient and new version of nonconformity created!");
        }

        public List<EnumViewModel> ListStatus()
        {
            return System.Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>().Select(p => new EnumViewModel {
                Id = ((int)p),
                Description = p.ToString()
            }).ToList();
        }

        public List<EnumViewModel> ListEvaluation()
        {
            return System.Enum.GetValues(typeof(EvaluationEnum)).Cast<EvaluationEnum>().Select(p => new EnumViewModel {
                Id = ((int)p),
                Description = p.ToString()
            }).ToList();
        }

    }
}