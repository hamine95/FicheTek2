using System.Windows;
using BackEnd2.ViewModel;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace FrontEnd.View
{
    [MvxWindowPresentation]
    [MvxViewFor(typeof(EditMonthReportViewModel))]
    public partial class EditMonthReportView : MvxWindow
    {
        public EditMonthReportView()
        {
            InitializeComponent();
        }
    }
}