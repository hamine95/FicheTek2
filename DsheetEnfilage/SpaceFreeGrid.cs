using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using BackEnd2.CustomClass;
using BackEnd2.Model;
using DSheetEnfilage.Properties;


namespace DSheetEnfilage
{
    public class SpaceFreeGrid:Panel
    {


        
        
        public enum ControlDirection
        {
            Horizontal,
            Vertical,
        }
      

        public static readonly DependencyProperty EnfilageListProperty = DependencyProperty.Register(
            nameof(EnfilageList), typeof(ObservableCollection<MatrixElement>), typeof(SpaceFreeGrid), new PropertyMetadata(default(ObservableCollection<MatrixElement>),OnSetEnfilageList));

        
        public ObservableCollection<MatrixElement> EnfilageList
        {
            get { return (ObservableCollection<MatrixElement>)GetValue(EnfilageListProperty); }
            set { SetValue(EnfilageListProperty, value); }
        }
        public static void OnSetEnfilageList(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var mycontrol = (SpaceFreeGrid)obj;
            mycontrol.SetEnfilageList((ObservableCollection<MatrixElement>)args.OldValue);
        }

        public static readonly DependencyProperty WorkspaceSpecProperty = DependencyProperty.Register(
            nameof(WorkspaceSpec), typeof(WorkRectangle), typeof(SpaceFreeGrid), new PropertyMetadata(default(WorkRectangle),OnsetWorkspaceSpec));

        public WorkRectangle WorkspaceSpec
        {
            get { return (WorkRectangle)GetValue(WorkspaceSpecProperty); }
            set { SetValue(WorkspaceSpecProperty, value); }
        }

        public static readonly DependencyProperty SecondRectProperty = DependencyProperty.Register(
            nameof(SecondRect), typeof(SecRectangle), typeof(SpaceFreeGrid), new PropertyMetadata(default(SecRectangle)));

        public SecRectangle SecondRect
        {
            get { return (SecRectangle)GetValue(SecondRectProperty); }
            set { SetValue(SecondRectProperty, value); }
        }

        public static void OnsetWorkspaceSpec(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var mycontrol = (SpaceFreeGrid)obj;
            if (args.OldValue != null)
            {
                if (args.NewValue != null)
                {
                    mycontrol.ClearPreviousWorkspace((WorkRectangle)args.OldValue,true);
                }
                else
                {
                    mycontrol.ClearPreviousWorkspace((WorkRectangle)args.OldValue,false);
                }
            }
              
            mycontrol.SetupWorkspace();
        }

        public int GetCurrentChunk(WorkRectangle WorkSpace,int y)
        {
            return (y- WorkSpace.PartsHeightStart) / WorkSpace.PartHeight;
        }

        public int GetPosition(WorkRectangle WorkSpace,int y,int ChunkNum,ComponentImage.RepresentationStage stage)
        {
            int FirstChunkProjection_Y=   y - ChunkNum * WorkSpace.PartHeight;
            if (stage==ComponentImage.RepresentationStage.Lisse)
            {
                return FirstChunkProjection_Y- ( WorkSpace.PartsHeightStart+(WorkSpace.SecEmptyLen-1));
            }else 
            {
                return FirstChunkProjection_Y-(WorkSpace.DentLen - 1 + WorkSpace.PartsHeightStart+(WorkSpace.SecEmptyLen-1));
            }
        }
        public int GetStageEnd(ComponentImage img)
        {
            if (img.ComponentStage == ComponentImage.RepresentationStage.Dent)
                return img.StartIndex+1;
            else
                return img.StartIndex+(WorkspaceSpec.LisseLen-1);
        }
        public int GetStageStart(ComponentImage img)
        {
            if (img.ComponentStage == ComponentImage.RepresentationStage.Dent)
                return  img.y - (img.position - 1);
            else
            {
                //if(WorkspaceSpec.SecEmptyLen==0)
                    //return img.y+1 - img.position;
                return img.y - (img.position-1);
                
            }
                
        }
        public int GetComplementaryStageStart(ComponentImage img)
        {
            if (img.ComponentStage == ComponentImage.RepresentationStage.Dent)
                return img.StartIndex-(WorkspaceSpec.LisseLen+1) ;
            else
                return img.StartIndex + (WorkspaceSpec.LisseLen+1);
            
        }
        public int GetComplementaryStageEnd(ComponentImage img)
        {
            if (img.ComponentStage == ComponentImage.RepresentationStage.Dent)
                return img.ComplemntaryStageStartIndex+(WorkspaceSpec.LisseLen-1);
            else
                return img.ComplemntaryStageStartIndex+1;
        }
        public ComponentImage.RepresentationStage GetCurrentStage(WorkRectangle WorkSpace,int y,int ChunkNum)
        {
          int FirstChunkProjection_Y=   y - ChunkNum * WorkSpace.PartHeight;

          int LisseStageStart = WorkSpace.SecEmptyLen + WorkSpace.PartsHeightStart;
          int LisseStageEnd =WorkSpace.SecEmptyLen + WorkSpace.LisseLen + WorkSpace.PartsHeightStart;
          // check if it belongs to the heddle stage
          if (FirstChunkProjection_Y >=
              LisseStageStart
              && FirstChunkProjection_Y <
              LisseStageEnd)
          {
              return ComponentImage.RepresentationStage.Lisse;
          }else if (FirstChunkProjection_Y ==
                    WorkSpace.DentLen + 1 + WorkSpace.PartsHeightStart+(WorkSpace.SecEmptyLen-1)
                    || FirstChunkProjection_Y ==
                    WorkSpace.DentLen + WorkSpace.PartsHeightStart+(WorkSpace.SecEmptyLen-1))
          {
              return ComponentImage.RepresentationStage.Dent;
          }
          else
          {
              return ComponentImage.RepresentationStage.StageSeperator;
          }
              
        }
         public void ClearPreviousWorkspace(WorkRectangle PrevWork,bool WorkspaceChange)
        {

                for (int h = PrevWork.PartsHeightStart; h <= PrevWork.PartsHeightEnd; h++)
                {
                    for (int l = PrevWork.PartsWidthStart; l <= PrevWork.PartsWidthEnd; l++)
                    {
                        var cell = GetGridCell(l, h);
                        var ChunkNum = GetCurrentChunk(PrevWork,h);
                        
                        if (WorkspaceChange)
                        {
                            if (cell.CellState == ChaineMatrixElement.ComponentState.Occupied)
                            {
                                if (cell.ComponentStage == ComponentImage.RepresentationStage.Lisse)
                                {
                                    var cp=  CompositionList.First(comp => comp.NumComposant == cell.NumComposant);
                                    if(cp.EnfNbrFil!=0)
                                        cp.EnfNbrFil = 0;
                                }
                                cell.CellState = ChaineMatrixElement.ComponentState.Vacant;
                                var element = EnfilageList.First(enf => enf.X == cell.x && enf.Y == cell.y);
                                EnfilageList.Remove(element);
                                AddNewElement = new EnfilageElement(element, false);
                            }
                        }
                        cell.ComponentStage = ComponentImage.RepresentationStage.OutRange;
                        cell.NumComposant =0;
                        cell.Focusable = false;
                        

                    }
                }

                if (WorkspaceChange)
                    NbrDent = 0;
        }

