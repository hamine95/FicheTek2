using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Composant : INotifyPropertyChanged
    {
        private int _ID;
        private int _parent;

        private string _Name;

        public int ID
        {
            get => _ID;
            set
            {
                _ID = value;
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

        public int parent
        {
            get => _parent;
            set
            {
                _parent = value;
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