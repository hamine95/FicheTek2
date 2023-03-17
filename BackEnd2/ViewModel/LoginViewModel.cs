using System.Windows.Controls;
using BackEnd2.CustomClass;
using BackEnd2.Data;
using BackEnd2.Model;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Navigation.EventArguments;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class LoginViewModel : MvxViewModel<MvxViewModel>
    {
        private readonly SqliteData _db2;

        private readonly IMvxNavigationService _navigationService;
        private IMvxCommand _RedacteurCmd;

        private IMvxCommand _VerificateurCmd;

        private user UserSession;

        public LoginViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
            _db2 = Mvx.IoCProvider.Resolve<SqliteData>();
            
        }

        private MvxViewModel SplashScreen;
        public override void Prepare(MvxViewModel parameter)
        {
            SplashScreen = parameter;
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            //_navigationService.DidNavigate+=CloseWindow;
        }

        public MvxInteraction<YesNoQuestion> ConfirmAction { get; } = new MvxInteraction<YesNoQuestion>();

        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();

        public string UsernameRed { get; set; }

        public string UsernameVer { get; set; }

        public IMvxCommand VerificateurCmd
        {
            get
            {
                _VerificateurCmd = new MvxCommand<object>(VerificateurLogin);
                return _VerificateurCmd;
            }
        }

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
            var passW = obj as PasswordBox;

            if (UsernameRed != null && passW.Password != null && !string.IsNullOrWhiteSpace(UsernameRed) &&
                !string.IsNullOrWhiteSpace(passW.Password))
            {
                var CorrectAuth = false;
                var userlist = _db2.GetUsers();
                foreach (var user in userlist)
                    if (user.type == user.UserType.redacteur)
                        if (user.username.Equals(UsernameRed) && user.password.Equals(passW.Password))
                        {
                            CorrectAuth = true;
                            UserSession = user;
                        }

                if (CorrectAuth)
                {
                    UserSession.LoginViewM = this;
                    _navigationService.Navigate<HomepageViewModel, user>(UserSession);
                    //_navigationService.Close(this);
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
        public async void CloseWindow()
        {
            var b= await _navigationService.Close(this);
            if(SplashScreen!=null)
                b= await _navigationService.Close(SplashScreen);
        }
        public async void CloseWindow(object obj,IMvxNavigateEventArgs args)
        {
          var b= await _navigationService.Close(this);
          if(SplashScreen!=null)
              b= await _navigationService.Close(SplashScreen);
        }
        public void VerificateurLogin(object obj)
        {
            var passW = obj as PasswordBox;
            if (UsernameVer != null && passW.Password != null && !string.IsNullOrWhiteSpace(UsernameVer) &&
                !string.IsNullOrWhiteSpace(passW.Password))
            {
                var CorrectAuth = false;
                var userlist = _db2.GetUsers();
                foreach (var user in userlist)
                    if (user.type == user.UserType.verificateur)
                        if (user.username.Equals(UsernameVer) && user.password.Equals(passW.Password))
                        {
                            CorrectAuth = true;
                            UserSession = user;
                        }

                if (CorrectAuth)
                {
                    UserSession.LoginViewM = this;
                    UIServices.SetBusyState();
                    _navigationService.Navigate<HomepageViewModel, user>(UserSession);
                    //_navigationService.Close(this);
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