         public bool CheckStageInconsistency(int StartIndex,int EndIndex,int x,int TargetIndex)
         {
             for (int i = StartIndex; i <= EndIndex; i++)
             {
                 if(i==TargetIndex)
                     continue;
                var element= GetGridCell(x, i);
                if (element.CellState == ChaineMatrixElement.ComponentState.Occupied)
                    return true;
             }

             return false;

         }
         public bool CheckComplementaryStageInconsistency(int StartIndex,int EndIndex,int x,int NumComposant)
         {
             
             for (int i = StartIndex; i <= EndIndex; i++)
             {
                 var element= GetGridCell(x, i);
                 if (element.CellState == ChaineMatrixElement.ComponentState.Occupied 
                     && element.NumComposant!=NumComposant)
                     return true;
             }

             return false;

         }
            public void SetupWorkspace()
        {
            
            if (WorkspaceSpec != null)
            {
                
                for (int h = WorkspaceSpec.PartsHeightStart; h <= WorkspaceSpec.PartsHeightEnd; h++)
                {
                    for (int l = WorkspaceSpec.PartsWidthStart; l <= WorkspaceSpec.PartsWidthEnd; l++)
                    {
                        if (SecondRect != null)
                        {
                            double TestDb = ((double)SecondRect.StartHeight / WorkspaceSpec.PartHeight);
                            int ChainePart = (int)Math.Ceiling(TestDb);
                            if (ChainePart <=
                                (int)Math.Ceiling((double)(h- WorkspaceSpec.PartsHeightStart) / WorkspaceSpec.PartHeight) )
                            {
                                if (SecondRect.StartWidth > l
                                    ||SecondRect.EndWidth < l
                                    )
                                {
                                    continue;
                                }
                            }
                          
                        }
                        
                        var cell = GetGridCell(l, h);
                        var ChunkNum = GetCurrentChunk(WorkspaceSpec,h);
                        cell.ComponentStage = GetCurrentStage(WorkspaceSpec,h,ChunkNum);
                                   
                        cell.Focusable = true;
                        cell.y = h;
                        cell.x = l;
                        if (cell.ComponentStage != ComponentImage.RepresentationStage.StageSeperator)
                        {
                            cell.position = GetPosition(WorkspaceSpec, h, ChunkNum,cell.ComponentStage);
                            cell.StartIndex = GetStageStart(cell);
                            cell.ComplemntaryStageStartIndex= GetComplementaryStageStart(cell);
                            cell.ComplemntaryStageEndIndex= GetComplementaryStageEnd(cell);
                            cell.EndIndex = GetStageEnd(cell);
                        }
                    }
                }
               
            }
        
         
        }

            public ComponentImage GetGridCell(int x, int y)
            {
            if (this.Children[y + 2].GetType() != typeof(SpaceFreeGrid))
                return null;

                var TargetRow =(SpaceFreeGrid) this.Children[y + 2];
                int TargetElementNum = 0;
                int TargetContainerNum = Math.DivRem(x,10,out TargetElementNum) ;
              
                
                var TargetContainer =(SpaceFreeGrid) TargetRow.Children[TargetContainerNum];
                var TargetElement =(ComponentImage) TargetContainer.Children[TargetElementNum];

                return TargetElement;
            }

            // public static readonly DependencyProperty isPrintViewProperty = DependencyProperty.Register(
            //     nameof(isPrintView), typeof(bool), typeof(SpaceFreeGrid), new PropertyMetadata(default(bool)));
            //
            // public bool isPrintView
            // {
            //     get { return (bool)GetValue(isPrintViewProperty); }
            //     set { SetValue(isPrintViewProperty, value); }
            // }

            private bool _isPrintView;

