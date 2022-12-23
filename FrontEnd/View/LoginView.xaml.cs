using System.Windows;
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
    [MvxViewFor(typeof(LoginViewModel))]
    public partial class LoginView : MvxWindow
    {
        private IMvxInteraction<YesNoQuestion> _ConfirmBox;

        private IMvxInteraction<string> _SentNote;
        public LoginView()
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


        private void LoginView_OnLoaded(object sender, RoutedEventArgs e)
        {
            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(this).For(view => view.SentNotification).To(viewmodel => viewmodel.SendNotification);
            set.Bind(this).For(view => view.ConfirmBox).To(viewmodel => viewmodel.ConfirmAction);
            set.Apply();
        }
    }
}