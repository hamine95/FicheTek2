using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BackEnd2.Model;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xaml.Behaviors.Layout;

namespace DSheetEnfilage
{
    public partial class CanvasControl : UserControl, INotifyPropertyChanged
    {
        private bool _Col7;

        public bool Col7
        {
            get { return _Col7; }
            set
            {
                _Col7 = value;
                if (value == true)
                {
                    SelectedComp = null;
                    SelectedColChain = "7";
                }

                NotifyPropertyChanged();
            }
        }

        private int _ChainColNum = 8;

        public int ChainColNum
        {
            get
            {
                return _ChainColNum;
            }
            set
            {
                _ChainColNum = value;
                NotifyPropertyChanged();
            }
        }

        private bool _Btn9Vis;

        public bool Btn9Vis
        {
            get
            {
                return _Btn9Vis;
            }
            set
            {
                _Btn9Vis = value;
                NotifyPropertyChanged();
            }
        }
        private bool _Btn10Vis;

        public bool Btn10Vis
        {
            get
            {
                return _Btn10Vis;
            }
            set
            {
                _Btn10Vis = value;
                NotifyPropertyChanged();
            }
        }
        private string _SelectedColChain;

        public string SelectedColChain
        {
            get { return _SelectedColChain; }
            set
            {
                _SelectedColChain = value;
                NotifyPropertyChanged();
            }
        }

        public static readonly DependencyProperty AngleChainProperty = DependencyProperty.Register(
            nameof(AngleChain), typeof(int), typeof(CanvasControl), new PropertyMetadata(0,SetAngleChain));

        public int AngleChain
        {
            get { return (int)GetValue(AngleChainProperty); }
            set { SetValue(AngleChainProperty, value); }
        }
        
        public static readonly DependencyProperty ChainRotateYProperty = DependencyProperty.Register(
            nameof(ChainRotateY), typeof(int), typeof(CanvasControl), new PropertyMetadata(0,SetChainRotateY));

        public int ChainRotateY
        {
            get { return (int)GetValue(ChainRotateYProperty); }
            set { SetValue(ChainRotateYProperty, value); }
        }
        public static readonly DependencyProperty ChainRotateXProperty = DependencyProperty.Register(
            nameof(ChainRotateX), typeof(int), typeof(CanvasControl), new PropertyMetadata(0,SetChainRotateX));

        public int ChainRotateX
        {
            get { return (int)GetValue(ChainRotateXProperty); }
            set { SetValue(ChainRotateXProperty, value); }
        }
        public static void SetAngleChain(DependencyObject e,DependencyPropertyChangedEventArgs arg)
        {
            
        }
        public static void SetChainRotateX(DependencyObject e,DependencyPropertyChangedEventArgs arg)
        {
            
        }
        public static void SetChainRotateY(DependencyObject e,DependencyPropertyChangedEventArgs arg)
        {
            
        }
        private bool _Col8;

        public bool Col8
        {
            get { return _Col8; }
            set
            {
                _Col8 = value;
                if (value == true)
                {
                    SelectedComp = null;
                    SelectedColChain = "8";
                }

                NotifyPropertyChanged();
            }
        }

        private bool _Col9;

        public bool Col9
        {
            get { return _Col9; }
            set
            {
                _Col9 = value;
                if (value == true)
                {
                    SelectedComp = null;
                    SelectedColChain = "9";
                }

                NotifyPropertyChanged();
            }
        }
        private bool _Col10;

        public bool Col10
        {
            get { return _Col10; }
            set
            {
                _Col10 = value;
                if (value == true)
                {
                    SelectedComp = null;
                    SelectedColChain = "10";
                }

                NotifyPropertyChanged();
            }
        }
        private bool _Col1;

        public bool Col1
        {
            get { return _Col1; }
            set
            {
                _Col1 = value;
                if (value == true)
                {
                    SelectedComp = null;
                    SelectedColChain = "1";
                }

                NotifyPropertyChanged();
            }
        }

        private bool _Col2;

        public bool Col2
        {
            get { return _Col2; }
            set
            {
                _Col2 = value;
                if (value == true)
                {
                    SelectedComp = null;
                    SelectedColChain = "2";
                }

                NotifyPropertyChanged();
            }
        }

        private bool _Col3;

        public bool Col3
        {
            get { return _Col3; }
            set
            {
                _Col3 = value;
                if (value == true)
                {
                    SelectedComp = null;
                    SelectedColChain = "3";
                }

                NotifyPropertyChanged();
            }
        }

        private bool _Col4;

        public bool Col4
        {
            get { return _Col4; }
            set
            {
                _Col4 = value;
                if (value == true)
                {
                    SelectedComp = null;
                    SelectedColChain = "4";
                }

                NotifyPropertyChanged();
            }
        }

        private static void OnSetRectWork(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CanvasControl mycontrol = (CanvasControl)d;
            mycontrol.SetWorkRectangle();

        }

        public void SetWorkRectangle()
        {
            if (WorkinRect == null)
            {
                foreach (var EnfCont in EnfilageBoard)
                {
                    if (EnfCont.TypBox != MatrixElement.BoxType.OutRange)
                    {
                        EnfCont.SetBoxType(MatrixElement.BoxType.OutRange);
                    }
                }
            }
            else
            {
                foreach (var EnfCont in EnfilageBoard)
            {
                if (EnfCont.X >= WorkinRect.PartsWidthStart
                    && EnfCont.X <= WorkinRect.PartsWidthEnd
                    && EnfCont.Y >= WorkinRect.PartsHeightStart
                    && EnfCont.Y <= WorkinRect.PartsHeightEnd)
                {
                    int PartNbr =(EnfCont.Y- WorkinRect.PartsHeightStart) / WorkinRect.PartHeight;
                    if ((EnfCont.Y - (PartNbr * WorkinRect.PartHeight)) ==
                        (WorkinRect.PartHeight - 2 + WorkinRect.PartsHeightStart)
                        || (EnfCont.Y - (PartNbr * WorkinRect.PartHeight)) ==
                        (WorkinRect.PartHeight - 3 + WorkinRect.PartsHeightStart))
                    {
                        EnfCont.SetBoxType(MatrixElement.BoxType.Dents);

                        if ((EnfCont.Y - (PartNbr * WorkinRect.PartHeight)) ==
                            (WorkinRect.PartHeight - 2 + WorkinRect.PartsHeightStart))
                        {
                            EnfCont.position = 2;
                        }
                        else
                        {
                            EnfCont.position = 1;
                        }

                    }
                    else if ((EnfCont.Y - (PartNbr * WorkinRect.PartHeight)) >=
                             (WorkinRect.PartsHeightStart)
                             && (EnfCont.Y - (PartNbr * WorkinRect.PartHeight)) <=
                             (WorkinRect.PartsHeightStart+(WorkinRect.PartHeight-5)))
                    {
                       int positionEl =EnfCont.Y - (PartNbr * WorkinRect.PartHeight)-(WorkinRect.PartsHeightStart)+1;
                        EnfCont.SetBoxType(MatrixElement.BoxType.Lisses);

                        EnfCont.SupportedComp = SelectedChaine.ChaineCompos.Single(ch => ch.ColNum == positionEl).Comp;
                        EnfCont.position = positionEl;

                    }
                    else
                    {
                        EnfCont.SetBoxType(MatrixElement.BoxType.Empty);
                    }
                }

            }
            }
            
        }
        
