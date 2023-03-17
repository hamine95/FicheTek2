using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using BackEnd2.ViewModel;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace FrontEnd.View
{
    [MvxContentPresentation]
    [MvxViewFor(typeof(RapportViewModel))]
    public partial class RapportView : MvxWpfView
    {
        public RapportView()
        {
            InitializeComponent();
        }

       
    }
}