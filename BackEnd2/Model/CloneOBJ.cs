using System;

namespace BackEnd2.Model
{
    public class CloneOBJ: ICloneable
    {
        
        
       private string _BKBorderComposant;

        private string _BKComposant;

        private string _DebutFil;

        private string _FGComposant;

        private bool _Intermittent;


        private string _ImagePath;
        private string _ImageReedPath;
        
        private int _NumComposant;


        public string BKBorderComposant
        {
            get => _BKBorderComposant;
            set
            {
                _BKBorderComposant = value;
            }
        }

        public string BKComposant
        {
            get => _BKComposant;
            set
            {
                _BKComposant = value;
            }
        }

        public int NumComposant
        {
            get => _NumComposant;
            set
            {
                _NumComposant = value;
            }
        }

        public string FGComposant
        {
            get => _FGComposant;
            set
            {
                _FGComposant = value;
            }
        }

      

        public string DebutFil
        {
            get => _DebutFil;
            set
            {
                _DebutFil = value;
            }
        }

        public bool Intermittent
        {
            get => _Intermittent;
            set
            {
                _Intermittent = value;
            }
        }

     

        
        
        private string _Emb;


        private int _Enfilage;


        private Composant _GetComposant;


        private Matiere _GetMatiere;

        private int _ID;

        private int _NbrFil;


        private int _EnfNbrFil;
        
        private int _Num;


        private string _Observation;


        private double _Poids;


        private Produit _ProdID;


        private string _Torsion;

        public int ID
        {
            get => _ID;
            set
            {
                _ID = value;
            }
        }

        public Composant GetComposant
        {
            get => _GetComposant;
            set
            {
                _GetComposant = value;
            }
        }

        public int Num
        {
            get => _Num;
            set
            {
                _Num = value;
            }
        }

        public Matiere GetMatiere
        {
            get => _GetMatiere;
            set
            {
                _GetMatiere = value;
            }
        }

        public int NbrFil
        {
            get => _NbrFil;
            set
            {
                _NbrFil = value;
            }
        }

        public string Torsion
        {
            get => _Torsion;
            set
            {
                _Torsion = value;
            }
        }

        public int Enfilage
        {
            get => _Enfilage;
            set
            {
                _Enfilage = value;
            }
        }

        public string Emb
        {
            get => _Emb;
            set
            {
                _Emb = value;
            }
        }

        public double Poids
        {
            get => _Poids;
            set
            {
                _Poids = value;
            }
        }

        public string Observation
        {
            get => _Observation;
            set
            {
                _Observation = value;
            }
        }

        public Produit ProdID
        {
            get => _ProdID;
            set
            {
                _ProdID = value;
            }
        }

        public string ImagePath
        {
            get => _ImagePath;
            set
            {
                _ImagePath = value;
            }
        }

        public string ImageReedPath
        {
            get => _ImageReedPath;
            set
            {
                _ImageReedPath = value;
            }
        }
        public int EnfNbrFil
        {
            get => _EnfNbrFil;
            set
            {
                _EnfNbrFil = value;
            }
        }

        
        public object Clone()
        {
            return new CloneOBJ
            {
                BKBorderComposant = this.BKBorderComposant,
                BKComposant = this.BKComposant,
                DebutFil=this.DebutFil,
                FGComposant=this.FGComposant,
                Intermittent=this.Intermittent,
                ImagePath=this.ImagePath,
                ImageReedPath=this.ImageReedPath,
                Emb = this.Emb,
                Enfilage = this.Enfilage,
                GetComposant = this.GetComposant,
                GetMatiere = this.GetMatiere,
                NbrFil = this.NbrFil,
                Num = this.Num,
                EnfNbrFil = this.EnfNbrFil,
                Observation = this.Observation,
                Poids = this.Poids,
                Torsion = this.Torsion,
                ProdID = this.ProdID,
                ID = this.ID,
                



            }; 
        }
    }
}