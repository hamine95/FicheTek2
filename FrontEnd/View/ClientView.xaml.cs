using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms ;
using BackEnd2.CustomClass;
using BackEnd2.ViewModel;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace FrontEnd.View
{
    /// <summary>
    ///     Interaction logic for ClientViewModel.xaml
    /// </summary>
    [MvxContentPresentation]
    [MvxViewFor(typeof(ClientViewModel))]
    public partial class ClientView : MvxWpfView
    {
        private IMvxInteraction<YesNoQuestion> _ConfirmBox;

        private IMvxInteraction<string> _SentNote;

        public ClientView()
        {
            InitializeComponent();
          

        }
        
        public IMvxInteraction<YesNoQuestion> ConfirmBox
        {
            get => _ConfirmBox;
            set
            {
                if (_ConfirmBox != null)
                    _ConfirmBox.Requested -= ConfirmMsg;
                _ConfirmBox = value;
                _ConfirmBox.Requested += ConfirmMsg;
            }
        }

        public IMvxInteraction<string> SentNotification
        {
            get => _SentNote;
            set
            {
                if (_SentNote != null)
                    _SentNote.Requested -= DisplayMsg;
                _SentNote = value;
                _SentNote.Requested += DisplayMsg;
            }
        }

        public void ConfirmMsg(object sender, MvxValueEventArgs<YesNoQuestion> args)
        {
            var result = System.Windows.MessageBox.Show(args.Value.Question, "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                args.Value.UploadCallback(true);
            else
                args.Value.UploadCallback(false);
        }

        public void DisplayMsg(object sender, MvxValueEventArgs<string> args)
        {
            System.Windows.MessageBox.Show(args.Value);
        }

        private void MvxWpfView_Loaded(object sender, RoutedEventArgs e)
        {
            var set = this.CreateBindingSet<ClientView, ClientViewModel>();
            set.Bind(this).For(view => view.SentNotification).To(viewmodel => viewmodel.SendNotification);
            set.Bind(this).For(view => view.ConfirmBox).To(viewmodel => viewmodel.ConfirmAction);
            set.Apply();
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as System.Windows.Controls.ListView;
            var gView = listView.View as GridView;

            var workingWidth =
                listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 0.40;
            var col2 = 0.60;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
        }
    }
}