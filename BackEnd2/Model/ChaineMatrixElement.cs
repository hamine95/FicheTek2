using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class ChaineMatrixElement : INotifyPropertyChanged
    {
        public enum ComponentState
        {
            Vacant,
            Occupied,
            Inaccessibe,
        }
        private bool _IsContent;


        private bool _IsSelected;


        public ChaineMatrixElement(int x, int y)
        {
            X = x;
            Y = y;
            
        }

        public bool IsSelected
        {
            get => _IsSelected;
            set
            {
                _IsSelected = value;
                NotifyPropertyChanged();
            }
        }

        public int X { get; }
        public int Y { get; }

        public bool IsContent
        {
            get => _IsContent;
            set
            {
                _IsContent = value;

                NotifyPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

       

        private ComponentState _CellState;

        public ComponentState CellState
        {
            get
            {
                return _CellState;
            }
            set
            {
                _CellState = value;
                NotifyPropertyChanged();
            }
        }

        private int _NumComposant=1;

        public int NumComposant
        {
            get
            {
                return _NumComposant;
            }
            set
            {
                _NumComposant = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}