            public bool isPrintView
            {
                get
                {
                    return _isPrintView;
                }
                set
                {
                    _isPrintView = value;
                }
            }

           
        public void SetEnfilageList(ObservableCollection<MatrixElement> OldEnfilageList)
        {
            if(OldEnfilageList!=null)
                foreach (var element in OldEnfilageList)
                 {  
                    
                     var TargetElement =GetGridCell(element.X,element.Y);
                     TargetElement.CellState = ChaineMatrixElement.ComponentState.Vacant;
                     TargetElement.NumComposant = 0;
                     TargetElement.ComponentStage = ComponentImage.RepresentationStage.OutRange;

                 }

            foreach (var element in EnfilageList)
            {
                var TargetElement = GetGridCell(element.X, element.Y);
                TargetElement.CellState = ChaineMatrixElement.ComponentState.Occupied;
                if (element.Content == null)
                {
                    TargetElement.NumComposant = 9;
                        TargetElement.ComponentStage = ComponentImage.RepresentationStage.StageSeperator;
                    continue;
                }
                else
                {
                    TargetElement.NumComposant = element.Content.NumComposant;
                }

                if (isPrintView)
                {
                    TargetElement.ComponentStage = ComponentImage.RepresentationStage.Lisse;
                    continue;
                }

                if (element.DentFil == 0)
                    TargetElement.ComponentStage = ComponentImage.RepresentationStage.Dent;
            else
               {
                   TargetElement.ComponentStage = ComponentImage.RepresentationStage.Lisse;
               }
            }
          
        }
        
        public static readonly DependencyProperty CreateChaineProperty = DependencyProperty.Register(
            nameof(CreateChaine), typeof(bool), typeof(SpaceFreeGrid), new PropertyMetadata(default(bool),OnSetChaineCreation));

        public bool CreateChaine
        {
            get { return (bool)GetValue(CreateChaineProperty); }
            set { SetValue(CreateChaineProperty, value); }
        }

        public static void OnSetChaineCreation(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var mycontrol = (SpaceFreeGrid)obj;
            mycontrol.SetChaineCreation();
        }

        public void SetChaineCreation()
        {
            if (CreateChaine)
            {
                
                mChaine.EnableChaineEdit = true;
               
               
               
            }
            else
            {
                mChaine.EnableChaineEdit = false;
                mChaine.SelectedComp = null;
            }
           
        }

        public static readonly DependencyProperty ChColListProperty = DependencyProperty.Register(
            nameof(ChColList), typeof(ObservableCollection<ChColComp> ), typeof(SpaceFreeGrid), new PropertyMetadata(default(ObservableCollection<ChColComp>),OnSetChaineColumn));

        public ObservableCollection<ChColComp> ChColList
        {
            get { return (ObservableCollection<ChColComp>)GetValue(ChColListProperty); }
            set { SetValue(ChColListProperty, value); }
        }

        public static void OnSetChaineColumn(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var mycontrol = (SpaceFreeGrid)obj;
            mycontrol.SetChaineColumn();
        }

        public void SetChaineColumn()
        {
            mChaine.ChaineColumnList = ChColList;
        }

        public static readonly DependencyProperty ComplistProperty = DependencyProperty.Register(
            nameof(Complist), typeof(ObservableCollection<Composant>), typeof(SpaceFreeGrid), new PropertyMetadata(null,OnSetListComposant));

        public ObservableCollection<Composant> Complist
        {
            get { return (ObservableCollection<Composant>)GetValue(ComplistProperty); }
            set { SetValue(ComplistProperty, value); }
        }

        public static void OnSetListComposant(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var mycontrol = (SpaceFreeGrid)obj;
            mycontrol.SetListComposant();
        }

        public void SetListComposant()
        {
            mChaine.CompList = Complist;
        }
        
        public static readonly DependencyProperty ChRowSumProperty = DependencyProperty.Register(
            nameof(ChRowSum), typeof(int), typeof(SpaceFreeGrid), new PropertyMetadata(default(int),OnSetChaineRowSum));

        public int ChRowSum
        {
            get { return (int)GetValue(ChRowSumProperty); }
            set { SetValue(ChRowSumProperty, value); }
        }

        public static void OnSetChaineRowSum(DependencyObject obj,DependencyPropertyChangedEventArgs args)
        {
            var mycontrol = (SpaceFreeGrid)obj;
            mycontrol.SetChaineRowSum();
        }

        public void SetChaineRowSum()
        {
            mChaine.ChaineRowSum = ChRowSum;
            SetChaineCreation();
        }
        
