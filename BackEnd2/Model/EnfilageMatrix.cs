using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd2.Model
{
    public class EnfilageMatrix
    {
        [Key]  public int ID { get; set; }

        [Key]  public int x { get; set; }

        [Key] public int y { get; set; }

        public Composition value { get; set; }
        
        public Enfilage Enf { get; set; }
        
        public int DentFil { get; set; }

        public int EnfID { get; set; }
        
        
    }
}