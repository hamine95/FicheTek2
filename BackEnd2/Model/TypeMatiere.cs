using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class TypeMatiere : INotifyPropertyChanged
    {
        private string _Codification;

        private int _ID;


        private string _MatiereNom;


        private List<Titrage> _Titrage;

        public int ID
        {
            get => _ID;
            set
            {
                _ID = value;
                NotifyPropertyChanged();
            }
        }

        public string MatiereNom
        {
            get => _MatiereNom;
            set
            {
                _MatiereNom = value;
                NotifyPropertyChanged();
            }
        }

        public string Codification
        {
            get => _Codification;
            set
            {
                _Codification = value;
                NotifyPropertyChanged();
            }
        }

        public List<Titrage> Titrage
        {
            get => _Titrage;
            set
            {
                _Titrage = value;
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