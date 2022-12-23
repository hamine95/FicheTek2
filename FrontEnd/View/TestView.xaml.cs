using System.Windows.Controls;
using BackEnd2.ViewModel;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace FrontEnd.View
{
    [MvxContentPresentation]
    [MvxViewFor(typeof(TestViewModel))]
    public partial class TestView : MvxWpfView
    {
        public TestView()
        {
            InitializeComponent();
        }
    }
}