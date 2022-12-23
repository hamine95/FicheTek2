using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataSheetDesign
{
    public class Composant : INotifyPropertyChanged
    {
        private string _BKBorderComposant;

        private string _BKComposant;

        private string _DebutFil;

        private string _FGComposant;

        private bool _Intermittent;


        private string _NameComposant;

        private int _NumComposant;

        private string _NumFil;

        public string BKBorderComposant
        {
            get => _BKBorderComposant;
            set
            {
                _BKBorderComposant = value;
                NotifyPropertyChanged();
            }
        }

        public string BKComposant
        {
            get => _BKComposant;
            set
            {
                _BKComposant = value;
                NotifyPropertyChanged();
            }
        }

        public int NumComposant
        {
            get => _NumComposant;
            set
            {
                _NumComposant = value;
                NotifyPropertyChanged();
            }
        }

        public string FGComposant
        {
            get => _FGComposant;
            set
            {
                _FGComposant = value;
                NotifyPropertyChanged();
            }
        }

        public string NumFil
        {
            get => _NumFil;
            set
            {
                _NumFil = value;
                NotifyPropertyChanged();
            }
        }

        public string DebutFil
        {
            get => _DebutFil;
            set
            {
                _DebutFil = value;
                NotifyPropertyChanged();
            }
        }

        public bool Intermittent
        {
            get => _Intermittent;
            set
            {
                _Intermittent = value;
                NotifyPropertyChanged();
            }
        }

        public string NameComposant
        {
            get => _NameComposant;
            set
            {
                _NameComposant = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}