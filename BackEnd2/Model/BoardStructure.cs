using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class BoardStructure : INotifyPropertyChanged
    {
        private List<MatrixElement> _board;


        public BoardStructure(int rows, int columns)
        {
            Board = new List<MatrixElement>();
            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < columns; c++)
                {
                   
                        Board.Add(new MatrixElement(c, r)
                            {Num="/Asset/squareLine2.png",TextBK = "Black",BorderCO = "Black",color = "White"});
                   
                        
                    
                  
                }  
            }
       
           
            ;
            ;
        }

        public List<MatrixElement> Board
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