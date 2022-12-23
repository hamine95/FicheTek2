using System;
using System.Threading.Tasks;
using BackEnd.ViewModel;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd
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