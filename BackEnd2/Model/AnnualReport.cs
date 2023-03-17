using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class AnnualReport: INotifyPropertyChanged
    {

        private string _categorie;

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
        
        private int _Commande;

        public int Commande
        {
            get
            {
                return _Commande;
            }
            set
            {
                _Commande = value;
                NotifyPropertyChanged();
            }
        }
        
        private int _Echantillon;

        public int Echantillon
        {
            get
            {
                return _Echantillon;
            }
            set
            {
                _Echantillon = value;
                NotifyPropertyChanged();
            }
        }
        private int _miseajour;

        public int miseajour
        {
            get
            {
                return _miseajour;
            }
            set
            {
                _miseajour = value;
                NotifyPropertyChanged();
            }
        }
        
        private int _Conforme;

        public int Conforme
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
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}