using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Media;

namespace DSheetEnfilage
{
    public class GameBoard: INotifyPropertyChanged
    {
        public GameBoard()
        {
            var m1 = new Block() { Name = "b1", Background = Brushes.Red, Col=0, Row = 0 };
            var m2 = new Block() { Name = "b2", Background = Brushes.Gray, Col=0, Row = 0 };
            var m3 = new Block() { Name = "b3", Background = Brushes.Goldenrod, Col=0, Row = 0 };
            var m4 = new Block() { Name = "b4", Background = Brushes.Honeydew, Col=0, Row = 0 };
          
            _blockList = new ObservableCollection<Block>();
            for (int i = 0; i < 4897; i++)
            {
                var m5 = new Block() { Name = "b1", Background = Brushes.Red, Col=0, Row = 0 };
                _blockList.Add(m5);
            }

          
        }

        int _rows = 2;
        public int Rows { get { return _rows; } set { _rows = value; RaisePropertyChanged("Rows"); } }

        int _cols = 2;
        public int Columns { get { return _cols; } set { _cols = value; RaisePropertyChanged("Columns"); } }

        ObservableCollection<Block> _blockList;
        public ObservableCollection<Block> BlockList { get { return _blockList; } set { _blockList = value; RaisePropertyChanged("BlockList"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
}