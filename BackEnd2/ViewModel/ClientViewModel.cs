using System.Threading.Tasks;
using BackEnd2.CustomClass;
using BackEnd2.Database;
using BackEnd2.Model;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class ClientViewModel : MvxViewModel<MyDBContext>
    {
        private string _ClientNom;


        private int _EditClientId;
        private bool _IsEditEnabled;
        private MvxObservableCollection<Client> _ListClient;
        private IMvxNavigationService _navigationService;


        private string _NovClientNom;

        private Client _SelectedClient;
        private  MyDBContext db;

        public ClientViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
           
        }

        public override Task Initialize()
        {
            LoadClients=MvxNotifyTask.Create(UpdateClientList);
            return base.Initialize();
        }

        private MvxNotifyTask _LoadClients;

        public MvxNotifyTask LoadClients
        {
            get => _LoadClients;
            set => SetProperty(ref _LoadClients, value);
        }
        public MvxInteraction<YesNoQuestion> ConfirmAction { get; } = new MvxInteraction<YesNoQuestion>();

        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();

        public int EditClientId
        {
            get => _EditClientId;
            set
            {
                _EditClientId = value;
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

        private IMvxCommand _Modifier;

        public IMvxCommand Modifier
        {
            get
            {
                _Modifier = new MvxCommand(ModifierClient);
                return _Modifier;
            }
        }

        private IMvxCommand _Supprimer;

        public IMvxCommand Supprimer
        {
            get
            {
                _Supprimer = new MvxCommand(SupprimerClient);
                return _Supprimer;
            }
        }

        private IMvxCommand _AjouterNovClient;

        public IMvxCommand AjouterNovClient
        {
            get
            {
                _AjouterNovClient = new MvxCommand(AjouterNoveauClient);
                return _AjouterNovClient;
            }
        }

        private IMvxCommand _CancelCmd;

        public IMvxCommand CancelCmd
        {
            get
            {
                _CancelCmd = new MvxCommand(CancelEdit);
                return _CancelCmd;
            }
        }

        private IMvxCommand _saveChange;

        public IMvxCommand SaveChange
        {
            get
            {
                _saveChange = new MvxCommand(SaveEditChange);
                return _saveChange;
            }
        }

        public MvxObservableCollection<Client> ListClient
        {
            get => _ListClient;
            set
            {
                _ListClient = value;
                RaisePropertyChanged();
            }
        }

        public Client SelectedClient
        {
            get => _SelectedClient;
            set
            {
                _SelectedClient = value;
                RaisePropertyChanged();
            }
        }

        public string ClientNom
        {
            get => _ClientNom;
            set
            {
                _ClientNom = value;
                RaisePropertyChanged();
            }
        }

        public string NovClientNom
        {
            get => _NovClientNom;
            set
            {
                _NovClientNom = value;
                RaisePropertyChanged();
            }
        }

        public void SaveEditChange()
        {
            if (!IsEditFieldsEmpty() && db.GetClient(NovClientNom) == null)
            {
                var NewClient = new Client();
                NewClient.ID = EditClientId;
                NewClient.Name = NovClientNom;
                db.EditClient(NewClient);
                UpdateClientList();
                CancelEdit();
            }
            else if (IsEditFieldsEmpty())
            {
                SendNotification.Raise("nom client est vide");
            }

            else
            {
                SendNotification.Raise("client existe déja");
            }
        }

        public async Task UpdateClientList()
        {
            await Task.Run(() =>
                {
                    var ListCli = db.GetClients();
                    foreach (var col in ListCli) col.Name = col.Name.ToUpper();
                    ListClient = new MvxObservableCollection<Client>(ListCli);
                }
            );

        }

        public void CancelEdit()
        {
            IsEditEnabled = false;
            NovClientNom = "";
        }

        public void ModifierClient()
        {
            if (SelectedClient != null)
            {
                IsEditEnabled = true;
                NovClientNom = SelectedClient.Name.ToUpper();
                EditClientId = SelectedClient.ID;
            }
            else
            {
                SendNotification.Raise("S.V.P Séléctionnez une couleur");
            }
        }

        public void SupprimerClient()
        {
            if (SelectedClient != null)
            {
                var req = new YesNoQuestion
                {
                    Question = "êtes-vous sûr de vouloir supprimer ce client séléctionnée ?",
                    UploadCallback = ok =>
                    {
                        if (ok)
                        {
                            db.DeleteClient(SelectedClient);
                            UpdateClientList();
                        }
                    }
                };
                ConfirmAction.Raise(req);
            }
            else
            {
                SendNotification.Raise("S.V.P Séléctionnez un client");
            }
        }

        public void AjouterNoveauClient()
        {
            if (!IsAddFieldsEmpty() && db.GetClient(ClientNom) == null)
            {
                var NewClient = new Client();
                NewClient.Name = ClientNom.ToUpper();
                db.AddNewClient(NewClient);
                UpdateClientList();
                ClientNom = "";
            }
            else if (IsAddFieldsEmpty())
            {
                SendNotification.Raise("Nom Client est vide");
            }

            else
            {
                SendNotification.Raise("Client existe déja");
            }
        }

        public bool IsAddFieldsEmpty()
        {
            return ClientNom == null || string.IsNullOrWhiteSpace(ClientNom);
        }

        public bool IsEditFieldsEmpty()
        {
            return NovClientNom == null || string.IsNullOrWhiteSpace(NovClientNom);
        }

        public override void Prepare(MyDBContext parameter)
        {
            db = parameter;
        }
    }
}