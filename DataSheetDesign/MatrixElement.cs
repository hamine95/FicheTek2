using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataSheetDesign
{
    public class MatrixElement : INotifyPropertyChanged
    {
        private string _BorderCO;
        private string _color;

        private Composant _Content;

        private string _Dash;
        private string _Hei;
        private bool _IsSelected;

        private string _Num;

        private string _TextBK;

        public MatrixElement(int x, int y)
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
                if (value)
                {
                    BorderCO = "Blue";
                    color = "Blue";
                }
                else
                {
                    if (Content == null)
                    {
                        BorderCO = "Black";
                        color = "White";
                    }
                    else
                    {
                        BorderCO = "" + Content.BKBorderComposant;
                        color = "" + Content.BKComposant;
                    }
                }
            }
        }

        public int X { get; }
        public int Y { get; }

        public string color
        {
            get => _color;
            set
            {
                _color = value;
                NotifyPropertyChanged();
            }
        }

        public string Hei
        {
            get => _Hei;
            set
            {
                _Hei = value;
                NotifyPropertyChanged();
            }
        }

        public string TextBK
        {
            get => _TextBK;
            set
            {
                _TextBK = value;
                NotifyPropertyChanged();
            }
        }

        public string BorderCO
        {
            get => _BorderCO;
            set
            {
                _BorderCO = value;
                NotifyPropertyChanged();
            }
        }

        public string Dash
        {
            get => _Dash;
            set
            {
                _Dash = value;
                NotifyPropertyChanged();
            }
        }

        public string Num
        {
            get => _Num;
            set
            {
                _Num = value;
                NotifyPropertyChanged();
            }
        }

        public Composant Content
        {
            get => _Content;
            set
            {
                _Content = value;
                NotifyPropertyChanged();
                SetContent();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetContent()
        {
            if (Content != null)
            {
                color = Content.BKComposant;

                Num = "" + Content.NumComposant;
                TextBK = Content.FGComposant;
                BorderCO = Content.BKBorderComposant;
                Dash = "";
            }
            else
            {
                color = "White";

                Num = "";
                BorderCO = "Black";
                Dash = "1";
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}