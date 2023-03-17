using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class Titrage : INotifyPropertyChanged
    {
        private string _Designation;

        private int _ID;


        private string _NumMetric;


        private int _NumTwist;


        private TypeMatiere _TypeMatiere;

        public int ID
        {
            get => _ID;
            set
            {
                _ID = value;
                NotifyPropertyChanged();
            }
        }

        public string Designation
        {
            get => _Designation;
            set
            {
                _Designation = value;
                NotifyPropertyChanged();
            }
        }

        public TypeMatiere TypeMatiere
        {
            get => _TypeMatiere;
            set
            {
                _TypeMatiere = value;
                NotifyPropertyChanged();
                if (TypeMatiere != null) TypeMatiere.PropertyChanged += Child_PropertyChanged;
            }
        }

        public int NumTwist
        {
            get => _NumTwist;
            set
            {
                _NumTwist = value;
                NotifyPropertyChanged();
            }
        }

        public string NumMetric
        {
            get => _NumMetric;
            set
            {
                _NumMetric = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            NotifyPropertyChangedParent(e.PropertyName);
        }

        private void NotifyPropertyChangedParent(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}