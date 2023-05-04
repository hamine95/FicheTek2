using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using BackEnd2.Model;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class PrintViewModel : MvxViewModel<Produit>
    {
        private double PointsToPixels(double wpfPoints, LengthDirection direction)
        {
            if (direction == LengthDirection.Horizontal)
            {
                return wpfPoints * System.Windows.SystemParameters.WorkArea.Width / SystemParameters.WorkArea.Width;
            }
            else
            {
                return wpfPoints * System.Windows.SystemParameters.WorkArea.Height / SystemParameters.WorkArea.Height;
            }
        }

        private double PixelsToPoints(int pixels, LengthDirection direction)
        {
            if (direction == LengthDirection.Horizontal)
            {
                return pixels * SystemParameters.WorkArea.Width / System.Windows.SystemParameters.WorkArea.Width;
            }
            else
            {
                return pixels * SystemParameters.WorkArea.Height / SystemParameters.WorkArea.Height;
            }
        }

        public enum LengthDirection
        {
            Vertical, // |
            Horizontal // ——
        }
        private bool _IsFicheNormal=true;

        public bool IsFicheNormal
        {
            get { return _IsFicheNormal; }
            set { _IsFicheNormal = value; RaisePropertyChanged(); }
        }
        private bool _IsFicheEHC;

        public bool IsFicheEHC
        {
            get { return _IsFicheEHC; }
            set { _IsFicheEHC = value;
                RaisePropertyChanged(); }
        }


        private bool _IsFicheCrochetage;

        public bool IsFicheCrochetage
        {
            get { return _IsFicheCrochetage; }
            set { _IsFicheCrochetage = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<ChaineMatrixElement> _ChaineList;
        private ObservableCollection<ChaineMatrixElement> _ChaineList2;

        public ObservableCollection<ChaineMatrixElement> ChaineList2
        {
            get { return _ChaineList2; }
            set { _ChaineList2 = value; RaisePropertyChanged(); }
        }

        private Composition _Comp1;

        private Composition _Comp2;

        private Composition _Comp3;
        private Composition _Comp4;

        private Composition _Comp5;
        private Composition _Comp6;
        private Composition _Comp7;
        private Composition _Comp8;
        private Composition _Comp9;
        private string _TrameXposition = "21.78";
        private string _TrameYposition = "0";

        private bool _Btn9Vis;

        public bool Btn9Vis
        {
            get { return _Btn9Vis; }
            set { _Btn9Vis = value;RaisePropertyChanged(); }
        }

        private bool _SecChainVis;

        public bool SecChainVis
        {
            get { return _SecChainVis; }
            set { _SecChainVis = value; RaisePropertyChanged(); }
        }

        private bool _Btn10Vis;

        public bool Btn10Vis
        {
            get { return _Btn10Vis; }
            set { _Btn10Vis = value; RaisePropertyChanged(); }
        }
        private bool _Btn11Vis;

        public bool Btn11Vis
        {
            get { return _Btn11Vis; }
            set { _Btn11Vis = value; RaisePropertyChanged(); }
        }
        private bool _Btn12Vis;

        public bool Btn12Vis
        {
            get { return _Btn12Vis; }
            set { _Btn12Vis = value; RaisePropertyChanged(); }
        }
        public string TrameXposition
        {
            get => _TrameXposition;
            set
            {
                _TrameXposition = value;
                RaisePropertyChanged();
            }
        }
        public string TrameYposition
        {
            get => _TrameYposition;
            set
            {
                _TrameYposition = value;
                RaisePropertyChanged();
            }
        }
        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();

        private MvxObservableCollection<MatrixElement> _ContentEnfilageList;

        private ObservableCollection<MatrixElement> _EnfilageList;

        public IMvxNavigationService _navigationService;
        private Produit _NewProd;
        private MvxObservableCollection<Composition> _SchCompList;
        private double _TotalPoids;

        public PrintViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
            
        }

        private int _ChRowSum;

        public int ChRowSum
        {
            get
            {
                return _ChRowSum;
            }
            set
            {
                _ChRowSum = value;
                RaisePropertyChanged();
            }
        }
        
        public Produit NewProd
        {
            get => _NewProd;
            set
            {
                _NewProd = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Composition> SchCompList
        {
            get => _SchCompList;
            set
            {
                _SchCompList = value;
                RaisePropertyChanged();
            }
        }
        private int _ChainColNum=8;

        public int ChainColNum
        {
            get { return _ChainColNum; }
            set { _ChainColNum = value; }
        }

        public int ChaineColumns { get; set; } = 4;

        public int EnfilageColumns { get; set; } = 83;

        public int EnfilageRow { get; set; } = 59;

        public int ChaineRows { get; set; } = 8;

        private int _ChaineRows2;

        public int ChaineRows2
        {
            get { return _ChaineRows2; }
            set { _ChaineRows2 = value; RaisePropertyChanged(); }
        }


        public ObservableCollection<ChaineMatrixElement> ChaineList
        {
            get => _ChaineList;
            set
            {
                _ChaineList = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<MatrixElement> EnfilageList
        {
            get => _EnfilageList;
            set
            {
                _EnfilageList = value;
                RaisePropertyChanged();
            }
        }

        public BoardStructure EnfilageBoard { get; set; }

        public ChaineBoardStructure ChaineBoard { get; set; }
        public ChaineBoardStructure ChaineBoard2 { get; set; }

        public MvxObservableCollection<MatrixElement> ContentEnfilageList
        {
            get => _ContentEnfilageList;
            set
            {
                _ContentEnfilageList = value;
                RaisePropertyChanged();
            }
        }

        public double TotalPoids
        {
            get => _TotalPoids;
            set
            {
                _TotalPoids = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp1
        {
            get => _Comp1;
            set
            {
                _Comp1 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp2
        {
            get => _Comp2;
            set
            {
                _Comp2 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp3
        {
            get => _Comp3;
            set
            {
                _Comp3 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp4
        {
            get => _Comp4;
            set
            {
                _Comp4 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp5
        {
            get => _Comp5;
            set
            {
                _Comp5 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp6
        {
            get => _Comp6;
            set
            {
                _Comp6 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp7
        {
            get => _Comp7;
            set
            {
                _Comp7 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp8
        {
            get => _Comp8;
            set
            {
                _Comp8 = value;
                RaisePropertyChanged();
            }
        }

        public Composition Comp9
        {
            get => _Comp9;
            set
            {
                _Comp9 = value;
                RaisePropertyChanged();
            }
        }

        private int _AngleChain;

        public int AngleChain
        {
            get { return _AngleChain; }
            set { _AngleChain = value; RaisePropertyChanged(); }
        }

        public void RotateChain(int ChRowSum)
        {
            if (NewProd.EnfilageID.GetChaine.Ligne > 78)
            {
                SecChainVis = true;
                ChaineBoard = new ChaineBoardStructure(78, NewProd.EnfilageID.GetChaine.Colonne);
                ChaineList = new ObservableCollection<ChaineMatrixElement>(ChaineBoard.Board);
                ChaineBoard2 = new ChaineBoardStructure(NewProd.EnfilageID.GetChaine.Ligne - 78, NewProd.EnfilageID.GetChaine.Colonne);
                ChaineList2 = new ObservableCollection<ChaineMatrixElement>(ChaineBoard2.Board);
                ChaineRows2 = NewProd.EnfilageID.GetChaine.Ligne - 78;
                ChaineRows = 78;
            }
            else
            {

                ChaineBoard = new ChaineBoardStructure(NewProd.EnfilageID.GetChaine.Ligne, NewProd.EnfilageID.GetChaine.Colonne);
                ChaineList = new ObservableCollection<ChaineMatrixElement>(ChaineBoard.Board);

                ChaineRows = NewProd.EnfilageID.GetChaine.Ligne;
            }
            if (ChaineRows > 26)
            {
               

                AngleChain = 270;

                if (ChaineRows >= 78)
                {
                    ChaineRows = 78;
                    ChaineRows2 = ChRowSum - 78;
                }
                else
                {
                    ChaineRows = ChRowSum;
                 
                }
            }
            else
            {
              
               

                ChaineRows = ChRowSum;
                
            }
           
        }
        public void SetupChaine()
        {
            if (NewProd.EnfilageID.GetChaine != null)
            {
                if (NewProd.EnfilageID.GetChaine.Colonne < 9)
                {
                    Btn9Vis = false;
                    Btn10Vis = false;
                    Btn11Vis = false;
                    Btn12Vis = false;
                }else if (NewProd.EnfilageID.GetChaine.Colonne == 9)
                {
                    Btn9Vis = true;
                    Btn10Vis = false;
                    Btn11Vis = false;
                    Btn12Vis = false;
                    ChainColNum = 9;
                }
                else if(NewProd.EnfilageID.GetChaine.Colonne==10)
                {
                    Btn9Vis = true;
                    Btn10Vis = true;
                    Btn11Vis = false;
                    Btn12Vis = false;
                    ChainColNum = 10;
                }else if(NewProd.EnfilageID.GetChaine.Colonne==11)
                {
                    Btn9Vis = true;
                    Btn10Vis = true;
                    Btn11Vis = true;
                    Btn12Vis = false;
                    ChainColNum = 11;
                }
                else if(NewProd.EnfilageID.GetChaine.Colonne==12)
                {
                    Btn9Vis = true;
                    Btn10Vis = true;
                    Btn11Vis = true;
                    Btn12Vis = true;
                    ChainColNum = 12;
                }

               

                ChRowSum = NewProd.EnfilageID.GetChaine.Ligne;
               
                RotateChain(NewProd.EnfilageID.GetChaine.Ligne);

                foreach (var ch in ChaineList)
                {
                    var exist = NewProd.EnfilageID.GetChaine.ChMatrix.SingleOrDefault(chi => chi.x == ch.X && chi.y == ch.Y);
                    if (exist != null)
                    {
                        ch.CellState = ChaineMatrixElement.ComponentState.Occupied;
                    }
                }
                if (NewProd.EnfilageID.GetChaine.Ligne > 78)
                {
                    foreach (var ch in ChaineList2)
                    {
                        var exist = NewProd.EnfilageID.GetChaine.ChMatrix.SingleOrDefault(chi => chi.x == ch.X && chi.y == ch.Y + 78);
                        if (exist != null)
                        {
                            if (ch.IsContent == false) ch.IsContent = true;
                        }
                        else
                        {
                            ch.IsContent = false;
                        }
                    }
                }
                
            }
        }
        public void DisplayEnfilage()
        {
            try
            {
               
                if (NewProd.EnfilageID != null)
                {

                    SetupChaine();



                    var TempList = new List<MatrixElement>();

                    foreach (var EleMatx in NewProd.EnfilageID.GetMatrix)
                    {
                        var EnfMatrix = new MatrixElement(EleMatx.x, EleMatx.y);

                        EnfMatrix.Content = EleMatx.value;
                        TempList.Add(EnfMatrix);
                    }


                    ContentEnfilageList = new MvxObservableCollection<MatrixElement>(TempList);
                    SetInitContents();
                    TrameXposition = ((Convert.ToDouble(NewProd.EnfilageID.TrXposition)) * double.Parse("1052.44", CultureInfo.InvariantCulture) + double.Parse("21.78", CultureInfo.InvariantCulture))
                                .ToString();
                    TrameYposition = (Convert.ToDouble(NewProd.EnfilageID.TrYposition)* double.Parse("748.34", CultureInfo.InvariantCulture)).ToString();
                   
                }
            }
            catch(Exception ex)
            {
                SendNotification.Raise(ex.ToString());
            }
           
         
        }

        private double _MainWinHeight;

        public double MainWinHeight
        {
            get
            {
                return _MainWinHeight;
            }
            set
            {
                _MainWinHeight = value;
                RaisePropertyChanged();
            }
        }
        private double _MainWinWidth;

        public double MainWinWidth
        {
            get
            {
                return _MainWinWidth;
            }
            set
            {
                _MainWinWidth = value;
                RaisePropertyChanged();
            }
        }
        
        private double _ContainerWidth;

        public double ContainerWidth
        {
            get
            {
                return _ContainerWidth;
            }
            set
            {
                _ContainerWidth = value;
                RaisePropertyChanged();
            }
        }
        private double _ContainerHeight;

        public double ContainerHeight
        {
            get
            {
                return _ContainerHeight;
            }
            set
            {
                _ContainerHeight = value;
                RaisePropertyChanged();
            }
        }

        public void SetInitContents()
        {
            foreach (var EnfCont in ContentEnfilageList)
            {
                var EleMatx = EnfilageList.FirstOrDefault(bo => bo.X == EnfCont.X && bo.Y == EnfCont.Y);
                EleMatx.Content = EnfCont.Content;
                //EleMatx.BorderCO = EnfCont.Content.BKBorderComposant;
            }
        }

        public void CalculateTotalWeight()
        {
            TotalPoids = 0;
            if (Comp1 != null) TotalPoids = TotalPoids + Comp1.Poids;
            if (Comp2 != null) TotalPoids = TotalPoids + Comp2.Poids;
            if (Comp3 != null) TotalPoids = TotalPoids + Comp3.Poids;
            if (Comp4 != null) TotalPoids = TotalPoids + Comp4.Poids;
            if (Comp5 != null) TotalPoids = TotalPoids + Comp5.Poids;
            if (Comp6 != null) TotalPoids = TotalPoids + Comp6.Poids;
            if (Comp7 != null) TotalPoids = TotalPoids + Comp7.Poids;
            if (Comp8 != null) TotalPoids = TotalPoids + Comp8.Poids;
            if (Comp9 != null) TotalPoids = TotalPoids + Comp9.Poids;
        }

        public override void Prepare(Produit parameter)
        {
            try
            {
                NewProd = parameter;
                if(NewProd.modelFiche==ModelFiche.ModelFicheTek.FicheTekEHC)
                {
                    IsFicheEHC = true;
                    IsFicheNormal = false;
                    IsFicheCrochetage = false;
                }else if(NewProd.modelFiche == ModelFiche.ModelFicheTek.FicheTekCrochetage)
                {

                }
                if (NewProd != null && NewProd.GetComposition != null)
                {
                    SchCompList = new MvxObservableCollection<Composition>();
                    ContentEnfilageList = new MvxObservableCollection<MatrixElement>();
                    if(NewProd.IsEnfilage==1 && (NewProd.EnfilageID==null 
                       || NewProd.EnfilageID.GetChaine==null))
                        return;
                    if (NewProd.IsEnfilage == 1)
                    {
                        ChaineBoard = new ChaineBoardStructure(NewProd.EnfilageID.GetChaine.Ligne, NewProd.EnfilageID.GetChaine.Colonne);
                        EnfilageBoard = new BoardStructure(EnfilageRow, EnfilageColumns);
                        EnfilageList = new ObservableCollection<MatrixElement>(EnfilageBoard.Board);
                        ChaineColumns = NewProd.EnfilageID.GetChaine.Colonne;
                        ChaineList = new ObservableCollection<ChaineMatrixElement>(ChaineBoard.Board);
                    }

                        for (var i = 0; i < NewProd.GetComposition.Count; i++)
                    {
                        if(NewProd.GetComposition[i].GetComposant != null)
                        {
                            if (NewProd.GetComposition[i].NumComposant == 8)
                            {
                                Comp8 = NewProd.GetComposition[i];
                            }
                            else if (NewProd.GetComposition[i].NumComposant == 9)
                            {
                                Comp9 = NewProd.GetComposition[i];
                            }
                            else
                            {
                                if (NewProd.GetComposition[i].NumComposant==1)
                                {
                                    Comp1 = NewProd.GetComposition[i];
                                    SchCompList.Add(Comp1);
                                }

                                if (NewProd.GetComposition[i].NumComposant == 2)
                                {
                                    Comp2 = NewProd.GetComposition[i];
                                    SchCompList.Add(Comp2);
                                }

                                if (NewProd.GetComposition[i].NumComposant ==3)
                                {
                                    Comp3 = NewProd.GetComposition[i];
                                    SchCompList.Add(Comp3);
                                }

                                if (NewProd.GetComposition[i].NumComposant == 4)
                                {
                                    Comp4 = NewProd.GetComposition[i];
                                    SchCompList.Add(Comp4);
                                }

                                if (NewProd.GetComposition[i].NumComposant == 5)
                                {
                                    Comp5 = NewProd.GetComposition[i];
                                    SchCompList.Add(Comp5);
                                }

                                if (NewProd.GetComposition[i].NumComposant == 6)
                                {
                                    Comp6 = NewProd.GetComposition[i];
                                    SchCompList.Add(Comp6);
                                }

                                if (NewProd.GetComposition[i].NumComposant == 7)
                                {
                                    Comp7 = NewProd.GetComposition[i];
                                    SchCompList.Add(Comp7);
                                }
                            }
                        }
                    }
                    
                    CalculateTotalWeight();

                    if (NewProd.IsEnfilage == 1)
                    {
                     
                        DisplayEnfilage();
                    }
                }
                
            }
            catch(Exception ex)
            {
                SendNotification.Raise(ex.ToString());
            }
            
        }
    }
}