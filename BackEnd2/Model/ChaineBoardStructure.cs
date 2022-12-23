using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class ChaineBoardStructure: INotifyPropertyChanged
    {
        private List<ChaineMatrixElement> _board;


        public ChaineBoardStructure(int rows, int columns)
        {
            Board = new List<ChaineMatrixElement>();
            for (var r = 0; r < rows; r++)
            for (var c = 0; c < columns; c++)
                Board.Add(new ChaineMatrixElement(c, r)
                    { ImagePath = "/Asset/squareLine2.png"});
            ;
            ;
        }

        public List<ChaineMatrixElement> Board
        {
            get
            {return _board;
            } 
            set
            {
                _board = value;
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