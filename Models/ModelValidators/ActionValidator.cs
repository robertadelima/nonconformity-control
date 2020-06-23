using FluentValidation;

namespace NonconformityControl.Models.ModelValidators
{
    public class ActionValidator : AbstractValidator<Action>
    {
        public ActionValidator()
        {
            RuleFor(p => p.Description).NotEmpty().MaximumLength(1024);
        }
    }
}