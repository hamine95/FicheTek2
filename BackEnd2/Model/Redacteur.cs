using System.Collections.Generic;
using System.ComponentModel;

using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Redacteur : INotifyPropertyChanged
    {
        private int _ID;


        private string _Name;

        private List<Produit> _Products;


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

        public List<Produit> Products
        {
            get => _Products;
            set
            {
                _Products = value;
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