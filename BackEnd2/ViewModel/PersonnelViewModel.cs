using System.Threading.Tasks;
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
    public class PersonnelViewModel : MvxViewModel<user>
    {
        private int _EditConcepteurId;
        private int _EditVerificateurId;
        private bool _IsEditEnabledConcepteur;
        private bool _IsEditEnabledVerificateur;


        private MvxObservableCollection<Concepteur> _ListConcepteur;
        private MvxObservableCollection<Verificateur> _ListVerificateur;

        private string _NameConcepteur;


        private string _NameVerificateur;

        private IMvxNavigationService _navigationService;

        private string _NovNameConcepteur;


        private string _NovNameVerificateur;

        private Concepteur _SelectedConcepteur;
        private Verificateur _SelectedVerificateur;
        private  MyDBContext db;

        public PersonnelViewModel(IMvxNavigationService _navser)
        {
            _navigationService = _navser;

        }

        public override Task Initialize()
        {
            LoadLists = MvxNotifyTask.Create(UpdateLists);
            return base.Initialize();
        }

        private MvxNotifyTask _LoadList;

        public MvxNotifyTask LoadLists
        {
            get => _LoadList;
            set => SetProperty(ref _LoadList, value);

        }
       
        
        public MvxInteraction<YesNoQuestion> ConfirmAction { get; } = new MvxInteraction<YesNoQuestion>();

        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();

        public int EditConcepteurId
        {
            get => _EditConcepteurId;
            set
            {
                _EditConcepteurId = value;
                RaisePropertyChanged();
            }
        }

        public int EditVerificateurId
        {
            get => _EditVerificateurId;
            set
            {
                _EditVerificateurId = value;
                RaisePropertyChanged();
            }
        }

        public bool IsEditEnabledConcepteur
        {
            get => _IsEditEnabledConcepteur;
            set
            {
                _IsEditEnabledConcepteur = value;
                RaisePropertyChanged();
            }
        }

        public bool IsEditEnabledVerificateur
        {
            get => _IsEditEnabledVerificateur;
            set
            {
                _IsEditEnabledVerificateur = value;
                RaisePropertyChanged();
            }
        }

        private IMvxCommand _ModifierConcepteur;

        public IMvxCommand ModifierConceptuer
        {
            get
            {
                _ModifierConcepteur = new MvxCommand(ModifierConcepteurSelected);
                return _ModifierConcepteur;
            }
        }

        private IMvxCommand _SupprimerConcepteur;

        public IMvxCommand SupprimerConcepteur
        {
            get
            {
                _SupprimerConcepteur = new MvxCommand(SupprimerConcepteurSelected);
                return _SupprimerConcepteur;
            }
        }

        private IMvxCommand _ModifierVerificateur;

        public IMvxCommand ModifierVerificateur
        {
            get
            {
                _ModifierVerificateur = new MvxCommand(ModifierVerificateurSelected);
                return _ModifierVerificateur;
            }
        }

        private IMvxCommand _SupprimerVerificateur;

        public IMvxCommand SupprimerVerificateur
        {
            get
            {
                _SupprimerVerificateur = new MvxCommand(SupprimerVerificateurSelected);
                return _SupprimerVerificateur;
            }
        }

        private IMvxCommand _AjouterNovConcepteur;

        public IMvxCommand AjouterNovConcepteur
        {
            get
            {
                _AjouterNovConcepteur = new MvxCommand(AjouterNoveauConcepteur);
                return _AjouterNovConcepteur;
            }
        }

        private IMvxCommand _AjouterNovVerificateur;

        public IMvxCommand AjouterNovVerificateur
        {
            get
            {
                _AjouterNovVerificateur = new MvxCommand(AjouterNoveauVerificateur);
                return _AjouterNovVerificateur;
            }
        }

        private IMvxCommand _CancelConcepteurCmd;

        public IMvxCommand CancelConcepteurCmd
        {
            get
            {
                _CancelConcepteurCmd = new MvxCommand(CancelEditConcepteur);
                return _CancelConcepteurCmd;
            }
        }

        private IMvxCommand _SaveConcepteurChange;

        public IMvxCommand SaveConcepteurChange
        {
            get
            {
                _SaveConcepteurChange = new MvxCommand(SaveEditConcepteurChange);
                return _SaveConcepteurChange;
            }
        }

        private IMvxCommand _CancelVerificateurCmd;

        public IMvxCommand CancelVerificateurCmd
        {
            get
            {
                _CancelVerificateurCmd = new MvxCommand(CancelEditVerificateur);
                return _CancelVerificateurCmd;
            }
        }

        private IMvxCommand _SaveVerificateurChange;

        public IMvxCommand SaveVerificateurChange
        {
            get
            {
                _SaveVerificateurChange = new MvxCommand(SaveEditVerificateurChange);
                return _SaveVerificateurChange;
            }
        }

        public MvxObservableCollection<Concepteur> ListConcepteur
        {
            get => _ListConcepteur;
            set
            {
                _ListConcepteur = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Verificateur> ListVerificateur
        {
            get => _ListVerificateur;
            set
            {
                _ListVerificateur = value;
                RaisePropertyChanged();
            }
        }

        public Verificateur SelectedVerificateur
        {
            get => _SelectedVerificateur;
            set
            {
                _SelectedVerificateur = value;
                RaisePropertyChanged();
            }
        }

        public Concepteur SelectedConcepteur
        {
            get => _SelectedConcepteur;
            set
            {
                _SelectedConcepteur = value;
                RaisePropertyChanged();
            }
        }

        public string NameConcepteur
        {
            get => _NameConcepteur;
            set
            {
                _NameConcepteur = value;
                RaisePropertyChanged();
            }
        }

        public string NameVerificateur
        {
            get => _NameVerificateur;
            set
            {
                _NameVerificateur = value;
                RaisePropertyChanged();
            }
        }

        public string NovNameConcepteur
        {
            get => _NovNameConcepteur;
            set
            {
                _NovNameConcepteur = value;
                RaisePropertyChanged();
            }
        }

        public string NovNameVerificateur
        {
            get => _NovNameVerificateur;
            set
            {
                _NovNameVerificateur = value;
                RaisePropertyChanged();
            }
        }

        public void SaveEditConcepteurChange()
        {
            if (!IsEditFieldsConcepteurEmpty() && db.GetConcepteur(NovNameConcepteur) == null)
            {
                var NewConcepteur = new Concepteur();
                NewConcepteur.Name = NovNameConcepteur.ToUpper();
                db.EditConcepteur(NewConcepteur);
                UpdateConcepteurList();
                CancelEditConcepteur();
            }
            else if (IsEditFieldsConcepteurEmpty())
            {
                SendNotification.Raise("le champ nom prénom est vide");
            }

            else
            {
                SendNotification.Raise("Concepteur existe déja");
            }
        }

        public void SaveEditVerificateurChange()
        {
            if (!IsEditFieldsVerificateurrEmpty() && db.GetVerificateur(NovNameVerificateur) == null)
            {
                var NewVerificateur = new Verificateur();
                NewVerificateur.Name = NovNameVerificateur.ToUpper();
                db.EditVerificateur(NewVerificateur);
                UpdateVerificateurList();
                CancelEditVerificateur();
            }
            else if (IsEditFieldsVerificateurrEmpty())
            {
                SendNotification.Raise("Le champ nom et prénom est vide");
            }

            else
            {
                SendNotification.Raise("vérificateur existe déja");
            }
        }

        public async Task UpdateVerificateurList()
        {
           await Task.Run(() =>
                {
                    var listVerifica = db.GetVerificateur();
                    foreach (var col in listVerifica) col.Name = col.Name.ToUpper();
                    ListVerificateur = new MvxObservableCollection<Verificateur>(listVerifica);
                }
            );

        }

        public async Task UpdateLists()
        {
            await UpdateConcepteurList();
            await UpdateVerificateurList();
            await UpdateClientList();
            await UpdateRedacteurList();
        }
        public async Task UpdateConcepteurList()
        {
          await  Task.Run(() =>
                {
                    var ListConcept = db.GetConcepteur();
                    foreach (var col in ListConcept) col.Name = col.Name.ToUpper();
                    ListConcepteur = new MvxObservableCollection<Concepteur>(ListConcept);
                }
            );
      
           
        }

        public void CancelEditConcepteur()
        {
            IsEditEnabledConcepteur = false;
            NovNameConcepteur = "";
        }

        public void CancelEditVerificateur()
        {
            IsEditEnabledVerificateur = false;
            NovNameVerificateur = "";
        }

        public void ModifierConcepteurSelected()
        {
            if (SelectedConcepteur != null)
            {
                IsEditEnabledConcepteur = true;
                NovNameConcepteur = SelectedConcepteur.Name;
                EditConcepteurId = SelectedConcepteur.ID;
            }
            else
            {
                SendNotification.Raise("S.V.P Séléctionnez un concepteur");
            }
        }

        public void ModifierVerificateurSelected()
        {
            if (SelectedVerificateur != null)
            {
                IsEditEnabledVerificateur = true;
                NovNameVerificateur = SelectedVerificateur.Name;
                EditVerificateurId = SelectedVerificateur.ID;
            }
            else
            {
                SendNotification.Raise("S.V.P Séléctionnez un vérificateur");
            }
        }

        public void SupprimerConcepteurSelected()
        {
            if (SelectedConcepteur != null)
            {
                var req = new YesNoQuestion
                {
                    Question = "êtes-vous sûr de vouloir supprimer ce concepteur séléctionnée ?",
                    UploadCallback = ok =>
                    {
                        if (ok)
                        {
                            db.DeleteConcepteur(SelectedConcepteur);
                            UpdateConcepteurList();
                        }
                    }
                };
                ConfirmAction.Raise(req);
            }
            else
            {
                SendNotification.Raise("S.V.P Séléctionnez une concepteur");
            }
        }

        public void SupprimerVerificateurSelected()
        {
            if (SelectedVerificateur != null)
            {
                var req = new YesNoQuestion
                {
                    Question = "êtes-vous sûr de vouloir supprimer ce verificateur séléctionnée ?",
                    UploadCallback = ok =>
                    {
                        if (ok)
                        {
                            db.DeleteVerificateur(SelectedVerificateur);
                            UpdateVerificateurList();
                        }
                    }
                };
                ConfirmAction.Raise(req);
            }
            else
            {
                SendNotification.Raise("S.V.P Séléctionnez une verificateur");
            }
        }

        public void AjouterNoveauConcepteur()
        {
            if (!IsAddFieldsConcepteurEmpty() && db.GetConcepteur(NameConcepteur) == null)
            {
                var NewConcept = new Concepteur();
                NewConcept.Name = NameConcepteur.ToUpper();
                db.AddNewConcepteur(NewConcept);
                UpdateConcepteurList();
                NameConcepteur = "";
            }
            else if (IsAddFieldsConcepteurEmpty())
            {
                SendNotification.Raise("le nom prénom est vide");
            }

            else
            {
                SendNotification.Raise("concepteur existe déja");
            }
        }

        public void AjouterNoveauVerificateur()
        {
            if (!IsAddFieldsVerificateurEmpty() && db.GetVerificateur(NameVerificateur) == null)
            {
                var NewVerificateur = new Verificateur();
                NewVerificateur.Name = NameVerificateur.ToUpper();
                db.AddNewVerificateur(NewVerificateur);
                UpdateVerificateurList();
                NameVerificateur = "";
            }
            else if (IsAddFieldsVerificateurEmpty())
            {
                SendNotification.Raise("le nom prénom est vide");
            }

            else
            {
                SendNotification.Raise("vérificateur existe déja");
            }
        }

        public bool IsAddFieldsConcepteurEmpty()
        {
            return NameConcepteur == null || string.IsNullOrWhiteSpace(NameConcepteur);
        }

        public bool IsAddFieldsVerificateurEmpty()
        {
            return NameVerificateur == null || string.IsNullOrWhiteSpace(NameVerificateur);
        }

        public bool IsEditFieldsConcepteurEmpty()
        {
            return NovNameConcepteur == null || string.IsNullOrWhiteSpace(NovNameConcepteur);
        }

        public bool IsEditFieldsVerificateurrEmpty()
        {
            return NovNameVerificateur == null || string.IsNullOrWhiteSpace(NovNameVerificateur);
        }

        private bool _IsVerificateur;

        public bool IsVerificateur
        {
            get
            {
                return _IsVerificateur;
            }
            set
            {
                _IsVerificateur = value;
                RaisePropertyChanged();
            }
        }
        private bool _IsSuperUser;

        public bool IsSuperUser
        {
            get
            {
                return _IsSuperUser;
            }
            set
            {
                _IsSuperUser = value;
                RaisePropertyChanged();
            }
        }
        private user UserSession;
        private SqliteData db2;
        public override void Prepare(user parameter)
        {
            UserSession = parameter;
            db = Mvx.IoCProvider.Resolve<MyDBContext>();
            db2 = Mvx.IoCProvider.Resolve<SqliteData>();
            if (UserSession.type == user.UserType.verificateur)
            {
                IsVerificateur = true;
                
            }
            else
            {
                IsVerificateur = false;
            }
        }

        #region Client
 private string _ClientNom;


        private int _EditClientId;
        private bool _IsEditEnabled;
        private MvxObservableCollection<Client> _ListClient;


        private string _NovClientNom;

        private Client _SelectedClient;
       

     
      

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
        

        #endregion

        #region Redacteur

        private IMvxCommand _AjouterNovRedacteur;

        public IMvxCommand AjouterNovRedacteur
        {
            get
            {
                _AjouterNovRedacteur = new MvxCommand(AjouterNoveauRedacteur);
                return _AjouterNovRedacteur;
            }
        }
        public bool IsAddFieldsRedacteurEmpty()
        {
            return NameRedacteur == null || string.IsNullOrWhiteSpace(NameRedacteur);
        }

        private string _NameRedacteur;

        public string NameRedacteur
        {
            get
            {
                return _NameRedacteur;
            }
            set
            {
                _NameRedacteur = value;
                RaisePropertyChanged();
            }
        }
        private IMvxCommand _SaveRedacteurChange;

        public IMvxCommand SaveRedacteurChange
        {
            get
            {
                _SaveRedacteurChange = new MvxCommand(SaveEditRedacteurChange);
                return _SaveRedacteurChange;
            }
        }
        public bool IsEditFieldsRedacteurEmpty()
        {
            return NovNameConcepteur == null || string.IsNullOrWhiteSpace(NovNameConcepteur);
        }
        public void SaveEditRedacteurChange()
        {
            if (!IsEditFieldsRedacteurEmpty() && db2.GetRedacteur(NovNameRedacteur) == null)
            {
                var NewRed = new Redacteur();
                NewRed.Name = NovNameRedacteur.ToUpper();
                NewRed.ID = EditRedacteurId;
                db2.EditRedacteur(NewRed);
                UpdateRedacteurList();
                CancelEditConcepteur();
            }
            else if (IsEditFieldsRedacteurEmpty())
            {
                SendNotification.Raise("le champ nom prénom est vide");
            }

            else
            {
                SendNotification.Raise("Rédacteur existe déja");
            }
        }
        public void CancelEditRedacteur()
        {
            IsEditEnabledRedacteur = false;
            NovNameRedacteur = "";
        }
        public void AjouterNoveauRedacteur()
        {
            if (!IsAddFieldsRedacteurEmpty() && db2.GetRedacteur(NameRedacteur) == null)
            {
                var NewRed = new Redacteur();
                NewRed.Name = NameRedacteur.ToUpper();
                db2.AddNewRedacteur(NewRed);
                UpdateRedacteurList();
                NameRedacteur = "";
            }
            else if (IsAddFieldsRedacteurEmpty())
            {
                SendNotification.Raise("le nom prénom est vide");
            }

            else
            {
                SendNotification.Raise("Rédacteur existe déja");
            }
        }
        private IMvxCommand _ModifierRedacteur;

        public IMvxCommand ModifierRedacteur
        {
            get
            {
                _ModifierRedacteur = new MvxCommand(ModifierRedacteurSelected);
                return _ModifierRedacteur;
            }
        }
        private IMvxCommand _SupprimerRedacteur;

        public IMvxCommand SupprimerRedacteur
        {
            get
            {
                _SupprimerRedacteur = new MvxCommand(SupprimerRedacteurSelected);
                return _SupprimerRedacteur;
            }
        }
        public void SupprimerRedacteurSelected()
        {
            if (SelectedRedacteur != null)
            {
                var req = new YesNoQuestion
                {
                    Question = "êtes-vous sûr de vouloir supprimer ce Rédacteur séléctionnée ?",
                    UploadCallback = ok =>
                    {
                        if (ok)
                        {
                            db2.DeleteRedacteur(SelectedRedacteur);
                            UpdateRedacteurList();
                        }
                    }
                };
                ConfirmAction.Raise(req);
            }
            else
            {
                SendNotification.Raise("S.V.P Séléctionnez une Rédacteur");
            }
        }
        private IMvxCommand _CancelRedacteurCmd;

        public IMvxCommand CancelRedacteurCmd
        {
            get
            {
                _CancelRedacteurCmd = new MvxCommand(CancelEditRedacteur);
                return _CancelRedacteurCmd;
            }
        }
        public async Task UpdateRedacteurList()
        {
            await  Task.Run(() =>
                {
                    var ListVer = db2.GetRedacteurs();
                    foreach (var col in ListVer) col.Name = col.Name.ToUpper();
                    ListRedacteur = new MvxObservableCollection<Redacteur>(ListVer);
                }
            );
      
           
        }
        private string _NovNameRedacteur;

        public string NovNameRedacteur
        {
            get
            {
                return _NovNameRedacteur;
            }
            set
            {
                _NovNameRedacteur = value;
                RaisePropertyChanged();
            }
        }
        public void ModifierRedacteurSelected()
        {
            if (SelectedRedacteur != null)
            {
                IsEditEnabledRedacteur = true;
                NovNameRedacteur = SelectedRedacteur.Name;
                EditRedacteurId = SelectedRedacteur.ID;
            }
            else
            {
                SendNotification.Raise("S.V.P Séléctionnez un Rédacteur");
            }
        }

        public int _EditRedacteurId;
        public int EditRedacteurId
        {
            get => _EditRedacteurId;
            set
            {
                _EditRedacteurId = value;
                RaisePropertyChanged();
            }
        }
        private bool _IsEditEnabledRedacteur;
        public bool IsEditEnabledRedacteur
        {
            get => _IsEditEnabledRedacteur;
            set
            {
                _IsEditEnabledRedacteur = value;
                RaisePropertyChanged();
            }
        }
        
        private Redacteur _SelectedRedacteur;
        public Redacteur SelectedRedacteur
        {
            get => _SelectedRedacteur;
            set
            {
                _SelectedRedacteur = value;
                RaisePropertyChanged();
            }
        }
        
        private MvxObservableCollection<Redacteur> _ListRedacteur;
        public MvxObservableCollection<Redacteur> ListRedacteur
        {
            get => _ListRedacteur;
            set
            {
                _ListRedacteur = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}