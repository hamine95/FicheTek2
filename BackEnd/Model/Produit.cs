using System;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Model
{
    public class Produit
    {
        [Key] public int ID { get; set; }

        public int NumArticle { get; set; }

        public int Version { get; set; }

        public DateTime DateCreation { get; set; }

        public DateTime MiseAJour { get; set; }

        public string Concepteur { get; set; }

        public DateTime Redaction { get; set; }

        public string Verificateur { get; set; }

        public Duitages IDMachine { get; set; }

        public string Duitage { get; set; }

        public int Dent { get; set; }

        public double Peigne { get; set; }

        public string Ref { get; set; }

        public string Name { get; set; }

        public int Largeur { get; set; }

        public int Epaisseur { get; set; }

        public string Client { get; set; }
    }
}