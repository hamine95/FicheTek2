using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BackEnd2.Model;
using Microsoft.Xaml.Behaviors.Core;

namespace DSheetEnfilage
{
    public class Chaine:Control,INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

       

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty CompListProperty = DependencyProperty.Register(
            nameof(CompList), typeof(ObservableCollection<Composant>), typeof(Chaine), new PropertyMetadata(null));

        public ObservableCollection<Composant> CompList
        {
            get { return (ObservableCollection<Composant>)GetValue(CompListProperty); }
            set { SetValue(CompListProperty, value); }
        }

        public static readonly DependencyProperty SelectedCompProperty = DependencyProperty.Register(
            nameof(SelectedComp), typeof(Composant), typeof(Chaine), new PropertyMetadata(default(Composant),OnSetSelectedComposant));

        public Composant SelectedComp
        {
            get { return (Composant)GetValue(SelectedCompProperty); }
            set { SetValue(SelectedCompProperty, value);NotifyPropertyChanged(); }
        }

        public static void OnSetSelectedComposant(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var mycontrol = (Chaine)obj;
            mycontrol.SetSelectedComposant();
        }

        public void SetSelectedComposant()
        {
            if(SelectedComp==null || SelectedChaineColumn==0)
                return;
            if (ChaineColumnList == null)
                ChaineColumnList = new ObservableCollection<ChColComp>();
            var chCol = ChaineColumnList.FirstOrDefault(chcol => chcol.ColNum == SelectedChaineColumn);
            if (chCol != null)
            {
                chCol.Comp = SelectedComp;
                return;
            }
            ChaineColumnList.Add(new ChColComp()
            {
                Comp = SelectedComp,
                ColNum =SelectedChaineColumn
                
            });
        }

        

        private ObservableCollection<ChColComp> _ChaineColumnList;

        public ObservableCollection<ChColComp> ChaineColumnList
        {
            get
            {
                return _ChaineColumnList;
            }
            set
            {
                _ChaineColumnList = value;
                NotifyPropertyChanged();
            }
        }

        private int _SelectedChaineColumn;
        public int SelectedChaineColumn
        {
            get { return _SelectedChaineColumn; }
            set { _SelectedChaineColumn= value; NotifyPropertyChanged();}
        }




        public Chaine()
        {
            CommandBindings.Add(new CommandBinding(ChaineColumnCommand, ClickedChaineColumn)); 
        }

        

        private void ClickedChaineColumn(object sender, ExecutedRoutedEventArgs e)
        {
            SelectedChaineColumn = Convert.ToInt32(e.Parameter);
            SelectedComp = null;
        }  
        private static ICommand _ChaineColumnCommand=new RoutedCommand();

        public static ICommand ChaineColumnCommand
        {
            get => _ChaineColumnCommand;
        }


        public static readonly DependencyProperty isPrintProperty = DependencyProperty.Register(
            nameof(isPrint), typeof(bool), typeof(Chaine), new PropertyMetadata(default(bool)));

        public bool isPrint
        {
            get { return (bool)GetValue(isPrintProperty); }
            set { SetValue(isPrintProperty, value); }
        }

        public static readonly DependencyProperty SelectedCompIndexProperty = DependencyProperty.Register(
            nameof(SelectedCompIndex), typeof(int), typeof(Chaine), new PropertyMetadata(default(int),OnSelectedCompIndex));

        public int SelectedCompIndex
        {
            get { return (int)GetValue(SelectedCompIndexProperty); }
            set { SetValue(SelectedCompIndexProperty, value); }
        }

