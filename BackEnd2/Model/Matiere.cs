using System.ComponentModel;

using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Matiere : INotifyPropertyChanged
    {
        private string _Designation;


        private Couleur _GetCouleur;

        private int _ID;


        private string _Ref;


        private Titrage _Titrage;

        public int ID
        {
            get => _ID;
            set
            {
                _ID = value;
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

        public string Designation
        {
            get => _Designation;
            set
            {
                _Designation = value;
                NotifyPropertyChanged();
            }
        }

        public Titrage Titrage
        {
            get => _Titrage;
            set
            {
                _Titrage = value;
                NotifyPropertyChanged();
                if (Titrage != null) Titrage.PropertyChanged += Child_PropertyChanged;
            }
        }

        public Couleur GetCouleur
        {
            get => _GetCouleur;
            set
            {
                _GetCouleur = value;
                if (value != null)
                {
                    NotifyPropertyChanged();
                    GetCouleur.PropertyChanged += Child_PropertyChanged;
                }
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChangedParent(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            NotifyPropertyChangedParent(e.PropertyName);
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}