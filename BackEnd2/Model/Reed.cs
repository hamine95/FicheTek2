using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Reed :INotifyPropertyChanged
    {
        private int _ID;


        private double _Nombre;


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

        public double Nombre
        {
            get => _Nombre;
            set
            {
                _Nombre = value;
                NotifyPropertyChanged();
            }
        }

        private List<Produit> _Products;

        public List<Produit> Products
        {
            get
            {
                return _Products;
            }
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