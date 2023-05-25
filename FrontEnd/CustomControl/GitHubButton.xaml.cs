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

namespace FrontEnd.CustomControl
{
    /// <summary>
    /// Interaction logic for GitHubButton.xaml
    /// </summary>
    public partial class GitHubButton : UserControl
    {
        public GitHubButton()
        {
            InitializeComponent();
        }


        public string BtnText
        {
            get { return (string)GetValue(BtnTextProperty); }
            set { SetValue(BtnTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BtnText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BtnTextProperty =
            DependencyProperty.Register("BtnText", typeof(string), typeof(GitHubButton), new PropertyMetadata(""));



        public ICommand BtnCmd
        {
            get { return (ICommand)GetValue(BtnCmdProperty); }
            set { SetValue(BtnCmdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BtnCmd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BtnCmdProperty =
            DependencyProperty.Register("BtnCmd", typeof(ICommand), typeof(GitHubButton), new PropertyMetadata());


    }
}
