using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Produit: INotifyPropertyChanged,ICloneable
    {
         private int ID;
         private int _Version;
         private int _NumArticle;
         private FicheTechnique _FicheTekId;
         private DateTime? _DateCreation;
         private DateTime? _MiseAJour;
         private Concepteur _Concepteur;
         private DateTime? _Redaction;
         private Verificateur _Verificateur;
         private int _Redacteur;
         private Redacteur _RedacteurObj;
         private Duitages _DuitageId;
         private DuitageGomme _DuitageGomme;
         private int _Dent;
         private Reed _PeigneObj;
         private int _Peigne;
         private string _Ref;
         private string _Name;
         private string _Name2;
         private int _Largeur;
         private int _Epaisseur;
         private Client _Client;
         private Enfilage _EnfilageId;
         private List<Composition> _GetComposition;
         private int _definite;

   
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

        private int _EnfDent;

        public int EnfDent
        {
            get
            {
                return _EnfDent;
            }
            set
            {
                _EnfDent = value;
                NotifyPropertyChanged();
            }
        }


        private int FicheID;
        
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

        private int _IsEnfilage;
        
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

        public int Definite
        {
            get => _definite;
            set => _definite = value;
        }


        private string _image;

        public string image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
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
            
            return new Produit()
            {
                ID=this.ID,
                image = this.image,
            Version=this.Version,
           NumArticle=this.NumArticle,
           FicheTekID=this.FicheTekID,
         DateCreation=this.DateCreation,
         MiseAJour=this.MiseAJour,
        Concepteur=this.Concepteur,
        Redaction=this.Redaction,
        EnfDent = this.EnfDent,
        Verificateur=this.Verificateur,
        DuitageID=this.DuitageID,
        DuitageGomme=this.DuitageGomme,
        Dent=this.Dent,
        Peigne=this.Peigne,
        Ref=this.Ref,
        Name=this.Name,
        Name2=this.Name2,
        Largeur=this.Largeur,
        Epaisseur=this.Epaisseur,
        Client=this.Client,
        EnfilageID = this.EnfilageID,
        GetComposition=this.GetComposition,
        IsEnfilage = this.IsEnfilage,
        FicheId=this.FicheId,
        Definite = this.Definite,
        RedacteurObj = this.RedacteurObj,
        Redacteur = this.Redacteur,
        PeigneObj = this.PeigneObj,
            };
        }
    }
}