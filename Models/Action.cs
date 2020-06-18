using System.ComponentModel.DataAnnotations;

namespace NonconformityControl.Models
{
    public class Action
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public string Description { get; set; }
    }
}