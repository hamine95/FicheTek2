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

namespace DSheetEnfilage
{
    /// <summary>
    /// Interaction logic for EnfilageBoard.xaml
    /// </summary>
    public partial class EnfilageBoard : UserControl
    {
        private Point ClickPoint;
        public EnfilageBoard()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var canvas = sender as Canvas;
            if (canvas == null)
                return;

             HitTestResult hitTestResult = VisualTreeHelper.HitTest(canvas, e.GetPosition(canvas));
            ClickPoint = e.GetPosition(canvas);
            var element = hitTestResult.VisualHit;
            Image img = (Image)element;
            img.Source =new BitmapImage(new Uri(@"./Asset/squareOne.png", UriKind.Relative));
            Keyboard.Focus(img);
        }

        private void Canvas_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var canvas = sender as Canvas;
            if (canvas == null || ClickPoint == null)
                return;

            Vector vector1 = new Vector(12, 0);
            HitTestResult hitTestResult = VisualTreeHelper.HitTest(canvas, ClickPoint);
            var element = hitTestResult.VisualHit;
            Image img = (Image)element;
            img.Source = new BitmapImage(new Uri(@"./Asset/square.png", UriKind.Relative));
            ClickPoint = Point.Add(ClickPoint, vector1);
             hitTestResult = VisualTreeHelper.HitTest(canvas, ClickPoint);
             element = hitTestResult.VisualHit;
             img = (Image)element;
            img.Source = new BitmapImage(new Uri(@"./Asset/squareHighlight.png", UriKind.Relative));
            
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
