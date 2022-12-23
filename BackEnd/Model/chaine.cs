using System.ComponentModel.DataAnnotations;

namespace BackEnd.Model
{
    public class chaine
    {
        [Key] public int ID { get; set; }

        [Key] public int x { get; set; }

        [Key] public int y { get; set; }

        public string Nom { get; set; }

        public int Colonne { get; set; }

        public int Ligne { get; set; }


        public string value { get; set; }
    }
}