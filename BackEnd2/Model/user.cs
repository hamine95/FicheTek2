using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BackEnd2.Model
{
    public class user: INotifyPropertyChanged
    {
        public enum UserType
        {
            verificateur=1,
            redacteur=2,
            superuser=3,
            
        }

        private int _ID;

        private string _username;

        private string _password;
        private UserType _type;

        public int Id
        {
            get => _ID;
            set
            {
                _ID = value;
                NotifyPropertyChanged();
            }
        }

        public string username
        {
            get => _username;
            set
            {
                _username = value;
                NotifyPropertyChanged();
            }
        }

        public string password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyPropertyChanged();
            }
        }

        public UserType type
        {
            get => _type;
            set
            {
                _type = value;
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