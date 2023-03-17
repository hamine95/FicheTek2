using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace DSheetEnfilage
{
    public class GameBoard : INotifyPropertyChanged
    {
        private ObservableCollection<Block> _blockList;

        private int _cols = 2;

        private int _rows = 2;

        public GameBoard()
        {
            var m1 = new Block { Name = "b1", Background = Brushes.Red, Col = 0, Row = 0 };
            var m2 = new Block { Name = "b2", Background = Brushes.Gray, Col = 0, Row = 0 };
            var m3 = new Block { Name = "b3", Background = Brushes.Goldenrod, Col = 0, Row = 0 };
            var m4 = new Block { Name = "b4", Background = Brushes.Honeydew, Col = 0, Row = 0 };

            _blockList = new ObservableCollection<Block>();
            for (var i = 0; i < 4897; i++)
            {
                var m5 = new Block { Name = "b1", Background = Brushes.Red, Col = 0, Row = 0 };
                _blockList.Add(m5);
            }
        }

        public int Rows
        {
            get => _rows;
            set
            {
                _rows = value;
                RaisePropertyChanged("Rows");
            }
        }

        public int Columns
        {
            get => _cols;
            set
            {
                _cols = value;
                RaisePropertyChanged("Columns");
            }
        }

        public ObservableCollection<Block> BlockList
        {
            get => _blockList;
            set
            {
                _blockList = value;
                RaisePropertyChanged("BlockList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
}