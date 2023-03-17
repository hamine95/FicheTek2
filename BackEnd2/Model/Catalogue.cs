using System.Collections.Generic;
using System.ComponentModel;

using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Catalogue : INotifyPropertyChanged
    {
        private List<Catalogue> _Children;
        private string _Designation;
        private int _ID;

        private int _parent;

        public int parent
        {
            get => _parent;

            set
            {
                _parent = value;
                NotifyPropertyChanged();
            }
        }

     
        public int ID
        {
            get => _ID;
            set
            {
                _ID = value;
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

        public List<Catalogue> Children
        {
            get => _Children;
            set
            {
                _Children = value;
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