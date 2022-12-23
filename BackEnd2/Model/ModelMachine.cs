using System.ComponentModel.DataAnnotations;

namespace BackEnd2.Model
{
    public class ModelMachine
    {
        public enum Method
        {
            Tissage=0,
            Crochetage=1,
            Tressage=2,
        }
        public Method method { get; set; }
        [Key] public int ID { get; set; }

        public string Name { get; set; }

        public string NomModel { get; set; }

        public int NbrBande { get; set; }

        public int MaxWidth { get; set; }
    }
}