using BackEnd2.CustomClass;
using BackEnd2.ViewModel;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrontEnd.View
{
    /// <summary>
    /// Interaction logic for ParamView.xaml
    /// </summary>
    [MvxContentPresentation]
    [MvxViewFor(typeof(ParamViewModel))]
    public partial class ParamView : MvxWpfView
    {

        private IMvxInteraction<YesNoQuestion> _ConfirmBox;

        private IMvxInteraction<string> _SentNote;

        public ParamView()
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

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gView = listView.View as GridView;

            var workingWidth =
                listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 0.20;
            var col2 = 0.50;
            var col3 = 0.30;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[1].Width = workingWidth * col3;
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
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var set = this.CreateBindingSet<ParamView, ParamViewModel>();
            set.Bind(this).For(view => view.SentNotification).To(viewmodel => viewmodel.SendNotification);
            set.Bind(this).For(view => view.ConfirmBox).To(viewmodel => viewmodel.ConfirmAction);
            set.Apply();
        }
    }
}
