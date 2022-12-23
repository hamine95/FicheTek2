using System.ComponentModel.DataAnnotations;

namespace BackEnd.Model
{
    public class EnfilageMatrix
    {
        [Key] public int ID { get; set; }


        [Key] public int x { get; set; }

        [Key] public int y { get; set; }

        public Composant value { get; set; }

        public int Column { get; set; }

        public int Row { get; set; }
    }
}