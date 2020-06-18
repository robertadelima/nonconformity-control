using System;
using System.ComponentModel.DataAnnotations;

namespace NonconformityControl.Models
{
    public class Nonconformity
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; private set; }     //{Ano}:{Identificador}:{Revisão}
        public string Name { get; set; }
        public string Description { get; private set; }  
        public Action[] Actions { get; private set; }
        public bool Efficient { get; private set; }
        public bool Active { get; set; }

        public Nonconformity(string name, string description, bool efficient)
        {
            Code = String.Concat(DateTime.UtcNow.Year.ToString(), ":", Id, ":"); //ver como será feita lógica de numero no ano
            Name = name;
            Description = description;
            Efficient = efficient;
            Active = true;
        }

        public void Validar() 
        {

        }
    }
}