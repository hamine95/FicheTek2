using BackEnd2.ViewModel;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace FrontEnd.View
{
    /// <summary>
    ///     Interaction logic for SplashScreenView.xaml
    /// </summary>
    [MvxContentPresentation]
    [MvxViewFor(typeof(SplashScreenViewModel))]
    public partial class SplashScreenView : MvxWpfView
    {
        public SplashScreenView()
        {
            InitializeComponent();
        }
    }
}