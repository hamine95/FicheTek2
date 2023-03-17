using System.Collections.Generic;


namespace BackEnd2.Model
{
    public class Machine
    {
        public int ID { get; set; }

        public int Num { get; set; }

        public int DoubleDuitage { get; set; }
        public string Name { get; set; }

        public ModelMachine Model { get; set; }

        public List<DuitageGomme> DuitageGommes { get; set; }
        public List<Duitages> GetDuitages { get; set; }
    }
}