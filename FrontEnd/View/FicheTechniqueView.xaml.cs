using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using BackEnd2.CustomClass;
using BackEnd2.ViewModel;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using ListView = System.Windows.Controls.ListView;
using MessageBox = System.Windows.MessageBox;

namespace FrontEnd.View
{
    /// <summary>
    ///     Interaction logic for FicheTechniqueView.xaml
    /// </summary>
    [MvxContentPresentation]
    [MvxViewFor(typeof(FicheTechniqueViewModel))]
    public partial class FicheTechniqueView : MvxWpfView
    {
        
        private IMvxInteraction<YesNoQuestion> _ConfirmBox;

        private IMvxInteraction<LoadedImage> _GetImageProd;

        private IMvxInteraction<string> _SentNote;

        public FicheTechniqueView()
        {
            InitializeComponent();
            Loaded += (s, e) => Keyboard.Focus(this);
        }

        public IMvxInteraction<LoadedImage> GetImageProd
        {
            get => _GetImageProd;
            set
            {
                if (_GetImageProd != null)
                    _GetImageProd.Requested -= GetImagePath;
                if (value != null)
                {
                    _GetImageProd = value;
                    _GetImageProd.Requested += GetImagePath;
                }
            }
        }

        public IMvxInteraction<YesNoQuestion> ConfirmBox
        {
            get => _ConfirmBox;
            set
            {
                if (_ConfirmBox != null)
                    _ConfirmBox.Requested -= ConfirmMsg;
                if (value != null)
                {
                    _ConfirmBox = value;
                    _ConfirmBox.Requested += ConfirmMsg;
                }
            }
        }

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

        public void GetImagePath(object sender, MvxValueEventArgs<LoadedImage> args)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = "Files|*.jpg;*.jpeg;*.png"; // Required file extension 
            fileDialog.Filter = "Files|*.jpg;*.jpeg;*.png"; // Optional file extensions

            if (fileDialog.ShowDialog() == DialogResult.OK)
                args.Value.UploadCallback(fileDialog.FileName, fileDialog.SafeFileName, true);
        }

        public void ConfirmMsg(object sender, MvxValueEventArgs<YesNoQuestion> args)
        {
            var result = MessageBox.Show(args.Value.Question, "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                args.Value.UploadCallback(true);
            else
                args.Value.UploadCallback(false);
        }

        public void DisplayMsg(object sender, MvxValueEventArgs<string> args)
        {
            MessageBox.Show(args.Value);
        }

        private void MvxWpfView_Loaded(object sender, RoutedEventArgs e)
        {
            var set = this.CreateBindingSet<FicheTechniqueView, FicheTechniqueViewModel>();
            set.Bind(this).For(view => view.SentNotification).To(viewmodel => viewmodel.SendNotification);
            set.Bind(this).For(view => view.ConfirmBox).To(viewmodel => viewmodel.ConfirmAction);
            set.Bind(this).For(view => view.GetImageProd).To(viewmodel => viewmodel.GetImagePath);
            set.Apply();
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
            //gView.Columns[2].Width = workingWidth * col3;
        }


        //private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        //{

        //   // Frm.Content=new FicheTekPart1();
        //}

        //private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    //Frm.Content = new FicheTekPart2();
        //}

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // if (File.Exists("printPreview.xps")) File.Delete("printPreview.xps");
            // var xpsDocument = new XpsDocument("printPreview.xps", FileAccess.ReadWrite);
            // var writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            // var packagePolicy = new XpsPackagingPolicy(xpsDocument);
            // var serializationMgr = new XpsSerializationManager(packagePolicy, false);
            // serializationMgr.SaveAsXaml(Part1Tek);
            // var hei = Part1Tek.ActualHeight;
            // var wid = Part1Tek.ActualWidth;
            // var fixedDocSeq = xpsDocument.GetFixedDocumentSequence();
            // var Document = xpsDocument.GetFixedDocumentSequence();
            // xpsDocument.Close();
         
        }

        private void Frm_OnKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) // Is Alt key pressed
            {
                if (Keyboard.IsKeyDown(Key.R) && Keyboard.IsKeyDown(Key.T))
                {
                    if(RepBtn.Visibility==Visibility.Collapsed)
                        RepBtn.Visibility = Visibility.Visible;
                    else
                    {
                        RepBtn.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
    }
}