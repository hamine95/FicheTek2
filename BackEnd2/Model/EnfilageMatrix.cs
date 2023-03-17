

namespace BackEnd2.Model
{
    public class EnfilageMatrix
    {
         public int ID { get; set; }

         public int x { get; set; }

        public int y { get; set; }

        public Composition value { get; set; }

        public Enfilage Enf { get; set; }

        public int DentFil { get; set; }

        public int EnfID { get; set; }
    }
}