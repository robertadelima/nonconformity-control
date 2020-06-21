using FluentValidation;
using NonconformityControl.Api.ViewModels;

namespace NonconformityControl.Api.ViewModelsValidators
{
    public class AddNonconformityViewModelValidator : AbstractValidator<AddNonconformityViewModel>
    {
        public AddNonconformityViewModelValidator()
        {
            RuleFor(p => p.Description).MaximumLength(1024);
        }
    }
}