using Microsoft.Xaml.Behaviors.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DSheetEnfilage
{
   partial class TrameSymbolStyle : ResourceDictionary
    {
        private void TrameDragger_DragFinished(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var df = (MouseDragElementBehavior)sender;
            
            //TranslateTransform myTranslate = new TranslateTransform();
            //myTranslate.X = df.X;
            //myTranslate.Y = df.Y;
            //StackPanel sp =(StackPanel) e.Source;
            //sp.RenderTransform = myTranslate;
        }
    }
}
