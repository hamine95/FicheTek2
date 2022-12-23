using System.ComponentModel.DataAnnotations;

namespace BackEnd.Model
{
    public class FicheTechnique
    {
        [Key] public int ID { get; set; }

        public Produit IDProduit { get; set; }

        public Composition GetComposition { get; set; }

        public Enfilage EnfilageID { get; set; }
    }
}