        public ControlDirection Direction
        {
            get { return (ControlDirection)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }
        public static readonly DependencyProperty ChaineList2Property = DependencyProperty.Register(
            nameof(ChaineList2), typeof(ObservableCollection<ChaineMatrixElement>), typeof(SpaceFreeGrid), new PropertyMetadata(OnSetChaineList2));

        public ObservableCollection<ChaineMatrixElement> ChaineList2
        {
            get { return (ObservableCollection<ChaineMatrixElement>)GetValue(ChaineList2Property); }
            set { SetValue(ChaineList2Property, value); }
        }

        public static void OnSetChaineList2(DependencyObject obj,DependencyPropertyChangedEventArgs args)
        {
            var mycontrol = (SpaceFreeGrid)obj;
            mycontrol.SetChaineList2();
        }

        public static readonly DependencyProperty TrameYpositionProperty = DependencyProperty.Register(
            nameof(TrameYposition), typeof(string), typeof(SpaceFreeGrid), new PropertyMetadata(default(string),OnSetTrameYPosition));

        public string TrameYposition
        {
            get { return (string)GetValue(TrameYpositionProperty); }
            set { SetValue(TrameYpositionProperty, value); }
        }

        public static void OnSetTrameYPosition(DependencyObject obj,DependencyPropertyChangedEventArgs args)
        {
            var mycontrol = (SpaceFreeGrid)obj;
            mycontrol.SetTrameYPosition();
        }

        public void SetTrameYPosition()
        {
            mChaine.ChainRotateY = Convert.ToDouble(TrameYposition);
        }
        public static readonly DependencyProperty TrameXpositionProperty = DependencyProperty.Register(
            nameof(TrameXposition), typeof(string), typeof(SpaceFreeGrid), new PropertyMetadata(default(string),OnSetTrameYPosition));

        public string TrameXposition
        {
            get { return (string)GetValue(TrameXpositionProperty); }
            set { SetValue(TrameXpositionProperty, value); }
        }
        public static void OnSetTrameXPosition(DependencyObject obj,DependencyPropertyChangedEventArgs args)
        {
            var mycontrol = (SpaceFreeGrid)obj;
            mycontrol.SetTrameXPosition();
        }
        public void SetTrameXPosition()
        {
            mChaine.ChainRotateX =Convert.ToDouble(TrameXposition);
        }
        public void SetChaineList2()
        {
            mChaine.ChList2 = ChaineList2;
        }
        public static readonly DependencyProperty ChaineListProperty = DependencyProperty.Register(
            nameof(ChaineList), typeof(ObservableCollection<ChaineMatrixElement>), typeof(SpaceFreeGrid), new PropertyMetadata(OnSetChaineList1));

        public ObservableCollection<ChaineMatrixElement> ChaineList
        {
            get { return (ObservableCollection<ChaineMatrixElement>)GetValue(ChaineListProperty); }
            set { SetValue(ChaineListProperty, value); }
        }

        public static void OnSetChaineList1(DependencyObject obj,DependencyPropertyChangedEventArgs args)
        {
            var mycontrol = (SpaceFreeGrid)obj;
            mycontrol.SetChaineList1();
        }
        public void SetChaineList1()
        {
            mChaine.ChList = ChaineList;
        }
        public static readonly DependencyProperty CompositionListProperty = DependencyProperty.Register(
            nameof(CompositionList), typeof(ObservableCollection<Composition>), typeof(SpaceFreeGrid),new PropertyMetadata(null,OnSetCompositionList));

        public static void OnSetCompositionList(DependencyObject obj,DependencyPropertyChangedEventArgs arg)
        {

            var mycontrol = (SpaceFreeGrid)obj;
            if(mycontrol.mlegend==null)
                return;
            mycontrol.SetCompositionList();
        }

        public void SetCompositionList()
        {
            mlegend.LegendList = CompositionList;
            mlegend.NotifySelectedComposantEvent = GetSelectedComposant;
        }

        private Composition SelectedComposant;
        public void GetSelectedComposant(Composition selectedComposant)
        {
            this.SelectedComposant = selectedComposant;
        }
        public ObservableCollection<Composition> CompositionList
        {
            get { return (ObservableCollection<Composition>)GetValue(CompositionListProperty); }
            set { SetValue(CompositionListProperty, value); }
        }
        
        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register("Direction", typeof(ControlDirection), typeof(SpaceFreeGrid), new PropertyMetadata(ControlDirection.Horizontal));


        private int NbrItem;

        private legend mlegend;

        private Chaine mChaine;
        
        public double BaseSideCalculation(Size ContainerSize)
        {
            if (Direction == ControlDirection.Horizontal)
            {
                _Columns = NbrItem;
                _Rows = 1;
                return ContainerSize.Width;
            }
            else
            {
                _Columns = 1;
                _Rows = NbrItem;
                return ContainerSize.Height;
            }
               

        }

        public static readonly DependencyProperty ContainerWidthProperty = DependencyProperty.Register(
            nameof(ContainerWidth), typeof(double), typeof(SpaceFreeGrid), new PropertyMetadata(default(double)));

        public double ContainerWidth
        {
            get { return (double)GetValue(ContainerWidthProperty); }
            set { SetValue(ContainerWidthProperty, value); }
        }
        private double _CellWidth;

        public double CellWidth
        {
            get
            {
                return _CellWidth;
            }
            set
            {
                _CellWidth = value;
                if (value != null)
                {
                    ContainerWidth = CellWidth * 83;
                    ContainerHeight = CellWidth * 59;
                }
                    
            }
        }

        public static readonly DependencyProperty ContainerHeightProperty = DependencyProperty.Register(
            nameof(ContainerHeight), typeof(double), typeof(SpaceFreeGrid), new PropertyMetadata(default(double)));

        public double ContainerHeight
        {
            get { return (double)GetValue(ContainerHeightProperty); }
            set { SetValue(ContainerHeightProperty, value); }
        }

        public static readonly DependencyProperty horizontalFreeSpaceProperty = DependencyProperty.Register(
            nameof(horizontalFreeSpace), typeof(double), typeof(SpaceFreeGrid), new PropertyMetadata(default(double)));

        public double horizontalFreeSpace
        {
            get { return (double)GetValue(horizontalFreeSpaceProperty); }
            set { SetValue(horizontalFreeSpaceProperty, value); }
        }

        public static readonly DependencyProperty VerticalFreeSpaceProperty = DependencyProperty.Register(
            nameof(VerticalFreeSpace), typeof(double), typeof(SpaceFreeGrid), new PropertyMetadata(default(double)));

        public double VerticalFreeSpace
        {
            get { return (double)GetValue(VerticalFreeSpaceProperty); }
            set { SetValue(VerticalFreeSpaceProperty, value); }
        }
        public double CalculateChildrenSize(double basedSize,Size ContainerSize)
        {
            if (_FirstHorizontalContainer)
            {
                double CellSize = basedSize / Columns;
                CellWidth = CellSize;
              return  CellSize* 10;

            }

            if (_FirstContainer)
            {
                double side = basedSize / NbrItem;
                if (true)
                {
                    horizontalFreeSpace = 0;
                    VerticalFreeSpace = 0;
                }

            else if ((ContainerSize.Width - side * Columns) < 0)
                {
                    CellWidth = ContainerSize.Width / Columns;
                     horizontalFreeSpace = (ContainerSize.Width-CellWidth * Columns)/2;
                    VerticalFreeSpace= (ContainerSize.Height - CellWidth * NbrItem)/2;
                }
                else
                {
                    horizontalFreeSpace = (ContainerSize.Width - side * Columns)/2;
                    VerticalFreeSpace= (ContainerSize.Height - side * NbrItem)/2;
                }

                

            }
                return basedSize / NbrItem;
        }

        private int _Columns;
        private int _Rows;

        private Size _ControlDesiredSize;

        
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
         
           
            base.OnMouseDown(e);
            Object obj=  e.Source;
            if (obj.GetType() == typeof(ComponentImage) )
            {
                var img = ((ComponentImage)obj);
                if (img.ChaineCell)
                {
                    img.CellState = ChaineMatrixElement.ComponentState.Occupied;
                    img.NumComposant = 1;
                    return;
                }
                if(img.ComponentStage==ComponentImage.RepresentationStage.OutRange
                   ||img.ComponentStage==ComponentImage.RepresentationStage.StageSeperator
                   || !FirstContainer)
                    return;
                
                img.Focus();
                img.Highlight = true;

                if(CheckStageInconsistency(img.StartIndex,img.EndIndex,img.x,img.y)
                   || SelectedComposant == null
                   || CheckComplementaryStageInconsistency(img.ComplemntaryStageStartIndex,img.ComplemntaryStageEndIndex,img.x,SelectedComposant.NumComposant))
                    return;
                if (img.ComponentStage == ComponentImage.RepresentationStage.Lisse)
                {
                    if (ChColList.First(ch => ch.ColNum == ((-img.position)+(ChColList.Count+1))).Comp.ID != SelectedComposant.GetComposant.ID
                        && ChColList.First(ch => ch.ColNum == ((-img.position)+(ChColList.Count+1))).Comp.ID != SelectedComposant.GetComposant.parent)
                    
                    {
                        MessageBox.Show("Réserver pour " +ChColList.First(ch => ch.ColNum ==((-img.position)+(ChColList.Count+1))).Comp.Name);
                        return;
                    }
                }
                else
                {
                    if (ChColList.FirstOrDefault(ch => ch.Comp.ID == SelectedComposant.GetComposant.ID)== null &&
                        ChColList.FirstOrDefault(ch => ch.Comp.ID == SelectedComposant.GetComposant.parent) == null)
                    {
                        MessageBox.Show("la chaine ne supporte pas " +SelectedComposant.GetComposant.Name);
                        return;
                    }
                }
               
                if ( SelectedComposant.NumComposant!=img.NumComposant) 
                {
                  bool ComponentRemoved=   removePrevComposant(img,true);
                    SetImageComponent(img,ComponentRemoved);
                }
                
               
            }
        }

