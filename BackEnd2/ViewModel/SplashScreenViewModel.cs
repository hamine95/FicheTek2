using System;
using System.Windows.Threading;
using MvvmCross.Navigation;
using MvvmCross.Navigation.EventArguments;
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
        public async void CloseWindow(object obj,IMvxNavigateEventArgs args)
        {
            var b= await _navigationService.Close(this);
        }
        private void ShowMainWindow(object sender, EventArgs e)
        {
            // code goes here
            
            _navigationService.Navigate<LoginViewModel,MvxViewModel>(this);
            //_navigationService.Close(this);
            dispatcherTimer.Stop();
        }
    }
}