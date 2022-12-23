using System;
using System.Windows.Threading;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class SplashScreenViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private DispatcherTimer dispatcherTimer;

        public SplashScreenViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
        }

        public override void ViewCreated()
        {
            base.ViewCreated();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
           dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += ShowMainWindow;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();
        }

        private void ShowMainWindow(object sender, EventArgs e)
        {
            // code goes here
            _navigationService.Navigate<LoginViewModel>();
            _navigationService.Close(this);
            dispatcherTimer.Stop();
        }
    }
}