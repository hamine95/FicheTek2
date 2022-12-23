using System.ComponentModel;
using System.Windows.Media;

namespace DSheetEnfilage
{
    public class Block : INotifyPropertyChanged
    {
        
        int _row;
        public int Row { get { return _row; } set { _row = value; RaisePropertyChanged("Row"); } }

        int _col;
        public int Col { get { return _col; } set { _col = value; RaisePropertyChanged("Col"); } }

        string _name;
        public string Name { get { return _name; } set { _name = value; RaisePropertyChanged("Name"); } }

        Brush _background;
        public Brush Background { get { return _background; } set { _background = value; RaisePropertyChanged("Background"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
}