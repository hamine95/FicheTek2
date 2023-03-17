using System.Collections.Generic;


namespace BackEnd2.Model
{
    public class FicheTechnique
    {
         public int ID { get; set; }

        public int Ordre { get; set; }

        public int ModelFiche { get; set; }

        public  List<Produit> Produits { get; set; }

        public Catalogue Catalog { get; set; }
        
        public int CatalogID { get; set; }

        public bool IsArchive { get; set; }
    }
}