using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class chaine : INotifyPropertyChanged
    {
        private List<ChColComp> _ChaineCompos;


        private int _Col;
        private int _lign;
        private string _Nom;

        private int id;


        public string Nom
        {
            get => _Nom;
            set
            {
                _Nom = value;
                NotifyPropertyChanged();
            }
        }

        public int Colonne
        {
            get => _Col;
            set
            {
                _Col = value;
                NotifyPropertyChanged();
            }
        }

        public int Ligne
        {
            get => _lign;
            set
            {
                _lign = value;
                NotifyPropertyChanged();
            }
        }

        public int ID
        {
            get => id;
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }

        public List<ChColComp> ChaineCompos
        {
            get => _ChaineCompos;
            set
            {
                _ChaineCompos = value;
                NotifyPropertyChanged();
            }
        }

        public List<ChaineMatrix> ChMatrix { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}