using System.ComponentModel.DataAnnotations;

namespace BackEnd.Model
{
    public class Duitages
    {
        [Key] public int IDMachine { get; set; }

        [Key] public string Duitage { get; set; }

        public double Vitesse { get; set; }
    }
}