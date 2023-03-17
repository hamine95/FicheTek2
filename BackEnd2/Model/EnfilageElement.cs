using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class EnfilageElement : INotifyPropertyChanged
    {
        private bool _AddElement;

        private MatrixElement _EnfElement;

        public EnfilageElement(MatrixElement enfEl, bool AddEl)
        {
            EnfElement = enfEl;
            AddElement = AddEl;
            UpdateContent = false;
            Content = null;
        }

        public EnfilageElement(MatrixElement enfEl, bool UpdatCont, Composition cont)
        {
            EnfElement = enfEl;
            AddElement = false;
            UpdateContent = UpdatCont;
            Content = cont;
        }

        public bool UpdateContent { get; set; }

        public Composition Content { get; set; }

        public MatrixElement EnfElement
        {
            get => _EnfElement;
            set
            {
                _EnfElement = value;
                NotifyPropertyChanged();
            }
        }

        public bool AddElement
        {
            get => _AddElement;
            set
            {
                _AddElement = value;
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