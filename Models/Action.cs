using System.ComponentModel.DataAnnotations;

namespace NonconformityControl.Models
{
    public class Action
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Nonconformity Nonconformity { get; set; } //ifremoved
        public int NonconformityId { get; set; }

        public Action(int nonconformityId, string description)
        {
            NonconformityId = nonconformityId;
            Description = description;
        }
    }
}