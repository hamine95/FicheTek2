using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class WorkRectangle : INotifyPropertyChanged
    {
        private int _DentLen;
        private int _FirstEmptyLen;

        private int _LisseLen;
        private int _NbrPart;
        private int _PartHeight;
        private int _PartsHeight;
        private int _PartsHeight_End;
        private int _PartsHeight_Start;
        private int _PartsWidth;
        private int _PartsWidth_End;
        private int _PartsWidth_Start;
        private int _SecEmptyLen;

        private int _ChaineRow;

        public int ChaineRow
        {
            get { return _ChaineRow; }
            set { _ChaineRow = value; NotifyPropertyChanged(); }
        }


        private int _ChaineColumn;

        public int ChaineColumn
        {
            get { return _ChaineColumn; }
            set { _ChaineColumn = value;
                NotifyPropertyChanged();
            }
        }

        private int _MaxNbrFil;

        public int MaxNbrFil
        {
            get { return _MaxNbrFil; }
            set { _MaxNbrFil = value;
                NotifyPropertyChanged();
            }
        }



        public int LisseLen
        {
            get => _LisseLen;
            set
            {
                _LisseLen = value;
                NotifyPropertyChanged();
            }
        }

        public int FirstEmptyLen
        {
            get => _FirstEmptyLen;
            set
            {
                _FirstEmptyLen = value;
                NotifyPropertyChanged();
            }
        }

        public int DentLen
        {
            get => _DentLen;
            set
            {
                _DentLen = value;
                NotifyPropertyChanged();
            }
        }

        public int SecEmptyLen
        {
            get => _SecEmptyLen;
            set
            {
                _SecEmptyLen = value;
                NotifyPropertyChanged();
            }
        }

        public int NbrPart
        {
            get => _NbrPart;
            set
            {
                _NbrPart = value;
                NotifyPropertyChanged();
            }
        }

        public int PartHeight
        {
            get => _PartHeight;
            set
            {
                _PartHeight = value;
                NotifyPropertyChanged();
            }
        }

        public int PartsHeight
        {
            get => _PartsHeight;
            set
            {
                _PartsHeight = value;
                NotifyPropertyChanged();
            }
        }

        public int PartsWidth
        {
            get => _PartsWidth;
            set
            {
                _PartsWidth = value;
                NotifyPropertyChanged();
            }
        }

        public int PartsWidthStart
        {
            get => _PartsWidth_Start;
            set
            {
                _PartsWidth_Start = value;
                NotifyPropertyChanged();
            }
        }

        public int PartsWidthEnd
        {
            get => _PartsWidth_End;
            set
            {
                _PartsWidth_End = value;
                NotifyPropertyChanged();
            }
        }

        public int PartsHeightStart
        {
            get => _PartsHeight_Start;
            set
            {
                _PartsHeight_Start = value;
                NotifyPropertyChanged();
            }
        }

        public int PartsHeightEnd
        {
            get => _PartsHeight_End;
            set
            {
                _PartsHeight_End = value;
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