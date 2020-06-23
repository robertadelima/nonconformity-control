using FluentValidation;

namespace NonconformityControl.Models.ModelValidators
{
    public class NonconformityValidator : AbstractValidator<Nonconformity>
    {
        public NonconformityValidator()
        {
            RuleFor(p => p.Description).MaximumLength(1024);
        }
    }
}