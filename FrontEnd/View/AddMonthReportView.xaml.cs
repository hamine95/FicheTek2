using System.Windows;
using System.Windows.Controls;
using BackEnd2.ViewModel;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace FrontEnd.View
{
    [MvxWindowPresentation]
    [MvxViewFor(typeof(AddMonthReportViewModel))]
    public partial class AddMonthReportView : MvxWindow
    {
        public AddMonthReportView()
        {
            InitializeComponent();
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gView = listView.View as GridView;

            var workingWidth =
                listView.ActualWidth; // take into account vertical scrollbar
            var col1 = 0.20;
            var col2 = 0.80;
            //var col3 = 0.50;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
        }
    }
}