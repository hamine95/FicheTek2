using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Enfilage:INotifyPropertyChanged
    {
        [Key] public int ID { get; set; }

        public chaine GetChaine { get; set; }

        public List<EnfilageMatrix> GetMatrix { get; set; }

        private string _TrXposition;

        public string TrXposition
        {
            get => _TrXposition;
            set
            {
                _TrXposition = value;
                NotifyPropertyChanged();
            }
        }

        public string TrYposition
        {
            get => _TrYposition;
            set
            {
                _TrYposition = value;
                NotifyPropertyChanged();
            }
        }

        private string _TrYposition;

        private int _NbrDent;
        public int Column { get; set; }

        public int Row { get; set; }

        public int NbrDent
        {
            get => _NbrDent;
            set
            {
                _NbrDent = value;
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