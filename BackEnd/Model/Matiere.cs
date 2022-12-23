using System.ComponentModel.DataAnnotations;

namespace BackEnd.Model
{
    public class Matiere
    {
        [Key] public int ID { get; set; }

        public string Ref { get; set; }

        public string Designation { get; set; }

        public string Titrage { get; set; }

        public string TypeMatiere { get; set; }

        public Couleur GetCouleur { get; set; }
    }
}