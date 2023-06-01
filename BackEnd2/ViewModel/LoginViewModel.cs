using System.Collections.Generic;
using System.Windows.Controls;
using BackEnd2.CustomClass;
using BackEnd2.Data;
using BackEnd2.Model;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Navigation.EventArguments;
using MvvmCross.ViewModels;
using System.Linq;

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
            getRedacteur();
        }

        private MvxViewModel SplashScreen;
        public override void Prepare(MvxViewModel parameter)
        {
            SplashScreen = parameter;
        }

        
        public void getVerificateur()
        {
            var list = new MvxObservableCollection<user>(_db2.GetUsers());
            UserList = new MvxObservableCollection<user>(list.Where(user => user.type == user.UserType.verificateur));
            SelectedVerificateur = UserList.FirstOrDefault();
        }
        public void getRedacteur()
        {
            var list = new MvxObservableCollection<user>(_db2.GetUsers());
            UserList = new MvxObservableCollection<user>(list.Where(user => user.type == user.UserType.redacteur));
            SelectedRedacteur = UserList.FirstOrDefault();
        }
        private MvxObservableCollection<user> _UserList;

        public MvxObservableCollection<user> UserList
        {
            get { return _UserList; }
            set { _UserList = value; RaisePropertyChanged(); }
        }
        
        private user _SelectedRedacteur;

        public user SelectedRedacteur
        {
            get { return _SelectedRedacteur; }
            set {
                _SelectedRedacteur = value;
                RaisePropertyChanged();
            }
        }
        private user _SelectedVerificateur;

        public user SelectedVerificateur
        {
            get { return _SelectedVerificateur; }
            set
            {
                _SelectedVerificateur = value;
                RaisePropertyChanged();
            }
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            //_navigationService.DidNavigate+=CloseWindow;
        }

        public MvxInteraction<YesNoQuestion> ConfirmAction { get; } = new MvxInteraction<YesNoQuestion>();

        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();



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

            if (SelectedRedacteur != null && passW.Password != null  &&
                !string.IsNullOrWhiteSpace(passW.Password))
            {
                var CorrectAuth = false;
                var userlist = _db2.GetUsers();
                foreach (var user in userlist)
                    if (user.type == user.UserType.redacteur)
                        if (user.username.Equals(SelectedRedacteur.username) && user.password.Equals(passW.Password))
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
        private int _SelectedTab;

        private IMvxCommand<int> _TabClickCmd;

        public IMvxCommand<int> TabClickCmd
        {
            get {
                _TabClickCmd = new MvxCommand<int>(TabChanged);
                return _TabClickCmd; }
        }

        public void TabChanged(int index)
        {
            if (index == 0)
                getRedacteur();
            else
                getVerificateur();

        }

        public void VerificateurLogin(object obj)
        {
            var passW = obj as PasswordBox;
            if (SelectedVerificateur != null && passW.Password != null  &&
                !string.IsNullOrWhiteSpace(passW.Password))
            {
                var CorrectAuth = false;
                var userlist = _db2.GetUsers();
                foreach (var user in userlist)
                    if (user.type == user.UserType.verificateur)
                        if (user.username.Equals(SelectedVerificateur.username) && user.password.Equals(passW.Password))
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