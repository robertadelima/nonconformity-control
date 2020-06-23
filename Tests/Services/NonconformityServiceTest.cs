using System.Runtime.InteropServices;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NonconformityControl.Api.ViewModels;
using NonconformityControl.Infra.Repositories;
using NonconformityControl.Models;
using NonconformityControl.Services;
using Xunit;
using System;

namespace NonconformityControl.Tests.Services
{
    public class NonconformityServiceTest
    {
        private readonly NonconformityService _nonconformityService;
        private readonly NonconformityRepository _nonconformityRepository;
        private readonly ActionRepository _actionRepository;

        public NonconformityServiceTest()
        {
            var options = new DbContextOptionsBuilder<NonconformityContext>()
            .UseInMemoryDatabase(databaseName: "FakeDb").Options;
            var nonconformityContext = new NonconformityContext(options);
            _nonconformityRepository = new NonconformityRepository(nonconformityContext);
            _actionRepository = new ActionRepository(nonconformityContext);
            _nonconformityService = new NonconformityService(_nonconformityRepository, _actionRepository);
        }

        [Fact]
        public void ShouldAddNonconformity()
        {
            var nonconformityViewModel = new AddNonconformityViewModel();
            nonconformityViewModel.Description = "Controlled materials stored without proper indication.";
            var nonconformityQuantity = _nonconformityRepository.GetAll().Count();
            
            _nonconformityService.AddNonconformity(nonconformityViewModel);
            Assert.Equal(nonconformityQuantity + 1, _nonconformityRepository.GetAll().Count());
        }

        [Fact]
        public void ShouldSetVersionToOneWhenAddingNewNonconformity()
        {
            var nonconformityViewModel = new AddNonconformityViewModel();
            nonconformityViewModel.Description = "Controlled materials stored without proper indication.";
            
            _nonconformityService.AddNonconformity(nonconformityViewModel);
            Assert.Equal(1, _nonconformityRepository.GetAll().OrderByDescending(p => p.Id).FirstOrDefault().Version);
        }

        [Fact]
        public void YearInCodeShouldBeSetToCurrentYearWhenAddingNewNonconformity()
        {
            var nonconformityViewModel = new AddNonconformityViewModel();
            nonconformityViewModel.Description = "Controlled materials stored without proper indication.";
            
            _nonconformityService.AddNonconformity(nonconformityViewModel);
            Assert.Equal(DateTime.UtcNow.Year.ToString(), 
                _nonconformityRepository.GetAll().OrderByDescending(p => p.Id).FirstOrDefault().Code.Split(':', ':')[0]);
        }

        [Fact]
        public void ShouldRemoveNonconformity()
        {
            var nonconformityViewModel = new AddNonconformityViewModel();
            nonconformityViewModel.Description = "Controlled materials stored without proper indication.";
            var nonconformityQuantity = _nonconformityRepository.GetAll().Count();
            _nonconformityService.AddNonconformity(nonconformityViewModel);
            Assert.Equal(nonconformityQuantity + 1, _nonconformityRepository.GetAll().Count());

            var nonconformityId = _nonconformityRepository.GetAll().FirstOrDefault().Id;
            _nonconformityService.RemoveNonconformity(nonconformityId);
            Assert.Equal(nonconformityQuantity, _nonconformityRepository.GetAll().Count());
        }
        
        [Fact]
        public void ShouldAddActionToNonconformity()
        {
            var nonconformityViewModel = new AddNonconformityViewModel();
            nonconformityViewModel.Description = "Controlled materials stored without proper indication.";
            _nonconformityService.AddNonconformity(nonconformityViewModel);

            var nonconformityId = _nonconformityRepository.GetAll().OrderByDescending(p => p.Id).FirstOrDefault().Id;
            var actionViewModel = new AddActionViewModel();
            actionViewModel.Description = "Training team";
            _nonconformityService.AddAction(nonconformityId, actionViewModel);

            Assert.NotEmpty(_nonconformityRepository.GetAll().OrderByDescending(p => p.Id).FirstOrDefault().Actions);
        }

        [Fact]
        public void ShouldSetAsInactiveWhenEvaluteAsEfficient()
        {
            var nonconformityViewModel = new AddNonconformityViewModel();
            nonconformityViewModel.Description = "Controlled materials stored without proper indication.";
            _nonconformityService.AddNonconformity(nonconformityViewModel);
            var nonconformityId = _nonconformityRepository.GetAll().OrderByDescending(p => p.Id).FirstOrDefault().Id;

            _nonconformityService.EvaluateAsEfficient(nonconformityId);

            Assert.Equal(StatusEnum.Inactive, _nonconformityRepository.GetById(nonconformityId).Status);
        }

        [Fact]
        public void ShouldSetAsInactiveWhenEvaluateAsInefficient()
        {
            var nonconformityViewModel = new AddNonconformityViewModel();
            nonconformityViewModel.Description = "Controlled materials stored without proper indication.";
            _nonconformityService.AddNonconformity(nonconformityViewModel);
            var nonconformityId = _nonconformityRepository.GetAll().OrderByDescending(p => p.Id).FirstOrDefault().Id;

            _nonconformityService.EvaluateAsInefficient(nonconformityId);

            Assert.Equal(StatusEnum.Inactive, _nonconformityRepository.GetById(nonconformityId).Status);
        }
        
        [Fact]
        public void ShouldCreateNewNonconformityWhenEvaluateAsInefficient()
        {
            var nonconformityViewModel = new AddNonconformityViewModel();
            nonconformityViewModel.Description = "Controlled materials stored without proper indication.";
            _nonconformityService.AddNonconformity(nonconformityViewModel);
            var nonconformityId = _nonconformityRepository.GetAll().OrderByDescending(p => p.Id).FirstOrDefault().Id;
            var nonconformityQuantity = _nonconformityRepository.GetAll().Count();

            _nonconformityService.EvaluateAsInefficient(nonconformityId);
            Assert.NotEqual(nonconformityQuantity, _nonconformityRepository.GetAll().Count());
        }

        [Fact]
        public void VersionNumberShouldBeIncrementedOnCreatingNewNonconformityWhenEvaluteAsInefficient()
        {
            var nonconformityViewModel = new AddNonconformityViewModel();
            nonconformityViewModel.Description = "Controlled materials stored without proper indication.";
            _nonconformityService.AddNonconformity(nonconformityViewModel);
            var nonconformity = _nonconformityRepository.GetAll().OrderByDescending(p => p.Id).FirstOrDefault();

            _nonconformityService.EvaluateAsInefficient(nonconformity.Id);
            var newNonconformityCreated =  _nonconformityRepository.GetAll().OrderByDescending(p => p.Id).FirstOrDefault();
            Assert.Equal(nonconformity.Version + 1, newNonconformityCreated.Version);
        }

        [Fact]
        public void IdInCodeShouldBeSameAsOriginalOnCreatingNewNonconformityWhenEvaluateAsInefficient()
        {
            var nonconformityViewModel = new AddNonconformityViewModel();
            nonconformityViewModel.Description = "Controlled materials stored without proper indication.";
            _nonconformityService.AddNonconformity(nonconformityViewModel);
            var nonconformity = _nonconformityRepository.GetAll().OrderByDescending(p => p.Id).FirstOrDefault();

            _nonconformityService.EvaluateAsInefficient(nonconformity.Id);
            var newNonconformityCreated =  _nonconformityRepository.GetAll().OrderByDescending(p => p.Id).FirstOrDefault();
            
            Assert.Equal(nonconformity.Code.Split(':', ':')[1], newNonconformityCreated.Code.Split(':', ':')[1]);
        }

    }
}