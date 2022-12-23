using System.ComponentModel.DataAnnotations;

namespace BackEnd2.Model
{
    public class Duitages
    {
        [Key] public int ID { get; set; }

        public Machine Machine { get; set; }

       
        public double Duitage { get; set; }
        

        public double Vitesse { get; set; }
    }
}