        public static readonly DependencyProperty NbrDentProperty = DependencyProperty.Register(
            nameof(NbrDent), typeof(int), typeof(SpaceFreeGrid), new PropertyMetadata(default(int)));

        public int NbrDent
        {
            get { return (int)GetValue(NbrDentProperty); }
            set { SetValue(NbrDentProperty, value); }
        }

        public static readonly DependencyProperty AddNewElementProperty = DependencyProperty.Register(
            nameof(AddNewElement), typeof(EnfilageElement), typeof(SpaceFreeGrid), new PropertyMetadata(default(EnfilageElement)));

        public EnfilageElement AddNewElement
        {
            get { return (EnfilageElement)GetValue(AddNewElementProperty); }
            set { SetValue(AddNewElementProperty, value); }
        }
        public void SetImageComponent(ComponentImage img,bool RemovedComponent)
        {
            
            img.NumComposant = SelectedComposant.NumComposant;
            img.CellState = ChaineMatrixElement.ComponentState.Occupied;
            var el=new MatrixElement(img.x,img.y)
            {
                Content = SelectedComposant,
                DentFil = -(int)img.ComponentStage+3
            };
            
            if (!RemovedComponent)
            {
                EnfilageList.Add(el);
                AddNewElement = new EnfilageElement(el, true);
            }
            else
            {
                EnfilageList.First(enf => enf.X == el.X && enf.Y == el.Y).Content = SelectedComposant;
                AddNewElement = new EnfilageElement(el,true, el.Content);
            }
            
            if (img.ComponentStage == ComponentImage.RepresentationStage.Dent 
                && !RemovedComponent)
            {
                int adjacentPosition=0;
                if (img.position == 1)
                {
                    adjacentPosition = img.y + 1;
                }
                else
                {
                    adjacentPosition = img.y - 1;
                }
               int LeftTeeth= IterateReed(img.x, adjacentPosition, img.position, true);
               int RightTeeth= IterateReed(img.x, adjacentPosition, img.position, false);
               if(LeftTeeth==2 &&
                  RightTeeth==2)
               {
                   NbrDent = NbrDent + 3;
                   return;
               }else if ((LeftTeeth == -1 || RightTeeth == -1)
                         && (LeftTeeth == 2 || RightTeeth == 2))
               {
                   NbrDent = NbrDent +1;
                   return;
               }
                else if ((LeftTeeth == -1 && RightTeeth == 1) ||
                               (RightTeeth == -1 && LeftTeeth == 1))
                {
                    return;
                }
                NbrDent = NbrDent + Math.Max(LeftTeeth,RightTeeth);
              
            }
            else if(img.ComponentStage == ComponentImage.RepresentationStage.Lisse)
            {
                
                SelectedComposant.EnfNbrFil= SelectedComposant.EnfNbrFil+1;
            }
        }

