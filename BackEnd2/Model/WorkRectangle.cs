using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class WorkRectangle: INotifyPropertyChanged
    {
        private int _PartsHeight;
        private int _PartsWidth;
        private int _PartsWidth_Start;
        private int _PartsWidth_End;
        private int _PartsHeight_Start;
        private int _PartsHeight_End;
        private int _PartHeight;
        private int _NbrPart;

        public int NbrPart
        {
            get
            {
                return _NbrPart;
            }
            set
            {
                _NbrPart = value;
                NotifyPropertyChanged();
            }
        }

        public int PartHeight
        {
            get
            {
                return _PartHeight;
            }
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