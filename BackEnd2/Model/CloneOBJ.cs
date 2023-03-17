using System;

namespace BackEnd2.Model
{
    public class CloneOBJ : ICloneable
    {
        public string BKBorderComposant { get; set; }

        public string BKComposant { get; set; }

        public int NumComposant { get; set; }

        public string FGComposant { get; set; }


        public string DebutFil { get; set; }

        public bool Intermittent { get; set; }

        public int ID { get; set; }

        public Composant GetComposant { get; set; }

        public int Num { get; set; }

        public Matiere GetMatiere { get; set; }

        public int NbrFil { get; set; }

        public string Torsion { get; set; }

        public int Enfilage { get; set; }

        public string Emb { get; set; }

        public double Poids { get; set; }

        public string Observation { get; set; }

        public Produit ProdID { get; set; }

        public string ImagePath { get; set; }

        public string ImageReedPath { get; set; }

        public int EnfNbrFil { get; set; }


        public object Clone()
        {
            return new CloneOBJ
            {
                BKBorderComposant = BKBorderComposant,
                BKComposant = BKComposant,
                DebutFil = DebutFil,
                FGComposant = FGComposant,
                Intermittent = Intermittent,
                ImagePath = ImagePath,
                ImageReedPath = ImageReedPath,
                Emb = Emb,
                Enfilage = Enfilage,
                GetComposant = GetComposant,
                GetMatiere = GetMatiere,
                NbrFil = NbrFil,
                Num = Num,
                EnfNbrFil = EnfNbrFil,
                Observation = Observation,
                Poids = Poids,
                Torsion = Torsion,
                ProdID = ProdID,
                ID = ID
            };
        }
    }
}