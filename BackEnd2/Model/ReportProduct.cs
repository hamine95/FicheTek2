using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class ReportProduct: INotifyPropertyChanged
    {
        private int _id;
        private DateTime? _Date;
        private string _Ref;
        private string _Designation;
        private string _Version;
        private bool _Creation;
        private bool _Update;
        private bool _Conforme;
        private string _Remarque;
        private int _repID;
        private string _categorie;

        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }
        
        public string categorie
        {
            get
            {
                return _categorie;
            }
            set
            {
                _categorie = value;
                NotifyPropertyChanged();
            }
        }

        public int repID
        {
            get
            {
                return _repID;
            }
            set
            {
                _repID = value;
                NotifyPropertyChanged();
            }
        }
        public string Remarque
        {
            get
            {
                return _Remarque;
            }
            set
            {
                _Remarque = value;
                NotifyPropertyChanged();
            }
        }
        public string Ref
        {
            get
            {
                return _Ref;
            }
            set
            {
                _Ref = value;
                NotifyPropertyChanged();
            }
        }

        public string Designation
        {
            get
            {
                return _Designation;
            }
            set
            {
                _Designation = value;
                NotifyPropertyChanged();
            }
        }

        public string Version
        {
            get
            {
                return _Version;
            }
            set
            {
                _Version = value;
                NotifyPropertyChanged();
            }
        }

        public bool miseajour
        {
            get
            {
                return _Update;
                
            }
            set
            {
                _Update = value;
                NotifyPropertyChanged();
            }
        }

        public bool nonConforme
        {
            get
            {
                return _Conforme;
            }
            set
            {
                _Conforme = value;
                NotifyPropertyChanged();
            }
        }
        
        public bool Creation
        {
            get
            {
                return _Creation;
                
            }
            set
            {
                _Creation = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime? DateProd
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
                NotifyPropertyChanged();
            }
        }
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}