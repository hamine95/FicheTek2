using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using BackEnd2.Model;

namespace DSheetEnfilage
{
    /// <summary>
    ///     Interaction logic for EnfilageSchemas.xaml
    /// </summary>
    public partial class EnfilageSchemas : UserControl, INotifyPropertyChanged
    {
        // Using a DependencyProperty as the backing store for SelectedComposant.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedComposantProperty =
            DependencyProperty.Register("SelectedComposant", typeof(Composition), typeof(EnfilageSchemas),
                new PropertyMetadata(null, OnSetSelectedComposant));


        // Using a DependencyProperty as the backing store for ListComposant.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListComposantProperty =
            DependencyProperty.RegisterAttached("ListComposant", typeof(ObservableCollection<Composition>),
                typeof(EnfilageSchemas), new PropertyMetadata(null, OnSetComposantList));

        public static DependencyProperty SetRowProperty = DependencyProperty.RegisterAttached("SetRow", typeof(object),
            typeof(EnfilageSchemas), new PropertyMetadata(0, OnSetRow));

        public static readonly DependencyProperty SetColumnProperty =
            DependencyProperty.RegisterAttached("SetColumn", typeof(object), typeof(EnfilageSchemas),
                new PropertyMetadata(0, OnSetColumn));

        // Using a DependencyProperty as the backing store for SelectedChaineValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedChaineValueProperty =
            DependencyProperty.Register("SelectedChaineValue", typeof(UpDownCompo), typeof(EnfilageSchemas),
                new PropertyMetadata(null, OnSetSelectedChaineValue));

        public static readonly DependencyProperty ChaineValueProperty =
            DependencyProperty.Register("Chaine", typeof(ChaineBoardStructure), typeof(EnfilageSchemas),
                new PropertyMetadata(null, OnSetChaine));
        public static readonly DependencyProperty ChaineListValueProperty =
            DependencyProperty.Register("ChaineList", typeof(ObservableCollection<ChaineMatrixElement>), typeof(EnfilageSchemas),
                new PropertyMetadata(null, OnSetChaine));
        public static readonly DependencyProperty ChaineColumnsValueProperty =
            DependencyProperty.Register("ChaineColumns", typeof(object), typeof(EnfilageSchemas),
                new PropertyMetadata(null, OnSetChaine));
        public static readonly DependencyProperty ChaineRowsValueProperty =
            DependencyProperty.Register("ChaineRows", typeof(object), typeof(EnfilageSchemas),
                new PropertyMetadata(null, OnSetChaine));
        
        public static readonly DependencyProperty EnfilageBoardProperty =
            DependencyProperty.Register("BoardItems", typeof(ObservableCollection<MatrixElement>), typeof(EnfilageSchemas),
                new PropertyMetadata(null, OnSetChaine));
        
        private string _ArmureColumns;

        private string _ArmureRows;


        public BoardStructure _board;

        private ChaineBoardStructure _Chaine;


        private string _FilDent;

        private string _NumDents;
        private string _Rep;
        private int _Ro;


        public EnfilageSchemas()
        {
            InitializeComponent();
            //DataContext = this;

            //Chaine = new ChaineBoardStructure(Convert.ToInt32(ChaineRows), Convert.ToInt32(ChaineColumns));
            // ChaineList = new ObservableCollection<ChaineMatrixElement>(Chaine.Board);
            //ChaineBoard.ItemsSource = ChaineList;
           
           // BoardItems = new ObservableCollection<MatrixElement>(_board.Board);
        }


        public object SetColumn
        {
            get => GetValue(SetColumnProperty);
            set
            {
                SetValue(SetColumnProperty, value);
                NotifyPropertyChanged();
            }
        }

        public object SetRow
        {
            get => GetValue(SetRowProperty);
            set
            {
                SetValue(SetRowProperty, value);
                NotifyPropertyChanged();
            }
        }

        public int Ro
        {
            get => _Ro;
            set
            {
                _Ro = value;
                NotifyPropertyChanged();
            }
        }

        public int ChaineColumns
        {
            get => (int)GetValue(ChaineColumnsValueProperty);
            set => SetValue(ChaineColumnsValueProperty, value);

        }

        public string NumDents
        {
            get => _NumDents;
            set
            {
                _NumDents = value;
                NotifyPropertyChanged();
            }
        }

        public string Rep
        {
            get => _Rep;
            set
            {
                _Rep = value;
                NotifyPropertyChanged();
            }
        }

        public string FilDent
        {
            get => _FilDent;
            set
            {
                _FilDent = value;
                NotifyPropertyChanged();
            }
        }

