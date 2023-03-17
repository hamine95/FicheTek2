﻿using System.Printing;
using System.Windows;
using System.Windows.Controls;
using BackEnd2.ViewModel;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace FrontEnd.View
{
    [MvxWindowPresentation]
    [MvxViewFor(typeof(AnnualReportPrintViewModel))]
    public partial class AnnualReportPrintView : MvxWindow
    {
        public AnnualReportPrintView()
        {
            InitializeComponent();
        }

        private void CmbPrinterSelection_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comb = (ComboBox)sender;
            _SelectedPrinter = comb.SelectedItem.ToString();
        }
        private string _SelectedPrinter;
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if(_SelectedPrinter==null)
                return;
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);
            printDlg.UserPageRangeEnabled = true;
            printDlg.PrintQueue = new PrintQueue(new PrintServer(), _SelectedPrinter);
            printDlg.PrintVisual( DocViewer.Document.DocumentPaginator.GetPage(0).Visual, "Rapport Annuel" );
        }
    }
}