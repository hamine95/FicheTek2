using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;

namespace DataSheetDesign
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            DataContext = this;
            ListCompo = new ObservableCollection<Composant>();
            var cp = new Composant();
            //var converter = new System.Windows.Media.BrushConverter();
            //var brush = (Brush)converter.ConvertFromString("#FF000000");
            cp.BKBorderComposant = "Black";
            cp.BKComposant = "Black";
            cp.NameComposant = "Chaine";
            cp.NumComposant = 1;
            cp.FGComposant = "White";
            var cp2 = new Composant();
            //var converter = new System.Windows.Media.BrushConverter();
            //var brush = (Brush)converter.ConvertFromString("#FF000000");

            cp2.BKBorderComposant = "Black";
            cp2.BKComposant = "Gray";
            cp2.NameComposant = "Chaine 2";
            cp2.NumComposant = 2;
            cp2.FGComposant = "Black";
            ListCompo.Add(cp);
            ListCompo.Add(cp2);
            DataSheet.ListComposant = ListCompo;
        }

        public ObservableCollection<Composant> ListCompo { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("printPreview.xps")) File.Delete("printPreview.xps");
            var xpsDocument = new XpsDocument("printPreview.xps", FileAccess.ReadWrite);
            var writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            var packagePolicy = new XpsPackagingPolicy(xpsDocument);
            var serializationMgr = new XpsSerializationManager(packagePolicy, false);
            serializationMgr.SaveAsXaml(DataSheet);

            var fixedDocSeq = xpsDocument.GetFixedDocumentSequence();
            //writer.Write((fixedDocSeq).DocumentPaginator);
            //DocumentPaginator paginator = ((IDocumentPaginatorSource)xpsDocument.GetFixedDocumentSequence()).DocumentPaginator;
            var Document = xpsDocument.GetFixedDocumentSequence();
            xpsDocument.Close();
            var windows = new PrintWindow(Document);
            windows.ShowDialog();
        }
    }
}