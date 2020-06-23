using System;
using System.Collections.Generic;

namespace NonconformityControl.Models
{
    public class Nonconformity
    {
        public int Id { get; private set; }
        public int Version { get; private set; }
        public string Code { get; private set; } 
        public string Description { get; private set; }  
        public virtual List<Action> Actions { get; private set; }
        public StatusEnum Status { get; private set; }
        public EvaluationEnum Evaluation { get; private set; }

        public Nonconformity(string description, int version = 1)
        {
            Version = version;
            Description = description;
            Evaluation = EvaluationEnum.New;
            Status = StatusEnum.Active;
        }

        public void UpdateCode(string code)
        {
            this.Code = code;
        }

        public void setAsEfficient()
        {
            this.Evaluation = EvaluationEnum.Efficient;
            setAsInactive();
        }

        private void setAsInactive()
        {
            this.Status = StatusEnum.Inactive;
        }

        internal void setAsInefficient()
        {
            this.Evaluation = EvaluationEnum.Inefficient;
            setAsInactive();
        }

        public void Validar() 
        {
            
        }
    }
}