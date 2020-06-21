using System;
using System.Collections.Generic;

namespace NonconformityControl.Models
{
    public class Nonconformity
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public string Code { get; private set; } //{Ano}:{Identificador}:{Revisão}
        public string Description { get;  set; }  
        public virtual List<Action> Actions { get; private set; }
        public StatusEnum Status { get; private set; }
        public EvaluationEnum Evaluation { get; set; }

        public Nonconformity(string description, int version = 1)
        {
            Version = version;
            Description = description;
            Evaluation = EvaluationEnum.New;
            Status = StatusEnum.Active;
        }

        public void Validar() 
        {

        }
    }
}