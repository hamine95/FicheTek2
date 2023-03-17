using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DSheetEnfilage
{
    public class TrameSymbol : Control
    {


        public bool AllowDrag
        {
            get { return (bool)GetValue(AllowDragProperty); }
            set { SetValue(AllowDragProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowDragProperty =
            DependencyProperty.Register("AllowDrag", typeof(bool), typeof(TrameSymbol), new PropertyMetadata(default(bool)));


        public bool TrameXposition
        {
            get { return (bool)GetValue(TrameXpositionProperty); }
            set { SetValue(TrameXpositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TrameXposition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrameXpositionProperty =
            DependencyProperty.Register("TrameXposition", typeof(bool), typeof(TrameSymbol), new PropertyMetadata(default(bool)));


        public bool TrameYposition
        {
            get { return (bool)GetValue(TrameYpositionProperty); }
            set { SetValue(TrameYpositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TrameYposition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrameYpositionProperty =
            DependencyProperty.Register("TrameYposition", typeof(bool), typeof(TrameSymbol), new PropertyMetadata(default(bool)));

        private void DragEvent(object sender, System.Windows.Input.MouseEventArgs e)
        {


        }

    }
}
