using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using BackEnd2.CustomClass;
using BackEnd2.Data;
using BackEnd2.Database;
using BackEnd2.Model;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Presenters;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class MachineViewModel : MvxViewModel<user>
    {
        private IMvxNavigationService _navigationService;
        private  MyDBContext db;
        private  SqliteData db2;

        public MachineViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
           
        
        }

      

        public override Task Initialize()
        {
          
            LoadList=MvxNotifyTask.Create(RefreshLists);
            UpdateReedList();
            return base.Initialize();
        }

        public async Task RefreshLists()
        {
            await UpdateMachineModelList();
            await UpdateMachineList();
            
        }
        private MvxNotifyTask _LoadList;

        public MvxNotifyTask LoadList
        {
            get => _LoadList;
            set => SetProperty(ref _LoadList, value);
        }
        
       
        public MvxInteraction<YesNoQuestion> ConfirmAction { get; } = new MvxInteraction<YesNoQuestion>();

        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();


        #region Methods

        public void EmptyAfterAdding()
        {
            Num = "";
            Designation = "";
            SelectedMachineModel = null;
        }

        public void SupprimerMachine()
        {
            if (SelectedMachine != null)
            {
                var req = new YesNoQuestion
                {
                    Question = "êtes-vous sûr de vouloir supprimer cette Couleur séléctionnée ?",
                    UploadCallback = ok =>
                    {
                        if (ok)
                        {
                            db.DeleteMatiere(SelectedMachine);
                            UpdateMachineList();
                        }
                    }
                };
                ConfirmAction.Raise(req);
            }
            else
            {
                SendNotification.Raise("Aucune machine séléctionnée");
            }
        }

        public void SaveMachineChange()
        {
            if (SelectedModelEdit != null && EditNum != null && !string.IsNullOrWhiteSpace(EditNum))
            {
                int NumeroMachine;
                if (int.TryParse(EditNum, out NumeroMachine))
                {
                    var mMachine = new Machine();
                    mMachine.Num = NumeroMachine;
                    mMachine.Model = SelectedModelEdit;
                    mMachine.Name = EditDesignation;
                    mMachine.ID = EditingMachineID;
                    if (db2.GetMachine(mMachine.Num, mMachine.Model) == null)
                    {
                        db2.EditMachine(mMachine);
                        UpdateMachineList();
                        CancelChange();
                    }
                    else
                    {
                        SendNotification.Raise("Machine existe déja");
                    }
                }
                else
                {
                    SendNotification.Raise("Numéro Machine incorrect");
                }
            }
            else
            {
                if (SelectedModelEdit == null)
                    SendNotification.Raise("Model Machine est requis");
                else
                    SendNotification.Raise("Numéro machine est requis");
            }
        }

        public void CancelChange()
        {
            IsEditEnabled = false;
            EditNum = "";
            EditDesignation = "";
            SelectedModelEdit = null;
        }

        public void SetEditMachineName()
        {
            if (SelectedModelEdit != null && EditNum != null && !string.IsNullOrWhiteSpace(EditNum))
                EditDesignation = SelectedModelEdit.Name + " N°" + EditNum;
        }

        public void SetMachineName()
        {
            if (SelectedMachineModel != null && Num != null && !string.IsNullOrWhiteSpace(Num))
                Designation = SelectedMachineModel.Name + " N°" + Num;
        }

        public void AjouterNouvelleMachine()
        {
            if (SelectedMachineModel != null && Num != null && !string.IsNullOrWhiteSpace(Num))
            {
                int NumeroMachine;
                if (int.TryParse(Num, out NumeroMachine))
                {
                    var mMachine = new Machine();
                    mMachine.Num = NumeroMachine;
                    mMachine.Model = SelectedMachineModel;
                    mMachine.Name = Designation;
                    if (SelectedMachineModel.method != ModelMachine.Method.Crochetage)
                    {
                        mMachine.DoubleDuitage = 0;
                    }
                    else
                    {
                        mMachine.DoubleDuitage = 1;
                    }
                  
                    if (db2.GetMachine(mMachine.Num, mMachine.Model) == null)
                    {
                        db2.AddNewMachine(mMachine);
                        UpdateMachineList();
                        EmptyAfterAdding();
                    }
                    else
                    {
                        SendNotification.Raise("Machine existe déja");
                    }
                }
                else
                {
                    SendNotification.Raise("Numéro Machine incorrect");
                }
            }
            else
            {
                if (SelectedMachineModel == null)
                    SendNotification.Raise("Model Machine est requis");
                else
                    SendNotification.Raise("Numéro machine est requis");
            }
        }

      
        public async Task UpdateMachineList()
        {
           await Task.Run(() =>
               {
                   ListMachineModel = new MvxObservableCollection<ModelMachine>(db2.GetModelMachines());
                    ListMachine = new MvxObservableCollection<Machine>(db2.GetMachines());
                    MethodList = new MvxObservableCollection<ModelMachine.Method>();
                    
                    ListMachineDuit = new MvxObservableCollection<Machine>();
                    ListDuitage = new MvxObservableCollection<Duitages>();
                    ListMachineDD = new MvxObservableCollection<Machine>();
                    foreach (var Mmachine in ListMachine)
                    {
                        if (Mmachine.Model.method==ModelMachine.Method.Crochetage)
                        {
                            ListMachineDD.Add(Mmachine);
                        }
                        if (Mmachine.Model.method!=ModelMachine.Method.Tressage)
                        {
                            ListMachineDuit.Add(Mmachine);
                        }
                    }
                }

            );

        }

        public async Task  UpdateMachineModelList()
        {
            await Task.Run(() =>
            {
                ListMachineModel = new MvxObservableCollection<ModelMachine>(db2.GetModelMachines());
                
            });

        }
        public void AjouterNouveauCrochetModel()
        {
            if (CrochetNomModel!=null && !string.IsNullOrWhiteSpace(CrochetNomModel))
            {
                var modelM = new ModelMachine();
                modelM.NomModel = CrochetNomModel.ToUpper();
                modelM.Name = CrochetNomModel ;
                modelM.method = ModelMachine.Method.Crochetage;
                if (db2.GetTresseCrochetModelMachine(modelM) == null)
                {
                    db2.AddTresseCrochetModelMachine(modelM);
                    UpdateMachineModelList();

                   
                    CrochetNomModel = "";
                }
                else
                {
                    SendNotification.Raise("Model machine existe déja");
                }
            }
            else
            {
             
                SendNotification.Raise("Nom model est requis");
         
            }
        }
        public void AjouterNouveauTresseModel()
        {
            if (TresseNomModel!=null && !string.IsNullOrWhiteSpace(TresseNomModel))
            {
                var modelM = new ModelMachine();
                modelM.NomModel = TresseNomModel.ToUpper();
                modelM.Name = TresseNomModel ;
                modelM.method = ModelMachine.Method.Tressage;
                if (db2.GetTresseCrochetModelMachine(modelM) == null)
                {
                    db2.AddTresseCrochetModelMachine(modelM);
                    UpdateMachineModelList();

                   
                    TresseNomModel = "";
                }
                else
                {
                    SendNotification.Raise("Model machine existe déja");
                }
            }
            else
            {
             
                    SendNotification.Raise("Nom model est requis");
         
            }
        }
        public void AjouterNouveauModel()
        {
            if (NbrBande != null && !string.IsNullOrWhiteSpace(NbrBande) && MaxWidth != null &&
                !string.IsNullOrWhiteSpace(MaxWidth) && NomModel != null && !string.IsNullOrWhiteSpace(NomModel))
            {
                var modelM = new ModelMachine();
                modelM.NbrBande = Convert.ToInt32(NbrBande);
                modelM.MaxWidth = Convert.ToInt32(MaxWidth);
                modelM.NomModel = NomModel.ToUpper();
                modelM.Name = NomModel + " " + NbrBande + "/" + MaxWidth;
                
                if (db.GetModelMachine(modelM) == null)
                {
                    db.AddModelMachine(modelM);
                    UpdateMachineModelList();

                    NbrBande = "";
                    MaxWidth = "";
                    NomModel = "";
                }
                else
                {
                    SendNotification.Raise("Model machine existe déja");
                }
            }
            else
            {
                if (NbrBande == null || string.IsNullOrWhiteSpace(NbrBande))
                    SendNotification.Raise("Nombre de bande est requis");
                else if (NomModel == null || string.IsNullOrWhiteSpace(NomModel))
                    SendNotification.Raise("Nom model est requis");
                else
                    SendNotification.Raise("Largeur Maximale est requise");
            }
        }

        private int EditingMachineID;

        public void ModifierMachine()
        {
            if (SelectedMachine != null)
            {
                IsEditEnabled = true;
                EditNum = SelectedMachine.Num.ToString();
                EditDesignation = SelectedMachine.Name;
                SelectedModelEdit =ListMachineModel.SingleOrDefault(machM=>machM.ID ==SelectedMachine.Model.ID);
                if (SelectedMachine.DoubleDuitage == 1)
                {
                    NovDoubleDuitage = true;
                }
                else
                {
                    NovDoubleDuitage = false;
                }
                
                EditingMachineID = SelectedMachine.ID;
            }
            else
            {
                SendNotification.Raise("S.V.P séléctionnez une machine");
            }
        }

        private ModelMachine _SelectedModelMachine;

        public ModelMachine SelectedModelMachine
        {
            get
            {
                return _SelectedModelMachine;
            }
            set
            {
                _SelectedModelMachine = value;
                RaisePropertyChanged();
            }
        }

        private int EditModelID;
        public void ModifierModelMachine()
        {
            if (SelectedModelMachine != null)
            {
                IsEditModelEnabled = true;
                NovNomModel = SelectedModelMachine.NomModel;
                NovNbrBande = SelectedModelMachine.NbrBande.ToString();
                NovMaxWidth = SelectedModelMachine.MaxWidth.ToString();
                EditModelID = SelectedModelMachine.ID;
                MethodList.Add(ModelMachine.Method.Tissage);
                MethodList.Add(ModelMachine.Method.Crochetage);
                MethodList.Add(ModelMachine.Method.Tressage);
                if (SelectedModelMachine.method == ModelMachine.Method.Crochetage)
                {
                    SelectedMethod = ModelMachine.Method.Crochetage;
                    IsTissage = false;
                }else if (SelectedModelMachine.method == ModelMachine.Method.Tissage)
                {
                    SelectedMethod = ModelMachine.Method.Tissage;
                    IsTissage = true;
                }
                else
                {
                    SelectedMethod = ModelMachine.Method.Tressage;
                    IsTissage = false;
                }
            }
            else
            {
                SendNotification.Raise("S.V.P séléctionnez un model de machine");
            }
        }
        

        #endregion

        #region binding properties

        private bool _IsEditEnabled;

        public bool IsEditEnabled
        {
            get => _IsEditEnabled;
            set
            {
                _IsEditEnabled = value;
                RaisePropertyChanged();
            }
        }
        private bool _IsEditModelEnabled;

        public bool IsEditModelEnabled
        {
            get => _IsEditModelEnabled;
            set
            {
                _IsEditModelEnabled = value;
                RaisePropertyChanged();
            }
        }
        private MvxObservableCollection<Reed> _ListPeigne;

        public MvxObservableCollection<Reed> ListPeigne
        {
            get => _ListPeigne;
            set
            {
                _ListPeigne = value;
                RaisePropertyChanged();
            }
        }
        private Reed _SelectedPeigne;

        public Reed SelectedPeigne
        {
            get => _SelectedPeigne;
            set
            {
                _SelectedPeigne = value;
                RaisePropertyChanged();
            }
        }
        private string _Numero;

        public string Numero
        {
            get => _Numero;
            set
            {
                _Numero = value;
                RaisePropertyChanged();
            }
        }
        private string _NovNumero;

        public string NovNumero
        {
            get => _NovNumero;
            set
            {
                _NovNumero = value;
                RaisePropertyChanged();
            }
        }

        private IMvxCommand _SavePeigneChange;

        public IMvxCommand SavePeigneChange
        {
            get
            {
                _SavePeigneChange = new MvxCommand(SaveReedChanges);
                return _SavePeigneChange;
            }
           
        }

        public void SaveReedChanges()
        {
            double ReedNum;

            if (NovNumero != null && !string.IsNullOrWhiteSpace(NovNumero))
            {
                if (Double.TryParse(NovNumero,out ReedNum))
                {
                    Reed DublicateReed=  ListPeigne.SingleOrDefault(pn => pn.Nombre == ReedNum && pn.ID!=SelectedPeigne.ID);
                    if (DublicateReed == null)
                    {
                        db2.ModifierNouveauPeigne(new Reed() {ID = SelectedPeigne.ID,Nombre = ReedNum });
                        UpdateReedList();
                    }
                    else
                    {
                        SendNotification.Raise("Peigne existe déja");
                    }
                }
                else
                {
                    SendNotification.Raise("Valeaur Incorrect");
                }

            }
            else
            {
                SendNotification.Raise("Numero est requis");
            }
        }
       
        private IMvxCommand _CancelPeigneCmd;

        public IMvxCommand CancelPeigneCmd
        {
            get
            {
                _CancelPeigneCmd = new MvxCommand(CancelPeigneChange);
                return _CancelPeigneCmd;
            }
        }
        public void CancelPeigneChange()
        {
            IsEditPeigneEnabled = false;
            NovNumero = "";;
        }

        private bool _IsEditPeigneEnabled;

        public bool IsEditPeigneEnabled
        {
            get
            {
                return _IsEditPeigneEnabled;
            }

            set
            {
                _IsEditPeigneEnabled = value;
                RaisePropertyChanged();
            }
        }

        private IMvxCommand _AjouterNovPeigne;

        public IMvxCommand AjouterNovPeigne
        {
            get
            {
                _AjouterNovPeigne = new MvxCommand(AjouterNouveauPeigne);
                return _AjouterNovPeigne;
            }
        }

        public void UpdateReedList()
        {
            ListPeigne = new MvxObservableCollection<Reed>(db2.GetPeigneList());
        }
        public void AjouterNouveauPeigne()
        {
            double ReedNum;

            if (Numero != null && !string.IsNullOrWhiteSpace(Numero))
            {
                if (Double.TryParse(Numero,out ReedNum))
                {
                    Reed DublicateReed=  ListPeigne.SingleOrDefault(pn => pn.Nombre == ReedNum);
                    if (DublicateReed == null)
                    {
                        db2.AjouterNouveauPeigne(new Reed() { Nombre = ReedNum });
                        Numero = "";
                        
                        UpdateReedList();
                    }
                    else
                    {
                        SendNotification.Raise("Peigne existe déja");
                    }
                }
                else
                {
                    SendNotification.Raise("Valeaur Incorrect");
                }

            }
            else
            {
                SendNotification.Raise("Numero est requis");
            }
           
        }
        
      

        private MvxObservableCollection<Machine> _ListMachineDD;

        public MvxObservableCollection<Machine> ListMachineDD
        {
            get => _ListMachineDD;
            set
            {
                _ListMachineDD = value;
                RaisePropertyChanged();
            }
        }
        private MvxObservableCollection<Machine> _ListMachineDuit;

        public MvxObservableCollection<Machine> ListMachineDuit
        {
            get => _ListMachineDuit;
            set
            {
                _ListMachineDuit = value;
                RaisePropertyChanged();
            }
        }
        private MvxObservableCollection<Machine> _ListMachine;

        public MvxObservableCollection<Machine> ListMachine
        {
            get => _ListMachine;
            set
            {
                _ListMachine = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsTissage;

        public bool IsTissage
        {
            get
            {
                return _IsTissage;
            }
            set
            {
                _IsTissage = value;
                RaisePropertyChanged();
            }
        }
        private ModelMachine.Method? _SelectedMethod;

        public ModelMachine.Method? SelectedMethod
        {
            get => _SelectedMethod;
            set
            {
                _SelectedMethod = value;
                if (_SelectedMethod != null)
                {
                    if (_SelectedMethod == ModelMachine.Method.Tissage)
                    {
                        IsTissage = true;
                    }
                    else
                    {
                        IsTissage = false;
                    }
                }
                RaisePropertyChanged();
            }
        }
        private MvxObservableCollection<ModelMachine.Method> _MethodList;

        public MvxObservableCollection<ModelMachine.Method> MethodList
        {
            get => _MethodList;
            set
            {
                _MethodList = value;
                RaisePropertyChanged();
            }
        }
        private Machine _SelectedMachine;

        public Machine SelectedMachine
        {
            get => _SelectedMachine;
            set
            {
                _SelectedMachine = value;
                RaisePropertyChanged();
                
            }
        }

        private string _NomModel;

        public string NomModel
        {
            get => _NomModel;
            set
            {
                _NomModel = value;
                RaisePropertyChanged();
            }
        }
        private string _TresseNomModel;

        public string TresseNomModel
        {
            get => _TresseNomModel;
            set
            {
                _TresseNomModel = value;
                RaisePropertyChanged();
            }
        }
        private string _NovTresseNomModel;

        public string NovTresseNomModel
        {
            get => _NovTresseNomModel;
            set
            {
                _NovTresseNomModel = value;
                RaisePropertyChanged();
            }
        }
        private string _CrochetNomModel;

        public string CrochetNomModel
        {
            get => _CrochetNomModel;
            set
            {
                _CrochetNomModel = value;
                RaisePropertyChanged();
            }
        }
        private string _NovCrochetNomModel;

        public string NovCrochetNomModel
        {
            get => _NovCrochetNomModel;
            set
            {
                _NovCrochetNomModel = value;
                RaisePropertyChanged();
            }
        }
        private string _NovNomModel;

        public string NovNomModel
        {
            get => _NovNomModel;
            set
            {
                _NovNomModel = value;
                RaisePropertyChanged();
            }
        }
      

        private string _Num;

        public string Num
        {
            get => _Num;
            set
            {
                _Num = value;
                RaisePropertyChanged();
                SetMachineName();
            }
        }

        private string _Designation;

        public string Designation
        {
            get => _Designation;
            set
            {
                _Designation = value;
                RaisePropertyChanged();
            }
        }

        private MvxObservableCollection<ModelMachine> _ListMachineModel;

        public MvxObservableCollection<ModelMachine> ListMachineModel
        {
            get => _ListMachineModel;
            set
            {
                _ListMachineModel = value;
                RaisePropertyChanged();
            }
        }

        private ModelMachine _SelectedMachineModel;

        public ModelMachine SelectedMachineModel
        {
            get => _SelectedMachineModel;
            set
            {
                _SelectedMachineModel = value;
                RaisePropertyChanged();
                SetMachineName();
            }
        }

        private string _EditNum;

        public string EditNum
        {
            get => _EditNum;
            set
            {
                _EditNum = value;
                RaisePropertyChanged();
                SetEditMachineName();
            }
        }

        private string _EditDesignation;

        public string EditDesignation
        {
            get => _EditDesignation;
            set
            {
                _EditDesignation = value;
                RaisePropertyChanged();
            }
        }

        private ModelMachine _SelectedModelEdit;

        public ModelMachine SelectedModelEdit
        {
            get => _SelectedModelEdit;
            set
            {
                _SelectedModelEdit = value;
                RaisePropertyChanged();
                SetEditMachineName();
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
        #endregion

        #region Button Commands

        private IMvxCommand _Modifier;

        public IMvxCommand Modifier
        {
            get
            {
                _Modifier = new MvxCommand(ModifierMachine);
                return _Modifier;
            }
        }
        private IMvxCommand _ModifierDuitage;

        public IMvxCommand ModifierDuitage
        {
            get
            {
                _ModifierDuitage = new MvxCommand(EditingDuitage);
                return _ModifierDuitage;
            }
        }
        private IMvxCommand _ModifierDuitageGo;

        public IMvxCommand ModifierDuitageGo
        {
            get
            {
                _ModifierDuitageGo = new MvxCommand(EditingDuitageGo);
                return _ModifierDuitageGo;
            }
        }
        public void EditingDuitageGo()
        {
            if (SelectedDuitageGo != null)
            {
                IsEditEnabledDuitageGo = true;
                NumDuitageGoEdit = SelectedDuitageGo.Duitage.ToString();

                SelectedMachineDuitGoEdit =ListMachineDD.SingleOrDefault(mach=>mach.ID== SelectedDuitageGo.Machine.ID);
                //VitesseGoEdit = SelectedDuitageGo.Vitesse.ToString();
                 
            }
            else
            {
                SendNotification.Raise("aucun duitage séléctionné");
            }
        }
        private IMvxCommand _SaveDuitageChange;

        public IMvxCommand SaveDuitageChange
        {
            get
            {
                _SaveDuitageChange = new MvxCommand(SaveNovDuitage);
                return _SaveDuitageChange;
            }
        }
        
        private IMvxCommand _SaveDuitageGoChange;

        public IMvxCommand SaveDuitageGoChange
        {
            get
            {
                _SaveDuitageGoChange = new MvxCommand(SaveNovDuitageGo);
                return _SaveDuitageGoChange;
            }
        }
        public void SaveNovDuitageGo()
        {
            if (NumDuitageGoEdit != null && !string.IsNullOrWhiteSpace(NumDuitageGoEdit)
                                    )
            {
           
                        DuitageGomme NovDuit = new DuitageGomme();
                        NovDuit.ID = SelectedDuitageGo.ID;
                        NovDuit.Duitage = NumDuitageGoEdit;
                        NovDuit.Machine = SelectedMachineDuitGoEdit;
                        if (db2.GetDuitageGoEdit(NovDuit.ID,NovDuit.Duitage) == null)
                        {
                            db2.EditDuitageGo(NovDuit);
                            UpdateDuitageGoList();
                            CancelDuitageGoChange();
                        }
                        else
                        {
                            SendNotification.Raise("Duitage Gomme existe déja");
                        }
                    
                  
               
            }
            else
            {
               
                    SendNotification.Raise("Duitage requis");
            
            }
        }
        private IMvxCommand _CancelEditDuitageCmd;

        public IMvxCommand CancelEditDuitageCmd
        {
            get
            {
                _CancelEditDuitageCmd = new MvxCommand(CancelDuitageChange);
                return _CancelEditDuitageCmd;
            }
        }
        private IMvxCommand _CancelEditDuitageGoCmd;

        public IMvxCommand CancelEditDuitageGoCmd
        {
            get
            {
                _CancelEditDuitageGoCmd = new MvxCommand(CancelDuitageGoChange);
                return _CancelEditDuitageGoCmd;
            }
        }
        public void CancelDuitageGoChange()
        {
            NumDuitageGoEdit = "";
            VitesseGoEdit = "";
            SelectedMachineDuitGoEdit = null;
            IsEditEnabledDuitageGo= false;
        }
        public void CancelDuitageChange()
        {
            NovNumDuitage = "";
            NovVitesse = "";
            SelectedMachineDuitEdit = null;
            IsEditEnabledDuitage = false;
        }
        public void SaveNovDuitage()
        {
            if (NovNumDuitage != null && !string.IsNullOrWhiteSpace(NovNumDuitage)
                                      && NovVitesse != null && !string.IsNullOrWhiteSpace(NovVitesse))
            {
                double ConvertedVitesse;
                double ConvertedDuitage;
                if (double.TryParse(NovNumDuitage, out ConvertedDuitage))
                {
                    if (double.TryParse(NovVitesse, out ConvertedVitesse))
                    {
                        Duitages NovDuit = new Duitages();
                        NovDuit.ID = SelectedDuitage.ID;
                        NovDuit.Duitage = ConvertedDuitage;
                        NovDuit.Vitesse =ConvertedVitesse;
                        NovDuit.Machine = SelectedMachineDuitEdit;
                        if (db2.GetDuitageEdit(NovDuit.ID,NovDuit.Duitage,NovDuit.Vitesse) == null)
                        {
                            db2.EditDuitage(NovDuit);
                            UpdateDuitageList();
                            CancelDuitageChange();
                        }
                        else
                        {
                            SendNotification.Raise("Duitage existe déja");
                        }
                    }
                    else
                    {
                        SendNotification.Raise("Vitesse Incorrecte");
                    }
                    
                }
                else
                {
                    SendNotification.Raise("Duitage Incorrect");
                }
               
            }
            else
            {
                if(NovNumDuitage == null || string.IsNullOrWhiteSpace(NovNumDuitage))
                    SendNotification.Raise("Duitage requis");
                else if(NovVitesse == null || string.IsNullOrWhiteSpace(NovVitesse))
                    SendNotification.Raise("vitesse requise");
            }
        }
        public void EditingDuitage()
        {
            if (SelectedDuitage != null)
            {
                IsEditEnabledDuitage = true;
                NovNumDuitage = SelectedDuitage.Duitage.ToString();

                SelectedMachineDuitEdit =ListMachineDuit.SingleOrDefault(mach=>mach.ID== SelectedDuitage.Machine.ID);
                NovVitesse = SelectedDuitage.Vitesse.ToString();
                 
            }
            else
            {
                SendNotification.Raise("aucun duitage séléctionné");
            }
        }
        private IMvxCommand _ModifierModel;

        public IMvxCommand ModifierModel
        {
            get
            {
                _ModifierModel = new MvxCommand(ModifierModelMachine);
                return _ModifierModel;
            }
        }
        private IMvxCommand _Supprimer;

        public IMvxCommand Supprimer
        {
            get
            {
                _Supprimer = new MvxCommand(SupprimerMachine);
                return _Supprimer;
            }
        }

        private IMvxCommand _AjouterNovModelMachine;

        public IMvxCommand AjouterNovModelMachine
        {
            get
            {
                _AjouterNovModelMachine = new MvxCommand(AjouterNouveauModel);
                return _AjouterNovModelMachine;
            }
        }
        private IMvxCommand _AjouterTresseNovModelMachine;

        public IMvxCommand AjouterTresseNovModelMachine
        {
            get
            {
                _AjouterTresseNovModelMachine = new MvxCommand(AjouterNouveauTresseModel);
                return _AjouterTresseNovModelMachine;
            }
        }
        private IMvxCommand _AjouterCrochetNovModelMachine;

        public IMvxCommand AjouterCrochetNovModelMachine
        {
            get
            {
                _AjouterCrochetNovModelMachine = new MvxCommand(AjouterNouveauCrochetModel);
                return _AjouterCrochetNovModelMachine;
            }
        }
        private bool _DoubleDuitage=false;

        public bool DoubleDuitage
        {
            get
            {
                return _DoubleDuitage;
            }
            set
            {
                _DoubleDuitage = value;
                RaisePropertyChanged();
            }
        }
        private bool _NovDoubleDuitage;

        public bool NovDoubleDuitage
        {
            get
            {
                return _NovDoubleDuitage;
            }
            set
            {
                _NovDoubleDuitage = value;
                RaisePropertyChanged();
            }
        }
        private IMvxCommand _AjouterDuitageGoMachine;

        public IMvxCommand AjouterDuitageGoMachine
        {
            get
            {
                _AjouterDuitageGoMachine = new MvxCommand(AjouterDuitageGoAuMachine);
                return _AjouterDuitageGoMachine;
            }
        }
     

        private IMvxCommand _AjouterNovMachine;

        public IMvxCommand AjouterNovMachine
        {
            get
            {
                _AjouterNovMachine = new MvxCommand(AjouterNouvelleMachine);
                return _AjouterNovMachine;
            }
        }

        private IMvxCommand _CancelCmd;

        public IMvxCommand CancelCmd
        {
            get
            {
                _CancelCmd = new MvxCommand(CancelChange);
                return _CancelCmd;
            }
        }
        private IMvxCommand _SaveModelChange;

        public IMvxCommand SaveModelChange
        {
            get
            {
                _SaveModelChange = new MvxCommand(SavingModelChange);
                return _SaveModelChange;
            }
        }
        private IMvxCommand _CancelModelCmd;

        public IMvxCommand CancelModelCmd
        {
            get
            {
                _CancelModelCmd = new MvxCommand(CancelingModelChange);
                return _CancelModelCmd;
            }
        }

        public void CancelingModelChange()
        {
            IsEditModelEnabled = false;
            NovNomModel = "";
            NovMaxWidth = "";
            NovNbrBande = "";
            SelectedMethod = null;
            MethodList = new MvxObservableCollection<ModelMachine.Method>();
            IsTissage = false;
        }
        public bool IsEditModelEmpty()
        {
            if (NovNbrBande == null || string.IsNullOrWhiteSpace(NovNbrBande))
            {
                return true;
            }else if (NovNomModel==null || string.IsNullOrWhiteSpace(NovNomModel))
            {
                return true;
            }
            else if(NovMaxWidth==null || string.IsNullOrWhiteSpace(NovMaxWidth))
            {
                return true  ;
            }
            else
            {
                return false;
            }
        }
        public void SavingModelChange()
        {
            if (SelectedMethod != ModelMachine.Method.Tissage)
            {
                if (NovNomModel!=null && !string.IsNullOrWhiteSpace(NovNomModel))
                {
                   
                
                            var modelMachine = new ModelMachine();
                            modelMachine.ID = EditModelID;
                            modelMachine.NomModel = NovNomModel;
                            modelMachine.Name=NovNomModel;
                            modelMachine.method =(ModelMachine.Method) SelectedMethod;
                                if (db2.GetTresseCrochetModelMachine(modelMachine)==null)
                                {
                                    db2.SaveModelCrochetTresseMachineChange(modelMachine);
                                    UpdateMachineList();
                                    CancelingModelChange();
                                }
                                else
                                {
                                    SendNotification.Raise("Model Machine existe déja");
                                }
                      
                }
                else
                {
                    if (SelectedModelEdit == null)
                        SendNotification.Raise("Model Machine est requis");
                    else
                        SendNotification.Raise("Numéro machine est requis");
                }
            }
            else
            {
                if (!IsEditModelEmpty())
                {
                    int ConvertedMaxWidth;
                    int ConvertedNbrBande;
                    if (int.TryParse(NovMaxWidth, out ConvertedMaxWidth))
                    {
                        if (int.TryParse(NovNbrBande, out ConvertedNbrBande))
                        {
                            var modelMachine = new ModelMachine();
                            modelMachine.ID = EditModelID;
                            modelMachine.NomModel = NovNomModel;
                            modelMachine.MaxWidth = ConvertedMaxWidth;
                            modelMachine.NbrBande = ConvertedNbrBande;
                            modelMachine.method =(ModelMachine.Method) SelectedMethod;
                            modelMachine.Name=NovNomModel + " " + NovNbrBande + "/" + NovMaxWidth;
                            
                                if (!db2.CheckModelDuplicate(modelMachine))
                                {
                                    db2.SaveModelMachineChange(modelMachine);
                                    UpdateMachineList();
                                    CancelingModelChange();
                                }
                                else
                                {
                                    SendNotification.Raise("Model Machine existe déja");
                                }
                        }
                        else
                        {
                            SendNotification.Raise("N° Bande Incorrecte");
                        }
                    }
                    else
                    {
                        SendNotification.Raise("Largeur Incorrecte");
                    }
                   
              
                }
                else
                {
                    if (SelectedModelEdit == null)
                        SendNotification.Raise("Model Machine est requis");
                    else
                        SendNotification.Raise("Numéro machine est requis");
                }
            }
            
        }
        private IMvxCommand _SaveChange;

        public IMvxCommand SaveChange
        {
            get
            {
                _SaveChange = new MvxCommand(SaveMachineChange);
                return _SaveChange;
            }
        }
        
        
        private string _NbrBande;

        public string NbrBande
        {
            get => _NbrBande;
            set
            {
                _NbrBande = value;
                RaisePropertyChanged();
            }
        }
        private string _NovNbrBande;

        public string NovNbrBande
        {
            get => _NovNbrBande;
            set
            {
                _NovNbrBande = value;
                RaisePropertyChanged();
            }
        }
        private string _MaxWidth;

        public string MaxWidth
        {
            get => _MaxWidth;
            set
            {
                _MaxWidth = value;
                RaisePropertyChanged();
            }
        }
        private string _NovMaxWidth;

        public string NovMaxWidth
        {
            get => _NovMaxWidth;
            set
            {
                _NovMaxWidth = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        private IMvxCommand _SupprimerPeigne;

        public IMvxCommand SupprimerPeigne
        {
            get
            {
                _SupprimerPeigne = new MvxCommand(SupprimerSelectedPeigne);
                return _SupprimerPeigne;
            }
        }

        public void SupprimerSelectedPeigne()
        {
            if (SelectedPeigne != null)
            {
                var req = new YesNoQuestion
                {
                    Question = "êtes-vous sûr de vouloir supprimer cet Peigne séléctionné ?",
                    UploadCallback = ok =>
                    {
                        if (ok)
                        {
                            db2.DeletePeigne(SelectedPeigne);
                            UpdateReedList();
                        }
                    }
                };
                ConfirmAction.Raise(req);
            }
            else
            {
                SendNotification.Raise("Aucun Peigne séléctionné");
            }
        }
        
        private IMvxCommand _ModifierPeigne;

        public IMvxCommand ModifierPeigne
        {
            get
            {
                _ModifierPeigne = new MvxCommand(ModifierSelectedPeigne);
                return _ModifierPeigne;
            }
        }

        public void ModifierSelectedPeigne()
        {
            if (SelectedPeigne != null)
            {
                IsEditPeigneEnabled = true;
                NovNumero = SelectedPeigne.Nombre.ToString();
                
            }
            else
            {
                SendNotification.Raise("Aucun Peigne Séléctionné");
            }
        }
        private user UserSession;
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

        #region Duitage View

        private Machine _SelectedMachineDuitEdit;

        public Machine SelectedMachineDuitEdit
        {
            get
            {
                return _SelectedMachineDuitEdit;
            }
            set
            {
                _SelectedMachineDuitEdit = value;
                RaisePropertyChanged();
            }
        }
        private Machine _SelectedMachineDuitGoEdit;

        public Machine SelectedMachineDuitGoEdit
        {
            get
            {
                return _SelectedMachineDuitGoEdit;
            }
            set
            {
                _SelectedMachineDuitGoEdit = value;
                RaisePropertyChanged();
            }
        }    
            
        private Machine _SelectedMachine2;

        public Machine SelectedMachine2
        {
            get
            {
                return _SelectedMachine2;
                
            }
            set
            {
                _SelectedMachine2 = value;

                if (_SelectedMachine2 != null)
                {
                    if (_SelectedMachine2.Model.method==ModelMachine.Method.Crochetage)
                    {
                        IsDoubleDuitage = true;
                        UpdateDuitageGoList();
                    }
                    else
                    {
                        IsDoubleDuitage = false;
                    }
                }
                else
                {
                    IsDoubleDuitage = false;
                }
               
                RaisePropertyChanged();
                UpdateDuitageList();
            }
        }
        public void UpdateDuitageGoList()
        {
            if (SelectedMachine2 != null)
                ListDuitageGo = new MvxObservableCollection<DuitageGomme>(db2.GetDuitageMachineGo(SelectedMachine2));
        }
        public void UpdateDuitageList()
        {
            if (SelectedMachine2 != null)
                ListDuitage = new MvxObservableCollection<Duitages>(db2.GetDuitageMachine(SelectedMachine2));
        }
        private MvxObservableCollection<Duitages> _ListDuitage;

        public MvxObservableCollection<Duitages> ListDuitage
        {
            get => _ListDuitage;
            set
            {
                _ListDuitage = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsDoubleDuitage;

        public bool IsDoubleDuitage
        {
            get
            {
                return _IsDoubleDuitage;
            }
            set
            {
                _IsDoubleDuitage = value;
                RaisePropertyChanged();
            }
        }
        private MvxObservableCollection<DuitageGomme> _ListDuitageGo;

        public MvxObservableCollection<DuitageGomme> ListDuitageGo
        {
            get
            {
                return _ListDuitageGo;
            }
            set
            {
                _ListDuitageGo = value;
                RaisePropertyChanged();
            }
        }
        private Duitages _SelectedDuitage;

        public Duitages SelectedDuitage
        {
            get
            {
                return _SelectedDuitage;
            }
            set
            {
                _SelectedDuitage = value;
                RaisePropertyChanged();
            }
        }
        private DuitageGomme _SelectedDuitageGo;

        public DuitageGomme SelectedDuitageGo
        {
            get
            {
                return _SelectedDuitageGo;
            }
            set
            {
                _SelectedDuitageGo = value;
                RaisePropertyChanged();
            }
        }
        

        private string _NumDuitage;

        public string NumDuitage
        {
            get => _NumDuitage;
            set
            {
                _NumDuitage = value;
                RaisePropertyChanged();
            }
        }
        private string _NovNumDuitage;

        public string NovNumDuitage
        {
            get => _NovNumDuitage;
            set
            {
                _NovNumDuitage = value;
                RaisePropertyChanged();
            }
        }
        private string _NovNumDuitageGo;

        public string NovNumDuitageGo
        {
            get => _NovNumDuitageGo;
            set
            {
                _NovNumDuitageGo = value;
                RaisePropertyChanged();
            }
        }
        private string _NumDuitageGo;

        public string NumDuitageGo
        {
            get => _NumDuitageGo;
            set
            {
                _NumDuitageGo = value;
                RaisePropertyChanged();
            }
        }
        private string _NumDuitageGoEdit;

        public string NumDuitageGoEdit
        {
            get => _NumDuitageGoEdit;
            set
            {
                _NumDuitageGoEdit = value;
                RaisePropertyChanged();
            }
        }
        private Machine _SelectedMachineDuitGo;

        public Machine SelectedMachineDuitGo
        {
            get => _SelectedMachineDuitGo;
            set
            {
                _SelectedMachineDuitGo = value;
                RaisePropertyChanged();
            }
        }
        private Machine _SelectedMachineDuit;

        public Machine SelectedMachineDuit
        {
            get => _SelectedMachineDuit;
            set
            {
                _SelectedMachineDuit = value;
                RaisePropertyChanged();
            }
        }
        private string _Vitesse;

        public string Vitesse
        {
            get => _Vitesse;
            set
            {
                _Vitesse = value;
                RaisePropertyChanged();
            }
        }
        private string _NovVitesse;

        public string NovVitesse
        {
            get => _NovVitesse;
            set
            {
                _NovVitesse = value;
                RaisePropertyChanged();
            }
        }
        private string _VitesseGo;

        public string VitesseGo
        {
            get => _VitesseGo;
            set
            {
                _VitesseGo = value;
                RaisePropertyChanged();
            }
        }
        private string _VitesseGoEdit;

        public string VitesseGoEdit
        {
            get => _VitesseGoEdit;
            set
            {
                _VitesseGoEdit = value;
                RaisePropertyChanged();
            }
        }
       
          public void AjouterDuitageAuMachine()
        {
            if (SelectedMachineDuit != null && NumDuitage != null && !string.IsNullOrWhiteSpace(NumDuitage) &&
                Vitesse != null && !string.IsNullOrWhiteSpace(Vitesse))
            {
                var duit = new Duitages();
                duit.Machine = SelectedMachineDuit;
                double DuitageNum;
                double VitesseMachine;
                if (double.TryParse(NumDuitage, out DuitageNum))
                {
                    if (double.TryParse(Vitesse, out VitesseMachine))
                    {
                        duit.Duitage = DuitageNum;
                        duit.Vitesse = VitesseMachine;
                        if (db2.GetDuitage(SelectedMachineDuit.ID, duit.Duitage) == null)
                        {
                            db2.AddNewDuitage(duit);
                            UpdateMachineList();
                            SelectedMachine2 = null;
                            Vitesse = "";

                            NumDuitage = "";
                        }
                        else
                        {
                            SendNotification.Raise("Duitage existe déja dans la machine");
                        }
                    }
                    else
                    {
                        SendNotification.Raise("Vitesse Incorrect");
                    }
                }
                else
                {
                    SendNotification.Raise("Duitage Incorrect");
                }
            }
            else
            {
                if (SelectedMachineDuit != null)
                    SendNotification.Raise("Nom machine est requis");
                else if (Vitesse == null || string.IsNullOrWhiteSpace(Vitesse))
                    SendNotification.Raise("Vitesse est requise");
                else
                    SendNotification.Raise("Numéro duitage est requis");
            }
        }
          private IMvxCommand _AjouterDuitageMachine;

          public IMvxCommand AjouterDuitageMachine
          {
              get
              {
                  _AjouterDuitageMachine = new MvxCommand(AjouterDuitageAuMachine);
                  return _AjouterDuitageMachine;
              }
          }

          private bool _IsEditEnabledDuitage;

          public bool IsEditEnabledDuitage
          {
              get
              {
                  return _IsEditEnabledDuitage;
              }
              set
              {
                  _IsEditEnabledDuitage = value;
                  RaisePropertyChanged();
              }
          }
          private bool _IsEditEnabledDuitageGo;

          public bool IsEditEnabledDuitageGo
          {
              get
              {
                  return _IsEditEnabledDuitageGo;
              }
              set
              {
                  _IsEditEnabledDuitageGo = value;
                  RaisePropertyChanged();
              }
          }
        public void AjouterDuitageGoAuMachine()
        {
            if (SelectedMachineDuitGo != null && NumDuitageGo != null && !string.IsNullOrWhiteSpace(NumDuitageGo) )
            {
                var duit = new DuitageGomme();
                duit.Machine = SelectedMachineDuitGo;
               
                        duit.Duitage = NumDuitageGo;
                        if (db2.GetDuitageGo(SelectedMachineDuitGo.ID, duit.Duitage) == null)
                        {
                            db2.AddNewDuitageGo(duit);
                            UpdateMachineList();
                            SelectedMachineDuitGo = null;
                            NumDuitageGo = "";
                        }
                        else
                        {
                            SendNotification.Raise("Duitage existe déja dans la machine");
                        }
                  
            }
            else
            {
                if (SelectedMachineDuitGo == null)
                    SendNotification.Raise("Nom machine est requis");
                else if (VitesseGo == null || string.IsNullOrWhiteSpace(VitesseGo))
                    SendNotification.Raise("Vitesse est requise");
                else
                    SendNotification.Raise("Numéro duitage est requis");
            }
        }
        #endregion
    }
}