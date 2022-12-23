using System.ComponentModel.DataAnnotations;

namespace BackEnd.Model
{
    public class Couleur
    {
        [Key] public int Nbr { get; set; }

        public string Name { get; set; }
    }
}