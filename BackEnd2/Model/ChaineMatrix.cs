using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class ChaineMatrix: INotifyPropertyChanged
    {
        private int _ID;
        private int _x;
        private int _y;

        private chaine _Chaine;
        [Key]  public int ID {
            get
            {
               return _ID;
            }
            set
            {
                _ID = value;
                NotifyPropertyChanged();
            } }

        public int x {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                NotifyPropertyChanged();
            } }

        public int y {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                NotifyPropertyChanged();
            } }

        public chaine Chaine
        {
            get => _Chaine;
            set => _Chaine = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

  
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}