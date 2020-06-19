using System;
using System.Collections.Generic;

namespace NonconformityControl.Api.ViewModels
{
    public class NonconformityViewModel
    {
        public int Id { get; set; }
        public string Code { get; private set; }  
        public string Description { get; private set; }  
        public List<Action> Actions { get; private set; }
        public bool Efficient { get; private set; }
        public bool Active { get; set; }
    }
}