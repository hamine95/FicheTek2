using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class ChColComp : INotifyPropertyChanged
    {
        private int _ChaineID;
        private int _ColNum;

        private Composant _Comp;
        private int _ComposantID;
        private int _ID;

        public int ID
        {
            get => _ID;
            set
            {
                _ID = value;
                NotifyPropertyChanged();
            }
        }

        public int ChaineID
        {
            get => _ChaineID;
            set
            {
                _ChaineID = value;
                NotifyPropertyChanged();
            }
        }

        public int ColNum
        {
            get => _ColNum;
            set
            {
                _ColNum = value;
                NotifyPropertyChanged();
            }
        }

        public int ComposantID
        {
            get => _ComposantID;
            set
            {
                _ComposantID = value;
                NotifyPropertyChanged();
            }
        }

        public Composant Comp
        {
            get => _Comp;
            set
            {
                _Comp = value;
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