        public static void OnSelectedCompIndex(DependencyObject obj,DependencyPropertyChangedEventArgs args)
        {
            
        }
        
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            var obj = e.Source;
            if (obj.GetType() == typeof(ComponentImage))
            {
                ((ComponentImage)obj).Focus();
                ((ComponentImage)obj).CellState = ChaineMatrixElement.ComponentState.Occupied;
                ((ComponentImage)obj).NumComposant = 1;

            }
                base.OnMouseDown(e);
        }

        public int ChaineColumns { get; set; }
        
        public int ChaineRows2 { get; set; }

        private ObservableCollection<ChaineMatrixElement> _ChList2;

        public static readonly DependencyProperty ChList2Property = DependencyProperty.Register(
            nameof(ChList2), typeof(ObservableCollection<ChaineMatrixElement>), typeof(Chaine), new PropertyMetadata(null));

        public ObservableCollection<ChaineMatrixElement> ChList2
        {
            get { return (ObservableCollection<ChaineMatrixElement>)GetValue(ChList2Property); }
            set { SetValue(ChList2Property, value); }
        }

        public static readonly DependencyProperty ChListProperty = DependencyProperty.Register(
            nameof(ChList), typeof(ObservableCollection<ChaineMatrixElement>), typeof(Chaine), new PropertyMetadata(null));

        public ObservableCollection<ChaineMatrixElement> ChList
        {
            get { return (ObservableCollection<ChaineMatrixElement>)GetValue(ChListProperty); }
            set { SetValue(ChListProperty, value); }
        }


        public static readonly DependencyProperty EnableChaineEditProperty = DependencyProperty.Register(
            nameof(EnableChaineEdit), typeof(bool), typeof(Chaine), new PropertyMetadata(false));

        public bool EnableChaineEdit
        {
            get { return (bool)GetValue(EnableChaineEditProperty); }
            set { SetValue(EnableChaineEditProperty, value); }
        }


        public static readonly DependencyProperty ChainColNumProperty = DependencyProperty.Register(
            nameof(ChainColNum), typeof(int), typeof(Chaine), new PropertyMetadata(8));

        public int ChainColNum
        {
            get { return (int)GetValue(ChainColNumProperty); }
            set { SetValue(ChainColNumProperty, value); }
        }


        public static readonly DependencyProperty HeightRapportBoxProperty = DependencyProperty.Register(
            nameof(HeightRapportBox), typeof(double), typeof(Chaine), new PropertyMetadata(default(double)));

        public double HeightRapportBox
        {
            get { return (double)GetValue(HeightRapportBoxProperty); }
            set { SetValue(HeightRapportBoxProperty, value); }
        }

        public static readonly DependencyProperty CellWidthProperty = DependencyProperty.Register(
            nameof(CellWidth), typeof(double), typeof(Chaine), new PropertyMetadata((double)12,OnSetCellWidth));

        public double CellWidth
        {
            get { return (double)GetValue(CellWidthProperty); }
            set { SetValue(CellWidthProperty, value);NotifyPropertyChanged(); }
        }

        public static void OnSetCellWidth(DependencyObject obj,DependencyPropertyChangedEventArgs args)
        {
            var mycontrol = (Chaine)obj;
            mycontrol.SetRapportHeight();
        }

        public void SetRapportHeight()
        {
            HeightRapportBox = CellWidth * 3;
        }
        public static readonly DependencyProperty Btn9VisProperty = DependencyProperty.Register(
            nameof(Btn9Vis), typeof(bool), typeof(Chaine), new PropertyMetadata(false));

        public bool Btn9Vis
        {
            get { return (bool)GetValue(Btn9VisProperty); }
            set { SetValue(Btn9VisProperty, value); }
        }
        public static readonly DependencyProperty Btn10VisProperty = DependencyProperty.Register(
            nameof(Btn10Vis), typeof(bool), typeof(Chaine), new PropertyMetadata(false));

        public bool Btn10Vis
        {
            get { return (bool)GetValue(Btn10VisProperty); }
            set { SetValue(Btn10VisProperty, value); }
        }
       
        
        public static readonly DependencyProperty IsDisplayChainProperty = DependencyProperty.Register(
            nameof(IsDisplayChain), typeof(bool), typeof(Chaine), new PropertyMetadata(true));

        public bool IsDisplayChain
        {
            get { return (bool)GetValue(IsDisplayChainProperty); }
            set { SetValue(IsDisplayChainProperty, value); }
        }

        public static readonly DependencyProperty ChaineRowSumProperty = DependencyProperty.Register(
            nameof(ChaineRowSum), typeof(int), typeof(Chaine), new PropertyMetadata(default(int)));

        public int ChaineRowSum
        {
            get { return (int)GetValue(ChaineRowSumProperty); }
            set { SetValue(ChaineRowSumProperty, value); }
        }
        
       
        
        public static readonly DependencyProperty AngleChainProperty = DependencyProperty.Register(
            nameof(AngleChain), typeof(double), typeof(Chaine), new PropertyMetadata(null));

        public double AngleChain
        {
            get { return (double)GetValue(AngleChainProperty); }
            set { SetValue(AngleChainProperty, value); }
        }
        
        public static readonly DependencyProperty ChainRotateXProperty = DependencyProperty.Register(
            nameof(ChainRotateX), typeof(double), typeof(Chaine), new PropertyMetadata(null));

        public double ChainRotateX
        {
            get { return (double)GetValue(ChainRotateXProperty); }
            set { SetValue(ChainRotateXProperty, value); }
        }

        public static readonly DependencyProperty SecChainVisProperty = DependencyProperty.Register(
            nameof(SecChainVis), typeof(bool), typeof(Chaine), new PropertyMetadata(default(bool)));

        public bool SecChainVis
        {
            get { return (bool)GetValue(SecChainVisProperty); }
            set { SetValue(SecChainVisProperty, value); }
        }
        
        public static readonly DependencyProperty ChainRotateYProperty = DependencyProperty.Register(
            nameof(ChainRotateY), typeof(double), typeof(Chaine), new PropertyMetadata(null));

        public double ChainRotateY
        {
            get { return (double)GetValue(ChainRotateYProperty); }
            set { SetValue(ChainRotateYProperty, value); }
        }
    }
}