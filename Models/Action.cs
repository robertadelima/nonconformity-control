using FluentValidation.Results;
using NonconformityControl.Models.ModelValidators;

namespace NonconformityControl.Models
{
    public class Action
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public Nonconformity Nonconformity { get; private set; } 
        public int NonconformityId { get; private set; }

        public Action(int nonconformityId, string description)
        {
            NonconformityId = nonconformityId;
            Description = description;
        }

        public bool isValid() 
        {
            var validator = new ActionValidator();
            ValidationResult results = validator.Validate(this);
            return results.IsValid;
        } 
    }
}