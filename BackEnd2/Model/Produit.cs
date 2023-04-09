using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Produit : INotifyPropertyChanged, ICloneable
    {
        private Client _Client;
        private Concepteur _Concepteur;
        private DateTime? _DateCreation;
        private int _Dent;
        private DuitageGomme _DuitageGomme;
        private Duitages _DuitageId;

        private int _EnfDent;
        private Enfilage _EnfilageId;
        private int _Epaisseur;
        private FicheTechnique _FicheTekId;
        private List<Composition> _GetComposition;


        private string _image;

        private int _IsEnfilage;
        private int _Largeur;
        private DateTime? _MiseAJour;
        private string _Name;
        private string _Name2;
        private int _NumArticle;
        private int _Peigne;
        private Reed _PeigneObj;
        private int _Redacteur;
        private Redacteur _RedacteurObj;
        private DateTime? _Redaction;
        private string _Ref;
        private Verificateur _Verificateur;
        private int _Version;



        private ModelFiche.ModelFicheTek _modelFiche;

        public ModelFiche.ModelFicheTek modelFiche
        {
            get { return _modelFiche; }
            set { _modelFiche = value; }
        }


        private int FicheID;
        private int ID;


        public int Id
        {
            get => ID;
            set
            {
                ID = value;
                NotifyPropertyChanged();
            }
        }

        public int Version
        {
            get => _Version;
            set
            {
                _Version = value;
                NotifyPropertyChanged();
            }
        }

        public int NumArticle
        {
            get => _NumArticle;
            set
            {
                _NumArticle = value;
                NotifyPropertyChanged();
            }
        }

        public int EnfDent
        {
            get => _EnfDent;
            set
            {
                _EnfDent = value;
                NotifyPropertyChanged();
            }
        }

        //[ForeignKey("FicheTechnique")]
        public int FicheId
        {
            get => FicheID;
            set
            {
                FicheID = value;
                NotifyPropertyChanged();
            }
        }

        public virtual FicheTechnique FicheTekID
        {
            get => _FicheTekId;
            set
            {
                _FicheTekId = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime? DateCreation
        {
            get => _DateCreation;
            set
            {
                _DateCreation = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime? MiseAJour
        {
            get => _MiseAJour;
            set
            {
                _MiseAJour = value;
                NotifyPropertyChanged();
            }
        }

        public Concepteur Concepteur
        {
            get => _Concepteur;
            set
            {
                _Concepteur = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime? Redaction
        {
            get => _Redaction;
            set
            {
                _Redaction = value;
                NotifyPropertyChanged();
            }
        }

        public Redacteur RedacteurObj
        {
            get => _RedacteurObj;
            set
            {
                _RedacteurObj = value;
                NotifyPropertyChanged();
            }
        }

        public int Redacteur
        {
            get => _Redacteur;
            set
            {
                _Redacteur = value;
                NotifyPropertyChanged();
            }
        }

        public Verificateur Verificateur
        {
            get => _Verificateur;
            set
            {
                _Verificateur = value;
                NotifyPropertyChanged();
            }
        }

        public Duitages DuitageID
        {
            get => _DuitageId;
            set
            {
                _DuitageId = value;
                NotifyPropertyChanged();
            }
        }


        public int Dent
        {
            get => _Dent;
            set
            {
                _Dent = value;
                NotifyPropertyChanged();
            }
        }

        public Reed PeigneObj
        {
            get => _PeigneObj;
            set
            {
                _PeigneObj = value;
                NotifyPropertyChanged();
            }
        }

        public int Peigne
        {
            get => _Peigne;
            set
            {
                _Peigne = value;
                NotifyPropertyChanged();
            }
        }

        public string Ref
        {
            get => _Ref;
            set
            {
                _Ref = value;
                NotifyPropertyChanged();
            }
        }

        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                NotifyPropertyChanged();
            }
        }

        public int Largeur
        {
            get => _Largeur;
            set
            {
                _Largeur = value;
                NotifyPropertyChanged();
            }
        }

        public int Epaisseur
        {
            get => _Epaisseur;
            set
            {
                _Epaisseur = value;
                NotifyPropertyChanged();
            }
        }

        public Client Client
        {
            get => _Client;
            set
            {
                _Client = value;
                NotifyPropertyChanged();
            }
        }

        public Enfilage EnfilageID
        {
            get => _EnfilageId;
            set
            {
                _EnfilageId = value;
                NotifyPropertyChanged();
            }
        }

        public List<Composition> GetComposition
        {
            get => _GetComposition;
            set
            {
                _GetComposition = value;
                NotifyPropertyChanged();
            }
        }

        public string Name2
        {
            get => _Name2;
            set
            {
                _Name2 = value;
                NotifyPropertyChanged();
            }
        }

        public DuitageGomme DuitageGomme
        {
            get => _DuitageGomme;
            set
            {
                _DuitageGomme = value;
                NotifyPropertyChanged()
                    ;
            }
        }

        public int IsEnfilage
        {
            get => _IsEnfilage;
            set
            {
                _IsEnfilage = value;
                NotifyPropertyChanged();
            }
        }

        public int Definite { get; set; }

        public string image
        {
            get => _image;
            set
            {
                _image = value;
                NotifyPropertyChanged();
            }
        }

        public object Clone()
        {
            return new Produit
            {
                ID = ID,
                image = image,
                Version = Version,
                NumArticle = NumArticle,
                FicheTekID = FicheTekID,
                DateCreation = DateCreation,
                MiseAJour = MiseAJour,
                Concepteur = Concepteur,
                Redaction = Redaction,
                EnfDent = EnfDent,
                Verificateur = Verificateur,
                DuitageID = DuitageID,
                DuitageGomme = DuitageGomme,
                Dent = Dent,
                Peigne = Peigne,
                Ref = Ref,
                Name = Name,
                Name2 = Name2,
                Largeur = Largeur,
                Epaisseur = Epaisseur,
                Client = Client,
                EnfilageID = EnfilageID,
                GetComposition = GetComposition,
                IsEnfilage = IsEnfilage,
                FicheId = FicheId,
                Definite = Definite,
                RedacteurObj = RedacteurObj,
                Redacteur = Redacteur,
                PeigneObj = PeigneObj
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