using System.ComponentModel.DataAnnotations;

namespace BackEnd.Model
{
    public class Composant
    {
        [Key] public int ID { get; set; }

        public string Name { get; set; }
    }
}