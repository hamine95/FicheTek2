using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BackEnd2.CustomClass;
using BackEnd2.Data;
using BackEnd2.Database;
using BackEnd2.Model;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class FicheTechniqueViewModel : MvxViewModel<user>
    {

        private bool _Editing;

        public bool Editing
        {
            get
            {
                return _Editing;
            }
            set
            {
                _Editing = value;
            }
        }
        public delegate void NotifySafeThread(bool b);

        public static  NotifySafeThread SafeThEvent;
        private ChaineBoardStructure _ChaineBoard;
        private ChaineBoardStructure _ChaineBoard2;
        private int _NbrDent;
        
        private string _TrameXposition="0";
        private string _TrameYposition="0";

        private string _LastYposition;
        
        private string _LastXposition;

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

        public string LastYposition
        {
            get => _LastYposition;
            set
            {
                _LastYposition = value;
                RaisePropertyChanged();
                SaveTrameYPosition();
            }
        }

        public string LastXposition
        {
            get => _LastXposition;
            set
            {
                _LastXposition = value;
                RaisePropertyChanged();
                SaveTrameXPosition();
            }
        }

        public void SaveTrameXPosition()
        {

            if (NewProd != null)
            {
                NewProd.EnfilageID.TrXposition = LastXposition;
                _Db.UpdateTrameX(NewProd.EnfilageID);
            }
            
        }
        public void SaveTrameYPosition()
        {
            if (NewProd != null)
            {
                NewProd.EnfilageID.TrYposition = LastYposition;
                _Db.UpdateTrameY(NewProd.EnfilageID);
            }
            
        }
        private BoardStructure _EnfilageBoard;
        public MvxObservableCollection<Couleur> ListCouleur2
        {
            get => _ListCouleur2;
            set
            {
                _ListCouleur2 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Couleur> ListCouleur3
        {
            get => _ListCouleur3;
            set
            {
                _ListCouleur3 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Couleur> ListCouleur4
        {
            get => _ListCouleur4;
            set
            {
                _ListCouleur4 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Couleur> ListCouleur5
        {
            get => _ListCouleur5;
            set
            {
                _ListCouleur5 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Couleur> ListCouleur6
        {
            get => _ListCouleur6;
            set
            {
                _ListCouleur6 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Couleur> ListCouleur7
        {
            get => _ListCouleur7;
            set
            {
                _ListCouleur7 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Couleur> ListCouleur8
        {
            get => _ListCouleur8;
            set
            {
                _ListCouleur8 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Couleur> ListCouleur9
        {
            get => _ListCouleur9;
            set
            {
                _ListCouleur9 = value;
                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor2
        {
            get => _SelectedColor2;
            set
            {
                _SelectedColor2 = value;
                if (value != null)
                {
                    UpdateMatiareComp2();
                }

                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor3
        {
            get => _SelectedColor3;
            set
            {
                _SelectedColor3 = value;
                if (value != null)
                {
                    UpdateMatiareComp3();
                }

                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor4
        {
            get => _SelectedColor4;
            set
            {
                _SelectedColor4 = value;
                if (value != null)
                {
                    UpdateMatiareComp4();
                }

                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor5
        {
            get => _SelectedColor5;
            set
            {
                _SelectedColor5 = value;
                if (value != null)
                {
                    UpdateMatiareComp5();
                }

                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor6
        {
            get => _SelectedColor6;
            set
            {
                _SelectedColor6 = value;
                if (value != null)
                {
                    UpdateMatiareComp6();
                }

                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor7
        {
            get => _SelectedColor7;
            set
            {
                _SelectedColor7 = value;
                if (value != null)
                {
                    UpdateMatiareComp7();
                }

                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor8
        {
            get => _SelectedColor8;
            set
            {
                _SelectedColor8 = value;
                if (value != null)
                {
                    UpdateMatiareComp8();
                }

                RaisePropertyChanged();

            }
        }

        public Couleur SelectedColor9
        {
            get => _SelectedColor9;
            set
            {
                _SelectedColor9 = value;
                if (value != null)
                {
                    UpdateMatiareComp9();
                }

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere2
        {
            get => _SelectedTypeMatiere2;
            set
            {
                _SelectedTypeMatiere2 = value;
                if (value != null)
                {
                    UpdateListTitrage2();
                }

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere3
        {
            get => _SelectedTypeMatiere3;
            set
            {
                _SelectedTypeMatiere3 = value;
                if (value != null)
                {
                    UpdateListTitrage3();
                }

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere4
        {
            get => _SelectedTypeMatiere4;
            set
            {
                _SelectedTypeMatiere4 = value;
                if (value != null)
                {
                    UpdateListTitrage4();
                }

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere5
        {
            get => _SelectedTypeMatiere5;
            set
            {
                _SelectedTypeMatiere5 = value;
                if (value != null)
                {
                    UpdateListTitrage5();
                }

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere6
        {
            get => _SelectedTypeMatiere6;
            set
            {
                _SelectedTypeMatiere6 = value;
                if (value != null)
                {
                    UpdateListTitrage6();
                }

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere7
        {
            get => _SelectedTypeMatiere7;
            set
            {
                _SelectedTypeMatiere7 = value;
                if (value != null)
                {
                    UpdateListTitrage7();
                }

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere8
        {
            get => _SelectedTypeMatiere8;
            set
            {
                _SelectedTypeMatiere8 = value;
                if (value != null)
                {
                    UpdateListTitrage8();
                }

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere9
        {
            get => _SelectedTypeMatiere9;
            set
            {
                _SelectedTypeMatiere9 = value;
                if (value != null)
                {
                    UpdateListTitrage9();
                }

                RaisePropertyChanged();
            }
        }

        private Composition _Comp1;

        private Titrage _Comp1Titrage;

        private Composition _Comp2;

        private Composition _Comp3;

        private Composition _Comp4;

        private Composition _Comp5;

        private Composition _Comp6;
        private Composition _Comp7;

        private Composition _Comp8;

        private Composition _Comp9;
        private MvxObservableCollection<Composition> _CompoList;

        private string _Concepteur;


        private bool _DisplayDate = true;
        private MvxObservableCollection<Duitages> _DuitageList;
        private bool _EditDate;

        private bool _EnableEditing;

        private MvxObservableCollection<FicheTechnique> _FicheTechniqueList;
        private bool _IsNextPage = true;

        private bool _IsPrevPage;
        private MvxObservableCollection<Client> _ListClient;
        private MvxObservableCollection<Composant> _ListComposant;

        private MvxObservableCollection<Concepteur> _ListConcepteur;


        
        private MvxObservableCollection<Titrage> _ListTitrage1;

        private MvxObservableCollection<Titrage> _ListTitrage2;

        private MvxObservableCollection<Titrage> _ListTitrage3;
        private MvxObservableCollection<Titrage> _ListTitrage4;

        private MvxObservableCollection<Titrage> _ListTitrage5;

        private MvxObservableCollection<Titrage> _ListTitrage6;

        private MvxObservableCollection<Titrage> _ListTitrage7;
        private MvxObservableCollection<Titrage> _ListTitrage8;

        private MvxObservableCollection<Titrage> _ListTitrage9;
        private MvxObservableCollection<TypeMatiere> _ListTypeMatiere;
        private MvxObservableCollection<Verificateur> _ListVerificateur;
        private MvxObservableCollection<Machine> _MachineList;
        private IMvxNavigationService _NavigationService;
        private Produit _NewProd;

        private string _PageNumber;


        private bool _Part1Visible = true;
        private bool _Part2Visible;


        private bool _PrintBtn = true;


        private bool _SaveCancelBtn;
        private Duitages _SelectedDuitage;


        private string _Verificateur;
        private  MyDBContext _Db;
        private SqliteData _DB2;

        public FicheTechniqueViewModel(IMvxNavigationService _navSer)
        {
            _NavigationService = _navSer;
           
            PageNumber = "1/2";
            _DB2 = new SqliteData();
            InitBtnImage();
            InitComposantBtnsCommand();
      
        }

        private MvxObservableCollection<MatrixElement> _ContentEnfilageList;

        private string CurP;

        public override  Task Initialize()
        {
            CompoList = new MvxObservableCollection<Composition>();
            NewProd = new Produit();
            
            CurP  =
                System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule
                    .FileName)+"\\ImgProd\\";
         
            LoadLists=MvxNotifyTask.Create(MyTask);
            return base.Initialize();
        }

        private MvxNotifyTask _LoadLists;

        public MvxNotifyTask LoadLists
        {
            get
            {
                return _LoadLists;
            }
            set
            {
                SetProperty(ref _LoadLists, value);
                RaisePropertyChanged();
                
            }
        }
        private async Task MyTask()
        {
            await RefreshLists();
          
            SafeThEvent.Invoke(true);
        }

        public void RefreshChaine(int ChCol,int ChRow)
        {
            if (ChRow > 78)
            {
                ChaineBoard = new ChaineBoardStructure(78,ChCol);
                ChaineList = new ObservableCollection<ChaineMatrixElement>(ChaineBoard.Board);
                ChaineBoard2 = new ChaineBoardStructure((ChRow-78),ChCol);
                ChaineList2 = new ObservableCollection<ChaineMatrixElement>(ChaineBoard2.Board);
                ChaineRows2 = ChRow - 78;
                ChaineRows = 78;
            }
            else
            {
                ChaineBoard = new ChaineBoardStructure(ChRow,ChCol);
                ChaineList = new ObservableCollection<ChaineMatrixElement>(ChaineBoard.Board);

                ChaineRows = ChRow;
            }
         
        }
        public async Task RefreshLists()
        {
            await Task.Run(() =>
            {
                ContentEnfilageList = new MvxObservableCollection<MatrixElement>();
                ChaineBoard = new ChaineBoardStructure(ChRowSum,ChaineColumns);
                ChaineList = new ObservableCollection<ChaineMatrixElement>(ChaineBoard.Board);
                
                ChaineBoard2 = new ChaineBoardStructure(ChaineRows2,ChaineColumns);
                ChaineList2 = new ObservableCollection<ChaineMatrixElement>(ChaineBoard2.Board);
                
                EnfilageBoard = new BoardStructure(EnfilageRow, EnfilageColumns);
                EnfilageList = new ObservableCollection<MatrixElement>(EnfilageBoard.Board);

                CategorieList = new MvxObservableCollection<Catalogue>(_DB2.GetCategoriesWithoutChildren());
                 ListClient = new MvxObservableCollection<Client>(_Db.GetClients());
                 ListVerificateur = new MvxObservableCollection<Verificateur>(_Db.GetVerificateur());
                 ListConcepteur = new MvxObservableCollection<Concepteur>(_Db.GetConcepteur());

                 ListRedacteur = new MvxObservableCollection<Redacteur>(_DB2.GetRedacteurs());
                 MachineList = new MvxObservableCollection<Machine>(_Db.GetMachines());

                 ListPeigne = new MvxObservableCollection<Reed>(_DB2.GetPeigneList());
                 
                ListComposant = new MvxObservableCollection<Composant>(_Db.GetComposants());
                ListTypeMatiere = new MvxObservableCollection<TypeMatiere>(_Db.GetTypeMatieres());
                ChaineNameList = new MvxObservableCollection<chaine>(_DB2.GetChaines());
                foreach (var tm in ListTypeMatiere) tm.MatiereNom = FirstCharToUpper(tm.MatiereNom);
            });
 
             await UpdateFicheTek();
          
        }

        private ObservableCollection<MatrixElement> _EnfilageList;
        private ObservableCollection<ChaineMatrixElement> _ChaineList;
        private ObservableCollection<ChaineMatrixElement> _ChaineList2;
        private int _EnfilageRow = 58;
        private int _EnfilageColumns = 83;
        private int _ChRowSum=8;
        
        private int _ChaineRows2=8;
        private int _ChaineColumns=4;
        public void UpdateCouleurList1()
        {
            ListCouleur1 = new MvxObservableCollection<Couleur>(_Db.GetCouleurs(SelectedTitrage1));
        }
        public void UpdateCouleurList2()
        {
            ListCouleur2 = new MvxObservableCollection<Couleur>(_Db.GetCouleurs(SelectedTitrage2));
        }
        public void UpdateCouleurList3()
        {
            ListCouleur3 = new MvxObservableCollection<Couleur>(_Db.GetCouleurs(SelectedTitrage3));
        }
        public void UpdateCouleurList4()
        {
            ListCouleur4 = new MvxObservableCollection<Couleur>(_Db.GetCouleurs(SelectedTitrage4));
        }
        public void UpdateCouleurList5()
        {
            ListCouleur5 = new MvxObservableCollection<Couleur>(_Db.GetCouleurs(SelectedTitrage5));
        }
        public void UpdateCouleurList6()
        {
            ListCouleur6 = new MvxObservableCollection<Couleur>(_Db.GetCouleurs(SelectedTitrage6));
        }
        public void UpdateCouleurList7()
        {
            ListCouleur7 = new MvxObservableCollection<Couleur>(_Db.GetCouleurs(SelectedTitrage7));
        }
        public void UpdateCouleurList8()
        {
            ListCouleur8 = new MvxObservableCollection<Couleur>(_Db.GetCouleurs(SelectedTitrage8));
        }
        public void UpdateCouleurList9()
        {
            ListCouleur9 = new MvxObservableCollection<Couleur>(_Db.GetCouleurs(SelectedTitrage9));
        }

        private int _RotateY;

        public int RotateY
        {
            get
            {
                return _RotateY;
            }
            set
            {
                _RotateY = value;
                RaisePropertyChanged();
            }
        }
        private int _RotateX;

        public int RotateX
        {
            get
            {
                return _RotateX;
            }
            set
            {
                _RotateX = value;
                RaisePropertyChanged();
            }
        }
        public void EditFicheTechnique()
        {
            Editing = true;
            if (SelectedFicheTechnique != null)
            {
               
               int LastVersion= SelectedFicheTechnique.Produits.Count - 1;
               
               if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
               {
                   IsEnfilage = true;
                   if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine != null)
                   {
                       SelectedChaine = ChaineNameList.SingleOrDefault(ch =>
                           ch.ID == SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine.ID);
                   }
               }
               else
               {
                   IsEnfilage = false;
               }
               
                IsDentFilVis = true;
                EditDate = true;
                DisplayDate = false;
                EnableEditing = true;
                SaveCancelBtn = true;
                PrintBtn = false;
                BtnVis = true;
                IsAddEnabled = false;
                OldVersion = NewProd.Version;
                if (SelectedFicheTechnique.Produits[LastVersion].DuitageID != null)
                {
                    if (SelectedFicheTechnique.Produits[LastVersion].DuitageID.Machine!=null)
                    {
                        SelectedMachine = MachineList.SingleOrDefault(ma =>
                            ma.ID == SelectedFicheTechnique.Produits[LastVersion].DuitageID.Machine.ID);
                        NewProd.DuitageID = DuitageList.SingleOrDefault(duit =>
                            duit.ID == SelectedFicheTechnique.Produits[LastVersion].DuitageID.ID);
                    } 
                  
                }

                if (SelectedFicheTechnique.Produits[LastVersion].Version == 0)
                {
                    IsProduction = false;
                    MinVers = 0;
                }
                else
                {
                    IsProduction = true;
                    MinVers = 1;
                    if (SelectedFicheTechnique.Produits[LastVersion].Version == 1)
                    {
                        IsUpdate = false;
                    }
                    else
                    {
                        IsUpdate = true;
                    }
                }

                if(SelectedFicheTechnique.Produits[LastVersion].Client!=null)
                {
                    NewProd.Client = ListClient.SingleOrDefault(cl =>
                        cl.ID == SelectedFicheTechnique.Produits[LastVersion].Client.ID);
                }
                if(SelectedFicheTechnique.Produits[LastVersion].PeigneObj!=null)
                {
                    NewProd.PeigneObj = ListPeigne.SingleOrDefault(rd =>
                        rd.ID == SelectedFicheTechnique.Produits[LastVersion].PeigneObj.ID);
                }
                if(SelectedFicheTechnique.Produits[LastVersion].RedacteurObj!=null)
                {
                    NewProd.RedacteurObj = ListRedacteur.SingleOrDefault(red =>
                        red.ID == SelectedFicheTechnique.Produits[LastVersion].RedacteurObj.ID);
                }
                if(SelectedFicheTechnique.Produits[LastVersion].Concepteur!=null)
                {
                    NewProd.Concepteur = ListConcepteur.SingleOrDefault(cp =>
                        cp.ID == SelectedFicheTechnique.Produits[LastVersion].Concepteur.ID);
                }
                if(SelectedFicheTechnique.Produits[LastVersion].Verificateur!=null)
                {
                    NewProd.Verificateur = ListVerificateur.SingleOrDefault(vr =>
                        vr.ID == SelectedFicheTechnique.Produits[LastVersion].Verificateur.ID);
                }
                
                if (NewProd.DuitageID != null)
                {
                    Vitesse= NewProd.DuitageID.Vitesse.ToString();
                }
                
                
                 for(int i=0;i<NewProd.GetComposition.Count;i++)
            {
                if (NewProd.GetComposition[i].GetComposant != null)
                {
                       if (NewProd.GetComposition[i].NumComposant==8)
                       {
                           Comp8.GetComposant = ListComposant.SingleOrDefault(
                               cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                    Comp8Vis = true;
                    ChangeImageBtn8 = "../Asset/remove64.png";
                    if (Comp8.GetMatiere != null)
                    {
                        SelectedTypeMatiere8= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp8.GetMatiere.Titrage.TypeMatiere.ID);
                        SelectedTitrage8=ListTitrage8.SingleOrDefault(tit => tit.ID == Comp8.GetMatiere.Titrage.ID);
                        SelectedColor8=ListCouleur8.SingleOrDefault(co => co.ID == Comp8.GetMatiere.GetCouleur.ID);
 
                    }
                    
                    Comp8.PropertyChanged += Comp8_PropertyChanged;
                       }
                else if (NewProd.GetComposition[i].NumComposant==9)
                {
                    Comp9.GetComposant = ListComposant.SingleOrDefault(
                        cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                    Comp9Vis = true;
                    ChangeImageBtn9 = "../Asset/remove64.png";
                    if (Comp9.GetMatiere != null)
                    {
                        SelectedTypeMatiere9= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp9.GetMatiere.Titrage.TypeMatiere.ID);
                        SelectedTitrage9=ListTitrage9.SingleOrDefault(tit => tit.ID == Comp9.GetMatiere.Titrage.ID);
                        SelectedColor9=ListCouleur9.SingleOrDefault(co => co.ID == Comp9.GetMatiere.GetCouleur.ID);

                    }
                    
                    Comp9.PropertyChanged += Comp9_PropertyChanged;
                }
                else
                {
                    if (NewProd.GetComposition[i].NumComposant==1)
                    {
                        Comp1.GetComposant = ListComposant.SingleOrDefault(
                            cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                        Comp1Vis = true;
                        ChangeImageBtn1 = "../Asset/remove64.png";
                        if (Comp1.GetMatiere != null)
                        {
                            SelectedTypeMatiere1= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp1.GetMatiere.Titrage.TypeMatiere.ID);
                            SelectedTitrage1=ListTitrage1.SingleOrDefault(tit => tit.ID == Comp1.GetMatiere.Titrage.ID);
                            SelectedColor1=ListCouleur1.SingleOrDefault(co => co.ID == Comp1.GetMatiere.GetCouleur.ID);
                        }
                   
                        Comp1.PropertyChanged += Comp1_PropertyChanged;
                    }

                    if (NewProd.GetComposition[i].NumComposant==2)
                    {
                        Comp2.GetComposant = ListComposant.SingleOrDefault(
                            cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                        Comp2Vis = true;
                        ChangeImageBtn2 = "../Asset/remove64.png";
                        if (Comp2.GetMatiere != null)
                        {
                            SelectedTypeMatiere2= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp2.GetMatiere.Titrage.TypeMatiere.ID);
                            SelectedTitrage2=ListTitrage2.SingleOrDefault(tit => tit.ID == Comp2.GetMatiere.Titrage.ID);
                            SelectedColor2=ListCouleur2.SingleOrDefault(co => co.ID == Comp2.GetMatiere.GetCouleur.ID);
 
                        }
                       
                        Comp2.PropertyChanged += Comp2_PropertyChanged;
                    }

                    if (NewProd.GetComposition[i].NumComposant==3)
                    {
                        Comp3.GetComposant = ListComposant.SingleOrDefault(
                            cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                        Comp3Vis = true;
                        ChangeImageBtn3 = "../Asset/remove64.png";
                        if (Comp3.GetMatiere != null)
                        {
                            SelectedTypeMatiere3= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp3.GetMatiere.Titrage.TypeMatiere.ID);
                            SelectedTitrage3=ListTitrage3.SingleOrDefault(tit => tit.ID == Comp3.GetMatiere.Titrage.ID);
                            SelectedColor3=ListCouleur3.SingleOrDefault(co => co.ID == Comp3.GetMatiere.GetCouleur.ID);

                        }
                        
                        Comp3.PropertyChanged += Comp3_PropertyChanged;
                    }

                    if (NewProd.GetComposition[i].NumComposant==4)
                    {
                        Comp4.GetComposant = ListComposant.SingleOrDefault(
                            cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                        Comp4Vis = true;
                        ChangeImageBtn4 = "../Asset/remove64.png";
                        if (Comp4.GetMatiere != null)
                        {
                            SelectedTypeMatiere4= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp4.GetMatiere.Titrage.TypeMatiere.ID);
                            SelectedTitrage4=ListTitrage4.SingleOrDefault(tit => tit.ID == Comp4.GetMatiere.Titrage.ID);
                            SelectedColor4=ListCouleur4.SingleOrDefault(co => co.ID == Comp4.GetMatiere.GetCouleur.ID);

                        }
                       
                        Comp4.PropertyChanged += Comp4_PropertyChanged;
                    }

                    if (NewProd.GetComposition[i].NumComposant==5)
                    {
                        Comp5.GetComposant = ListComposant.SingleOrDefault(
                            cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                        Comp5Vis = true;
                        ChangeImageBtn5 = "../Asset/remove64.png";
                        if (Comp5.GetMatiere != null)
                        {
                            SelectedTypeMatiere5= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp5.GetMatiere.Titrage.TypeMatiere.ID);
                            SelectedTitrage5=ListTitrage5.SingleOrDefault(tit => tit.ID == Comp5.GetMatiere.Titrage.ID);
                            SelectedColor5=ListCouleur5.SingleOrDefault(co => co.ID == Comp5.GetMatiere.GetCouleur.ID);
 
                        }
                       
                        Comp5.PropertyChanged += Comp5_PropertyChanged;
                    }

                    if (NewProd.GetComposition[i].NumComposant==6)
                    {
                        Comp6.GetComposant = ListComposant.SingleOrDefault(
                            cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                        Comp6Vis = true;
                        ChangeImageBtn6 = "../Asset/remove64.png";
                        if (Comp6.GetMatiere != null)
                        {
                            SelectedTypeMatiere6= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp6.GetMatiere.Titrage.TypeMatiere.ID);
                            SelectedTitrage6=ListTitrage6.SingleOrDefault(tit => tit.ID == Comp6.GetMatiere.Titrage.ID);
                            SelectedColor6=ListCouleur6.SingleOrDefault(co => co.ID == Comp6.GetMatiere.GetCouleur.ID);

                        }
                        
                        Comp6.PropertyChanged += Comp6_PropertyChanged;
                    }

                    if (NewProd.GetComposition[i].NumComposant==7)
                    {
                        Comp7.GetComposant = ListComposant.SingleOrDefault(
                            cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                        Comp7Vis = true;
                        ChangeImageBtn7 = "../Asset/remove64.png";
                        if (Comp7.GetMatiere != null)
                        {
                            SelectedTypeMatiere7= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp7.GetMatiere.Titrage.TypeMatiere.ID);
                            SelectedTitrage7=ListTitrage7.SingleOrDefault(tit => tit.ID == Comp7.GetMatiere.Titrage.ID);
                            SelectedColor7=ListCouleur7.SingleOrDefault(co => co.ID == Comp7.GetMatiere.GetCouleur.ID);

                        }
                        
                        Comp7.PropertyChanged += Comp7_PropertyChanged;
                    }
                }
                }

               
            }


                
                
                 if(SelectedFicheTechnique.Catalog!=null)
                     SelectedCategorie =CategorieList.SingleOrDefault(cat=>cat.ID==SelectedFicheTechnique.Catalog.ID) ;
               
                
                 NewProd.PropertyChanged += Prod_PropertyChanged;
            }
            else
            {
                SendNotification.Raise("Aucune Fiche Technique Séléctionnée");
            }
        }
        public void DeleteFicheTechnique()
        {
            if(SelectedFicheTechnique!=null)
                {
                    var req = new YesNoQuestion()
                    {
                        Question = "êtes-vous sûr de vouloir supprimer cette Fiche Technique séléctionnée ?",

                        UploadCallback = (ok)=>
                        {
                            if(ok)
                            {
                              
                               foreach (var prod in SelectedFicheTechnique.Produits)
                               {
                                   if (prod.EnfilageID != null)
                                   {
                                       _DB2.DeleteFicheTechnique(SelectedFicheTechnique.ID,prod.Id,prod.EnfilageID.ID);
                                   }
                                   else
                                   {
                                       _DB2.DeleteFicheTechnique(SelectedFicheTechnique.ID,prod.Id);
                                   }
                                 
                               }
                               
                                AnnulerFicheTechnique();
                                UpdateFicheTek();
                            }
                
                        }
                    };
                    ConfirmAction.Raise(req);
                }
            else
            {
                SendNotification.Raise("Aucun Fiche Technique séléctionnée");
            }
            
        }
        private void Prod_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            if (e.PropertyName.Equals("DuitageID"))
            {
                NewProd.DuitageID.Machine = SelectedMachine;
                SetupVitesse();
                _DB2.UpdateProdDuitage(NewProd);
            }else if (e.PropertyName.Equals("NumArticle"))
            {
                if (NewProd.NumArticle != OldNumArticle)
                {
                    if (_DB2.CheckNArticleUnique(NewProd.NumArticle,NewProd.Id))
                    {
                        _DB2.UpdateProdNumArticle(NewProd);

                        OldNumArticle = NewProd.NumArticle;
                    }
                    else
                    {
                        SendNotification.Raise("Numéro D'article déja utilisé");
                        NewProd.NumArticle = OldNumArticle;
                    }
                }
                
            }else if (e.PropertyName.Equals("Version"))
            {
                if (NewProd.Version < 2)
                {
                    IsUpdate = false;
                    NewProd.MiseAJour = null;
                }
                else
                {
                    IsUpdate = true;
                   // NewProd.MiseAJour=DateTime.Now;
                }
                if (NewProd.Version != OldVersion)
                {
                    if (_DB2.CheckVersionUnique(NewProd))
                    {
                        _DB2.UpdateProdVersion(NewProd);
                        OldVersion = NewProd.Version;
                    }
                    else
                    {
                        SendNotification.Raise("Cette Version existe déja");
                         NewProd.Version =OldVersion;
                    } 
                }
            
            }else if (e.PropertyName.Equals("Ref"))
            {
              
               _DB2.UpdateProdRef(NewProd);
               UpdateFicheTek();
               
            }else if (e.PropertyName.Equals("EnfDent"))
            {
              
                _DB2.UpdateEnfDent(NewProd);
               
            }else if (e.PropertyName.Equals("Name"))
            {

                if (mf!=null && mf.IsEchantillon == false)
                {
                    if (NewProd.FicheTekID != null)
                    {
                        if (NewProd.FicheTekID.Catalog != null)
                        {
                            int ord=  _DB2.AssignOrderToFicheTechnique(NewProd.FicheTekID.ID, NewProd.FicheTekID.Catalog.ID);
                            Ordre = ord.ToString();
                        }
                    }
                }
              
                _DB2.UpdateProdName(NewProd);
                UpdateFicheTek();
            }else if (e.PropertyName.Equals("Name2"))
            {
               
                _DB2.UpdateProdName2(NewProd);
             
            }else if (e.PropertyName.Equals("Client"))
            {
               
                _DB2.UpdateProdClient(NewProd);
               
            }else if (e.PropertyName.Equals("Largeur"))
            {
                if (mf!=null && mf.IsEchantillon == false)
                {
                    if (NewProd.FicheTekID != null)
                    {
                        if (NewProd.FicheTekID.Catalog != null)
                        {
                            int ord= _DB2.AssignOrderToFicheTechnique(NewProd.FicheTekID.ID, NewProd.FicheTekID.Catalog.ID);
                            Ordre = ord.ToString();
                        }
                    }
                }
              
                _DB2.UpdateProdLargeur(NewProd);
               
            }else if (e.PropertyName.Equals("Epaisseur"))
            {
                if (mf != null && mf.IsEchantillon == false)
                {
                    if (NewProd.FicheTekID != null)
                    {
                        if (NewProd.FicheTekID.Catalog != null)
                        {
                            int ord= _DB2.AssignOrderToFicheTechnique(NewProd.FicheTekID.ID, NewProd.FicheTekID.Catalog.ID);
                            Ordre = ord.ToString();
                        }
                    }
                }
               
                _DB2.UpdateProdEpaiseur(NewProd);
               
            }else if (e.PropertyName.Equals("DuitageID"))
            {
               
                _DB2.UpdateProdDuitage(NewProd);
               
            }else if (e.PropertyName.Equals("DuitageGomme"))
            {
               
                _DB2.UpdateProdDuitageGomme(NewProd);
               
            }else if (e.PropertyName.Equals("Dent"))
            {
               
                _DB2.UpdateProdDent(NewProd);
               
            }else if (e.PropertyName.Equals("PeigneObj"))
            {
               
                _DB2.UpdateProdPeigne(NewProd);
               
            }else if (e.PropertyName.Equals("IsEnfilage"))
            {
               
                _DB2.UpdateProdIsEnfilage(NewProd);
                if (IsEnfilage)
                {
                    
                    NewProd.EnfilageID = new Enfilage();
                    NewProd.EnfilageID.Column = EnfilageColumns;
                    NewProd.EnfilageID.Row  = EnfilageRow;
                    NewProd.EnfilageID.TrXposition = LastXposition;
                    NewProd.EnfilageID.TrYposition = LastYposition;
                    NewProd.EnfilageID.ID= _DB2.AddNewEnfilage(NewProd.EnfilageID);
                    
                }
                else
                {
                  
                    _DB2.RemoveEnfilage(NewProd.EnfilageID);
                    NewProd.EnfilageID = null;
                }
               
            }else if (e.PropertyName.Equals("Concepteur"))
            {
               
                _DB2.UpdateProdConcepteur(NewProd);
               
            }else if (e.PropertyName.Equals("Verificateur"))
            {
               
                _DB2.UpdateProdVerificateur(NewProd);
               
            }else if (e.PropertyName.Equals("Redaction"))
            {
               
                _DB2.UpdateProdRedaction(NewProd);
               
            }else if (e.PropertyName.Equals("DateCreation"))
            {
               
                _DB2.UpdateProdDateCreation(NewProd);
               
            }else if (e.PropertyName.Equals("MiseAJour"))
            {
               
                _DB2.UpdateProdMiseAJour(NewProd);
               
            }else if (e.PropertyName.Equals("RedacteurObj"))
            {
               
                _DB2.UpdateProdRedacteur(NewProd);
               
            }
           
        }
        public async Task UpdateFicheTek()
        {
            await Task.Run(() =>
            {
              //  FicheTechniqueList = new MvxObservableCollection<FicheTechnique>(_Db.GetFicheTechniques());
              if (IsAllFT)
              {
                  FullFTList = new MvxObservableCollection<FicheTechnique>(_DB2.GetFicheTechniques());
                  FullFTByCatList=FullFTList;
                  FicheTechniqueList = FullFTList;
              }else if (IsProdFT)
              {
                  FullFTList = new MvxObservableCollection<FicheTechnique>(_DB2.GetFicheTechniquesProd());
                  FullFTByCatList=FullFTList;
                  FicheTechniqueList = FullFTList;
              }
              else
              {
                  FullFTList = new MvxObservableCollection<FicheTechnique>(_DB2.GetFicheTechniquesEch());
                  FullFTByCatList=FullFTList;
                  FicheTechniqueList = FullFTList;
              }
              
            });

        }

        private Produit _SelectedVersion;

        public Produit SelectedVersion
        {
            get
            {
                return _SelectedVersion;
            }
            set
            {
                _SelectedVersion = value;
                if (value != null)
                {
                  int index= ListVersion.IndexOf(value);
                  SelectedFicheTechnique.Produits[index] = _DB2.GetFullProduct(SelectedFicheTechnique.Produits[index]);
                    DisplaySelectedFicheTek(index);
                }
                RaisePropertyChanged();
            }
        }
        private MvxObservableCollection<Produit> _ListVersion;

        public MvxObservableCollection<Produit> ListVersion
        {
            get
            {
                return _ListVersion;
            }
            set
            {
                _ListVersion = value;
                RaisePropertyChanged();
            }
        }

        private FicheTechnique _SelectedFicheTechnique;

        public FicheTechnique SelectedFicheTechnique
        {
            get { return _SelectedFicheTechnique;}
            set { _SelectedFicheTechnique = value;RaisePropertyChanged();
                if (value != null)
                {
                    int vers =SelectedFicheTechnique.Produits.Count - 1;
                    _SelectedFicheTechnique = _DB2.GetFullFicheTechnique(value,vers);
                    if (SelectedFicheTechnique.Produits.Count > 0)
                    {
                        ListVersion =new MvxObservableCollection<Produit>(SelectedFicheTechnique.Produits) ;
                    }
                   
                    SelectedVersion =
                        ListVersion.SingleOrDefault(ver => ver.Id == SelectedFicheTechnique.Produits[vers].Id);
                }
                else
                {
                    tempo = false;
                    if (UserSession.type == user.UserType.verificateur)
                    {
                        IsVerificateur = true;
                    }
              
                }
            }
        }

        private DateTime _DateEndLimit;

        public DateTime DateEndLimit
        {
            get
            {
                return _DateEndLimit;
            }
            set
            {
                _DateEndLimit = value;
                RaisePropertyChanged();
            }
        }

        public void ResetComposition()
        {
            TotalPoids = 0;
            Comp1 = null;
            Comp2 = null;
            Comp3 = null;
            Comp4 = null;
            Comp5 = null;
            Comp6 = null;
            Comp7 = null;
            Comp8 = null;
            Comp9 = null;
            
        }
        public void DisplaySelectedFicheTek(int vers)
        {
            Ordre = "";
            SchCompList = new MvxObservableCollection<Composition>();
            if (SelectedFicheTechnique.Produits.Count > 0)
            {
                if (SelectedFicheTechnique.ModelFiche == (int)ModelFiche.ModelFicheTek.FicheTekCrochetage)
                {
                    IsFicheCrochetage = true;
                    IsFicheUniDuitage = false;
                    IsFicheEHC = false;
                    IsFicheNormal = true;
                }else if (SelectedFicheTechnique.ModelFiche == (int)ModelFiche.ModelFicheTek.FicheTekEHC)
                {
                    IsFicheCrochetage = false;
                    IsFicheUniDuitage = true;
                    IsFicheEHC = true;
                    IsFicheNormal = false;
                    
                }
                else
                {
                    IsFicheCrochetage = false;
                    IsFicheUniDuitage = true;
                    IsFicheEHC = false;
                    IsFicheNormal = true;
                }

                NbrDent = SelectedFicheTechnique.Produits[vers].EnfDent;
               
                if (SelectedFicheTechnique.Ordre != 0)
                {
                    Ordre = SelectedFicheTechnique.Ordre.ToString();
                }
                if (SelectedFicheTechnique.Produits[vers].Definite == 0)
                {
                    IsPerm = false;
                    IsConfirm = false;
                }
                else
                {
                    if (UserSession.type == user.UserType.redacteur && SelectedFicheTechnique.Produits[vers].Version!=0)
                    {
                        IsPerm = true;
                        IsConfirm = false; 
                    }else if (SelectedFicheTechnique.Produits[vers].Version == 0)
                    {
                        IsConfirm = true;
                        IsPerm = false;
                    }
                    else
                    {
                        IsConfirm = false; 
                        IsPerm = false;
                    }
                    
                }
                if (SelectedFicheTechnique.Produits[vers].Definite == 0)
                {
                    tempo = true;
                    if (UserSession.type == user.UserType.verificateur)
                    {
                        IsVerificateur = true;
                    }
                } else
                {
                    tempo = false;
                    if (UserSession.type == user.UserType.verificateur)
                    {
                        IsVerificateur = false;
                    }
                }

                ImageProd = null;
                if (SelectedFicheTechnique.Produits[vers].image != null)
                {
                    ImageProd = CurP + SelectedFicheTechnique.Produits[vers].image;
                }
               
                  
                NewProd =(Produit) SelectedFicheTechnique.Produits[vers].Clone();
         
            if (SelectedFicheTechnique.ModelFiche ==((int) ModelFiche.ModelFicheTek.FicheTekCrochetage))
            {
                IsFicheCrochetage = true;
                IsFicheUniDuitage = false;
                IsEnfilage = false;


            }else if (SelectedFicheTechnique.ModelFiche == ((int)ModelFiche.ModelFicheTek.FicheTekEHC))
            {
                IsFicheEHC = true;
                IsFicheNormal = false;
            }

            ResetComposition();
            for(int i=0;i<NewProd.GetComposition.Count;i++)
            {
                if (NewProd.GetComposition[i].GetComposant != null)
                {
                       if (NewProd.GetComposition[i].NumComposant==8)
                {
                    Comp8 =(Composition) NewProd.GetComposition[i].Clone();
                }
                else if (NewProd.GetComposition[i].NumComposant==9)
                {
                    Comp9 =(Composition) NewProd.GetComposition[i].Clone();
                }
                else
                {
                    if ( NewProd.GetComposition[i].NumComposant==1)
                    {
                        Comp1 =(Composition) NewProd.GetComposition[i].Clone();
                        if (NewProd.IsEnfilage==1)
                        {
                            SchCompList.Add(Comp1);
                        }
                       
                    }

                    if (NewProd.GetComposition[i].NumComposant==2)
                    {
                        Comp2 =(Composition) NewProd.GetComposition[i].Clone();
                        if (NewProd.IsEnfilage==1)
                        {
                            SchCompList.Add(Comp2);
                        }
                    }

                    if (NewProd.GetComposition[i].NumComposant==3)
                    {
                        Comp3 =(Composition) NewProd.GetComposition[i].Clone();
                        if (NewProd.IsEnfilage==1)
                        {
                            SchCompList.Add(Comp3);
                        }
                    }

                    if (NewProd.GetComposition[i].NumComposant==4)
                    {
                        Comp4 =(Composition) NewProd.GetComposition[i].Clone();
                        if (NewProd.IsEnfilage==1)
                        {
                            SchCompList.Add(Comp4);
                        }
                    }

                    if (NewProd.GetComposition[i].NumComposant==5)
                    {
                        Comp5 =(Composition) NewProd.GetComposition[i].Clone();
                        if (NewProd.IsEnfilage==1)
                        {
                            SchCompList.Add(Comp5);
                        }
                    }

                    if (NewProd.GetComposition[i].NumComposant==6)
                    {
                        Comp6 =(Composition) NewProd.GetComposition[i].Clone();
                        if (NewProd.IsEnfilage==1)
                        {
                            SchCompList.Add(Comp6);
                        }
                    }

                    if (NewProd.GetComposition[i].NumComposant==7)
                    {
                        Comp7 =(Composition) NewProd.GetComposition[i].Clone();
                        if (NewProd.IsEnfilage==1)
                        {
                            SchCompList.Add(Comp7);
                        }
                    }
                }
                }
             
               
            }

            TotalPoids = 0;
            foreach (var compo in NewProd.GetComposition)
            {
                TotalPoids = TotalPoids + compo.Poids;
            }

            if (SelectedFicheTechnique.Produits[vers].EnfilageID != null)
            {
                if (SelectedFicheTechnique.Produits[vers].EnfilageID.GetChaine != null)
                {
                    SetupChaine(SelectedFicheTechnique.Produits[vers].EnfilageID.GetChaine.Colonne,
                        SelectedFicheTechnique.Produits[vers].EnfilageID.GetChaine.Ligne,
                        SelectedFicheTechnique.Produits[vers].EnfilageID.GetChaine.ChMatrix);
                
                }
                else

                {
                    ChaineList = new ObservableCollection<ChaineMatrixElement>();
                    ChaineList2 = new ObservableCollection<ChaineMatrixElement>();
                    
                    ChaineColumns = 0;
                    ChaineRows = 0;
                    ChaineRows2 = 0;
                    ChRowSum = 0;
                }
          
                List<MatrixElement> TempList = new List<MatrixElement>();
            
                foreach (var EleMatx in SelectedFicheTechnique.Produits[vers].EnfilageID.GetMatrix)
                {
                    MatrixElement EnfMatrix = new MatrixElement(EleMatx.x,EleMatx.y);
                
                    EnfMatrix.Content = EleMatx.value;
                    EnfMatrix.DentFil = EleMatx.DentFil;
                    TempList.Add(EnfMatrix);
                }

               
                TrameXposition= (Convert.ToInt32(Convert.ToDouble( SelectedFicheTechnique.Produits[vers].EnfilageID.TrXposition))-340).ToString();
                TrameYposition=Convert.ToInt32(Convert.ToDouble(SelectedFicheTechnique.Produits[vers].EnfilageID.TrYposition)).ToString();
                ContentEnfilageList = new MvxObservableCollection<MatrixElement>(TempList);
            }
            else
            {
                ChaineList = new ObservableCollection<ChaineMatrixElement>(); 
                
                ContentEnfilageList = new MvxObservableCollection<MatrixElement>();
                ChaineList2 = new ObservableCollection<ChaineMatrixElement>();
                    
                ChaineColumns = 0;
                ChaineRows = 0;
                ChaineRows2 = 0;
                ChRowSum = 0;
            }

            if (SelectedFicheTechnique.Catalog != null)
            {
                CategorieName = SelectedFicheTechnique.Catalog.Designation;
            }
            else
            {
                CategorieName = "";
            }
                

            
           
            }
            else
            {
                //SendNotification.Raise("Fiche Technique Vide");
            }
            
            
        }
        public MvxInteraction<LoadedImage> GetImagePath { get; } = new MvxInteraction<LoadedImage>();
        public MvxInteraction<YesNoQuestion> ConfirmAction { get; } = new MvxInteraction<YesNoQuestion>();

        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();

        public Produit NewProd
        {
            get => _NewProd;
            set
            {
                _NewProd = value;
                RaisePropertyChanged();
            }
        }

        public bool Part1Visible
        {
            get => _Part1Visible;
            set
            {
                _Part1Visible = value;
                RaisePropertyChanged();
            }
        }

        public bool Part2Visible
        {
            get => _Part2Visible;
            set
            {
                _Part2Visible = value;
                RaisePropertyChanged();
            }
        }

        

        public bool PrintBtn
        {
            get => _PrintBtn;
            set
            {
                _PrintBtn = value;
                RaisePropertyChanged();
            }
        }

        public bool SaveCancelBtn
        {
            get => _SaveCancelBtn;
            set
            {
                _SaveCancelBtn = value;
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

        public string Concepteur
        {
            get => _Concepteur;
            set
            {
                _Concepteur = value;
                RaisePropertyChanged();
            }
        }

        public string Verificateur
        {
            get => _Verificateur;
            set
            {
                _Verificateur = value;
                RaisePropertyChanged();
            }
        }

        private MvxObservableCollection<FicheTechnique> _FullFTByCatList;
        public MvxObservableCollection<FicheTechnique> FullFTByCatList
        {
            get => _FullFTByCatList;
            set
            {
                _FullFTByCatList = value;
                RaisePropertyChanged();
            }
        }
        private MvxObservableCollection<FicheTechnique> _FullFTList;
        public MvxObservableCollection<FicheTechnique> FullFTList
        {
            get => _FullFTList;
            set
            {
                _FullFTList = value;
                RaisePropertyChanged();
            }
        }

        private string _SearchText;

        public string SearchText
        {
            get
            {
                return _SearchText;
            }
            set
            {
                _SearchText = value;
                RaisePropertyChanged();
                FilterFicheTechniqueList();
            }
        }

        private string FullTextSearch;
       
        public void FilterFicheTechniqueList()
        {

            
               int ConvertedSearchT;
            if (int.TryParse(SearchText, out ConvertedSearchT))
            {
                
                 FicheTechniqueList = new MvxObservableCollection<FicheTechnique>();
                
                foreach (var ft in FullFTByCatList)
                {
                    if (ft.Produits[ft.Produits.Count - 1].Client != null && ft.Produits[ft.Produits.Count - 1].Name2!=null)
                    {
                        if(ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Name2.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Client.Name.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].NumArticle==ConvertedSearchT
                          )
                        {
                            FicheTechniqueList.Add(ft);
                        }
                    }else if (ft.Produits[ft.Produits.Count - 1].Client != null)
                    {
                        if(ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Client.Name.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].NumArticle==ConvertedSearchT
                          )
                        {
                            FicheTechniqueList.Add(ft);
                        }
                    }else if (ft.Produits[ft.Produits.Count - 1].Name2 != null)
                    {
                        if(ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Name2.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].NumArticle==ConvertedSearchT
                          )
                        {
                            FicheTechniqueList.Add(ft);
                        }
                    }
                    else
                    {
                        if(ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].NumArticle==ConvertedSearchT
                          )
                        {
                            FicheTechniqueList.Add(ft);
                        }
                    }
                }
            }
            else
            {
                FicheTechniqueList = new MvxObservableCollection<FicheTechnique>();
                
                foreach (var ft in FullFTByCatList)
                {
                    if (ft.Produits[ft.Produits.Count - 1].Client != null && ft.Produits[ft.Produits.Count - 1].Name2!=null)
                    {
                        if(ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Name2.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Client.Name.ToLower().Contains(SearchText.ToLower()))
                        {
                            FicheTechniqueList.Add(ft);
                        }
                    }else if (ft.Produits[ft.Produits.Count - 1].Client != null)
                    {
                        if(ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Client.Name.ToLower().Contains(SearchText.ToLower()))
                        {
                            FicheTechniqueList.Add(ft);
                        }
                    }else if (ft.Produits[ft.Produits.Count - 1].Name2 != null)
                    {
                        if(ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Name2.ToLower().Contains(SearchText.ToLower())
                           )
                        {
                            FicheTechniqueList.Add(ft);
                        }
                    }
                    else
                    {
                        if(ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                           || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                          )
                        {
                            FicheTechniqueList.Add(ft);
                        }
                    }
                }
            } 
            
            
            
        }
        public MvxObservableCollection<FicheTechnique> FicheTechniqueList
        {
            get => _FicheTechniqueList;
            set
            {
                _FicheTechniqueList = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsSearchByCat;

        public bool IsSearchByCat
        {
            get
            {
                return _IsSearchByCat;
                
            }
            set
            {
                _IsSearchByCat = value;
                RaisePropertyChanged();
                if (value == false)
                {
                    SelectedCatSearch = null;
                }
            }
        }
        public bool EnableEditing
        {
            get => _EnableEditing;
            set
            {
                _EnableEditing = value;
                RaisePropertyChanged();
            }
        }

    

        public MvxObservableCollection<Duitages> DuitageList
        {
            get => _DuitageList;
            set
            {
                _DuitageList = value;
                RaisePropertyChanged();
            }
        }

        public Duitages SelectedDuitage
        {
            get => _SelectedDuitage;
            set
            {
                _SelectedDuitage = value;
                RaisePropertyChanged();
                SetupVitesse();
            }
        }

        public MvxObservableCollection<Machine> MachineList
        {
            get => _MachineList;
            set
            {
                _MachineList = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Composant> ListComposant
        {
            get => _ListComposant;
            set
            {
                _ListComposant = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<TypeMatiere> ListTypeMatiere
        {
            get => _ListTypeMatiere;
            set
            {
                _ListTypeMatiere = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Titrage> ListTitrage1
        {
            get => _ListTitrage1;
            set
            {
                _ListTitrage1 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Titrage> ListTitrage2
        {
            get
            {
                return _ListTitrage2;
                RaisePropertyChanged();
            }
            set
            {
                _ListTitrage2 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Titrage> ListTitrage3
        {
            get => _ListTitrage3;
            set
            {
                _ListTitrage3 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Titrage> ListTitrage4
        {
            get => _ListTitrage4;
            set
            {
                _ListTitrage4 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Titrage> ListTitrage5
        {
            get => _ListTitrage5;
            set
            {
                _ListTitrage5 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Titrage> ListTitrage6
        {
            get => _ListTitrage6;
            set
            {
                _ListTitrage6 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Titrage> ListTitrage7
        {
            get => _ListTitrage7;
            set
            {
                _ListTitrage7 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Titrage> ListTitrage8
        {
            get => _ListTitrage8;
            set
            {
                _ListTitrage8 = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Titrage> ListTitrage9
        {
            get => _ListTitrage9;
            set
            {
                _ListTitrage9 = value;
                RaisePropertyChanged();
            }
        }
        private MvxObservableCollection<Couleur> _ListCouleur1;

        public MvxObservableCollection<Couleur> ListCouleur1
        {
            get => _ListCouleur1;
            set
            {
                _ListCouleur1 = value;
                RaisePropertyChanged();
            }
        }

        private MvxObservableCollection<Couleur> _ListCouleur2;
        private MvxObservableCollection<Couleur> _ListCouleur3;
        private MvxObservableCollection<Couleur> _ListCouleur4;
        private MvxObservableCollection<Couleur> _ListCouleur5;
        private MvxObservableCollection<Couleur> _ListCouleur6;
        private MvxObservableCollection<Couleur> _ListCouleur7;
        private MvxObservableCollection<Couleur> _ListCouleur8;
        private MvxObservableCollection<Couleur> _ListCouleur9;
        
        public bool EditDate
        {
            get => _EditDate;
            set
            {
                _EditDate = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _CreationDisplayDate;

        public DateTime CreationDisplayDate
        {
            get
            {
                return _CreationDisplayDate;
            }
            set
            {
                _CreationDisplayDate=value;
                RaisePropertyChanged();
            }
        }

        public bool DisplayDate
        {
            get => _DisplayDate;
            set
            {
                _DisplayDate = value;
                RaisePropertyChanged();
            }
        }

        public Titrage Comp1Titrage
        {
            get => _Comp1Titrage;
            set
            {
                _Comp1Titrage = value;
                RaisePropertyChanged();
            }
        }

        public string PageNumber
        {
            get => _PageNumber;
            set
            {
                _PageNumber = value;
                RaisePropertyChanged();
            }
        }

        public bool IsNextPage
        {
            get => _IsNextPage;
            set
            {
                _IsNextPage = value;
                RaisePropertyChanged();
            }
        }

        public bool IsPrevPage
        {
            get => _IsPrevPage;
            set
            {
                _IsPrevPage = value;
                RaisePropertyChanged();
            }
        }

        private MvxObservableCollection<Redacteur> _ListRedacteur;
        public MvxObservableCollection<Redacteur> ListRedacteur
        {
            get => _ListRedacteur;
            set
            {
                _ListRedacteur = value;
                RaisePropertyChanged();
            }
        }
        public MvxObservableCollection<Concepteur> ListConcepteur
        {
            get => _ListConcepteur;
            set
            {
                _ListConcepteur = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Verificateur> ListVerificateur
        {
            get => _ListVerificateur;
            set
            {
                _ListVerificateur = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Client> ListClient
        {
            get => _ListClient;
            set
            {
                _ListClient = value;
                RaisePropertyChanged();
            }
        }

        private string _ImageProd;

        public string ImageProd
        {
            get
            {
                return _ImageProd;
            }
            set
            {
                _ImageProd = value;
                RaisePropertyChanged();
            }
        }
        private IMvxCommand _CmdSelectImage;

        public IMvxCommand CmdSelectImage
        {
            get
            {
                _CmdSelectImage = new MvxCommand(SelectImage);
                return _CmdSelectImage;
            }
        }

        public void SelectImage()
        {
            var req = new LoadedImage()
            {
                UploadCallback = (imageP,imageN,ok) =>
                {

                    if (ok)
                    {
                        string CurPath =
                            System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule
                                .FileName)+"\\ImgProd\\";
                        Directory.CreateDirectory(CurPath);
                        if (File.Exists(CurPath + imageN) == false)
                        {
                            File.Copy(imageP, CurPath +imageN);
                        }
                        
                        ImageProd =CurPath+ imageN;
                        NewProd.image = imageN;
                        _DB2.UpdateProdImage(NewProd);
                    }
                    
                }
            };
            GetImagePath.Raise(req);
        }

        private IMvxCommand _ValiderChaine;
        
        
        private IMvxCommand _SecondPageCmd;

        public IMvxCommand SecondPageCmd
        {
            get
            {
                _SecondPageCmd = new MvxCommand(ShowSecondPart);
                return _SecondPageCmd;
            }
        }
        
        private IMvxCommand _FirstPageCmd;

        public IMvxCommand FirstPageCmd
        {
            get
            {
                _FirstPageCmd = new MvxCommand(ShowFirstPart);
                return _FirstPageCmd;
            }
        }
       
        private IMvxCommand _CmdCancelFicheTek;

        public IMvxCommand CmdCancelFicheTek
        {
            get
            {
                _CmdCancelFicheTek = new MvxCommand(DeleteBeforeCancel);
                return _CmdCancelFicheTek;
            }
        }

        public void DeleteBeforeCancel()
        {
            if (Editing == false)
            {
                if (NewProd.IsEnfilage == 1)
                {
                    _DB2.DeleteFicheTechnique(NewProd.FicheId,NewProd.Id,NewProd.EnfilageID.ID);
                }
                else
                {
                    _DB2.DeleteFicheTechnique(NewProd.FicheId,NewProd.Id);
                }
                
                
            }
            else
            {
                Editing = true;
            }

            
            AnnulerFicheTechnique();
            UpdateFicheTek();
            SelectedFicheTechnique = null;
        }

        public void DisplayPrintView()
        {
            _NavigationService.Navigate<PrintViewModel, Produit>(NewProd);
        }
        
        private IMvxCommand _PrintCmd;

        public IMvxCommand PrintCmd
        {
            get
            {
                _PrintCmd = new MvxCommand(DisplayPrintView);
                return _PrintCmd;
            }
        }

        private IMvxCommand _CmdAddNewVersion;

        public IMvxCommand CmdAddNewVersion
        {
            get
            {
                _CmdAddNewVersion = new MvxCommand(AddNewVersion);
               return _CmdAddNewVersion;
            }
          
        }


        private bool _IsDateCr=true;

        public bool IsDateCr
        {
            get
            {
                return _IsDateCr;
            }
            set
            {
                _IsDateCr = value;
                RaisePropertyChanged();
            }
        }
        public void AddNewVersion()
        {
            UIServices.SetBusyState();
            SchCompList = new MvxObservableCollection<Composition>();
             if (SelectedFicheTechnique != null)
             {
                 IsProduction = true;
                 IsDateCr = false;
                 IsUpdate = true;
                 MinVers = 1;
               int LastVersion= SelectedFicheTechnique.Produits.Count - 1;
               
               
               NewProd.Id = 0;
               NewProd.Version = NewProd.Version + 1;
               var prod = (Produit)NewProd.Clone();
               
               OldVersion = NewProd.Version;
                NewProd.Id= _DB2.AddNewProductVersion(prod);
                if (prod.Client != null)
                {
                    _DB2.UpdateProdClient(prod);
                    NewProd.Client = ListClient.SingleOrDefault(cl => cl.ID == prod.Client.ID);
                }
                if (prod.Concepteur != null)
                {
                    _DB2.UpdateProdConcepteur(prod);
                    NewProd.Concepteur = ListConcepteur.SingleOrDefault(cl => cl.ID == prod.Concepteur.ID);
                }
                if (prod.Verificateur != null)
                {
                    _DB2.UpdateProdVerificateur(prod);
                    NewProd.Verificateur = ListVerificateur.SingleOrDefault(cl => cl.ID == prod.Verificateur.ID);
                }
                if (prod.RedacteurObj != null)
                {
                    _DB2.UpdateProdRedacteur(prod);
                    NewProd.RedacteurObj = ListRedacteur.SingleOrDefault(cl => cl.ID == prod.RedacteurObj.ID);
                }
                if (prod.PeigneObj != null)
                {
                    _DB2.UpdateProdPeigne(prod);
                    NewProd.PeigneObj = ListPeigne.SingleOrDefault(cl => cl.ID == prod.PeigneObj.ID);
                }
                if (prod.DuitageID  != null)
                {
                    if (SelectedFicheTechnique.Produits[LastVersion].DuitageID.Machine!=null)
                    {
                        _DB2.UpdateProdDuitage(prod);
                        SelectedMachine = MachineList.SingleOrDefault(ma =>
                            ma.ID == SelectedFicheTechnique.Produits[LastVersion].DuitageID.Machine.ID);
                        NewProd.DuitageID = DuitageList.SingleOrDefault(cl => cl.ID == prod.DuitageID.ID);
                    } 
                }

                if (prod.EnfilageID != null)
                {
                    _DB2.UpdateProdEnfilage(prod);
                }
               
                if (prod.Epaisseur>0)
                {
                    _DB2.UpdateProdEpaiseur(prod);
                }

                if (SelectedFicheTechnique.Catalog != null)
                {
                    SelectedCategorie =
                        CategorieList.SingleOrDefault(cat => cat.ID == SelectedFicheTechnique.Catalog.ID);
                }
                    
                    
                NewProd.PropertyChanged += Prod_PropertyChanged;
                //NewProd.MiseAJour=DateTime.Now;
                IsDentFilVis = true;
                EditDate = true;
                DisplayDate = false;
                EnableEditing = true;
                SaveCancelBtn = true;
                PrintBtn = false;
                BtnVis = true;
                IsAddEnabled = false;
                

                if (NewProd.DuitageID != null)
                {
                    Vitesse= NewProd.DuitageID.Vitesse.ToString();
                }

                List<EquivalentID> EquivList = new List<EquivalentID>();
                
                List<Composition> NewTemp =new List<Composition>(NewProd.GetComposition.Count) ;
                NewProd.GetComposition.ForEach((item) =>
                {
                    NewTemp.Add((Composition)item.Clone());
                });
                 for(int i=0;i<NewTemp.Count;i++)
            {
                if (NewTemp[i].GetComposant != null)
                {
                       if (NewTemp[i].NumComposant==8)
                {
                    Comp8Vis = true;
                    ChangeImageBtn8 = "../Asset/remove64.png";
                    
                    
                     Comp8 =(Composition) NewTemp[i].Clone();
                     EquivalentID eqid = new EquivalentID();
                     eqid.Id = Comp8.ID;

                     Comp8.ProdID =(Produit)NewProd.Clone();
                    Comp8.ID = 0;
                    Comp8.ID= _DB2.AddProdCompo((Composition)Comp8.Clone());
                    eqid.EquivId = Comp8.ID;
                    EquivList.Add(eqid);
                    if (NewTemp[i].GetMatiere != null)
                    {
                        _DB2.UpdateCompoMatiere(Comp8);
                        SelectedTypeMatiere8= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp8.GetMatiere.Titrage.TypeMatiere.ID);
                        SelectedTitrage8=ListTitrage8.SingleOrDefault(tit => tit.ID == Comp8.GetMatiere.Titrage.ID);
                        SelectedColor8=ListCouleur8.SingleOrDefault(co => co.ID == Comp8.GetMatiere.GetCouleur.ID);
                    }
                    if (NewTemp[i].GetComposant != null)
                    {
                        _DB2.UpdateCompoComposant(Comp8);
                        
                        Comp8.GetComposant =
                            ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                    }
                    
                    Comp8.PropertyChanged += Comp8_PropertyChanged;

                }
                else if (NewTemp[i].NumComposant==9)
                {
                    Comp9Vis = true;
                    ChangeImageBtn9 = "../Asset/remove64.png";
                    

                    
                    Comp9 =(Composition) NewTemp[i].Clone();
                    EquivalentID eqid = new EquivalentID();
                    eqid.Id = Comp9.ID;
                    Comp9.ProdID =(Produit)NewProd.Clone();
                    Comp9.ID = 0;
                    Comp9.ID= _DB2.AddProdCompo((Composition)Comp9.Clone());
                    eqid.EquivId = Comp9.ID;
                    EquivList.Add(eqid);
                    if (NewTemp[i].GetMatiere != null)
                    {
                        _DB2.UpdateCompoMatiere(Comp9);
                        SelectedTypeMatiere9= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp9.GetMatiere.Titrage.TypeMatiere.ID);
                        SelectedTitrage9=ListTitrage9.SingleOrDefault(tit => tit.ID == Comp9.GetMatiere.Titrage.ID);
                        SelectedColor9=ListCouleur9.SingleOrDefault(co => co.ID == Comp9.GetMatiere.GetCouleur.ID);
                    }
                    if (NewTemp[i].GetComposant != null)
                    {
                        _DB2.UpdateCompoComposant(Comp9);
                        Comp9.GetComposant =
                            ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                    }
                    Comp9.PropertyChanged += Comp9_PropertyChanged;
                }
                else
                {
                    if (NewTemp[i].NumComposant==1)
                    {
                      List<Composition> TempList= _Db.GetCompositions();
                        Comp1Vis = true;
                        ChangeImageBtn1 = "../Asset/remove64.png";
                      

                       
                         Comp1 =(Composition) NewTemp[i].Clone();
                         EquivalentID eqid = new EquivalentID();
                         eqid.Id = Comp1.ID;
                         Comp1.ProdID =(Produit)NewProd.Clone();
                         Comp1.ID = 0;
                         Comp1.ID= _DB2.AddProdCompo((Composition)Comp1.Clone());
                         eqid.EquivId = Comp1.ID;
                         EquivList.Add(eqid);
                         if (NewTemp[i].GetMatiere != null)
                         {
                             SelectedTypeMatiere1= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp1.GetMatiere.Titrage.TypeMatiere.ID);
                             SelectedTitrage1=ListTitrage1.SingleOrDefault(tit => tit.ID == Comp1.GetMatiere.Titrage.ID);
                             SelectedColor1=ListCouleur1.SingleOrDefault(co => co.ID == Comp1.GetMatiere.GetCouleur.ID);
                             _DB2.UpdateCompoMatiere(Comp1);
                         }
                         if (NewTemp[i].GetComposant != null)
                         {
                             _DB2.UpdateCompoComposant(Comp1);
                             Comp1.GetComposant =
                                 ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                         }

                         if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                         {
                             SchCompList.Add(Comp1);
                         }

                         
                         Comp1.PropertyChanged += Comp1_PropertyChanged;
                    }

                    if (NewTemp[i].NumComposant==2)
                    {
                        Comp2Vis = true;
                        ChangeImageBtn2 = "../Asset/remove64.png";
                       

                        
                        Comp2 =(Composition) NewTemp[i].Clone();
                        EquivalentID eqid = new EquivalentID();
                        eqid.Id = Comp2.ID;
                        Comp2.ProdID =(Produit) NewProd.Clone();
                        Comp2.ID = 0;
                        Comp2.ID= _DB2.AddProdCompo((Composition)Comp2.Clone());
                        eqid.EquivId = Comp2.ID;
                        EquivList.Add(eqid);
                        if (NewTemp[i].GetMatiere != null)
                        {
                            SelectedTypeMatiere2= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp2.GetMatiere.Titrage.TypeMatiere.ID);
                            SelectedTitrage2=ListTitrage2.SingleOrDefault(tit => tit.ID == Comp2.GetMatiere.Titrage.ID);
                            SelectedColor2=ListCouleur2.SingleOrDefault(co => co.ID == Comp2.GetMatiere.GetCouleur.ID);
                            _DB2.UpdateCompoMatiere(Comp2);
                        }
                        if (NewTemp[i].GetComposant != null)
                        {
                            _DB2.UpdateCompoComposant(Comp2);
                            Comp2.GetComposant =
                                ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                        }
                        if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                        {
                            SchCompList.Add(Comp2);
                        }
                        Comp2.PropertyChanged += Comp2_PropertyChanged;
                    }

                    if (NewTemp[i].NumComposant==3)
                    {
                        Comp3Vis = true;
                        ChangeImageBtn3 = "../Asset/remove64.png";
                        

                      
                        Comp3 =(Composition) NewTemp[i].Clone();
                        EquivalentID eqid = new EquivalentID();
                        eqid.Id = Comp3.ID;
                        Comp3.ProdID =(Produit) NewProd.Clone();
                        Comp3.ID = 0;
                        Comp3.ID= _DB2.AddProdCompo((Composition)Comp3.Clone());
                        eqid.Id = Comp3.ID;
                        EquivList.Add(eqid);
                        if (NewTemp[i].GetMatiere != null)
                        {
                            SelectedTypeMatiere3= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp3.GetMatiere.Titrage.TypeMatiere.ID);
                            SelectedTitrage3=ListTitrage3.SingleOrDefault(tit => tit.ID == Comp3.GetMatiere.Titrage.ID);
                            SelectedColor3=ListCouleur3.SingleOrDefault(co => co.ID == Comp3.GetMatiere.GetCouleur.ID);
                            _DB2.UpdateCompoMatiere(Comp3);
                        }
                        if (NewTemp[i].GetComposant != null)
                        {
                            _DB2.UpdateCompoComposant(Comp3);
                            Comp3.GetComposant =
                                ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                        }
                        if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                        {
                            SchCompList.Add(Comp3);
                        }
                        Comp3.PropertyChanged += Comp3_PropertyChanged;
                    }

                    if (NewTemp[i].NumComposant==4)
                    {
                        Comp4Vis = true;
                        ChangeImageBtn4 = "../Asset/remove64.png";
                      

                       
                        Comp4 =(Composition) NewTemp[i].Clone();
                        EquivalentID eqid = new EquivalentID();
                        eqid.Id = Comp4.ID;
                        Comp4.ProdID =(Produit) NewProd.Clone();
                        Comp4.ID = 0;
                        Comp4.ID= _DB2.AddProdCompo((Composition)Comp4.Clone());
                        eqid.EquivId = Comp4.ID;
                        EquivList.Add(eqid);
                        if (NewTemp[i].GetMatiere != null)
                        {
                            SelectedTypeMatiere4= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp4.GetMatiere.Titrage.TypeMatiere.ID);
                            SelectedTitrage4=ListTitrage4.SingleOrDefault(tit => tit.ID == Comp4.GetMatiere.Titrage.ID);
                            SelectedColor4=ListCouleur4.SingleOrDefault(co => co.ID == Comp4.GetMatiere.GetCouleur.ID);
                            _DB2.UpdateCompoMatiere(Comp4);
                        }
                        if (NewTemp[i].GetComposant != null)
                        {
                            _DB2.UpdateCompoComposant(Comp4);
                            Comp4.GetComposant =
                                ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                        }
                        if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                        {
                            SchCompList.Add(Comp4);
                        }
                        Comp4.PropertyChanged += Comp4_PropertyChanged;
                    }

                    if (NewTemp[i].NumComposant==5)
                    {
                        Comp5Vis = true;
                        ChangeImageBtn5 = "../Asset/remove64.png";
                        

                       
                        Comp5 =(Composition) NewTemp[i].Clone();
                        EquivalentID eqid = new EquivalentID();
                        eqid.Id = Comp5.ID;
                        Comp5.ProdID =(Produit) NewProd.Clone();
                        Comp5.ID = 0;
                        Comp5.ID= _DB2.AddProdCompo((Composition)Comp5.Clone());
                        eqid.EquivId = Comp5.ID;
                        EquivList.Add(eqid);
                        if (NewTemp[i].GetMatiere != null)
                        {
                            SelectedTypeMatiere5= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp5.GetMatiere.Titrage.TypeMatiere.ID);
                            SelectedTitrage5=ListTitrage5.SingleOrDefault(tit => tit.ID == Comp5.GetMatiere.Titrage.ID);
                            SelectedColor5=ListCouleur5.SingleOrDefault(co => co.ID == Comp5.GetMatiere.GetCouleur.ID);
                            _DB2.UpdateCompoMatiere(Comp5);
                        }
                        if (NewTemp[i].GetComposant != null)
                        {
                            _DB2.UpdateCompoComposant(Comp5);
                            Comp5.GetComposant =
                                ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                        }
                        if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                        {
                            SchCompList.Add(Comp5);
                        }
                        Comp5.PropertyChanged += Comp5_PropertyChanged;
                    }

                    if (NewTemp[i].NumComposant==6)
                    {
                        Comp6Vis = true;
                        ChangeImageBtn6 = "../Asset/remove64.png";
                        

                       
                        Comp6 =(Composition) NewTemp[i].Clone();
                        EquivalentID eqid = new EquivalentID();
                        eqid.Id = Comp6.ID;
                        Comp6.ProdID =(Produit) NewProd.Clone();
                        Comp6.ID = 0;
                        Comp6.ID= _DB2.AddProdCompo((Composition)Comp6.Clone());
                        eqid.EquivId = Comp6.ID;
                        EquivList.Add(eqid);
                        if (NewTemp[i].GetMatiere != null)
                        {
                            SelectedTypeMatiere6= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp6.GetMatiere.Titrage.TypeMatiere.ID);
                            SelectedTitrage6=ListTitrage6.SingleOrDefault(tit => tit.ID == Comp6.GetMatiere.Titrage.ID);
                            SelectedColor6=ListCouleur6.SingleOrDefault(co => co.ID == Comp6.GetMatiere.GetCouleur.ID);
                            _DB2.UpdateCompoMatiere(Comp6);
                        }
                        if (NewTemp[i].GetComposant != null)
                        {
                            _DB2.UpdateCompoComposant(Comp6);
                            Comp6.GetComposant =
                                ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                        }
                        if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                        {
                            SchCompList.Add(Comp6);
                        }
                        Comp6.PropertyChanged += Comp6_PropertyChanged;
                    }

                    if (NewTemp[i].NumComposant==7)
                    {
                        Comp7Vis = true;
                        ChangeImageBtn7 = "../Asset/remove64.png";
                        

                        
                        Comp7 =(Composition) NewTemp[i].Clone();
                        EquivalentID eqid = new EquivalentID();
                        eqid.Id = Comp7.ID;
                        Comp7.ProdID =(Produit) NewProd.Clone();
                        Comp7.ID = 0;
                        Comp7.ID= _DB2.AddProdCompo((Composition)Comp7.Clone());
                        eqid.EquivId = Comp7.ID;
                        EquivList.Add(eqid);
                        if (NewTemp[i].GetMatiere != null)
                        {
                            SelectedTypeMatiere7= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp7.GetMatiere.Titrage.TypeMatiere.ID);
                            SelectedTitrage7=ListTitrage7.SingleOrDefault(tit => tit.ID == Comp7.GetMatiere.Titrage.ID);
                            SelectedColor7=ListCouleur7.SingleOrDefault(co => co.ID == Comp7.GetMatiere.GetCouleur.ID);
                            _DB2.UpdateCompoMatiere(Comp7);
                        }
                        if (NewTemp[i].GetComposant != null)
                        {
                            _DB2.UpdateCompoComposant(Comp7);
                            Comp7.GetComposant =
                                ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                        }
                        if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                        {
                            SchCompList.Add(Comp7);
                        }
                        Comp7.PropertyChanged += Comp7_PropertyChanged;
                    }
                }
                }

              
            }


                 if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
               {
                   if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine != null)
                   {
                       SelectedChaine = ChaineNameList.SingleOrDefault(ch =>
                           ch.ID == SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine.ID);
                   }
                   if (IsEnfilage)
                   {
                       NewProd.IsEnfilage = 1;
                       NewProd.EnfilageID = new Enfilage();
                       NewProd.EnfilageID.Column = EnfilageColumns;
                       NewProd.EnfilageID.Row  = EnfilageRow;
                       NewProd.EnfilageID.TrXposition =SelectedFicheTechnique.Produits[LastVersion].EnfilageID.TrXposition;
                       NewProd.EnfilageID.TrYposition = SelectedFicheTechnique.Produits[LastVersion].EnfilageID.TrYposition;
                       if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine != null)
                       {
                           NewProd.EnfilageID.GetChaine = SelectedChaine;
                       }

                       NewProd.EnfilageID.ID= _DB2.AddNewEnfilage(NewProd.EnfilageID);
                   }
                   else
                   {
                       NewProd.IsEnfilage = 0;
                   }

                   
                   foreach (var EleMatx in SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetMatrix)
                   {
                       int LastMatrixID = _DB2.GetLatestEnfilageMatrixID();
                       EnfilageMatrix EnfMatrix = new EnfilageMatrix();
                       EnfMatrix.ID = LastMatrixID;
                       EnfMatrix.x = EleMatx.x ;
                       EnfMatrix.y = EleMatx.y;
                       EnfMatrix.DentFil = EleMatx.DentFil;
                       var eqObj= EquivList.SingleOrDefault(eq => eq.Id == EleMatx.value.ID);
                       
                       EnfMatrix.value =(Composition)EleMatx.value;
                       EnfMatrix.value.ID = eqObj.EquivId;
                       EnfMatrix.Enf = NewProd.EnfilageID;
                       _DB2.AddNewEnfilageMatrix(EnfMatrix);
                       
                   }
               }
            }
            else
            {
                SendNotification.Raise("Aucune Fiche Technique Séléctionnée");
            }
        }
        private IMvxCommand _CmdConfirmer;

        public IMvxCommand CmdConfirmer
        {
            get
            {
                _CmdConfirmer = new MvxCommand(ConfirmFT);
                return _CmdConfirmer;
            }
        }

        public void ConfirmFT()
        {
               UIServices.SetBusyState();
               
             if (SelectedFicheTechnique != null)
             {
                 int SaveFTID = SelectedFicheTechnique.ID;
                 IsProduction = true;
                 IsDateCr = false;
                 IsUpdate = true;
                 MinVers = 1;
               int LastVersion= SelectedFicheTechnique.Produits.Count - 1;
               
               
               NewProd.Id = 0;
               NewProd.Version = NewProd.Version + 1;
               var prod = (Produit)NewProd.Clone();
               
               OldVersion = NewProd.Version;
                NewProd.Id= _DB2.AddNewProductVersion(prod);
                if (prod.Client != null)
                {
                    _DB2.UpdateProdClient(prod);
                }
                if (prod.Concepteur != null)
                {
                    _DB2.UpdateProdConcepteur(prod);
                }
                if (prod.Verificateur != null)
                {
                    _DB2.UpdateProdVerificateur(prod);
                }
                if (prod.RedacteurObj != null)
                {
                    _DB2.UpdateProdRedacteur(prod);
                }
                if (prod.PeigneObj != null)
                {
                    _DB2.UpdateProdPeigne(prod);
                }
                if (prod.DuitageID  != null)
                {
                    if (SelectedFicheTechnique.Produits[LastVersion].DuitageID.Machine!=null)
                    {
                        _DB2.UpdateProdDuitage(prod);
                    } 
                }

                if (prod.EnfilageID != null)
                {
                    _DB2.UpdateProdEnfilage(prod);
                }
               
                if (prod.Epaisseur>0)
                {
                    _DB2.UpdateProdEpaiseur(prod);
                }

                

                List<EquivalentID> EquivList = new List<EquivalentID>();
                
                List<Composition> NewTemp =new List<Composition>(NewProd.GetComposition.Count) ;
                NewProd.GetComposition.ForEach((item) =>
                {
                    NewTemp.Add((Composition)item.Clone());
                });
                 for(int i=0;i<NewTemp.Count;i++)
            {
                if (NewTemp[i].GetComposant != null)
                {
                       if (NewTemp[i].NumComposant==8)
                {
                    
                    
                     Comp8 =(Composition) NewTemp[i].Clone();
                     EquivalentID eqid = new EquivalentID();
                     eqid.Id = Comp8.ID;

                     Comp8.ProdID =(Produit)NewProd.Clone();
                    Comp8.ID = 0;
                    Comp8.ID= _DB2.AddProdCompo((Composition)Comp8.Clone());
                    eqid.EquivId = Comp8.ID;
                    EquivList.Add(eqid);
                    if (NewTemp[i].GetMatiere != null)
                    {
                        _DB2.UpdateCompoMatiere(Comp8);
                    }
                    if (NewTemp[i].GetComposant != null)
                    {
                        _DB2.UpdateCompoComposant(Comp8);
                        
                        Comp8.GetComposant =
                            ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                    }
                    

                }
                else if (NewTemp[i].NumComposant==9)
                {
                   
                    
                    Comp9 =(Composition) NewTemp[i].Clone();
                    EquivalentID eqid = new EquivalentID();
                    eqid.Id = Comp9.ID;
                    Comp9.ProdID =(Produit)NewProd.Clone();
                    Comp9.ID = 0;
                    Comp9.ID= _DB2.AddProdCompo((Composition)Comp9.Clone());
                    eqid.EquivId = Comp9.ID;
                    EquivList.Add(eqid);
                    if (NewTemp[i].GetMatiere != null)
                    {
                        _DB2.UpdateCompoMatiere(Comp9);
                    }
                    if (NewTemp[i].GetComposant != null)
                    {
                        _DB2.UpdateCompoComposant(Comp9);
                        Comp9.GetComposant =
                            ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                    }
                }
                else
                {
                    if (NewTemp[i].NumComposant==1)
                    {
                        Comp1 =(Composition) NewTemp[i].Clone();
                         EquivalentID eqid = new EquivalentID();
                         eqid.Id = Comp1.ID;
                         Comp1.ProdID =(Produit)NewProd.Clone();
                         Comp1.ID = 0;
                         Comp1.ID= _DB2.AddProdCompo((Composition)Comp1.Clone());
                         eqid.EquivId = Comp1.ID;
                         EquivList.Add(eqid);
                         if (NewTemp[i].GetMatiere != null)
                         {
                     
                             _DB2.UpdateCompoMatiere(Comp1);
                         }
                         if (NewTemp[i].GetComposant != null)
                         {
                             _DB2.UpdateCompoComposant(Comp1);
                             Comp1.GetComposant =
                                 ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                         }

                        

                    }

                    if (NewTemp[i].NumComposant==2)
                    {
                        Comp2 =(Composition) NewTemp[i].Clone();
                        EquivalentID eqid = new EquivalentID();
                        eqid.Id = Comp2.ID;
                        Comp2.ProdID =(Produit) NewProd.Clone();
                        Comp2.ID = 0;
                        Comp2.ID= _DB2.AddProdCompo((Composition)Comp2.Clone());
                        eqid.EquivId = Comp2.ID;
                        EquivList.Add(eqid);
                        if (NewTemp[i].GetMatiere != null)
                        {
                           
                            _DB2.UpdateCompoMatiere(Comp2);
                        }
                        if (NewTemp[i].GetComposant != null)
                        {
                            _DB2.UpdateCompoComposant(Comp2);
                            Comp2.GetComposant =
                                ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                        }
                    
                    }

                    if (NewTemp[i].NumComposant==3)
                    {
                       
                        Comp3 =(Composition) NewTemp[i].Clone();
                        EquivalentID eqid = new EquivalentID();
                        eqid.Id = Comp3.ID;
                        Comp3.ProdID =(Produit) NewProd.Clone();
                        Comp3.ID = 0;
                        Comp3.ID= _DB2.AddProdCompo((Composition)Comp3.Clone());
                        eqid.Id = Comp3.ID;
                        EquivList.Add(eqid);
                        if (NewTemp[i].GetMatiere != null)
                        {
                            SelectedTypeMatiere3= ListTypeMatiere.SingleOrDefault(ma => ma.ID==Comp3.GetMatiere.Titrage.TypeMatiere.ID);
                            SelectedTitrage3=ListTitrage3.SingleOrDefault(tit => tit.ID == Comp3.GetMatiere.Titrage.ID);
                            SelectedColor3=ListCouleur3.SingleOrDefault(co => co.ID == Comp3.GetMatiere.GetCouleur.ID);
                            _DB2.UpdateCompoMatiere(Comp3);
                        }
                        if (NewTemp[i].GetComposant != null)
                        {
                            _DB2.UpdateCompoComposant(Comp3);
                            Comp3.GetComposant =
                                ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                        }
                      
                    }

                    if (NewTemp[i].NumComposant==4)
                    {
                        Comp4 =(Composition) NewTemp[i].Clone();
                        EquivalentID eqid = new EquivalentID();
                        eqid.Id = Comp4.ID;
                        Comp4.ProdID =(Produit) NewProd.Clone();
                        Comp4.ID = 0;
                        Comp4.ID= _DB2.AddProdCompo((Composition)Comp4.Clone());
                        eqid.EquivId = Comp4.ID;
                        EquivList.Add(eqid);
                        if (NewTemp[i].GetMatiere != null)
                        {
                          
                            _DB2.UpdateCompoMatiere(Comp4);
                        }
                        if (NewTemp[i].GetComposant != null)
                        {
                            _DB2.UpdateCompoComposant(Comp4);
                            Comp4.GetComposant =
                                ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                        }
                     
                    }

                    if (NewTemp[i].NumComposant==5)
                    {
                        Comp5 =(Composition) NewTemp[i].Clone();
                        EquivalentID eqid = new EquivalentID();
                        eqid.Id = Comp5.ID;
                        Comp5.ProdID =(Produit) NewProd.Clone();
                        Comp5.ID = 0;
                        Comp5.ID= _DB2.AddProdCompo((Composition)Comp5.Clone());
                        eqid.EquivId = Comp5.ID;
                        EquivList.Add(eqid);
                        if (NewTemp[i].GetMatiere != null)
                        {
                           
                            _DB2.UpdateCompoMatiere(Comp5);
                        }
                        if (NewTemp[i].GetComposant != null)
                        {
                            _DB2.UpdateCompoComposant(Comp5);
                            Comp5.GetComposant =
                                ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                        }
                       
                    }

                    if (NewTemp[i].NumComposant==6)
                    {
                        Comp6 =(Composition) NewTemp[i].Clone();
                        EquivalentID eqid = new EquivalentID();
                        eqid.Id = Comp6.ID;
                        Comp6.ProdID =(Produit) NewProd.Clone();
                        Comp6.ID = 0;
                        Comp6.ID= _DB2.AddProdCompo((Composition)Comp6.Clone());
                        eqid.EquivId = Comp6.ID;
                        EquivList.Add(eqid);
                        if (NewTemp[i].GetMatiere != null)
                        {
                            _DB2.UpdateCompoMatiere(Comp6);
                        }
                        if (NewTemp[i].GetComposant != null)
                        {
                            _DB2.UpdateCompoComposant(Comp6);
                            Comp6.GetComposant =
                                ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                        }
                      
                    }

                    if (NewTemp[i].NumComposant==7)
                    {
                        Comp7 =(Composition) NewTemp[i].Clone();
                        EquivalentID eqid = new EquivalentID();
                        eqid.Id = Comp7.ID;
                        Comp7.ProdID =(Produit) NewProd.Clone();
                        Comp7.ID = 0;
                        Comp7.ID= _DB2.AddProdCompo((Composition)Comp7.Clone());
                        eqid.EquivId = Comp7.ID;
                        EquivList.Add(eqid);
                        if (NewTemp[i].GetMatiere != null)
                        {
                            _DB2.UpdateCompoMatiere(Comp7);
                        }
                        if (NewTemp[i].GetComposant != null)
                        {
                            _DB2.UpdateCompoComposant(Comp7);
                            Comp7.GetComposant =
                                ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                        }
                       
                    }
                }
                }

           
            }


                 if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
               {
                   if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine != null)
                   {
                       SelectedChaine = ChaineNameList.SingleOrDefault(ch =>
                           ch.ID == SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine.ID);
                   }
                   if (IsEnfilage)
                   {
                       NewProd.IsEnfilage = 1;
                       NewProd.EnfilageID = new Enfilage();
                       NewProd.EnfilageID.Column = EnfilageColumns;
                       NewProd.EnfilageID.Row  = EnfilageRow;
                       NewProd.EnfilageID.TrXposition =SelectedFicheTechnique.Produits[LastVersion].EnfilageID.TrXposition;
                       NewProd.EnfilageID.TrYposition = SelectedFicheTechnique.Produits[LastVersion].EnfilageID.TrYposition;
                       if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine != null)
                       {
                           NewProd.EnfilageID.GetChaine = SelectedChaine;
                       }

                       NewProd.EnfilageID.ID= _DB2.AddNewEnfilage(NewProd.EnfilageID);
                   }
                   else
                   {
                       NewProd.IsEnfilage = 0;
                   }

                   
                   foreach (var EleMatx in SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetMatrix)
                   {
                       int LastMatrixID = _DB2.GetLatestEnfilageMatrixID();
                       EnfilageMatrix EnfMatrix = new EnfilageMatrix();
                       EnfMatrix.ID = LastMatrixID;
                       EnfMatrix.x = EleMatx.x ;
                       EnfMatrix.y = EleMatx.y;
                       EnfMatrix.DentFil = EleMatx.DentFil;
                       var eqObj= EquivList.SingleOrDefault(eq => eq.Id == EleMatx.value.ID);
                       
                       EnfMatrix.value =(Composition)EleMatx.value;
                       EnfMatrix.value.ID = eqObj.EquivId;
                       EnfMatrix.Enf = NewProd.EnfilageID;
                       _DB2.AddNewEnfilageMatrix(EnfMatrix);
                       
                   }
               }
                 AnnulerFicheTechnique();
          
                 SelectedFicheTechnique = null;
                 if (IsAllFT)
                 {
                     FullFTList = new MvxObservableCollection<FicheTechnique>(_DB2.GetFicheTechniques());
                     FullFTByCatList=FullFTList;
                     FicheTechniqueList = FullFTList;
                 }else if (IsProdFT)
                 {
                     FullFTList = new MvxObservableCollection<FicheTechnique>(_DB2.GetFicheTechniquesProd());
                     FullFTByCatList=FullFTList;
                     FicheTechniqueList = FullFTList;
                 }
                 else
                 {
                     FullFTList = new MvxObservableCollection<FicheTechnique>(_DB2.GetFicheTechniquesEch());
                     FullFTByCatList=FullFTList;
                     FicheTechniqueList = FullFTList;
                 }
                 SelectedFicheTechnique = FicheTechniqueList.SingleOrDefault(ft => ft.ID == SaveFTID);
             }
            else
            {
                SendNotification.Raise("Aucune Fiche Technique Séléctionnée");
            }
        }
        
        private IMvxCommand _CmdAddFicheTek;

        public IMvxCommand CmdAddFicheTek
        {
            get
            {
                _CmdAddFicheTek = new MvxAsyncCommand(DisplayFicheTekModel);
                return _CmdAddFicheTek;
            }
        }
        private IMvxCommand _CmdValiderFicheTek;

        public IMvxCommand CmdValiderFicheTek
        {
            get
            {
                _CmdValiderFicheTek = new MvxCommand(ValiderFicheTechnique);
                return _CmdValiderFicheTek;
            }
        }

        public void ValiderFicheTechnique()
        {
            if (SelectedFicheTechnique != null)
            {
                if (SelectedFicheTechnique.Produits.Count > 0)
                {
                    int lastIn = SelectedFicheTechnique.Produits.Count - 1;
                    _DB2.ValidateFicheTechnique(SelectedFicheTechnique.Produits[lastIn]);
                    UpdateFicheTek();
                }
               
            }
            else
            {
                SendNotification.Raise("Aucune Fiche Technique Séléctionnée");
            }
        }
        private IMvxCommand _CmdSaveFicheTek;

        public IMvxCommand CmdSaveFicheTek
        {
            get
            {
                _CmdSaveFicheTek = new MvxCommand(SaveFicheTechnique);
                return _CmdSaveFicheTek;
            }
        }

        private IMvxCommand _NextPage;

        public IMvxCommand NextPage
        {
            get
            {
                _NextPage = new MvxCommand(ShowSecondPart);
                return _NextPage;
            }
        }

        private IMvxCommand _PrevPage;

        public IMvxCommand PrevPage
        {
            get
            {
                _PrevPage = new MvxCommand(ShowFirstPart);
                return _PrevPage;
            }
        }

        public MvxObservableCollection<Composition> CompoList
        {
            get => _CompoList;
            set
            {
                _CompoList = value;
                RaisePropertyChanged();
            }
        }

        public static string FirstCharToUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public void ShowFirstPart()
        {
            IsPrevPage = false;
            IsNextPage = true;
            Part1Visible = true;
            Part2Visible = false;
            PageNumber = "1/2";
        }

        public void ShowSecondPart()
        {
            IsNextPage = false;
            IsPrevPage = true;
            Part1Visible = false;
            Part2Visible = true;
            PageNumber = "2/2";
        }


        private bool _IsEchantillon;

        public bool IsEchantillon
        {
            get
            {
                return _IsEchantillon;
            }
            set
            {
                _IsEchantillon = value;
                RaisePropertyChanged();
            }
        }
        
        private bool _IsAddEnabled=true;

        private ModelFiche mf;
        
        public async Task DisplayFicheTekModel()
        {
           
            mf=await  _NavigationService.Navigate<ModelFicheTekViewModel,ModelFiche,ModelFiche>(new ModelFiche());
            if (mf != null)
            {
                if (mf.model == ModelFiche.ModelFicheTek.FicheTekCrochetage)
                {
                    IsEnfilage = false;
                }

                if (mf!=null && mf.IsEchantillon)
                {
                    IsProduction = false;
                    MinVers = 0;
                }
                else
                {
                    IsProduction = true;
                    MinVers = 1;
                }
                DateEndLimit=DateTime.Now;
                
                AjouterFicheTechnique(mf);
            }
          
        }

        public void ResetCompo()
        {
            Comp1 = null;
            Comp2 = null;
            Comp3 = null;
            Comp4 = null;
            Comp5 = null;
            Comp7 = null;
            Comp8 = null;
            Comp9 = null;
        }

        private string _OldRef;

        public string OldRef
        {
            get
            {
                return _OldRef;
            }
            set
            {
                _OldRef = value;
                RaisePropertyChanged();
            }
        }
        private int _OldNumArticle;

        public int OldNumArticle
        {
            get
            {
                return _OldNumArticle;
            }
            set
            {
                _OldNumArticle = value;
                RaisePropertyChanged();
            }
        }
        private int _OldVersion;

        public int OldVersion
        {
            get
            {
                return _OldVersion;
            }
            set
            {
                _OldVersion = value;
                RaisePropertyChanged();
            }
        }

        private int _MinVers=0;

        public int MinVers
        {
            get
            {
                return _MinVers;
            }
            set
            {
                _MinVers = value;
                RaisePropertyChanged();
            }
        }
        public void AjouterFicheTechnique(ModelFiche mf)
        {
            Ordre = "";
            CreationDisplayDate = DateTime.Now;
            IsDentFilVis = true;
            EditDate = true;
            DisplayDate = false;
            EnableEditing = true;
            SaveCancelBtn = true;
            PrintBtn = false;
            BtnVis = true;
            IsAddEnabled = false;
            TotalPoids = 0;
            if (mf.model == ModelFiche.ModelFicheTek.FicheTekCrochetage)
            {
                
                
                IsFicheCrochetage = true;
                IsFicheUniDuitage = false;
                IsFicheEHC = false;
                IsFicheNormal = true;
                MachineList = new MvxObservableCollection<Machine>(_DB2.GetCrochtageMachines());

            }else if (mf.model == ModelFiche.ModelFicheTek.FicheTekEHC)
            {
                IsFicheCrochetage = false;
                IsFicheUniDuitage = true;
                IsFicheEHC = true;
                IsFicheNormal = false;
                
            }
            else
            {
                IsFicheCrochetage = false;
                IsFicheUniDuitage = true;
                IsFicheEHC = false;
                IsFicheNormal = true;
            }

            ResetCompo();
            NewProd = new Produit();
            
            foreach (var CellContent in ContentEnfilageList)
            {
                EnfilageList.First(en => en.X == CellContent.X && en.Y == CellContent.Y).Content=null;
            }

            ContentEnfilageList = new MvxObservableCollection<MatrixElement>();
            
            SchCompList = new MvxObservableCollection<Composition>();
            ChaineNameList = new MvxObservableCollection<chaine>(_DB2.GetChaines());
            var NewFicheTek = new FicheTechnique();
            if (IsFicheCrochetage)
            {
                NewFicheTek.ModelFiche = (int)ModelFiche.ModelFicheTek.FicheTekCrochetage;
            }else if (IsFicheNormal)
            {
                NewFicheTek.ModelFiche = (int)ModelFiche.ModelFicheTek.FicheTekNormal;
            }
            else
            {
                NewFicheTek.ModelFiche = (int)ModelFiche.ModelFicheTek.FicheTekEHC;
            }
            
            FicheTechnique GivenFiche=_DB2.AddNewFicheTechnique(NewFicheTek);
            if (IsProduction)
            {
                NewProd.Version = 1;
            }
            else
            {
                NewProd.Version = 0;
            }
            
            NewProd.FicheTekID = GivenFiche;
            NewProd.FicheId = GivenFiche.ID;
            NewProd.Id= _DB2.GetLastProductID()+1;
            NewProd.Definite = 0;
           //NewProd.DateCreation=DateTime.Now;
            NewProd.Ref="FT"+(FicheTechniqueList.Count() + 1).ToString();
            OldRef = NewProd.Ref;
            OldVersion = NewProd.Version;
            OldNumArticle = NewProd.NumArticle;
            NewProd.Name = "Fiche Technique " + (FicheTechniqueList.Count() + 1).ToString();
            if (IsEnfilage)
            {
                NewProd.IsEnfilage = 1;
                NewProd.EnfilageID = new Enfilage();
                NewProd.EnfilageID.Column = EnfilageColumns;
                NewProd.EnfilageID.Row  = EnfilageRow;
                NewProd.EnfilageID.TrXposition = "340";
                NewProd.EnfilageID.TrYposition = "0";
                NewProd.EnfilageID.ID= _DB2.AddNewEnfilage(NewProd.EnfilageID);
                NewProd.PropertyChanged += Prod_PropertyChanged;
                NewProd.Id= _DB2.AddNewProductWithEnfilage((Produit)NewProd.Clone());
                
            }
            else
            {
                NewProd.IsEnfilage = 0;
                NewProd.PropertyChanged += Prod_PropertyChanged;
                NewProd.Id= _DB2.AddNewProduct((Produit)NewProd.Clone());
            }
           
            FicheTechniqueList = new MvxObservableCollection<FicheTechnique>(_DB2.GetFicheTechniques());
        }

        private bool _IsUpdate=false;

        public bool IsUpdate
        {
            get
            {
                return _IsUpdate;
            }
            set
            {
                _IsUpdate = value;
                RaisePropertyChanged();
            }
        }
        public void SaveFicheTechnique()
        {

            AnnulerFicheTechnique();
            UpdateFicheTek();
          
            SelectedFicheTechnique = null;
           
        }


        public void AnnulerFicheTechnique()
        {
            if (IsFicheCrochetage)
            {
                MachineList = new MvxObservableCollection<Machine>(_Db.GetMachines());
            }
            IsDentFilVis = false;

            WorkRectan = null;

          
            SelectedChaine = null;
            IsDateCr = true;
            TotalPoids = 0;
            
            IsFicheCrochetage = false;
            IsFicheUniDuitage = true;
            IsFicheEHC = false;
            IsFicheNormal = true;
            EditDate = false;
            DisplayDate = true;
            IsAddEnabled = true;
            EnableEditing = false;
            SaveCancelBtn = false;
            PrintBtn = true;
            BtnVis = false;
            Comp1Vis = false;
            Comp2Vis = false;
            Comp3Vis = false;
            Comp4Vis = false;
            Comp5Vis = false;
            Comp6Vis = false;
            Comp7Vis = false;
            Comp8Vis = false;
            Comp9Vis = false;
            NewProd = new Produit();
            Comp1 = null;
            Comp2 = null;
            Comp3 = null;
            Comp4 = null;
            Comp5 = null;
            Comp6 = null;
            Comp7 = null;
            Comp8 = null;
            Comp9 = null;
            InitBtnImage();
            SelectedCategorie = null;
            CategorieName = "";
            SchCompList = new MvxObservableCollection<Composition>();
            ChaineList = new ObservableCollection<ChaineMatrixElement>();
            List<MatrixElement> lvide = new List<MatrixElement>();
            ContentEnfilageList = new MvxObservableCollection<MatrixElement>(lvide);
            
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

        private Machine _SelectedMachine;

        public Machine SelectedMachine
        {
            get => _SelectedMachine;
            set
            {
                _SelectedMachine = value;
                RaisePropertyChanged();
                if (SelectedMachine != null)
                {
                    SetupDuitageList();
                }
            }
        }

        public void SetupDuitageList()
        {
            if (SelectedMachine != null)
            {
                
                DuitageList = new MvxObservableCollection<Duitages>(_DB2.GetDuitageMachine(SelectedMachine));
                if (IsFicheCrochetage)
                {
                    DuitageGoList = new MvxObservableCollection<DuitageGomme>(_DB2.GetDuitageMachineGo(SelectedMachine));  
                }
            }
           
        }

        public void SetupVitesse()
        {
            if (NewProd.DuitageID != null)
                Vitesse = _Db.GetDuitage(SelectedMachine.ID, NewProd.DuitageID.Duitage).Vitesse.ToString();
        }

        #region Composant Properties and Methods

        public void InitBtnImage()
        {
            ChangeImageBtn1 = "../Asset/plus64.png";
            ChangeImageBtn2 = "../Asset/plus64.png";
            ChangeImageBtn3 = "../Asset/plus64.png";
            ChangeImageBtn4 = "../Asset/plus64.png";
            ChangeImageBtn5 = "../Asset/plus64.png";
            ChangeImageBtn6 = "../Asset/plus64.png";
            ChangeImageBtn7 = "../Asset/plus64.png";
            ChangeImageBtn8 = "../Asset/plus64.png";
            ChangeImageBtn9 = "../Asset/plus64.png";
        }

        public void InitComposantBtnsCommand()
        {
            CmdAddComp1 = new MvxCommand(() => { AddComp(1); });
            CmdAddComp2 = new MvxCommand(() => { AddComp(2); });
            CmdAddComp3 = new MvxCommand(() => { AddComp(3); });
            CmdAddComp4 = new MvxCommand(() => { AddComp(4); });
            CmdAddComp5 = new MvxCommand(() => { AddComp(5); });
            CmdAddComp6 = new MvxCommand(() => { AddComp(6); });
            CmdAddComp7 = new MvxCommand(() => { AddComp(7); });
            CmdAddComp8 = new MvxCommand(() => { AddComp(8); });
            CmdAddComp9 = new MvxCommand(() => { AddComp(9); });
        }

        public void UpdateListTitrage1()
        {
            ListTitrage1 =
                new MvxObservableCollection<Titrage>(
                    _Db.GetTitrageByTypMat(SelectedTypeMatiere1));
        }
        public void UpdateListTitrage2()
        {
            ListTitrage2 =
                new MvxObservableCollection<Titrage>(
                    _Db.GetTitrageByTypMat(SelectedTypeMatiere2));
        }
        public void UpdateListTitrage3()
        {
            ListTitrage3 =
                new MvxObservableCollection<Titrage>(
                    _Db.GetTitrageByTypMat(SelectedTypeMatiere3));
        }
        public void UpdateListTitrage4()
        {
            ListTitrage4 =
                new MvxObservableCollection<Titrage>(
                    _Db.GetTitrageByTypMat(SelectedTypeMatiere4));
        }
        public void UpdateListTitrage5()
        {
            ListTitrage5 =
                new MvxObservableCollection<Titrage>(
                    _Db.GetTitrageByTypMat(SelectedTypeMatiere5));
        }
        public void UpdateListTitrage6()
        {
            ListTitrage6 =
                new MvxObservableCollection<Titrage>(
                    _Db.GetTitrageByTypMat(SelectedTypeMatiere6));
        }
        public void UpdateListTitrage7()
        {
            ListTitrage7 =
                new MvxObservableCollection<Titrage>(
                    _Db.GetTitrageByTypMat(SelectedTypeMatiere7));
        }
        public void UpdateListTitrage8()
        {
            ListTitrage8 =
                new MvxObservableCollection<Titrage>(
                    _Db.GetTitrageByTypMat(SelectedTypeMatiere8));
        }
        public void UpdateListTitrage9()
        {
            ListTitrage9 =
                new MvxObservableCollection<Titrage>(
                    _Db.GetTitrageByTypMat(SelectedTypeMatiere9));
        }
        private TypeMatiere _SelectedTypeMatiere1;
        private void Comp1_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
             if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _Db.UpdateCompoPoids(Comp1);
            }else  if (e.PropertyName.Equals("EnfNbrFil"))
             {
               
                 _Db.UpdateCompoEnfNbr(Comp1);
             }else  if (e.PropertyName.Equals("NbrFil"))
             {
               
                 _Db.UpdateCompoNbrFil(Comp1);
                CheckWorkspaceReadiness();
                
             }else  if (e.PropertyName.Equals("GetComposant"))
             {
               
               _Db.UpdateCompoComposant(Comp1);
             }else  if (e.PropertyName.Equals("Num"))
             {
               
                 _Db.UpdateCompoNum(Comp1);
             }else  if (e.PropertyName.Equals("NumComposant"))
             {
               
                 _Db.UpdateCompoNumComposant(Comp1);
             }else  if (e.PropertyName.Equals("GetMatiere"))
             {
               
                 _Db.UpdateCompoMatiere(Comp1);
             }else  if (e.PropertyName.Equals("Torsion"))
             {
               
                 _Db.UpdateCompoTorsion(Comp1);
             }else  if (e.PropertyName.Equals("Enfilage"))
             {
               
                 _Db.UpdateCompoEnfilage(Comp1);
             }else  if (e.PropertyName.Equals("Emb"))
             {
               
                 _Db.UpdateCompoEmb(Comp1);
             }else  if (e.PropertyName.Equals("Observation"))
             {
               
                 _Db.UpdateCompoObservation(Comp1);
             }
        }

        public void CheckWorkspaceReadiness()
        {
            if (SelectedChaine != null)
            {
                if(SchCompList.Count>0)
                {
                    bool b = true;
                    int sumNbrFil = 0;
                    foreach (var sch in SchCompList)
                    {
                        if (sch.NbrFil == 0)
                        {
                            b = true;
                        }
                        else
                        {
                            sumNbrFil = sumNbrFil+sch.NbrFil;
                        }
                    }

                    if (b)
                    {
                         
                        SetupEnfilageWorkspace(sumNbrFil);
                    }
                   

                }
            }
        }

        private void Comp2_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
           if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _Db.UpdateCompoPoids(Comp2);
            }else  if (e.PropertyName.Equals("EnfNbrFil"))
           {
               
               _Db.UpdateCompoEnfNbr(Comp2);
           }else  if (e.PropertyName.Equals("NbrFil"))
           {
               
               _Db.UpdateCompoNbrFil(Comp2);
              CheckWorkspaceReadiness();
           }else  if (e.PropertyName.Equals("GetComposant"))
           {
               
               _Db.UpdateCompoComposant(Comp2);
           }else  if (e.PropertyName.Equals("NumComposant"))
           {
               
               _Db.UpdateCompoNumComposant(Comp2);
           }else  if (e.PropertyName.Equals("GetMatiere"))
           {
               
               _Db.UpdateCompoMatiere(Comp2);
           }else  if (e.PropertyName.Equals("Torsion"))
           {
               
               _Db.UpdateCompoTorsion(Comp2);
           }else  if (e.PropertyName.Equals("Enfilage"))
           {
               
               _Db.UpdateCompoEnfilage(Comp2);
           }else  if (e.PropertyName.Equals("Emb"))
           {
               
               _Db.UpdateCompoEmb(Comp2);
           }else  if (e.PropertyName.Equals("Num"))
           {
               
               _Db.UpdateCompoNum(Comp2);
           }else  if (e.PropertyName.Equals("Observation"))
           {
               
               _Db.UpdateCompoObservation(Comp2);
           }
        }

        private void Comp3_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
           if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _Db.UpdateCompoPoids(Comp3);
            }else  if (e.PropertyName.Equals("EnfNbrFil"))
           {
               
               _Db.UpdateCompoEnfNbr(Comp3);
           }else  if (e.PropertyName.Equals("NbrFil"))
           {
               
               _Db.UpdateCompoNbrFil(Comp3);
               CheckWorkspaceReadiness();
           }else  if (e.PropertyName.Equals("GetComposant"))
           {
               
               _Db.UpdateCompoComposant(Comp3);
           }else  if (e.PropertyName.Equals("NumComposant"))
           {
               
               _Db.UpdateCompoNumComposant(Comp3);
           }else  if (e.PropertyName.Equals("GetMatiere"))
           {
               
               _Db.UpdateCompoMatiere(Comp3);
           }else  if (e.PropertyName.Equals("Torsion"))
           {
               
               _Db.UpdateCompoTorsion(Comp3);
           }else  if (e.PropertyName.Equals("Enfilage"))
           {
               
               _Db.UpdateCompoEnfilage(Comp3);
           }else  if (e.PropertyName.Equals("Emb"))
           {
               
               _Db.UpdateCompoEmb(Comp3);
           }else  if (e.PropertyName.Equals("Num"))
           {
               
               _Db.UpdateCompoNum(Comp3);
           }else  if (e.PropertyName.Equals("Observation"))
           {
               
               _Db.UpdateCompoObservation(Comp3);
           }
        }

        private void Comp4_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
           if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _Db.UpdateCompoPoids(Comp4);
            }else  if (e.PropertyName.Equals("EnfNbrFil"))
           {
               
               _Db.UpdateCompoEnfNbr(Comp4);
           }else  if (e.PropertyName.Equals("NbrFil"))
           {
               
               _Db.UpdateCompoNbrFil(Comp4);
               CheckWorkspaceReadiness();
           }else  if (e.PropertyName.Equals("GetComposant"))
           {
               
               _Db.UpdateCompoComposant(Comp4);
           }else  if (e.PropertyName.Equals("NumComposant"))
           {
               
               _Db.UpdateCompoNumComposant(Comp4);
           }else  if (e.PropertyName.Equals("GetMatiere"))
           {
               
               _Db.UpdateCompoMatiere(Comp4);
           }else  if (e.PropertyName.Equals("Torsion"))
           {
               
               _Db.UpdateCompoTorsion(Comp4);
           }else  if (e.PropertyName.Equals("Enfilage"))
           {
               
               _Db.UpdateCompoEnfilage(Comp4);
           }else  if (e.PropertyName.Equals("Emb"))
           {
               
               _Db.UpdateCompoEmb(Comp4);
           }else  if (e.PropertyName.Equals("Num"))
           {
               
               _Db.UpdateCompoNum(Comp4);
           }else  if (e.PropertyName.Equals("Observation"))
           {
               
               _Db.UpdateCompoObservation(Comp4);
           }
        }

        private void Comp5_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
           if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _Db.UpdateCompoPoids(Comp5);
            }else  if (e.PropertyName.Equals("EnfNbrFil"))
           {
               
               _Db.UpdateCompoEnfNbr(Comp5);
           }else  if (e.PropertyName.Equals("NbrFil"))
           {
               
               _Db.UpdateCompoNbrFil(Comp5);
              CheckWorkspaceReadiness();
           }else  if (e.PropertyName.Equals("GetComposant"))
           {
               
               _Db.UpdateCompoComposant(Comp5);
           }else  if (e.PropertyName.Equals("NumComposant"))
           {
               
               _Db.UpdateCompoNumComposant(Comp5);
           }else  if (e.PropertyName.Equals("GetMatiere"))
           {
               
               _Db.UpdateCompoMatiere(Comp5);
           }else  if (e.PropertyName.Equals("Torsion"))
           {
               
               _Db.UpdateCompoTorsion(Comp5);
           }else  if (e.PropertyName.Equals("Enfilage"))
           {
               
               _Db.UpdateCompoEnfilage(Comp5);
           }else  if (e.PropertyName.Equals("Emb"))
           {
               
               _Db.UpdateCompoEmb(Comp5);
           }else  if (e.PropertyName.Equals("Num"))
           {
               
               _Db.UpdateCompoNum(Comp5);
           }else  if (e.PropertyName.Equals("Observation"))
           {
               
               _Db.UpdateCompoObservation(Comp5);
           }
        }

        private void Comp6_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _Db.UpdateCompoPoids(Comp6);
            }else  if (e.PropertyName.Equals("EnfNbrFil"))
            {
               
                _Db.UpdateCompoEnfNbr(Comp6);
            }else  if (e.PropertyName.Equals("NbrFil"))
            {
               
                _Db.UpdateCompoNbrFil(Comp6);
                CheckWorkspaceReadiness();
            }else  if (e.PropertyName.Equals("GetComposant"))
            {
               
                _Db.UpdateCompoComposant(Comp6);
            }else  if (e.PropertyName.Equals("NumComposant"))
            {
               
                _Db.UpdateCompoNumComposant(Comp6);
            }else  if (e.PropertyName.Equals("GetMatiere"))
            {
               
                _Db.UpdateCompoMatiere(Comp6);
            }else  if (e.PropertyName.Equals("Torsion"))
            {
               
                _Db.UpdateCompoTorsion(Comp6);
            }else  if (e.PropertyName.Equals("Enfilage"))
            {
               
                _Db.UpdateCompoEnfilage(Comp6);
            }else  if (e.PropertyName.Equals("Num"))
            {
               
                _Db.UpdateCompoNum(Comp6);
            }else  if (e.PropertyName.Equals("Emb"))
            {
               
                _Db.UpdateCompoEmb(Comp6);
            }else  if (e.PropertyName.Equals("Observation"))
            {
               
                _Db.UpdateCompoObservation(Comp6);
            }
        }

        private void Comp7_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _Db.UpdateCompoPoids(Comp7);
            }else  if (e.PropertyName.Equals("EnfNbrFil"))
            {
               
                _Db.UpdateCompoEnfNbr(Comp7);
            }else  if (e.PropertyName.Equals("NbrFil"))
            {
               
                _Db.UpdateCompoNbrFil(Comp7);
                CheckWorkspaceReadiness();
            }else  if (e.PropertyName.Equals("GetComposant"))
            {
               
                _Db.UpdateCompoComposant(Comp7);
            }else  if (e.PropertyName.Equals("NumComposant"))
            {
               
                _Db.UpdateCompoNumComposant(Comp7);
            }else  if (e.PropertyName.Equals("GetMatiere"))
            {
               
                _Db.UpdateCompoMatiere(Comp7);
            }else  if (e.PropertyName.Equals("Torsion"))
            {
               
                _Db.UpdateCompoTorsion(Comp7);
            }else  if (e.PropertyName.Equals("Num"))
            {
               
                _Db.UpdateCompoNum(Comp7);
            }else  if (e.PropertyName.Equals("Enfilage"))
            {
               
                _Db.UpdateCompoEnfilage(Comp7);
            }else  if (e.PropertyName.Equals("Emb"))
            {
               
                _Db.UpdateCompoEmb(Comp7);
            }else  if (e.PropertyName.Equals("Observation"))
            {
               
                _Db.UpdateCompoObservation(Comp7);
            }
        }

        private void Comp8_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _Db.UpdateCompoPoids(Comp8);
            }else  if (e.PropertyName.Equals("EnfNbrFil"))
            {
               
                _Db.UpdateCompoEnfNbr(Comp8);
            }else  if (e.PropertyName.Equals("NbrFil"))
            {
               
                _Db.UpdateCompoNbrFil(Comp8);
            }else  if (e.PropertyName.Equals("GetComposant"))
            {
               
                _Db.UpdateCompoComposant(Comp8);
            }else  if (e.PropertyName.Equals("NumComposant"))
            {
               
                _Db.UpdateCompoNumComposant(Comp8);
            }else  if (e.PropertyName.Equals("GetMatiere"))
            {
               
                _Db.UpdateCompoMatiere(Comp8);
            }else  if (e.PropertyName.Equals("Torsion"))
            {
               
                _Db.UpdateCompoTorsion(Comp8);
            }else  if (e.PropertyName.Equals("Enfilage"))
            {
               
                _Db.UpdateCompoEnfilage(Comp8);
            }else  if (e.PropertyName.Equals("Emb"))
            {
               
                _Db.UpdateCompoEmb(Comp8);
            }else  if (e.PropertyName.Equals("Observation"))
            {
               
                _Db.UpdateCompoObservation(Comp8);
            }
        }

        private void Comp9_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _Db.UpdateCompoPoids(Comp9);
            }else  if (e.PropertyName.Equals("EnfNbrFil"))
            {
               
                _Db.UpdateCompoEnfNbr(Comp9);
            }else  if (e.PropertyName.Equals("NbrFil"))
            {
               
                _Db.UpdateCompoNbrFil(Comp9);
            }else  if (e.PropertyName.Equals("GetComposant"))
            {
               
                _Db.UpdateCompoComposant(Comp9);
            }else  if (e.PropertyName.Equals("NumComposant"))
            {
               
                _Db.UpdateCompoNumComposant(Comp9);
            }else  if (e.PropertyName.Equals("GetMatiere"))
            {
               
                _Db.UpdateCompoMatiere(Comp9);
            }else  if (e.PropertyName.Equals("Torsion"))
            {
               
                _Db.UpdateCompoTorsion(Comp9);
            }else  if (e.PropertyName.Equals("Enfilage"))
            {
               
                _Db.UpdateCompoEnfilage(Comp9);
            }else  if (e.PropertyName.Equals("Emb"))
            {
               
                _Db.UpdateCompoEmb(Comp9);
            }else  if (e.PropertyName.Equals("Observation"))
            {
               
                _Db.UpdateCompoObservation(Comp9);
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

        public void AddLegend(int CompNum)
        {
            if (CompNum == 1)
            {
                SchCompList.Add(Comp1);
                SchCompList=new MvxObservableCollection<Composition>(SchCompList.OrderBy(c => c.Num));
            }else if (CompNum ==2)
            {
                SchCompList.Add(Comp2);
                SchCompList=new MvxObservableCollection<Composition>(SchCompList.OrderBy(c => c.Num));
            }else if (CompNum ==3)
            {
                SchCompList.Add(Comp3);
            }else if (CompNum ==4)
            {
                SchCompList.Add(Comp4);
            }else if (CompNum ==5)
            {
                SchCompList.Add(Comp5);
            }else if (CompNum ==6)
            {
                SchCompList.Add(Comp6);
            }else if (CompNum ==7)
            {
                SchCompList.Add(Comp7);
            }
        }

        public void RemoveLegend(int CompNum)
        {
            if (CompNum == 1)
            {
                SchCompList.Remove(Comp1);
            }else  if (CompNum == 2)
            {
                SchCompList.Remove(Comp2);
            }else  if (CompNum == 3)
            {
                SchCompList.Remove(Comp3);
            }else  if (CompNum == 4)
            {
                SchCompList.Remove(Comp4);
            }else  if (CompNum == 5)
            {
                SchCompList.Remove(Comp5);
            }else  if (CompNum == 6)
            {
                SchCompList.Remove(Comp6);
            }else  if (CompNum == 7)
            {
                SchCompList.Remove(Comp7);
            }
        }

        public void DestroyLegend()
        {
            
        }
        public void SetupLegend()
        {
            
        }

        public void SetupEnfilageElement(int CompNum)
        {
            if (CompNum == 1)
            {
                Comp1.BKComposant = "Black";
                Comp1.FGComposant = "White";
                Comp1.NumComposant = 1;
                Comp1.BKBorderComposant = "Black";
                Comp1.ImagePath = @"/Asset/Comp1.png";
                Comp1.ImageReedPath = @"/Asset/dent1.png";
            }else if (CompNum == 2)
            {
     
                Comp2.BKComposant = "Gray";
                Comp2.FGComposant = "White";
                Comp2.NumComposant = 2;
                Comp2.BKBorderComposant = "Gray";
                Comp2.ImagePath = @"/Asset/Comp2.png";
                Comp2.ImageReedPath = @"/Asset/dent2.png";
            }else if (CompNum == 3)
            {
                Comp3.BKComposant = "LightGray";
                Comp3.FGComposant = "Black";
                Comp3.NumComposant = 3;
                Comp3.BKBorderComposant = "White";
                Comp3.ImagePath = @"/Asset/comp3.png";
                Comp3.ImageReedPath = @"/Asset/dent3.png";
            }else if (CompNum == 4)
            {
                Comp4.BKComposant = "White";
                Comp4.FGComposant = "Black";
                Comp4.NumComposant = 4;
                Comp4.BKBorderComposant = "Black";
                Comp4.ImagePath = @"/Asset/comp4.png";
                Comp4.ImageReedPath = @"/Asset/dent4.png";
            }else if (CompNum == 5)
            {
              
                Comp5.BKComposant = "Gray";
                Comp5.FGComposant = "Black";
                Comp5.NumComposant = 5;
                Comp5.BKBorderComposant = "Black";
                Comp5.ImagePath = @"/Asset/comp5.png";
                Comp5.ImageReedPath = @"/Asset/dent5.png";
            }else if (CompNum == 6)
            {
                Comp6.BKComposant = "DimGray";
                Comp6.FGComposant = "Black";
                Comp6.NumComposant = 6;
                Comp6.BKBorderComposant = "White";
                Comp6.ImagePath = @"/Asset/comp6.png";
                Comp6.ImageReedPath = @"/Asset/dent6.png";
            }else if (CompNum == 7)
            {
                Comp7.BKComposant = "White";
                Comp7.FGComposant = "Gray";
                Comp7.NumComposant = 7;
                Comp7.BKBorderComposant = "Black";
                Comp7.ImagePath = @"/Asset/comp7.png";
                Comp7.ImageReedPath = @"/Asset/dent7.png";
            }
        }
        public void AddComp(int i)
        {
            switch (i)
            {
                case 1:

                    if (Comp1Vis != true)
                    {
                        Comp1Vis = !Comp1Vis;
                        ChangeImageBtn1 = "../Asset/remove64.png";
                        Comp1 = new Composition();
                        SetupEnfilageElement(1);
                        if (IsEnfilage)
                        {
                            AddLegend(1);
                        }

                        Comp1.NumComposant = 1;
                        Comp1.ProdID = NewProd;
                        Composition TempoComp = new Composition();
                        TempoComp = (Composition)Comp1.Clone();
                       int tempID = _DB2.AddProdCompo( TempoComp);
                       Comp1.ID = tempID;
                        Comp1.PropertyChanged += Comp1_PropertyChanged;
                        
                        
                        
                    }
                    else
                    {
                        var req = new YesNoQuestion
                        {
                            Question = "êtes-vous sûr de vouloir supprimer ce composant séléctionnée ?",
                            UploadCallback = ok =>
                            {
                                if (ok)
                                {
                                    ChangeImageBtn1 = "../Asset/plus64.png";
                                   
                                    RemoveLegend(1);
                                    _Db.RemoveProdCompo(Comp1);
                                    Comp1 = null;
                                    Comp1Vis = !Comp1Vis;
                                }
                            }
                        };

                        ConfirmAction.Raise(req);
                    }

                    ;
                    break;
                case 2:
                    if (Comp2Vis != true)
                    {
                        Comp2Vis = !Comp2Vis;
                        ChangeImageBtn2 = "../Asset/remove64.png";
                        Comp2 = new Composition();
                        SetupEnfilageElement(2);
                        if (IsEnfilage)
                        {
                            AddLegend(2);
                        }
                        Comp2.ProdID = NewProd;
                        Comp2.NumComposant = 2;
                        Comp2.ID= _DB2.AddProdCompo((Composition)Comp2.Clone());
                        Comp2.PropertyChanged += Comp2_PropertyChanged;
                    }
                    else
                    {
                        var req = new YesNoQuestion
                        {
                            Question = "êtes-vous sûr de vouloir supprimer ce composant séléctionnée ?",
                            UploadCallback = ok =>
                            {
                                if (ok)
                                {
                                    ChangeImageBtn2 = "../Asset/plus64.png";
                                    RemoveLegend(2);
                                    _Db.RemoveProdCompo(Comp2);
                                    Comp2 = null;
                                    Comp2Vis = !Comp2Vis;
                                }
                            }
                        };

                        ConfirmAction.Raise(req);
                    }

                    ;
                    break;
                case 3:
                    if (Comp3Vis != true)
                    {
                        Comp3Vis = !Comp3Vis;
                        ChangeImageBtn3 = "../Asset/remove64.png";
                        Comp3 = new Composition();
                        SetupEnfilageElement(3);
                        if (IsEnfilage)
                        {
                            AddLegend(3);
                        }
                        Comp3.ProdID = NewProd;
                        Comp3.NumComposant = 3;
                        Comp3.ID= _DB2.AddProdCompo((Composition) Comp3.Clone());
                        Comp3.PropertyChanged += Comp3_PropertyChanged;
                    }
                    else
                    {
                        var req = new YesNoQuestion
                        {
                            Question = "êtes-vous sûr de vouloir supprimer ce composant séléctionnée ?",
                            UploadCallback = ok =>
                            {
                                if (ok)
                                {
                                    ChangeImageBtn3 = "../Asset/plus64.png";
                                    RemoveLegend(3);
                                    _Db.RemoveProdCompo(Comp3);
                                    Comp3 = null;
                                    Comp3Vis = !Comp3Vis;
                                }
                            }
                        };

                        ConfirmAction.Raise(req);
                    }

                    break;
                case 4:
                    if (Comp4Vis != true)
                    {
                        Comp4Vis = !Comp4Vis;
                        ChangeImageBtn4 = "../Asset/remove64.png";
                        Comp4 = new Composition();
                       
                        SetupEnfilageElement(4);
                        if (IsEnfilage)
                        {
                            AddLegend(4);
                        }
                        Comp4.ProdID = NewProd;
                        Comp4.NumComposant = 4;
                        Comp4.ID= _DB2.AddProdCompo((Composition) Comp4.Clone());
                        Comp4.PropertyChanged += Comp4_PropertyChanged;
                    }
                    else
                    {
                        var req = new YesNoQuestion
                        {
                            Question = "êtes-vous sûr de vouloir supprimer ce composant séléctionnée ?",
                            UploadCallback = ok =>
                            {
                                if (ok)
                                {
                                    ChangeImageBtn4 = "../Asset/plus64.png";
                                    RemoveLegend(4);
                                    _Db.RemoveProdCompo(Comp4);
                                    Comp4 = null;
                                    Comp4Vis = !Comp4Vis;
                                }
                            }
                        };

                        ConfirmAction.Raise(req);
                    }

                    break;
                case 5:
                    if (Comp5Vis != true)
                    {
                        Comp5Vis = !Comp5Vis;
                        ChangeImageBtn5 = "../Asset/remove64.png";
                        Comp5 = new Composition();
                        SetupEnfilageElement(5);
                        if (IsEnfilage)
                        {
                            AddLegend(5);
                        }
                        Comp5.ProdID = NewProd;
                        Comp5.NumComposant = 5;
                        Comp5.ID= _DB2.AddProdCompo((Composition) Comp5.Clone());
                        Comp5.PropertyChanged += Comp5_PropertyChanged;
                    }
                    else
                    {
                        var req = new YesNoQuestion
                        {
                            Question = "êtes-vous sûr de vouloir supprimer ce composant séléctionnée ?",
                            UploadCallback = ok =>
                            {
                                if (ok)
                                {
                                    ChangeImageBtn5 = "../Asset/plus64.png";
                                    RemoveLegend(5);
                                    _Db.RemoveProdCompo(Comp5);
                                    Comp5 = null;
                                    Comp5Vis = !Comp5Vis;
                                }
                            }
                        };

                        ConfirmAction.Raise(req);
                    }

                    break;
                case 6:
                    if (Comp6Vis != true)
                    {
                        Comp6Vis = !Comp6Vis;
                        ChangeImageBtn6 = "../Asset/remove64.png";
                        Comp6 = new Composition();
                        SetupEnfilageElement(6);
                        if (IsEnfilage)
                        {
                            AddLegend(6);
                        }
                        Comp6.ProdID = NewProd;
                        Comp6.NumComposant = 6;
                        Comp6.ID= _DB2.AddProdCompo((Composition) Comp6.Clone());
                        Comp6.PropertyChanged += Comp6_PropertyChanged;
                    }
                    else
                    {
                        var req = new YesNoQuestion
                        {
                            Question = "êtes-vous sûr de vouloir supprimer ce composant séléctionnée ?",
                            UploadCallback = ok =>
                            {
                                if (ok)
                                {
                                    ChangeImageBtn6 = "../Asset/plus64.png";
                                    RemoveLegend(6);
                                    _Db.RemoveProdCompo(Comp6);
                                    Comp6 = null;
                                    Comp6Vis = !Comp6Vis;
                                }
                            }
                        };

                        ConfirmAction.Raise(req);
                    }

                    break;
                case 7:
                    if (Comp7Vis != true)
                    {
                        Comp7Vis = !Comp7Vis;
                        ChangeImageBtn7 = "../Asset/remove64.png";
                        Comp7 = new Composition();
                        SetupEnfilageElement(7);
                        if (IsEnfilage)
                        {
                            AddLegend(7);
                        }
                        Comp7.ProdID = NewProd;
                        Comp7.NumComposant = 7;
                        Comp7.ID= _DB2.AddProdCompo((Composition) Comp7.Clone());
                        Comp7.PropertyChanged += Comp7_PropertyChanged;
                    }
                    else
                    {
                        var req = new YesNoQuestion
                        {
                            Question = "êtes-vous sûr de vouloir supprimer ce composant séléctionnée ?",
                            UploadCallback = ok =>
                            {
                                if (ok)
                                {
                                    ChangeImageBtn7 = "../Asset/plus64.png";
                                    RemoveLegend(7);
                                    _Db.RemoveProdCompo(Comp7);
                                    Comp7 = null;
                                    Comp7Vis = !Comp7Vis;
                                }
                            }
                        };

                        ConfirmAction.Raise(req);
                    }

                    break;
                case 8:
                    if (Comp8Vis != true)
                    {
                        Comp8Vis = !Comp8Vis;
                        ChangeImageBtn8 = "../Asset/remove64.png";
                        Comp8 = new Composition();
                        Comp8.ProdID = NewProd;
                        Comp8.NumComposant = 8;
                        Comp8.ID= _DB2.AddProdCompo((Composition) Comp8.Clone());
                        Comp8.PropertyChanged += Comp8_PropertyChanged;
                    }
                    else
                    {
                        var req = new YesNoQuestion
                        {
                            Question = "êtes-vous sûr de vouloir supprimer ce composant séléctionnée ?",
                            UploadCallback = ok =>
                            {
                                if (ok)
                                {
                                    ChangeImageBtn8 = "../Asset/plus64.png";
                                    _Db.RemoveProdCompo(Comp8);
                                    Comp8 = null;
                                    Comp8Vis = !Comp8Vis;
                                }
                            }
                        };

                        ConfirmAction.Raise(req);
                    }

                    break;
                case 9:
                    if (Comp9Vis != true)
                    {
                        Comp9Vis = !Comp9Vis;
                        ChangeImageBtn9 = "../Asset/remove64.png";
                        Comp9 = new Composition();
                        Comp9.ProdID = NewProd;
                        Comp9.NumComposant = 9;
                        Comp9.ID= _DB2.AddProdCompo((Composition) Comp9.Clone());
                        Comp9.PropertyChanged += Comp9_PropertyChanged;
                    }
                    else
                    {
                        var req = new YesNoQuestion
                        {
                            Question = "êtes-vous sûr de vouloir supprimer ce composant séléctionnée ?",
                            UploadCallback = ok =>
                            {
                                if (ok)
                                {
                                    ChangeImageBtn9 = "../Asset/plus64.png";
                                    _Db.RemoveProdCompo(Comp9);
                                    Comp9 = null;
                                    Comp9Vis = !Comp9Vis;
                                }
                            }
                        };

                        ConfirmAction.Raise(req);
                    }

                    break;
            }
        }

        private bool _IsEnfilage=true;

        private bool _IsDentFilVis;
        private bool _BtnVis;

        public bool BtnVis
        {
            get => _BtnVis;
            set
            {
                _BtnVis = value;
                RaisePropertyChanged();
            }
        }

        private bool _Comp1Vis;

        public bool Comp1Vis
        {
            get => _Comp1Vis;
            set
            {
                _Comp1Vis = value;
                RaisePropertyChanged();
            }
        }

        private bool _Comp2Vis;

        public bool Comp2Vis
        {
            get => _Comp2Vis;
            set
            {
                _Comp2Vis = value;
                RaisePropertyChanged();
            }
        }

        private bool _Comp3Vis;

        public bool Comp3Vis
        {
            get => _Comp3Vis;
            set
            {
                _Comp3Vis = value;
                RaisePropertyChanged();
            }
        }

        private bool _Comp4Vis;

        public bool Comp4Vis
        {
            get => _Comp4Vis;
            set
            {
                _Comp4Vis = value;
                RaisePropertyChanged();
            }
        }

        private bool _Comp5Vis;

        public bool Comp5Vis
        {
            get => _Comp5Vis;
            set
            {
                _Comp5Vis = value;
                RaisePropertyChanged();
            }
        }

        private bool _Comp6Vis;

        public bool Comp6Vis
        {
            get => _Comp6Vis;
            set
            {
                _Comp6Vis = value;
                RaisePropertyChanged();
            }
        }

        private bool _Comp7Vis;

        public bool Comp7Vis
        {
            get => _Comp7Vis;
            set
            {
                _Comp7Vis = value;
                RaisePropertyChanged();
            }
        }

        private bool _Comp8Vis;

        public bool Comp8Vis
        {
            get => _Comp8Vis;
            set
            {
                _Comp8Vis = value;
                RaisePropertyChanged();
            }
        }

        private bool _Comp9Vis;

        public bool Comp9Vis
        {
            get => _Comp9Vis;
            set
            {
                _Comp9Vis = value;
                RaisePropertyChanged();
            }
        }


        private string _ChangeImageBtn1;

        public string ChangeImageBtn1
        {
            get => _ChangeImageBtn1;
            set
            {
                _ChangeImageBtn1 = value;
                RaisePropertyChanged();
            }
        }

        private string _ChangeImageBtn2;

        public string ChangeImageBtn2
        {
            get => _ChangeImageBtn2;
            set
            {
                _ChangeImageBtn2 = value;
                RaisePropertyChanged();
            }
        }

        private string _ChangeImageBtn3;

        public string ChangeImageBtn3
        {
            get => _ChangeImageBtn3;
            set
            {
                _ChangeImageBtn3 = value;
                RaisePropertyChanged();
            }
        }

        private string _ChangeImageBtn4;

        public string ChangeImageBtn4
        {
            get => _ChangeImageBtn4;
            set
            {
                _ChangeImageBtn4 = value;
                RaisePropertyChanged();
            }
        }

        private string _ChangeImageBtn5;

        public string ChangeImageBtn5
        {
            get => _ChangeImageBtn5;
            set
            {
                _ChangeImageBtn5 = value;
                RaisePropertyChanged();
            }
        }

        private string _ChangeImageBtn6;

        public string ChangeImageBtn6
        {
            get => _ChangeImageBtn6;
            set
            {
                _ChangeImageBtn6 = value;
                RaisePropertyChanged();
            }
        }

        private string _ChangeImageBtn7;

        public string ChangeImageBtn7
        {
            get => _ChangeImageBtn7;
            set
            {
                _ChangeImageBtn7 = value;
                RaisePropertyChanged();
            }
        }

        private string _ChangeImageBtn8;

        public string ChangeImageBtn8
        {
            get => _ChangeImageBtn8;
            set
            {
                _ChangeImageBtn8 = value;
                RaisePropertyChanged();
            }
        }

        private string _ChangeImageBtn9;

        public string ChangeImageBtn9
        {
            get => _ChangeImageBtn9;
            set
            {
                _ChangeImageBtn9 = value;
                RaisePropertyChanged();
            }
        }

        private IMvxCommand _TousFTCmd;

        public IMvxCommand TousFTCmd
        {
            get
            {
                _TousFTCmd = new MvxCommand(GetAllFT);
                return _TousFTCmd;
            }
        }
        private IMvxCommand _ProdFTCmd;

        public IMvxCommand ProdFTCmd
        {
            get
            {
                _ProdFTCmd = new MvxCommand(GetProdFT);
                return _ProdFTCmd;
            }
        }
        private IMvxCommand _EchFTCmd;

        public IMvxCommand EchFTCmd
        {
            get
            {
                _EchFTCmd = new MvxCommand(GetEchFT);
                return _EchFTCmd;
            }
        }

        public void GetEchFT()
        {
            IsAllFT = false;
            IsEchFT = true;
            IsProdFT = false;
            SelectedCatSearch = null;
            
        }
        public void GetProdFT()
        {
            IsAllFT = false;
            IsEchFT = false;
            IsProdFT = true;
            SelectedCatSearch = null;
            
        }
        private bool IsAllFT=true;
        private bool IsEchFT;
        private bool IsProdFT;
        public void GetAllFT()
        {
            IsAllFT = true;
            IsEchFT = false;
            IsProdFT = false;
            SelectedCatSearch = null;
            
        }

        private IMvxCommand _CmdEditFicheTek;

        public IMvxCommand CmdEditFicheTek
        {
            get
            {
                _CmdEditFicheTek = new MvxCommand(EditFicheTechnique);
                return _CmdEditFicheTek;
            }
        }

       
    
  
        private IMvxCommand _CmdDeleteFicheTek;

        public IMvxCommand CmdDeleteFicheTek
        {
            get
            {
                _CmdDeleteFicheTek = new MvxCommand(DeleteFicheTechnique);
                return _CmdDeleteFicheTek;
            }
        }
        public IMvxCommand CmdAddComp1 { get; set; }

        public IMvxCommand CmdAddComp2 { get; set; }

        public IMvxCommand CmdAddComp3 { get; set; }
        public IMvxCommand CmdAddComp4 { get; set; }
        public IMvxCommand CmdAddComp5 { get; set; }
        public IMvxCommand CmdAddComp6 { get; set; }
        public IMvxCommand CmdAddComp7 { get; set; }
        public IMvxCommand CmdAddComp8 { get; set; }
        public IMvxCommand CmdAddComp9 { get; set; }

        #endregion

        #region Top Part Of Data sheet

        private string _FicheTekVersion;

        public string FicheTekVersion
        {
            get => _FicheTekVersion;
            set
            {
                _FicheTekVersion = value;
                RaisePropertyChanged();
            }
        }

        private string _Ref;

        public string Ref
        {
            get => _Ref;
            set
            {
                _Ref = value;
                RaisePropertyChanged();
            }
        }

        private string _Designation;

        public string Designation
        {
            get => _Designation;
            set
            {
                _Designation = value;
                RaisePropertyChanged();
            }
        }


        private string _Client;

        public string Client
        {
            get => _Client;
            set
            {
                _Client = value;
                RaisePropertyChanged();
            }
        }

        private string _Dimension;

        public string Dimension
        {
            get => _Dimension;
            set
            {
                _Dimension = value;
                RaisePropertyChanged();
            }
        }

        private string _Duitage;

        public string Duitage
        {
            get => _Duitage;
            set
            {
                _Duitage = value;
                RaisePropertyChanged();
            }
        }

        private string _Dent;

        public string Dent
        {
            get => _Dent;
            set
            {
                _Dent = value;
                RaisePropertyChanged();
            }
        }

        private string _Vitesse;

        public string Vitesse
        {
            get => _Vitesse;
            set
            {
                _Vitesse = value;
                RaisePropertyChanged();
            }
        }

        private string _Peigne;

        public string Peigne
        {
            get => _Peigne;
            set
            {
                _Peigne = value;
                RaisePropertyChanged();
            }
        }

        private string _MachineName;

        public string MachineName
        {
            get => _MachineName;
            set
            {
                _MachineName = value;
                RaisePropertyChanged();
            }
        }

        
        private Couleur _SelectedColor1;
        
        private Titrage _SelectedTitrage1;
        public TypeMatiere SelectedTypeMatiere1
        {
            get => _SelectedTypeMatiere1;
            set
            {
                _SelectedTypeMatiere1 = value;
                if (value != null)
                {
                    UpdateListTitrage1();
                }

                RaisePropertyChanged();

            }
        }


        private TypeMatiere _SelectedTypeMatiere2;
        private TypeMatiere _SelectedTypeMatiere3;
        private TypeMatiere _SelectedTypeMatiere4;
        private TypeMatiere _SelectedTypeMatiere5;
        private TypeMatiere _SelectedTypeMatiere6;
        private TypeMatiere _SelectedTypeMatiere7;
        private TypeMatiere _SelectedTypeMatiere8;
        private TypeMatiere _SelectedTypeMatiere9;

        private Couleur _SelectedColor2;
        private Couleur _SelectedColor3;
        private Couleur _SelectedColor4;
        private Couleur _SelectedColor5;
        private Couleur _SelectedColor6;
        private Couleur _SelectedColor7;
        private Couleur _SelectedColor8;
        private Couleur _SelectedColor9;
        
        private Titrage _SelectedTitrage2;
        
        private Titrage _SelectedTitrage3;
        private Titrage _SelectedTitrage4;
        private Titrage _SelectedTitrage5;
        private Titrage _SelectedTitrage6;
        private Titrage _SelectedTitrage7;
        private Titrage _SelectedTitrage8;
        private Titrage _SelectedTitrage9;
        
        public Titrage SelectedTitrage2
        {
            get => _SelectedTitrage2;
            set
            {
                _SelectedTitrage2 = value;
                if (value != null)
                {
                    UpdateCouleurList2();
                }

                RaisePropertyChanged();
            }
        }

        public Titrage SelectedTitrage3
        {
            get => _SelectedTitrage3;
            set
            {
                _SelectedTitrage3 = value;
                if (value != null)
                {
                    UpdateCouleurList3();
                }

                RaisePropertyChanged();
            }
        }

        public Titrage SelectedTitrage4
        {
            get => _SelectedTitrage4;
            set
            {
                _SelectedTitrage4 = value;
                if (value != null)
                {
                    UpdateCouleurList4();
                }

                RaisePropertyChanged();
            }
        }

        public Titrage SelectedTitrage5
        {
            get => _SelectedTitrage5;
            set
            {
                _SelectedTitrage5 = value;
                if (value != null)
                {
                    UpdateCouleurList5();;
                }

                RaisePropertyChanged();
            }
        }

        public Titrage SelectedTitrage6
        {
            get => _SelectedTitrage6;
            set
            {
                _SelectedTitrage6 = value;
                if (value != null)
                {
                    UpdateCouleurList6();
                }

                RaisePropertyChanged();
            }
        }

        public Titrage SelectedTitrage7
        {
            get => _SelectedTitrage7;
            set
            {
                _SelectedTitrage7 = value;
                if (value != null)
                {
                    UpdateCouleurList7();
                }

                RaisePropertyChanged();
            }
        }

        private MvxObservableCollection<DuitageGomme> _DuitageGoList;


        private MvxObservableCollection<chaine> _ChaineNameList;

        private bool _IsFicheUniDuitage = true;
        
        private bool _IsFicheNormal=true;
        
        private bool _IsFicheEHC=false;
        
        
        public Titrage SelectedTitrage8
        {
            get => _SelectedTitrage8;
            set
            {
                _SelectedTitrage8 = value;
                if (value != null)
                {
                    UpdateCouleurList8();
                }

                RaisePropertyChanged();
            }
        }

        public Titrage SelectedTitrage9
        {
            get => _SelectedTitrage9;
            set
            {
                _SelectedTitrage9 = value;
                if (value != null)
                {
                    UpdateCouleurList9();
                }

                RaisePropertyChanged();
            }
        }


        public Couleur SelectedColor1
        {
            get => _SelectedColor1;
            set
            {
                _SelectedColor1 = value;
                if (value != null)
                {
                    UpdateMatiareComp1();
                }

                RaisePropertyChanged();
            }
        }

        public void UpdateMatiareComp1()
        {
            if (SelectedColor1 != null && SelectedTitrage1 != null)
            {
                Comp1.GetMatiere = _Db.GetMatiere(SelectedTitrage1, SelectedColor1);
            }
        }
        public void UpdateMatiareComp2()
        {
            if (SelectedColor2 != null && SelectedTitrage2 != null)
            {
                Comp2.GetMatiere = _Db.GetMatiere(SelectedTitrage2, SelectedColor2);
            }
        }
        public void UpdateMatiareComp3()
        {
            if (SelectedColor3 != null && SelectedTitrage3 != null)
            {
                Comp3.GetMatiere = _Db.GetMatiere(SelectedTitrage3, SelectedColor3);
            }
        }
        public void UpdateMatiareComp4()
        {
            if (SelectedColor4 != null && SelectedTitrage4 != null)
            {
                Comp4.GetMatiere = _Db.GetMatiere(SelectedTitrage4, SelectedColor4);
            }
        }
        public void UpdateMatiareComp5()
        {
            if (SelectedColor5 != null && SelectedTitrage5 != null)
            {
                Comp5.GetMatiere = _Db.GetMatiere(SelectedTitrage5, SelectedColor5);
            }
        }
        public void UpdateMatiareComp6()
        {
            if (SelectedColor6 != null && SelectedTitrage6 != null)
            {
                Comp6.GetMatiere = _Db.GetMatiere(SelectedTitrage6, SelectedColor6);
            }
        }
        public void UpdateMatiareComp7()
        {
            if (SelectedColor7 != null && SelectedTitrage7 != null)
            {
                Comp7.GetMatiere = _Db.GetMatiere(SelectedTitrage7, SelectedColor7);
            }
        }
        public void UpdateMatiareComp8()
        {
            if (SelectedColor8 != null && SelectedTitrage8 != null)
            {
                Comp8.GetMatiere = _Db.GetMatiere(SelectedTitrage8, SelectedColor8);
            }
        }
        public void UpdateMatiareComp9()
        {
            if (SelectedColor9 != null && SelectedTitrage9 != null)
            {
                Comp9.GetMatiere = _Db.GetMatiere(SelectedTitrage9, SelectedColor9);
            }
        }
        public Titrage SelectedTitrage1
        {
            get => _SelectedTitrage1;
            set
            {
                _SelectedTitrage1 = value;
                if (value != null)
                {
                    UpdateCouleurList1();
                }

                RaisePropertyChanged();
            }
        }

        public ChaineBoardStructure ChaineBoard
        {
            get => _ChaineBoard;
            set => _ChaineBoard = value;
        }
        public ChaineBoardStructure ChaineBoard2
        {
            get => _ChaineBoard2;
            set => _ChaineBoard2 = value;
        }
        public int ChaineColumns
        {
            get => _ChaineColumns;
            set
            {
                _ChaineColumns = value;
                if (EnableChaineCreation)
                {
                    RefreshChaine(ChaineColumns,ChRowSum);
                }
             
                RaisePropertyChanged();
                
            }
        }
        
        private List<ChColComp> _ChcolList;
        public void SetComposantOfColChaine()
        {
            var FoundDup = _ChcolList.SingleOrDefault(ch =>
                ch.ColNum == SelectedColComp.ColNum && ch.ChaineID == SelectedColComp.ChaineID);
           if (FoundDup != null)
           {
               _ChcolList[_ChcolList.IndexOf(FoundDup)].ComposantID = SelectedColComp.ComposantID;
           }
           else
           {
               _ChcolList.Add(SelectedColComp);
           }
          
        }

        private ChColComp _SelectedColComp;

        public ChColComp SelectedColComp
        {
            get
            {
                return _SelectedColComp;
            }
            set
            {
                _SelectedColComp = value;
                if (value != null)
                {
                    SetComposantOfColChaine();
                }
                RaisePropertyChanged();
            }
        }
        public int ChRowSum
        {
            get => _ChRowSum;
            set
            {
                _ChRowSum = value;
                if (EnableChaineCreation)
                {
                    RefreshChaine(ChaineColumns,ChRowSum);
                }
                
                RaisePropertyChanged();
            }
        }

        private int _ChaineRows;
        public int ChaineRows
        {
            get => _ChaineRows;
            set
            {
                _ChaineRows = value;
                RaisePropertyChanged();
            }
        }
        public int ChaineRows2
        {
            get => _ChaineRows2;
            set
            {
                _ChaineRows2 = value;
                RaisePropertyChanged();
            }
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
        public ObservableCollection<ChaineMatrixElement> ChaineList2
        {
            get => _ChaineList2;
            set
            {
                _ChaineList2 = value;
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

        public BoardStructure EnfilageBoard
        {
            get => _EnfilageBoard;
            set => _EnfilageBoard = value;
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

        public MvxObservableCollection<MatrixElement> ContentEnfilageList
        {
            get => _ContentEnfilageList;
            set
            {
                _ContentEnfilageList = value;
                RaisePropertyChanged();
            }
        }

        public bool IsFicheNormal
        {
            get => _IsFicheNormal;
            set
            {
                _IsFicheNormal = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<DuitageGomme> DuitageGoList
        {
            get => _DuitageGoList;
            set
            {
                _DuitageGoList = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsCreateChaine;
        

        private bool _IsFicheCrochetage=false;

        public bool IsFicheEHC
        {
            get => _IsFicheEHC;
            set
            {
                _IsFicheEHC = value;
                RaisePropertyChanged();
            }
        }

        public bool IsFicheCrochetage
        {
            get => _IsFicheCrochetage;
            set
            {
                _IsFicheCrochetage = value;
                RaisePropertyChanged();
            }
        }

        public bool IsFicheUniDuitage
        {
            get => _IsFicheUniDuitage;
            set
            {
                _IsFicheUniDuitage = value;
                RaisePropertyChanged();
            }
        }

        public bool IsAddEnabled
        {
            get => _IsAddEnabled;
            set {
                 _IsAddEnabled = value;
                 RaisePropertyChanged();

            }
           
        }

        private bool _IsTempo=true;

        public bool IsTempo
        {
            get => _IsTempo;
            set
            {
                _IsTempo = value;
                RaisePropertyChanged();
            }
        }

        public bool IsDentFilVis
        {
            get => _IsDentFilVis;
            set
            {
                _IsDentFilVis = value;
                RaisePropertyChanged();
            }
        }

        public int NbrDent
        {
            get => _NbrDent;
            set
            {
                _NbrDent = value;
                NewProd.EnfDent = value;
                RaisePropertyChanged();
            }
        }

        private string _DesignationChaine;
        
        
        public MvxObservableCollection<chaine> ChaineNameList
        {
            get => _ChaineNameList;
            set
            {
                _ChaineNameList = value;
                RaisePropertyChanged();
            }
        }

        private List<Composant> _Compolist;

        public List<Composant> Compolist
        {
            get
            {
                return _Compolist;
            }
            set
            {
                _Compolist = value;
                RaisePropertyChanged();
            }
        }
        
        public bool IsCreateChaine
        {
            get => _IsCreateChaine;
            set
            {
                _IsCreateChaine = value;
                RaisePropertyChanged();
                if (value == true)
                {
                    _ChcolList = new List<ChColComp>();
                    Compolist = new List<Composant>(_DB2.GetComposants());
                    RefreshChaine(4,8);
                    ChaineColumns = 4;
                    ChRowSum = 8;
                    EnableChaineCreation = true;
                    UseCreatedChaine = false;
                   
                    SelectedChaine = null;
                    DesignationChaine = "Chaine " + ChaineNameList.Count;
                    if (NewProd.EnfilageID.GetChaine != null)
                    {
                        _Db.RemoveEnfilageChaine(NewProd.EnfilageID);
                        NewProd.EnfilageID.GetChaine = null;
                    }
                }
                else
                {
                    EnableChaineCreation = false;
                    UseCreatedChaine = true;
                    RefreshChaine(4,8);
                    ChaineColumns = 4;
                    ChRowSum = 8;
                    DesignationChaine = "";
                    
                }
            }
        }

        public bool EnableChaineCreation
        {
            get => _EnableChaineCreation;
            set
            {
                _EnableChaineCreation = value;
                RaisePropertyChanged();
            }
        }

        public bool UseCreatedChaine
        {
            get => _UseCreatedChaine;
            set
            {
                _UseCreatedChaine = value;
                RaisePropertyChanged();
            }
        }

        public IMvxCommand ValiderChaine
        {
            get
            {
                _ValiderChaine = new MvxCommand(ValidationChaine);
               return _ValiderChaine;
            }
        }

        public string DesignationChaine
        {
            get => _DesignationChaine;
            set
            {
                _DesignationChaine = value;
                RaisePropertyChanged();
            }
        }

        public chaine SelectedChaine
        {
            get => _SelectedChaine;
            set
            {
                _SelectedChaine = value;
                RaisePropertyChanged();
                if (SelectedChaine != null)
                {
                    SetupChaine(SelectedChaine.Colonne,SelectedChaine.Ligne, SelectedChaine.ChMatrix);
                }
             
            }
        }

        private bool _IsProduction;

        public bool IsProduction
        {
            get
            {
                return _IsProduction;
            }
            set
            {
                _IsProduction = value;
                RaisePropertyChanged();
            }
        }
        public bool IsEnfilage
        {
            get => _IsEnfilage;
            set
            {
                _IsEnfilage = value;
                RaisePropertyChanged();
                if (Editing == false)
                {
                    if (IsEnfilage == false)
                    {
                        SchCompList = new MvxObservableCollection<Composition>();
                    }
                    else
                    {
                        if (Comp1 != null)
                        {
                            AddLegend(1);
                        }
                        if (Comp2 != null)
                        {
                            AddLegend(2);
                        }
                        if (Comp3 != null)
                        {
                            AddLegend(3);
                        }
                        if (Comp4 != null)
                        {
                            AddLegend(4);
                        }else if (Comp5 != null)
                        {
                            AddLegend(5);
                        }
                        if (Comp6 != null)
                        {
                            AddLegend(6);
                        }
                        if (Comp7 != null)
                        {
                            AddLegend(7);
                        }
                    }
                }
               
            }
        }

        public EnfilageElement AddedElement
        {
            get => _AddedElement;
            set
            {
                _AddedElement = value;
                RaisePropertyChanged();
                ChangeEnfilage();
            }
        }

        public void ChangeEnfilage()
        {
            if (AddedElement != null)
            {
                if (AddedElement.UpdateContent)
                {
                    _DB2.UpdateEnfilageElement(AddedElement.EnfElement.X,AddedElement.EnfElement.Y,NewProd.EnfilageID.ID,AddedElement.Content);

                }else if (AddedElement.AddElement)
                {
                    int LastMatrixID = _DB2.GetLatestEnfilageMatrixID();
                    EnfilageMatrix EnfMatrix = new EnfilageMatrix();
                    EnfMatrix.ID = LastMatrixID;
                    EnfMatrix.x = AddedElement.EnfElement.X ;
                    EnfMatrix.y = AddedElement.EnfElement.Y;
                    EnfMatrix.DentFil = AddedElement.EnfElement.DentFil;
                    List<Composition> TempCompo = _DB2.GetCompositions(NewProd.Id, NewProd.Version);
                    EnfMatrix.value =TempCompo.First(co=>co.ID== AddedElement.EnfElement.Content.ID);
                    EnfMatrix.Enf = NewProd.EnfilageID;
                    _DB2.AddNewEnfilageMatrix(EnfMatrix);
                }else if (AddedElement.AddElement == false)
                { 
                  
                    _DB2.DeleteEnfilageElement(AddedElement.EnfElement.X,AddedElement.EnfElement.Y,NewProd.EnfilageID.ID);
                    
                }
            }
           
        }

        public void ClearChaineContents()
        {
            foreach (var ch in ChaineList)
            {
                
                ch.IsContent = false;
            }
            foreach (var ch in ChaineList2)
            {
                
                ch.IsContent = false;
            }
        }
        public void ClearSetChainContent(List<ChaineMatrix> chmat)
        {
            foreach (var ch in ChaineList)
            {
                
              var exist=  chmat.SingleOrDefault(chi => chi.x == ch.X && chi.y == ch.Y);
              if (exist!=null)
              {
                  if (ch.IsContent == false)
                  {
                      ch.IsContent = true;
                  }
                  
              }
              else
              {
                  ch.IsContent = false;
              }
            }
            foreach (var ch in ChaineList2)
            {
                
                var exist=  chmat.SingleOrDefault(chi => chi.x == ch.X && chi.y == (ch.Y+78));
                if (exist!=null)
                {
                    if (ch.IsContent == false)
                    {
                        ch.IsContent = true;
                    }
                  
                }
                else
                {
                    ch.IsContent = false;
                }
            }
        }
        public void SetupChaine(int ChCol,int ChRow,List<ChaineMatrix> Chmat)
        {
            if (ChCol != ChaineColumns || ChRow != ChRowSum || ChaineList.Count==0)
            {
                
                RefreshChaine(ChCol,ChRow);
                ChaineColumns = ChCol;
                ChRowSum = ChRow;
            }
            ClearSetChainContent(Chmat);

            if (SelectedChaine != null)
            {
                NewProd.EnfilageID.GetChaine = SelectedChaine;
                _DB2.UpdateEnfilageChaine(NewProd.EnfilageID);
                if(SchCompList.Count>0)
                {
                    bool b = true;
                    int sumNbrFil = 0;
                    foreach (var sch in SchCompList)
                    {
                        if (sch.NbrFil == 0)
                        {
                            b = true;
                        }
                        else
                        {
                            sumNbrFil = sumNbrFil+sch.NbrFil;
                        }
                    }

                    if (b)
                    {
                        if (WorkRectan != null)
                        {
                            if ( SelectedChaine.Colonne != (WorkRectan.PartHeight - 4))
                            {
                                SetupEnfilageWorkspace(sumNbrFil);
                            }
                        }
                        else
                        {
                            SetupEnfilageWorkspace(sumNbrFil); 
                        }
                        
                        
                    }
                   

                }
            }
                
            
          
        }
        public void SetupEnfilageWorkspace(int sumNbrFil)
        {
            decimal pn =decimal.Divide(sumNbrFil , EnfilageColumns);
            int  NbrPart =Convert.ToInt32(Math.Ceiling(pn));
            if (WorkRectan == null)
            {
                int PartsHeight = (NbrPart - 1) + (2 + 1 + SelectedChaine.Colonne) * NbrPart;
                int PartHeight = 4 + SelectedChaine.Colonne;
                int PartsWidth = 81;
                int PartsWidth_Start = 1;
                int PartsWidth_End = 81;
                int PartsHeight_Start = (EnfilageRow - PartsHeight) / 2;
                int PartsHeight_End = PartsHeight_Start + PartsHeight;
                var workRect = new WorkRectangle();
                workRect.NbrPart = NbrPart;
                workRect.PartsHeight = PartsHeight;
                workRect.PartsWidth = PartsWidth;
                workRect.PartsHeightStart = PartsHeight_Start;
                workRect.PartsHeightEnd = PartsHeight_End;
                workRect.PartsWidthStart = PartsWidth_Start;
                workRect.PartsWidthEnd = PartsWidth_End;
                workRect.PartHeight = PartHeight;
                WorkRectan = workRect;
            }
            else
            {
                if (NbrPart != WorkRectan.NbrPart)
                {
                    int PartsHeight = (NbrPart - 1) + (2 + 1 + SelectedChaine.Colonne) * NbrPart;
                    int PartHeight = 4 + SelectedChaine.Colonne;
                    int PartsWidth = 81;
                    int PartsWidth_Start = 1;
                    int PartsWidth_End = 81;
                    int PartsHeight_Start = (59 - PartsHeight) / 2;
                    int PartsHeight_End = PartsHeight_Start + PartsHeight;
                    var workRect = new WorkRectangle();
                    workRect.NbrPart = NbrPart;
                    workRect.PartsHeight = PartsHeight;
                    workRect.PartsWidth = PartsWidth;
                    workRect.PartsHeightStart = PartsHeight_Start;
                    workRect.PartsHeightEnd = PartsHeight_End;
                    workRect.PartsWidthStart = PartsWidth_Start;
                    workRect.PartsWidthEnd = PartsWidth_End;
                    workRect.PartHeight = PartHeight;
                    WorkRectan = workRect;
                }
            }
           
        }

        private WorkRectangle _WorkRectan;

        public WorkRectangle WorkRectan
        {
            get
            {
                return _WorkRectan;
            }
            set
            {
                _WorkRectan = value;
                RaisePropertyChanged();
            }
        }
        private chaine _SelectedChaine;
        public void ValidationChaine()
        {

            if (_ChcolList.Count == ChaineColumns)
            {
                
                chaine NewChaine= new chaine();
                NewChaine.Colonne = ChaineColumns;
                NewChaine.Ligne = ChRowSum;
                NewChaine.Nom = DesignationChaine;
                NewChaine.ID= _Db.AddNewChaine(NewChaine);
                foreach (var Ch in ChaineList)
                {
                    if (Ch.IsContent)
                    {
                        ChaineMatrix ChMatrix = new ChaineMatrix();
                        ChMatrix.x = Ch.X;
                        ChMatrix.y = Ch.Y;
                        ChMatrix.Chaine =  NewChaine;
                        _Db.AddNewChaineElement(ChMatrix);
                    }
                
                }
                foreach (var Ch in ChaineList2)
                {
                    if (Ch.IsContent)
                    {
                        ChaineMatrix ChMatrix = new ChaineMatrix();
                        ChMatrix.x = Ch.X;
                        ChMatrix.y = Ch.Y+78;
                        ChMatrix.Chaine =  NewChaine;
                        _Db.AddNewChaineElement(ChMatrix);
                    }
                
                }
                IsCreateChaine = false;
                ChaineNameList = new MvxObservableCollection<chaine>(_DB2.GetChaines());
                SelectedChaine = ChaineNameList.First(ch => ch.ID == NewChaine.ID);

                foreach (var chcol in _ChcolList)
                {

                    chcol.ChaineID = NewChaine.ID;
                        _DB2.AddChColComp(chcol);
                }

                SelectedChaine.ChaineCompos = _ChcolList;

            }
            else
            {
                SendNotification.Raise("Vérifier les composants de chaque colonne");
            }
            
        }

        private MvxObservableCollection<Catalogue> _CategorieList;

        public MvxObservableCollection<Catalogue> CategorieList
        {
            get
            {
                return _CategorieList;
            }
            set
            {
                _CategorieList = value;
                RaisePropertyChanged();
            }
        }

        public void FilterFTByCategorie()
        {
            SearchText = "";
            if (SelectedCatSearch != null)
            {
                FullFTByCatList = new MvxObservableCollection<FicheTechnique>();
                foreach (var ft in FullFTList)
                {
                    if (ft.Catalog != null && ft.Catalog.ID == SelectedCatSearch.ID)
                    {
                        FullFTByCatList.Add(ft);
                    }
                }

                
                FicheTechniqueList = FullFTByCatList;
            }
            else
            {
                UpdateFicheTek();
            }
        }
        
        private Catalogue _SelectedCatSearch;

        public Catalogue SelectedCatSearch
        {
            get
            {
                return _SelectedCatSearch;
            }
            set
            {
                
                _SelectedCatSearch = value;
                RaisePropertyChanged();
                FilterFTByCategorie();
            }
        }
        private Catalogue _SelectedCategorie;

        public Catalogue SelectedCategorie
        {
            get
            {
                return _SelectedCategorie;
            }
            set
            {
                if (_SelectedCategorie != null)
                {
                    _PrevCat = _SelectedCategorie;
                }
               
                _SelectedCategorie = value;
                RaisePropertyChanged();
                SetCategorie();
            }
        }

        private Catalogue _PrevCat;
        private string _Ordre;

        public string Ordre
        {
            get
            {
                return _Ordre;
            }
            set
            {
                _Ordre = value;
                RaisePropertyChanged();
            }
        }
        
        public void SetCategorie()
        {
            if (SelectedCategorie != null)
            {

                _DB2.UpdateFicheTechniqueCategorie(NewProd.FicheId,SelectedCategorie);
                if (mf!=null && mf.IsEchantillon == false)
                {
                    if (_PrevCat != null)
                    {
                        _DB2.AssignOrderToFicheTechnique( _PrevCat.ID);
                    }
                
              
                
                    int ord= _DB2.AssignOrderToFicheTechnique(NewProd.FicheId, SelectedCategorie.ID);
                    Ordre = ord.ToString();
                }
              
            }
        }
        private string _CategorieName;

        public string CategorieName
        {
            get
            {
                return _CategorieName;
            }
            set
            {
                _CategorieName = value;
                RaisePropertyChanged();
            }
        }

        private bool _tempo=false;

        public bool tempo
        {
            get
            {
                return _tempo;
            }
            set
            {
                _tempo = value;
                RaisePropertyChanged();
            }
        }
        private bool _SuperUser;

        public bool SuperUser
        {
            get
            {
                return _SuperUser;
            }
            set
            {
                _SuperUser = value;
                RaisePropertyChanged();
            }
        }
        private bool _IsVerificateur;

        public bool IsVerificateur
        {
            get
            {
                return _IsVerificateur;
            }
            set
            {
                _IsVerificateur = value;
                RaisePropertyChanged();
            }
        }

        private MvxObservableCollection<Reed> _ListPeigne;

        public MvxObservableCollection<Reed> ListPeigne
        {
            get
            {
                return _ListPeigne;
            }
            set
            {
                _ListPeigne = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsRedacteur;

        public bool IsRedacteur
        {
            get
            {
                return _IsRedacteur;
            }
            set
            {
                _IsRedacteur = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsConfirm=false;

        public bool IsConfirm
        {
            get
            {
                return _IsConfirm;
            }
            set
            {
                _IsConfirm = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsPerm = false;

        public bool IsPerm
        {
            get
            {
                return _IsPerm;
            }
            set
            {
                _IsPerm = value;
                RaisePropertyChanged();
            }
        }
        private EnfilageElement _AddedElement;
        
        private bool _UseCreatedChaine=true;

        private bool _EnableChaineCreation=false;
        
        #endregion

        private user UserSession;
        public override void Prepare(user parameter)
        {
            _Db = Mvx.IoCProvider.Resolve<MyDBContext>();
            UserSession = parameter;
            if (UserSession.type == user.UserType.redacteur)
            {
                IsRedacteur = true;
                IsVerificateur = false;
                SuperUser = false;
            }
            else if(UserSession.type == user.UserType.verificateur)
            {
                IsRedacteur = false;
                IsVerificateur = true;
                SuperUser = false;
            }
            else
            {
                IsRedacteur = true;
                IsVerificateur = true;
                SuperUser = true;
            }
        }
    }
}