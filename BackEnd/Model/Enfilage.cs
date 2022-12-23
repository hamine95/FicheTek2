using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Model
{
    public class Enfilage
    {
        [Key] public int ID { get; set; }

        public chaine GetChaine { get; set; }

        public List<EnfilageMatrix> GetMatrix { get; set; }
    }
}