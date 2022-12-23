using System.ComponentModel.DataAnnotations;

namespace BackEnd.Model
{
    public class Machine
    {
        [Key] public int ID { get; set; }

        public int Num { get; set; }

        public string Name { get; set; }

        public ModelMachine Model { get; set; }

        public Duitages Duitages { get; set; }
    }
}