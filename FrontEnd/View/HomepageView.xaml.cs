using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using BackEnd2.ViewModel;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace FrontEnd.View
{
    /// <summary>
    ///     Interaction logic for HomepageView.xaml
    /// </summary>
    [MvxWindowPresentation]
    [MvxViewFor(typeof(HomepageViewModel))]
    public partial class HomepageView : MvxWindow
    {
        public HomepageView()
        {
            InitializeComponent();
        }

        private bool expanded;
        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Handle single leftbutton mouse clicks
            if (e.ClickCount < 2 && e.LeftButton == MouseButtonState.Pressed)
            {
                
                    if (expanded == false)
                        ((StackPanel)Template.FindName("SidePanel",HomeP)).BeginStoryboard((Storyboard)this.Resources["expandStoryBoard"]);
                    else
                        ((StackPanel)Template.FindName("SidePanel",HomeP)).BeginStoryboard((Storyboard)this.Resources["collapseStoryBoard"]);
 
                    expanded = !expanded;
               
            }
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
         
        }
    }
}