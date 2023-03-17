namespace BackEnd2.Model
{
    public class ModelFiche
    {
        public enum ModelFicheTek
        {
            FicheTekNormal,
            FicheTekEHC,
            FicheTekCrochetage
        }

        public bool IsEchantillon;


        public ModelFicheTek model;
    }
}