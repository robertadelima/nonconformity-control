using System.ComponentModel.DataAnnotations;

namespace NonconformityControl.Models
{
    public class Action
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public Action(string description)
        {
            Description = description;
        }
    }
}