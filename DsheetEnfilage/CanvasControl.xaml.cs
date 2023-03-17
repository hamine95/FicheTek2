using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using BackEnd2.CustomClass;
using BackEnd2.Model;
using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Layout;

namespace DSheetEnfilage
{
    public partial class CanvasControl : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty SecondRectProperty = DependencyProperty.Register(
            nameof(SecondRect), typeof(SecRectangle), typeof(CanvasControl), new PropertyMetadata(null));

        public static readonly DependencyProperty AngleChainProperty = DependencyProperty.Register(
            nameof(AngleChain), typeof(int), typeof(CanvasControl), new PropertyMetadata(0, SetAngleChain));

        public static readonly DependencyProperty ChainRotateYProperty = DependencyProperty.Register(
            nameof(ChainRotateY), typeof(int), typeof(CanvasControl), new PropertyMetadata(0, SetChainRotateY));

        public static readonly DependencyProperty ChainRotateXProperty = DependencyProperty.Register(
            nameof(ChainRotateX), typeof(int), typeof(CanvasControl), new PropertyMetadata(0, SetChainRotateX));

        public static readonly DependencyProperty WorkinRectProperty = DependencyProperty.Register(
            nameof(WorkinRect), typeof(WorkRectangle), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetRectWork));

        public static readonly DependencyProperty CompListProperty =
            DependencyProperty.RegisterAttached("CompList", typeof(List<Composant>),
                typeof(CanvasControl), new PropertyMetadata(null, OnSetCompList));

        public static readonly DependencyProperty SelectedChaineProperty = DependencyProperty.Register(
            nameof(SelectedChaine), typeof(chaine), typeof(CanvasControl),
            new PropertyMetadata(null, OnsetSelectedChaine));

        public static readonly DependencyProperty SelectedColCompProperty = DependencyProperty.Register(
            nameof(SelectedColComp), typeof(ChColComp), typeof(CanvasControl),
            new PropertyMetadata(null, OnsetChaineColComp));

        public static readonly DependencyProperty enfilageElementProperty = DependencyProperty.Register(
            nameof(enfilageElement), typeof(EnfilageElement), typeof(CanvasControl),
            new PropertyMetadata(default(EnfilageElement)));

        public static readonly DependencyProperty EnableChaineEditProperty = DependencyProperty.Register(
            nameof(EnableChaineEdit), typeof(bool), typeof(CanvasControl),
            new PropertyMetadata(default(bool), OnStartEditing));


        public static readonly DependencyProperty RangsProperty = DependencyProperty.Register(
            nameof(Rangs), typeof(LinkedList<Rang>), typeof(CanvasControl), new PropertyMetadata(null, OnSetRang));

        public static readonly DependencyProperty RogueDentListProperty = DependencyProperty.Register(
            nameof(RogueDentList), typeof(LinkedList<LinkedList<MatrixElement>>), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetRogueDentList));

        public static readonly DependencyProperty DentListProperty = DependencyProperty.Register(
            nameof(DentList), typeof(LinkedList<LinkedList<MatrixElement>>), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetDentList));

        public static readonly DependencyProperty NbrDentProperty = DependencyProperty.Register(
            nameof(NbrDent), typeof(int), typeof(CanvasControl), new PropertyMetadata(default(int)));

        public static readonly DependencyProperty IsDentFilProperty = DependencyProperty.Register(
            nameof(IsDentFil), typeof(bool), typeof(CanvasControl),
            new PropertyMetadata(default(bool), OnSetIsDentFil));

        public static readonly DependencyProperty TrameXpositionProperty = DependencyProperty.Register(
            nameof(TrameXposition), typeof(string), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetTramePosition));

        public static readonly DependencyProperty TrameYpositionProperty = DependencyProperty.Register(
            nameof(TrameYposition), typeof(string), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetTramePosition));

        public static readonly DependencyProperty LastXpositionProperty = DependencyProperty.Register(
            nameof(LastXposition), typeof(string), typeof(CanvasControl), new PropertyMetadata(null, OnSetPosition));

        public static readonly DependencyProperty LastYpositionProperty = DependencyProperty.Register(
            nameof(LastYposition), typeof(string), typeof(CanvasControl), new PropertyMetadata(null, OnSetPosition));

        public static readonly DependencyProperty ChaineList2ValueProperty = DependencyProperty.Register(
            nameof(ChaineList2), typeof(ObservableCollection<ChaineMatrixElement>), typeof(CanvasControl),
            new PropertyMetadata(null, null));

        public static readonly DependencyProperty ChaineListValueProperty =
            DependencyProperty.Register("ChaineList", typeof(ObservableCollection<ChaineMatrixElement>),
                typeof(CanvasControl),
                new PropertyMetadata(null, null));

        public static readonly DependencyProperty ChaineRows2Property = DependencyProperty.Register(
            nameof(ChaineRows2), typeof(int), typeof(CanvasControl), new PropertyMetadata(0, null));

        public static readonly DependencyProperty ChRowSumProperty = DependencyProperty.Register(
            nameof(ChRowSum), typeof(int), typeof(CanvasControl), new PropertyMetadata(0, OnSetChaine));


        public static readonly DependencyProperty ChaineColumnsValueProperty =
            DependencyProperty.Register("ChaineColumns", typeof(object), typeof(CanvasControl),
                new PropertyMetadata(0, OnSetChaineCol));

        public static readonly DependencyProperty ChaineRowsValueProperty =
            DependencyProperty.Register("ChaineRows", typeof(object), typeof(CanvasControl),
                new PropertyMetadata(0, null));


        public static readonly DependencyProperty ProhibitAreaProperty = DependencyProperty.Register(
            nameof(ProhibitArea), typeof(ProhibitedRectangle), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetProhibitedArea));

        public static readonly DependencyProperty ListComposantProperty = DependencyProperty.Register(
            nameof(ListComposant), typeof(ObservableCollection<Composition>), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetComposantList));


        public static readonly DependencyProperty ContentEnfilageListProperty = DependencyProperty.Register(
            nameof(ContentEnfilageList), typeof(ObservableCollection<MatrixElement>), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetContentEnfilage));

        public static readonly DependencyProperty EnfilageBoardProperty = DependencyProperty.Register(
            nameof(EnfilageBoard), typeof(TrulyObservableCollection<MatrixElement>), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetEnfBoard));


        public static DependencyProperty SetRowProperty = DependencyProperty.RegisterAttached("SetRow", typeof(object),
            typeof(EnfilageSchemas), new PropertyMetadata(0, OnSetRow));

        public static readonly DependencyProperty SetColumnProperty =
            DependencyProperty.RegisterAttached("SetColumn", typeof(object), typeof(EnfilageSchemas),
                new PropertyMetadata(0, OnSetColumn));


        public BoardStructure _board;
        private bool _Btn1;
        private bool _Btn10;
        private bool _Btn10Vis;

        private bool _Btn2;

        private bool _Btn3;

        private bool _Btn4;

        private bool _Btn5;

        private bool _Btn6;

        private bool _Btn7;

        private bool _Btn8;
        private bool _Btn9;

        private bool _Btn9Vis;

        private int _ChainColNum = 8;


        private int _ChaineRo;
        private bool _Col1;
        private bool _Col10;

        private bool _Col2;

        private bool _Col3;

        private bool _Col4;

        private bool _Col5;

        private bool _Col6;
        private bool _Col7;
        private bool _Col8;

        private bool _Col9;


        private List<Composant> _CompList;
        private bool _IsDisplayChain = true;

        private bool _IsEditChain;
        private bool _IsEditChain2;
        private bool _IsEditChainBtn;

        private bool _SecChainVis;
        private string _SelectedColChain;

        private Composant _SelectedComp;

        private int _StartChaineRow;
        private int _StartLegendRow;

        public bool IsDent = true;

        public ObservableCollection<CanvasElement> ListItem;

        public CanvasControl()
        {
            InitializeComponent();
            SetRow = 58;
              SetColumn = 83;
        }

        public bool Col7
        {
            get => _Col7;
            set
            {
                _Col7 = value;
                if (value)
                {
                    SelectedComp = null;
                    SelectedColChain = "7";
                }

                NotifyPropertyChanged();
            }
        }

        public int ChainColNum
        {
            get => _ChainColNum;
            set
            {
                _ChainColNum = value;
                NotifyPropertyChanged();
            }
        }

        public bool Btn9Vis
        {
            get => _Btn9Vis;
            set
            {
                _Btn9Vis = value;
                NotifyPropertyChanged();
            }
        }

        public bool Btn10Vis
        {
            get => _Btn10Vis;
            set
            {
                _Btn10Vis = value;
                NotifyPropertyChanged();
            }
        }

        public string SelectedColChain
        {
            get => _SelectedColChain;
            set
            {
                _SelectedColChain = value;
                NotifyPropertyChanged();
            }
        }

        public SecRectangle SecondRect
        {
            get => (SecRectangle)GetValue(SecondRectProperty);
            set => SetValue(SecondRectProperty, value);
        }

        public int AngleChain
        {
            get => (int)GetValue(AngleChainProperty);
            set => SetValue(AngleChainProperty, value);
        }

        public int ChainRotateY
        {
            get => (int)GetValue(ChainRotateYProperty);
            set => SetValue(ChainRotateYProperty, value);
        }

        public int ChainRotateX
        {
            get => (int)GetValue(ChainRotateXProperty);
            set => SetValue(ChainRotateXProperty, value);
        }

        public bool Col8
        {
            get => _Col8;
            set
            {
                _Col8 = value;
                if (value)
                {
                    SelectedComp = null;
                    SelectedColChain = "8";
                }

                NotifyPropertyChanged();
            }
        }

        public bool Col9
        {
            get => _Col9;
            set
            {
                _Col9 = value;
                if (value)
                {
                    SelectedComp = null;
                    SelectedColChain = "9";
                }

                NotifyPropertyChanged();
            }
        }

        public bool Col10
        {
            get => _Col10;
            set
            {
                _Col10 = value;
                if (value)
                {
                    SelectedComp = null;
                    SelectedColChain = "10";
                }

                NotifyPropertyChanged();
            }
        }

        public bool Col1
        {
            get => _Col1;
            set
            {
                _Col1 = value;
                if (value)
                {
                    SelectedComp = null;
                    SelectedColChain = "1";
                }

                NotifyPropertyChanged();
            }
        }

        public bool Col2
        {
            get => _Col2;
            set
            {
                _Col2 = value;
                if (value)
                {
                    SelectedComp = null;
                    SelectedColChain = "2";
                }

                NotifyPropertyChanged();
            }
        }

        public bool Col3
        {
            get => _Col3;
            set
            {
                _Col3 = value;
                if (value)
                {
                    SelectedComp = null;
                    SelectedColChain = "3";
                }

                NotifyPropertyChanged();
            }
        }

        public bool Col4
        {
            get => _Col4;
            set
            {
                _Col4 = value;
                if (value)
                {
                    SelectedComp = null;
                    SelectedColChain = "4";
                }

                NotifyPropertyChanged();
            }
        }

        public WorkRectangle WorkinRect
        {
            get => (WorkRectangle)GetValue(WorkinRectProperty);
            set => SetValue(WorkinRectProperty, value);
        }

        public int StartChaineRow
        {
            get => _StartChaineRow;
            set
            {
                _StartChaineRow = value;
                NotifyPropertyChanged();
            }
        }

        public int StartLegendRow
        {
            get => _StartLegendRow;
            set
            {
                _StartLegendRow = value;
                NotifyPropertyChanged();
            }
        }

        public chaine SelectedChaine
        {
            get => (chaine)GetValue(SelectedChaineProperty);
            set => SetValue(SelectedChaineProperty, value);
        }

        public List<Composant> CompList
        {
            get => (List<Composant>)GetValue(CompListProperty);
            set => SetValue(CompListProperty, value);
        }

        public Composant SelectedComp
        {
            get => _SelectedComp;
            set
            {
                _SelectedComp = value;
                if (value != null && SelectedColChain != null && !string.IsNullOrWhiteSpace(SelectedColChain))
                {
                    var chcolcomp = new ChColComp();
                    chcolcomp.ComposantID = SelectedComp.ID;
                    chcolcomp.ColNum = Convert.ToInt32(SelectedColChain);
                    SelectedColComp = chcolcomp;
                }

                NotifyPropertyChanged();
            }
        }

        public ChColComp SelectedColComp
        {
            get => (ChColComp)GetValue(SelectedColCompProperty);
            set => SetValue(SelectedColCompProperty, value);
        }

        public bool Col5
        {
            get => _Col5;
            set
            {
                _Col5 = value;
                if (value)
                {
                    SelectedComp = null;
                    SelectedColChain = "5";
                }

                NotifyPropertyChanged();
            }
        }

        public bool Col6
        {
            get => _Col6;
            set
            {
                _Col6 = value;
                if (value)
                {
                    SelectedComp = null;
                    SelectedColChain = "6";
                }

                NotifyPropertyChanged();
            }
        }

        public bool Btn8
        {
            get => _Btn8;
            set
            {
                _Btn8 = value;
                NotifyPropertyChanged();
            }
        }

        public bool Btn9
        {
            get => _Btn9;
            set
            {
                _Btn9 = value;
                NotifyPropertyChanged();
            }
        }

        public bool Btn10
        {
            get => _Btn10;
            set
            {
                _Btn10 = value;
                NotifyPropertyChanged();
            }
        }

        public bool Btn1
        {
            get => _Btn1;
            set
            {
                _Btn1 = value;
                NotifyPropertyChanged();
            }
        }

        public bool Btn2
        {
            get => _Btn2;
            set
            {
                _Btn2 = value;
                NotifyPropertyChanged();
            }
        }

        public bool Btn3
        {
            get => _Btn3;
            set
            {
                _Btn3 = value;
                NotifyPropertyChanged();
            }
        }

        public bool Btn4
        {
            get => _Btn4;
            set
            {
                _Btn4 = value;
                NotifyPropertyChanged();
            }
        }

        public bool Btn5
        {
            get => _Btn5;
            set
            {
                _Btn5 = value;
                NotifyPropertyChanged();
            }
        }

        public bool Btn6
        {
            get => _Btn6;
            set
            {
                _Btn6 = value;
                NotifyPropertyChanged();
            }
        }

        public bool Btn7
        {
            get => _Btn7;
            set
            {
                _Btn7 = value;
                NotifyPropertyChanged();
            }
        }

        public EnfilageElement enfilageElement
        {
            get => (EnfilageElement)GetValue(enfilageElementProperty);
            set => SetValue(enfilageElementProperty, value);
        }

        public bool EnableChaineEdit
        {
            get => (bool)GetValue(EnableChaineEditProperty);
            set => SetValue(EnableChaineEditProperty, value);
        }

        public LinkedList<Rang> Rangs
        {
            get => (LinkedList<Rang>)GetValue(RangsProperty);
            set => SetValue(RangsProperty, value);
        }

        public LinkedList<LinkedList<MatrixElement>> RogueDentList
        {
            get => (LinkedList<LinkedList<MatrixElement>>)GetValue(RogueDentListProperty);
            set => SetValue(RogueDentListProperty, value);
        }

        public LinkedList<LinkedList<MatrixElement>> DentList
        {
            get => (LinkedList<LinkedList<MatrixElement>>)GetValue(DentListProperty);
            set => SetValue(DentListProperty, value);
        }

        public int NbrDent
        {
            get => (int)GetValue(NbrDentProperty);
            set => SetValue(NbrDentProperty, value);
        }

        public bool IsEditChain
        {
            get => _IsEditChain;
            set
            {
                _IsEditChain = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsEditChainBtn
        {
            get => _IsEditChainBtn;
            set
            {
                _IsEditChainBtn = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsEditChain2
        {
            get => _IsEditChain2;
            set
            {
                _IsEditChain2 = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsDisplayChain
        {
            get => _IsDisplayChain;
            set
            {
                _IsDisplayChain = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsDisplay=true;
        public bool IsDisplay
        {
            get {
                return _IsDisplay;
            }
            set {
                _IsDisplay = value;
                NotifyPropertyChanged();
                    }
        }

        public bool IsDentFil
        {
            get => (bool)GetValue(IsDentFilProperty);
            set{
                SetValue(IsDentFilProperty, value);
               
                    }
        }

        public string TrameXposition
        {
            get => (string)GetValue(TrameXpositionProperty);
            set => SetValue(TrameXpositionProperty, value);
        }

        public string TrameYposition
        {
            get => (string)GetValue(TrameYpositionProperty);
            set => SetValue(TrameYpositionProperty, value);
        }

        public string LastXposition
        {
            get => (string)GetValue(LastXpositionProperty);
            set
            {
                SetValue(LastXpositionProperty, value);
                NotifyPropertyChanged();
            }
        }

        public string LastYposition
        {
            get => (string)GetValue(LastYpositionProperty);
            set
            {
                SetValue(LastYpositionProperty, value);
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<ChaineMatrixElement> ChaineList2
        {
            get => (ObservableCollection<ChaineMatrixElement>)GetValue(ChaineList2ValueProperty);
            set => SetValue(ChaineList2ValueProperty, value);
        }

        public int ChaineRows2
        {
            get => (int)GetValue(ChaineRows2Property);
            set => SetValue(ChaineRows2Property, value);
        }

        public int ChRowSum
        {
            get => (int)GetValue(ChRowSumProperty);
            set => SetValue(ChRowSumProperty, value);
        }

        public bool SecChainVis
        {
            get => _SecChainVis;
            set
            {
                _SecChainVis = value;
                NotifyPropertyChanged();
            }
        }

        public int ChaineRows
        {
            get => (int)GetValue(ChaineRowsValueProperty);
            set => SetValue(ChaineRowsValueProperty, value);
        }

        public int ChaineColumns
        {
            get => (int)GetValue(ChaineColumnsValueProperty);
            set => SetValue(ChaineColumnsValueProperty, value);
        }

        public ObservableCollection<ChaineMatrixElement> ChaineList
        {
            get => (ObservableCollection<ChaineMatrixElement>)GetValue(ChaineListValueProperty);
            set => SetValue(ChaineListValueProperty, value);
        }

        public ProhibitedRectangle ProhibitArea
        {
            get => (ProhibitedRectangle)GetValue(ProhibitAreaProperty);
            set => SetValue(ProhibitAreaProperty, value);
        }


        public ObservableCollection<Composition> ListComposant
        {
            get => (ObservableCollection<Composition>)GetValue(ListComposantProperty);
            set
            {
                SetValue(ListComposantProperty, value);
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<MatrixElement> ContentEnfilageList
        {
            get => (ObservableCollection<MatrixElement>)GetValue(ContentEnfilageListProperty);
            set
            {
                SetValue(ContentEnfilageListProperty, value);
                NotifyPropertyChanged();
            }
        }


        public ObservableCollection<MatrixElement> EnfilageBoard
        {
            get => (ObservableCollection<MatrixElement>)GetValue(EnfilageBoardProperty);
            set => SetValue(EnfilageBoardProperty, value);
        }

        public object SetRow
        {
            get => GetValue(SetRowProperty);
            set => SetValue(SetRowProperty, value);
        }

        public object SetColumn
        {
            get => GetValue(SetColumnProperty);
            set => SetValue(SetColumnProperty, value);
        }

        public int ChaineRo
        {
            get => _ChaineRo;
            set
            {
                _ChaineRo = value;
                NotifyPropertyChanged();
            }
        }

        public MatrixElement SelectedCell { get; set; }

        public Composition SelectedComposant { get; set; }

        public ChaineMatrixElement SelectedChaineCell { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public static void SetAngleChain(DependencyObject e, DependencyPropertyChangedEventArgs arg)
        {
        }

        public static void SetChainRotateX(DependencyObject e, DependencyPropertyChangedEventArgs arg)
        {
        }

        public static void SetChainRotateY(DependencyObject e, DependencyPropertyChangedEventArgs arg)
        {
        }

        private static void OnSetRectWork(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mycontrol = (CanvasControl)d;
            if (e.OldValue != null)
            {
                mycontrol.ResetWorkRectangle((WorkRectangle)e.OldValue,(WorkRectangle)e.NewValue);
            }
            mycontrol.SetWorkRectangle();
        }

        public void ResetWorkRectangle(WorkRectangle WorkRect,WorkRectangle NewWorkRect)
        {

            bool b = false;
            if (NewWorkRect != null)
            {
                if (WorkRect.NbrPart != NewWorkRect.NbrPart
                    || WorkRect.PartHeight!=NewWorkRect.PartHeight)
                {
                    b = true;
                }
            }
            foreach (var EnfCont in EnfilageBoard)
            {
                if (EnfCont.TypBox != MatrixElement.BoxType.OutRange &&
                    EnfCont.TypBox != MatrixElement.BoxType.Inaccessible)
                {
                    EnfCont.SetBoxType(MatrixElement.BoxType.OutRange);
                    if (b)
                    {
                        EnfCont.Content = null;
                        SettingContent(EnfCont);
                    }
                }
                   
            }

            if (b)
            {
                foreach (var comp in ListComposant)
                {
                    comp.EnfNbrFil = 0;
                }

                NbrDent = 0;
            }
                 
      
        }
        public void SetWorkRectangle()
        {

            if (WorkinRect != null)
            {
                        foreach (var EnfCont in EnfilageBoard)
                    if (EnfCont.X >= WorkinRect.PartsWidthStart
                        && EnfCont.X <= WorkinRect.PartsWidthEnd
                        && EnfCont.Y >= WorkinRect.PartsHeightStart
                        && EnfCont.Y < WorkinRect.PartsHeightEnd
                        && EnfCont.TypBox != MatrixElement.BoxType.Inaccessible)
                    {
                        var PartNbr = (EnfCont.Y - WorkinRect.PartsHeightStart) / WorkinRect.PartHeight;
                        if (EnfCont.Y - PartNbr * WorkinRect.PartHeight ==
                            WorkinRect.DentLen - 1 + WorkinRect.PartsHeightStart
                            || EnfCont.Y - PartNbr * WorkinRect.PartHeight ==
                            WorkinRect.DentLen + WorkinRect.PartsHeightStart)
                        {
                            EnfCont.SetBoxType(MatrixElement.BoxType.Dents);

                            if (EnfCont.Y - PartNbr * WorkinRect.PartHeight ==
                                WorkinRect.PartHeight - 2 + WorkinRect.PartsHeightStart)
                                EnfCont.position = 1;
                            else
                                EnfCont.position = 2;
                        }
                        else if (EnfCont.Y - PartNbr * WorkinRect.PartHeight >=
                                 WorkinRect.PartsHeightStart + WorkinRect.SecEmptyLen
                                 && EnfCont.Y - PartNbr * WorkinRect.PartHeight <
                                 WorkinRect.PartsHeightStart + WorkinRect.LisseLen)
                        {
                            var positionEl = EnfCont.Y - PartNbr * WorkinRect.PartHeight - WorkinRect.PartsHeightStart;
                            EnfCont.SetBoxType(MatrixElement.BoxType.Lisses);


                            if (SelectedChaine.ChaineCompos.SingleOrDefault(ch => ch.ColNum == positionEl) != null)
                            {
                                EnfCont.SupportedComp = SelectedChaine.ChaineCompos
                                    .Single(ch => ch.ColNum == positionEl).Comp;
                                EnfCont.position = positionEl;
                            }
                        }
                        else
                        {
                            EnfCont.SetBoxType(MatrixElement.BoxType.Empty);
                        }
                    }
            }
        
         
        }

        private static void OnSetCompList(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public void SetInaccessibleCompCells(MatrixElement.BoxType typ)
        {
            if (IsDentFil)
            {
                if (SelectedChaine != null && ChRowSum >= 78)
                {
                    var NumRow = ChaineColumns * 2 + 1;

                    var lastcol = 4 + ChaineRows;
                    if (ChaineColumns <= 8) NumRow = 8 * 2 + 1;
                    StartChaineRow = 58 - NumRow;
                    SecondRect = new SecRectangle(StartChaineRow, 0, 0);

                    for (var j = StartChaineRow; j < 58; j++)
                    for (var i = 0; i < lastcol; i++)
                    {
                        var ij = j * 83 + i;
                        EnfilageBoard[ij].SetBoxType(typ);
                    }
                }
                else
                {
                    if (ListComposant.Count > 0)
                    {
                        var NumCol = Convert.ToInt32(Math.Ceiling(ListComposant.Count * 18 / (double)12));
                        StartLegendRow = 58 - NumCol;
                        for (var i = StartLegendRow; i <= 57; i++)
                        for (var j = 76; j <= 82; j++)
                        {
                            var ij = i * 83 + j;
                            EnfilageBoard[ij].SetBoxType(typ);
                        }
                    }

                    if (SelectedChaine != null)
                    {
                        if (SelectedChaine.Ligne <= 26)
                        {
                            var NumRow = 4 + SelectedChaine.Ligne;
                            StartChaineRow = 58 - NumRow;
                            var lastcol = SelectedChaine.Colonne;
                            if (SelectedChaine.Colonne <= 8) lastcol = 8;

                            for (var j = StartChaineRow; j < 58; j++)
                            for (var i = 0; i < lastcol; i++)
                            {
                                var ij = j * 83 + i;
                                EnfilageBoard[ij].SetBoxType(typ);
                            }
                        }
                        else if (ChRowSum > 26)
                        {
                            var NumRow = ChaineColumns + 1;

                            var lastcol = 4 + ChaineRows;
                            if (ChaineColumns <= 8) NumRow = 8 + 1;
                            StartChaineRow = 58 - NumRow;
                            for (var j = StartChaineRow; j < 58; j++)
                            for (var i = 0; i < lastcol; i++)
                            {
                                var ij = j * 83 + i;
                                EnfilageBoard[ij].SetBoxType(typ);
                            }
                        }
                    }

                    var StartWidth = ChaineColumns + 1;
                    if (ChRowSum > 26)
                    {
                        StartWidth = 4 + ChaineRows;
                    }
                    else
                    {
                        if (ChaineColumns <= 8) StartWidth = 8 + 1;
                    }

                    if (StartChaineRow > StartLegendRow)
                        SecondRect = new SecRectangle(StartChaineRow, StartWidth, 82 - 7);
                    else
                        SecondRect = new SecRectangle(StartLegendRow, StartWidth, 82 - 7);
                }
            }
            else if (IsDentFil == false && WorkinRect == null)
            {
                if (SelectedChaine != null && ChRowSum >= 78)
                {
                    var NumRow = ChaineColumns * 2 + 1;

                    var lastcol = 4 + ChaineRows;
                    if (ChaineColumns <= 8) NumRow = 8 * 2 + 1;
                    var startRow = 58 - NumRow;
                    for (var j = startRow; j < 58; j++)
                    for (var i = 0; i < lastcol; i++)
                    {
                        var ij = j * 83 + i;
                        EnfilageBoard[ij].SetBoxType(typ);
                    }
                }
                else
                {
                    if (ListComposant != null && ListComposant.Count > 0)
                    {
                        var NumCol = Convert.ToInt32(Math.Ceiling(ListComposant.Count * 18 / (double)12));
                        var StartCol = 58 - NumCol;
                        for (var i = StartCol; i <= 57; i++)
                        for (var j = 76; j <= 82; j++)
                        {
                            var ij = i * 83 + j;
                            EnfilageBoard[ij].SetBoxType(typ);
                        }
                    }

                    if (SelectedChaine != null)
                    {
                        if (ChaineRows <= 26)
                        {
                            var NumRow = 4 + ChaineRows;
                            var startRow = 58 - NumRow;
                            var lastcol = ChaineColumns;
                            if (ChaineColumns <= 8) lastcol = 8;

                            for (var j = startRow; j < 58; j++)
                            for (var i = 0; i < lastcol; i++)
                            {
                                var ij = j * 83 + i;
                                EnfilageBoard[ij].SetBoxType(typ);
                            }
                        }
                        else if (ChRowSum > 26)
                        {
                            var NumRow = ChaineColumns + 1;

                            var lastcol = 4 + ChaineRows;
                            if (ChaineColumns <= 8) NumRow = 8 + 1;
                            var startRow = 58 - NumRow;
                            for (var j = startRow; j < 58; j++)
                            for (var i = 0; i < lastcol; i++)
                            {
                                var ij = j * 83 + i;
                                EnfilageBoard[ij].SetBoxType(typ);
                            }
                        }
                    }
                }
            }
        }

        public static void OnsetSelectedChaine(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mycontrol = (CanvasControl)d;
        }

        public static void OnsetChaineColComp(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public static void OnStartEditing(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mycontrol = (CanvasControl)d;
            mycontrol.StartEditing();
        }

        public void StartEditing()
        {
            if (EnableChaineEdit)
            {
                IsEditChainBtn = true;
                if (ChaineColumns > 26)
                {
                    IsEditChain2 = true;
                    IsEditChain = false;
                    IsDisplayChain = false;
                }
                else
                {
                    IsEditChain2 = false;
                    IsEditChain = true;
                    IsDisplayChain = false;
                }
            }
            else
            {
                IsEditChainBtn = false;
                IsEditChain = false;
                IsEditChain2 = false;
                IsDisplayChain = true;
            }
        }

        private static void OnSetRang(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnSetRogueDentList(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnSetDentList(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnSetIsDentFil(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mycontrol = (CanvasControl)d;
            mycontrol.IsDisplay = !(bool)e.NewValue;
        }

        private static void OnSetChaine(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mycontrol = (CanvasControl)d;
            mycontrol.RotateChain();
        }

        public void RotateChain()
        {
            if (ChaineRows > 26)
            {
                if (EnableChaineEdit)
                {
                    IsEditChain = false;
                    IsEditChain2 = true;
                }

                AngleChain = 270;

                if (ChaineRows >= 78)
                {
                    SecChainVis = true;
                    ChaineRows = 78;
                    ChaineRows2 = ChRowSum - 78;
                }
                else
                {
                    ChaineRo = ChRowSum;
                    SecChainVis = false;
                }
            }
            else
            {
                if (SecChainVis) SecChainVis = false;
                if (EnableChaineEdit)
                {
                    IsEditChain = true;
                    IsEditChain2 = false;
                }

                ChaineRows = ChRowSum;
                if (AngleChain > 0)
                {
                    AngleChain = 0;

                    ChainRotateX = 0;
                    ChainRotateY = 0;
                }
            }

        }

        private static void OnSetChaineCol(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var myControl = (CanvasControl)d;
            myControl.SetChainColumn();
        }

        public void SetChainColumn()
        {
            Col1 = false;
            Col2 = false;
            Col3 = false;
            Col4 = false;
            Col5 = false;
            Col6 = false;
            Col7 = false;
            Col8 = false;
            Col9 = false;
            Col10 = false;
            if (ChaineColumns == 10)
            {
                Btn10Vis = true;
                Btn9Vis = true;
                ChainColNum = 10;
            }
            else if (ChaineColumns == 9)
            {
                Btn10Vis = false;
                Btn9Vis = true;
                ChainColNum = 9;
            }
            else
            {
                Btn10Vis = false;
                Btn9Vis = true;
                ChainColNum = 8;
            }

            SelectedColChain = null;
            if (ChaineColumns == 10)
            {
                Btn10 = true;
                Btn9 = true;
                Btn8 = true;
                Btn7 = true;
                Btn6 = true;
                Btn5 = true;
                Btn4 = true;
                Btn3 = true;
                Btn2 = true;
                Btn1 = true;
            }
            else if (ChaineColumns == 9)
            {
                Btn10 = false;
                Btn9 = true;
                Btn8 = true;
                Btn7 = true;
                Btn6 = true;
                Btn5 = true;
                Btn4 = true;
                Btn3 = true;
                Btn2 = true;
                Btn1 = true;
            }
            else if (ChaineColumns == 8)
            {
                Btn10 = false;
                Btn9 = false;
                Btn8 = true;
                Btn7 = true;
                Btn6 = true;
                Btn5 = true;
                Btn4 = true;
                Btn3 = true;
                Btn2 = true;
                Btn1 = true;
            }
            else if (ChaineColumns == 7)
            {
                Btn10 = false;
                Btn9 = false;
                Btn8 = false;
                Btn7 = true;
                Btn6 = true;
                Btn5 = true;
                Btn4 = true;
                Btn3 = true;
                Btn2 = true;
                Btn1 = true;
            }
            else if (ChaineColumns == 6)
            {
                Btn10 = false;
                Btn9 = false;
                Btn8 = false;
                Btn7 = false;
                Btn6 = true;
                Btn5 = true;
                Btn4 = true;
                Btn3 = true;
                Btn2 = true;
                Btn1 = true;
            }
            else if (ChaineColumns == 5)
            {
                Btn10 = false;
                Btn9 = false;
                Btn8 = false;
                Btn7 = false;
                Btn6 = false;
                Btn5 = true;
                Btn4 = true;
                Btn3 = true;
                Btn2 = true;
                Btn1 = true;
            }
            else if (ChaineColumns == 4)
            {
                Btn10 = false;
                Btn9 = false;
                Btn8 = false;
                Btn7 = false;
                Btn6 = false;
                Btn5 = false;
                Btn4 = true;
                Btn3 = true;
                Btn2 = true;
                Btn1 = true;
            }
            else if (ChaineColumns == 3)
            {
                Btn10 = false;
                Btn9 = false;
                Btn8 = false;
                Btn7 = false;
                Btn6 = false;
                Btn5 = false;
                Btn4 = false;
                Btn3 = true;
                Btn2 = true;
                Btn1 = true;
            }
            else if (ChaineColumns == 2)
            {
                Btn10 = false;
                Btn9 = false;
                Btn8 = false;
                Btn7 = false;
                Btn6 = false;
                Btn5 = false;
                Btn4 = false;
                Btn3 = false;
                Btn2 = true;
                Btn1 = true;
            }
            else if (ChaineColumns == 1)
            {
                Btn10 = false;
                Btn9 = false;
                Btn8 = false;
                Btn7 = false;
                Btn6 = false;
                Btn5 = false;
                Btn4 = false;
                Btn3 = false;
                Btn2 = false;
                Btn1 = true;
            }
        }

        private static void OnSetTramePosition(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var val = e.NewValue.ToString();
            }
        }

        private static void OnSetPosition(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public static void OnSetProhibitedArea(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var mycontrol = (CanvasControl)obj;
            if(mycontrol.IsSizeChanged)
            {
                mycontrol.ExecuteProhibitedArea = false;
                if (args.OldValue != null)
                    mycontrol.SetProhibitedArea((ProhibitedRectangle)args.OldValue, MatrixElement.BoxType.OutRange);

                mycontrol.SetProhibitedArea((ProhibitedRectangle)args.NewValue, MatrixElement.BoxType.Inaccessible);
            }
            else
            {
                mycontrol.ExecuteProhibitedArea = true;
                if (args.OldValue != null)
                    mycontrol.OldRectangle = (ProhibitedRectangle)args.OldValue;

            }
           
        }
        private ProhibitedRectangle OldRectangle;

        public void SetProhibitedArea(ProhibitedRectangle ProhibArea, MatrixElement.BoxType typ)
        {
            if (ProhibArea != null)
            {
                if (ProhibArea.StartLegendRow > 0)
                {
                    int StartIndex =58-
                        Convert.ToInt32(Math.Ceiling(ProhibArea.StartLegendRow * 18 / ((double)Board.ActualHeight/59)));
                    for (var i = StartIndex; i <= 57; i++)
                    for (var j = 76; j <= 82; j++)
                    {
                        var ij = i * 83 + j;
                        EnfilageBoard[ij].SetBoxType(typ);
                    }
                }
                  

                for (var j = ProhibArea.StartChaineRow; j < 58; j++)
                for (var i = 0; i < ProhibArea.EndColumn; i++)
                {
                    var ij = j * 83 + i;
                    EnfilageBoard[ij].SetBoxType(typ);
                }
            }
        }

        private static void OnSetComposantList(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mycontrol = (CanvasControl)d;
            if (e.OldValue != null)
            {
                var coll = (ObservableCollection<Composition>)e.OldValue;
                // Unsubscribe from CollectionChanged on the old collection
                // mycontrol.TransientMethod(coll, false);
            }

            if (e.NewValue != null)
            {
                var coll = (ObservableCollection<Composition>)e.NewValue;
                // Subscribe to CollectionChanged on the new collection
                //   mycontrol.TransientMethod(coll, true);
            }
        }

        public void TransientMethod(ObservableCollection<Composition> col, bool b)
        {
            if (b)
                col.CollectionChanged += ListComposant_CollectionChanged;
            else
                col.CollectionChanged -= ListComposant_CollectionChanged;
        }

        private void ListComposant_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // handle CollectionChanged
        }

        public void SetInitContents()
        {
            ResetEnfilageBoard();
            foreach (var EnfCont in ContentEnfilageList)
            {
                var EleMatx = EnfilageBoard.FirstOrDefault(bo => bo.X == EnfCont.X && bo.Y == EnfCont.Y);
                EleMatx.DentFil = EnfCont.DentFil;
                EleMatx.Content = EnfCont.Content;
            }
        }

        public void SettingContent(MatrixElement ex)
        {
            var matx = ContentEnfilageList.FirstOrDefault(co => co.X == ex.X && co.Y == ex.Y);
            if (ex.Content == null)
            {
                if (matx != null)
                {
                    ContentEnfilageList.Remove(matx);
                    enfilageElement = new EnfilageElement(matx, false);
                }
            }
            else
            {
                if (matx == null)
                {
                    ContentEnfilageList.Add(ex);
                    enfilageElement = new EnfilageElement(ex, true);
                }
                else
                {
                    if (matx.Content.ID == ex.Content.ID)
                    {
                        ContentEnfilageList.Remove(matx);
                        enfilageElement = new EnfilageElement(matx, false);
                    }
                    else
                    {
                        ContentEnfilageList[ContentEnfilageList.IndexOf(matx)] = ex;
                        enfilageElement = new EnfilageElement(ex, true, ex.Content);
                    }
                }
            }
        }

        private static void OnSetContentEnfilage(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var myControl = (CanvasControl)d;
            if (myControl.ContentEnfilageList != null && myControl.ContentEnfilageList.Count > 0)
                myControl.SetInitContents();
            else
                myControl.ResetEnfilageBoard();
        }

        public void ResetEnfilageBoard()
        {
            if (EnfilageBoard != null)
                foreach (var element in EnfilageBoard)
                    if (element.Content != null)
                        element.Content = null;
        }

        public void ResetEnfilageBoardWithPermission()
        {
            var result = MessageBox.Show("Etes-vous sur de vouloir effacer tous ?", "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
                foreach (var element in EnfilageBoard)
                    if (element.Content != null)
                        element.Content = null;
        }

        private static void OnSetEnfBoard(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mycontrol = (CanvasControl)d;
        }


        private void ColChange(object d, NotifyCollectionChangedEventArgs args)
        {
        }

        private void CanvasControl_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // _board = new BoardStructure(59, 83);
            // Board.ItemsSource=new ObservableCollection<MatrixElement>(_board.Board);
        }

        private static void OnSetRow(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public void MoveUp()
        {
            SelectedCell.IsSelected = false;
            if (SelectedCell.Y == 0)
            {
                var FindY = Convert.ToInt32(SetRow) - 1;
                while (EnfilageBoard.First(b => b.Y == Convert.ToInt32(SetRow) - 1 && b.X == SelectedCell.X).TypBox ==
                       MatrixElement.BoxType.Inaccessible) FindY--;
                SelectedCell = EnfilageBoard.First(b => b.Y == FindY && b.X == SelectedCell.X);
            }

            else
            {
                SelectedCell = EnfilageBoard.First(b => b.Y == SelectedCell.Y - 1 && b.X == SelectedCell.X);
            }

            SelectedCell.IsSelected = true;
        }

        public void MoveDown()
        {
            SelectedCell.IsSelected = false;
            if (SelectedCell.Y == Convert.ToInt32(SetRow) - 1
                || EnfilageBoard.First(b => b.Y == SelectedCell.Y + 1 && b.X == SelectedCell.X).TypBox ==
                MatrixElement.BoxType.Inaccessible)
                SelectedCell = EnfilageBoard.First(b => b.Y == 0 && b.X == SelectedCell.X);
            else
                SelectedCell = EnfilageBoard.First(b => b.Y == SelectedCell.Y + 1 && b.X == SelectedCell.X);

            SelectedCell.IsSelected = true;
        }

        private static void OnSetColumn(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // some code here
        }

        public void MoveRight()
        {
            SelectedCell.IsSelected = false;
            if (SelectedCell.X == Convert.ToInt32(SetColumn) - 1
                || EnfilageBoard.First(b => b.Y == SelectedCell.Y && b.X == SelectedCell.X + 1).TypBox ==
                MatrixElement.BoxType.Inaccessible)
            {
                var FindX = 0;
                while (EnfilageBoard.First(b => b.Y == SelectedCell.Y && b.X == FindX).TypBox ==
                       MatrixElement.BoxType.Inaccessible) FindX++;
                SelectedCell = EnfilageBoard.First(b => b.Y == SelectedCell.Y && b.X == FindX);
            }

            else
            {
                SelectedCell = EnfilageBoard.First(b => b.Y == SelectedCell.Y && b.X == SelectedCell.X + 1);
            }

            SelectedCell.IsSelected = true;
        }

        public void MoveLeft()
        {
            SelectedCell.IsSelected = false;
            if (SelectedCell.X == 0
                || EnfilageBoard.First(b => b.Y == SelectedCell.Y && b.X == SelectedCell.X - 1).TypBox ==
                MatrixElement.BoxType.Inaccessible)
            {
                var FindX = Convert.ToInt32(SetColumn) - 1;
                while (EnfilageBoard.First(b => b.Y == SelectedCell.Y && b.X == FindX).TypBox ==
                       MatrixElement.BoxType.Inaccessible) FindX--;
                SelectedCell = EnfilageBoard.First(b => b.Y == SelectedCell.Y && b.X == FindX);
            }

            else
            {
                SelectedCell = EnfilageBoard.First(b => b.Y == SelectedCell.Y && b.X == SelectedCell.X - 1);
            }

            SelectedCell.IsSelected = true;
        }

        private void Board_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;


            if (e.Key == Key.Up)
                MoveUp();
            else if (e.Key == Key.Down)
                MoveDown();
            else if (e.Key == Key.Right)
                MoveLeft();
            else if (e.Key == Key.Left)
                MoveRight();
            else if (e.Key == Key.D1 || e.Key == Key.NumPad1)
                SetContent(1);


            else if (e.Key == Key.D2 || e.Key == Key.NumPad2)
                SetContent(2);

            else if (e.Key == Key.D3 || e.Key == Key.NumPad3)
                SetContent(3);

            else if (e.Key == Key.D4 || e.Key == Key.NumPad4)
                SetContent(4);

            else if (e.Key == Key.D5 || e.Key == Key.NumPad5)
                SetContent(5);

            else if (e.Key == Key.D6 || e.Key == Key.NumPad6)
                SetContent(6);

            else if (e.Key == Key.D7 || e.Key == Key.NumPad7) SetContent(7);
        }
        public bool IsAddNewContent()
        {
            return SelectedCell.Content == null;
        }
        public bool DoesCellAboveHasContent()
        {
            return SelectedCell.Y != 0 && EnfilageBoard
                                        .SingleOrDefault(enf => enf.Y == SelectedCell.Y - 1 && enf.X == SelectedCell.X)
                                        .IsContent;
        }
        public bool DoesCellBeneathHasContent()
        {
            return SelectedCell.Y != 57 && EnfilageBoard
                                           .SingleOrDefault(enf => enf.Y == SelectedCell.Y + 1 && enf.X == SelectedCell.X)
                                           .IsContent;
        }
        public void SetMatrixContent()
        {
            try
            {
                if (
                SelectedCell.TypBox != MatrixElement.BoxType.Inaccessible
                && SelectedCell.TypBox != MatrixElement.BoxType.OutRange
                && SelectedCell.TypBox != MatrixElement.BoxType.Empty
                && ((SelectedCell.TypBox == MatrixElement.BoxType.Dents && IsDent)
                    || (SelectedCell.TypBox == MatrixElement.BoxType.Lisses && IsDent == false))
            )
                {
                    if (SelectedComposant == null) SelectedComposant = ListComposant[0];

                    if (SelectedCell.TypBox == MatrixElement.BoxType.Dents
                        || (SelectedCell.TypBox == MatrixElement.BoxType.Lisses
                        && SelectedCell.SupportedComp.ID == SelectedComposant.GetComposant.ID))
                    {
                        if (IsAddNewContent())
                        {
                            var IsItAcceptible = true;
                            if (SelectedCell.TypBox == MatrixElement.BoxType.Dents)
                            {
                                if (SelectedCell.position == 1)
                                {
                                    if (DoesCellBeneathHasContent())
                                        IsItAcceptible = false;
                                }
                                else
                                {
                                    if (DoesCellAboveHasContent())
                                        IsItAcceptible = false;
                                }
                            }
                            else
                            {
                                for (var i = 1; i <= ChaineColumns; i++)
                                    if (i != SelectedCell.position)
                                    {
                                        var ecart = i - SelectedCell.position;
                                        if (EnfilageBoard.SingleOrDefault(enf =>
                                                enf.Y == SelectedCell.Y + ecart && enf.X == SelectedCell.X).IsContent)
                                            IsItAcceptible = false;
                                    }
                            }

                            if (IsItAcceptible)
                            {
                                if (IsDent)
                                {
                                    SelectedCell.DentFil = 0;


                                    var LeftAdjacentCellOnNextLine = EnfilageBoard.Single(enf =>
                                        enf.X == SelectedCell.X + 1 && enf.Y == SelectedCell.Y + 1);

                                    var RightAdjacentCellOnNextLine = EnfilageBoard.Single(enf =>
                                        enf.X == SelectedCell.X - 1 && enf.Y == SelectedCell.Y + 1);
                                    var Case_Pos2Plus = EnfilageBoard.Single(enf =>
                                        enf.X == SelectedCell.X + 1 && enf.Y == SelectedCell.Y - 1);
                                    var Case_Pos2Minus = EnfilageBoard.Single(enf =>
                                        enf.X == SelectedCell.X - 1 && enf.Y == SelectedCell.Y - 1);
                                    var Content_Pos1Plus = false;
                                    var Content_Pos2Plus = false;
                                    var Content_Pos1Minus = false;
                                    var Content_Pos2Minus = false;
                                    var InitLevelPlus = 0;
                                    var InitXPlus = 1;
                                    var InitLevelMinus = 0;
                                    var InitXMinus = 1;
                                    if (LeftAdjacentCellOnNextLine == null || WorkinRect.PartsWidthEnd < LeftAdjacentCellOnNextLine.X)
                                    {
                                        var PartNbr = Convert.ToInt32(Math.Ceiling(
                                            (SelectedCell.Y - WorkinRect.PartsHeightStart) /
                                            (double)WorkinRect.PartHeight));

                                        if (PartNbr < WorkinRect.NbrPart)
                                        {
                                            InitXPlus = WorkinRect.PartsWidthStart - SelectedCell.X;
                                            InitLevelPlus = InitLevelPlus + WorkinRect.PartHeight;

                                            Content_Pos1Plus = EnfilageBoard.Single(enf =>
                                                enf.X == SelectedCell.X + InitXPlus &&
                                                enf.Y == SelectedCell.Y + 1 + InitLevelPlus).IsContent;
                                            Content_Pos2Plus = EnfilageBoard.Single(enf =>
                                                enf.X == SelectedCell.X + InitXPlus &&
                                                enf.Y == SelectedCell.Y - 1 + InitLevelPlus).IsContent;
                                        }
                                    }
                                    else
                                    {
                                        Content_Pos1Plus = EnfilageBoard.Single(enf =>
                                            enf.X == SelectedCell.X + 1 && enf.Y == SelectedCell.Y + 1).IsContent;
                                        Content_Pos2Plus = EnfilageBoard.Single(enf =>
                                            enf.X == SelectedCell.X + 1 && enf.Y == SelectedCell.Y - 1).IsContent;
                                    }

                                    if (RightAdjacentCellOnNextLine == null || WorkinRect.PartsWidthStart > LeftAdjacentCellOnNextLine.X)
                                    {
                                        var PartNbr = Convert.ToInt32(Math.Ceiling((SelectedCell.Y - WorkinRect.PartsHeightStart) /
                                                      (double)WorkinRect.PartHeight));

                                        if (PartNbr > 1)
                                        {
                                            InitXMinus = SelectedCell.X - WorkinRect.PartsWidthEnd;
                                            InitLevelMinus = WorkinRect.PartHeight - InitLevelMinus;

                                            Content_Pos1Minus = EnfilageBoard.Single(enf =>
                                                enf.X == SelectedCell.X - InitXMinus &&
                                                enf.Y == SelectedCell.Y - InitLevelMinus + 1).IsContent;

                                            Content_Pos2Minus = EnfilageBoard.Single(enf =>
                                                enf.X == SelectedCell.X - InitXMinus &&
                                                enf.Y == SelectedCell.Y - InitLevelMinus - 1).IsContent;
                                        }
                                    }
                                    else
                                    {
                                        Content_Pos1Minus = EnfilageBoard.Single(enf =>
                                            enf.X == SelectedCell.X - 1 && enf.Y == SelectedCell.Y + 1).IsContent;

                                        Content_Pos2Minus = EnfilageBoard.Single(enf =>
                                            enf.X == SelectedCell.X - 1 && enf.Y == SelectedCell.Y - 1).IsContent;
                                    }


                                    var SearchXPlus = 0;
                                    var SearchYPlus = 0;
                                    var SearchXMinus = 0;
                                    var SearchYMinus = 0;
                                    var PossibleTripleTeeth = false;

                                    var NotLinked = true;
                                    if ((SelectedCell.position == 1 && Content_Pos1Plus)
                                        || (SelectedCell.position == 2 && Content_Pos2Plus))
                                    {
                                        PossibleTripleTeeth = true;
                                        SearchXPlus = SelectedCell.X + 1;
                                        if (SelectedCell.position == 1)
                                            SearchYPlus = SelectedCell.Y + 1;
                                        else
                                            SearchYPlus = SelectedCell.Y - 1;


                                        var AdjacentContent = true;
                                        var IsInRange = true;
                                        var IncX = InitXPlus + 1;
                                        var LevelY = InitLevelPlus;

                                        while (NotLinked && AdjacentContent && IsInRange)
                                        {
                                            var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == SelectedCell.X + IncX && enf.Y == SelectedCell.Y + LevelY);
                                            if (LinkCase == null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                            {
                                                var PartNbr = Convert.ToInt32(Math.Ceiling(
                                                    (SelectedCell.Y + LevelY - WorkinRect.PartsHeightStart) /
                                                    (double)WorkinRect.PartHeight));

                                                if (PartNbr >= WorkinRect.NbrPart)
                                                {
                                                    IsInRange = false;
                                                    NbrDent = NbrDent + 2;
                                                }
                                                else
                                                {
                                                    IncX = SelectedCell.X - WorkinRect.PartsWidthStart;
                                                    LevelY = LevelY + WorkinRect.PartHeight;
                                                }
                                            }
                                            else if (LinkCase.IsContent)
                                            {
                                                NotLinked = false;
                                                NbrDent++;
                                            }
                                            else
                                            {
                                                var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                    enf.X == SelectedCell.X + IncX && enf.Y == SearchYPlus + LevelY);
                                                if (CaseAdjacent.IsContent == false)
                                                {
                                                    AdjacentContent = false;
                                                    NbrDent = NbrDent + 2;
                                                }
                                                else
                                                {
                                                    IncX++;
                                                }
                                            }
                                        }
                                    }

                                    if ((SelectedCell.position == 1 && Content_Pos1Minus)
                                        || (SelectedCell.position == 2 && Content_Pos2Minus))
                                    {
                                        SearchXMinus = SelectedCell.X + 1;
                                        if (SelectedCell.position == 1)
                                            SearchYMinus = SelectedCell.Y + 1;
                                        else
                                            SearchYMinus = SelectedCell.Y - 1;
                                        NotLinked = true;
                                        var AdjacentContent = true;
                                        var IsInRange = true;
                                        var SubX = InitXMinus + 1;
                                        var LevelY = InitLevelMinus;
                                        while (NotLinked && AdjacentContent && IsInRange)
                                        {
                                            var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == SelectedCell.X - SubX && enf.Y == SelectedCell.Y - LevelY);
                                            if (LinkCase == null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                            {
                                                var PartNbr = Convert.ToInt32(Math.Ceiling(
                                                    (SelectedCell.Y - LevelY - WorkinRect.PartsHeightStart) /
                                                    (double)WorkinRect.PartHeight));
                                                if (PartNbr <= 1)
                                                {
                                                    IsInRange = false;
                                                    if (PossibleTripleTeeth)
                                                        NbrDent++;
                                                    else
                                                        NbrDent = NbrDent + 2;
                                                }
                                                else
                                                {
                                                    SubX = SelectedCell.X - WorkinRect.PartsWidthEnd;
                                                    LevelY = LevelY + WorkinRect.PartHeight;
                                                }
                                            }
                                            else if (LinkCase.IsContent)
                                            {
                                                NotLinked = false;

                                                NbrDent++;
                                            }
                                            else
                                            {
                                                var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                    enf.X == SelectedCell.X - SubX && enf.Y == SearchYMinus - LevelY);
                                                if (CaseAdjacent.IsContent == false)
                                                {
                                                    AdjacentContent = false;
                                                    if (PossibleTripleTeeth)
                                                        NbrDent++;
                                                    else
                                                        NbrDent = NbrDent + 2;
                                                }
                                                else
                                                {
                                                    SubX++;
                                                }
                                            }
                                        }

                                        if (NotLinked == false)
                                        {
                                            NotLinked = true;
                                            AdjacentContent = true;
                                            IsInRange = true;
                                            var IncX = InitXPlus;

                                            LevelY = InitLevelPlus;
                                            while (NotLinked && AdjacentContent && IsInRange)
                                            {
                                                var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                                    enf.X == SelectedCell.X + IncX && enf.Y == SearchYMinus + LevelY);
                                                if (LinkCase == null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                                {
                                                    var PartNbr = Convert.ToInt32(Math.Ceiling(
                                                        (SelectedCell.Y + LevelY - WorkinRect.PartsHeightStart) /
                                                        (double)WorkinRect.PartHeight));
                                                    if (PartNbr >= WorkinRect.NbrPart)
                                                    {
                                                        IsInRange = false;
                                                    }
                                                    else
                                                    {
                                                        IncX = SelectedCell.X - WorkinRect.PartsWidthStart;
                                                        LevelY = LevelY + WorkinRect.PartHeight;
                                                    }
                                                }
                                                else if (LinkCase.IsContent)
                                                {
                                                    NotLinked = false;
                                                    NbrDent--;
                                                }
                                                else
                                                {
                                                    var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                        enf.X == SelectedCell.X + IncX && enf.Y == SelectedCell.Y + LevelY);
                                                    if (CaseAdjacent.IsContent == false)
                                                        AdjacentContent = false;
                                                    else
                                                        IncX++;
                                                }
                                            }
                                        }
                                    }
                                    else if (NotLinked == false)
                                    {
                                        NotLinked = true;
                                        var AdjacentContent = true;
                                        var IsInRange = true;
                                        var SubX = InitXMinus;
                                        var LevelY = InitLevelMinus;
                                        while (NotLinked && AdjacentContent && IsInRange)
                                        {
                                            var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == SelectedCell.X - SubX && enf.Y == SearchYPlus - LevelY);
                                            if (LinkCase == null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                            {
                                                var PartNbr = Convert.ToInt32(Math.Ceiling(
                                                    (SelectedCell.Y - LevelY - WorkinRect.PartsHeightStart) /
                                                    (double)WorkinRect.PartHeight));
                                                if (PartNbr <= 1)
                                                {
                                                    IsInRange = false;
                                                }
                                                else
                                                {
                                                    SubX = SelectedCell.X - WorkinRect.PartsWidthEnd;
                                                    LevelY = LevelY + WorkinRect.PartHeight;
                                                }
                                            }
                                            else if (LinkCase.IsContent)
                                            {
                                                NotLinked = false;

                                                NbrDent--;
                                            }
                                            else
                                            {
                                                var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                    enf.X == SelectedCell.X - SubX && enf.Y == SelectedCell.Y - LevelY);
                                                if (CaseAdjacent.IsContent == false)
                                                    AdjacentContent = false;
                                                else
                                                    SubX++;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    SelectedCell.DentFil = 1;
                                    SelectedComposant.EnfNbrFil++;
                                }

                                SelectedCell.Content = SelectedComposant;
                            }
                        }
                        else
                        {
                            if (IsDent)
                            {
                                var LeftAdjacentCellOnNextLine = EnfilageBoard.Single(enf =>
                                    enf.X == SelectedCell.X + 1 && enf.Y == SelectedCell.Y + 1);

                                var RightAdjacentCellOnNextLine = EnfilageBoard.Single(enf =>
                                    enf.X == SelectedCell.X - 1 && enf.Y == SelectedCell.Y + 1);
                                var Case_Pos2Plus = EnfilageBoard.Single(enf =>
                                    enf.X == SelectedCell.X + 1 && enf.Y == SelectedCell.Y - 1);
                                var Case_Pos2Minus = EnfilageBoard.Single(enf =>
                                    enf.X == SelectedCell.X - 1 && enf.Y == SelectedCell.Y - 1);
                                var Content_Pos1Plus = false;
                                var Content_Pos2Plus = false;
                                var Content_Pos1Minus = false;
                                var Content_Pos2Minus = false;
                                var InitLevelPlus = 0;
                                var InitXPlus = 1;
                                var InitLevelMinus = 0;
                                var InitXMinus = 1;
                                if (LeftAdjacentCellOnNextLine == null || WorkinRect.PartsWidthEnd < LeftAdjacentCellOnNextLine.X)
                                {
                                    var PartNbr = Convert.ToInt32(Math.Ceiling(
                                        (SelectedCell.Y - WorkinRect.PartsHeightStart) / (double)WorkinRect.PartHeight));

                                    if (PartNbr < WorkinRect.NbrPart)
                                    {
                                        InitXPlus = WorkinRect.PartsWidthStart - SelectedCell.X;
                                        InitLevelPlus = InitLevelPlus + WorkinRect.PartHeight;

                                        Content_Pos1Plus = EnfilageBoard.Single(enf =>
                                            enf.X == SelectedCell.X + InitXPlus &&
                                            enf.Y == SelectedCell.Y + 1 + InitLevelPlus).IsContent;
                                        Content_Pos2Plus = EnfilageBoard.Single(enf =>
                                            enf.X == SelectedCell.X + InitXPlus &&
                                            enf.Y == SelectedCell.Y - 1 + InitLevelPlus).IsContent;
                                    }
                                }
                                else
                                {
                                    Content_Pos1Plus = EnfilageBoard.Single(enf =>
                                        enf.X == SelectedCell.X + 1 && enf.Y == SelectedCell.Y + 1).IsContent;
                                    Content_Pos2Plus = EnfilageBoard.Single(enf =>
                                        enf.X == SelectedCell.X + 1 && enf.Y == SelectedCell.Y - 1).IsContent;
                                }

                                if (RightAdjacentCellOnNextLine == null || WorkinRect.PartsWidthStart > LeftAdjacentCellOnNextLine.X)
                                {
                                    var PartNbr = Convert.ToInt32(Math.Ceiling(
                                        (SelectedCell.Y - WorkinRect.PartsHeightStart) / (double)WorkinRect.PartHeight));

                                    if (PartNbr > 1)
                                    {
                                        InitXMinus = SelectedCell.X - WorkinRect.PartsWidthEnd;
                                        InitLevelMinus = WorkinRect.PartHeight - InitLevelMinus;

                                        Content_Pos1Minus = EnfilageBoard.Single(enf =>
                                            enf.X == SelectedCell.X - InitXMinus &&
                                            enf.Y == SelectedCell.Y - InitLevelMinus + 1).IsContent;

                                        Content_Pos2Minus = EnfilageBoard.Single(enf =>
                                            enf.X == SelectedCell.X - InitXMinus &&
                                            enf.Y == SelectedCell.Y - InitLevelMinus - 1).IsContent;
                                    }
                                }
                                else
                                {
                                    Content_Pos1Minus = EnfilageBoard.Single(enf =>
                                        enf.X == SelectedCell.X - 1 && enf.Y == SelectedCell.Y + 1).IsContent;

                                    Content_Pos2Minus = EnfilageBoard.Single(enf =>
                                        enf.X == SelectedCell.X - 1 && enf.Y == SelectedCell.Y - 1).IsContent;
                                }

                                var SearchXPlus = 0;
                                var SearchYPlus = 0;
                                var SearchXMinus = 0;
                                var SearchYMinus = 0;
                                var PossibleTripleTeeth = false;
                                var NotLinked = true;

                                if ((SelectedCell.position == 1 && Content_Pos1Plus)
                                    || (SelectedCell.position == 2 && Content_Pos2Plus))
                                {
                                    SearchXPlus = SelectedCell.X + 1;
                                    if (SelectedCell.position == 1)
                                        SearchYPlus = SelectedCell.Y + 1;
                                    else
                                        SearchYPlus = SelectedCell.Y - 1;

                                    NotLinked = true;
                                    var AdjacentContent = true;
                                    var IsInRange = true;
                                    var IncX = InitXPlus + 1;
                                    PossibleTripleTeeth = true;
                                    var LevelY = InitLevelPlus;
                                    while (NotLinked && AdjacentContent && IsInRange)
                                    {
                                        var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                            enf.X == SelectedCell.X + IncX && enf.Y == SelectedCell.Y + LevelY);
                                        if (LinkCase == null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                        {
                                            var PartNbr = Convert.ToInt32(Math.Ceiling(
                                                (SelectedCell.Y + LevelY - WorkinRect.PartsHeightStart) /
                                                (double)WorkinRect.PartHeight));

                                            if (PartNbr >= WorkinRect.NbrPart)
                                            {
                                                IsInRange = false;
                                                NbrDent = NbrDent - 2;
                                            }
                                            else
                                            {
                                                IncX = SelectedCell.X - WorkinRect.PartsWidthStart;
                                                LevelY = LevelY + WorkinRect.PartHeight;
                                            }
                                        }
                                        else if (LinkCase.IsContent)
                                        {
                                            NotLinked = false;

                                            NbrDent--;
                                        }
                                        else
                                        {
                                            var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == SelectedCell.X + IncX && enf.Y == SearchYPlus + LevelY);
                                            if (CaseAdjacent.IsContent == false)
                                            {
                                                AdjacentContent = false;
                                                NbrDent = NbrDent - 2;
                                            }
                                            else
                                            {
                                                IncX++;
                                            }
                                        }
                                    }
                                }

                                if ((SelectedCell.position == 1 && Content_Pos1Minus)
                                    || (SelectedCell.position == 2 && Content_Pos2Minus))
                                {
                                    SearchXMinus = SelectedCell.X + 1;
                                    if (SelectedCell.position == 1)
                                        SearchYMinus = SelectedCell.Y + 1;
                                    else
                                        SearchYMinus = SelectedCell.Y - 1;

                                    var AdjacentContent = true;
                                    var IsInRange = true;
                                    NotLinked = true;
                                    var SubX = InitXMinus + 1;
                                    var LevelY = InitLevelMinus;
                                    while (NotLinked && AdjacentContent && IsInRange)
                                    {
                                        var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                            enf.X == SelectedCell.X - SubX && enf.Y == SelectedCell.Y - LevelY);
                                        if (LinkCase == null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                        {
                                            var PartNbr = Convert.ToInt32(Math.Ceiling(
                                                (SelectedCell.Y + LevelY - WorkinRect.PartsHeightStart) /
                                                (double)WorkinRect.PartHeight));

                                            if (PartNbr <= 1)
                                            {
                                                IsInRange = false;
                                                if (PossibleTripleTeeth)
                                                    NbrDent--;
                                                else
                                                    NbrDent = NbrDent - 2;
                                            }
                                            else
                                            {
                                                SubX = SelectedCell.X - WorkinRect.PartsWidthStart;
                                                LevelY = LevelY + WorkinRect.PartHeight;
                                            }
                                        }
                                        else if (LinkCase.IsContent)
                                        {
                                            NotLinked = false;

                                            NbrDent--;
                                        }
                                        else
                                        {
                                            var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == SelectedCell.X - SubX && enf.Y == SearchYMinus - LevelY);
                                            if (CaseAdjacent.IsContent == false)
                                            {
                                                AdjacentContent = false;
                                                if (PossibleTripleTeeth)
                                                    NbrDent--;
                                                else
                                                    NbrDent = NbrDent - 2;
                                            }
                                            else
                                            {
                                                SubX++;
                                            }
                                        }
                                    }

                                    if (NotLinked == false)
                                    {
                                        NotLinked = true;
                                        AdjacentContent = true;
                                        IsInRange = true;
                                        var IncX = InitXPlus;

                                        LevelY = InitLevelPlus;
                                        while (NotLinked && AdjacentContent && IsInRange)
                                        {
                                            var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == SelectedCell.X + IncX && enf.Y == SearchYMinus + LevelY);
                                            if (LinkCase == null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                            {
                                                var PartNbr = Convert.ToInt32(Math.Ceiling(
                                                    (SelectedCell.Y + LevelY - WorkinRect.PartsHeightStart) /
                                                    (double)WorkinRect.PartHeight));

                                                if (PartNbr >= WorkinRect.NbrPart)
                                                {
                                                    IsInRange = false;
                                                }
                                                else
                                                {
                                                    IncX = SelectedCell.X - WorkinRect.PartsWidthStart;
                                                    LevelY = LevelY + WorkinRect.PartHeight;
                                                }
                                            }
                                            else if (LinkCase.IsContent)
                                            {
                                                NotLinked = false;
                                                NbrDent++;
                                            }
                                            else
                                            {
                                                var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                    enf.X == SelectedCell.X + IncX && enf.Y == SelectedCell.Y + LevelY);
                                                if (CaseAdjacent.IsContent == false)
                                                    AdjacentContent = false;
                                                else
                                                    IncX++;
                                            }
                                        }
                                    }
                                }
                                else if (NotLinked == false)
                                {
                                    NotLinked = true;
                                    var AdjacentContent = true;
                                    var IsInRange = true;
                                    var SubX = InitXMinus;
                                    var LevelY = InitLevelMinus;
                                    while (NotLinked && AdjacentContent && IsInRange)
                                    {
                                        var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                            enf.X == SelectedCell.X - SubX && enf.Y == SearchYPlus);
                                        if (LinkCase == null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                        {
                                            var PartNbr = Convert.ToInt32(Math.Ceiling(
                                                (SelectedCell.Y + LevelY - WorkinRect.PartsHeightStart) /
                                                (double)WorkinRect.PartHeight));

                                            if (PartNbr <= 1)
                                            {
                                                IsInRange = false;
                                            }
                                            else
                                            {
                                                SubX = SelectedCell.X - WorkinRect.PartsWidthStart;
                                                LevelY = LevelY + WorkinRect.PartHeight;
                                            }
                                        }
                                        else if (LinkCase.IsContent)
                                        {
                                            NotLinked = false;

                                            NbrDent++;
                                        }
                                        else
                                        {
                                            var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == SelectedCell.X - SubX && enf.Y == SelectedCell.Y);
                                            if (CaseAdjacent.IsContent == false)
                                                AdjacentContent = false;
                                            else
                                                SubX++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                SelectedCell.Content.EnfNbrFil--;
                            }

                            SelectedCell.Content = null;
                        }

                        SettingContent(SelectedCell);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        public void SetContent(int Num)
        {
            try
            {
                if (ListComposant.Count >= Num)
                {
                    SelectedComposant = ListComposant[Num - 1];
                    SetMatrixContent();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }


        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Border_LostFocus_1(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Border_GotFocus_1(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ImageFocus(object sender, RoutedEventArgs e)
        {
            var img = (Image)sender;
            SelectedCell = (MatrixElement)img.Tag;
            SelectedCell.IsSelected = true;
        }

        private void ImageLostFocus(object sender, RoutedEventArgs e)
        {
            var img = (Image)sender;
            SelectedCell = (MatrixElement)img.Tag;
            SelectedCell.IsSelected = false;
        }

        public void ReedCalculation()
        {
            if (DentList != null && DentList.Count == 0)
            {
                var dent = new LinkedList<MatrixElement>();
                dent.AddLast(SelectedCell);
                DentList.AddLast(dent);
                NbrDent++;
                var MRang = new Rang();
                MRang.Y1 = SelectedCell.Y;
                Rangs.AddLast(MRang);
            }
            else
            {
                if (Rangs.Count == 1 && Rangs.First().Y2 == -1)
                {
                    if (SelectedCell.Y == Rangs.First().Y1)
                    {
                        var node = RogueDentList.First;
                        LinkedListNode<LinkedList<MatrixElement>> FoundNode = null;
                        var ContinueLoop = true;
                        var LastCheck = false;
                        while (node != null && ContinueLoop)
                            if (LastCheck == false)
                            {
                                if (node.Value.First().X == SelectedCell.X + 1)
                                {
                                    node.Value.AddFirst(SelectedCell);
                                    ContinueLoop = false;
                                    FoundNode = node;
                                }
                                else if (node.Value.Last().X == SelectedCell.X - 1)
                                {
                                    node.Value.AddLast(SelectedCell);
                                    LastCheck = true;
                                    FoundNode = node;
                                }

                                node = node.Next;
                            }
                            else
                            {
                                if (node.Value.First().X == SelectedCell.X + 1)
                                {
                                    var tempList = node.Value;
                                    var PrevNode = node.Previous;
                                    foreach (var el in tempList)
                                    {
                                        PrevNode.Value.AddLast(el);
                                        RogueDentList.Remove(node.Value);
                                    }

                                    FoundNode = PrevNode;
                                }

                                ContinueLoop = false;
                            }

                        if (DentList.Last().First().X - 1 == SelectedCell.X)
                        {
                            if (FoundNode == null)
                                DentList.Last().AddFirst(SelectedCell);
                            else
                                foreach (var el in FoundNode.Value.Reverse())
                                    DentList.Last().AddFirst(el);
                        }
                        else if (DentList.Last().Last().X + 1 == SelectedCell.X)
                        {
                            if (FoundNode == null)
                                DentList.Last().AddLast(SelectedCell);
                            else
                                foreach (var el in FoundNode.Value)
                                    DentList.Last().AddLast(el);
                        }
                    }
                    else if (SelectedCell.Y == Rangs.First().Y1 - 1 || SelectedCell.Y == Rangs.First().Y1 + 1)
                    {
                       
                    }
                }
            }
        }


        private void ImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (SelectedCell != null)
                    SelectedCell.IsSelected = false;
                var img = (Image)sender;
                SelectedCell = (MatrixElement)img.Tag;

                SetMatrixContent();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void ImageMouseDown1(object sender, MouseButtonEventArgs e)
        {
            if (SelectedChaineCell != null)
                SelectedChaineCell.IsSelected = false;
            var border = (Image)sender;
            SelectedChaineCell = (ChaineMatrixElement)border.Tag;
            if(SelectedChaineCell.IsContent==false)
            {
                SelectedChaineCell.IsContent = true;
            }else
            {
                SelectedChaineCell.IsContent = false;
            }
           
        }
        private bool _AllowDrag=true;

        public bool AllowDrag
        {
            get { return _AllowDrag; }
            set { _AllowDrag= value; NotifyPropertyChanged(); }
        }


        private void MouseDragElementBehavior_OnDragFinished(object sender, MouseEventArgs e)
        {
            var df = (MouseDragElementBehavior)sender;
            this.SetValue(CanvasControl.LastXpositionProperty, df.X.ToString());
            this.SetValue(CanvasControl.LastYpositionProperty, df.Y.ToString());
            


        }

            private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            IsDent = true;
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            IsDent = false;
        }
        private bool IsSizeChanged;
        private bool ExecuteProhibitedArea;

        private void Board_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
            IsSizeChanged = true;
            if(ExecuteProhibitedArea)
            {
                if (OldRectangle != null)
                    SetProhibitedArea(OldRectangle, MatrixElement.BoxType.OutRange);

               SetProhibitedArea(ProhibitArea, MatrixElement.BoxType.Inaccessible);
                ExecuteProhibitedArea = false;
            }

        }
    }
}