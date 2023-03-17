using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace FrontEnd.View
{
    /// <summary>
    ///     Interaction logic for FicheTekPart1.xaml
    /// </summary>
    public partial class FicheTekPart1 : UserControl
    {
        private static readonly Regex _regex = new Regex("[^0-9]+");
        private static readonly Regex _regexDouble = new Regex(@"^[0-9]*(?:\,[0-9]*)?$");
        private static readonly Regex _regexDouble2 = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");

        public FicheTekPart1()
        {
            InitializeComponent();
            Language = XmlLanguage.GetLanguage("fr-FR");
        }

        private static bool IsDouble(string text)
        {
            return !_regexDouble.IsMatch(text);
        }

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void CheckForDoubleValue(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsDouble(e.Text);
        }

        private void Pasting_CheckDoubleValue(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var text = (string)e.DataObject.GetData(typeof(string));
                if (!IsDouble(text)) e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void Comp1NbrFilAccept(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
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

        private void TextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
        }
    }
}