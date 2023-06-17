using BackEnd2.ViewModel;
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
using System.Windows.Shapes;

namespace FrontEnd.View
{
    /// <summary>
    /// Interaction logic for EnfilageCorrectionView.xaml
    /// </summary>
    [MvxWindowPresentation]
    [MvxViewFor(typeof(EnfilageCorrectionViewModel))]
    public partial class EnfilageCorrectionView : MvxWindow
    {
        public EnfilageCorrectionView()
        {
            InitializeComponent();
        }
    }
}
