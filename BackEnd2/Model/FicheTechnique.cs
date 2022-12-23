using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd2.Model
{
    public class FicheTechnique
    {
        [Key] public int ID { get; set; }

        public int Ordre { get; set; }
        
        public int ModelFiche { get; set; }

        public virtual IList<Produit> Produits { get; set; }

        public Catalogue Catalog { get; set; }

        public bool IsArchive { get; set; }
    }
}