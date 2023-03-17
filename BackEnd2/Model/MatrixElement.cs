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
            Inaccessible
        }
        
        public enum CompImages
        {
            [Description(@"/Asset/comp1light.png")]
            comp1 = 1,
            [Description(@"/Asset/comp2light.png")]
            comp2,
            [Description(@"/Asset/comp3light.png")]
            comp3,
            [Description(@"/Asset/comp4light.png")]
            comp4,
            [Description(@"/Asset/comp5light.png")]
            comp5,
            [Description(@"/Asset/comp6light.png")]
            comp6,
            [Description(@"/Asset/comp7light.png")]
            comp7,
            [Description(@"/Asset/comp1.png")]
            comp1Print,
            [Description(@"/Asset/comp2.png")]
            comp2Print,
            [Description(@"/Asset/comp3.png")]
            comp3Print,
            [Description(@"/Asset/comp4.png")]
            comp4Print,
            [Description(@"/Asset/comp5.png")]
            comp5Print,
            [Description(@"/Asset/comp6.png")]
            comp6Print,
            [Description(@"/Asset/comp7.png")]
            comp7Print,
            [Description(@"/Asset/squareHighlight.png")]
            HighlightSquare,

        }

        private string _BorderCO;
        private string _color;
        private Composition _Content;

        private string _Dash;

        private int _DentFil;
        private string _Hei;

        private bool _IsContent;
        private bool _IsSelected;

        private ComponentDepiction _Num;

        private int _position;

        private Composant _SupportedComp;

        private string _TextBK;

        private BoxType _TypBox;

        public MatrixElement(int x, int y)
        {
            X = x;
            Y = y;
        }

        public BoxType TypBox
        {
            get => _TypBox;
            set
            {
                _TypBox = value;
                NotifyPropertyChanged();
            }
        }

    

        public int position
        {
            get => _position;
            set
            {
                _position = value;
                NotifyPropertyChanged();
            }
        }


        public string ImagePath(CompImages val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
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
                        Num = new ComponentDepiction(ImagePath(CompImages.HighlightSquare), ImagePath(CompImages.HighlightSquare));
                    }
                    else
                    {
                        if (Content.NumComposant == 1)
                            Num =new ComponentDepiction(ImagePath(CompImages.comp1Print), ImagePath(CompImages.comp1));
                        else if (Content.NumComposant == 2)
                            Num = new ComponentDepiction(ImagePath(CompImages.comp2Print), ImagePath(CompImages.comp2));
                        else if (Content.NumComposant == 3)
                            Num = new ComponentDepiction(ImagePath(CompImages.comp3Print), ImagePath(CompImages.comp3));
                        else if (Content.NumComposant == 4)
                            Num = new ComponentDepiction(ImagePath(CompImages.comp4Print), ImagePath(CompImages.comp4));
                        else if (Content.NumComposant == 5)
                            Num = new ComponentDepiction(ImagePath(CompImages.comp5Print), ImagePath(CompImages.comp5));
                        else if (Content.NumComposant == 6)
                            Num = new ComponentDepiction(ImagePath(CompImages.comp6Print), ImagePath(CompImages.comp6));
                        else if (Content.NumComposant == 7) Num = new ComponentDepiction(ImagePath(CompImages.comp7Print), ImagePath(CompImages.comp7));
                    }
                }
                else
                {
                    if (Content == null)
                    {
                        if (TypBox == BoxType.Dents)
                            Num = new ComponentDepiction("/Asset/squareDent.png", "/Asset/squareDent.png");
                        else if (TypBox == BoxType.Lisses)
                            Num = new ComponentDepiction("/Asset/squareLisse.png", "/Asset/squareLisse.png");
                        else if (TypBox == BoxType.Empty)
                            Num = new ComponentDepiction("/Asset/squareRect.png", "/Asset/squareRect.png"); 
                        else
                            Num = new ComponentDepiction("/Asset/squareLine2.png", "/Asset/squareLine2.png");
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

        public ComponentDepiction Num
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

        public Composant SupportedComp
        {
            get => _SupportedComp;
            set
            {
                _SupportedComp = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public void SetBoxType(BoxType typB)
        {
            TypBox = typB;
            if (IsContent == false)
            {
                if (typB == BoxType.Dents)
                    Num = new ComponentDepiction("/Asset/squareDent.png", "/Asset/squareDent.png"); 
                else if (typB == BoxType.Lisses)
                    Num = new ComponentDepiction("/Asset/squareLisse.png", "/Asset/squareLisse.png"); 
                else if (typB == BoxType.Empty)
                    Num = new ComponentDepiction("/Asset/squareRect.png", "/Asset/squareRect.png");
                else if (typB == BoxType.OutRange)
                    Num = new ComponentDepiction("/Asset/squareLine2.png", "/Asset/squareLine2.png");
                else
                    Num = new ComponentDepiction("/Asset/squareRect.png", "/Asset/squareRect.png"); 
            }
        }

        


        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}