        public int ChaineRows
        {
            get => (int)GetValue(ChaineRowsValueProperty);
            set => SetValue(ChaineRowsValueProperty, value);

        }

        public string ArmureColumns
        {
            get => _ArmureColumns;
            set
            {
                _ArmureColumns = value;
                NotifyPropertyChanged();
            }
        }

        public string ArmureRows
        {
            get => _ArmureRows;
            set
            {
                _ArmureRows = value;
                NotifyPropertyChanged();
            }
        }


        public ObservableCollection<Composition> ListComposant
        {
            get => (ObservableCollection<Composition>)GetValue(ListComposantProperty);
            set => SetValue(ListComposantProperty, value);
        }


        public Composition SelectedComposant
        {
            get => (Composition)GetValue(SelectedComposantProperty);
            set => SetValue(SelectedComposantProperty, value);
        }

        public ObservableCollection<MatrixElement> BoardItems
        {
            get =>(ObservableCollection<MatrixElement>)GetValue(EnfilageBoardProperty);
            set => SetValue(EnfilageBoardProperty, value);
        }

        public ChaineBoardStructure Chaine
        {
            get => _Chaine;
            set
            {
                _Chaine = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<ChaineMatrixElement> ChaineList
        {
            get => (ObservableCollection<ChaineMatrixElement>)GetValue(ChaineListValueProperty);
            set => SetValue(ChaineListValueProperty, value);
        }

        public MatrixElement SelectedCell { get; set; }


        public UpDownCompo SelectedChaineValue
        {
            get => (UpDownCompo)GetValue(SelectedChaineValueProperty);
            set => SetValue(SelectedChaineValueProperty, value);
        }

        public ChaineMatrixElement SelectedChaineCell { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Control> AllChildren(DependencyObject parent)
        {
            var _List = new List<Control>();
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);
                if (_Child is Control)
                    _List.Add(_Child as Control);
                _List.AddRange(AllChildren(_Child));
            }

            return _List;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private static void OnSetChaine(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnSetSelectedComposant(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnSetComposantList(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }


        private static void OnSetRow(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // some code here
            var UserControl1Control = d as EnfilageSchemas;
            UserControl1Control.OnSetRowChanged(e);
        }

        private void OnSetRowChanged(DependencyPropertyChangedEventArgs e)
        {
            _board = new BoardStructure(Convert.ToInt32(SetRow), Convert.ToInt32(SetColumn));

            //BoardItems = new ObservableCollection<MatrixElement>(_board.Board);


            // foreach (var item in Board.Items)
            // {
            //     var _Container = Board.ItemContainerGenerator
            //         .ContainerFromItem(item);
            //     var _Children = AllChildren(_Container);
            //     var hei = _Children[0].ActualHeight;
            //     var wid = _Children[0].ActualWidth;
            // }
        }

        private static void OnSetColumn(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // some code here
            var UserControl1Control = d as EnfilageSchemas;
            UserControl1Control.OnSetColumnChanged(e);
        }

        private void OnSetColumnChanged(DependencyPropertyChangedEventArgs e)
        {
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedCell != null)
                SelectedCell.IsSelected = false;
            var border = (Border)sender;
            SelectedCell = (MatrixElement)border.Tag;
            if (SelectedCell.Content == null)
            {
                if (SelectedComposant != null) SelectedCell.Content = SelectedComposant;
            }
            else
            {
                if (SelectedComposant != null)
                {
                    if (SelectedCell.Content == SelectedComposant)
                        SelectedCell.Content = null;
                    else
                        SelectedCell.Content = SelectedComposant;
                }
                else
                {
                    SelectedCell.Content = null;
                }
            }
        }



        private void Border_GotFocus(object sender, RoutedEventArgs e)
        {
            var border = (Border)sender;
            SelectedCell = (MatrixElement)border.Tag;
            SelectedCell.IsSelected = true;
        }

        private void Border_LostFocus(object sender, RoutedEventArgs e)
        {
            var border = (Border)sender;
            SelectedCell = (MatrixElement)border.Tag;
            SelectedCell.IsSelected = false;
        }


        public void MoveUp()
        {
            SelectedCell.IsSelected = false;
            if (SelectedCell.Y == 0)
                SelectedCell = _board.Board.Find(b => b.Y == Convert.ToInt32(SetRow) - 1 && b.X == SelectedCell.X);
            else
                SelectedCell = _board.Board.Find(b => b.Y == SelectedCell.Y - 1 && b.X == SelectedCell.X);

            SelectedCell.IsSelected = true;
        }

        public void MoveDown()
        {
            SelectedCell.IsSelected = false;
            if (SelectedCell.Y == Convert.ToInt32(SetRow) - 1)
                SelectedCell = _board.Board.Find(b => b.Y == 0 && b.X == SelectedCell.X);
            else
                SelectedCell = _board.Board.Find(b => b.Y == SelectedCell.Y + 1 && b.X == SelectedCell.X);

            SelectedCell.IsSelected = true;
        }

        public void MoveRight()
        {
            SelectedCell.IsSelected = false;
            if (SelectedCell.X == Convert.ToInt32(SetColumn) - 1)
                SelectedCell = _board.Board.Find(b => b.Y == SelectedCell.Y && b.X == 0);
            else
                SelectedCell = _board.Board.Find(b => b.Y == SelectedCell.Y && b.X == SelectedCell.X + 1);

            SelectedCell.IsSelected = true;
        }

        public void MoveLeft()
        {
            SelectedCell.IsSelected = false;
            if (SelectedCell.X == 0)
                SelectedCell = _board.Board.Find(b => b.Y == SelectedCell.Y && b.X == Convert.ToInt32(SetColumn) - 1);
            else
                SelectedCell = _board.Board.Find(b => b.Y == SelectedCell.Y && b.X == SelectedCell.X - 1);

            SelectedCell.IsSelected = true;
        }

        private void Board_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                MoveUp();
            else if (e.Key == Key.Down)
                MoveDown();
            else if (e.Key == Key.Right)
                MoveRight();
            else if (e.Key == Key.Left)
                MoveLeft();
            else if (e.Key == Key.D1)
                SetContent(1);

            else if (e.Key == Key.D2)
                SetContent(2);
            else if (e.Key == Key.D3)
                SetContent(3);
            else if (e.Key == Key.D4)
                SetContent(4);
            else if (e.Key == Key.D5)
                SetContent(5);
            else if (e.Key == Key.D6)
                SetContent(6);
            else if (e.Key == Key.D7) SetContent(7);
        }

        public void SetContent(int Num)
        {
            if (ListComposant.Count >= Num) SelectedCell.Content = ListComposant[Num - 1];
        }

        private void ScrollViewer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right) e.Handled = true;
        }

