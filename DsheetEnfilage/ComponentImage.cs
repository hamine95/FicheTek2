using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BackEnd2.Model;

namespace DSheetEnfilage
{
    public class ComponentImage:Image
    {

     

        public static readonly DependencyProperty IsPrintViewProperty = DependencyProperty.Register(
            nameof(IsPrintView), typeof(bool), typeof(ComponentImage), new PropertyMetadata(default(bool)));

        public bool IsPrintView
        {
            get { return (bool)GetValue(IsPrintViewProperty); }
            set { SetValue(IsPrintViewProperty, value); }
        }

        
        private int _y;

        public int y
        {
            get { return _y; }
            set
            {
                _y = value;
            }

        }
        private int _x;
        public int x
        {
            get { return _x; }
            set
            {
                _x = value;
            }

        }

        private int _position;

        public int position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                
            }
        }

        private int _ComplemntaryStageStartIndex;
        
        private int _ComplemntaryStageEndIndex;

        public int ComplemntaryStageStartIndex
        {
            get
            {
                return _ComplemntaryStageStartIndex;
            }
            set
            {
                _ComplemntaryStageStartIndex = value;
                
            }
        }
        public int ComplemntaryStageEndIndex
        {
            get
            {
                return _ComplemntaryStageEndIndex;
            }
            set
            {
                _ComplemntaryStageEndIndex = value;
                
            }
        }

        private int _StartIndex;

        public int StartIndex
        {
            get
            {
                return _StartIndex;
            }
            set
            {
                _StartIndex = value;
                
            }
        }
        private int _EndIndex;

        public int EndIndex
        {
            get
            {
                return _EndIndex;
            }
            set
            {
                _EndIndex = value;
                
            }
        }
        public static readonly DependencyProperty HighlightProperty = DependencyProperty.Register(
            nameof(Highlight), typeof(bool), typeof(ComponentImage), new PropertyMetadata(false));

        public bool Highlight
        {
            get { return (bool)GetValue(HighlightProperty); }
            set { SetValue(HighlightProperty, value); }
        }


        public static readonly DependencyProperty ChaineCellProperty = DependencyProperty.Register(
            nameof(ChaineCell), typeof(bool), typeof(Chaine), new PropertyMetadata(default(bool)));

        public bool ChaineCell
        {
            get { return (bool)GetValue(ChaineCellProperty); }
            set { SetValue(ChaineCellProperty, value); }
        }
        public static readonly DependencyProperty CellStateProperty = DependencyProperty.Register(
            nameof(CellState), typeof(ChaineMatrixElement.ComponentState), typeof(ComponentImage), new FrameworkPropertyMetadata( ChaineMatrixElement.ComponentState.Vacant,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty NumComposantProperty = DependencyProperty.Register(
            nameof(NumComposant), typeof(int), typeof(ComponentImage), new FrameworkPropertyMetadata( 0,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public int NumComposant
        {
            get { return (int)GetValue(NumComposantProperty); }
            set { SetValue(NumComposantProperty, value); }
        }

        public enum RepresentationStage
        {
            OutRange,
            StageSeperator,
            Lisse,
            Dent
        }
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            object obj = e.Source;
            if(obj.GetType()!=(typeof(ComponentImage)))
                return;
            var img = (ComponentImage)obj;
            if (img.CellState != ChaineMatrixElement.ComponentState.Inaccessibe &&
                img.ComponentStage!=RepresentationStage.OutRange
                )
            {
                img.Highlight = true;
                return;
            }
            var request = new TraversalRequest(FocusNavigationDirection.Next);
            img.MoveFocus(request);
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            var obj = e.Source;
            if (obj.GetType() == typeof(ComponentImage))
            {
                if(!ChaineCell)
                    return;
                ((ComponentImage)obj).Focus();
                if (((ComponentImage)obj).CellState == ChaineMatrixElement.ComponentState.Occupied)
                {
                    ((ComponentImage)obj).CellState = ChaineMatrixElement.ComponentState.Vacant;
                    ((ComponentImage)obj).NumComposant = 0;
                }
                else
                {
                    
                    ((ComponentImage)obj).CellState = ChaineMatrixElement.ComponentState.Occupied;
                    ((ComponentImage)obj).NumComposant = 1;
                }
                

            }
            base.OnMouseDown(e);
        }
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            object obj = e.Source;
            if(obj.GetType()!=(typeof(ComponentImage)))
                return;
            var img = (ComponentImage)obj;
            img.Highlight = false;
        }

        public static readonly DependencyProperty ComponentStageProperty = DependencyProperty.Register(
            nameof(ComponentStage), typeof(RepresentationStage), typeof(ComponentImage), new PropertyMetadata(default(RepresentationStage)));

        public RepresentationStage ComponentStage
        {
            get { return (RepresentationStage)GetValue(ComponentStageProperty); }
            set { SetValue(ComponentStageProperty, value); }
        }

       

        

        public ChaineMatrixElement.ComponentState CellState
        {
            get { return (ChaineMatrixElement.ComponentState)GetValue(CellStateProperty); }
            set { SetValue(CellStateProperty, value); }
        }

        
    }
}