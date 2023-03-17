using System;
using System.Threading.Tasks;
using BackEnd2.Database;
using BackEnd2.ViewModel;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd2
{
    public class AppStart : MvxAppStart
    {
        private readonly IMvxNavigationService _navigationService;

        public AppStart(IMvxApplication application, IMvxNavigationService navigationService) : base(application,
            navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            
            await _navigationService.Navigate<SplashScreenViewModel>();
        }
    }
}