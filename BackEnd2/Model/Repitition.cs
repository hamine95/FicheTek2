using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Repitition: INotifyPropertyChanged
    {
        private int _id;
        private int _enfilageID;
        private string _x;
        private string _y;
        private string _value;

        public string value
        {
            get
            {
                return _value;
                
            }
            set
            {
                _value = value;
                NotifyPropertyChanged();
            }
        }

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
        public string y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                NotifyPropertyChanged();
            }
        }
        public string x
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                NotifyPropertyChanged();
            }
        }
        public int enfilageID
        {
            get
            {
                return _enfilageID;
            }
            set
            {
                _enfilageID = value;
                NotifyPropertyChanged();
            }
        }

        private bool _vis;

        public bool vis
        {
            get
            {
                return _vis;
            }
            set
            {
                _vis = value;
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