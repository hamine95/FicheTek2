using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Catalogue : INotifyPropertyChanged
    {
        private string _Designation;

        private int _parent;

        public int parent
        {
            get
            {
                return _parent;
            }

            set
            {
                _parent = value;
                NotifyPropertyChanged();
            }
        }
        private int _ID;

        [Key]
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

        private List<Catalogue> _Children;

        public List<Catalogue> Children
        {
            get
            {
                return _Children;
            }
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