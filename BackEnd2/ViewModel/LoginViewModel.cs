using System.Collections.Generic;
using System.Windows.Controls;
using BackEnd2.CustomClass;
using BackEnd2.Data;
using BackEnd2.Database;
using BackEnd2.Model;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class LoginViewModel: MvxViewModel
    {
        public MvxInteraction<YesNoQuestion> ConfirmAction { get; } = new MvxInteraction<YesNoQuestion>();

        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();
        private SqliteData _db2;

        private IMvxNavigationService _navigationService;

        public LoginViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
            _db2 = Mvx.IoCProvider.Resolve<SqliteData>();
            
      
        }

        private user UserSession;

        private string _UsernameRed;
        private string _UsernameVer;

        public string UsernameRed
        {
            get
            {
                return _UsernameRed;
            }
            set
            {
                _UsernameRed = value;
            }
        }
        
        public string UsernameVer
        {
            get
            {
                return _UsernameVer;
            }
            set
            {
                _UsernameVer = value;
            }
        }
        private IMvxCommand _VerificateurCmd;

        public IMvxCommand VerificateurCmd
        {
            get
            {
                _VerificateurCmd = new MvxCommand<object>(VerificateurLogin);
                return _VerificateurCmd;
            }
        }
        private IMvxCommand _RedacteurCmd;

        public IMvxCommand RedacteurCmd
        {
            get
            {
                _RedacteurCmd = new MvxCommand<object>(RedacteurLogin);
                return _RedacteurCmd;
            }
        }

        public void RedacteurLogin(object obj)
        {
            PasswordBox passW = obj as PasswordBox;
      
            if (UsernameRed != null && passW.Password != null && !string.IsNullOrWhiteSpace(UsernameRed) && !string.IsNullOrWhiteSpace(passW.Password) )
            {
                bool CorrectAuth = false;
                List<user> userlist= _db2.GetUsers();
                foreach (var user in userlist)
                {
                    if (user.type == user.UserType.redacteur)
                    {
                        if (user.username.Equals(UsernameRed) && user.password.Equals(passW.Password))
                        {
                            CorrectAuth = true;
                            UserSession = user;
                        }
                    }
                }

                if (CorrectAuth)
                {
                    _navigationService.Navigate<HomepageViewModel,user>(UserSession);
                    _navigationService.Close(this);
                }
                else
                {
                    SendNotification.Raise("Utilisateur ou mode passe incorrect");
                }
             
            }
            else
            {
                SendNotification.Raise("Champ vide");
            }
        }
        public void VerificateurLogin(object obj)
        {
            PasswordBox passW = obj as PasswordBox;
            if (UsernameVer != null && passW.Password != null && !string.IsNullOrWhiteSpace(UsernameVer) && !string.IsNullOrWhiteSpace(passW.Password) )
            {
                bool CorrectAuth = false;
               List<user> userlist= _db2.GetUsers();
               foreach (var user in userlist)
               {
                   if (user.type == user.UserType.verificateur)
                   {
                       if (user.username.Equals(UsernameVer) && user.password.Equals(passW.Password))
                       {
                           CorrectAuth = true;
                           UserSession = user;
                       }
                   }
               }

               if (CorrectAuth)
               {
                   _navigationService.Navigate<HomepageViewModel,user>(UserSession);
                   _navigationService.Close(this);
               }
               else
               {
                   SendNotification.Raise("Utilisateur ou mode passe incorrect");
               }
             
            }
            else
            {
                SendNotification.Raise("Champ vide");
            }
        }
    }
}