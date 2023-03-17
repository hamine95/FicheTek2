using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Composition : INotifyPropertyChanged, ICloneable
    {


        private string _DebutFil;


        private string _Emb;


        private int _Enfilage;


        private int _EnfNbrFil;



         private Composant _GetComposant;


        private Matiere _GetMatiere;

         private int _ID;


        private bool _Intermittent;

        private int _NbrFil;

        private int _Num;

        private int _NumComposant;


        private string _Observation;


        private double _Poids;


        private Produit _ProdID;


        private string _Torsion;




        public int NumComposant
        {
            get => _NumComposant;
            set
            {
                _NumComposant = value;
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



        public int EnfNbrFil
        {
            get => _EnfNbrFil;
            set
            {
                _EnfNbrFil = value;
                NotifyPropertyChanged();
            }
        }

        private bool _Highlight;

        public bool Highlight
        {
            get
            {
                return _Highlight;
            }
            set
            {
                _Highlight = value;
                NotifyPropertyChanged();
            }
        }

        public object Clone()
        {
            return new Composition
            {
                DebutFil = DebutFil,
                Intermittent = Intermittent,
                Emb = Emb,
                Enfilage = Enfilage,
                GetComposant = GetComposant,
                GetMatiere = GetMatiere,
                NbrFil = NbrFil,
                Num = Num,
                NumComposant = NumComposant,
                EnfNbrFil = EnfNbrFil,
                Observation = Observation,
                Poids = Poids,
                Torsion = Torsion,
                ProdID = ProdID,
                ID = ID
            };
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
    }
}