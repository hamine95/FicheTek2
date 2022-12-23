using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Composition : INotifyPropertyChanged, ICloneable
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
                NotifyPropertyChanged();
            }
        }

        public string BKComposant
        {
            get => _BKComposant;
            set
            {
                _BKComposant = value;
                NotifyPropertyChanged();
            }
        }

        public int NumComposant
        {
            get => _NumComposant;
            set
            {
                _NumComposant = value;
                NotifyPropertyChanged();
            }
        }

        public string FGComposant
        {
            get => _FGComposant;
            set
            {
                _FGComposant = value;
                NotifyPropertyChanged();
            }
        }

      

        public string DebutFil
        {
            get => _DebutFil;
            set
            {
                _DebutFil = value;
                NotifyPropertyChanged();
            }
        }

        public bool Intermittent
        {
            get => _Intermittent;
            set
            {
                _Intermittent = value;
                NotifyPropertyChanged();
            }
        }

     

        
        
        private string _Emb;


        private int _Enfilage;


        [Key] private Composant _GetComposant;


        private Matiere _GetMatiere;

        [Key] private int _ID;

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
                NotifyPropertyChanged();
            }
        }

        public Composant GetComposant
        {
            get => _GetComposant;
            set
            {
                _GetComposant = value;
                NotifyPropertyChanged();
            }
        }

        public int Num
        {
            get => _Num;
            set
            {
                _Num = value;
                NotifyPropertyChanged();
            }
        }

        public Matiere GetMatiere
        {
            get => _GetMatiere;
            set
            {
                _GetMatiere = value;
                NotifyPropertyChanged();
                if (GetMatiere != null) GetMatiere.PropertyChanged += Child_PropertyChanged;
            }
        }

        public int NbrFil
        {
            get => _NbrFil;
            set
            {
                _NbrFil = value;
                NotifyPropertyChanged();
            }
        }

        public string Torsion
        {
            get => _Torsion;
            set
            {
                _Torsion = value;
                NotifyPropertyChanged();
            }
        }

        public int Enfilage
        {
            get => _Enfilage;
            set
            {
                _Enfilage = value;
                NotifyPropertyChanged();
            }
        }

        public string Emb
        {
            get => _Emb;
            set
            {
                _Emb = value;
                NotifyPropertyChanged();
            }
        }

        public double Poids
        {
            get => _Poids;
            set
            {
                _Poids = value;
                NotifyPropertyChanged();
            }
        }

        public string Observation
        {
            get => _Observation;
            set
            {
                _Observation = value;
                NotifyPropertyChanged();
            }
        }

        public Produit ProdID
        {
            get => _ProdID;
            set
            {
                _ProdID = value;
                NotifyPropertyChanged();
            }
        }

        public string ImagePath
        {
            get => _ImagePath;
            set
            {
                _ImagePath = value;
                NotifyPropertyChanged();
            }
        }

        public string ImageReedPath
        {
            get => _ImageReedPath;
            set
            {
                _ImageReedPath = value;
                NotifyPropertyChanged();
            }
        }
        public int EnfNbrFil
        {
            get => _EnfNbrFil;
            set
            {
                _EnfNbrFil = value;
                NotifyPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            NotifyPropertyChanged(e.PropertyName);
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public object Clone()
        {
            return new Composition()
            {
                BKBorderComposant = this.BKBorderComposant,
                BKComposant = this.BKComposant,
                DebutFil = this.DebutFil,
                FGComposant = this.FGComposant,
                Intermittent = this.Intermittent,
                ImagePath = this.ImagePath,
                ImageReedPath = this.ImageReedPath,
                Emb = this.Emb,
                Enfilage = this.Enfilage,
                GetComposant = this.GetComposant,
                GetMatiere = this.GetMatiere,
                NbrFil = this.NbrFil,
                Num = this.Num,
                NumComposant = this.NumComposant,
                EnfNbrFil = this.EnfNbrFil,
                Observation = this.Observation,
                Poids = this.Poids,
                Torsion = this.Torsion, 
              
                ProdID =this.ProdID,
                ID = this.ID,
                



            }; 
        }
    }
}