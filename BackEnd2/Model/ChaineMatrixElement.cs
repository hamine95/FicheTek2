using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class ChaineMatrixElement: INotifyPropertyChanged
    {


        private bool _IsSelected;

        private bool _IsContent;


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

                SetContent();
                NotifyPropertyChanged();
            }
        }

        public string ImagePath
        {
            get => _ImagePath;
            set
            {
                _ImagePath = value;
                NotifyPropertyChanged();
            }
        }


        private string _ImagePath;
        
        public void SetContent()
        {
            
            if (IsContent==false)
            {
                ImagePath = "/Asset/squareLine2.png";
            }
            else
            {
                ImagePath = "/Asset/ChainSquare.png";
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