using System.ComponentModel.DataAnnotations;

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
    }
}