using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Rang : INotifyPropertyChanged
    {
        private int y1;

        private int y2;


        public Rang()
        {
            y1 = -1;
            y2 = -1;
        }

        public int Y1
        {
            get => y1;
            set
            {
                y1 = value;
                NotifyPropertyChanged();
            }
        }

        public int Y2
        {
            get => y2;
            set
            {
                y2 = value;
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