        public static readonly DependencyProperty WorkinRectProperty = DependencyProperty.Register(
            nameof(WorkinRect), typeof(WorkRectangle), typeof(CanvasControl), new PropertyMetadata(null,OnSetRectWork));

        public WorkRectangle WorkinRect
        {
            get { return (WorkRectangle)GetValue(WorkinRectProperty); }
            set { SetValue(WorkinRectProperty, value); }
        }
        
        public static readonly DependencyProperty CompListProperty =
            DependencyProperty.RegisterAttached("CompList", typeof(List<Composant>),
                typeof(CanvasControl), new PropertyMetadata(null, OnSetCompList));

        private static void OnSetCompList(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
         
        }

        public void SetInaccessibleCompCells()
        {
            if (IsDentFil )
            {
                if (SelectedChaine != null && ChRowSum >= 78)
                {
                    int NumRow =ChaineColumns*2+1;
                        
                    int lastcol = 4+ChaineRows;
                    if (ChaineColumns <= 8)
                    {
                        NumRow = 8*2+1;
                    }
                    int startRow= 58 - NumRow;
                    for(int j=startRow;j<58;j++)
                    {
                        for (int i = 0; i < lastcol; i++)
                        {
                            int ij = j * 83 + i;
                            EnfilageBoard[ij].SetBoxType(MatrixElement.BoxType.Inaccessible);
                        }
                    }
                }
                else
                {
                    if (ListComposant.Count > 0)
                {
                    int NumCol =Convert.ToInt32(Math.Ceiling((ListComposant.Count*18)/(double)12));
                    int StartCol = 58 - NumCol;
                    for (int i = StartCol; i <= 57; i++)
                    {
                        for (int j = 76; j <= 82; j++)
                        {
                            int ij = i * 83+j;
                            EnfilageBoard[ij].SetBoxType(MatrixElement.BoxType.Inaccessible);
                        }
                    
                    }
                }

                if (SelectedChaine != null)
                {
                    if (ChaineRows <= 26)
                    {
                        int NumRow = 4 + ChaineRows;
                        int startRow = 58 - NumRow;
                        int lastcol = ChaineColumns;
                        if (ChaineColumns <= 8)
                        {
                            lastcol = 8;
                        }
                       
                        for(int j=startRow;j<58;j++)
                        {
                            for (int i = 0; i < lastcol; i++)
                            {
                                int ij = j * 83 + i;
                                EnfilageBoard[ij].SetBoxType(MatrixElement.BoxType.Inaccessible);
                            }
                            }
                        
                    }else if (ChRowSum > 26)
                    {
                        int NumRow =ChaineColumns+1;
                        
                        int lastcol = 4+ChaineRows;
                        if (ChaineColumns <= 8)
                        {
                            NumRow = 8+1;
                        }
                        int startRow= 58 - NumRow;
                        for(int j=startRow;j<58;j++)
                        {
                            for (int i = 0; i < lastcol; i++)
                            {
                                int ij = j * 83 + i;
                                EnfilageBoard[ij].SetBoxType(MatrixElement.BoxType.Inaccessible);
                            }
                        }
                    }
                        
                        
                }
                }
                
                
            }
            else if (IsDentFil == false && WorkinRect == null  )
            {

               if (SelectedChaine != null && ChRowSum >= 78)
                {
                    int NumRow =ChaineColumns*2+1;
                        
                    int lastcol = 4+ChaineRows;
                    if (ChaineColumns <= 8)
                    {
                        NumRow = 8*2+1;
                    }
                    int startRow= 58 - NumRow;
                    for(int j=startRow;j<58;j++)
                    {
                        for (int i = 0; i < lastcol; i++)
                        {
                            int ij = j * 83 + i;
                            EnfilageBoard[ij].SetBoxType(MatrixElement.BoxType.OutRange);
                        }
                    }
                }
                else
                {
                    if (ListComposant.Count > 0)
                {
                    int NumCol =Convert.ToInt32(Math.Ceiling((ListComposant.Count*18)/(double)12));
                    int StartCol = 58 - NumCol;
                    for (int i = StartCol; i <= 57; i++)
                    {
                        for (int j = 76; j <= 82; j++)
                        {
                            int ij = i * 83+j;
                            EnfilageBoard[ij].SetBoxType(MatrixElement.BoxType.OutRange);
                        }
                    
                    }
                }

                if (SelectedChaine != null)
                {
                    if (ChaineRows <= 26)
                    {
                        int NumRow = 4 + ChaineRows;
                        int startRow = 58 - NumRow;
                        int lastcol = ChaineColumns;
                        if (ChaineColumns <= 8)
                        {
                            lastcol = 8;
                        }
                       
                        for(int j=startRow;j<58;j++)
                        {
                            for (int i = 0; i < lastcol; i++)
                            {
                                int ij = j * 83 + i;
                                EnfilageBoard[ij].SetBoxType(MatrixElement.BoxType.OutRange);
                            }
                            }
                        
                    }else if (ChRowSum > 26)
                    {
                        int NumRow =ChaineColumns+1;
                        
                        int lastcol = 4+ChaineRows;
                        if (ChaineColumns <= 8)
                        {
                            NumRow = 8+1;
                        }
                        int startRow= 58 - NumRow;
                        for(int j=startRow;j<58;j++)
                        {
                            for (int i = 0; i < lastcol; i++)
                            {
                                int ij = j * 83 + i;
                                EnfilageBoard[ij].SetBoxType(MatrixElement.BoxType.OutRange);
                            }
                        }
                    }
                        
                        
                }
                }
               
            }
        }

        public static readonly DependencyProperty SelectedChaineProperty = DependencyProperty.Register(
            nameof(SelectedChaine), typeof(chaine), typeof(CanvasControl), new PropertyMetadata(null,OnSetSelectedChain));

        public chaine SelectedChaine
        {
            get { return (chaine)GetValue(SelectedChaineProperty); }
            set { SetValue(SelectedChaineProperty, value); }
        }

        public static void OnSetSelectedChain(DependencyObject obj,DependencyPropertyChangedEventArgs e)
        {
            
        }
        private List<Composant> _CompList;

        public List<Composant> CompList
        {
            get => (List<Composant>)GetValue(CompListProperty);
            set { SetValue(CompListProperty, value); }
        }

        private Composant _SelectedComp;

