using System.ComponentModel.DataAnnotations;

namespace BackEnd.Model
{
    public class Composition
    {
        [Key] public int ID { get; set; }

        [Key] public Composant GetComposant { get; set; }

        public int Num { get; set; }

        public Matiere GetMatiere { get; set; }

        public int NbrFil { get; set; }

        public string Torsion { get; set; }

        public int Enfilage { get; set; }

        public string Emb { get; set; }

        public double Poids { get; set; }

        public string Observation { get; set; }
    }
}