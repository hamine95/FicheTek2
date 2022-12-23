using System;
using System.Linq;
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
    public class ComposantViewModel : MvxViewModel<user>
    {
        private string _CompDesignation;

        private user UserSession;
        private int _EditComposantId;
        private bool _IsEditMatiereEnabled;
        private MvxObservableCollection<Composant> _ListComposant;


        private string _NovCompDesignation;

        private Composant _SelectedComposant;
        private  MyDBContext db;

        public ComposantViewModel(IMvxNavigationService _navServ)
        {
            _navigationService = _navServ;
           
            
        }

      

      

        public int EditComposantId
        {
            get => _EditComposantId;
            set
            {
                _EditComposantId = value;
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
                _Modifier = new MvxCommand(ModifierComposant);
                return _Modifier;
            }
      }

        private IMvxCommand _Supprimer;
        public IMvxCommand Supprimer
        {
            get
            {
                _Supprimer = new MvxCommand(SupprimerComposant);
                return _Supprimer;
            }
        }

        private IMvxCommand _AjouterNovComposant;
        public IMvxCommand AjouterNovComposant
        {
            get
            {
                _AjouterNovComposant = new MvxCommand(AjouterNoveauComposant);
                return _AjouterNovComposant;
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

        public MvxObservableCollection<Composant> ListComposant
        {
            get => _ListComposant;
            set
            {
                _ListComposant = value;
                RaisePropertyChanged();
            }
        }

        public Composant SelectedComposant
        {
            get => _SelectedComposant;
            set
            {
                _SelectedComposant = value;
                RaisePropertyChanged();
            }
        }

        public string CompDesignation
        {
            get => _CompDesignation;
            set
            {
                _CompDesignation = value;
                RaisePropertyChanged();
            }
        }

        public string NovCompDesignation
        {
            get => _NovCompDesignation;
            set
            {
                _NovCompDesignation = value;
                RaisePropertyChanged();
            }
        }

        public void SaveEditChange()
        {
            if (!IsEditFieldsEmpty() && db.GetComposant(NovCompDesignation) == null)
            {
                var NewComposant = new Composant();
                NewComposant.ID = EditComposantId;
                NewComposant.Name = NovCompDesignation;
                db.EditComposant(NewComposant);
                UpdateComposantList();
                CancelEdit();
            }
            else if (IsEditFieldsEmpty())
            {
                SendNotification.Raise("Designation est requis");
            }
            else
            {
                SendNotification.Raise("Composant existe déja");
            }
        }

        public async Task UpdateComposantList()
        {
            await Task.Run(() =>
                {
                    var listCompo = db.GetComposants();
                    foreach (var comp in listCompo) comp.Name = comp.Name.ToUpper();
                    ListComposant = new MvxObservableCollection<Composant>(listCompo);
                }
            );

        }

        public void CancelEdit()
        {
            IsEditEnabled = false;
            NovCompDesignation = "";
        }

        public void ModifierComposant()
        {
            if (SelectedComposant != null)
            {
                IsEditEnabled = true;
                NovCompDesignation = SelectedComposant.Name;
                EditComposantId = SelectedComposant.ID;
            }
            else
            {
                SendNotification.Raise("S.V.P Séléctionnez un composant");
            }
        }

        public void SupprimerComposant()
        {
            if (SelectedComposant != null)
            {
                var req = new YesNoQuestion
                {
                    Question = "êtes-vous sûr de vouloir supprimer ce composant séléctionnée ?",
                    UploadCallback = ok =>
                    {
                        if (ok)
                        {
                            db.DeleteComposant(SelectedComposant);
                            UpdateComposantList();
                        }
                    }
                };
                ConfirmAction.Raise(req);
            }
            else
            {
                SendNotification.Raise("S.V.P Séléctionnez une couleur");
            }
        }

        public void AjouterNoveauComposant()
        {
            if (!IsAddFieldsEmpty() && db.GetComposant(CompDesignation) == null)
            {
                var NewComposant = new Composant();
                NewComposant.Name = CompDesignation;
                db.AddNewComposant(NewComposant);
                UpdateComposantList();
                CompDesignation = "";
            }
            else if (IsAddFieldsEmpty())
            {
                SendNotification.Raise("Designation est requis");
            }

            else
            {
                SendNotification.Raise("Designation existe déja");
            }
        }

        public bool IsAddFieldsEmpty()
        {
            return CompDesignation == null || string.IsNullOrWhiteSpace(CompDesignation);
        }

        public bool IsEditFieldsEmpty()
        {
            return NovCompDesignation == null || string.IsNullOrWhiteSpace(NovCompDesignation);
        }

        public override void Prepare(user parameter)
        {
            db = Mvx.IoCProvider.Resolve<MyDBContext>();
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


        #region Matiere

        
private bool _AllColor = true;
        private string _CodificationTypM;

        private string _Designation;
        private string _EditDesignation;

        private MvxObservableCollection<Couleur> _EditListeCouleur;
        private Matiere _EditMatiere = new Matiere();
        private string _EditRef;

        private bool _EnableColorSelection;
        private bool _EnableColorSelectionEdit;

        private bool _EnableRetordage;
        private bool _IsEditEnabled;

        private bool _IsRetordu;

        private MvxObservableCollection<Couleur> _ListeCouleur;


        private MvxObservableCollection<Matiere> _ListMatiere;
        private MvxObservableCollection<Titrage> _ListTitrage;
        private MvxObservableCollection<Titrage> _ListTitrageEdit;

        private IMvxNavigationService _navigationService;
        private string _NomMatiere;


        private string _NumMetric;


        private string _NumTwist;
        private string _Ref;
        private Couleur _SelectedCouleur;


        private Couleur _SelectedCouleurEdit;
        private Titrage _selectedEditTitrage;

        private Matiere _SelectedMatiere;
        private Titrage _SelectedTitrage;


        private TypeMatiere _SelectedTypeMatiere;


        private TypeMatiere _SelectedTypeMatiereEdit;
        private TypeMatiere _SelectedTypeMatiereT;
        private MvxObservableCollection<TypeMatiere> _TypeMatiereList;
        private int SelectedMatiereEdit;

       

        public override Task Initialize()
        {
       
            LoadLists=MvxNotifyTask.Create( RefreshLists);
        
            return base.Initialize();
        }

        public async Task RefreshLists()
        {
            await UpdateListeMatiere();
            await UpdateTypeMatiereList();
            await UpdateCouleur();
            await UpdateColorList();
            await UpdateComposantList();
        }

       
        
        private MvxNotifyTask _LoadLists;

        public MvxNotifyTask LoadLists
        {
            get => _LoadLists;
            set => SetProperty(ref _LoadLists, value);

        }
        public MvxInteraction<YesNoQuestion> ConfirmAction { get; } = new MvxInteraction<YesNoQuestion>();

        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();

        public string NumMetric
        {
            get => _NumMetric;
            set
            {
                _NumMetric = value;
                RaisePropertyChanged();
            }
        }

        public string NumTwist
        {
            get => _NumTwist;
            set
            {
                _NumTwist = value;
                RaisePropertyChanged();
            }
        }

        public bool IsRetordu
        {
            get => _IsRetordu;
            set
            {
                _IsRetordu = value;
                RaisePropertyChanged();
            }
        }

        public bool EnableRetordage
        {
            get => _EnableRetordage;
            set
            {
                _EnableRetordage = value;
                RaisePropertyChanged();
                if (value) EnablingRetordageField();
            }
        }

        public string EditRef
        {
            get => _EditRef;
            set
            {
                _EditRef = value;
                RaisePropertyChanged();
            }
        }

        public Titrage selectedEditTitrage
        {
            get => _selectedEditTitrage;
            set
            {
                _selectedEditTitrage = value;
                RaisePropertyChanged();
                OnEditChangeNovFields();
            }
        }

        public string EditDesignation
        {
            get => _EditDesignation;
            set
            {
                _EditDesignation = value;
                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiereEdit
        {
            get => _SelectedTypeMatiereEdit;
            set
            {
                _SelectedTypeMatiereEdit = value;
                RaisePropertyChanged();
                
                if (value != null)
                {
                    OnEditChangeNovFields();
                    UpdateTitrageEdit(SelectedTypeMatiereEdit);
                }
                
            }
        }

        public MvxObservableCollection<TypeMatiere> TypeMatiereList
        {
            get => _TypeMatiereList;
            set
            {
                _TypeMatiereList = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Titrage> ListTitrage
        {
            get => _ListTitrage;
            set
            {
                _ListTitrage = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Titrage> ListTitrageEdit
        {
            get => _ListTitrageEdit;
            set
            {
                _ListTitrageEdit = value;
                RaisePropertyChanged();
            }
        }

        public string CodificationTypM
        {
            get => _CodificationTypM;
            set
            {
                _CodificationTypM = value;
                RaisePropertyChanged();
            }
        }

        public string Ref
        {
            get => _Ref;
            set
            {
                _Ref = value;
                RaiseAllPropertiesChanged();
            }
        }

        public Titrage SelectedTitrage
        {
            get => _SelectedTitrage;
            set
            {
                _SelectedTitrage = value;
                RaiseAllPropertiesChanged();
                OnChangeNovFields();
            }
        }

        public string Designation
        {
            get => _Designation;
            set
            {
                _Designation = value;
                RaiseAllPropertiesChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiereT
        {
            get => _SelectedTypeMatiereT;
            set
            {
                _SelectedTypeMatiereT = value;
                RaiseAllPropertiesChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere
        {
            get => _SelectedTypeMatiere;
            set
            {
                _SelectedTypeMatiere = value;
                RaisePropertyChanged();
                
                if (value != null)
                {
                    OnChangeNovFields();
                    UpdateTitrage(SelectedTypeMatiere);
                }
                
            }
        }

        public Matiere EditMatiere
        {
            get => _EditMatiere;
            set
            {
                _EditMatiere = value;
                RaisePropertyChanged();
            }
        }

        public bool IsEditMatiereEnabled
        {
            get => _IsEditMatiereEnabled;
            set
            {
                _IsEditMatiereEnabled = value;
                RaisePropertyChanged();
            }
        }

        public bool EnableColorSelection
        {
            get => _EnableColorSelection;
            set
            {
                _EnableColorSelection = value;
                RaisePropertyChanged();
            }
        }

        public bool EnableColorSelectionEdit
        {
            get => _EnableColorSelectionEdit;
            set
            {
                _EnableColorSelectionEdit = value;
                RaisePropertyChanged();
            }
        }

        public Couleur SelectedCouleurEdit
        {
            get => _SelectedCouleurEdit;
            set
            {
                _SelectedCouleurEdit = value;
                RaisePropertyChanged();
                OnEditChangeNovFields();
            }
        }

        public Couleur SelectedCouleur
        {
            get => _SelectedCouleur;
            set
            {
                _SelectedCouleur = value;
                RaisePropertyChanged();
                OnChangeNovFields();
            }
        }

        public MvxObservableCollection<Couleur> EditListeCouleur
        {
            get => _EditListeCouleur;
            set
            {
                _EditListeCouleur = value;
                RaisePropertyChanged();
            }
        }

        public bool AllColor
        {
            get => _AllColor;
            set
            {
                _AllColor = value;
                RaisePropertyChanged();
                if (AllColor)
                {
                    EnableColorSelection = false;
                    SelectedCouleur = null;
                }
                else
                {
                    EnableColorSelection = true;
                }
            }
        }

        public MvxObservableCollection<Couleur> ListeCouleur
        {
            get => _ListeCouleur;
            set
            {
                _ListeCouleur = value;
                RaisePropertyChanged();
            }
        }

        public string NomMatiere
        {
            get => _NomMatiere;
            set
            {
                _NomMatiere = value;
                RaisePropertyChanged();
            }
        }

        private IMvxCommand _AjouterNovTitrage;

        public IMvxCommand AjouterNovTitrage
        {
            get
            {
                _AjouterNovTitrage = new MvxCommand(AjouterNouveauTitrage);
                return _AjouterNovTitrage;
            }
        }

        private IMvxCommand _AjouterNovTypeMatiere;

        public IMvxCommand AjouterNovTypeMatiere
        {
            get
            {
                _AjouterNovTypeMatiere = new MvxCommand(AjouterNoveauTypeMatiere);
                return _AjouterNovTypeMatiere;
            }
        }

        private IMvxCommand _saveMatiereChange;

        public IMvxCommand SaveMatiereChange
        {
            get
            {
                _saveMatiereChange = new MvxCommand(SaveMatiereEditChange);
                return _saveMatiereChange;
            }
        }

        private IMvxCommand _CancelMatiereCmd;

        public IMvxCommand CancelMatiereCmd
        {
            get
            {
                _CancelMatiereCmd = new MvxCommand(CancelEditing);
                return _CancelMatiereCmd;
            }
        }

        private IMvxCommand _AjouterNovMatiere;

        public IMvxCommand AjouterNovMatiere
        {
            get
            {
                _AjouterNovMatiere = new MvxCommand(AjouterNouveauMatiere);
                return _AjouterNovMatiere;
            }
        }

        private IMvxCommand _ModifierMatiereCmd;

        public IMvxCommand ModifierMatiereCmd
        {
            get
            {
                _ModifierMatiereCmd = new MvxCommand(ModifierMatiere);
                return _Modifier;
            }
        }

        private IMvxCommand _SupprimerMatiereCmd;

        public IMvxCommand SupprimerMatiereCmd
        {
            get
            {
                _SupprimerMatiereCmd = new MvxCommand(SupprimerMatiere);
                return _Supprimer;
            }
        }

        public Matiere SelectedMatiere
        {
            get => _SelectedMatiere;
            set
            {
                _SelectedMatiere = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Matiere> ListMatiere
        {
            get => _ListMatiere;
            set
            {
                _ListMatiere = value;
                RaisePropertyChanged();
            }
        }

        public void EnablingRetordageField()
        {
            if (EnableRetordage) IsRetordu = true;
        }


        public void AjouterNouveauTitrage()
        {
            if (EnableRetordage)
            {
                if (SelectedTypeMatiereT != null && NumTwist != null && !string.IsNullOrWhiteSpace(NumTwist) &&
                    NumMetric != null && !string.IsNullOrWhiteSpace(NumMetric))
                {
                    var NewTitrage = new Titrage();
                    int NTwist;
                    if (int.TryParse(NumTwist, out NTwist))
                    {
                       
                            NewTitrage.NumTwist = NTwist;
                            NewTitrage.NumMetric = NumMetric;
                            NewTitrage.TypeMatiere = SelectedTypeMatiereT;
                            NewTitrage.Designation = NumTwist + "/" + NumMetric;
                            db.AddNewTitrage(NewTitrage);
                            UpdateTypeMatiereList();
                            SelectedTypeMatiere = null;
                            SelectedTypeMatiereEdit = null;
                            
                            SelectedTypeMatiereT = null;
                            NumMetric = "";
                            NumTwist = "";

                    }
                    else
                    {
                        SendNotification.Raise("Valeur Incorrect Retoradage");
                    }
                }
            }
            else
            {
                var NewTitrage = new Titrage();
                int NMetric;
                
                    NewTitrage.NumTwist = 0;
                    NewTitrage.NumMetric = NumMetric;
                    NewTitrage.TypeMatiere = SelectedTypeMatiereT;
                    NewTitrage.Designation = NumMetric;
                    db.AddNewTitrage(NewTitrage);
                    UpdateTypeMatiereList();
                    SelectedTypeMatiere = null;
                    SelectedTypeMatiereEdit = null;
                    SelectedTypeMatiereT = null;
                    NumMetric = "";
                
            }
        }

        public void SupprimerMatiere()
        {
            if (SelectedMatiere != null)
            {
                var req = new YesNoQuestion
                {
                    Question = "êtes-vous sûr de vouloir supprimer cette matière première séléctionnée ?",
                    UploadCallback = ok =>
                    {
                        if (ok)
                        {
                            db.DeleteMatiere(SelectedMatiere);
                            UpdateListeMatiere();
                        }
                    }
                };
                ConfirmAction.Raise(req);
            }
            else
            {
                SendNotification.Raise("S.V.P séléctionnez une Matiere");
            }
        }

        public void SaveMatiereEditChange()
        {
            if (!IsEditFieldsEmpty2())
            {
                if (db.GetMatiere(EditRef) == null)
                {
                    var mMatiere = new Matiere();
                    mMatiere.ID = SelectedMatiereEdit;
                    mMatiere.Ref = EditRef;
                    mMatiere.Designation = EditDesignation;
                    mMatiere.Titrage = selectedEditTitrage;
                    mMatiere.GetCouleur = SelectedCouleurEdit;
                    db.EditMatiere(mMatiere);
                    UpdateListeMatiere();
                    CancelEditing();
                }
                else
                {
                    SendNotification.Raise("Cette matière Première existe déja");
                }
            }
            else
            {
                SendNotification.Raise("S.V.P remplit tous les champs");
            }
        }

        public void CancelEditing()
        {
            IsEditMatiereEnabled = false;
            EditRef = "";
            selectedEditTitrage = null;
            SelectedTypeMatiereEdit = null;
            EditDesignation = "";
            EnableColorSelectionEdit = false;
            SelectedCouleurEdit = null;
        }

        public void ModifierMatiere()
        {
            if (SelectedMatiere != null)
            {
                IsEditMatiereEnabled = true;
                EditRef = SelectedMatiere.Ref;
                SelectedTypeMatiereEdit = TypeMatiereList.First(m => m.ID == SelectedMatiere.Titrage.TypeMatiere.ID);
                UpdateTitrageEdit(SelectedTypeMatiereEdit);
                selectedEditTitrage = ListTitrage.First(t => t.ID == SelectedMatiere.Titrage.ID);

                EditDesignation = SelectedMatiere.Designation;
                EnableColorSelectionEdit = true;
                SelectedCouleurEdit = ListeCouleur.First(co => co.ID == SelectedMatiere.GetCouleur.ID);
                SelectedMatiereEdit = SelectedMatiere.ID;
            }
            else
            {
                SendNotification.Raise("S.V.P séléctionnez une Matiere");
            }
        }

        public void UpdateTitrage(TypeMatiere tm)
        {
            ListTitrage = new MvxObservableCollection<Titrage>(db.GetTitrageByTypMat(tm));
        }

        public void UpdateTitrageEdit(TypeMatiere tm)
        {
            ListTitrageEdit = new MvxObservableCollection<Titrage>(db.GetTitrageByTypMat(tm));
        }

        public void AjouterNoveauTypeMatiere()
        {
            if (NomMatiere != null && !string.IsNullOrWhiteSpace(NomMatiere) && CodificationTypM != null &&
                !string.IsNullOrWhiteSpace(CodificationTypM))
            {
                if (db.GetTypeMatiereNom(NomMatiere) == null)
                {
                    var typeM = new TypeMatiere();
                    typeM.MatiereNom = NomMatiere;
                    typeM.Codification = CodificationTypM;
                    db.AddNewTypeMatiere(typeM);
                    UpdateTypeMatiereList();
                    NomMatiere = "";
                    CodificationTypM = "";
                }
                else
                {
                    SendNotification.Raise("Type Matière existe déja");
                }
               
            }
            else
            {
                SendNotification.Raise("S.V.P remplit le champ");
            }
        }

        public async Task UpdateTypeMatiereList()
        {
          await  Task.Run(() =>
            {
                TypeMatiereList = new MvxObservableCollection<TypeMatiere>(db.GetTypeMatieres());
            });

        }


        public bool IsMatiereEditFieldsEmpty()
        {
            if (AllColor == false)
            {
                var bo = SelectedCouleur == null || SelectedTitrage == null;
                return bo;
            }
            else
            {
                var bo = SelectedTitrage == null;
                return bo;
            }
        }

        public bool IsEditFieldsEmpty2()
        {
            var bo = SelectedCouleurEdit == null || selectedEditTitrage == null;
            return bo;
        }

        public void AjouterNouveauMatiere()
        {
            if (!IsMatiereEditFieldsEmpty())
            {
                if (AllColor == false)
                {
                    if (db.GetMatiere(Ref) == null)
                    {
                        var mMatiere = new Matiere();
                        mMatiere.Ref = Ref;
                        mMatiere.Designation = Designation;
                        mMatiere.Titrage = SelectedTitrage;
                        mMatiere.GetCouleur = SelectedCouleur;
                        db.AddNewMatiere(mMatiere);
                        UpdateListeMatiere();

                        Ref = "";
                        Designation = "";
                        
                        SelectedTitrage = null;
                        SelectedCouleur = null;
                        SelectedCouleur = null;
                    }
                    else
                    {
                        SendNotification.Raise("Cette matière Première existe déja");
                    }
                }
                else
                {
                    if (ListeCouleur.Count == 0)
                    {
                        SendNotification.Raise("Aucune couleur existe");
                    }
                    else
                    {
                        var AddedNewMatiere = false;
                        foreach (var colr in ListeCouleur)
                        {
                            var mMatiere = new Matiere();
                            mMatiere.Ref = Ref + colr.Nbr.ToString("00");
                            if (db.GetMatiere(mMatiere.Ref) == null)
                            {
                                AddedNewMatiere = true;
                                mMatiere.Designation = Designation + " " + colr.Name;
                                mMatiere.Titrage = SelectedTitrage;
                                mMatiere.GetCouleur = colr;
                                db.AddNewMatiere(mMatiere);
                            }
                        }

                        SelectedTitrage = null;
                        SelectedCouleur = null;
                        if (AddedNewMatiere == false) SendNotification.Raise("Tous les matières existe déja");
                        UpdateListeMatiere();
                    }
                }
            }
            else
            {
                SendNotification.Raise("S.V.P remplit tous les champs");
            }
        }

        public void OnEditChangeNovFields()
        {
            if (SelectedCouleurEdit != null && selectedEditTitrage != null)
            {
                if (SelectedTitrage.NumTwist > 0)
                {
                    EditRef = SelectedTypeMatiereEdit.Codification.ToUpper() + selectedEditTitrage.NumTwist + "/" +
                              selectedEditTitrage.NumMetric + SelectedCouleurEdit.Nbr.ToString("00");
                    EditDesignation = SelectedTypeMatiereEdit.MatiereNom.ToUpper() + " " +
                                      selectedEditTitrage.NumTwist + "/" + selectedEditTitrage.NumMetric + " " +
                                      SelectedCouleurEdit.Name.ToUpper();
                }
                else
                {
                    EditRef = SelectedTypeMatiereEdit.Codification.ToUpper() + selectedEditTitrage.NumMetric +
                              SelectedCouleurEdit.Nbr.ToString("00");
                    EditDesignation = SelectedTypeMatiereEdit.MatiereNom.ToUpper() + " " +
                                      selectedEditTitrage.NumMetric + " " + SelectedCouleurEdit.Name.ToUpper();
                }
            }
        }

        public void OnChangeNovFields()
        {
            if (SelectedTitrage != null)
            {
                if (SelectedCouleur != null && AllColor == false)
                {
                    if (SelectedTitrage.NumTwist > 0)
                    {
                        Ref = SelectedTypeMatiere.Codification.ToUpper() + SelectedTitrage.NumTwist + "/" +
                              SelectedTitrage.NumMetric + SelectedCouleur.Nbr.ToString("00");
                        Designation = SelectedTypeMatiere.MatiereNom.ToUpper() + " " + SelectedTitrage.NumTwist + "/" +
                                      SelectedTitrage.NumMetric + " " + SelectedCouleur.Name.ToUpper();
                    }
                    else
                    {
                        Ref = SelectedTypeMatiere.Codification.ToUpper() + SelectedTitrage.NumMetric +
                              SelectedCouleur.Nbr.ToString("00");
                        Designation = SelectedTypeMatiere.MatiereNom.ToUpper() + " " + SelectedTitrage.NumMetric + " " +
                                      SelectedCouleur.Name.ToUpper();
                    }
                }
                else
                {
                    if (SelectedTitrage.NumTwist > 0)
                    {
                        Ref = SelectedTypeMatiere.Codification.ToUpper() + SelectedTitrage.NumTwist + "/" +
                              SelectedTitrage.NumMetric;
                        Designation = SelectedTypeMatiere.MatiereNom.ToUpper() + " " + SelectedTitrage.NumTwist + "/" +
                                      SelectedTitrage.NumMetric + " ";
                    }
                    else
                    {
                        Ref = SelectedTypeMatiere.Codification.ToUpper() + SelectedTitrage.NumMetric;
                        Designation = SelectedTypeMatiere.MatiereNom.ToUpper() + " " + SelectedTitrage.NumMetric;
                    }
                }
            }
        }

        public async Task UpdateCouleur()
        {
            await Task.Run(async() =>
                {
                   var collist= await db.GetCouleurs();
                    ListeCouleur = new MvxObservableCollection<Couleur>(collist );
                }
            );
          
        }
        public async Task UpdateListeMatiere()
        {
            await Task.Run(() =>
                {
                    ListMatiere = new MvxObservableCollection<Matiere>(db.GetMatieres());
                }
            );
          
        }
        #endregion

        #region Couleur

          private string _CouleurNom;

    private int _EditColorId;
    private bool _IsCouleurEditEnabled;
    private MvxObservableCollection<Couleur> _ListCouleur;



    private string _NovCouleurNom;
    private string _NovNumero;
    private string _Numero;

    private Couleur _SelectedColor;




   
    
   



    public int EditColorId
    {
        get => _EditColorId;
        set
        {
            _EditColorId = value;
            RaisePropertyChanged();
        }
    }

    public bool IsCouleurEditEnabled
    {
        get => _IsCouleurEditEnabled;
        set
        {
            _IsCouleurEditEnabled = value;
            RaisePropertyChanged();
        }
    }

    private IMvxCommand _ModifierCouleurCmd;

    public IMvxCommand ModifierCouleurCmd
    {
        get
        {
            _ModifierCouleurCmd = new MvxCommand(ModifierColor);
            return _ModifierCouleurCmd;
        }

    }

    private IMvxCommand _SupprimerCouleurCmd;

    public IMvxCommand SupprimerCouleurCmd
    {
        get
        {
            _SupprimerCouleurCmd = new MvxCommand(SupprimerColor);
            return _SupprimerCouleurCmd;
        }
    }

    private IMvxCommand _AjouterNovCol;

    public IMvxCommand AjouterNovCol
    {
        get
        {
            _AjouterNovCol = new MvxAsyncCommand(async() =>
            {
                await AjouterNovColor();
            });
       
            return _AjouterNovCol;
        }
    }

    private IMvxCommand _CancelCouleurCmd;

    public IMvxCommand CancelCouleurCmd
    {
        get
        {
            _CancelCouleurCmd = new MvxCommand(CancelCouleurEdit);
            return _CancelCouleurCmd;
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
    private IMvxCommand _SaveCouleurChange;

    public IMvxCommand SaveCouleurChange
    {
        get
        {
            _SaveCouleurChange = new MvxCommand(SaveCouleurEditChange);
            return _SaveCouleurChange;
        }
    }

    public MvxObservableCollection<Couleur> ListCouleur
    {
        get => _ListCouleur;
        set
        {
            _ListCouleur = value;
            RaisePropertyChanged();
        }
    }

    public Couleur SelectedColor
    {
        get => _SelectedColor;
        set
        {
            _SelectedColor = value;
            RaisePropertyChanged();
        }
    }

    public string Numero
    {
        get => _Numero;
        set
        {
            _Numero = value;
            RaisePropertyChanged();
        }
    }

    public string CouleurNom
    {
        get => _CouleurNom;
        set
        {
            _CouleurNom = value;
            RaisePropertyChanged();
        }
    }

    public string NovNumero
    {
        get => _NovNumero;
        set
        {
            _NovNumero = value;
            RaisePropertyChanged();
        }
    }

    public string NovCouleurNom
    {
        get => _NovCouleurNom;
        set
        {
            _NovCouleurNom = value;
            RaisePropertyChanged();
        }
    }

    public void SaveCouleurEditChange()
    {
        if (!IsCouleurEditFieldsEmpty() && IsNumero(NovNumero) &&
            db.CheckUniqueColor(EditColorId,Convert.ToInt32(NovNumero), NovCouleurNom) == null)
        {
            var NewColour = new Couleur();
            NewColour.ID = EditColorId;
            NewColour.Nbr = Convert.ToInt32(NovNumero);
            NewColour.Name = NovCouleurNom;
            db.EditColor(NewColour);
            UpdateColorList();
            CancelCouleurEdit();
        }
        else if (IsCouleurEditFieldsEmpty())
        {
            SendNotification.Raise("Remplit tout les champs");
        }
        else if (!IsNumero(NovNumero))
        {
            SendNotification.Raise("Choisir un numero correct");
        }
        else
        {
            SendNotification.Raise("Couleur existe déja");
        }
    }

    public async Task UpdateColorList()
    {
     
        var listColor =await db.GetCouleurs();
        foreach (var col in listColor) col.Name = col.Name.ToUpper();
     
       
        ListCouleur = new MvxObservableCollection<Couleur>(listColor.ToList());
    }

    public void CancelCouleurEdit()
    {
        IsCouleurEditEnabled = false;
        NovCouleurNom = "";
        NovNumero = "";
    }

    public void ModifierColor()
    {
        if (SelectedColor != null)
        {
            IsCouleurEditEnabled = true;
            NovCouleurNom = SelectedColor.Name;
            NovNumero = SelectedColor.Nbr.ToString();
            EditColorId = SelectedColor.ID;
        }
        else
        {
            SendNotification.Raise("S.V.P Séléctionnez une couleur");
        }
    }

    public void SupprimerColor()
    {
        if (SelectedColor != null)
        {
            var req = new YesNoQuestion
            {
                Question = "êtes-vous sûr de vouloir supprimer cette Couleur séléctionnée ?",
                UploadCallback = ok =>
                {
                    if (ok)
                    {
                        db.DeleteColor(SelectedColor);
                        UpdateColorList();
                    }
                }
            };
            ConfirmAction.Raise(req);
        }
        else
        {
            SendNotification.Raise("S.V.P Séléctionnez une couleur");
        }
    }

    public async Task AjouterNovColor()
    {
        await Task.Run(async() =>
            {
                var test = db.GetColor(Convert.ToInt32(Numero), CouleurNom);
                if (!IsCouleurAddFieldsEmpty() && IsNumero(Numero) && db.GetColor(Convert.ToInt32(Numero), CouleurNom) == null)
                {
                    var NewColour = new Couleur();
                    NewColour.Nbr = Convert.ToInt32(Numero);
                    NewColour.Name = CouleurNom;
                    db.AddNewColor(NewColour);
                    await UpdateColorList();
                    Numero = "";
                    CouleurNom = "";
                }
                else if (IsCouleurAddFieldsEmpty())
                {
                    SendNotification.Raise("Remplit tout les champs");
                }
                else if (!IsNumero(Numero))
                {
                    SendNotification.Raise("Choisir un numero correct");
                }
                else
                {
                    SendNotification.Raise("Couleur existe déja");
                }
            }
        );
      
    }

    public bool IsNumero(string Num)
    {
        try
        {
            Convert.ToInt32(Num);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool IsCouleurAddFieldsEmpty()
    {
        return Numero == null || CouleurNom == null || string.IsNullOrWhiteSpace(Numero) ||
               string.IsNullOrWhiteSpace(CouleurNom);
    }

    public bool IsCouleurEditFieldsEmpty()
    {
        return NovNumero == null || NovCouleurNom == null || string.IsNullOrWhiteSpace(NovNumero) ||
               string.IsNullOrWhiteSpace(NovCouleurNom);
    }

        #endregion
    }
}