using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DataSheetDesign
{
    /// <summary>
    ///     Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : Window
    {
        private FixedDocumentSequence _document;

        public PrintWindow(FixedDocumentSequence document)
        {
            _document = document;
            InitializeComponent();
            PreviewD.Document = document;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                //Set PageOrientation to Landscape
                dialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
                dialog.PrintVisual(PreviewD.Document.DocumentPaginator.GetPage(0).Visual, "My Canvas");
            }
        }
    }
}