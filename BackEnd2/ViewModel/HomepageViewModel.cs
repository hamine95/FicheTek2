using System;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using BackEnd2.Database;
using BackEnd2.Model;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Navigation.EventArguments;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class HomepageViewModel : MvxViewModel<user>
    {
        private readonly IMvxNavigationService _navigationService;

        private IMvxCommand _ActivateSuperUser;

        private IMvxCommand _CmdLogout;

        private string _ImageSrc;
        private bool _IsVerificateur = false;
        private bool _IsParam= true;

        private bool _IsCategorie = true;
        private bool _IsClient = true;
        private bool _IsComposant = true;
        private bool _IsCouleur = true;
        private bool _IsFicheTechnique;
        private bool _IsRapport=true;

        private bool _IsLogout = true;
        private bool _IsMachine = true;

        private bool _IsMatiere = true;
        private bool _IsPersonnel = true;
        private bool _IsProduit = true;
        private string _TipText;

        private bool _ToolTipVis = true;

        private int CountClick;
        private readonly MyDBContext db;

        private string _BarTitle =  "Gestionnaire Fiche Technique " + Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, Assembly.GetExecutingAssembly().GetName().Version.ToString().LastIndexOf("."));

        public string BarTitle
        {
            get { return _BarTitle; }
            set { _BarTitle = value; RaisePropertyChanged(); }
        }


        private DispatcherTimer dispatcherTimer;

        public bool IsSafePassage = true;

        private user.UserType PrevType;

        private user UserSession;

        public HomepageViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
            db = Mvx.IoCProvider.Resolve<MyDBContext>();

            FicheTechniqueBtn = new MvxCommand(StartFicheTechniquePanel);
            RapportBtn = new MvxCommand(NavigateToRapportView);
            CmdMachine = new MvxCommand(NavigateToMachineView);
            CmdComposant = new MvxCommand(NavigateToComposantView);
            CmdCategorie = new MvxCommand(NavigateToCategorieView);
            CmdPersonnel = new MvxCommand(NavigateToPersonnel);
            CmdClient = new MvxCommand(NavigateToClient);
            CmdParam = new MvxCommand(NavigateToParam);
            MenuCmd = new MvxCommand(OpenMenu);
        }

        public IMvxCommand MenuCmd { get; set; }

        public bool ToolTipVis
        {
            get => _ToolTipVis;
            set
            {
                _ToolTipVis = value;
                RaisePropertyChanged();
            }
        }

        public IMvxCommand ActivateSuperUser
        {
            get
            {
                _ActivateSuperUser = new MvxCommand(ActivatingSuperUser);
                return _ActivateSuperUser;
            }
        }

        public IMvxCommand CmdTest { get; set; }
        public IMvxCommand CmdPersonnel { get; set; }
        public IMvxCommand CmdParam{ get; set; }
        public IMvxCommand CmdClient { get; set; }

        public IMvxCommand CmdCategorie { get; set; }
        public IMvxCommand CmdComposant { get; set; }
        public IMvxCommand CmdMachine { get; set; }
        public IMvxCommand FicheTechniqueBtn { get; set; }
        public IMvxCommand RapportBtn { get; set; }

        public IMvxCommand CmdLogout
        {
            get
            {
                _CmdLogout = new MvxCommand(LogOut);
                return _CmdLogout;
            }
        }

        public bool IsProduit
        {
            get => _IsProduit;
            set
            {
                _IsProduit = value;
                RaisePropertyChanged();
            }
        }

        public bool IsMachine
        {
            get => _IsMachine;
            set
            {
                _IsMachine = value;
                RaisePropertyChanged();
            }
        }

        public bool IsMatiere
        {
            get => _IsMatiere;
            set
            {
                _IsMatiere = value;
                RaisePropertyChanged();
            }
        }

        public bool IsComposant
        {
            get => _IsComposant;
            set
            {
                _IsComposant = value;
                RaisePropertyChanged();
            }
        }

        public bool IsCouleur
        {
            get => _IsCouleur;
            set
            {
                _IsCouleur = value;
                RaisePropertyChanged();
            }
        }

        public bool IsPersonnel
        {
            get => _IsPersonnel;
            set
            {
                _IsPersonnel = value;
                RaisePropertyChanged();
            }
        }

        public bool IsClient
        {
            get => _IsClient;
            set
            {
                _IsClient = value;
                RaisePropertyChanged();
            }
        }
        public bool IsParam
        {
            get => _IsParam;
            set
            {
                _IsParam = value;
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
        public bool IsCategorie
        {
            get => _IsCategorie;
            set
            {
                _IsCategorie = value;
                RaisePropertyChanged();
            }
        }

        public bool IsLogout
        {
            get => _IsLogout;
            set
            {
                _IsLogout = value;
                RaisePropertyChanged();
            }
        }
        public bool IsRapport
        {
            get => _IsRapport;
            set
            {
                _IsRapport = value;
                RaisePropertyChanged();
            }
        }
        public bool IsFicheTechnique
        {
            get => _IsFicheTechnique;
            set
            {
                _IsFicheTechnique = value;
                RaisePropertyChanged();
            }
        }

        public string ImageSrc
        {
            get => _ImageSrc;
            set
            {
                _ImageSrc = value;
                RaisePropertyChanged();
            }
        }

        public string TipText
        {
            get => _TipText;
            set
            {
                _TipText = value;
                RaisePropertyChanged();
            }
        }


        public void OpenMenu()
        {
            ToolTipVis = !ToolTipVis;
        }

        public override void Prepare(user parameter)
        {
            UserSession = parameter;
            PrevType = UserSession.type;
            if (UserSession.type == user.UserType.redacteur)
            {
                TipText = "Rédacteur";
                ImageSrc = "/Asset/editor.png";
            }
            else
            {
                IsVerificateur = true;
                TipText = "Vérificateur";
                ImageSrc = "/Asset/checkerB.png";
            }
        }


        public override  void ViewAppeared()
        {
            base.ViewAppeared();
            
            if(UserSession.LoginViewM!=null)
                UserSession.LoginViewM.CloseWindow();
            StartFicheTechniquePanel();
            
        }

       
       

        public void ActivatingSuperUser()
        {
          
            if (CountClick > 3)
            {
               if(PrevType==user.UserType.redacteur)
                {
                    ImageSrc = "/Asset/editor.png";
                }
                else
                {
                    ImageSrc = "/Asset/checkerB.png";
                }
               
                UserSession.type = PrevType;
                CountClick = 0;
            }
            else if (CountClick == 3)
            {
                PrevType = UserSession.type;
                UserSession.type = user.UserType.superuser;
                ImageSrc = "/Asset/administrator.png";
               
            }
            CountClick++;

        }

        public void LogOut()
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();

            //_navigationService.Navigate<LoginViewModel>();
            //_navigationService.Close(this);
        }
        public void NavigateToParam()
        {
            if (IsSafePassage)
            {
                IsCouleur = true;
                IsClient = true;
                IsFicheTechnique = true;
                IsProduit = true;
                IsComposant = true;
                IsPersonnel = true;
                IsMatiere = true;
                IsMachine = true;
                IsCategorie = true;
                IsParam = false;
                IsRapport = true;
                _navigationService.Navigate<ParamViewModel, user>(UserSession);
            }
        }
        public void NavigateToClient()
        {
            if (IsSafePassage)
            {
                IsCouleur = true;
                IsClient = false;
                IsFicheTechnique = true;
                IsProduit = true;
                IsComposant = true;
                IsPersonnel = true;
                IsMatiere = true;
                IsMachine = true;
                IsCategorie = true;
                IsParam = true;
                IsRapport = true;
                _navigationService.Navigate<ClientViewModel, user>(UserSession);
            }
        }

        public void NavigateToPersonnel()
        {
            if (IsSafePassage)
            {
                IsCouleur = true;
                IsClient = true;
                IsFicheTechnique = true;
                IsProduit = true;
                IsComposant = true;
                IsPersonnel = false;
                IsMatiere = true;
                IsMachine = true;
                IsCategorie = true;
                IsParam = true;
                IsRapport = true;
                _navigationService.Navigate<PersonnelViewModel, user>(UserSession);
            }
        }

        public void NavigateToCategorieView()
        {
            if (IsSafePassage)
            {
                IsCouleur = true;
                IsClient = true;
                IsFicheTechnique = true;
                IsProduit = true;
                IsComposant = true;
                IsPersonnel = true;
                IsMatiere = true;
                IsMachine = true;
                IsCategorie = false;
                IsParam = true;
                IsRapport = true;
                _navigationService.Navigate<CategorieViewModel, user>(UserSession);
            }
        }

        public void NavigateToComposantView()
        {
            if (IsSafePassage)
            {
                IsCouleur = true;
                IsClient = true;
                IsFicheTechnique = true;
                IsProduit = true;
                IsComposant = false;
                IsPersonnel = true;
                IsMatiere = true;
                IsMachine = true;
                IsCategorie = true;
                IsParam = true;
                IsRapport = true;
                _navigationService.Navigate<ComposantViewModel, user>(UserSession);
            }
        }

        public void NavigateToMachineView()
        {
            if (IsSafePassage)
            {
                IsCouleur = true;
                IsClient = true;
                IsFicheTechnique = true;
                IsProduit = true;
                IsComposant = true;
                IsPersonnel = true;
                IsMatiere = true;
                IsMachine = false;
                IsCategorie = true;
                IsParam = true;
                IsRapport = true;
                _navigationService.Navigate<MachineViewModel, user>(UserSession);
            }
        }


        public void NavigateToRapportView()
        {
            if (IsSafePassage)
            {
                IsCouleur = true;
                IsClient = true;
                IsFicheTechnique = true;
                IsProduit = true;
                IsComposant = true;
                IsPersonnel = true;
                IsMatiere = true;
                IsMachine = true;
                IsCategorie = true;
                IsParam = true;
                IsRapport = false;
                _navigationService.Navigate<RapportViewModel, user>(UserSession);
            }
        }
        public void StartFicheTechniquePanel()
        {
            IsSafePassage = false;
            IsCouleur = true;
            IsClient = true;
            IsFicheTechnique = false;
            IsProduit = true;
            IsComposant = true;
            IsPersonnel = true;
            IsMatiere = true;
            IsMachine = true;
            IsRapport = true;

            _navigationService.Navigate<FicheTechniqueViewModel, user>(UserSession);
            FicheTechniqueViewModel.SafeThEvent = SafePassage;
        }

        public void SafePassage(bool b)
        {
            IsSafePassage = true;
        }
    }
}