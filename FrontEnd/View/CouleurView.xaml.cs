﻿using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
    ///     Interaction logic for CouleurView.xaml
    /// </summary>
    [MvxContentPresentation]
    [MvxViewFor(typeof(CouleurViewModel))]
    public partial class CouleurView : MvxWpfView
    {
        private static readonly Regex _regex = new Regex("[^0-9]+");
        private IMvxInteraction<YesNoQuestion> _ConfirmBox;

        private IMvxInteraction<string> _SentNote;

        public CouleurView()
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

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
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
            var set = this.CreateBindingSet<CouleurView, CouleurViewModel>();
            set.Bind(this).For(view => view.SentNotification).To(viewmodel => viewmodel.SendNotification);
            set.Bind(this).For(view => view.ConfirmBox).To(viewmodel => viewmodel.ConfirmAction);
            set.Apply();
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gView = listView.View as GridView;

            var workingWidth =
                listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 0.40;
            var col2 = 0.60;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void DataObject_OnPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text)) e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}