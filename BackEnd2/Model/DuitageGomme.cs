using System.ComponentModel.DataAnnotations;

namespace BackEnd2.Model
{
    public class DuitageGomme
    {
        [Key] public int ID { get; set; }

        public Machine Machine { get; set; }

       
        public string Duitage { get; set; }
        

        public double Vitesse { get; set; }
    }
}