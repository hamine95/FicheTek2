using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Couleur : INotifyPropertyChanged
    {
        private int _ID;


        private string _Name;


        private int _Nbr;

        public int ID
        {
            get => _ID;
            set
            {
                _ID = value;
                NotifyPropertyChanged();
            }
        }

        public int Nbr
        {
            get => _Nbr;
            set
            {
                _Nbr = value;
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


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}