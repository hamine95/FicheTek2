using BackEnd2.CustomClass;
using BackEnd2.Data;
using BackEnd2.Model;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd2.ViewModel
{
    public class ParamViewModel : MvxViewModel<user>
    {

        private SqliteData db;
        private user UserSession;
        private IMvxCommand _SaveChange;
        private IMvxCommand _CancelCmd;
        private IMvxCommand _Modifier;
        private bool _IsEditEnabled;
        private user _SelectedUser;
        private int _EditUserId;

        private IMvxNavigationService _navigationService;
        public ParamViewModel(IMvxNavigationService  _navServ)
        {
            _navigationService = _navServ;
            
        }

        private string _NovUsername;

        public string NovUsername
        {
            get { return _NovUsername; }
            set { _NovUsername = value; RaisePropertyChanged(); }
        }

        private string _NovPassword;

        public string NovPassword
        {
            get { return _NovPassword; }
            set { _NovPassword = value; RaisePropertyChanged(); }
        }


        public MvxInteraction<YesNoQuestion> ConfirmAction { get; } = new MvxInteraction<YesNoQuestion>();

        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();

        public IMvxCommand CancelCmd
        {
            get
            {
                _CancelCmd = new MvxCommand(CancelEdit);
                return _CancelCmd;
            }
        }
        public void CancelEdit()
        {
            IsEditEnabled = false;
            NovUsername = "";
            NovPassword = "";
        }
        public user SelectedUser
        {
            get => _SelectedUser;
            set
            {
                _SelectedUser = value;
                RaisePropertyChanged();
            }
        }

        public IMvxCommand Modifier
        {
            get
            {
                _Modifier = new MvxCommand(ModifierUtilisateur);
                return _Modifier;
            }
        }
        public void ModifierUtilisateur()
        {
            if (SelectedUser != null)
            {
                IsEditEnabled = true;
                NovUsername = SelectedUser.username;
                EditUserId = SelectedUser.ID;
            }
            else
            {
                SendNotification.Raise("S.V.P Séléctionnez un composant");
            }
        }

        public int EditUserId
        {
            get => _EditUserId;
            set
            {
                _EditUserId = value;
                RaisePropertyChanged();
            }
        }
        public bool IsEditEnabled
        {
            get => _IsEditEnabled;
            set
            {
                _IsEditEnabled = value;
                RaisePropertyChanged();
            }
        }
        public IMvxCommand SaveChange
        {
            get
            {
                _SaveChange = new MvxCommand(SaveEditChange);
                return _SaveChange;
            }
        }
        public bool IsEditFieldsEmpty()
        {
            return NovUsername == null || string.IsNullOrWhiteSpace(NovUsername)
                || NovPassword == null || string.IsNullOrWhiteSpace(NovPassword);
        }
        public void SaveEditChange()
        {
            if (!IsEditFieldsEmpty() )
            {
                var EditUser = new user();
                EditUser.ID = EditUserId;
                EditUser.username = NovUsername;
                EditUser.password = NovPassword;
                db.EditUser(EditUser);
                UpdateUserList();
                CancelEdit();
            }
            else if (IsEditFieldsEmpty())
            {
                SendNotification.Raise("Nom utilisateur ou mot de passe vide");
            }
        }

        private MvxObservableCollection<user> _ListUser;

        public MvxObservableCollection<user> ListUser
        {
            get { return _ListUser; }
            set { _ListUser = value; RaisePropertyChanged(); }
        }

        public void UpdateUserList()
        {
            ListUser = new MvxObservableCollection<user>(db.GetUserList());
           
        }
        public override void Prepare(user parameter)
        {
            db = Mvx.IoCProvider.Resolve<SqliteData>();
            UserSession = parameter;
            UpdateUserList();
        }
    }
}
