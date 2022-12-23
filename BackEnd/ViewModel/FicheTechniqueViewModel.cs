using BackEnd.Model;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd.ViewModel
{
    public class FicheTechniqueViewModel : MvxViewModel
    {
        private Composition _Comp1;

        private Composition _Comp2;

        private Composition _Comp3;

        private Composition _Comp4;

        private Composition _Comp5;

        private Composition _Comp6;
        private Composition _Comp7;

        private Composition _Comp8;

        private Composition _Comp9;


        private MvxObservableCollection<Composition> _CompoList;

        private string _Concepteur;
        private IMvxNavigationService _navigationService;

        private string _Verificateur;

        public FicheTechniqueViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
            CompoList = new MvxObservableCollection<Composition>();
            var comp1 = new Composition();
            comp1.ID = 1;
            var composant1 = new Composant();
            composant1.ID = 0;
            composant1.Name = "Chaine";
            comp1.GetComposant = composant1;
            comp1.Num = 1;

            var comp2 = new Composition();
            comp2.ID = 2;
            var composant2 = new Composant();
            composant2.ID = 1;
            composant2.Name = "Chain";
            comp2.GetComposant = composant2;
            comp2.Num = 2;
            CompoList.Add(comp1);
            CompoList.Add(comp2);
        }

        public Composition Comp1
        {
            get => _Comp1;
            set
            {
                _Comp1 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp2
        {
            get => _Comp2;
            set
            {
                _Comp2 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp3
        {
            get => _Comp3;
            set
            {
                _Comp3 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp4
        {
            get => _Comp4;
            set
            {
                _Comp4 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp5
        {
            get => _Comp5;
            set
            {
                _Comp5 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp6
        {
            get => _Comp6;
            set
            {
                _Comp6 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp7
        {
            get => _Comp7;
            set
            {
                _Comp7 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp8
        {
            get => _Comp8;
            set
            {
                _Comp8 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp9
        {
            get => _Comp9;
            set
            {
                _Comp9 = value;
                RaisePropertyChanged();
            }
        }

        public string Concepteur
        {
            get => _Concepteur;
            set
            {
                _Concepteur = value;
                RaisePropertyChanged();
            }
        }

        public string Verificateur
        {
            get => _Verificateur;
            set
            {
                _Verificateur = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Composition> CompoList
        {
            get => _CompoList;
            set
            {
                _CompoList = value;
                RaisePropertyChanged();
            }
        }
    }
}