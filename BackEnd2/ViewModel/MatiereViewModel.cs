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
    public class MatiereViewModel : MvxViewModel<user>
    {
        private IMvxCommand _AjouterNovMatiere;

        private IMvxCommand _AjouterNovTitrage;

        private IMvxCommand _AjouterNovTypeMatiere;
        private bool _AllColor = true;

        private IMvxCommand _CancelCmd;
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


        private MvxNotifyTask _LoadLists;

        private IMvxCommand _Modifier;

        private IMvxNavigationService _navigationService;
        private string _NomMatiere;


        private string _NumMetric;


        private string _NumTwist;
        private string _Ref;

        private IMvxCommand _saveChange;
        private Couleur _SelectedCouleur;


        private Couleur _SelectedCouleurEdit;
        private Titrage _selectedEditTitrage;

        private Matiere _SelectedMatiere;
        private Titrage _SelectedTitrage;


        private TypeMatiere _SelectedTypeMatiere;


        private TypeMatiere _SelectedTypeMatiereEdit;
        private TypeMatiere _SelectedTypeMatiereT;

        private IMvxCommand _Supprimer;
        private MvxObservableCollection<TypeMatiere> _TypeMatiereList;
        private SqliteData db2;
        private int SelectedMatiereEdit;

        public MatiereViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
        }

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

        public bool IsEditEnabled
        {
            get => _IsEditEnabled;
            set
            {
                _IsEditEnabled = value;
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

        public IMvxCommand AjouterNovTitrage
        {
            get
            {
                _AjouterNovTitrage = new MvxCommand(AjouterNouveauTitrage);
                return _AjouterNovTitrage;
            }
        }

        public IMvxCommand AjouterNovTypeMatiere
        {
            get
            {
                _AjouterNovTypeMatiere = new MvxCommand(AjouterNoveauTypeMatiere);
                return _AjouterNovTypeMatiere;
            }
        }

        public IMvxCommand SaveChange
        {
            get
            {
                _saveChange = new MvxCommand(SaveEditChange);
                return _saveChange;
            }
        }

        public IMvxCommand CancelCmd
        {
            get
            {
                _CancelCmd = new MvxCommand(CancelEditing);
                return _CancelCmd;
            }
        }

        public IMvxCommand AjouterNovMatiere
        {
            get
            {
                _AjouterNovMatiere = new MvxCommand(AjouterNouveauMatiere);
                return _AjouterNovMatiere;
            }
        }

        public IMvxCommand Modifier
        {
            get
            {
                _Modifier = new MvxCommand(ModifierMatiere);
                return _Modifier;
            }
        }

        public IMvxCommand Supprimer
        {
            get
            {
                _Supprimer = new MvxCommand(SupprimerMatiere);
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

        public override Task Initialize()
        {
            LoadLists = MvxNotifyTask.Create(RefreshLists);
            return base.Initialize();
        }

        public async Task RefreshLists()
        {
            await UpdateListeMatiere();
            await UpdateTypeMatiereList();
            await UpdateCouleur();
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
                    int NMetric;
                    if (int.TryParse(NumTwist, out NTwist))
                    {
                        if (int.TryParse(NumMetric, out NMetric))
                        {
                            NewTitrage.NumTwist = NTwist;
                            NewTitrage.NumMetric = NumMetric;
                            NewTitrage.TypeMatiere = SelectedTypeMatiereT;
                            NewTitrage.Designation = NumTwist + "/" + NumMetric;
                            db2.AddNewTitrage(NewTitrage);
                            UpdateTypeMatiereList();
                            SelectedTypeMatiere = null;
                            SelectedTypeMatiereEdit = null;

                            SelectedTypeMatiereT = null;
                            NumMetric = "";
                            NumTwist = "";
                        }
                        else
                        {
                            SendNotification.Raise("Valeur Incorrect Titrage");
                        }
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
                if (int.TryParse(NumMetric, out NMetric))
                {
                    NewTitrage.NumTwist = 0;
                    NewTitrage.NumMetric = NumMetric;
                    NewTitrage.TypeMatiere = SelectedTypeMatiereT;
                    NewTitrage.Designation = NumMetric;
                    db2.AddNewTitrage(NewTitrage);
                    UpdateTypeMatiereList();
                    SelectedTypeMatiere = null;
                    SelectedTypeMatiereEdit = null;
                    SelectedTypeMatiereT = null;
                    NumMetric = "";
                }
                else
                {
                    SendNotification.Raise("Valeur Incorrect Titrage");
                }
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
                            db2.DeleteMatiere(SelectedMatiere);
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

        public void SaveEditChange()
        {
            if (!IsEditFieldsEmpty2())
            {
                if (db2.GetMatiere(EditRef) == null)
                {
                    var mMatiere = new Matiere();
                    mMatiere.ID = SelectedMatiereEdit;
                    mMatiere.Ref = EditRef;
                    mMatiere.Designation = EditDesignation;
                    mMatiere.Titrage = selectedEditTitrage;
                    mMatiere.GetCouleur = SelectedCouleurEdit;
                    db2.EditMatiere(mMatiere);
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
            IsEditEnabled = false;
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
                IsEditEnabled = true;
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
            ListTitrage = new MvxObservableCollection<Titrage>(db2.GetTitrageByTypMat(tm));
        }

        public void UpdateTitrageEdit(TypeMatiere tm)
        {
            ListTitrageEdit = new MvxObservableCollection<Titrage>(db2.GetTitrageByTypMat(tm));
        }
        
        public void AjouterNoveauTypeMatiere()
        {
            if (NomMatiere != null && !string.IsNullOrWhiteSpace(NomMatiere) && CodificationTypM != null &&
                !string.IsNullOrWhiteSpace(CodificationTypM))
            {
                if (db2.GetTypeMatiereNom(NomMatiere) == null)
                {
                    var typeM = new TypeMatiere();
                    typeM.MatiereNom = NomMatiere;
                    typeM.Codification = CodificationTypM;
                    db2.AddNewTypeMatiere(typeM);
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
            await Task.Run(() => { TypeMatiereList = new MvxObservableCollection<TypeMatiere>(db2.GetTypeMatieres()); });
        }


        public bool IsEditFieldsEmpty()
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
            if (!IsEditFieldsEmpty())
            {
                if (AllColor == false)
                {
                    if (db2.GetMatiere(Ref) == null)
                    {
                        var mMatiere = new Matiere();
                        mMatiere.Ref = Ref;
                        mMatiere.Designation = Designation;
                        mMatiere.Titrage = SelectedTitrage;
                        mMatiere.GetCouleur = SelectedCouleur;
                        db2.AddNewMatiere(mMatiere);
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
                            if (db2.GetMatiere(mMatiere.Ref) == null)
                            {
                                AddedNewMatiere = true;
                                mMatiere.Designation = Designation + " " + colr.Name;
                                mMatiere.Titrage = SelectedTitrage;
                                mMatiere.GetCouleur = colr;
                                db2.AddNewMatiere(mMatiere);
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
            await Task.Run(async () =>
                {
                    var collist =  db2.GetCouleurs();
                    ListeCouleur = new MvxObservableCollection<Couleur>(collist);
                }
            );
        }

        public async Task UpdateListeMatiere()
        {
            await Task.Run(() => { ListMatiere = new MvxObservableCollection<Matiere>(db2.GetMatieres()); }
            );
        }

        public override void Prepare(user parameter)
        {
            
            db2 = Mvx.IoCProvider.Resolve<SqliteData>();
        }
    }
}