        private void Border_LostFocus_1(object sender, RoutedEventArgs e)
        {
        }

        private void Border_GotFocus_1(object sender, RoutedEventArgs e)
        {
        }

        private static void OnSetSelectedChaineValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("printPreview.xps")) File.Delete("printPreview.xps");
            var xpsDocument = new XpsDocument("printPreview.xps", FileAccess.ReadWrite);
            var writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            var packagePolicy = new XpsPackagingPolicy(xpsDocument);
            var serializationMgr = new XpsSerializationManager(packagePolicy, false);
            serializationMgr.SaveAsXaml(FicheTec);
            var hei = Board.ActualHeight;
            var wid = Board.ActualWidth;
            var fixedDocSeq = xpsDocument.GetFixedDocumentSequence();
            //writer.Write((fixedDocSeq).DocumentPaginator);
            //DocumentPaginator paginator = ((IDocumentPaginatorSource)xpsDocument.GetFixedDocumentSequence()).DocumentPaginator;
            var Document = xpsDocument.GetFixedDocumentSequence();
            xpsDocument.Close();
            //var windows = new PrintWindow(Document);
            //windows.ShowDialog();
        }

        private void EnfilageCell_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Chaine = new ChaineBoardStructure(Convert.ToInt32(ArmureColumns), Convert.ToInt32(ArmureRows));
            ChaineList = new ObservableCollection<ChaineMatrixElement>(Chaine.Board);
            ChaineBoard.ItemsSource = ChaineList;

            ChaineColumns =Convert.ToInt32( ArmureColumns);
            ChaineRows =Convert.ToInt32(  ArmureRows);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var H = 1;
            var FilD = 1;
            var Haut = true;
            var TotalNbrFil = 0;
            foreach (var comp in ListComposant) TotalNbrFil = TotalNbrFil + Convert.ToInt32(comp.NbrFil);

            var ChaineObstacle = 4 + Convert.ToInt32(ChaineRows);
            var LegendObstacle = 2 * ListComposant.Count;
            var LongObstacle = 0;
            var SmallObstacle = 0;
            var NbrFilCapacity = 0;
            if (ChaineObstacle > LegendObstacle)
            {
                LongObstacle = ChaineObstacle;
                SmallObstacle = LegendObstacle;
            }
            else
            {
                LongObstacle = LegendObstacle;
                SmallObstacle = ChaineObstacle;
            }

            var FullColumns = Convert.ToInt32(SetRow) - LongObstacle;
            var FullColNbrFil = FullColumns / (Convert.ToInt32(ChaineColumns) + 4) * Convert.ToInt32(SetColumn);
            var RemainedRows = 0;
            var SumBitch = Convert.ToInt32(ChaineColumns) + 4;
            Math.DivRem(Convert.ToInt32(FullColumns), SumBitch, out RemainedRows);
            if (LongObstacle - SmallObstacle + RemainedRows >= Convert.ToInt32(ChaineColumns) + 4)
            {
                var FirstObstacle = LongObstacle - SmallObstacle + RemainedRows;
                var NbrFilFirstObst = FirstObstacle / (Convert.ToInt32(ChaineColumns) + 4) *
                                      (Convert.ToInt32(SetColumn) - 8);
                RemainedRows = FirstObstacle % (Convert.ToInt32(ChaineColumns) + 4);
                var SecondObstacle = SmallObstacle + RemainedRows;
                var NbrFilSecObst = SecondObstacle / (Convert.ToInt32(ChaineColumns) + 4) *
                                    (Convert.ToInt32(SetColumn) - 16);
                NbrFilCapacity = FullColNbrFil + NbrFilFirstObst + NbrFilSecObst;
            }
            else
            {
                var ObstacleRows = LongObstacle + RemainedRows;
                var ObstacleColNbrFil = ObstacleRows / (Convert.ToInt32(ChaineColumns) + 4) *
                                        (Convert.ToInt32(SetColumn) - 16);
                NbrFilCapacity = FullColNbrFil + ObstacleColNbrFil;
            }


            if (TotalNbrFil > NbrFilCapacity)
            {
                MessageBox.Show("Espace de Dessin insiffusant, essayez de le compresser on evitant les répétitions",
                    "Espace Dessin Insiffusant", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var FirstObstacle = true;
                var ObstaclePart = 0;
                for (var i = 1; i <= Convert.ToInt32(ListComposant[0].NbrFil); i++)
                {
                    int DepartX;
                    var ChaineObstacleY = Convert.ToInt32(ChaineRows) + 4;
                    var ChaineObstacleX = 8;
                    var ReachObstacle = Convert.ToInt32(SetRow) - ChaineObstacleY;

                    Math.DivRem(i, Convert.ToInt32(SetColumn), out DepartX);
                    var Quo = i / Convert.ToInt32(SetColumn);
                    var DepartY = (Quo + 1) * (Convert.ToInt32(ChaineColumns) + 1) + 3 * Quo;

                    if (DepartY + 2 >= ReachObstacle)
                    {
                        if (FirstObstacle) ObstaclePart = i;

                        FirstObstacle = false;
                        Math.DivRem(i - ObstaclePart, Convert.ToInt32(SetColumn) - ChaineObstacleX, out DepartX);
                        DepartX = DepartX + ChaineObstacleX;
                        Quo = (i + 8) / Convert.ToInt32(SetColumn);
                        DepartY = (Quo + 1) * (Convert.ToInt32(ChaineColumns) + 1) + 3 * Quo;
                    }

                    if (H > 4) H = 1;
                    if (FilD > Convert.ToInt32(FilDent))
                    {
                        FilD = 1;
                        Haut = !Haut;
                    }

                    if (Haut)
                    {
                        _board.Board.Find(bo => bo.Y == DepartY + 2 && bo.X == DepartX).Content = ListComposant[0];
                        _board.Board.Find(bo => bo.Y == DepartY - H && bo.X == DepartX).Content = ListComposant[0];
                    }
                    else
                    {
                        _board.Board.Find(bo => bo.Y == DepartY + 1 && bo.X == DepartX).Content = ListComposant[0];
                        _board.Board.Find(bo => bo.Y == DepartY - H && bo.X == DepartX).Content = ListComposant[0];
                    }

                    H++;
                    FilD++;
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Etes-vous sur de vouloir effacer tous ?", "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
                foreach (var element in _board.Board)
                    if (element.Content != null)
                        element.Content = null;
        }

        private void EnfilageSchemas_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ChaineComposant = new UpDownCompo();
            ChaineComposant.BKComposant = "Black";
            ChaineComposant.BKBorderComposant = "White";
            SelectedChaineValue = ChaineComposant;
            
            
            
            _board = new BoardStructure(59, 83);
            Board.ItemsSource=new ObservableCollection<MatrixElement>(_board.Board);
        }
    }

    public class GroupNameGenerator : IValueConverter
    {
        public GroupNameGenerator()
        {
            Index = 0;
        }

        public int Index { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format("Group{0}", ++Index);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}