        public int IterateReed(int x,int y,int position,bool moveForward)
        {
            int iterateDirection = 1;
            if (!moveForward)
                iterateDirection = -1;
            x=x+iterateDirection;
            bool stopLoop = false;
            bool changedPosition = false;
            int newTeethNum = 0;
            while (!stopLoop )
            {
               
                //move to another chunk
                if (moveForward)
                {

                    bool IsWorkspaceLastCell = x > WorkspaceSpec.PartsWidthEnd;
                    bool IsReachSecondRectangle = SecondRect.StartHeight <= y;
                    if (IsReachSecondRectangle)
                    {
                        IsWorkspaceLastCell = x > SecondRect.EndWidth;
                    }
                        
                    if (IsWorkspaceLastCell)
                    {
                        x = WorkspaceSpec.PartsWidthStart;


                        y = y - WorkspaceSpec.PartHeight;
                        IsReachSecondRectangle = SecondRect.StartHeight <= y;
                        if (IsReachSecondRectangle)
                            x = SecondRect.StartWidth;
                        if (y <= WorkspaceSpec.PartsHeightStart)
                        {
                            stopLoop = true;
                            if (newTeethNum == 1)
                                newTeethNum++;
                            continue;
                        }
                        
                    }
                }
                else
                {
                    int StartingCell = WorkspaceSpec.PartsWidthEnd;
                    bool IsWorkspaceFirstCell = x < WorkspaceSpec.PartsWidthStart;
                    bool IsReachSecondRectangle = SecondRect.StartHeight <= y;
                    if (IsReachSecondRectangle)
                    {
                        IsWorkspaceFirstCell = x < SecondRect.StartWidth;
                        StartingCell = SecondRect.EndWidth;
                    }
                        
                    if (IsWorkspaceFirstCell)
                    {
                        x = StartingCell;
                        y = y + WorkspaceSpec.PartHeight;
                        if (y > WorkspaceSpec.PartsHeightEnd)
                        {
                            stopLoop = true;
                            continue;
                        }
                        
                    }
                }
                    var img=  GetGridCell(x, y);
                
                    if (changedPosition)
                    {
                        
                        if (img.CellState != ChaineMatrixElement.ComponentState.Occupied)
                            newTeethNum++;
                        stopLoop = true;
                        continue;

                    }
                   if (img.CellState != ChaineMatrixElement.ComponentState.Occupied)
                   {

                       if (position == 1)
                       {
                           y--;
                           position = 2;
                       }
                       else
                       {
                           y++;
                           position = 1;
                       }
                           
                      

                       if (newTeethNum == 0)
                       {
                           newTeethNum = -1;
                           continue;
                       }
                           
                       changedPosition = true;
                   }
                   else
                   {
                       x=x+iterateDirection;
                       if(newTeethNum==0 )
                       {
                           newTeethNum++;
                       }
                   }
                    
            }
           return newTeethNum;
        }
        public bool removePrevComposant(ComponentImage img,bool isUpdate)
        {
            if (img.CellState == ChaineMatrixElement.ComponentState.Occupied 
                && img.NumComposant!=0 
                
                )
            {
                
                if (img.ComponentStage == ComponentImage.RepresentationStage.Lisse)
                {
                    var cp=  CompositionList.First(comp => comp.NumComposant == img.NumComposant);
                    cp.EnfNbrFil = cp.EnfNbrFil - 1;
                }

                if (!isUpdate)
                {
                    var element = EnfilageList.First(enf => enf.X == img.x && enf.Y == img.y);
                    EnfilageList.Remove(element);
                    AddNewElement = new EnfilageElement(element, false);
                }
                
                return true;
            }

            return false;
        }
        public void ClearSelectedComposant()
        {
            if (mlegend.SelectedComposant != null)
            {
                mlegend.SelectedComposant.Highlight = false;
                mlegend.SelectedComposant = null;
            }
        }

        public void SetSelectedComposant(int num)
        {
            mlegend.SelectedComposant = CompositionList.First(comp => comp.NumComposant == num);
            mlegend.SelectedComposant.Highlight = true;
        }
        public void SetComponent(ComponentImage img, Key pressedKey)
        {
            if (pressedKey == Key.NumPad9
                || pressedKey == Key.D9
                || pressedKey== Key.R)
            {

                if (img.ComponentStage == ComponentImage.RepresentationStage.StageSeperator)
                {
                    
                        img.NumComposant = 9;
                        var el=new MatrixElement(img.x,img.y)
                        {
                            Content = null,
                            DentFil = 2
                        };
                        AddNewElement = new EnfilageElement(el, true);
                   
                  
                }
                    
            }
            if(img.ComponentStage==ComponentImage.RepresentationStage.OutRange
               ||img.ComponentStage==ComponentImage.RepresentationStage.StageSeperator
               || !FirstContainer)
                return;
            if (pressedKey == Key.Back || pressedKey == Key.Delete)
            {
                if (img.NumComposant != 0)
                {
                     if (img.ComponentStage == ComponentImage.RepresentationStage.Dent)
                    {

                        int adjacentPosition=0;
                        if (img.position == 1)
                        {
                            adjacentPosition = img.y + 1;
                        }
                        else
                        {
                            adjacentPosition = img.y - 1;
                        }
                        int LeftTeeth= IterateReed(img.x, adjacentPosition, img.position, true);
                        int RightTeeth= IterateReed(img.x, adjacentPosition, img.position, false);
                        if(LeftTeeth==2 &&
                           RightTeeth==2)
                        {
                            NbrDent = NbrDent - 3;
                        }else if ((LeftTeeth == -1 || RightTeeth == -1)
                                  && (LeftTeeth == 2 || RightTeeth == 2))
                        {
                            
                                NbrDent = NbrDent - 1;
                         
                            
                        }
                        else if ((LeftTeeth == -1 && RightTeeth == 1) ||
                               (RightTeeth == -1 && LeftTeeth == 1))
                        {

                        }
                        else
                        {
                            NbrDent = NbrDent - Math.Max(LeftTeeth,RightTeeth);
                        }
                        
                    }
                    removePrevComposant(img,false);
                    img.CellState = ChaineMatrixElement.ComponentState.Vacant;
                    img.NumComposant = 0;
                }

                
                return;
            }

            

            if (pressedKey == Key.Escape)
                mlegend.SelectedComposant = null;
           
            if(CheckStageInconsistency(img.StartIndex,img.EndIndex,img.x,img.y)
               ||CompositionList==null)
                return;
            if (!(pressedKey >= Key.D1 && pressedKey <= Key.D7) &&
                !(pressedKey >= Key.NumPad1 && pressedKey<= Key.NumPad7))
                return;

            
            int num=0;
            if (pressedKey >= Key.D1 && pressedKey <= Key.D7)
            {
                num = (int)pressedKey - 34;
            }
            else
            {
                num = (int)pressedKey - 74;
            }
            if( CheckComplementaryStageInconsistency(img.ComplemntaryStageStartIndex,img.ComplemntaryStageEndIndex,img.x,num))
                return;
            if( CompositionList.Count<num
               || num==img.NumComposant)
                return;
            ClearSelectedComposant();
            SetSelectedComposant(num);
            if (img.ComponentStage == ComponentImage.RepresentationStage.Lisse)
            {
                if (ChColList.First(ch => ch.ColNum == ((-img.position)+(ChColList.Count+1))).Comp.ID != SelectedComposant.GetComposant.ID
                    && ChColList.First(ch => ch.ColNum == ((-img.position)+(ChColList.Count+1))).Comp.ID != SelectedComposant.GetComposant.parent)
                {
                    MessageBox.Show("Réserver pour " +ChColList.First(ch => ch.ColNum ==((-img.position)+(ChColList.Count+1))).Comp.Name);
                    return;
                }
            }
            else
            {
                if (ChColList.FirstOrDefault(ch => ch.Comp.ID == SelectedComposant.GetComposant.ID)== null &&
                    ChColList.FirstOrDefault(ch => ch.Comp.ID == SelectedComposant.GetComposant.parent) == null)
                {
                    MessageBox.Show("la chaine ne supporte pas " +SelectedComposant.GetComposant.Name);
                    return;
                }
            }
            bool ComponentRemoved= removePrevComposant(img,true);
           
            SetImageComponent(img,ComponentRemoved);

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            var obj = e.Source;
            if(obj.GetType()==typeof(ComponentImage))
                SetComponent((ComponentImage)obj,e.Key);
                
        }

       

       


