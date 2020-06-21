using System;
using System.Collections.Generic;

namespace NonconformityControl.Models
{
    public class Nonconformity
    {
        public int Id { get; set; }
        public string Code { get; private set; }     //{Ano}:{Identificador}:{Revisão}
        public string Description { get;  set; }  
        public virtual List<Action> Actions { get; private set; }
        public StatusEnum Status { get; private set; }
        public EvaluationEnum Evaluation { get; set; }

        public Nonconformity(string description)
        {
            Code = String.Concat(DateTime.UtcNow.Year.ToString(), ":", Id, ":", "00"); //ver como será feita lógica de numero no ano
            Description = description;
            Evaluation = EvaluationEnum.New;
            Status = StatusEnum.Active;
        }

        public void Validar() 
        {

        }
    }
}