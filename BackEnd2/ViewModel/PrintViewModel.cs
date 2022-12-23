using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BackEnd2.Model;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class PrintViewModel:MvxViewModel<Produit>
    {

        public IMvxNavigationService _navigationService ;
        private Produit _NewProd;

        public Produit NewProd
        {
            get
            {
                return _NewProd;
            }
            set
            {
                _NewProd = value;
                RaisePropertyChanged();
            }
            
        }
        private  MvxObservableCollection<Composition> _SchCompList;

        public MvxObservableCollection<Composition> SchCompList
        {
            get => _SchCompList;
            set
            {
                _SchCompList = value;
                RaisePropertyChanged();
            }
        }
        public PrintViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
            SchCompList = new MvxObservableCollection<Composition>();
            ContentEnfilageList = new MvxObservableCollection<MatrixElement>();
            ChaineBoard = new ChaineBoardStructure(ChaineRows,ChaineColumns);
            EnfilageBoard = new BoardStructure(EnfilageRow, EnfilageColumns);
            EnfilageList = new ObservableCollection<MatrixElement>(EnfilageBoard.Board);
            ChaineList = new ObservableCollection<ChaineMatrixElement>(ChaineBoard.Board);
        }

        public void DisplayEnfilage()
        {
            foreach (var Chi in NewProd.EnfilageID.GetChaine.ChMatrix)
            {
                ChaineList.First(ch => ch.X == Chi.x && ch.Y == Chi.y).IsContent = false;
                ChaineList.First(ch=> ch.X==Chi.x && ch.Y==Chi.y).SetContent();
            }
            List<MatrixElement> TempList = new List<MatrixElement>();
            
            foreach (var EleMatx in NewProd.EnfilageID.GetMatrix)
            {
                MatrixElement EnfMatrix = new MatrixElement(EleMatx.x,EleMatx.y);
                
                EnfMatrix.Content = EleMatx.value;
                TempList.Add(EnfMatrix);
            }
            ContentEnfilageList = new MvxObservableCollection<MatrixElement>(TempList);
            SetInitContents();
        }
        public void SetInitContents()
        {
            
            foreach (var EnfCont in ContentEnfilageList)
            {
                var EleMatx= EnfilageList.FirstOrDefault(bo => bo.X == EnfCont.X && bo.Y == EnfCont.Y);
                EleMatx.Content = EnfCont.Content;
                EleMatx.BorderCO = EnfCont.Content.BKBorderComposant;
             
            }
        }
        private int _EnfilageRow = 59;
        private int _EnfilageColumns = 83;
        private int _ChaineRows=8;
        private int _ChaineColumns=4;
        public int ChaineColumns
        {
            get => _ChaineColumns;
            set => _ChaineColumns = value;
        }
        public int EnfilageColumns
        {
            get => _EnfilageColumns;
            set => _EnfilageColumns = value;
        }

        public int EnfilageRow
        {
            get => _EnfilageRow;
            set => _EnfilageRow = value;
        }
        public int ChaineRows
        {
            get => _ChaineRows;
            set => _ChaineRows = value;
        }
        private ObservableCollection<ChaineMatrixElement> _ChaineList;
        public ObservableCollection<ChaineMatrixElement> ChaineList
        {
            get => _ChaineList;
            set
            {
                _ChaineList = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<MatrixElement> _EnfilageList;
        public ObservableCollection<MatrixElement> EnfilageList
        {
            get => _EnfilageList;
            set
            {
                _EnfilageList = value;
                RaisePropertyChanged();
            }
        }
        
        private BoardStructure _EnfilageBoard;
        public BoardStructure EnfilageBoard
        {
            get => _EnfilageBoard;
            set => _EnfilageBoard = value;
        }
        private ChaineBoardStructure _ChaineBoard;
        public ChaineBoardStructure ChaineBoard
        {
            get => _ChaineBoard;
            set => _ChaineBoard = value;
        }
        private MvxObservableCollection<MatrixElement> _ContentEnfilageList;
        public MvxObservableCollection<MatrixElement> ContentEnfilageList
        {
            get => _ContentEnfilageList;
            set
            {
                _ContentEnfilageList = value;
                RaisePropertyChanged();
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
        private double _TotalPoids;

        public double TotalPoids
        {
            get => _TotalPoids;
            set
            {
                _TotalPoids = value;
                RaisePropertyChanged();
            }
        }

        public override void Prepare(Produit parameter)
        {
            NewProd = parameter;
            if(NewProd!=null && NewProd.GetComposition !=null)
              { 
                  for(int i=0;i<NewProd.GetComposition.Count;i++)
                  {
                      if (NewProd.GetComposition[i].GetComposant.Name.ToLower().Equals("trame"))
                      {
                          Comp8 = NewProd.GetComposition[i];
                      }
                      else if (NewProd.GetComposition[i].GetComposant.Name.ToLower().Equals("apport"))
                      {
                          Comp9 = NewProd.GetComposition[i];
                      }
                      else
                      {
                          if (i == 0)
                          {
                              Comp1 = NewProd.GetComposition[i];
                              SchCompList.Add(Comp1);
                          }

                          if (i == 1)
                          {
                              Comp2 = NewProd.GetComposition[i];
                              SchCompList.Add(Comp2);
                          }

                          if (i == 2)
                          {
                              Comp3 = NewProd.GetComposition[i];
                              SchCompList.Add(Comp3);
                          }

                          if (i == 3)
                          {
                              Comp4 = NewProd.GetComposition[i];
                              SchCompList.Add(Comp4);
                          }

                          if (i == 4)
                          {
                              Comp5 = NewProd.GetComposition[i];
                              SchCompList.Add(Comp5);
                          }

                          if (i == 5)
                          {
                              Comp6 = NewProd.GetComposition[i];
                              SchCompList.Add(Comp6);
                          }

                          if (i == 6)
                          {
                              Comp7 = NewProd.GetComposition[i];
                              SchCompList.Add(Comp7);
                          }
                      }
               
                  }

                  CalculateTotalWeight();
                  DisplayEnfilage();
                }

           

        }
        
        private Composition _Comp1;
        
        public Composition Comp1
        {
            get => _Comp1;
            set
            {
                _Comp1 = value;
                RaisePropertyChanged();
            }
        }
        
        private Composition _Comp2;
        
        public Composition Comp2
        {
            get => _Comp2;
            set
            {
                _Comp2 = value;
                RaisePropertyChanged();
            }
        }
        
        private Composition _Comp3;
        
        public Composition Comp3
        {
            get => _Comp3;
            set
            {
                _Comp3 = value;
                RaisePropertyChanged();
            }
        }
        private Composition _Comp4;
        
        public Composition Comp4
        {
            get => _Comp4;
            set
            {
                _Comp4 = value;
                RaisePropertyChanged();
            }
        }
        
        private Composition _Comp5;
        
        public Composition Comp5
        {
            get => _Comp5;
            set
            {
                _Comp5 = value;
                RaisePropertyChanged();
            }
        }
        private Composition _Comp6;
        
        public Composition Comp6
        {
            get => _Comp6;
            set
            {
                _Comp6 = value;
                RaisePropertyChanged();
            }
        }
        private Composition _Comp7;
        
        public Composition Comp7
        {
            get => _Comp7;
            set
            {
                _Comp7 = value;
                RaisePropertyChanged();
            }
        }
        private Composition _Comp8;
        
        public Composition Comp8
        {
            get => _Comp8;
            set
            {
                _Comp8 = value;
                RaisePropertyChanged();
            }
        }
        private Composition _Comp9;
        
        public Composition Comp9
        {
            get => _Comp9;
            set
            {
                _Comp9 = value;
                RaisePropertyChanged();
            }
        }
    }
}