        private bool _FirstContainer=true;
        public bool FirstContainer
        {
            get { return _FirstContainer; }
            set
            {
                _FirstContainer = value;
            }
        }

      

        private bool _RestContainer;

        
        
        public override void EndInit()
        {
            base.EndInit();
            
             if (FirstContainer)
             {
                mlegend = new legend();
                 mChaine = new Chaine();
                
                if (isPrintView)
                 {
                     mChaine.isPrint = true;
                     mlegend.isPrint = true;
                 }
                     
                 this.Children.Add(mlegend);
                 this.Children.Add(mChaine);
                 mBorder = new Border();
                 mBorder.BorderBrush=Brushes.Black;
                 mBorder.BorderThickness =new Thickness(1);
                
                SetZIndex(mChaine,8);
                 SetZIndex(mlegend,1);
                
                InitNestedChildren();

                //mtrameSymbol = new TrameSymbol();
                //this.Children.Add(mtrameSymbol);
                //SetZIndex(mtrameSymbol, 8);
                this.Children.Add(mBorder);
                

                NbrItem = Children.Count-3;
                
             }
                
            
           
        }

        private Border mBorder;


        public SpaceFreeGrid ConstructHorizontalLine(SpaceFreeGrid root )
        {
            int rest = 0;
            root._FirstHorizontalContainer = true;
            root.Columns = Columns;
            root.NbrItem =Convert.ToInt32(Math.Ceiling(Columns /(double)10));
            int NbrContainer = Math.DivRem(Columns,10, out rest);
            if (NbrContainer == 0 && rest>0)
            {
                NestedContainer = new SpaceFreeGrid();
                
                ResourceDictionary res2 = new ResourceDictionary();
                res2.Source=new Uri("/SpaceFreeGridStyle.xaml", 
                    UriKind.RelativeOrAbsolute);
                if(isPrintView)
                    NestedContainer =(SpaceFreeGrid)res2["SpaceFreeGridTemplatePrint"];
                else
                {
                    NestedContainer =(SpaceFreeGrid)res2["SpaceFreeGridTemplate"];
                }
                
                NestedContainer.FirstContainer = false;
                
                NestedContainer.Children.RemoveRange(0,(10-rest));
                NestedContainer._RestContainer = true;
                NestedContainer.NbrItem = rest;
                root.Children.Add(NestedContainer);
                
            }
            for (int i = 0; i < NbrContainer; i++)
            {
                NestedContainer= new SpaceFreeGrid();
                
                ResourceDictionary ress = new ResourceDictionary();
                ResourceDictionary res = new ResourceDictionary();

                res = (ResourceDictionary)Application.Current.FindResource("spaceX2");
                ress.Source=res.Source;
                if(isPrintView)
                    NestedContainer =(SpaceFreeGrid)ress["SpaceFreeGridTemplatePrint"];
                else
                {
                    NestedContainer =(SpaceFreeGrid)ress["SpaceFreeGridTemplate"];
                }
                
                NestedContainer.FirstContainer = false;
                NestedContainer.NbrItem = 10;
                
                root.Children.Add(NestedContainer);
            }

            if (rest > 0)
            {
                NestedContainer = new SpaceFreeGrid();
                
                ResourceDictionary res2 = new ResourceDictionary();
                res2.Source=((ResourceDictionary)Application.Current.FindResource("spaceX2")).Source;
                if(isPrintView)
                    NestedContainer =(SpaceFreeGrid)res2["SpaceFreeGridTemplatePrint"];
                else
                {
                    NestedContainer =(SpaceFreeGrid)res2["SpaceFreeGridTemplate"];
                }
                NestedContainer.FirstContainer = false;
                NestedContainer.Children.RemoveRange(0,(10-rest)); 
                NestedContainer._RestContainer = true;
                NestedContainer.NbrItem = rest;
                root.Children.Add(NestedContainer);
            }

            return root;
        }

