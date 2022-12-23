using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Threading;
using BackEnd2.Database;
using BackEnd2.Model;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class HomepageViewModel : MvxViewModel<user>
    {
        private readonly IMvxNavigationService _navigationService;
        private MyDBContext db;

        private user UserSession;

        public bool IsSafePassage = true;
        public IMvxCommand MenuCmd { get; set; }

        private bool _ToolTipVis = true;

        public bool ToolTipVis
        {
            get { return _ToolTipVis; }
            set
            {
                _ToolTipVis = value;
                RaisePropertyChanged();
            }
        }

        private DispatcherTimer dispatcherTimer;

        public HomepageViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
            FicheTechniqueBtn = new MvxCommand(StartFicheTechniquePanel);
            db = Mvx.IoCProvider.Resolve<MyDBContext>();
            Nav2Color = new MvxCommand(NavigateToCouleurView);
            CmdMatiere = new MvxCommand(NavigateToMatiereView);
            CmdMachine = new MvxCommand(NavigateToMachineView);
            CmdComposant = new MvxCommand(NavigateToComposantView);
            CmdCategorie = new MvxCommand(NavigateToCategorieView);
            CmdPersonnel = new MvxCommand(NavigateToPersonnel);
            CmdClient = new MvxCommand(NavigateToClient);
            MenuCmd = new MvxCommand(OpenMenu);
        }


        public void OpenMenu()
        {
            ToolTipVis = !ToolTipVis;
        }

        public override void Prepare(user parameter)
        {
            UserSession = parameter;
            if (UserSession.type == user.UserType.redacteur)
            {
                TipText = "Rédacteur";
                ImageSrc = "/Asset/editor.png";

            }
            else
            {
                TipText = "Vérificateur";
                ImageSrc = "/Asset/checkerB.png";
            }
        }

        public override void ViewCreated()
        {
            base.ViewCreated();
        }

        public override async void ViewAppeared()
        {
            base.ViewAppeared();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += StartFirstView;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();
        }

        private void StartFirstView(object sender, EventArgs e)
        {
            // code goes here
            StartFicheTechniquePanel();
            dispatcherTimer.Stop();
        }

        private IMvxCommand _ActivateSuperUser;

        public IMvxCommand ActivateSuperUser
        {
            get
            {
                _ActivateSuperUser = new MvxCommand(ActivatingSuperUser);
                return _ActivateSuperUser;
            }
           
        }

        private int CountClick;
        public void ActivatingSuperUser()
        {
            CountClick++;
            if (CountClick > 5)
            {
                if (UserSession.type == user.UserType.superuser)
                {
                    UserSession.type = PrevType;
                }
                else
                {
                    PrevType = UserSession.type;
                    UserSession.type = user.UserType.superuser;
                }

                CountClick = 0;
            }
        }

        private user.UserType PrevType;
        public IMvxCommand CmdTest { get; set; }
        public IMvxCommand CmdPersonnel { get; set; }

        public IMvxCommand CmdClient { get; set; }

        public IMvxCommand CmdCategorie { get; set; }
        public IMvxCommand CmdComposant { get; set; }
        public IMvxCommand CmdMachine { get; set; }
        public IMvxCommand CmdMatiere { get; set; }
        public IMvxCommand Nav2Color { get; set; }
        public IMvxCommand FicheTechniqueBtn { get; set; }

        private IMvxCommand _CmdLogout;

        public IMvxCommand CmdLogout
        {
            get
            {
                _CmdLogout = new MvxCommand(LogOut);
                return _CmdLogout;
            }
        }

        public void LogOut()
        {
            _navigationService.Navigate<LoginViewModel>();
            _navigationService.Close(this);
        }
        private bool _IsFicheTechnique = false;
        private bool _IsProduit = true;
        private bool _IsMachine = true;

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

        public bool IsCategorie
        {
            get => _IsCategorie;
            set
            {
                _IsCategorie = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsLogout=true;
        public bool IsLogout
        {
            get => _IsLogout;
            set
            {
                _IsLogout = value;
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

        private bool _IsMatiere = true;
        private bool _IsComposant = true;
        private bool _IsCouleur = true;
        private bool _IsPersonnel = true;
        private bool _IsClient = true;
        private bool _IsCategorie = true;

        public void NavigateToClient()
        {
            CountClick = 0;
            if (IsSafePassage == true)
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
                _navigationService.Navigate<ClientViewModel, MyDBContext>(db);
            }
        }

        public void NavigateToPersonnel()
        {
            CountClick = 0;
            if (IsSafePassage == true)
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
                _navigationService.Navigate<PersonnelViewModel, user>(UserSession);
            }
        }

        public void NavigateToCategorieView()
        {
            CountClick = 0;
            if (IsSafePassage == true)
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
                _navigationService.Navigate<CategorieViewModel, user>(UserSession);
            }
        }

        public void NavigateToComposantView()
        {
            CountClick = 0;
            if (IsSafePassage == true)
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
                _navigationService.Navigate<ComposantViewModel, user>(UserSession);
            }
        }

        public void NavigateToMachineView()
        {
            CountClick = 0;
            if (IsSafePassage == true)
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
                _navigationService.Navigate<MachineViewModel, user>(UserSession);
            }
        }

        public void NavigateToMatiereView()
        {
            CountClick = 0;
            if (IsSafePassage == true)
            {
                IsCouleur = true;
                IsClient = true;
                IsFicheTechnique = true;
                IsProduit = true;
                IsComposant = true;
                IsPersonnel = true;
                IsMatiere = false;
                IsMachine = true;
                IsCategorie = true;
                _navigationService.Navigate<MatiereViewModel, MyDBContext>(db);
            }
        }

        public void NavigateToCouleurView()
        {
            CountClick = 0;
            if (IsSafePassage == true)
            {
                IsCouleur = false;
                IsClient = true;
                IsFicheTechnique = true;
                IsProduit = true;
                IsComposant = true;
                IsPersonnel = true;
                IsMatiere = true;
                IsMachine = true;
                IsCategorie = true;
                _navigationService.Navigate<CouleurViewModel, MyDBContext>(db);
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

            _navigationService.Navigate<FicheTechniqueViewModel, user>(UserSession);
            FicheTechniqueViewModel.SafeThEvent = SafePassage;
        }

        private string _ImageSrc;
        private string _TipText;

        public string ImageSrc
        {
            get { return _ImageSrc; }
            set
            {
                _ImageSrc = value;
                RaisePropertyChanged();
            }
        }

        public string TipText
        {
            get { return _TipText; }
            set
            {
                _TipText = value;
                RaisePropertyChanged();
            }
        }

        public void SafePassage(bool b)
        {
            IsSafePassage = true;
        }
    }
}