using System.Printing;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using BackEnd2.CustomClass;
using BackEnd2.ViewModel;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace FrontEnd.View
{
    [MvxWindowPresentation]
    [MvxViewFor(typeof(PrintViewModel))]
    public partial class PrintView : MvxWindow
    {
        
        public PrintView()
        {
           
            InitializeComponent();
            this.Language = XmlLanguage.GetLanguage("fr-FR");
            PreviewD.Language= XmlLanguage.GetLanguage("fr-FR");
           
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void PrintView_OnLoaded(object sender, RoutedEventArgs e)
        {
        
        }

        private void FixedDocument_Loaded(object sender, RoutedEventArgs e)
        {
        
        }

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new PrintDialog();
            dialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
            dialog.PrintTicket.PageMediaSize   = new PageMediaSize(PageMediaSizeName.ISOA4);
            dialog.UserPageRangeEnabled=true;
         
            dialog.MinPage = 1;
            dialog.MaxPage = 2;
            
            if (dialog.ShowDialog() == true)
            {


               int  _startIndex = dialog.PageRange.PageFrom - 1;
                int _endIndex = dialog.PageRange.PageTo - 1;
                if (_startIndex >= 0)
                {
                    if (_endIndex > 0 && _endIndex>_startIndex)
                    {
                       
                        PageRangeDocumentPaginator paginator = new PageRangeDocumentPaginator(
                            PreviewD.Document.DocumentPaginator,
                            dialog.PageRange);
                        
                        dialog.PrintVisual(paginator.GetPage(0).Visual,"Fiche Technique");
                    }
                    else
                    {
                        dialog.PrintVisual(PreviewD.Document.DocumentPaginator.GetPage(_startIndex).Visual, "Fiche Technique");
                    }
                }
               PageRangeSelection df= dialog.PageRangeSelection;
           
                //dialog.PrintVisual(PreviewD.Document.DocumentPaginator.GetPage(_startIndex).Visual, "Fiche Technique");
            }
        }

        private void PreviewD_OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}