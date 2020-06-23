using System.Linq;
using Microsoft.EntityFrameworkCore;
using NonconformityControl.Api.ViewModels;
using NonconformityControl.Infra.Repositories;
using NonconformityControl.Models;
using NonconformityControl.Services;
using Xunit;

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
            
            _nonconformityService.AddNonconformity(nonconformityViewModel);
            Assert.Equal(1, _nonconformityRepository.GetAll().Count());
        }

        [Fact]
        public void ShouldRemoveNonconformity()
        {
            var nonconformityId = _nonconformityRepository.GetAll().FirstOrDefault().Id;
            _nonconformityService.RemoveNonconformity(nonconformityId);
            Assert.Equal(0, _nonconformityRepository.GetAll().Count());
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

            Assert.Equal(1, _nonconformityRepository.GetAll().OrderByDescending(p => p.Id).FirstOrDefault().Actions.Count());
        }

    }
}