        public Composant SelectedComp
        {
            get { return _SelectedComp; }
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

        public static readonly DependencyProperty SelectedColCompProperty = DependencyProperty.Register(
            nameof(SelectedColComp), typeof(ChColComp), typeof(CanvasControl),
            new PropertyMetadata(null, OnsetChaineColComp));

        public ChColComp SelectedColComp
        {
            get { return (ChColComp)GetValue(SelectedColCompProperty); }
            set { SetValue(SelectedColCompProperty, value); }
        }

        public static void OnsetChaineColComp(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
      
            
        }

        private bool _Col5;

        public bool Col5
        {
            get { return _Col5; }
            set
            {
                _Col5 = value;
                if (value == true)
                {
                    SelectedComp = null;
                    SelectedColChain = "5";
                }

                NotifyPropertyChanged();
            }
        }

        private bool _Col6;

        public bool Col6
        {
            get { return _Col6; }
            set
            {
                _Col6 = value;
                if (value == true)
                {
                    SelectedComp = null;
                    SelectedColChain = "6";
                }

                NotifyPropertyChanged();
            }
        }

        private bool _Btn8;

        public bool Btn8
        {
            get { return _Btn8; }
            set
            {
                _Btn8 = value;
                NotifyPropertyChanged();
            }
        }
        private bool _Btn9;

        public bool Btn9
        {
            get { return _Btn9; }
            set
            {
                _Btn9 = value;
                NotifyPropertyChanged();
            }
        }
        private bool _Btn10;

        public bool Btn10
        {
            get { return _Btn10; }
            set
            {
                _Btn10 = value;
                NotifyPropertyChanged();
            }
        }
        private bool _Btn1;

        public bool Btn1
        {
            get { return _Btn1; }
            set
            {
                _Btn1 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _Btn2;

        public bool Btn2
        {
            get { return _Btn2; }
            set
            {
                _Btn2 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _Btn3;

        public bool Btn3
        {
            get { return _Btn3; }
            set
            {
                _Btn3 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _Btn4;

        public bool Btn4
        {
            get { return _Btn4; }
            set
            {
                _Btn4 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _Btn5;

        public bool Btn5
        {
            get { return _Btn5; }
            set
            {
                _Btn5 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _Btn6;

        public bool Btn6
        {
            get { return _Btn6; }
            set
            {
                _Btn6 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _Btn7;

        public bool Btn7
        {
            get { return _Btn7; }
            set
            {
                _Btn7 = value;
                NotifyPropertyChanged();
            }
        }

        public static readonly DependencyProperty enfilageElementProperty = DependencyProperty.Register(
            nameof(enfilageElement), typeof(EnfilageElement), typeof(CanvasControl),
            new PropertyMetadata(default(EnfilageElement)));

        public EnfilageElement enfilageElement
        {
            get { return (EnfilageElement)GetValue(enfilageElementProperty); }
            set { SetValue(enfilageElementProperty, value); }
        }

        public static readonly DependencyProperty EnableChaineEditProperty = DependencyProperty.Register(
            nameof(EnableChaineEdit), typeof(bool), typeof(CanvasControl),
            new PropertyMetadata(default(bool), OnStartEditing));

        public static void OnStartEditing(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CanvasControl mycontrol = (CanvasControl)d;
            mycontrol.StartEditing();
        }

        public void StartEditing()
        {
            if (EnableChaineEdit == true)
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

        public bool EnableChaineEdit
        {
            get { return (bool)GetValue(EnableChaineEditProperty); }
            set { SetValue(EnableChaineEditProperty, value); }
        }


        public static readonly DependencyProperty RangsProperty = DependencyProperty.Register(
            nameof(Rangs), typeof(LinkedList<Rang>), typeof(CanvasControl), new PropertyMetadata(null, OnSetRang));

        public LinkedList<Rang> Rangs
        {
            get { return (LinkedList<Rang>)GetValue(RangsProperty); }
            set { SetValue(RangsProperty, value); }
        }

        public static readonly DependencyProperty RogueDentListProperty = DependencyProperty.Register(
            nameof(RogueDentList), typeof(LinkedList<LinkedList<MatrixElement>>), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetRogueDentList));

        public LinkedList<LinkedList<MatrixElement>> RogueDentList
        {
            get { return (LinkedList<LinkedList<MatrixElement>>)GetValue(RogueDentListProperty); }
            set { SetValue(RogueDentListProperty, value); }
        }

        public static readonly DependencyProperty DentListProperty = DependencyProperty.Register(
            nameof(DentList), typeof(LinkedList<LinkedList<MatrixElement>>), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetDentList));

        public LinkedList<LinkedList<MatrixElement>> DentList
        {
            get { return (LinkedList<LinkedList<MatrixElement>>)GetValue(DentListProperty); }
            set { SetValue(DentListProperty, value); }
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

        public bool IsDent = true;

        public static readonly DependencyProperty NbrDentProperty = DependencyProperty.Register(
            nameof(NbrDent), typeof(int), typeof(CanvasControl), new PropertyMetadata(default(int)));

        public int NbrDent
        {
            get { return (int)GetValue(NbrDentProperty); }
            set { SetValue(NbrDentProperty, value); }
        }

        private bool _IsEditChain;

        public bool IsEditChain
        {
            get { return _IsEditChain; }
            set
            {
                _IsEditChain = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsEditChainBtn;

        public bool IsEditChainBtn
        {
            get { return _IsEditChainBtn; }
            set
            {
                _IsEditChainBtn = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsEditChain2;

        public bool IsEditChain2
        {
            get { return _IsEditChain2; }
            set
            {
                _IsEditChain2 = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsDisplayChain = true;

        public bool IsDisplayChain
        {
            get { return _IsDisplayChain; }
            set
            {
                _IsDisplayChain = value;
                NotifyPropertyChanged();
            }
        }

        public static readonly DependencyProperty IsDentFilProperty = DependencyProperty.Register(
            nameof(IsDentFil), typeof(bool), typeof(CanvasControl), new PropertyMetadata(default(bool),OnSetComposantList));

        public bool IsDentFil
        {
            get { return (bool)GetValue(IsDentFilProperty); }
            set { SetValue(IsDentFilProperty, value); }
        }

        public static readonly DependencyProperty TrameXpositionProperty = DependencyProperty.Register(
            nameof(TrameXposition), typeof(string), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetTramePosition));

        public string TrameXposition
        {
            get { return (string)GetValue(TrameXpositionProperty); }
            set { SetValue(TrameXpositionProperty, value); }
        }

        public static readonly DependencyProperty TrameYpositionProperty = DependencyProperty.Register(
            nameof(TrameYposition), typeof(string), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetTramePosition));

        public string TrameYposition
        {
            get { return (string)GetValue(TrameYpositionProperty); }
            set { SetValue(TrameYpositionProperty, value); }
        }

        public static readonly DependencyProperty LastXpositionProperty = DependencyProperty.Register(
            nameof(LastXposition), typeof(string), typeof(CanvasControl), new PropertyMetadata(null, OnSetPosition));

        public string LastXposition
        {
            get { return (string)GetValue(LastXpositionProperty); }
            set
            {
                SetValue(LastXpositionProperty, value);
                NotifyPropertyChanged();
            }
        }

        public static readonly DependencyProperty LastYpositionProperty = DependencyProperty.Register(
            nameof(LastYposition), typeof(string), typeof(CanvasControl), new PropertyMetadata(null, OnSetPosition));

        public string LastYposition
        {
            get { return (string)GetValue(LastYpositionProperty); }
            set
            {
                SetValue(LastYpositionProperty, value);
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty ChaineList2ValueProperty = DependencyProperty.Register(
            nameof(ChaineList2), typeof(ObservableCollection<ChaineMatrixElement>), typeof(CanvasControl), new PropertyMetadata(null,null));

        public ObservableCollection<ChaineMatrixElement> ChaineList2
        {
            get { return (ObservableCollection<ChaineMatrixElement>)GetValue(ChaineList2ValueProperty); }
            set { SetValue(ChaineList2ValueProperty, value); }
        }
        public static readonly DependencyProperty ChaineListValueProperty =
            DependencyProperty.Register("ChaineList", typeof(ObservableCollection<ChaineMatrixElement>),
                typeof(CanvasControl),
                new PropertyMetadata(null, null));

        public static readonly DependencyProperty ChaineRows2Property = DependencyProperty.Register(
            nameof(ChaineRows2), typeof(int), typeof(CanvasControl), new PropertyMetadata(0,null));

        public int ChaineRows2
        {
            get { return (int)GetValue(ChaineRows2Property); }
            set { SetValue(ChaineRows2Property, value); }
        }
        
        public static readonly DependencyProperty ChRowSumProperty = DependencyProperty.Register(
            nameof(ChRowSum), typeof(int), typeof(CanvasControl), new PropertyMetadata(0,OnSetChaine));

        public int ChRowSum
        {
            get { return (int)GetValue(ChRowSumProperty); }
            set { SetValue(ChRowSumProperty, value); }
        }
      
        

 
      
        
        public static readonly DependencyProperty ChaineColumnsValueProperty =
            DependencyProperty.Register("ChaineColumns", typeof(object), typeof(CanvasControl),
                new PropertyMetadata(0, OnSetChaineCol));

        private static void OnSetChaine(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mycontrol = (CanvasControl)d;
            mycontrol.RotateChain();
        }
        
        private bool _SecChainVis;

        public bool SecChainVis
        {
            get
            {
                return _SecChainVis;
            }
            set
            {
                _SecChainVis = value;
                NotifyPropertyChanged();
            }
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
                if (SecChainVis)
                {
                    SecChainVis = false;
                }
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
            CanvasControl myControl = (CanvasControl)d;
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
            }else if (ChaineColumns == 9)
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
            else  if (ChaineColumns == 9)
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
                string val = e.NewValue.ToString();
            }
        }

        private static void OnSetPosition(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public static readonly DependencyProperty ChaineRowsValueProperty =
            DependencyProperty.Register("ChaineRows", typeof(object), typeof(CanvasControl),
                new PropertyMetadata(0, null));

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

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<CanvasElement> ListItem;

        public CanvasControl()
        {
            InitializeComponent();
            SetRow = 58;
            SetColumn = 83;
        }

        [Browsable(true)] [Category("Action")] [Description("Invoked when user clicks button")]
        public static readonly DependencyProperty ListComposantProperty =
            DependencyProperty.RegisterAttached("ListComposant", typeof(ObservableCollection<Composition>),
                typeof(CanvasControl), new PropertyMetadata(null, OnSetComposantList));

        private static void OnSetComposantList(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mycontrol=  (CanvasControl)d;
            mycontrol.SetInaccessibleCompCells();
        }

        public static readonly DependencyProperty ContentEnfilageListProperty = DependencyProperty.Register(
            nameof(ContentEnfilageList), typeof(ObservableCollection<MatrixElement>), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetContentEnfilage));

        public ObservableCollection<MatrixElement> ContentEnfilageList
        {
            get { return (ObservableCollection<MatrixElement>)GetValue(ContentEnfilageListProperty); }
            set
            {
                SetValue(ContentEnfilageListProperty, value);
                NotifyPropertyChanged();
            }
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
            MatrixElement matx = ContentEnfilageList.FirstOrDefault(co => co.X == ex.X && co.Y == ex.Y);
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
            CanvasControl myControl = (CanvasControl)d;
            if (myControl.ContentEnfilageList != null && myControl.ContentEnfilageList.Count > 0)
            {
                myControl.SetInitContents();
            }
            else
            {
                myControl.ResetEnfilageBoard();
            }
        }

        public void ResetEnfilageBoard()
        {
            if (EnfilageBoard != null)
            {
                foreach (var element in EnfilageBoard)
                    if (element.Content != null)
                        element.Content = null;
            }
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

        public static readonly DependencyProperty EnfilageBoardProperty = DependencyProperty.Register(
            nameof(EnfilageBoard), typeof(ObservableCollection<MatrixElement>), typeof(CanvasControl),
            new PropertyMetadata(null, OnSetEnfBoard));

        private static void OnSetEnfBoard(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public ObservableCollection<Composition> ListComposant
        {
            get => (ObservableCollection<Composition>)GetValue(ListComposantProperty);
            set => SetValue(ListComposantProperty, value);
        }

        public ObservableCollection<MatrixElement> EnfilageBoard
        {
            get { return (ObservableCollection<MatrixElement>)GetValue(EnfilageBoardProperty); }
            set { SetValue(EnfilageBoardProperty, value); }
        }


        public BoardStructure _board;

        private void CanvasControl_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // _board = new BoardStructure(59, 83);
            // Board.ItemsSource=new ObservableCollection<MatrixElement>(_board.Board);
        }


        public static DependencyProperty SetRowProperty = DependencyProperty.RegisterAttached("SetRow", typeof(object),
            typeof(EnfilageSchemas), new PropertyMetadata(0, OnSetRow));

        private static void OnSetRow(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public object SetRow
        {
            get => GetValue(SetRowProperty);
            set { SetValue(SetRowProperty, value); }
        }

        public void MoveUp()
        {
            SelectedCell.IsSelected = false;
            if (SelectedCell.Y == 0)
                SelectedCell = EnfilageBoard.First(b => b.Y == Convert.ToInt32(SetRow) - 1 && b.X == SelectedCell.X);
            else
                SelectedCell = EnfilageBoard.First(b => b.Y == SelectedCell.Y - 1 && b.X == SelectedCell.X);

            SelectedCell.IsSelected = true;
        }

        public void MoveDown()
        {
            SelectedCell.IsSelected = false;
            if (SelectedCell.Y == Convert.ToInt32(SetRow) - 1)
                SelectedCell = EnfilageBoard.First(b => b.Y == 0 && b.X == SelectedCell.X);
            else
                SelectedCell = EnfilageBoard.First(b => b.Y == SelectedCell.Y + 1 && b.X == SelectedCell.X);

            SelectedCell.IsSelected = true;
        }

        private static void OnSetColumn(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // some code here
        }

        public static readonly DependencyProperty SetColumnProperty =
            DependencyProperty.RegisterAttached("SetColumn", typeof(object), typeof(EnfilageSchemas),
                new PropertyMetadata(0, OnSetColumn));

        public object SetColumn
        {
            get => GetValue(SetColumnProperty);
            set { SetValue(SetColumnProperty, value); }
        }

        public void MoveRight()
        {
            SelectedCell.IsSelected = false;
            if (SelectedCell.X == Convert.ToInt32(SetColumn) - 1)
                SelectedCell = EnfilageBoard.First(b => b.Y == SelectedCell.Y && b.X == 0);
            else
                SelectedCell = EnfilageBoard.First(b => b.Y == SelectedCell.Y && b.X == SelectedCell.X + 1);

            SelectedCell.IsSelected = true;
        }

        public void MoveLeft()
        {
            SelectedCell.IsSelected = false;
            if (SelectedCell.X == 0)
                SelectedCell = EnfilageBoard.First(b => b.Y == SelectedCell.Y && b.X == Convert.ToInt32(SetColumn) - 1);
            else
                SelectedCell = EnfilageBoard.First(b => b.Y == SelectedCell.Y && b.X == SelectedCell.X - 1);

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
            {
                SetContent(1);
                
            }


            else if (e.Key == Key.D2 || e.Key == Key.NumPad2)
            {
                SetContent(2);
            }

            else if (e.Key == Key.D3 || e.Key == Key.NumPad3)
            {
                SetContent(3);
            }

            else if (e.Key == Key.D4 || e.Key == Key.NumPad4)
            {
                SetContent(4);
            }

            else if (e.Key == Key.D5 || e.Key == Key.NumPad5)
            {
                SetContent(5);
            }

            else if (e.Key == Key.D6 || e.Key == Key.NumPad6)
            {
                SetContent(6);
            }

            else if (e.Key == Key.D7 || e.Key == Key.NumPad7)
            {
                SetContent(7);
            }
        }

  
        private int _ChaineRo;

        public int ChaineRo
        {
            get
            {
                return _ChaineRo;
            }
            set
            {
                _ChaineRo = value;
                NotifyPropertyChanged();
            }
        }
        public void SetMatrixContent()
        {
            if (
                SelectedCell.TypBox != MatrixElement.BoxType.OutRange
                && SelectedCell.TypBox != MatrixElement.BoxType.Empty
                &&((SelectedCell.TypBox == MatrixElement.BoxType.Dents && IsDent)
                   ||(SelectedCell.TypBox == MatrixElement.BoxType.Lisses && IsDent==false))
                )
            {
                if (SelectedComposant == null)
                {
                    SelectedComposant=  ListComposant[0];
                }

                if (SelectedCell.SupportedComp.ID == SelectedComposant.GetComposant.ID)
                {
                       if (SelectedCell.Content == null)
                {
                    bool IsCorrect = true;
                    if (SelectedCell.TypBox == MatrixElement.BoxType.Dents)
                    {
                        if (SelectedCell.position == 1)
                        {
                            if (SelectedCell.Y!=57 &&  EnfilageBoard
                                    .SingleOrDefault(enf => enf.Y == (SelectedCell.Y + 1) && enf.X == SelectedCell.X)
                                    .IsContent==true)
                            {

                                IsCorrect = false;
                            }
                        }
                        else
                        {
                            if (SelectedCell.Y!=0 && EnfilageBoard
                                    .SingleOrDefault(enf => enf.Y == (SelectedCell.Y - 1) && enf.X == SelectedCell.X)
                                    .IsContent==true)
                            {

                                IsCorrect = false;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= ChaineColumns; i++)
                        {
                            if (i != SelectedCell.position)
                            {
                                int ecart =i- SelectedCell.position;
                                if (EnfilageBoard.SingleOrDefault(enf =>
                                        enf.Y == (SelectedCell.Y + ecart) && enf.X == SelectedCell.X).IsContent)
                                {
                                    IsCorrect = false;
                                }
                            }
                            
                        }
                    }

                    if (IsCorrect)
                    {
                        if (IsDent)
                        {
                            SelectedCell.DentFil = 0;
                            

                            
                             var Case_Pos1Plus= EnfilageBoard.Single(enf =>
                                        enf.X == (SelectedCell.X + 1) && enf.Y == (SelectedCell.Y + 1));
                             
                             var Case_Pos1Minus = EnfilageBoard.Single(enf =>
                                 enf.X == (SelectedCell.X - 1) && enf.Y == (SelectedCell.Y + 1));
                             var Case_Pos2Plus = EnfilageBoard.Single(enf =>
                                 enf.X == (SelectedCell.X + 1) && enf.Y == (SelectedCell.Y - 1));
                             var Case_Pos2Minus = EnfilageBoard.Single(enf =>
                                 enf.X == (SelectedCell.X - 1) && enf.Y == (SelectedCell.Y - 1));
                             bool Content_Pos1Plus=false;
                             bool Content_Pos2Plus=false;
                             bool Content_Pos1Minus=false;
                             bool Content_Pos2Minus=false;
                             int InitLevelPlus = 0;
                             int InitXPlus = 1;
                             int InitLevelMinus = 0;
                             int InitXMinus= 1;
                             if (Case_Pos1Plus == null || Case_Pos1Plus.TypBox == MatrixElement.BoxType.OutRange)
                             {
                                 int PartNbr =Convert.ToInt32(Math.Ceiling(((SelectedCell.Y- WorkinRect.PartsHeightStart) /(double) WorkinRect.PartHeight)));

                                 if (PartNbr < WorkinRect.NbrPart)
                                 {
                                     InitXPlus=WorkinRect.PartsWidthStart- SelectedCell.X ;
                                     InitLevelPlus=InitLevelPlus+WorkinRect.PartHeight;
                                     
                                     Content_Pos1Plus = EnfilageBoard.Single(enf =>
                                         enf.X == (SelectedCell.X + InitXPlus) && enf.Y == (SelectedCell.Y + 1+InitLevelPlus)).IsContent;
                                     Content_Pos2Plus = EnfilageBoard.Single(enf =>
                                         enf.X == (SelectedCell.X + InitXPlus) && enf.Y == (SelectedCell.Y - 1+InitLevelPlus)).IsContent;
                                 }
                                 
                                 
                             }
                             else
                             {
                                  Content_Pos1Plus = EnfilageBoard.Single(enf =>
                                     enf.X == (SelectedCell.X + 1) && enf.Y == (SelectedCell.Y + 1)).IsContent;
                                  Content_Pos2Plus = EnfilageBoard.Single(enf =>
                                     enf.X == (SelectedCell.X + 1) && enf.Y == (SelectedCell.Y - 1)).IsContent;
                             }
                             if (Case_Pos1Minus == null || Case_Pos1Minus.TypBox == MatrixElement.BoxType.OutRange)
                             {
                                 double tempNbr = (SelectedCell.Y - WorkinRect.PartsHeightStart) /(double)WorkinRect.PartHeight;
                                 int PartNbr =Convert.ToInt32(Math.Ceiling(tempNbr));

                                 if (PartNbr > 1)
                                 {
                                     InitXMinus=SelectedCell.X -WorkinRect.PartsWidthEnd;
                                     InitLevelMinus=WorkinRect.PartHeight-InitLevelMinus;
                                     
                                     Content_Pos1Minus = EnfilageBoard.Single(enf =>
                                         enf.X == (SelectedCell.X -InitXMinus) && enf.Y == (SelectedCell.Y -InitLevelMinus+ 1)).IsContent;
                                   
                                     Content_Pos2Minus = EnfilageBoard.Single(enf =>
                                         enf.X == (SelectedCell.X -InitXMinus) && enf.Y == (SelectedCell.Y -InitLevelMinus-1)).IsContent;
                                 }
                             }
                             else
                             {
                                  Content_Pos1Minus = EnfilageBoard.Single(enf =>
                                     enf.X == (SelectedCell.X - 1) && enf.Y == (SelectedCell.Y + 1)).IsContent;
                                   
                                  Content_Pos2Minus = EnfilageBoard.Single(enf =>
                                     enf.X == (SelectedCell.X - 1) && enf.Y == (SelectedCell.Y - 1)).IsContent;
                             }
                                    

                                   

                                    int SearchXPlus=0;
                                    int SearchYPlus=0;
                                    int SearchXMinus=0;
                                    int SearchYMinus=0;
                                    bool PossibleTripleTeeth = false;
                                
                                    bool NotLinked = true;
                                        if ((SelectedCell.position == 1 && Content_Pos1Plus)
                                            || (SelectedCell.position == 2 && Content_Pos2Plus))
                                        {
                                            PossibleTripleTeeth = true;
                                            SearchXPlus = SelectedCell.X + 1;
                                            if (SelectedCell.position == 1)
                                            {
                                                SearchYPlus = SelectedCell.Y + 1;
                                            }
                                            else
                                            {
                                                SearchYPlus = SelectedCell.Y - 1;
                                            }
                                            
                                        
                                        bool AdjacentContent = true;
                                        bool IsInRange = true;
                                         int IncX =InitXPlus+1;
                                         int LevelY = InitLevelPlus;
                                    
                                    while (NotLinked && AdjacentContent && IsInRange)
                                    {
                                       
                                        var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                            enf.X == (SelectedCell.X + IncX) && enf.Y == (SelectedCell.Y +LevelY));
                                        if (LinkCase==null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                        {
                                            int PartNbr =Convert.ToInt32(Math.Ceiling(((SelectedCell.Y+LevelY- WorkinRect.PartsHeightStart) /(double) WorkinRect.PartHeight)));
                                            
                                            if (PartNbr >= WorkinRect.NbrPart)
                                            {
                                                IsInRange = false;
                                                NbrDent=NbrDent+2;
                                            }
                                            else
                                            {
                                                IncX= SelectedCell.X - WorkinRect.PartsWidthStart;
                                                LevelY=LevelY+WorkinRect.PartHeight;
                                            }
                                           
                                        }else
                                        if (LinkCase.IsContent)
                                        {
                                            NotLinked = false;
                                            NbrDent++;
                                        }
                                        else
                                        {
                                            var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == (SelectedCell.X + IncX) && enf.Y == (SearchYPlus+LevelY));
                                            if (CaseAdjacent.IsContent==false)
                                            {
                                                AdjacentContent = false;
                                                NbrDent=NbrDent+2;
                                                
                                            }
                                            else
                                            {
                                                IncX++;
                                            }
                                        }
                                    }
                                        
                                        }
                                        if((SelectedCell.position == 1 && Content_Pos1Minus)
                                           || (SelectedCell.position == 2 && Content_Pos2Minus)) 
                                        {
                                            SearchXMinus = SelectedCell.X + 1;
                                            if (SelectedCell.position == 1)
                                            {
                                                SearchYMinus = SelectedCell.Y + 1;
                                            }
                                            else
                                            {
                                                SearchYMinus = SelectedCell.Y - 1;
                                            }
                                             NotLinked = true;
                                    bool AdjacentContent = true;
                                    bool IsInRange = true;
                                    int SubX =InitXMinus+ 1;
                                    int LevelY = InitLevelMinus;
                                    while (NotLinked && AdjacentContent && IsInRange)
                                    {
                                        var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                            enf.X == (SelectedCell.X - SubX) && enf.Y == (SelectedCell.Y -LevelY));
                                        if (LinkCase==null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                        {
                                            int PartNbr =Convert.ToInt32(Math.Ceiling(((SelectedCell.Y-LevelY- WorkinRect.PartsHeightStart) / (double)WorkinRect.PartHeight)));
                                            if (PartNbr <= 1)
                                            {
                                                IsInRange = false;
                                                if (PossibleTripleTeeth)
                                                {
                                                    NbrDent++;
                                                }
                                                else
                                                {
                                                    NbrDent=NbrDent+2;
                                                }
                                            }
                                            else
                                            {
                                                SubX= SelectedCell.X - WorkinRect.PartsWidthEnd;
                                                LevelY=LevelY+WorkinRect.PartHeight;
                                            }
                                           
                                           
                                        }else
                                        if (LinkCase.IsContent)
                                        {
                                            NotLinked = false;
                                    
                                                NbrDent++; 
                                            
                                        }
                                        else
                                        {
                                            var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == (SelectedCell.X - SubX) && enf.Y == (SearchYMinus-LevelY));
                                            if (CaseAdjacent.IsContent==false)
                                            {
                                                AdjacentContent = false;
                                                if (PossibleTripleTeeth)
                                                {
                                                    NbrDent++;
                                                }
                                                else
                                                {
                                                    NbrDent=NbrDent+2; 
                                                }
                                               
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
                                         int IncX = InitXPlus;

                                          LevelY = InitLevelPlus;
                                    while (NotLinked && AdjacentContent && IsInRange)
                                    {
                                        var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                            enf.X == (SelectedCell.X + IncX) && enf.Y == (SearchYMinus+LevelY));
                                        if (LinkCase==null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                        {
                                            int PartNbr =Convert.ToInt32(Math.Ceiling(((SelectedCell.Y+LevelY- WorkinRect.PartsHeightStart) /(double) WorkinRect.PartHeight)));
                                            if (PartNbr >= WorkinRect.NbrPart)
                                            {
                                                IsInRange = false;
                                            }
                                            else
                                            {
                                                IncX= SelectedCell.X - WorkinRect.PartsWidthStart;
                                                LevelY=LevelY+WorkinRect.PartHeight;
                                            }
                                            
                                        }else
                                        if (LinkCase.IsContent)
                                        {
                                            NotLinked = false;
                                            NbrDent--;
                                        }
                                        else
                                        {
                                            var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == (SelectedCell.X + IncX) && enf.Y == (SelectedCell.Y +LevelY));
                                            if (CaseAdjacent.IsContent==false)
                                            {
                                                AdjacentContent = false;
                                            }
                                            else
                                            {
                                                IncX++;
                                            }
                                        }
                                    }
                                    }
                                       }
                                        else if (NotLinked == false)
                                        {
                                                  NotLinked = true;
                                    bool AdjacentContent = true;
                                    bool IsInRange = true;
                                    int SubX =InitXMinus;
                                    int LevelY = InitLevelMinus;
                                    while (NotLinked && AdjacentContent && IsInRange)
                                    {
                                        var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                            enf.X == (SelectedCell.X - SubX) && enf.Y == ( SearchYPlus -LevelY));
                                        if (LinkCase==null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                        {
                                            int PartNbr =Convert.ToInt32(Math.Ceiling(((SelectedCell.Y-LevelY- WorkinRect.PartsHeightStart) / (double)WorkinRect.PartHeight)));
                                            if (PartNbr <= 1)
                                            {
                                                
                                                IsInRange = false;
                                            }
                                            else
                                            {
                                                SubX= SelectedCell.X - WorkinRect.PartsWidthEnd;
                                                LevelY=LevelY+WorkinRect.PartHeight;
                                            }
                                        }else
                                        if (LinkCase.IsContent)
                                        {
                                            NotLinked = false;
                                           
                                                NbrDent--; 
                                          
                                            
                                        }
                                        else
                                        {
                                            var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == (SelectedCell.X - SubX) && enf.Y == (SelectedCell.Y-LevelY));
                                            if (CaseAdjacent.IsContent==false)
                                            {
                                                AdjacentContent = false;
                                             
                                               
                                            }
                                            else
                                            {
                                                SubX++;
                                            }
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
                            
                                    var Case_Pos1Plus= EnfilageBoard.Single(enf =>
                                        enf.X == (SelectedCell.X + 1) && enf.Y == (SelectedCell.Y + 1));
                             
                             var Case_Pos1Minus = EnfilageBoard.Single(enf =>
                                 enf.X == (SelectedCell.X - 1) && enf.Y == (SelectedCell.Y + 1));
                             var Case_Pos2Plus = EnfilageBoard.Single(enf =>
                                 enf.X == (SelectedCell.X + 1) && enf.Y == (SelectedCell.Y - 1));
                             var Case_Pos2Minus = EnfilageBoard.Single(enf =>
                                 enf.X == (SelectedCell.X - 1) && enf.Y == (SelectedCell.Y - 1));
                             bool Content_Pos1Plus=false;
                             bool Content_Pos2Plus=false;
                             bool Content_Pos1Minus=false;
                             bool Content_Pos2Minus=false;
                             int InitLevelPlus = 0;
                             int InitXPlus = 1;
                             int InitLevelMinus = 0;
                             int InitXMinus= 1;
                             if (Case_Pos1Plus == null || Case_Pos1Plus.TypBox == MatrixElement.BoxType.OutRange)
                             {
                                 int PartNbr =Convert.ToInt32(Math.Ceiling(((SelectedCell.Y- WorkinRect.PartsHeightStart) / (double)WorkinRect.PartHeight)));

                                 if (PartNbr < WorkinRect.NbrPart)
                                 {
                                     InitXPlus=WorkinRect.PartsWidthStart- SelectedCell.X ;
                                     InitLevelPlus=InitLevelPlus+WorkinRect.PartHeight;
                                     
                                     Content_Pos1Plus = EnfilageBoard.Single(enf =>
                                         enf.X == (SelectedCell.X + InitXPlus) && enf.Y == (SelectedCell.Y + 1+InitLevelPlus)).IsContent;
                                     Content_Pos2Plus = EnfilageBoard.Single(enf =>
                                         enf.X == (SelectedCell.X + InitXPlus) && enf.Y == (SelectedCell.Y - 1+InitLevelPlus)).IsContent;
                                 }
                                 
                                 
                             }
                             else
                             {
                                  Content_Pos1Plus = EnfilageBoard.Single(enf =>
                                     enf.X == (SelectedCell.X + 1) && enf.Y == (SelectedCell.Y + 1)).IsContent;
                                  Content_Pos2Plus = EnfilageBoard.Single(enf =>
                                     enf.X == (SelectedCell.X + 1) && enf.Y == (SelectedCell.Y - 1)).IsContent;
                             }
                             if (Case_Pos1Minus == null || Case_Pos1Minus.TypBox == MatrixElement.BoxType.OutRange)
                             {
                                 int PartNbr =Convert.ToInt32(Math.Ceiling((SelectedCell.Y- WorkinRect.PartsHeightStart) / (double)WorkinRect.PartHeight));

                                 if (PartNbr > 1)
                                 {
                                     InitXMinus=SelectedCell.X -WorkinRect.PartsWidthEnd;
                                     InitLevelMinus=WorkinRect.PartHeight-InitLevelMinus;
                                     
                                     Content_Pos1Minus = EnfilageBoard.Single(enf =>
                                         enf.X == (SelectedCell.X -InitXMinus) && enf.Y == (SelectedCell.Y -InitLevelMinus+1)).IsContent;
                                   
                                     Content_Pos2Minus = EnfilageBoard.Single(enf =>
                                         enf.X == (SelectedCell.X -InitXMinus) && enf.Y == (SelectedCell.Y -InitLevelMinus-1)).IsContent;
                                 }
                             }
                             else
                             {
                                  Content_Pos1Minus = EnfilageBoard.Single(enf =>
                                     enf.X == (SelectedCell.X - 1) && enf.Y == (SelectedCell.Y + 1)).IsContent;
                                   
                                  Content_Pos2Minus = EnfilageBoard.Single(enf =>
                                     enf.X == (SelectedCell.X - 1) && enf.Y == (SelectedCell.Y - 1)).IsContent;
                             }
                                    int SearchXPlus=0;
                                    int SearchYPlus=0;
                                    int SearchXMinus=0;
                                    int SearchYMinus=0;
                                    bool PossibleTripleTeeth = false;
                                    bool NotLinked = true;
                                        
                                        if ((SelectedCell.position == 1 && Content_Pos1Plus)
                                            || (SelectedCell.position == 2 && Content_Pos2Plus))
                                        {
                                            SearchXPlus = SelectedCell.X + 1;
                                            if (SelectedCell.position == 1)
                                            {
                                                SearchYPlus = SelectedCell.Y + 1;
                                            }
                                            else
                                            {
                                                SearchYPlus = SelectedCell.Y - 1;
                                            }
                                            
                                         NotLinked = true;
                                        bool AdjacentContent = true;
                                        bool IsInRange = true;
                                         int IncX = InitXPlus+ 1;
                                         PossibleTripleTeeth = true;
                                         int LevelY = InitLevelPlus;
                                    while (NotLinked && AdjacentContent && IsInRange)
                                    {
                                        var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                            enf.X == (SelectedCell.X + IncX) && enf.Y == (SelectedCell.Y +LevelY));
                                        if (LinkCase==null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                        {
                                            int PartNbr =Convert.ToInt32(Math.Ceiling(((SelectedCell.Y+LevelY- WorkinRect.PartsHeightStart) /(double) WorkinRect.PartHeight)));
                                            
                                            if (PartNbr >= WorkinRect.NbrPart)
                                            {
                                                IsInRange = false;
                                                NbrDent=NbrDent-2;
                                            } else
                                            {
                                                IncX= SelectedCell.X - WorkinRect.PartsWidthStart;
                                                LevelY=LevelY+WorkinRect.PartHeight;
                                            }
                                            
                                           
                                        }else
                                        if (LinkCase.IsContent)
                                        {
                                            NotLinked = false;
                                           
                                                NbrDent--;
                                           
                                            
                                        }
                                        else
                                        {
                                            var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == (SelectedCell.X + IncX) && enf.Y == (SearchYPlus+LevelY));
                                            if (CaseAdjacent.IsContent==false)
                                            {
                                                AdjacentContent = false;
                                                NbrDent=NbrDent-2;
                                            }
                                            else
                                            {
                                                IncX++;
                                            }
                                        }
                                    }
                                        
                                        }
                                        
                                        if((SelectedCell.position == 1 && Content_Pos1Minus)
                                           || (SelectedCell.position == 2 && Content_Pos2Minus)) 
                                        {
                                            SearchXMinus = SelectedCell.X + 1;
                                            if (SelectedCell.position == 1)
                                            {
                                                SearchYMinus = SelectedCell.Y + 1;
                                            }
                                            else
                                            {
                                                SearchYMinus = SelectedCell.Y - 1;
                                            }
                                           
                                    bool AdjacentContent = true;
                                    bool IsInRange = true;
                                    NotLinked = true;
                                    int SubX =InitXMinus+ 1;
                                    int LevelY = InitLevelMinus;
                                    while (NotLinked && AdjacentContent && IsInRange)
                                    {
                                        var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                            enf.X == (SelectedCell.X - SubX) && enf.Y == (SelectedCell.Y -LevelY));
                                        if (LinkCase==null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                        {
                                            int PartNbr =Convert.ToInt32(Math.Ceiling(((SelectedCell.Y+LevelY- WorkinRect.PartsHeightStart) /(double) WorkinRect.PartHeight)));

                                            if (PartNbr <= 1)
                                            {
                                                IsInRange = false;
                                                if (PossibleTripleTeeth)
                                                {
                                                    NbrDent--;
                                                }
                                                else
                                                {
                                                    NbrDent=NbrDent-2;
                                                }
                                            }
                                            else
                                            {
                                                SubX= SelectedCell.X - WorkinRect.PartsWidthStart;
                                                LevelY=LevelY+WorkinRect.PartHeight;
                                            }
                                            
                                           
                                        }else
                                        if (LinkCase.IsContent)
                                        {
                                            NotLinked = false;
                                            
                                                NbrDent--;
                                            
                                        }
                                        else
                                        {
                                            var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == (SelectedCell.X - SubX) && enf.Y == (SearchYMinus-LevelY));
                                            if (CaseAdjacent.IsContent==false)
                                            {
                                                AdjacentContent = false;
                                                if (PossibleTripleTeeth)
                                                {
                                                    NbrDent--;
                                                }
                                                else
                                                {
                                                    NbrDent=NbrDent-2; 
                                                }
                                               
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
                                         int IncX = InitXPlus;

                                         LevelY = InitLevelPlus;
                                    while (NotLinked && AdjacentContent && IsInRange)
                                    {
                                        var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                            enf.X == (SelectedCell.X + IncX) && enf.Y == (SearchYMinus+LevelY));
                                        if (LinkCase==null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                        {
                                            int PartNbr =Convert.ToInt32(Math.Ceiling(((SelectedCell.Y+LevelY- WorkinRect.PartsHeightStart) / (double)WorkinRect.PartHeight)));

                                            if (PartNbr >= WorkinRect.NbrPart)
                                            {
                                                IsInRange = false;
                                            }
                                            else
                                            {
                                                IncX= SelectedCell.X - WorkinRect.PartsWidthStart;
                                                LevelY=LevelY+WorkinRect.PartHeight;
                                            }
                                           
                                        }else
                                        if (LinkCase.IsContent)
                                        {
                                            NotLinked = false;
                                            NbrDent++;
                                        }
                                        else
                                        {
                                            var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == (SelectedCell.X + IncX) && enf.Y == (SelectedCell.Y+LevelY ));
                                            if (CaseAdjacent.IsContent==false)
                                            {
                                                AdjacentContent = false;
                                            }
                                            else
                                            {
                                                IncX++;
                                            }
                                        }
                                    }
                                    }
                                       }
                                        else if (NotLinked == false)
                                        {
                                                  NotLinked = true;
                                    bool AdjacentContent = true;
                                    bool IsInRange = true;
                                    int SubX = InitXMinus;
                                    int LevelY = InitLevelMinus;
                                    while (NotLinked && AdjacentContent && IsInRange)
                                    {
                                        var LinkCase = EnfilageBoard.SingleOrDefault(enf =>
                                            enf.X == (SelectedCell.X - SubX) && enf.Y == ( SearchYPlus ));
                                        if (LinkCase==null || LinkCase.TypBox == MatrixElement.BoxType.OutRange)
                                        {
                                            int PartNbr =Convert.ToInt32(Math.Ceiling(((SelectedCell.Y+LevelY- WorkinRect.PartsHeightStart) /(double) WorkinRect.PartHeight)));

                                            if (PartNbr <= 1)
                                            {
                                                IsInRange = false;
                                            }
                                            else
                                            {
                                                SubX= SelectedCell.X - WorkinRect.PartsWidthStart;
                                                LevelY=LevelY+WorkinRect.PartHeight;
                                            }
                                            
                                          
                                        }else
                                        if (LinkCase.IsContent)
                                        {
                                            NotLinked = false;
                                           
                                                NbrDent++; 
                                          
                                            
                                        }
                                        else
                                        {
                                            var CaseAdjacent = EnfilageBoard.SingleOrDefault(enf =>
                                                enf.X == (SelectedCell.X - SubX) && enf.Y == (SelectedCell.Y));
                                            if (CaseAdjacent.IsContent==false)
                                            {
                                                AdjacentContent = false;
                                             
                                               
                                            }
                                            else
                                            {
                                                SubX++;
                                            }
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
        public void SetContent(int Num)
        {
            if (ListComposant.Count >= Num )
            {
                SelectedComposant = ListComposant[Num - 1];
                SetMatrixContent();
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

        public MatrixElement SelectedCell { get; set; }

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

        public Composition SelectedComposant { get; set; }

        public void ReedCalculation()
        {
            if (DentList != null && DentList.Count == 0)
            {
                LinkedList<MatrixElement> dent = new LinkedList<MatrixElement>();
                dent.AddLast(SelectedCell);
                DentList.AddLast(dent);
                NbrDent++;
                Rang MRang = new Rang();
                MRang.Y1 = SelectedCell.Y;
                Rangs.AddLast(MRang);
            }
            else
            {
                if (Rangs.Count == 1 && Rangs.First().Y2 == -1)
                {
                    if (SelectedCell.Y == Rangs.First().Y1)
                    {
                        LinkedListNode<LinkedList<MatrixElement>> node = RogueDentList.First;
                        LinkedListNode<LinkedList<MatrixElement>> FoundNode = null;
                        bool ContinueLoop = true;
                        bool LastCheck = false;
                        while (node != null && ContinueLoop == true)
                        {
                            if (LastCheck == false)
                            {
                                if (node.Value.First().X == (SelectedCell.X + 1))
                                {
                                    node.Value.AddFirst(SelectedCell);
                                    ContinueLoop = false;
                                    FoundNode = node;
                                }
                                else if (node.Value.Last().X == (SelectedCell.X - 1))
                                {
                                    node.Value.AddLast(SelectedCell);
                                    LastCheck = true;
                                    FoundNode = node;
                                }

                                node = node.Next;
                            }
                            else
                            {
                                if (node.Value.First().X == (SelectedCell.X + 1))
                                {
                                    LinkedList<MatrixElement> tempList = node.Value;
                                    LinkedListNode<LinkedList<MatrixElement>> PrevNode = node.Previous;
                                    foreach (var el in tempList)
                                    {
                                        PrevNode.Value.AddLast(el);
                                        RogueDentList.Remove(node.Value);
                                    }

                                    FoundNode = PrevNode;
                                }

                                ContinueLoop = false;
                            }
                        }

                        if ((DentList.Last().First().X - 1) == SelectedCell.X)
                        {
                            if (FoundNode == null)
                            {
                                DentList.Last().AddFirst(SelectedCell);
                            }
                            else
                            {
                                foreach (var el in FoundNode.Value.Reverse())
                                {
                                    DentList.Last().AddFirst(el);
                                }
                            }
                        }
                        else if ((DentList.Last().Last().X + 1) == SelectedCell.X)
                        {
                            if (FoundNode == null)
                            {
                                DentList.Last().AddLast(SelectedCell);
                            }
                            else
                            {
                                foreach (var el in FoundNode.Value)
                                {
                                    DentList.Last().AddLast(el);
                                }
                            }
                        }
                    }
                    else if (SelectedCell.Y == (Rangs.First().Y1 - 1) || SelectedCell.Y == (Rangs.First().Y1 + 1))
                    {
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
        }

        
        private void ImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedCell != null)
                SelectedCell.IsSelected = false;
            var img = (Image)sender;
            SelectedCell = (MatrixElement)img.Tag;

            SetMatrixContent();
       
        }

        public ChaineMatrixElement SelectedChaineCell { get; set; }

        private void ImageMouseDown1(object sender, MouseButtonEventArgs e)
        {
            if (SelectedChaineCell != null)
                SelectedChaineCell.IsSelected = false;
            var border = (Image)sender;
            SelectedChaineCell = (ChaineMatrixElement)border.Tag;
            SelectedChaineCell.IsContent=true;
        }

        private void MouseDragElementBehavior_OnDragFinished(object sender, MouseEventArgs e)
        {
            MouseDragElementBehavior df = (MouseDragElementBehavior)sender;
            LastXposition = df.X.ToString();
            LastYposition = df.Y.ToString();
            string testx = TrameXposition;
            string testy = TrameYposition;
            NotifyPropertyChanged();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            IsDent = true;
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            IsDent = false;
        }
    }
}