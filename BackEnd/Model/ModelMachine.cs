using System.ComponentModel.DataAnnotations;

namespace BackEnd.Model
{
    public class ModelMachine
    {
        [Key] public int ID { get; set; }

        public string Name { get; set; }

        public int NbrBande { get; set; }
    }
}