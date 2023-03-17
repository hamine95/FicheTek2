using System.Windows;
using System.Windows.Controls;

namespace DSheetEnfilage
{
    public class legendElement:Control
    {


        public static readonly DependencyProperty NameProperty = DependencyProperty.Register(
            nameof(Name), typeof(string), typeof(legendElement), new PropertyMetadata(null));

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
    }
}