        private SpaceFreeGrid RootContainer;
        public void InitNestedChildren()
        {
            this.NbrItem = Rows;
            for (int i = 0; i < Rows; i++)
            {
                this.Direction = ControlDirection.Vertical;
                SpaceFreeGrid root2 = new SpaceFreeGrid();
                root2.FirstContainer = false;
                root2.RootContainer = this;
                 root2= ConstructHorizontalLine(root2);
                 
                this.Children.Add(root2);
                SetZIndex(root2,0);
            }
        }

        public SpaceFreeGrid()
        {
            
        }

        
        
        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(
            nameof(Columns), typeof(int), typeof(SpaceFreeGrid), new PropertyMetadata(1));

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
            nameof(Rows), typeof(int), typeof(SpaceFreeGrid), new PropertyMetadata(1));

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }
        
        private SpaceFreeGrid NestedContainer;
        private bool _FirstHorizontalContainer;


        protected override Size MeasureOverride(Size constraint)
        {
            double basedSize = BaseSideCalculation(constraint);
           
           
           double ChildrenSide = CalculateChildrenSize(basedSize,constraint );
           Size childConstraint = new Size();
           double MaxChildWidth = 0;
           double MaxChildHeight = 0;
           if (Direction == ControlDirection.Horizontal)
           {
               
               childConstraint= new Size(ChildrenSide, constraint.Height);
           }
           else
           {
               childConstraint= new Size(constraint.Width, ChildrenSide);
           }
              
                 foreach (UIElement child in InternalChildren)
                 {
                     if (child == null) { continue; }

                     if (child.GetType() == typeof(ComponentImage))
                     {
                         
                     }
                     if (child.GetType() == typeof(legend)
                         || child.GetType() == typeof(Chaine))
                     {
                         if (child.GetType() == typeof(Chaine))
                             ((Chaine)child).CellWidth = Math.Min(constraint.Height/Rows,constraint.Width/Columns);
                             
                         child.Measure(new Size(constraint.Width,constraint.Height));
                         
                         continue;
                     }
                     if (child.GetType() == typeof(SpaceFreeGrid) && ((SpaceFreeGrid)child)._RestContainer)
                     {
                         
                         childConstraint= new Size(basedSize / Columns * ((SpaceFreeGrid)child).Children.Count, constraint.Height);
                         child.Measure(childConstraint);
                     }
                     else
                     {
                         child.Measure(childConstraint);
                     }
                     
                     if (MaxChildHeight < child.DesiredSize.Height)
                         MaxChildHeight = child.DesiredSize.Height;
                    if (MaxChildWidth < child.DesiredSize.Width)
                        MaxChildWidth = child.DesiredSize.Width;
                    
                 }
                _ControlDesiredSize= new Size(MaxChildWidth* _Columns, MaxChildHeight*_Rows);
                if (this.FirstContainer == true )
                {
                    _ControlDesiredSize= new Size(MaxChildHeight* 83, MaxChildHeight*59);
                }

                return _ControlDesiredSize;


        }
        
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            
            for (int i = 0; i < InternalChildren.Count; i++)
            {
               
                if (InternalChildren[i].GetType() == typeof(Border)
                   )
                {
                    var msize = new Size();
                    msize.Width = Math.Min(arrangeSize.Height / Rows, arrangeSize.Width / Columns) * Columns;
                    msize.Height = Math.Min(arrangeSize.Height / Rows, arrangeSize.Width / Columns) * Rows;
                    InternalChildren[i].Arrange(new Rect(new Point(0, 0), msize));

                    continue;
                }
                

                if (InternalChildren[i].GetType() == typeof(legend)
                   )
                {
                    InternalChildren[i].Arrange(new Rect(new Point(horizontalFreeSpace+1, arrangeSize.Height- InternalChildren[i].DesiredSize.Height-VerticalFreeSpace-1), InternalChildren[i].DesiredSize));

                    continue;
                }


                if ( InternalChildren[i].GetType() == typeof(Chaine))
                {
                    if (InternalChildren.Count > 1)
                    {
                        
                        InternalChildren[i].Arrange(new Rect(new Point(InternalChildren[i+1].DesiredSize.Height*Columns+horizontalFreeSpace- InternalChildren[i].DesiredSize.Width, arrangeSize.Height- InternalChildren[i].DesiredSize.Height-VerticalFreeSpace), InternalChildren[i].DesiredSize));

                    }
                    
                    continue;
                }
              
                if (Direction==ControlDirection.Vertical)
                {
                   
                    InternalChildren[i].Arrange(new Rect(new Point(horizontalFreeSpace, InternalChildren[i].DesiredSize.Height * (i-2)+VerticalFreeSpace), InternalChildren[i].DesiredSize));
                    
                }
                else
                {
                    if (InternalChildren[i].GetType() == typeof(SpaceFreeGrid) 
                        &&((SpaceFreeGrid)InternalChildren[i])._RestContainer)
                    {
                        InternalChildren[i].Arrange(new Rect(new Point(InternalChildren[0].DesiredSize.Width * i, 0), InternalChildren[i].DesiredSize));
                        continue;
                    }
                    InternalChildren[i].Arrange(new Rect(new Point(InternalChildren[i].DesiredSize.Width * i, 0), InternalChildren[i].DesiredSize));
                }


            }
            return _ControlDesiredSize;
            
        }
      

        
    }
}
