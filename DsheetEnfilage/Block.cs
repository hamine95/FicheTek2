using System.ComponentModel;
using System.Windows.Media;

namespace DSheetEnfilage
{
    public class Block : INotifyPropertyChanged
    {
        private Brush _background;

        private int _col;

        private string _name;

        private int _row;

        public int Row
        {
            get => _row;
            set
            {
                _row = value;
                RaisePropertyChanged("Row");
            }
        }

        public int Col
        {
            get => _col;
            set
            {
                _col = value;
                RaisePropertyChanged("Col");
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        public Brush Background
        {
            get => _background;
            set
            {
                _background = value;
                RaisePropertyChanged("Background");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
}