using FluentValidation;
using NonconformityControl.Api.ViewModels;

namespace NonconformityControl.Api.ViewModelsValidators
{
    public class AddActionViewModelValidator : AbstractValidator<AddActionViewModel>
    {
        public AddActionViewModelValidator()
        {
            RuleFor(p => p.Description).NotEmpty().MaximumLength(1024);
        }
    }
}