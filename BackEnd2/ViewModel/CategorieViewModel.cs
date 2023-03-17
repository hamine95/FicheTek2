using System.Linq;
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
    public class CategorieViewModel : MvxViewModel<user>
    {
        private IMvxCommand _AjouterNovCategorie;

        private IMvxCommand _CancelCmd;

        private SqliteData _db2;

        private string _Designation;
        private int _EditCatId;

        private bool _IsEditEnabled;

        private bool _IsSous;
        private bool _IsSousEdit;

        private bool _IsSuperUser;

        private bool _IsVerificateur;
        private MvxObservableCollection<Catalogue> _ListCategorie;

        private MvxNotifyTask _LoadTask;

        private IMvxCommand _Modifier;


        private IMvxNavigationService _navigationService;

        private string _NovDesignation;

        private IMvxCommand _SaveChange;

        private Catalogue _SelectedCategorie;


        private Catalogue _SelectedParent;
        private Catalogue _SelectedParentEdit;

        private IMvxCommand _Supprimer;

        private user UserSession;

        public CategorieViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
        }

        public MvxNotifyTask LoadTask
        {
            get => _LoadTask;
            private set => SetProperty(ref _LoadTask, value);
        }

        public MvxInteraction<YesNoQuestion> ConfirmAction { get; } = new MvxInteraction<YesNoQuestion>();

        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();


        public bool IsEditEnabled
        {
            get => _IsEditEnabled;
            set
            {
                _IsEditEnabled = value;
                RaisePropertyChanged();
            }
        }

        public IMvxCommand Modifier
        {
            get
            {
                _Modifier = new MvxCommand(ModifierCategorie);
                return _Modifier;
            }
        }

        public IMvxCommand Supprimer
        {
            get
            {
                _Supprimer = new MvxCommand(SupprimerCategorie);
                return _Supprimer;
            }
        }

        public IMvxCommand AjouterNovCategorie
        {
            get
            {
                _AjouterNovCategorie = new MvxAsyncCommand(async () => { await AjouterNoveauCategorie(); });

                return _AjouterNovCategorie;
            }
        }

        public IMvxCommand CancelCmd
        {
            get
            {
                _CancelCmd = new MvxCommand(CancelEdit);
                return _CancelCmd;
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

        public MvxObservableCollection<Catalogue> ListCategorie
        {
            get => _ListCategorie;
            set
            {
                _ListCategorie = value;
                RaisePropertyChanged();
            }
        }

        public Catalogue SelectedCategorie
        {
            get => _SelectedCategorie;
            set
            {
                _SelectedCategorie = value;
                RaisePropertyChanged();
            }
        }


        public string Designation
        {
            get => _Designation;
            set
            {
                _Designation = value;
                RaisePropertyChanged();
            }
        }


        public string NovDesignation
        {
            get => _NovDesignation;
            set
            {
                _NovDesignation = value;
                RaisePropertyChanged();
            }
        }

        public int EditCatId
        {
            get => _EditCatId;
            set
            {
                _EditCatId = value;
                RaisePropertyChanged();
            }
        }

        public Catalogue SelectedParent
        {
            get => _SelectedParent;
            set
            {
                _SelectedParent = value;
                RaisePropertyChanged();
            }
        }

        public Catalogue SelectedParentEdit
        {
            get => _SelectedParentEdit;
            set
            {
                _SelectedParentEdit = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSousEdit
        {
            get => _IsSousEdit;
            set
            {
                _IsSousEdit = value;
                if (value == false) SelectedParentEdit = null;
                RaisePropertyChanged();
            }
        }

        public bool IsSous
        {
            get => _IsSous;
            set
            {
                _IsSous = value;
                if (value == false) SelectedParent = null;
                RaisePropertyChanged();
            }
        }

        public bool IsVerificateur
        {
            get => _IsVerificateur;
            set
            {
                _IsVerificateur = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSuperUser
        {
            get => _IsSuperUser;
            set
            {
                _IsSuperUser = value;
                RaisePropertyChanged();
            }
        }

        public override void Prepare(user parameter)
        {
            
            _db2 = Mvx.IoCProvider.Resolve<SqliteData>();
            UserSession = parameter;

            if (UserSession.type == user.UserType.verificateur)
                IsVerificateur = true;
            else
                IsVerificateur = false;
        }


        public override Task Initialize()
        {
            LoadTask = MvxNotifyTask.Create(UpdateCategorieList);
            return base.Initialize();
        }

        public void SaveEditChange()
        {
            if (!IsEditFieldsEmpty() &&
                _db2.GetCategorie(NovDesignation) == null)
            {
                var NewCategorie = new Catalogue();
                NewCategorie.ID = EditCatId;
                NewCategorie.Designation = NovDesignation;
                _db2.EditCategorie(NewCategorie);
                UpdateCategorieList();
                CancelEdit();
            }
            else if (IsEditFieldsEmpty())
            {
                SendNotification.Raise("Remplit tout les champs");
            }

            else
            {
                SendNotification.Raise("Categorie existe déja");
            }
        }

        public async Task UpdateCategorieList()
        {
            var listCat = _db2.GetRootCategories();
            foreach (var cat in listCat)
            {
                cat.Designation = cat.Designation.ToUpper();
                cat.Children = _db2.GetCategorieChildren(cat.ID);
            }


            ListCategorie = new MvxObservableCollection<Catalogue>(listCat.ToList());
        }

        public void CancelEdit()
        {
            IsEditEnabled = false;
            NovDesignation = "";
        }

        public void ModifierCategorie()
        {
            if (SelectedCategorie != null)
            {
                IsEditEnabled = true;
                NovDesignation = SelectedCategorie.Designation;
                EditCatId = SelectedCategorie.ID;
                if (SelectedCategorie.parent != -1)
                {
                    IsSousEdit = true;
                    SelectedParentEdit = ListCategorie.SingleOrDefault(cat => cat.ID == SelectedCategorie.parent);
                }
            }
            else
            {
                SendNotification.Raise("Aucune Categorie séléctionnée");
            }
        }

        public void SupprimerCategorie()
        {
            if (SelectedCategorie != null)
            {
                var req = new YesNoQuestion
                {
                    Question = "êtes-vous sûr de vouloir supprimer cette Categorie séléctionnée ?",
                    UploadCallback = ok =>
                    {
                        if (ok)
                        {
                            _db2.DeleteCategorie(SelectedCategorie);
                            UpdateCategorieList();
                        }
                    }
                };
                ConfirmAction.Raise(req);
            }
            else
            {
                SendNotification.Raise("Aucune Categorie séléctionnée");
            }
        }

        public async Task AjouterNoveauCategorie()
        {
            await Task.Run(async () =>
                {
                    if (!IsAddFieldsEmpty() && _db2.GetCategorie(Designation) == null)
                    {
                        var NewCat = new Catalogue();
                        NewCat.Designation = Designation;
                        if (SelectedParent != null)
                            NewCat.parent = SelectedParent.ID;
                        else
                            NewCat.parent = -1;
                        _db2.AddNewCategorie(NewCat);
                        await UpdateCategorieList();
                        Designation = "";
                    }
                    else if (IsAddFieldsEmpty())
                    {
                        SendNotification.Raise("Remplit tout les champs");
                    }

                    else
                    {
                        SendNotification.Raise("Categorie existe déja");
                    }
                }
            );
        }


        public bool IsAddFieldsEmpty()
        {
            return Designation == null ||
                   string.IsNullOrWhiteSpace(Designation);
        }

        public bool IsEditFieldsEmpty()
        {
            return NovDesignation == null ||
                   string.IsNullOrWhiteSpace(NovDesignation);
        }
    }
}