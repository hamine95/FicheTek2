using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class UpDownCompo: INotifyPropertyChanged
    {


        private string _BKComposant;

        private string _BKBorderComposant;

        public string BKComposant
        {
            get => _BKComposant;
            set
            {
                _BKComposant = value;
                NotifyPropertyChanged();
            }
        }

        public string BKBorderComposant
        {
            get => _BKBorderComposant;
            set
            {
                _BKBorderComposant = value;
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