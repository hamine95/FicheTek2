using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd.ViewModel
{
    public class HomepageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public HomepageViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
            FicheTechniqueBtn = new MvxCommand(StartFicheTechniquePanel);
        }

        public IMvxCommand FicheTechniqueBtn { get; set; }

        public void StartFicheTechniquePanel()
        {
            _navigationService.Navigate<FicheTechniqueViewModel>();
        }
    }
}