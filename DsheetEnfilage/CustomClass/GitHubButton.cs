using System.Windows;
using System.Windows.Controls;

namespace DSheetEnfilage.CustomClass
{
    public class GitHubButton: Button
    {
        static GitHubButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GitHubButton), new FrameworkPropertyMetadata(typeof(GitHubButton)));
        }
    }
}