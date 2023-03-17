using System;
using System.Drawing.Printing;
using System.Management;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using BackEnd2.CustomClass;
using BackEnd2.ViewModel;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
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
            Language = XmlLanguage.GetLanguage("fr-FR");
            DocViewer.Language = XmlLanguage.GetLanguage("fr-FR");
            double poin = PixelsToPoints(21, LengthDirection.Horizontal);
            double pix = PointsToPixels(96, LengthDirection.Horizontal);
        }
        public enum LengthDirection
        {
            Vertical, // |
            Horizontal // ——
        }
        private double PointsToPixels(double wpfPoints, LengthDirection direction)
        {
            if (direction == LengthDirection.Horizontal)
            {
                return wpfPoints * System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width / SystemParameters.WorkArea.Width;
            }
            else
            {
                return wpfPoints * System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height / SystemParameters.WorkArea.Height;
            }
        }

        private double PixelsToPoints(int pixels, LengthDirection direction)
        {
            if (direction == LengthDirection.Horizontal)
            {
                return pixels * SystemParameters.WorkArea.Width / System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            }
            else
            {
                return pixels * SystemParameters.WorkArea.Height / System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            }
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
            /*try
            {
                var dialog = new PrintDialog();
                dialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
                dialog.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);
                dialog.UserPageRangeEnabled = true;

                dialog.MinPage = 1;
                dialog.MaxPage = 2;

                if (dialog.ShowDialog() == true)
                {
                    var _startIndex = dialog.PageRange.PageFrom - 1;
                    var _endIndex = dialog.PageRange.PageTo - 1;
                    if (_startIndex >= 0)
                    {
                        if (_endIndex > 0 && _endIndex > _startIndex)
                        {
                            var paginator = new PageRangeDocumentPaginator(
                                PreviewD.Document.DocumentPaginator,
                                dialog.PageRange);

                            dialog.PrintVisual(paginator.GetPage(0).Visual, "Fiche Technique");
                        }
                        else
                        {
                            dialog.PrintVisual(PreviewD.Document.DocumentPaginator.GetPage(_startIndex).Visual,
                                "Fiche Technique");
                        }
                    }

                    var df = dialog.PageRangeSelection;

                    //dialog.PrintVisual(PreviewD.Document.DocumentPaginator.GetPage(_startIndex).Visual, "Fiche Technique");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }*/
           
        }
        public void DisplayMsg(object sender, MvxValueEventArgs<string> args)
        {
            MessageBox.Show(args.Value);
        }

        private IMvxInteraction<string> _SentNote;
        public IMvxInteraction<string> SentNotification
        {
            get => _SentNote;
            set
            {
                if (_SentNote != null)
                    _SentNote.Requested -= DisplayMsg;
                if (value != null)
                {
                    _SentNote = value;
                    _SentNote.Requested += DisplayMsg;
                }
            }
        }

        private void PreviewD_OnLoaded(object sender, RoutedEventArgs e)
        {
            var set = this.CreateBindingSet<PrintView, PrintViewModel>();
            set.Bind(this).For(view => view.SentNotification).To(viewmodel => viewmodel.SendNotification);
    
            set.Apply();
        }
        private string _SelectedPrinter;

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void UIElement_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
          
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
            if(_SelectedPrinter==null)
                return;
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);
            printDlg.PrintTicket.PageOrientation = PageOrientation.Landscape;
            printDlg.UserPageRangeEnabled = true;
            printDlg.PrintQueue = new PrintQueue(new PrintServer(), _SelectedPrinter);
            if(NumPage.Value==1)
                printDlg.PrintVisual( DocViewer.Document.DocumentPaginator.GetPage(0).Visual, "Rapport Mensuel" );
            if(!(bool)NumPageActivator.IsChecked || NumPage.Value==2)
                printDlg.PrintVisual( DocViewer.Document.DocumentPaginator.GetPage(1).Visual, "Rapport Mensuel" );
        }

        private void CmbPrinterSelection_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comb = (ComboBox)sender;
            _SelectedPrinter = comb.SelectedItem.ToString();
            

        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) 
            {
                if (e.Key == Key.H)
                {
                    if (Xpanel.Visibility == Visibility.Collapsed)
                    {
                        Xpanel.Visibility = Visibility.Visible;
                        Ypanel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Xpanel.Visibility = Visibility.Collapsed;
                        Ypanel.Visibility = Visibility.Collapsed;
                    }
                }
            }

        }
    }
}