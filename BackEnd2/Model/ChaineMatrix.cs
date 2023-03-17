using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class ChaineMatrix : INotifyPropertyChanged
    {
        private int _ID;
        private int _x;
        private int _y;

        public int ID
        {
            get => _ID;
            set
            {
                _ID = value;
                NotifyPropertyChanged();
            }
        }

        public int x
        {
            get => _x;
            set
            {
                _x = value;
                NotifyPropertyChanged();
            }
        }

        public int y
        {
            get => _y;
            set
            {
                _y = value;
                NotifyPropertyChanged();
            }
        }

        public chaine Chaine { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}