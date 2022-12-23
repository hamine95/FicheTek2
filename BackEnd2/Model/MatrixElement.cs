using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class MatrixElement : INotifyPropertyChanged
    {
        public enum BoxType
        {
            OutRange,
            Dents,
            Lisses,
            Empty,
            Inaccessible,
        }

        private BoxType _TypBox;

        public BoxType TypBox
        {
            get
            {
                return _TypBox;
            }
            set
            {
                _TypBox = value;
                NotifyPropertyChanged();
            }
        }
          private string _BorderCO;
        private string _color;

        private int _DentFil;
        private Composition _Content;

        private string _Dash;
        private string _Hei;
        private bool _IsSelected;

        private string _Num;

        private string _TextBK;

        private bool _IsContent;

        private int _position;

        public int position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                NotifyPropertyChanged();
            }
        }

        

        public void SetBoxType(BoxType typB)
        {
            TypBox = typB;
            if (IsContent == false)
            {
                if (typB == BoxType.Dents)
                {
                    Num = "/Asset/squareDent.png";
                
                }else if (typB == BoxType.Lisses)
                {
                    Num = "/Asset/squareLisse.png";
                }
                else if(typB==BoxType.Empty)
                {
                    Num = "/Asset/squareRect.png";
                }
                else if(typB==BoxType.OutRange)
                {
                    Num = "/Asset/squareLine2.png";
                }
                else
                {
                    Num = "/Asset/squareRect.png";
                }
            }
            
        }
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
                    if (Content == null)
                    {
                        Num = @"/Asset/squareHighlight.png";
                    }
                    else
                    {
                        if (Content.NumComposant == 1)
                        {
                            Num = @"/Asset/comp1light.png";
                        }else if (Content.NumComposant == 2)
                        {
                            Num = @"/Asset/comp2light.png";
                        }else if (Content.NumComposant == 3)
                        {
                            Num = @"/Asset/comp3light.png";
                        }else if (Content.NumComposant == 4)
                        {
                            Num = @"/Asset/comp4light.png";
                        }else if (Content.NumComposant ==5)
                        {
                            Num = @"/Asset/comp5light.png";
                        }else if (Content.NumComposant == 6)
                        {
                            Num = @"/Asset/comp6light.png";
                        }else if (Content.NumComposant == 7)
                        {
                            Num = @"/Asset/comp7light.png";
                        }
                       
                    }
                 
                }
                else
                {
                    if (Content == null)
                    {
                        if (TypBox == BoxType.Dents)
                        {
                            Num = "/Asset/squareDent.png";
                
                        }else if (TypBox == BoxType.Lisses)
                        {
                            Num = "/Asset/squareLisse.png";
                        }else if (TypBox == BoxType.Empty)
                        {
                            Num = "/Asset/squareRect.png";
                        }
                        else
                        {
                            Num = @"/Asset/squareLine2.png";
                        }
                        
                      
                    }
                    else
                    {
                      SetContent();
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

        public Composition Content
        {
            get => _Content;
            set
            {
                _Content = value;
                NotifyPropertyChanged();
                SetContent();
            }
        }

        public bool IsContent
        {
            get => _IsContent;
            set
            {
                _IsContent = value;
                NotifyPropertyChanged();
            }
        }

        
        public int DentFil
        {
            get => _DentFil;
            set
            {
                _DentFil = value;
                NotifyPropertyChanged();
            }
        }

        private Composant _SupportedComp;

        public Composant SupportedComp
        {
            get
            {
                return _SupportedComp;
            }
            set
            {
                _SupportedComp = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetContentComp1()
        {
            TextBK = "White";
            BorderCO = "Black";
            color = "Black";
        }
        public void SetContent()
        {
            if (Content != null)
            {
             
                if (DentFil == 0)
                {
                    Num = Content.ImageReedPath;
                }
                else
                {
                    Num = Content.ImagePath;
                }
                
                IsContent = true;
                
            }
            else
            {
                Num = "/Asset/squareLine2.png";
                IsContent = false;
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}