using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class EnfilageElement: INotifyPropertyChanged
    {
        private MatrixElement _EnfElement;

        private bool _AddElement;

        private bool _UpdateContent;

        private Composition _Content;

        public bool UpdateContent
        {
            get => _UpdateContent;
            set => _UpdateContent = value;
        }

        public Composition Content
        {
            get => _Content;
            set => _Content = value;
        }

        public EnfilageElement(MatrixElement enfEl,bool AddEl)
        {
            EnfElement = enfEl;
            AddElement = AddEl;
            UpdateContent = false;
            Content = null;
        }
        public EnfilageElement(MatrixElement enfEl,bool UpdatCont,Composition cont)
        {
            EnfElement = enfEl;
            AddElement = false;
            UpdateContent = UpdatCont;
            Content = cont;
        }
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