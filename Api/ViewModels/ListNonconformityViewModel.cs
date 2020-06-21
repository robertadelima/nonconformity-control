using System.Collections.Generic;
using NonconformityControl.Models;

namespace NonconformityControl.Api.ViewModels
{
    public class ListNonconformityViewModel
    {
        
        public int Id { get; set; }
        public string Code { get; set; }  
        public string Description { get; set; }  
        public List<ActionViewModel> Actions { get; set; }
        public StatusEnum Status { get; set; }
    }
}