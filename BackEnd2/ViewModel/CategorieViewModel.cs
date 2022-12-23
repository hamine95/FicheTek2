using System;
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
using Convert = System.Convert;

namespace BackEnd2.ViewModel
{
    public class CategorieViewModel:MvxViewModel<user>
    {
        
        private string _Designation;

    private bool _IsEditEnabled;
    private MvxObservableCollection<Catalogue> _ListCategorie;


    private IMvxNavigationService _navigationService;

    private string _NovDesignation;

    private Catalogue _SelectedCategorie;
    private MyDBContext db;

    public CategorieViewModel(IMvxNavigationService _navSer)
    {
        _navigationService = _navSer;
      
       

    }


    private user UserSession;

    private SqliteData _db2;
    public override void Prepare(user parameter)
    {
        db = Mvx.IoCProvider.Resolve<MyDBContext>();
        _db2 = Mvx.IoCProvider.Resolve<SqliteData>();
        UserSession = parameter;

        if (UserSession.type == user.UserType.verificateur)
        {
            IsVerificateur = true;
        }
        else
        {
            IsVerificateur = false; 
        }
    }

    private MvxNotifyTask _LoadTask;
    public MvxNotifyTask LoadTask 
    {
        get => _LoadTask;
        private set => SetProperty(ref _LoadTask, value);
    }

    
        public override  Task Initialize()
    {
        LoadTask = MvxNotifyTask.Create(UpdateCategorieList);
        return base.Initialize();
      
        
        



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

    private IMvxCommand _Modifier;

    public IMvxCommand Modifier
    {
        get
        {
            _Modifier = new MvxCommand(ModifierCategorie);
            return _Modifier;
        }

    }

    private IMvxCommand _Supprimer;

    public IMvxCommand Supprimer
    {
        get
        {
            _Supprimer = new MvxCommand(SupprimerCategorie);
            return _Supprimer;
        }
    }

    private IMvxCommand _AjouterNovCategorie;

    public IMvxCommand AjouterNovCategorie
    {
        get
        {
            _AjouterNovCategorie = new MvxAsyncCommand(async() =>
            {
                await AjouterNoveauCategorie();
            });
       
            return _AjouterNovCategorie;
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

    private IMvxCommand _SaveChange;

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
    private int _EditCatId;
    public int EditCatId
    {
        get => _EditCatId;
        set
        {
            _EditCatId = value;
            RaisePropertyChanged();
        }
    }
    public void SaveEditChange()
    {
        if (!IsEditFieldsEmpty() &&
            db.GetCategorie(NovDesignation) == null)
        {
            var NewCategorie = new Catalogue();
            NewCategorie.ID = EditCatId;
            NewCategorie.Designation = NovDesignation;
            db.EditCategorie(NewCategorie);
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
                        db.DeleteCategorie(SelectedCategorie);
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

    
    private Catalogue _SelectedParent;

    public Catalogue SelectedParent
    {
        get
        {
            return _SelectedParent;
        }
        set
        {
            _SelectedParent = value;
            RaisePropertyChanged();
        }
    }
    private Catalogue _SelectedParentEdit;

    public Catalogue SelectedParentEdit
    {
        get
        {
            return _SelectedParentEdit;
        }
        set
        {
            _SelectedParentEdit = value;
            RaisePropertyChanged();
        }
    }

    private bool _IsSous;
    private bool _IsSousEdit;

    public bool IsSousEdit
    {
        get
        {
            return _IsSousEdit;
        }
        set
        {
            _IsSousEdit = value;
            if (value == false)
            {
                SelectedParentEdit = null;
            }
            RaisePropertyChanged();
        }
    }
    public bool IsSous
    {
        get
        {
            return _IsSous;
        }
        set
        {
            _IsSous = value;
            if (value == false)
            {
                SelectedParent = null;
            }
            RaisePropertyChanged();
        }
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
    
    public async Task AjouterNoveauCategorie()
    {
        await Task.Run(async() =>
            {
               
                if (!IsAddFieldsEmpty()  && db.GetCategorie( Designation) == null)
                {
                    var NewCat = new Catalogue();
                    NewCat.Designation = Designation;
                    if (SelectedCategorie != null)
                    {
                        NewCat.parent = SelectedParent.ID;
                    }
                    else
                    {
                        NewCat.parent = -1;
                    }
                    db.AddNewCategorie(NewCat);
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