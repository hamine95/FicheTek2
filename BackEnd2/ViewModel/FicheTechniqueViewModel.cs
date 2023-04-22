using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        public delegate void NotifySafeThread(bool b);

        public static NotifySafeThread SafeThEvent;
        private int _ChaineColumns = 4;
        private ObservableCollection<ChaineMatrixElement> _ChaineList;
        private ObservableCollection<ChaineMatrixElement> _ChaineList2;

        private int _ChaineRows2 = 8;
        private int _ChRowSum = 8;

        private IMvxCommand _CmdAddFicheTek;

        private IMvxCommand _CmdAddNewVersion;

        private IMvxCommand _CmdCancelFicheTek;
        private IMvxCommand _CmdConfirmer;
        private IMvxCommand _CmdSaveFicheTek;
        private IMvxCommand _CmdSelectImage;
        private IMvxCommand _CmdValiderFicheTek;

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
        
        private double _ContWidth;

        public double ContWidth
        {
            get
            {
                return _ContWidth;
            }
            set
            {
                _ContWidth = value;
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

        private double _horizontalFreeSpace;

        public double horizontalFreeSpace
        {
            get
            {
                return _horizontalFreeSpace;
            }
            set
            {
                _horizontalFreeSpace = value;
                RaisePropertyChanged();
            }
        }

        private double _VerticalFreeSpace;
        public double VerticalFreeSpace
        {
            get
            {
                return _VerticalFreeSpace;
            }
            set
            {
                _VerticalFreeSpace = value;
                RaisePropertyChanged();
            }
        }
        
        private MvxObservableCollection<Composition> _CompoList;

        private string _Concepteur;

        private MvxObservableCollection<MatrixElement> _ContentEnfilageList;

        private DateTime _CreationDisplayDate;

        private DateTime _DateEndLimit;
        private MyDBContext _Db;
        private readonly SqliteData _DB2;


        private bool _DisplayDate = true;
        private MvxObservableCollection<Duitages> _DuitageList;
        private bool _EditDate;

        private bool _EnableEditing;

        private TrulyObservableCollection<MatrixElement> _EnfilageList;

        private MvxObservableCollection<FicheTechnique> _FicheTechniqueList;

        private IMvxCommand _FirstPageCmd;

        private MvxObservableCollection<FicheTechnique> _FullFTByCatList;
        private MvxObservableCollection<FicheTechnique> _FullFTList;

        private string _ImageProd;

        private bool _IsAddEnabled = true;


        private bool _IsDateCr = true;


        private bool _IsEchantillon;
        private bool _IsNextPage = true;

        private bool _IsPrevPage;

        private bool _IsSearchByCat;

        private bool _IsUpdate;

        private string _LastXposition;

        private string _LastYposition;
        private MvxObservableCollection<Client> _ListClient;
        private MvxObservableCollection<Composant> _ListComposant;

        private MvxObservableCollection<Concepteur> _ListConcepteur;
        private MvxObservableCollection<Couleur> _ListCouleur1;

        private MvxObservableCollection<Couleur> _ListCouleur2;
        private MvxObservableCollection<Couleur> _ListCouleur3;
        private MvxObservableCollection<Couleur> _ListCouleur4;
        private MvxObservableCollection<Couleur> _ListCouleur5;
        private MvxObservableCollection<Couleur> _ListCouleur6;
        private MvxObservableCollection<Couleur> _ListCouleur7;
        private MvxObservableCollection<Couleur> _ListCouleur8;
        private MvxObservableCollection<Couleur> _ListCouleur9;

        private MvxObservableCollection<Redacteur> _ListRedacteur;


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
        private MvxObservableCollection<Produit> _ListVersion;

        private MvxNotifyTask _LoadLists;
        private MvxObservableCollection<Machine> _MachineList;

        private int _MinVers;
        private readonly IMvxNavigationService _NavigationService;
        private int _NbrDent;
        private Produit _NewProd;

        private IMvxCommand _NextPage;
        private int _OldNumArticle;

        private string _OldRef;
        private int _OldVersion;

        private string _PageNumber;


        private bool _Part1Visible = true;
        private bool _Part2Visible;

        private IMvxCommand _PrevPage;


        private bool _PrintBtn = true;

        private IMvxCommand _PrintCmd;
        private int _RotateX;

        private int _RotateY;


        private bool _SaveCancelBtn;

       

        

        private ObservableCollection<Composition> _SchCompList;

        private string _SearchText;


        private IMvxCommand _SecondPageCmd;

        private SecRectangle _SecondRect;
        private Duitages _SelectedDuitage;

        private FicheTechnique _SelectedFicheTechnique;

        private Machine _SelectedMachine;

        private Produit _SelectedVersion;

        private string _TrameXposition = "0";
        private string _TrameYposition = "0";

        private IMvxCommand _ValiderChaine;


        private string _Verificateur;

        private string CurP;

        private string FullTextSearch;

        private ModelFiche mf;

        private user UserSession;

        public FicheTechniqueViewModel(IMvxNavigationService _navSer)
        {
            _NavigationService = _navSer;

            PageNumber = "1/2";
            _DB2 = new SqliteData();
            InitBtnImage();
            InitComposantBtnsCommand();
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

       

      

        private Repitition _rep1=new Repitition();

        public Repitition rep1
        {
            get
            {
                return _rep1;
            }
            set
            {
                _rep1 = value;
                RaisePropertyChanged();
            }
        }
        private Repitition _rep2=new Repitition();

        public Repitition rep2
        {
            get
            {
                return _rep2;
            }
            set
            {
                _rep2 = value;
                RaisePropertyChanged();
            }
        }
        
        private Repitition _rep3=new Repitition();

        public Repitition rep3
        {
            get
            {
                return _rep3;
            }
            set
            {
                _rep3 = value;
                RaisePropertyChanged();
            }
        }
        private Repitition _rep4=new Repitition();

        public Repitition rep4
        {
            get
            {
                return _rep4;
            }
            set
            {
                _rep4 = value;
                RaisePropertyChanged();
            }
        }
        private Repitition _rep5=new Repitition();

        public Repitition rep5
        {
            get
            {
                return _rep5;
            }
            set
            {
                _rep5 = value;
                RaisePropertyChanged();
            }
        }
        
        public void SetRep1(double x, double y)
        {
            rep1.x = (((x-((MainWinWidth-ContainerWidth)/2)-PanelWidth-40)/ContainerWidth)).ToString();
            rep1.y = ((y-((MainWinHeight-ContainerHeight)/2))/ContainerHeight).ToString();
            _DB2.UpdateReptitionPosition(rep1);
        }

        private IMvxCommand _CmdAddRep;

        public IMvxCommand CmdAddRep
        {
            get
            {
                _CmdAddRep = new MvxCommand(AddReptition);
                return _CmdAddRep;
            }
        }

        public void AddReptition()
        {
            if (!rep1.vis)
            {
                rep1.vis = true;
                rep1.enfilageID = NewProd.EnfilageID.ID;
                rep1.id=  _DB2.AddNewReptition(rep1);
            }else if (!rep2.vis)
            {
                rep2.vis = true;
                rep2.id= _DB2.AddNewReptition(rep2);
            }else if (!rep3.vis)
            {
                rep3.vis = true;
                rep3.id= _DB2.AddNewReptition(rep3);
            }else if (!rep4.vis)
            {
                rep4.vis = true;
                rep4.id=  _DB2.AddNewReptition(rep4);
            }else if (!rep5.vis)
            {
                rep5.vis = true;
                rep5.id= _DB2.AddNewReptition(rep5);
            }
        }
        
        public void SetRep2(double x, double y)
        {
            rep2.x = (((x-((MainWinWidth-ContainerWidth)/2)-PanelWidth-40)/ContainerWidth)).ToString();
            rep2.y = ((y-((MainWinHeight-ContainerHeight)/2))/ContainerHeight).ToString();
            _DB2.UpdateReptitionPosition(rep2);
        }
        public void SetRep3(double x, double y)
        {
            rep3.x = (((x-((MainWinWidth-ContainerWidth)/2)-PanelWidth-40)/ContainerWidth)).ToString();
            rep3.y = ((y-((MainWinHeight-ContainerHeight)/2))/ContainerHeight).ToString();
            _DB2.UpdateReptitionPosition(rep3);
        }
        public void SetRep4(double x, double y)
        {
            rep4.x = (((x-((MainWinWidth-ContainerWidth)/2)-PanelWidth-40)/ContainerWidth)).ToString();
            rep4.y = ((y-((MainWinHeight-ContainerHeight)/2))/ContainerHeight).ToString();
            _DB2.UpdateReptitionPosition(rep4);
        }
        public void SetRep5(double x, double y)
        {
            rep5.x = (((x-((MainWinWidth-ContainerWidth)/2)-PanelWidth-40)/ContainerWidth)).ToString();
            rep5.y = ((y-((MainWinHeight-ContainerHeight)/2))/ContainerHeight).ToString();
            _DB2.UpdateReptitionPosition(rep5);
        }

        public void SetTramePosition(double x, double y)
        {
            double tdb = ContWidth;
            LastXposition = (((x-((MainWinWidth-ContainerWidth)/2)-PanelWidth-40)/ContainerWidth)).ToString();
            LastYposition = ((y-((MainWinHeight-ContainerHeight)/2))/ContainerHeight).ToString();
            SaveTrameXPosition();
            SaveTrameYPosition();
        }

        private double _PanelWidth;

        public double PanelWidth
        {
            get
            {
                return _PanelWidth;
            }

            set
            {
                _PanelWidth = value;
                RaisePropertyChanged();
            }
        }

        private int _AngleChain;

        public int AngleChain
        {
            get => _AngleChain;
            set
            {
                _AngleChain = value;
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
                if (value != null) UpdateMatiareComp2();

                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor3
        {
            get => _SelectedColor3;
            set
            {
                _SelectedColor3 = value;
                if (value != null) UpdateMatiareComp3();

                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor4
        {
            get => _SelectedColor4;
            set
            {
                _SelectedColor4 = value;
                if (value != null) UpdateMatiareComp4();

                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor5
        {
            get => _SelectedColor5;
            set
            {
                _SelectedColor5 = value;
                if (value != null) UpdateMatiareComp5();

                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor6
        {
            get => _SelectedColor6;
            set
            {
                _SelectedColor6 = value;
                if (value != null) UpdateMatiareComp6();

                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor7
        {
            get => _SelectedColor7;
            set
            {
                _SelectedColor7 = value;
                if (value != null) UpdateMatiareComp7();

                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor8
        {
            get => _SelectedColor8;
            set
            {
                _SelectedColor8 = value;
                if (value != null) UpdateMatiareComp8();

                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor9
        {
            get => _SelectedColor9;
            set
            {
                _SelectedColor9 = value;
                if (value != null) UpdateMatiareComp9();

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere2
        {
            get => _SelectedTypeMatiere2;
            set
            {
                _SelectedTypeMatiere2 = value;
                if (value != null) UpdateListTitrage2();

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere3
        {
            get => _SelectedTypeMatiere3;
            set
            {
                _SelectedTypeMatiere3 = value;
                if (value != null) UpdateListTitrage3();

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere4
        {
            get => _SelectedTypeMatiere4;
            set
            {
                _SelectedTypeMatiere4 = value;
                if (value != null) UpdateListTitrage4();

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere5
        {
            get => _SelectedTypeMatiere5;
            set
            {
                _SelectedTypeMatiere5 = value;
                if (value != null) UpdateListTitrage5();

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere6
        {
            get => _SelectedTypeMatiere6;
            set
            {
                _SelectedTypeMatiere6 = value;
                if (value != null) UpdateListTitrage6();

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere7
        {
            get => _SelectedTypeMatiere7;
            set
            {
                _SelectedTypeMatiere7 = value;
                if (value != null) UpdateListTitrage7();

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere8
        {
            get => _SelectedTypeMatiere8;
            set
            {
                _SelectedTypeMatiere8 = value;
                if (value != null) UpdateListTitrage8();

                RaisePropertyChanged();
            }
        }

        public TypeMatiere SelectedTypeMatiere9
        {
            get => _SelectedTypeMatiere9;
            set
            {
                _SelectedTypeMatiere9 = value;
                if (value != null) UpdateListTitrage9();

                RaisePropertyChanged();
            }
        }

        public MvxNotifyTask LoadLists
        {
            get => _LoadLists;
            set
            {
                SetProperty(ref _LoadLists, value);
                RaisePropertyChanged();
            }
        }

        public int RotateY
        {
            get => _RotateY;
            set
            {
                _RotateY = value;
                RaisePropertyChanged();
            }
        }

        public int RotateX
        {
            get => _RotateX;
            set
            {
                _RotateX = value;
                RaisePropertyChanged();
            }
        }

        public Produit SelectedVersion
        {
            get => _SelectedVersion;
            set
            {
                _SelectedVersion = value;
                if (value != null)
                {
                    UIServices.SetBusyState();
                    var index = ListVersion.IndexOf(value);
                     SelectedFicheTechnique.Produits[index] =
                        _DB2.GetFullProduct(SelectedFicheTechnique.Produits[index]);
                     DisplaySelectedFicheTek(index);
                }

                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<Produit> ListVersion
        {
            get => _ListVersion;
            set
            {
                _ListVersion = value;
                RaisePropertyChanged();
            }
        }

        public FicheTechnique SelectedFicheTechnique
        {
            get => _SelectedFicheTechnique;
            set
            {
                _SelectedFicheTechnique = value;
                RaisePropertyChanged();
                if (value != null)
                {
                    var vers = SelectedFicheTechnique.Produits.Count - 1;
                    if (SelectedFicheTechnique.Produits.Count > 0)
                        ListVersion = new MvxObservableCollection<Produit>(SelectedFicheTechnique.Produits);
                      
                    SelectedVersion =
                        ListVersion.SingleOrDefault(ver => ver.Id == SelectedFicheTechnique.Produits[vers].Id);
                   
                }
                else
                {
                    tempo = false;
                    if (UserSession.type == user.UserType.verificateur) IsVerificateur = true;
                }
            }
        }

        public DateTime DateEndLimit
        {
            get => _DateEndLimit;
            set
            {
                _DateEndLimit = value;
                RaisePropertyChanged();
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

        public MvxObservableCollection<FicheTechnique> FullFTByCatList
        {
            get => _FullFTByCatList;
            set
            {
                _FullFTByCatList = value;
                RaisePropertyChanged();
            }
        }

        public MvxObservableCollection<FicheTechnique> FullFTList
        {
            get => _FullFTList;
            set
            {
                _FullFTList = value;
                RaisePropertyChanged();
            }
        }

        public string SearchText
        {
            get => _SearchText;
            set
            {
                _SearchText = value;
                RaisePropertyChanged();
                FilterFicheTechniqueList();
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

        public bool IsSearchByCat
        {
            get => _IsSearchByCat;
            set
            {
                _IsSearchByCat = value;
                RaisePropertyChanged();
                if (value == false) SelectedCatSearch = null;
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

        public MvxObservableCollection<Couleur> ListCouleur1
        {
            get => _ListCouleur1;
            set
            {
                _ListCouleur1 = value;
                RaisePropertyChanged();
            }
        }

        public bool EditDate
        {
            get => _EditDate;
            set
            {
                _EditDate = value;
                RaisePropertyChanged();
            }
        }

        public DateTime CreationDisplayDate
        {
            get => _CreationDisplayDate;
            set
            {
                _CreationDisplayDate = value;
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

        public string ImageProd
        {
            get => _ImageProd;
            set
            {
                _ImageProd = value;
                RaisePropertyChanged();
            }
        }

        public IMvxCommand CmdSelectImage
        {
            get
            {
                _CmdSelectImage = new MvxCommand(SelectImage);
                return _CmdSelectImage;
            }
        }

        public IMvxCommand SecondPageCmd
        {
            get
            {
                _SecondPageCmd = new MvxCommand(ShowSecondPart);
                return _SecondPageCmd;
            }
        }

        public IMvxCommand FirstPageCmd
        {
            get
            {
                _FirstPageCmd = new MvxCommand(ShowFirstPart);
                return _FirstPageCmd;
            }
        }

        public IMvxCommand CmdCancelFicheTek
        {
            get
            {
                _CmdCancelFicheTek = new MvxCommand(DeleteBeforeCancel);
                return _CmdCancelFicheTek;
            }
        }

        public IMvxCommand PrintCmd
        {
            get
            {
                _PrintCmd = new MvxCommand(DisplayPrintView);
                return _PrintCmd;
            }
        }

        public IMvxCommand CmdAddNewVersion
        {
            get
            {
                _CmdAddNewVersion = new MvxCommand(AddNewVersion);
                return _CmdAddNewVersion;
            }
        }

        public bool IsDateCr
        {
            get => _IsDateCr;
            set
            {
                _IsDateCr = value;
                RaisePropertyChanged();
            }
        }

        public SecRectangle SecondRect
        {
            get => _SecondRect;
            set
            {
                _SecondRect = value;
                RaisePropertyChanged();
            }
        }

        public IMvxCommand CmdConfirmer
        {
            get
            {
                _CmdConfirmer = new MvxCommand(ConfirmFT);
                return _CmdConfirmer;
            }
        }

        public IMvxCommand CmdAddFicheTek
        {
            get
            {
                _CmdAddFicheTek = new MvxAsyncCommand(DisplayFicheTekModel);
                return _CmdAddFicheTek;
            }
        }

        public IMvxCommand CmdValiderFicheTek
        {
            get
            {
                _CmdValiderFicheTek = new MvxCommand(ValiderFicheTechnique);
                return _CmdValiderFicheTek;
            }
        }

        public IMvxCommand CmdSaveFicheTek
        {
            get
            {
                _CmdSaveFicheTek = new MvxCommand(SaveFicheTechnique);
                return _CmdSaveFicheTek;
            }
        }

        public IMvxCommand NextPage
        {
            get
            {
                _NextPage = new MvxCommand(ShowSecondPart);
                return _NextPage;
            }
        }
        private bool IsSecondPageDisplayed;

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

        public bool IsEchantillon
        {
            get => _IsEchantillon;
            set
            {
                _IsEchantillon = value;
                RaisePropertyChanged();
            }
        }

        public string OldRef
        {
            get => _OldRef;
            set
            {
                _OldRef = value;
                RaisePropertyChanged();
            }
        }

        public int OldNumArticle
        {
            get => _OldNumArticle;
            set
            {
                _OldNumArticle = value;
                RaisePropertyChanged();
            }
        }

        public int OldVersion
        {
            get => _OldVersion;
            set
            {
                _OldVersion = value;
                RaisePropertyChanged();
            }
        }

        public int MinVers
        {
            get => _MinVers;
            set
            {
                _MinVers = value;
                RaisePropertyChanged();
            }
        }

        public bool IsUpdate
        {
            get => _IsUpdate;
            set
            {
                _IsUpdate = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Composition> SchCompList
        {
            get => _SchCompList;
            set
            {
                _SchCompList = value;
                if (SchCompList != null && SchCompList.Count > 0) SetInaccessibleCompCells();
                RaisePropertyChanged();
            }
        }

        public Machine SelectedMachine
        {
            get => _SelectedMachine;
            set
            {
                _SelectedMachine = value;
                RaisePropertyChanged();
                if (SelectedMachine != null) SetupDuitageList();
            }
        }

        public void SaveTrameXPosition()
        {
            if (NewProd != null)
            {
                NewProd.EnfilageID.TrXposition = LastXposition;
                _DB2.UpdateTrameX(NewProd.EnfilageID);
            }
        }

        public void SaveTrameYPosition()
        {
            if (NewProd != null)
            {
                NewProd.EnfilageID.TrYposition = LastYposition;
                _DB2.UpdateTrameY(NewProd.EnfilageID);
            }
        }

        public override Task Initialize()
        {
            CompoList = new MvxObservableCollection<Composition>();
            NewProd = new Produit();

            CurP =
                Path.GetDirectoryName(Process.GetCurrentProcess().MainModule
                    .FileName) + "\\ImgProd\\";

            LoadLists = MvxNotifyTask.Create(MyTask);
            return base.Initialize();
        }

        private async Task MyTask()
        {
            await RefreshLists();

            SafeThEvent.Invoke(true);
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
                RaisePropertyChanged();
            }
        }
        
        public void RefreshChaine(int ChCol, int ChRow)
        {
            if (ChCol < 9)
            {
                Btn9Vis = false;
                Btn10Vis = false;
                Btn11Vis = false;
                Btn12Vis = false;
            }else if (ChCol == 9)
            {
                Btn9Vis = true;
                Btn10Vis = false;
                Btn11Vis = false;
                Btn12Vis = false;
            }
            else if(ChCol==10)
            {
                Btn9Vis = true;
                Btn10Vis = true;
                Btn11Vis = false;
                Btn12Vis = false;
            }else if(ChCol==11)
            {
                Btn9Vis = true;
                Btn10Vis = true;
                Btn11Vis = true;
                Btn12Vis = false;
            }
            else if(ChCol==12)
            {
                Btn9Vis = true;
                Btn10Vis = true;
                Btn11Vis = true;
                Btn12Vis = true;
            }
            
            
            if (ChRow > 26)
                AngleChain = 270;
            else
            {
                AngleChain = 0;
            }
            if (ChRow > 78)
            {
                SecChainVis = true;
                ChaineBoard = new ChaineBoardStructure(78, ChCol);
                ChaineList = new ObservableCollection<ChaineMatrixElement>(ChaineBoard.Board);
                ChaineBoard2 = new ChaineBoardStructure(ChRow - 78, ChCol);
                ChaineList2 = new ObservableCollection<ChaineMatrixElement>(ChaineBoard2.Board);
                ChaineRows2 = ChRow - 78;
                ChaineRows = 78;
            }
            else
            {
                SecChainVis = false;
                ChaineBoard = new ChaineBoardStructure(ChRow, ChCol);
                ChaineList = new ObservableCollection<ChaineMatrixElement>(ChaineBoard.Board);

                ChaineRows = ChRow;
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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
            }
        }
        
        private bool _Btn11Vis;

        public bool Btn11Vis
        {
            get
            {
                return _Btn11Vis;
            }
            set
            {
                _Btn11Vis = value;
                RaisePropertyChanged();
            }
        }
        
        private bool _Btn12Vis;

        public bool Btn12Vis
        {
            get
            {
                return _Btn12Vis;
            }
            set
            {
                _Btn12Vis = value;
                RaisePropertyChanged();
            }
        }

        public async Task RefreshLists()
        {
           
                ContentEnfilageList = new MvxObservableCollection<MatrixElement>();
                ChaineBoard = new ChaineBoardStructure(ChRowSum, ChaineColumns);
                ChaineList = new ObservableCollection<ChaineMatrixElement>(ChaineBoard.Board);

                ChaineBoard2 = new ChaineBoardStructure(ChaineRows2, ChaineColumns);
                ChaineList2 = new ObservableCollection<ChaineMatrixElement>(ChaineBoard2.Board);

                EnfilageBoard = new BoardStructure(EnfilageRow, EnfilageColumns);
                EnfilageList = new TrulyObservableCollection<MatrixElement>(EnfilageBoard.Board);

                CategorieList = new MvxObservableCollection<Catalogue>(_DB2.GetCategoriesWithoutChildren());
                ListClient = new MvxObservableCollection<Client>(_DB2.GetClient());
                ListVerificateur = new MvxObservableCollection<Verificateur>(_DB2.GetVerificateur());
                ListConcepteur = new MvxObservableCollection<Concepteur>(_DB2.GetConcepteurs());

                ListRedacteur = new MvxObservableCollection<Redacteur>(_DB2.GetRedacteurs());
                MachineList = new MvxObservableCollection<Machine>(_DB2.GetMachines());

                ListPeigne = new MvxObservableCollection<Reed>(_DB2.GetPeigneList());

                ListComposant = new MvxObservableCollection<Composant>(_DB2.GetComposants());
                ListTypeMatiere = new MvxObservableCollection<TypeMatiere>(_DB2.GetTypeMatieres());
                ChaineNameList = new MvxObservableCollection<chaine>(_DB2.GetChaines());
                foreach (var tm in ListTypeMatiere) tm.MatiereNom = FirstCharToUpper(tm.MatiereNom);

                await UpdateFicheTek();
        }

        public void UpdateCouleurList1()
        {
            ListCouleur1 = new MvxObservableCollection<Couleur>(_DB2.GetCouleurs(SelectedTitrage1));
        }

        public void UpdateCouleurList2()
        {
            ListCouleur2 = new MvxObservableCollection<Couleur>(_DB2.GetCouleurs(SelectedTitrage2));
        }

        public void UpdateCouleurList3()
        {
            ListCouleur3 = new MvxObservableCollection<Couleur>(_DB2.GetCouleurs(SelectedTitrage3));
        }

        public void UpdateCouleurList4()
        {
            ListCouleur4 = new MvxObservableCollection<Couleur>(_DB2.GetCouleurs(SelectedTitrage4));
        }

        public void UpdateCouleurList5()
        {
            ListCouleur5 = new MvxObservableCollection<Couleur>(_DB2.GetCouleurs(SelectedTitrage5));
        }

        public void UpdateCouleurList6()
        {
            ListCouleur6 = new MvxObservableCollection<Couleur>(_DB2.GetCouleurs(SelectedTitrage6));
        }

        public void UpdateCouleurList7()
        {
            ListCouleur7 = new MvxObservableCollection<Couleur>(_DB2.GetCouleurs(SelectedTitrage7));
        }

        public void UpdateCouleurList8()
        {
            ListCouleur8 = new MvxObservableCollection<Couleur>(_DB2.GetCouleurs(SelectedTitrage8));
        }

        public void UpdateCouleurList9()
        {
            ListCouleur9 = new MvxObservableCollection<Couleur>(_DB2.GetCouleurs(SelectedTitrage9));
        }

        public void CallWorkspace()
        {
            try
            {
                if(SelectedChaine!=null)
                {
                    var b = true;
                    var sumNbrFil = 0;
                    foreach (var sch in SchCompList)
                        if (sch.NbrFil == 0)
                            b = true;
                        else
                            sumNbrFil = sumNbrFil + sch.NbrFil;

                    if (b)
                    {
                        if (WorkRectan != null)
                        {
                            if (SelectedChaine.Colonne != WorkRectan.PartHeight - 4) SetupEnfilageWorkspace(sumNbrFil);
                        }
                        else
                        {
                            SetupEnfilageWorkspace(sumNbrFil);
                        }
                    }
                }
               
            }
            catch(Exception ex)
            {
                SendNotification.Raise(ex.ToString());
            }
            
           
        }
        private bool IsNewDatasheet=true;

        public void EditFicheTechnique()
        {
            try
            {
                IsNewDatasheet = false;
                if (SelectedFicheTechnique != null)
                {
                    var LastVersion = SelectedFicheTechnique.Produits.Count - 1;

                    if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                    {
                        IsEnfilage = true;
                        if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine != null)
                            SelectedChaine = ChaineNameList.SingleOrDefault(ch =>
                                ch.ID == SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine.ID);
                    }
                    else
                    {
                        IsEnfilage = false;
                    }

                    IsDentFilVis = true;
                    if (SelectedChaine != null)
                    {


                        SetInaccessibleCompCells();

                        CallWorkspace();
                    }


                    EditDate = true;
                    DisplayDate = false;
                    EnableEditing = true;
                    SaveCancelBtn = true;
                    PrintBtn = false;
                    BtnVis = true;
                    IsAddEnabled = false;
                    OldVersion = NewProd.Version;
                    if (SelectedFicheTechnique.Produits[LastVersion].DuitageID != null)
                        if (SelectedFicheTechnique.Produits[LastVersion].DuitageID.Machine != null)
                        {
                            SelectedMachine = MachineList.SingleOrDefault(ma =>
                                ma.ID == SelectedFicheTechnique.Produits[LastVersion].DuitageID.Machine.ID);
                            NewProd.DuitageID = DuitageList.SingleOrDefault(duit =>
                                duit.ID == SelectedFicheTechnique.Produits[LastVersion].DuitageID.ID);
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
                            IsUpdate = false;
                        else
                            IsUpdate = true;
                    }

                    if (SelectedFicheTechnique.Produits[LastVersion].Client != null)
                        NewProd.Client = ListClient.SingleOrDefault(cl =>
                            cl.ID == SelectedFicheTechnique.Produits[LastVersion].Client.ID);
                    if (SelectedFicheTechnique.Produits[LastVersion].PeigneObj != null)
                        NewProd.PeigneObj = ListPeigne.SingleOrDefault(rd =>
                            rd.ID == SelectedFicheTechnique.Produits[LastVersion].PeigneObj.ID);
                    if (SelectedFicheTechnique.Produits[LastVersion].RedacteurObj != null)
                        NewProd.RedacteurObj = ListRedacteur.SingleOrDefault(red =>
                            red.ID == SelectedFicheTechnique.Produits[LastVersion].RedacteurObj.ID);
                    if (SelectedFicheTechnique.Produits[LastVersion].Concepteur != null)
                        NewProd.Concepteur = ListConcepteur.SingleOrDefault(cp =>
                            cp.ID == SelectedFicheTechnique.Produits[LastVersion].Concepteur.ID);
                    if (SelectedFicheTechnique.Produits[LastVersion].Verificateur != null)
                        NewProd.Verificateur = ListVerificateur.SingleOrDefault(vr =>
                            vr.ID == SelectedFicheTechnique.Produits[LastVersion].Verificateur.ID);

                    if (NewProd.DuitageID != null) Vitesse = NewProd.DuitageID.Vitesse.ToString();


                    for (var i = 0; i < NewProd.GetComposition.Count; i++)
                        if (NewProd.GetComposition[i].GetComposant != null)
                        {
                            if (NewProd.GetComposition[i].NumComposant == 8)
                            {
                                Comp8.GetComposant = ListComposant.SingleOrDefault(
                                    cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                                Comp8Vis = true;
                                ChangeImageBtn8 = "../Asset/remove64.png";
                                if (Comp8.GetMatiere != null)
                                {
                                    SelectedTypeMatiere8 = ListTypeMatiere.SingleOrDefault(ma =>
                                        ma.ID == Comp8.GetMatiere.Titrage.TypeMatiere.ID);
                                    SelectedTitrage8 =
                                        ListTitrage8.SingleOrDefault(tit => tit.ID == Comp8.GetMatiere.Titrage.ID);
                                    SelectedColor8 =
                                        ListCouleur8.SingleOrDefault(co => co.ID == Comp8.GetMatiere.GetCouleur.ID);
                                }

                                Comp8.PropertyChanged += Comp8_PropertyChanged;
                            }
                            else if (NewProd.GetComposition[i].NumComposant == 9)
                            {
                                Comp9.GetComposant = ListComposant.SingleOrDefault(
                                    cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                                Comp9Vis = true;
                                ChangeImageBtn9 = "../Asset/remove64.png";
                                if (Comp9.GetMatiere != null)
                                {
                                    SelectedTypeMatiere9 = ListTypeMatiere.SingleOrDefault(ma =>
                                        ma.ID == Comp9.GetMatiere.Titrage.TypeMatiere.ID);
                                    SelectedTitrage9 =
                                        ListTitrage9.SingleOrDefault(tit => tit.ID == Comp9.GetMatiere.Titrage.ID);
                                    SelectedColor9 =
                                        ListCouleur9.SingleOrDefault(co => co.ID == Comp9.GetMatiere.GetCouleur.ID);
                                }

                                Comp9.PropertyChanged += Comp9_PropertyChanged;
                            }
                            else
                            {
                                if (NewProd.GetComposition[i].NumComposant == 1)
                                {
                                    Comp1.GetComposant = ListComposant.SingleOrDefault(
                                        cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                                    Comp1Vis = true;
                                    ChangeImageBtn1 = "../Asset/remove64.png";
                                    if (Comp1.GetMatiere != null)
                                    {
                                        SelectedTypeMatiere1 = ListTypeMatiere.SingleOrDefault(ma =>
                                            ma.ID == Comp1.GetMatiere.Titrage.TypeMatiere.ID);
                                        SelectedTitrage1 =
                                            ListTitrage1.SingleOrDefault(tit => tit.ID == Comp1.GetMatiere.Titrage.ID);
                                        SelectedColor1 =
                                            ListCouleur1.SingleOrDefault(co => co.ID == Comp1.GetMatiere.GetCouleur.ID);
                                    }

                                    Comp1.PropertyChanged += Comp1_PropertyChanged;
                                }

                                if (NewProd.GetComposition[i].NumComposant == 2)
                                {
                                    Comp2.GetComposant = ListComposant.SingleOrDefault(
                                        cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                                    Comp2Vis = true;
                                    ChangeImageBtn2 = "../Asset/remove64.png";
                                    if (Comp2.GetMatiere != null)
                                    {
                                        SelectedTypeMatiere2 = ListTypeMatiere.SingleOrDefault(ma =>
                                            ma.ID == Comp2.GetMatiere.Titrage.TypeMatiere.ID);
                                        SelectedTitrage2 =
                                            ListTitrage2.SingleOrDefault(tit => tit.ID == Comp2.GetMatiere.Titrage.ID);
                                        SelectedColor2 =
                                            ListCouleur2.SingleOrDefault(co => co.ID == Comp2.GetMatiere.GetCouleur.ID);
                                    }

                                    Comp2.PropertyChanged += Comp2_PropertyChanged;
                                }

                                if (NewProd.GetComposition[i].NumComposant == 3)
                                {
                                    Comp3.GetComposant = ListComposant.SingleOrDefault(
                                        cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                                    Comp3Vis = true;
                                    ChangeImageBtn3 = "../Asset/remove64.png";
                                    if (Comp3.GetMatiere != null)
                                    {
                                        SelectedTypeMatiere3 = ListTypeMatiere.SingleOrDefault(ma =>
                                            ma.ID == Comp3.GetMatiere.Titrage.TypeMatiere.ID);
                                        SelectedTitrage3 =
                                            ListTitrage3.SingleOrDefault(tit => tit.ID == Comp3.GetMatiere.Titrage.ID);
                                        SelectedColor3 =
                                            ListCouleur3.SingleOrDefault(co => co.ID == Comp3.GetMatiere.GetCouleur.ID);
                                    }

                                    Comp3.PropertyChanged += Comp3_PropertyChanged;
                                }

                                if (NewProd.GetComposition[i].NumComposant == 4)
                                {
                                    Comp4.GetComposant = ListComposant.SingleOrDefault(
                                        cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                                    Comp4Vis = true;
                                    ChangeImageBtn4 = "../Asset/remove64.png";
                                    if (Comp4.GetMatiere != null)
                                    {
                                        SelectedTypeMatiere4 = ListTypeMatiere.SingleOrDefault(ma =>
                                            ma.ID == Comp4.GetMatiere.Titrage.TypeMatiere.ID);
                                        SelectedTitrage4 =
                                            ListTitrage4.SingleOrDefault(tit => tit.ID == Comp4.GetMatiere.Titrage.ID);
                                        SelectedColor4 =
                                            ListCouleur4.SingleOrDefault(co => co.ID == Comp4.GetMatiere.GetCouleur.ID);
                                    }

                                    Comp4.PropertyChanged += Comp4_PropertyChanged;
                                }

                                if (NewProd.GetComposition[i].NumComposant == 5)
                                {
                                    Comp5.GetComposant = ListComposant.SingleOrDefault(
                                        cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                                    Comp5Vis = true;
                                    ChangeImageBtn5 = "../Asset/remove64.png";
                                    if (Comp5.GetMatiere != null)
                                    {
                                        SelectedTypeMatiere5 = ListTypeMatiere.SingleOrDefault(ma =>
                                            ma.ID == Comp5.GetMatiere.Titrage.TypeMatiere.ID);
                                        SelectedTitrage5 =
                                            ListTitrage5.SingleOrDefault(tit => tit.ID == Comp5.GetMatiere.Titrage.ID);
                                        SelectedColor5 =
                                            ListCouleur5.SingleOrDefault(co => co.ID == Comp5.GetMatiere.GetCouleur.ID);
                                    }

                                    Comp5.PropertyChanged += Comp5_PropertyChanged;
                                }

                                if (NewProd.GetComposition[i].NumComposant == 6)
                                {
                                    Comp6.GetComposant = ListComposant.SingleOrDefault(
                                        cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                                    Comp6Vis = true;
                                    ChangeImageBtn6 = "../Asset/remove64.png";
                                    if (Comp6.GetMatiere != null)
                                    {
                                        SelectedTypeMatiere6 = ListTypeMatiere.SingleOrDefault(ma =>
                                            ma.ID == Comp6.GetMatiere.Titrage.TypeMatiere.ID);
                                        SelectedTitrage6 =
                                            ListTitrage6.SingleOrDefault(tit => tit.ID == Comp6.GetMatiere.Titrage.ID);
                                        SelectedColor6 =
                                            ListCouleur6.SingleOrDefault(co => co.ID == Comp6.GetMatiere.GetCouleur.ID);
                                    }

                                    Comp6.PropertyChanged += Comp6_PropertyChanged;
                                }

                                if (NewProd.GetComposition[i].NumComposant == 7)
                                {
                                    Comp7.GetComposant = ListComposant.SingleOrDefault(
                                        cmp => cmp.ID == NewProd.GetComposition[i].GetComposant.ID);
                                    Comp7Vis = true;
                                    ChangeImageBtn7 = "../Asset/remove64.png";
                                    if (Comp7.GetMatiere != null)
                                    {
                                        SelectedTypeMatiere7 = ListTypeMatiere.SingleOrDefault(ma =>
                                            ma.ID == Comp7.GetMatiere.Titrage.TypeMatiere.ID);
                                        SelectedTitrage7 =
                                            ListTitrage7.SingleOrDefault(tit => tit.ID == Comp7.GetMatiere.Titrage.ID);
                                        SelectedColor7 =
                                            ListCouleur7.SingleOrDefault(co => co.ID == Comp7.GetMatiere.GetCouleur.ID);
                                    }

                                    Comp7.PropertyChanged += Comp7_PropertyChanged;
                                }
                            }
                        }


                    if (SelectedFicheTechnique.Catalog != null)
                        SelectedCategorie =
                            CategorieList.SingleOrDefault(cat => cat.ID == SelectedFicheTechnique.Catalog.ID);


                    NewProd.PropertyChanged += Prod_PropertyChanged;
                }
                else
                {
                    SendNotification.Raise("Aucune Fiche Technique Séléctionnée");
                }
            }
            catch(Exception ex)
            {
                SendNotification.Raise(ex.ToString());
            }
           
          
            
        }

        public void DeleteFicheTechnique()
        {
            if (SelectedFicheTechnique != null)
            {
                var req = new YesNoQuestion
                {
                    Question = "êtes-vous sûr de vouloir supprimer cette Fiche Technique séléctionnée ?",

                    UploadCallback = ok =>
                    {
                        if (ok)
                        {
                            try
                            {
                                foreach (var prod in SelectedFicheTechnique.Produits)
                                    if (prod.EnfilageID != null)
                                        _DB2.DeleteFicheTechnique(SelectedFicheTechnique.ID, prod.Id, prod.EnfilageID.ID);
                                    else
                                        _DB2.DeleteFicheTechnique(SelectedFicheTechnique.ID, prod.Id);

                                AnnulerFicheTechnique();
                                UpdateFicheTek();
                            }
                            catch(Exception ex)
                            {
                                SendNotification.Raise(ex.ToString());
                            }
                            
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
            }
            else if (e.PropertyName.Equals("NumArticle"))
            {
                if (NewProd.NumArticle != OldNumArticle)
                {
                    if (_DB2.CheckNArticleUnique(NewProd.NumArticle, NewProd.Id))
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
            }
            else if (e.PropertyName.Equals("Version"))
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
                        NewProd.Version = OldVersion;
                    }
                }
            }
            else if (e.PropertyName.Equals("Ref"))
            {
                _DB2.UpdateProdRef(NewProd);
                UpdateFicheTek();
            }
            else if (e.PropertyName.Equals("EnfDent"))
            {
                _DB2.UpdateEnfDent(NewProd);
            }
            else if (e.PropertyName.Equals("Name"))
            {
                if (mf != null && mf.IsEchantillon == false)
                    if (NewProd.FicheTekID != null)
                        if (NewProd.FicheTekID.Catalog != null)
                        {
                            var ord = _DB2.AssignOrderToFicheTechnique(NewProd.FicheTekID.ID,
                                NewProd.FicheTekID.Catalog.ID);
                            Ordre = ord.ToString();
                        }

                _DB2.UpdateProdName(NewProd);
                UpdateFicheTek();
            }
            else if (e.PropertyName.Equals("Name2"))
            {
                _DB2.UpdateProdName2(NewProd);
            }
            else if (e.PropertyName.Equals("Client"))
            {
                _DB2.UpdateProdClient(NewProd);
            }
            else if (e.PropertyName.Equals("Largeur"))
            {
                if (mf != null && mf.IsEchantillon == false)
                    if (NewProd.FicheTekID != null)
                        if (NewProd.FicheTekID.Catalog != null)
                        {
                            var ord = _DB2.AssignOrderToFicheTechnique(NewProd.FicheTekID.ID,
                                NewProd.FicheTekID.Catalog.ID);
                            Ordre = ord.ToString();
                        }

                _DB2.UpdateProdLargeur(NewProd);
            }
            else if (e.PropertyName.Equals("Epaisseur"))
            {
                if (mf != null && mf.IsEchantillon == false)
                    if (NewProd.FicheTekID != null)
                        if (NewProd.FicheTekID.Catalog != null)
                        {
                            var ord = _DB2.AssignOrderToFicheTechnique(NewProd.FicheTekID.ID,
                                NewProd.FicheTekID.Catalog.ID);
                            Ordre = ord.ToString();
                        }

                _DB2.UpdateProdEpaiseur(NewProd);
            }
            else if (e.PropertyName.Equals("DuitageID"))
            {
                _DB2.UpdateProdDuitage(NewProd);
            }
            else if (e.PropertyName.Equals("DuitageGomme"))
            {
                _DB2.UpdateProdDuitageGomme(NewProd);
            }
            else if (e.PropertyName.Equals("Dent"))
            {
                _DB2.UpdateProdDent(NewProd);
            }
            else if (e.PropertyName.Equals("PeigneObj"))
            {
                _DB2.UpdateProdPeigne(NewProd);
            }
            else if (e.PropertyName.Equals("IsEnfilage"))
            {
                _DB2.UpdateProdIsEnfilage(NewProd);
                if (IsEnfilage)
                {
                     if (Comp1 != null) AddLegend(1);
                    if (Comp2 != null) AddLegend(2);
                    if (Comp3 != null) AddLegend(3);
                    if (Comp4 != null)
                        AddLegend(4);
                    else if (Comp5 != null) AddLegend(5);
                    if (Comp6 != null) AddLegend(6);
                    if (Comp7 != null) AddLegend(7);
                    NewProd.EnfilageID = new Enfilage();
                    NewProd.EnfilageID.Column = EnfilageColumns;
                    NewProd.EnfilageID.Row = EnfilageRow;
                    NewProd.EnfilageID.TrXposition = LastXposition;
                    NewProd.EnfilageID.TrYposition = LastYposition;
                    if(NewProd.EnfilageID.GetChaine!=null)
                    {
                        NewProd.EnfilageID.ID = _DB2.AddNewEnfilageWithChaine(NewProd.EnfilageID);
                    }
                    else
                    {
                        NewProd.EnfilageID.ID = _DB2.AddNewEnfilage(NewProd.EnfilageID);
                    }

                    _DB2.UpdateProdEnfilage(NewProd);


                }
                else
                {
                    SchCompList = new MvxObservableCollection<Composition>();
                    _DB2.RemoveEnfilage(NewProd.EnfilageID);
                    NewProd.EnfilageID = null;
                    _DB2.UpdateProdRemoveEnfilage(NewProd);
                }
                
                   
                
            }
            else if (e.PropertyName.Equals("Concepteur"))
            {
                _DB2.UpdateProdConcepteur(NewProd);
            }
            else if (e.PropertyName.Equals("Verificateur"))
            {
                _DB2.UpdateProdVerificateur(NewProd);
            }
            else if (e.PropertyName.Equals("Redaction"))
            {
                _DB2.UpdateProdRedaction(NewProd);
            }
            else if (e.PropertyName.Equals("DateCreation"))
            {
                _DB2.UpdateProdDateCreation(NewProd);
            }
            else if (e.PropertyName.Equals("MiseAJour"))
            {
                _DB2.UpdateProdMiseAJour(NewProd);
            }
            else if (e.PropertyName.Equals("RedacteurObj"))
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
                    FullFTByCatList = FullFTList;
                    FicheTechniqueList = FullFTList;
                }
                else if (IsProdFT)
                {
                    FullFTList = new MvxObservableCollection<FicheTechnique>(_DB2.GetFicheTechniquesProd());
                    FullFTByCatList = FullFTList;
                    FicheTechniqueList = FullFTList;
                }
                else
                {
                    FullFTList = new MvxObservableCollection<FicheTechnique>(_DB2.GetFicheTechniquesEch());
                    FullFTByCatList = FullFTList;
                    FicheTechniqueList = FullFTList;
                }
            });
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

        public void GetCompositions()
        {
            TotalPoids = 0;
            
            if(NewProd.GetComposition==null || NewProd.GetComposition.Count<=0)
                return;
            for (var i = 0; i < NewProd.GetComposition.Count; i++)
                    if (NewProd.GetComposition[i].GetComposant != null)
                    {
                        TotalPoids = TotalPoids + NewProd.GetComposition[i].Poids;
                        if (NewProd.GetComposition[i].NumComposant == 8)
                        {
                            Comp8 = (Composition)NewProd.GetComposition[i].Clone();
                        }
                        else if (NewProd.GetComposition[i].NumComposant == 9)
                        {
                            Comp9 = (Composition)NewProd.GetComposition[i].Clone();
                        }
                        else
                        {
                            if (NewProd.GetComposition[i].NumComposant == 1)
                            {
                                Comp1 = (Composition)NewProd.GetComposition[i].Clone();
                                if (NewProd.IsEnfilage == 1) SchCompList.Add(Comp1);
                            }

                            if (NewProd.GetComposition[i].NumComposant == 2)
                            {
                                Comp2 = (Composition)NewProd.GetComposition[i].Clone();
                                if (NewProd.IsEnfilage == 1) SchCompList.Add(Comp2);
                            }

                            if (NewProd.GetComposition[i].NumComposant == 3)
                            {
                                Comp3 = (Composition)NewProd.GetComposition[i].Clone();
                                if (NewProd.IsEnfilage == 1) SchCompList.Add(Comp3);
                            }

                            if (NewProd.GetComposition[i].NumComposant == 4)
                            {
                                Comp4 = (Composition)NewProd.GetComposition[i].Clone();
                                if (NewProd.IsEnfilage == 1) SchCompList.Add(Comp4);
                            }

                            if (NewProd.GetComposition[i].NumComposant == 5)
                            {
                                Comp5 = (Composition)NewProd.GetComposition[i].Clone();
                                if (NewProd.IsEnfilage == 1) SchCompList.Add(Comp5);
                            }

                            if (NewProd.GetComposition[i].NumComposant == 6)
                            {
                                Comp6 = (Composition)NewProd.GetComposition[i].Clone();
                                if (NewProd.IsEnfilage == 1) SchCompList.Add(Comp6);
                            }

                            if (NewProd.GetComposition[i].NumComposant == 7)
                            {
                                Comp7 = (Composition)NewProd.GetComposition[i].Clone();
                                if (NewProd.IsEnfilage == 1) SchCompList.Add(Comp7);
                            }
                        }
                    }
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
                }
                else if (SelectedFicheTechnique.ModelFiche == (int)ModelFiche.ModelFicheTek.FicheTekEHC)
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

                if (SelectedFicheTechnique.Ordre != 0) Ordre = SelectedFicheTechnique.Ordre.ToString();
                if (SelectedFicheTechnique.Produits[vers].Definite == 0)
                {
                    IsPerm = false;
                    IsConfirm = false;
                }
                else
                {
                    if (UserSession.type == user.UserType.redacteur &&
                        SelectedFicheTechnique.Produits[vers].Version != 0)
                    {
                        IsPerm = true;
                        IsConfirm = false;
                    }
                    else if (SelectedFicheTechnique.Produits[vers].Version == 0)
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
                    if (UserSession.type == user.UserType.verificateur) IsVerificateur = true;
                }
                else
                {
                    tempo = false;
                    if (UserSession.type == user.UserType.verificateur) IsVerificateur = false;
                }

                ImageProd = null;
                if (SelectedFicheTechnique.Produits[vers].image != null)
                    ImageProd = CurP + SelectedFicheTechnique.Produits[vers].image;


                NewProd = (Produit)SelectedFicheTechnique.Produits[vers].Clone();

                if (SelectedFicheTechnique.ModelFiche == (int)ModelFiche.ModelFicheTek.FicheTekCrochetage)
                {
                    IsFicheCrochetage = true;
                    IsFicheUniDuitage = false;
                    IsEnfilage = false;
                }
                else if (SelectedFicheTechnique.ModelFiche == (int)ModelFiche.ModelFicheTek.FicheTekEHC)
                {
                    IsFicheEHC = true;
                    IsFicheNormal = false;
                }

                ResetComposition();
                GetCompositions();

                rep1 = new Repitition();
                rep2 = new Repitition();
                rep3 = new Repitition();
                rep4 = new Repitition();
                rep5 = new Repitition();

                if (SelectedFicheTechnique.Produits[vers].EnfilageID != null)
                {
                   
                    List<Repitition> replist = _DB2.getRepition(SelectedFicheTechnique.Produits[vers].EnfilageID.ID);
                    for (int i=0;i<replist.Count;i++ )
                    {
                        if (i == 0)
                        {
                            rep1=replist[i];
                            rep1.vis = true;
                            if (rep1.x == null || rep1.y==null)
                            {
                                rep1.x = "0";
                                rep1.y= "0";
                            }
                            else
                            {
                                double nbconv =0;

                                bool IsParsable=Double.TryParse(rep1.x, out nbconv);
                                if(IsParsable)
                                {
                                    var str = (nbconv * ContainerWidth).ToString();
                                    rep1.x = str;
                                    rep1.y = (Convert.ToDouble(rep1.y) * ContainerHeight).ToString();
                                }
                              
                            }
                        }else if (i == 1)
                        {
                            rep2=replist[i];
                            rep2.vis = true;
                            if (rep2.x == null || rep2.y==null)
                            {
                                rep2.x = "0";
                                rep2.y= "0";
                            } else
                            {
                                rep2.x = (Convert.ToDouble(rep2.x) * ContainerWidth).ToString();
                                rep2.y = (Convert.ToDouble(rep2.y) * ContainerHeight).ToString();
                            }
                        }else if (i == 2)
                        {
                            rep3=replist[i];
                            rep3.vis = true;
                            if (rep3.x == null || rep3.y==null)
                            {
                                rep3.x = "0";
                                rep3.y= "0";
                            }else
                            {
                                rep3.x = (Convert.ToDouble(rep3.x) * ContainerWidth).ToString();
                                rep3.y = (Convert.ToDouble(rep3.y) * ContainerHeight).ToString();
                            }
                        }else if (i == 3)
                        {
                            rep4=replist[i];
                            rep4.vis = true;
                            if (rep4.x == null || rep4.y==null)
                            {
                                rep4.x = "0";
                                rep4.y= "0";
                            }else
                            {
                                rep4.x = (Convert.ToDouble(rep4.x) * ContainerWidth).ToString();
                                rep4.y = (Convert.ToDouble(rep4.y) * ContainerHeight).ToString();
                            }
                        }else if (i == 4)
                        {
                            rep5=replist[i];
                            rep5.vis = true;
                            if (rep5.x == null || rep5.y==null)
                            {
                                rep5.x = "0";
                                rep5.y= "0";
                            }else
                            {
                                rep5.x = (Convert.ToDouble(rep5.x) * ContainerWidth).ToString();
                                rep5.y = (Convert.ToDouble(rep5.y) * ContainerHeight).ToString();
                            }
                        }
                        
                    }

                    
                    if (SelectedFicheTechnique.Produits[vers].EnfilageID.GetChaine != null)
                    {
                        ChcolList =new ObservableCollection<ChColComp>(SelectedFicheTechnique.Produits[vers].EnfilageID.GetChaine.ChaineCompos) ;
                        SetupChaine(SelectedFicheTechnique.Produits[vers].EnfilageID.GetChaine.Colonne,
                            SelectedFicheTechnique.Produits[vers].EnfilageID.GetChaine.Ligne,
                            SelectedFicheTechnique.Produits[vers].EnfilageID.GetChaine.ChMatrix);
                      
                    }
                    else

                    {
                        ChaineList = new ObservableCollection<ChaineMatrixElement>();
                        ChaineList2 = new ObservableCollection<ChaineMatrixElement>();

                        ChaineColumns = 8;
                        ChaineRows = 8;
                        ChaineRows2 = 0;
                        ChRowSum = 0;
                    }

                    var TempList = new List<MatrixElement>();

                    foreach (var EleMatx in SelectedFicheTechnique.Produits[vers].EnfilageID.GetMatrix)
                    {
                        var EnfMatrix = new MatrixElement(EleMatx.x, EleMatx.y);

                        EnfMatrix.Content = EleMatx.value;
                        EnfMatrix.DentFil = EleMatx.DentFil;
                        TempList.Add(EnfMatrix);
                    }
                    double TrPos = 0;
                    bool b=Double.TryParse(SelectedFicheTechnique.Produits[vers].EnfilageID.TrXposition, out TrPos);
                    if(b)
                    {
                        TrameXposition =
                                               (
                                                   Convert.ToDouble(SelectedFicheTechnique.Produits[vers].EnfilageID.TrXposition) * ContainerWidth)
                                               .ToString();
                        TrameYposition = (Convert.ToDouble(SelectedFicheTechnique.Produits[vers].EnfilageID.TrYposition) * ContainerHeight).ToString();

                    }else
                    {

                    }

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
                    CategorieName = SelectedFicheTechnique.Catalog.Designation;
                else
                    CategorieName = "";
            }
        }

        public void FilterFicheTechniqueList()
        {
            int ConvertedSearchT;
            if (int.TryParse(SearchText, out ConvertedSearchT))
            {
                FicheTechniqueList = new MvxObservableCollection<FicheTechnique>();

                foreach (var ft in FullFTByCatList)
                    if (ft.Produits[ft.Produits.Count - 1].Client != null &&
                        ft.Produits[ft.Produits.Count - 1].Name2 != null)
                    {
                        if (ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Name2.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Client.Name.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].NumArticle == ConvertedSearchT
                           )
                            FicheTechniqueList.Add(ft);
                    }
                    else if (ft.Produits[ft.Produits.Count - 1].Client != null)
                    {
                        if (ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Client.Name.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].NumArticle == ConvertedSearchT
                           )
                            FicheTechniqueList.Add(ft);
                    }
                    else if (ft.Produits[ft.Produits.Count - 1].Name2 != null)
                    {
                        if (ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Name2.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].NumArticle == ConvertedSearchT
                           )
                            FicheTechniqueList.Add(ft);
                    }
                    else
                    {
                        if (ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].NumArticle == ConvertedSearchT
                           )
                            FicheTechniqueList.Add(ft);
                    }
            }
            else
            {
                FicheTechniqueList = new MvxObservableCollection<FicheTechnique>();

                foreach (var ft in FullFTByCatList)
                    if (ft.Produits[ft.Produits.Count - 1].Client != null &&
                        ft.Produits[ft.Produits.Count - 1].Name2 != null)
                    {
                        if (ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Name2.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Client.Name.ToLower().Contains(SearchText.ToLower()))
                            FicheTechniqueList.Add(ft);
                    }
                    else if (ft.Produits[ft.Produits.Count - 1].Client != null)
                    {
                        if (ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Client.Name.ToLower().Contains(SearchText.ToLower()))
                            FicheTechniqueList.Add(ft);
                    }
                    else if (ft.Produits[ft.Produits.Count - 1].Name2 != null)
                    {
                        if (ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Name2.ToLower().Contains(SearchText.ToLower())
                           )
                            FicheTechniqueList.Add(ft);
                    }
                    else
                    {
                        if (ft.Produits[ft.Produits.Count - 1].Name.ToLower().Contains(SearchText.ToLower())
                            || ft.Produits[ft.Produits.Count - 1].Ref.ToLower().Contains(SearchText.ToLower())
                           )
                            FicheTechniqueList.Add(ft);
                    }
            }
        }

        public void SelectImage()
        {
            var req = new LoadedImage
            {
                UploadCallback = (imageP, imageN, ok) =>
                {
                    if (ok)
                    {
                        var CurPath =
                            Path.GetDirectoryName(Process.GetCurrentProcess().MainModule
                                .FileName) + "\\ImgProd\\";
                        Directory.CreateDirectory(CurPath);
                        if (File.Exists(CurPath + imageN) == false) File.Copy(imageP, CurPath + imageN);

                        ImageProd = CurPath + imageN;
                        NewProd.image = imageN;
                        _DB2.UpdateProdImage(NewProd);
                    }
                }
            };
            GetImagePath.Raise(req);
        }

        public void DeleteBeforeCancel()
        {
            IsCreateChaine = false;
            if (IsNewDatasheet)
            {
                if (NewProd.IsEnfilage == 1)
                    _DB2.DeleteFicheTechnique(NewProd.FicheId, NewProd.Id, NewProd.EnfilageID.ID);
                else
                    _DB2.DeleteFicheTechnique(NewProd.FicheId, NewProd.Id);
            }
            


            AnnulerFicheTechnique();
            UpdateFicheTek();
            SelectedFicheTechnique = null;
        }

        public void DisplayPrintView()
        {
            UIServices.SetBusyState();
            if(SelectedFicheTechnique!=null)
            {
                if(IsFicheEHC)
                {
                    NewProd.modelFiche = ModelFiche.ModelFicheTek.FicheTekEHC;
                }
                else if(IsFicheCrochetage)
                {
                    NewProd.modelFiche = ModelFiche.ModelFicheTek.FicheTekCrochetage;
                }
                else
                {
                    NewProd.modelFiche = ModelFiche.ModelFicheTek.FicheTekNormal;
                }
               
                _NavigationService.Navigate<PrintViewModel, Produit>(NewProd);
            }else
            {
                SendNotification.Raise("Séléctionnez une fiche technique");
            }
            
        }

        public void AddNewVersion()
        {
            try
            {
                UIServices.SetBusyState();
                SchCompList = new MvxObservableCollection<Composition>();
                if (SelectedFicheTechnique != null)
                {
                    IsProduction = true;
                    IsDateCr = false;
                    IsUpdate = true;
                    MinVers = 1;
                    var LastVersion = SelectedFicheTechnique.Produits.Count - 1;


                    NewProd.Id = 0;
                    NewProd.Version = NewProd.Version + 1;
                    var prod = (Produit)NewProd.Clone();

                    OldVersion = NewProd.Version;
                    NewProd.Id = _DB2.AddNewProductVersion(prod);
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

                    if (prod.DuitageID != null)
                        if (SelectedFicheTechnique.Produits[LastVersion].DuitageID.Machine != null)
                        {
                            _DB2.UpdateProdDuitage(prod);
                            SelectedMachine = MachineList.SingleOrDefault(ma =>
                                ma.ID == SelectedFicheTechnique.Produits[LastVersion].DuitageID.Machine.ID);
                            NewProd.DuitageID = DuitageList.SingleOrDefault(cl => cl.ID == prod.DuitageID.ID);
                        }



                    if (prod.Epaisseur > 0) _DB2.UpdateProdEpaiseur(prod);

                    if (SelectedFicheTechnique.Catalog != null)
                        SelectedCategorie =
                            CategorieList.SingleOrDefault(cat => cat.ID == SelectedFicheTechnique.Catalog.ID);



                    //NewProd.MiseAJour=DateTime.Now;
                    IsDentFilVis = true;
                    EditDate = true;
                    DisplayDate = false;
                    EnableEditing = true;
                    SaveCancelBtn = true;
                    PrintBtn = false;
                    BtnVis = true;
                    IsAddEnabled = false;


                    if (NewProd.DuitageID != null) Vitesse = NewProd.DuitageID.Vitesse.ToString();

                    var EquivList = new List<EquivalentID>();

                    var NewTemp = new List<Composition>(NewProd.GetComposition.Count);
                    NewProd.GetComposition.ForEach(item => { NewTemp.Add((Composition)item.Clone()); });
                    for (var i = 0; i < NewTemp.Count; i++)
                        if (NewTemp[i].GetComposant != null)
                        {
                            if (NewTemp[i].NumComposant == 8)
                            {
                                Comp8Vis = true;
                                ChangeImageBtn8 = "../Asset/remove64.png";


                                Comp8 = (Composition)NewTemp[i].Clone();
                                var eqid = new EquivalentID();
                                eqid.Id = Comp8.ID;

                                Comp8.ProdID = (Produit)NewProd.Clone();
                                Comp8.ID = 0;
                                Comp8.ID = _DB2.AddProdCompo((Composition)Comp8.Clone());
                                eqid.EquivId = Comp8.ID;
                                EquivList.Add(eqid);
                                if (NewTemp[i].GetMatiere != null)
                                {
                                    _DB2.UpdateCompoMatiere(Comp8);
                                    SelectedTypeMatiere8 = ListTypeMatiere.SingleOrDefault(ma =>
                                        ma.ID == Comp8.GetMatiere.Titrage.TypeMatiere.ID);
                                    SelectedTitrage8 =
                                        ListTitrage8.SingleOrDefault(tit => tit.ID == Comp8.GetMatiere.Titrage.ID);
                                    SelectedColor8 =
                                        ListCouleur8.SingleOrDefault(co => co.ID == Comp8.GetMatiere.GetCouleur.ID);
                                }

                                if (NewTemp[i].GetComposant != null)
                                {
                                    _DB2.UpdateCompoComposant(Comp8);

                                    Comp8.GetComposant =
                                        ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                }

                                Comp8.PropertyChanged += Comp8_PropertyChanged;
                            }
                            else if (NewTemp[i].NumComposant == 9)
                            {
                                Comp9Vis = true;
                                ChangeImageBtn9 = "../Asset/remove64.png";


                                Comp9 = (Composition)NewTemp[i].Clone();
                                var eqid = new EquivalentID();
                                eqid.Id = Comp9.ID;
                                Comp9.ProdID = (Produit)NewProd.Clone();
                                Comp9.ID = 0;
                                Comp9.ID = _DB2.AddProdCompo((Composition)Comp9.Clone());
                                eqid.EquivId = Comp9.ID;
                                EquivList.Add(eqid);
                                if (NewTemp[i].GetMatiere != null)
                                {
                                    _DB2.UpdateCompoMatiere(Comp9);
                                    SelectedTypeMatiere9 = ListTypeMatiere.SingleOrDefault(ma =>
                                        ma.ID == Comp9.GetMatiere.Titrage.TypeMatiere.ID);
                                    SelectedTitrage9 =
                                        ListTitrage9.SingleOrDefault(tit => tit.ID == Comp9.GetMatiere.Titrage.ID);
                                    SelectedColor9 =
                                        ListCouleur9.SingleOrDefault(co => co.ID == Comp9.GetMatiere.GetCouleur.ID);
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
                                if (NewTemp[i].NumComposant == 1)
                                {

                                    Comp1Vis = true;
                                    ChangeImageBtn1 = "../Asset/remove64.png";


                                    Comp1 = (Composition)NewTemp[i].Clone();
                                    var eqid = new EquivalentID();
                                    eqid.Id = Comp1.ID;
                                    Comp1.ProdID = (Produit)NewProd.Clone();
                                    Comp1.ID = 0;
                                    Comp1.ID = _DB2.AddProdCompo((Composition)Comp1.Clone());
                                    eqid.EquivId = Comp1.ID;
                                    EquivList.Add(eqid);
                                    if (NewTemp[i].GetMatiere != null)
                                    {
                                        SelectedTypeMatiere1 = ListTypeMatiere.SingleOrDefault(ma =>
                                            ma.ID == Comp1.GetMatiere.Titrage.TypeMatiere.ID);
                                        SelectedTitrage1 =
                                            ListTitrage1.SingleOrDefault(tit => tit.ID == Comp1.GetMatiere.Titrage.ID);
                                        SelectedColor1 =
                                            ListCouleur1.SingleOrDefault(co => co.ID == Comp1.GetMatiere.GetCouleur.ID);
                                        _DB2.UpdateCompoMatiere(Comp1);
                                    }

                                    if (NewTemp[i].GetComposant != null)
                                    {
                                        _DB2.UpdateCompoComposant(Comp1);
                                        Comp1.GetComposant =
                                            ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                    }

                                    if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                                        SchCompList.Add(Comp1);


                                    Comp1.PropertyChanged += Comp1_PropertyChanged;
                                }

                                if (NewTemp[i].NumComposant == 2)
                                {
                                    Comp2Vis = true;
                                    ChangeImageBtn2 = "../Asset/remove64.png";


                                    Comp2 = (Composition)NewTemp[i].Clone();
                                    var eqid = new EquivalentID();
                                    eqid.Id = Comp2.ID;
                                    Comp2.ProdID = (Produit)NewProd.Clone();
                                    Comp2.ID = 0;
                                    Comp2.ID = _DB2.AddProdCompo((Composition)Comp2.Clone());
                                    eqid.EquivId = Comp2.ID;
                                    EquivList.Add(eqid);
                                    if (NewTemp[i].GetMatiere != null)
                                    {
                                        SelectedTypeMatiere2 = ListTypeMatiere.SingleOrDefault(ma =>
                                            ma.ID == Comp2.GetMatiere.Titrage.TypeMatiere.ID);
                                        SelectedTitrage2 =
                                            ListTitrage2.SingleOrDefault(tit => tit.ID == Comp2.GetMatiere.Titrage.ID);
                                        SelectedColor2 =
                                            ListCouleur2.SingleOrDefault(co => co.ID == Comp2.GetMatiere.GetCouleur.ID);
                                        _DB2.UpdateCompoMatiere(Comp2);
                                    }

                                    if (NewTemp[i].GetComposant != null)
                                    {
                                        _DB2.UpdateCompoComposant(Comp2);
                                        Comp2.GetComposant =
                                            ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                    }

                                    if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                                        SchCompList.Add(Comp2);
                                    Comp2.PropertyChanged += Comp2_PropertyChanged;
                                }

                                if (NewTemp[i].NumComposant == 3)
                                {
                                    Comp3Vis = true;
                                    ChangeImageBtn3 = "../Asset/remove64.png";


                                    Comp3 = (Composition)NewTemp[i].Clone();
                                    var eqid = new EquivalentID();
                                    eqid.Id = Comp3.ID;
                                    Comp3.ProdID = (Produit)NewProd.Clone();
                                    Comp3.ID = 0;
                                    Comp3.ID = _DB2.AddProdCompo((Composition)Comp3.Clone());
                                    eqid.Id = Comp3.ID;
                                    EquivList.Add(eqid);
                                    if (NewTemp[i].GetMatiere != null)
                                    {
                                        SelectedTypeMatiere3 = ListTypeMatiere.SingleOrDefault(ma =>
                                            ma.ID == Comp3.GetMatiere.Titrage.TypeMatiere.ID);
                                        SelectedTitrage3 =
                                            ListTitrage3.SingleOrDefault(tit => tit.ID == Comp3.GetMatiere.Titrage.ID);
                                        SelectedColor3 =
                                            ListCouleur3.SingleOrDefault(co => co.ID == Comp3.GetMatiere.GetCouleur.ID);
                                        _DB2.UpdateCompoMatiere(Comp3);
                                    }

                                    if (NewTemp[i].GetComposant != null)
                                    {
                                        _DB2.UpdateCompoComposant(Comp3);
                                        Comp3.GetComposant =
                                            ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                    }

                                    if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                                        SchCompList.Add(Comp3);
                                    Comp3.PropertyChanged += Comp3_PropertyChanged;
                                }

                                if (NewTemp[i].NumComposant == 4)
                                {
                                    Comp4Vis = true;
                                    ChangeImageBtn4 = "../Asset/remove64.png";


                                    Comp4 = (Composition)NewTemp[i].Clone();
                                    var eqid = new EquivalentID();
                                    eqid.Id = Comp4.ID;
                                    Comp4.ProdID = (Produit)NewProd.Clone();
                                    Comp4.ID = 0;
                                    Comp4.ID = _DB2.AddProdCompo((Composition)Comp4.Clone());
                                    eqid.EquivId = Comp4.ID;
                                    EquivList.Add(eqid);
                                    if (NewTemp[i].GetMatiere != null)
                                    {
                                        SelectedTypeMatiere4 = ListTypeMatiere.SingleOrDefault(ma =>
                                            ma.ID == Comp4.GetMatiere.Titrage.TypeMatiere.ID);
                                        SelectedTitrage4 =
                                            ListTitrage4.SingleOrDefault(tit => tit.ID == Comp4.GetMatiere.Titrage.ID);
                                        SelectedColor4 =
                                            ListCouleur4.SingleOrDefault(co => co.ID == Comp4.GetMatiere.GetCouleur.ID);
                                        _DB2.UpdateCompoMatiere(Comp4);
                                    }

                                    if (NewTemp[i].GetComposant != null)
                                    {
                                        _DB2.UpdateCompoComposant(Comp4);
                                        Comp4.GetComposant =
                                            ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                    }

                                    if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                                        SchCompList.Add(Comp4);
                                    Comp4.PropertyChanged += Comp4_PropertyChanged;
                                }

                                if (NewTemp[i].NumComposant == 5)
                                {
                                    Comp5Vis = true;
                                    ChangeImageBtn5 = "../Asset/remove64.png";


                                    Comp5 = (Composition)NewTemp[i].Clone();
                                    var eqid = new EquivalentID();
                                    eqid.Id = Comp5.ID;
                                    Comp5.ProdID = (Produit)NewProd.Clone();
                                    Comp5.ID = 0;
                                    Comp5.ID = _DB2.AddProdCompo((Composition)Comp5.Clone());
                                    eqid.EquivId = Comp5.ID;
                                    EquivList.Add(eqid);
                                    if (NewTemp[i].GetMatiere != null)
                                    {
                                        SelectedTypeMatiere5 = ListTypeMatiere.SingleOrDefault(ma =>
                                            ma.ID == Comp5.GetMatiere.Titrage.TypeMatiere.ID);
                                        SelectedTitrage5 =
                                            ListTitrage5.SingleOrDefault(tit => tit.ID == Comp5.GetMatiere.Titrage.ID);
                                        SelectedColor5 =
                                            ListCouleur5.SingleOrDefault(co => co.ID == Comp5.GetMatiere.GetCouleur.ID);
                                        _DB2.UpdateCompoMatiere(Comp5);
                                    }

                                    if (NewTemp[i].GetComposant != null)
                                    {
                                        _DB2.UpdateCompoComposant(Comp5);
                                        Comp5.GetComposant =
                                            ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                    }

                                    if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                                        SchCompList.Add(Comp5);
                                    Comp5.PropertyChanged += Comp5_PropertyChanged;
                                }

                                if (NewTemp[i].NumComposant == 6)
                                {
                                    Comp6Vis = true;
                                    ChangeImageBtn6 = "../Asset/remove64.png";


                                    Comp6 = (Composition)NewTemp[i].Clone();
                                    var eqid = new EquivalentID();
                                    eqid.Id = Comp6.ID;
                                    Comp6.ProdID = (Produit)NewProd.Clone();
                                    Comp6.ID = 0;
                                    Comp6.ID = _DB2.AddProdCompo((Composition)Comp6.Clone());
                                    eqid.EquivId = Comp6.ID;
                                    EquivList.Add(eqid);
                                    if (NewTemp[i].GetMatiere != null)
                                    {
                                        SelectedTypeMatiere6 = ListTypeMatiere.SingleOrDefault(ma =>
                                            ma.ID == Comp6.GetMatiere.Titrage.TypeMatiere.ID);
                                        SelectedTitrage6 =
                                            ListTitrage6.SingleOrDefault(tit => tit.ID == Comp6.GetMatiere.Titrage.ID);
                                        SelectedColor6 =
                                            ListCouleur6.SingleOrDefault(co => co.ID == Comp6.GetMatiere.GetCouleur.ID);
                                        _DB2.UpdateCompoMatiere(Comp6);
                                    }

                                    if (NewTemp[i].GetComposant != null)
                                    {
                                        _DB2.UpdateCompoComposant(Comp6);
                                        Comp6.GetComposant =
                                            ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                    }

                                    if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                                        SchCompList.Add(Comp6);
                                    Comp6.PropertyChanged += Comp6_PropertyChanged;
                                }

                                if (NewTemp[i].NumComposant == 7)
                                {
                                    Comp7Vis = true;
                                    ChangeImageBtn7 = "../Asset/remove64.png";


                                    Comp7 = (Composition)NewTemp[i].Clone();
                                    var eqid = new EquivalentID();
                                    eqid.Id = Comp7.ID;
                                    Comp7.ProdID = (Produit)NewProd.Clone();
                                    Comp7.ID = 0;
                                    Comp7.ID = _DB2.AddProdCompo((Composition)Comp7.Clone());
                                    eqid.EquivId = Comp7.ID;
                                    EquivList.Add(eqid);
                                    if (NewTemp[i].GetMatiere != null)
                                    {
                                        SelectedTypeMatiere7 = ListTypeMatiere.SingleOrDefault(ma =>
                                            ma.ID == Comp7.GetMatiere.Titrage.TypeMatiere.ID);
                                        SelectedTitrage7 =
                                            ListTitrage7.SingleOrDefault(tit => tit.ID == Comp7.GetMatiere.Titrage.ID);
                                        SelectedColor7 =
                                            ListCouleur7.SingleOrDefault(co => co.ID == Comp7.GetMatiere.GetCouleur.ID);
                                        _DB2.UpdateCompoMatiere(Comp7);
                                    }

                                    if (NewTemp[i].GetComposant != null)
                                    {
                                        _DB2.UpdateCompoComposant(Comp7);
                                        Comp7.GetComposant =
                                            ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                    }

                                    if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                                        SchCompList.Add(Comp7);
                                    Comp7.PropertyChanged += Comp7_PropertyChanged;
                                }
                            }
                        }


                    if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                    {
                        if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine != null)
                            SelectedChaine = ChaineNameList.SingleOrDefault(ch =>
                                ch.ID == SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine.ID);
                        if (IsEnfilage)
                        {
                            NewProd.IsEnfilage = 1;
                            NewProd.EnfilageID = new Enfilage();
                            NewProd.EnfilageID.Column = EnfilageColumns;
                            NewProd.EnfilageID.Row = EnfilageRow;
                            NewProd.EnfilageID.TrXposition =
                                SelectedFicheTechnique.Produits[LastVersion].EnfilageID.TrXposition;
                            NewProd.EnfilageID.TrYposition =
                                SelectedFicheTechnique.Produits[LastVersion].EnfilageID.TrYposition;
                            if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine != null)
                            {
                                NewProd.EnfilageID.GetChaine = SelectedChaine;
                                NewProd.EnfilageID.ID = _DB2.AddNewEnfilageWithChaine(NewProd.EnfilageID);
                            }
                            else
                            {
                                NewProd.EnfilageID.ID = _DB2.AddNewEnfilage(NewProd.EnfilageID);
                            }
                               

                            
                            _DB2.UpdateProdEnfilage(NewProd);
                        }
                        else
                        {
                            NewProd.IsEnfilage = 0;
                        }


                        foreach (var EleMatx in SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetMatrix)
                        {
                            var LastMatrixID = _DB2.GetLatestEnfilageMatrixID();
                            var EnfMatrix = new EnfilageMatrix();
                            EnfMatrix.ID = LastMatrixID;
                            EnfMatrix.x = EleMatx.x;
                            EnfMatrix.y = EleMatx.y;
                            EnfMatrix.DentFil = EleMatx.DentFil;
                            var eqObj = EquivList.SingleOrDefault(eq => eq.Id == EleMatx.value.ID);

                            EnfMatrix.value = EleMatx.value;
                            EnfMatrix.value.ID = eqObj.EquivId;
                            EnfMatrix.Enf = NewProd.EnfilageID;
                            _DB2.AddNewEnfilageMatrix(EnfMatrix);
                        }
                    }
                    NewProd.PropertyChanged += Prod_PropertyChanged;
                }
                else
                {
                    SendNotification.Raise("Aucune Fiche Technique Séléctionnée");
                }
            }
            catch(Exception ex)
            {
                SendNotification.Raise(ex.ToString());
            }
        }

        public void ConfirmFT()
        {
            UIServices.SetBusyState();

            if (SelectedFicheTechnique != null)
            {
                var SaveFTID = SelectedFicheTechnique.ID;
                IsProduction = true;
                IsDateCr = false;
                IsUpdate = true;
                MinVers = 1;
                var LastVersion = SelectedFicheTechnique.Produits.Count - 1;


                NewProd.Id = 0;
                NewProd.Version = NewProd.Version + 1;
                var prod = (Produit)NewProd.Clone();

                OldVersion = NewProd.Version;
                NewProd.Id = _DB2.AddNewProductVersion(prod);
                if (prod.Client != null) _DB2.UpdateProdClient(prod);
                if (prod.Concepteur != null) _DB2.UpdateProdConcepteur(prod);
                if (prod.Verificateur != null) _DB2.UpdateProdVerificateur(prod);
                if (prod.RedacteurObj != null) _DB2.UpdateProdRedacteur(prod);
                if (prod.PeigneObj != null) _DB2.UpdateProdPeigne(prod);
                if (prod.DuitageID != null)
                    if (SelectedFicheTechnique.Produits[LastVersion].DuitageID.Machine != null)
                        _DB2.UpdateProdDuitage(prod);

                if (prod.EnfilageID != null) _DB2.UpdateProdEnfilage(prod);

                if (prod.Epaisseur > 0) _DB2.UpdateProdEpaiseur(prod);


                var EquivList = new List<EquivalentID>();

                var NewTemp = new List<Composition>(NewProd.GetComposition.Count);
                NewProd.GetComposition.ForEach(item => { NewTemp.Add((Composition)item.Clone()); });
                for (var i = 0; i < NewTemp.Count; i++)
                    if (NewTemp[i].GetComposant != null)
                    {
                        if (NewTemp[i].NumComposant == 8)
                        {
                            Comp8 = (Composition)NewTemp[i].Clone();
                            var eqid = new EquivalentID();
                            eqid.Id = Comp8.ID;

                            Comp8.ProdID = (Produit)NewProd.Clone();
                            Comp8.ID = 0;
                            Comp8.ID = _DB2.AddProdCompo((Composition)Comp8.Clone());
                            eqid.EquivId = Comp8.ID;
                            EquivList.Add(eqid);
                            if (NewTemp[i].GetMatiere != null) _DB2.UpdateCompoMatiere(Comp8);
                            if (NewTemp[i].GetComposant != null)
                            {
                                _DB2.UpdateCompoComposant(Comp8);

                                Comp8.GetComposant =
                                    ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                            }
                        }
                        else if (NewTemp[i].NumComposant == 9)
                        {
                            Comp9 = (Composition)NewTemp[i].Clone();
                            var eqid = new EquivalentID();
                            eqid.Id = Comp9.ID;
                            Comp9.ProdID = (Produit)NewProd.Clone();
                            Comp9.ID = 0;
                            Comp9.ID = _DB2.AddProdCompo((Composition)Comp9.Clone());
                            eqid.EquivId = Comp9.ID;
                            EquivList.Add(eqid);
                            if (NewTemp[i].GetMatiere != null) _DB2.UpdateCompoMatiere(Comp9);
                            if (NewTemp[i].GetComposant != null)
                            {
                                _DB2.UpdateCompoComposant(Comp9);
                                Comp9.GetComposant =
                                    ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                            }
                        }
                        else
                        {
                            if (NewTemp[i].NumComposant == 1)
                            {
                                Comp1 = (Composition)NewTemp[i].Clone();
                                var eqid = new EquivalentID();
                                eqid.Id = Comp1.ID;
                                Comp1.ProdID = (Produit)NewProd.Clone();
                                Comp1.ID = 0;
                                Comp1.ID = _DB2.AddProdCompo((Composition)Comp1.Clone());
                                eqid.EquivId = Comp1.ID;
                                EquivList.Add(eqid);
                                if (NewTemp[i].GetMatiere != null) _DB2.UpdateCompoMatiere(Comp1);
                                if (NewTemp[i].GetComposant != null)
                                {
                                    _DB2.UpdateCompoComposant(Comp1);
                                    Comp1.GetComposant =
                                        ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                }
                            }

                            if (NewTemp[i].NumComposant == 2)
                            {
                                Comp2 = (Composition)NewTemp[i].Clone();
                                var eqid = new EquivalentID();
                                eqid.Id = Comp2.ID;
                                Comp2.ProdID = (Produit)NewProd.Clone();
                                Comp2.ID = 0;
                                Comp2.ID = _DB2.AddProdCompo((Composition)Comp2.Clone());
                                eqid.EquivId = Comp2.ID;
                                EquivList.Add(eqid);
                                if (NewTemp[i].GetMatiere != null) _DB2.UpdateCompoMatiere(Comp2);
                                if (NewTemp[i].GetComposant != null)
                                {
                                    _DB2.UpdateCompoComposant(Comp2);
                                    Comp2.GetComposant =
                                        ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                }
                            }

                            if (NewTemp[i].NumComposant == 3)
                            {
                                Comp3 = (Composition)NewTemp[i].Clone();
                                var eqid = new EquivalentID();
                                eqid.Id = Comp3.ID;
                                Comp3.ProdID = (Produit)NewProd.Clone();
                                Comp3.ID = 0;
                                Comp3.ID = _DB2.AddProdCompo((Composition)Comp3.Clone());
                                eqid.Id = Comp3.ID;
                                EquivList.Add(eqid);
                                if (NewTemp[i].GetMatiere != null)
                                {
                                    SelectedTypeMatiere3 = ListTypeMatiere.SingleOrDefault(ma =>
                                        ma.ID == Comp3.GetMatiere.Titrage.TypeMatiere.ID);
                                    SelectedTitrage3 =
                                        ListTitrage3.SingleOrDefault(tit => tit.ID == Comp3.GetMatiere.Titrage.ID);
                                    SelectedColor3 =
                                        ListCouleur3.SingleOrDefault(co => co.ID == Comp3.GetMatiere.GetCouleur.ID);
                                    _DB2.UpdateCompoMatiere(Comp3);
                                }

                                if (NewTemp[i].GetComposant != null)
                                {
                                    _DB2.UpdateCompoComposant(Comp3);
                                    Comp3.GetComposant =
                                        ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                }
                            }

                            if (NewTemp[i].NumComposant == 4)
                            {
                                Comp4 = (Composition)NewTemp[i].Clone();
                                var eqid = new EquivalentID();
                                eqid.Id = Comp4.ID;
                                Comp4.ProdID = (Produit)NewProd.Clone();
                                Comp4.ID = 0;
                                Comp4.ID = _DB2.AddProdCompo((Composition)Comp4.Clone());
                                eqid.EquivId = Comp4.ID;
                                EquivList.Add(eqid);
                                if (NewTemp[i].GetMatiere != null) _DB2.UpdateCompoMatiere(Comp4);
                                if (NewTemp[i].GetComposant != null)
                                {
                                    _DB2.UpdateCompoComposant(Comp4);
                                    Comp4.GetComposant =
                                        ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                }
                            }

                            if (NewTemp[i].NumComposant == 5)
                            {
                                Comp5 = (Composition)NewTemp[i].Clone();
                                var eqid = new EquivalentID();
                                eqid.Id = Comp5.ID;
                                Comp5.ProdID = (Produit)NewProd.Clone();
                                Comp5.ID = 0;
                                Comp5.ID = _DB2.AddProdCompo((Composition)Comp5.Clone());
                                eqid.EquivId = Comp5.ID;
                                EquivList.Add(eqid);
                                if (NewTemp[i].GetMatiere != null) _DB2.UpdateCompoMatiere(Comp5);
                                if (NewTemp[i].GetComposant != null)
                                {
                                    _DB2.UpdateCompoComposant(Comp5);
                                    Comp5.GetComposant =
                                        ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                }
                            }

                            if (NewTemp[i].NumComposant == 6)
                            {
                                Comp6 = (Composition)NewTemp[i].Clone();
                                var eqid = new EquivalentID();
                                eqid.Id = Comp6.ID;
                                Comp6.ProdID = (Produit)NewProd.Clone();
                                Comp6.ID = 0;
                                Comp6.ID = _DB2.AddProdCompo((Composition)Comp6.Clone());
                                eqid.EquivId = Comp6.ID;
                                EquivList.Add(eqid);
                                if (NewTemp[i].GetMatiere != null) _DB2.UpdateCompoMatiere(Comp6);
                                if (NewTemp[i].GetComposant != null)
                                {
                                    _DB2.UpdateCompoComposant(Comp6);
                                    Comp6.GetComposant =
                                        ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                }
                            }

                            if (NewTemp[i].NumComposant == 7)
                            {
                                Comp7 = (Composition)NewTemp[i].Clone();
                                var eqid = new EquivalentID();
                                eqid.Id = Comp7.ID;
                                Comp7.ProdID = (Produit)NewProd.Clone();
                                Comp7.ID = 0;
                                Comp7.ID = _DB2.AddProdCompo((Composition)Comp7.Clone());
                                eqid.EquivId = Comp7.ID;
                                EquivList.Add(eqid);
                                if (NewTemp[i].GetMatiere != null) _DB2.UpdateCompoMatiere(Comp7);
                                if (NewTemp[i].GetComposant != null)
                                {
                                    _DB2.UpdateCompoComposant(Comp7);
                                    Comp7.GetComposant =
                                        ListComposant.SingleOrDefault(comp => comp.ID == NewTemp[i].GetComposant.ID);
                                }
                            }
                        }
                    }


                if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID != null)
                {
                    if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine != null)
                        SelectedChaine = ChaineNameList.SingleOrDefault(ch =>
                            ch.ID == SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine.ID);
                    if (IsEnfilage)
                    {
                        NewProd.IsEnfilage = 1;
                        NewProd.EnfilageID = new Enfilage();
                        NewProd.EnfilageID.Column = EnfilageColumns;
                        NewProd.EnfilageID.Row = EnfilageRow;
                        NewProd.EnfilageID.TrXposition =
                            SelectedFicheTechnique.Produits[LastVersion].EnfilageID.TrXposition;
                        NewProd.EnfilageID.TrYposition =
                            SelectedFicheTechnique.Produits[LastVersion].EnfilageID.TrYposition;
                        if (SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetChaine != null)
                            NewProd.EnfilageID.GetChaine = SelectedChaine;
                        if(NewProd.EnfilageID.GetChaine!=null)
                        {
                            NewProd.EnfilageID.ID = _DB2.AddNewEnfilageWithChaine(NewProd.EnfilageID);
                        }
                        else
                        {
                            NewProd.EnfilageID.ID = _DB2.AddNewEnfilage(NewProd.EnfilageID);
                        }
                        
                    }
                    else
                    {
                        NewProd.IsEnfilage = 0;
                    }


                    foreach (var EleMatx in SelectedFicheTechnique.Produits[LastVersion].EnfilageID.GetMatrix)
                    {
                        var LastMatrixID = _DB2.GetLatestEnfilageMatrixID();
                        var EnfMatrix = new EnfilageMatrix();
                        EnfMatrix.ID = LastMatrixID;
                        EnfMatrix.x = EleMatx.x;
                        EnfMatrix.y = EleMatx.y;
                        EnfMatrix.DentFil = EleMatx.DentFil;
                        var eqObj = EquivList.SingleOrDefault(eq => eq.Id == EleMatx.value.ID);

                        EnfMatrix.value = EleMatx.value;
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
                    FullFTByCatList = FullFTList;
                    FicheTechniqueList = FullFTList;
                }
                else if (IsProdFT)
                {
                    FullFTList = new MvxObservableCollection<FicheTechnique>(_DB2.GetFicheTechniquesProd());
                    FullFTByCatList = FullFTList;
                    FicheTechniqueList = FullFTList;
                }
                else
                {
                    FullFTList = new MvxObservableCollection<FicheTechnique>(_DB2.GetFicheTechniquesEch());
                    FullFTByCatList = FullFTList;
                    FicheTechniqueList = FullFTList;
                }

                SelectedFicheTechnique = FicheTechniqueList.SingleOrDefault(ft => ft.ID == SaveFTID);
            }
            else
            {
                SendNotification.Raise("Aucune Fiche Technique Séléctionnée");
            }
        }

        public void ValiderFicheTechnique()
        {
            if (SelectedFicheTechnique != null)
            {
                if (SelectedFicheTechnique.Produits.Count > 0)
                {
                    var lastIn = SelectedFicheTechnique.Produits.Count - 1;
                    _DB2.ValidateFicheTechnique(SelectedFicheTechnique.Produits[lastIn]);
                    UpdateFicheTek();
                }
            }
            else
            {
                SendNotification.Raise("Aucune Fiche Technique Séléctionnée");
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
            if(IsSecondPageDisplayed==false)
            {
                IsSecondPageDisplayed = true;
            }
            IsNextPage = false;
            IsPrevPage = true;
            Part1Visible = false;
            Part2Visible = true;
            PageNumber = "2/2";
        }

        public async Task DisplayFicheTekModel()
        {
            mf = await _NavigationService.Navigate<ModelFicheTekViewModel, ModelFiche, ModelFiche>(new ModelFiche());
            if (mf != null)
            {
                IsEnfilage = true;
                if (mf.model == ModelFiche.ModelFicheTek.FicheTekCrochetage) IsEnfilage = false;

                if (mf != null && mf.IsEchantillon)
                {
                    IsProduction = false;
                    MinVers = 0;
                }
                else
                {
                    IsProduction = true;
                    MinVers = 1;
                }

                DateEndLimit = DateTime.Now;

                AnnulerFicheTechnique();
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

        public void AjouterFicheTechnique(ModelFiche mf)
        {
            try
            {
                IsNewDatasheet = true;
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
                }
                else if (mf.model == ModelFiche.ModelFicheTek.FicheTekEHC)
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
                    EnfilageList.First(en => en.X == CellContent.X && en.Y == CellContent.Y).Content = null;

                ContentEnfilageList = new MvxObservableCollection<MatrixElement>();

                SchCompList = new MvxObservableCollection<Composition>();
                ChaineNameList = new MvxObservableCollection<chaine>(_DB2.GetChaines());
                var NewFicheTek = new FicheTechnique();
                if (IsFicheCrochetage)
                    NewFicheTek.ModelFiche = (int)ModelFiche.ModelFicheTek.FicheTekCrochetage;
                else if (IsFicheNormal)
                    NewFicheTek.ModelFiche = (int)ModelFiche.ModelFicheTek.FicheTekNormal;
                else
                    NewFicheTek.ModelFiche = (int)ModelFiche.ModelFicheTek.FicheTekEHC;

                var GivenFiche = _DB2.AddNewFicheTechnique(NewFicheTek);
                if (IsProduction)
                    NewProd.Version = 1;
                else
                    NewProd.Version = 0;

                NewProd.FicheTekID = GivenFiche;
                NewProd.FicheId = GivenFiche.ID;
                NewProd.Id = _DB2.GetLastProductID() + 1;
                NewProd.Definite = 0;
                //NewProd.DateCreation=DateTime.Now;
                NewProd.Ref = "FT" + (FicheTechniqueList.Count() + 1);
                OldRef = NewProd.Ref;
                OldVersion = NewProd.Version;
                OldNumArticle = NewProd.NumArticle;
                NewProd.Name = "Fiche Technique " + (FicheTechniqueList.Count() + 1);
                if (IsEnfilage)
                {
                    NewProd.IsEnfilage = 1;
                    NewProd.EnfilageID = new Enfilage();
                    NewProd.EnfilageID.Column = EnfilageColumns;
                    NewProd.EnfilageID.Row = EnfilageRow;
                    NewProd.EnfilageID.TrXposition = "0";
                    NewProd.EnfilageID.TrYposition = "0";
                    NewProd.EnfilageID.ID = _DB2.AddNewEnfilage(NewProd.EnfilageID);
                    NewProd.PropertyChanged += Prod_PropertyChanged;
                    NewProd.Id = _DB2.AddNewProductWithEnfilage((Produit)NewProd.Clone());
                }
                else
                {
                    NewProd.IsEnfilage = 0;
                    NewProd.PropertyChanged += Prod_PropertyChanged;
                    NewProd.Id = _DB2.AddNewProduct((Produit)NewProd.Clone());
                }

                FicheTechniqueList = new MvxObservableCollection<FicheTechnique>(_DB2.GetFicheTechniques());
            }
            catch(Exception ex)
            {
                SendNotification.Raise(ex.ToString());
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
            if (IsFicheCrochetage) MachineList = new MvxObservableCollection<Machine>(_DB2.GetMachines());
            IsDentFilVis = false;

            rep1 = new Repitition();
            rep2 = new Repitition();
            rep3 = new Repitition();
            rep4 = new Repitition();
            rep5 = new Repitition();
            ImageProd = null;
            IsCreateChaine = false;
            WorkRectan = null;
            ProhibitArea = null;
            SelectedVersion = null;
            TrameXposition = "0";
            TrameYposition = "0";

            SecondRect = null;
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
            NbrDent = 0;
            Comp1 = null;
            Comp2 = null;
            Comp3 = null;
            Comp4 = null;
            Comp5 = null;
            Comp6 = null;
            Comp7 = null;
            Comp8 = null;
            Comp9 = null;
            SelectedColor1 = null;
            SelectedColor2 = null;
            SelectedColor3 = null;
            SelectedColor4 = null;
            SelectedColor5 = null;
            SelectedColor6 = null;
            SelectedColor7 = null;
            SelectedColor8 = null;
            SelectedColor9 = null;

            SelectedTitrage1 = null;
            SelectedTitrage2 = null;
            SelectedTitrage3 = null;
            SelectedTitrage4 = null;
            SelectedTitrage5 = null;
            SelectedTitrage6 = null;
            SelectedTitrage7 = null;
            SelectedTitrage8 = null;
            SelectedTitrage9 = null;
           
            SelectedTypeMatiere1 = null;
            SelectedTypeMatiere2 = null;
            SelectedTypeMatiere3 = null;
            SelectedTypeMatiere4 = null;
            SelectedTypeMatiere5 = null;
            SelectedTypeMatiere6 = null;
            SelectedTypeMatiere7 = null;
            SelectedTypeMatiere8 = null;
            SelectedTypeMatiere9 = null;
         
            InitBtnImage();
            SelectedCategorie = null;
            CategorieName = "";
            SchCompList = new MvxObservableCollection<Composition>();
            ChaineList = new ObservableCollection<ChaineMatrixElement>();
            var lvide = new List<MatrixElement>();
            ContentEnfilageList = new MvxObservableCollection<MatrixElement>(lvide);
        }

        public void SetupDuitageList()
        {
            if (SelectedMachine != null)
            {
                DuitageList = new MvxObservableCollection<Duitages>(_DB2.GetDuitageMachine(SelectedMachine));
                if (IsFicheCrochetage)
                    DuitageGoList =
                        new MvxObservableCollection<DuitageGomme>(_DB2.GetDuitageMachineGo(SelectedMachine));
            }
        }

        public void SetupVitesse()
        {
            if (NewProd.DuitageID != null)
                Vitesse = _DB2.GetDuitage(SelectedMachine.ID, NewProd.DuitageID.Duitage).Vitesse.ToString();
        }

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
            else if (UserSession.type == user.UserType.verificateur)
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
                    _DB2.GetTitrageByTypMat(SelectedTypeMatiere1));
        }

        public void UpdateListTitrage2()
        {
            ListTitrage2 =
                new MvxObservableCollection<Titrage>(
                    _DB2.GetTitrageByTypMat(SelectedTypeMatiere2));
        }

        public void UpdateListTitrage3()
        {
            ListTitrage3 =
                new MvxObservableCollection<Titrage>(
                    _DB2.GetTitrageByTypMat(SelectedTypeMatiere3));
        }

        public void UpdateListTitrage4()
        {
            ListTitrage4 =
                new MvxObservableCollection<Titrage>(
                    _DB2.GetTitrageByTypMat(SelectedTypeMatiere4));
        }

        public void UpdateListTitrage5()
        {
            ListTitrage5 =
                new MvxObservableCollection<Titrage>(
                    _DB2.GetTitrageByTypMat(SelectedTypeMatiere5));
        }

        public void UpdateListTitrage6()
        {
            ListTitrage6 =
                new MvxObservableCollection<Titrage>(
                    _DB2.GetTitrageByTypMat(SelectedTypeMatiere6));
        }

        public void UpdateListTitrage7()
        {
            ListTitrage7 =
                new MvxObservableCollection<Titrage>(
                    _DB2.GetTitrageByTypMat(SelectedTypeMatiere7));
        }

        public void UpdateListTitrage8()
        {
            ListTitrage8 =
                new MvxObservableCollection<Titrage>(
                    _DB2.GetTitrageByTypMat(SelectedTypeMatiere8));
        }

        public void UpdateListTitrage9()
        {
            ListTitrage9 =
                new MvxObservableCollection<Titrage>(
                    _DB2.GetTitrageByTypMat(SelectedTypeMatiere9));
        }

        private TypeMatiere _SelectedTypeMatiere1;

        private void Comp1_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _DB2.UpdateCompoPoids(Comp1);
            }
            else if (e.PropertyName.Equals("EnfNbrFil"))
            {
                _DB2.UpdateCompoEnfNbr(Comp1);
            }
            else if (e.PropertyName.Equals("NbrFil"))
            {
                _DB2.UpdateCompoNbrFil(Comp1);
                CheckWorkspaceReadiness();
            }
            else if (e.PropertyName.Equals("GetComposant"))
            {
                _DB2.UpdateCompoComposant(Comp1);
            }
            else if (e.PropertyName.Equals("Num"))
            {
                _DB2.UpdateCompoNum(Comp1);
            }
            else if (e.PropertyName.Equals("NumComposant"))
            {
                _DB2.UpdateCompoNumComposant(Comp1);
            }
            else if (e.PropertyName.Equals("GetMatiere"))
            {
                _DB2.UpdateCompoMatiere(Comp1);
            }
            else if (e.PropertyName.Equals("Torsion"))
            {
                _DB2.UpdateCompoTorsion(Comp1);
            }
            else if (e.PropertyName.Equals("Enfilage"))
            {
                _DB2.UpdateCompoEnfilage(Comp1);
            }
            else if (e.PropertyName.Equals("Emb"))
            {
                _DB2.UpdateCompoEmb(Comp1);
            }
            else if (e.PropertyName.Equals("Observation"))
            {
                _DB2.UpdateCompoObservation(Comp1);
            }
        }

        public void CheckWorkspaceReadiness()
        {
           

            if (SelectedChaine != null)
                if (SchCompList.Count > 0)
                {
                    var b = true;
                    var sumNbrFil = 0;
                    foreach (var sch in SchCompList)
                        if (sch.NbrFil == 0)
                            b = false;
                        else
                            sumNbrFil = sumNbrFil + sch.NbrFil;

                    if(WorkRectan != null)
                    {
                        var pn = decimal.Divide(sumNbrFil, EnfilageColumns - 2);
                        var NbrPart = Convert.ToInt32(Math.Ceiling(pn));

                        if (SelectedChaine.Colonne == WorkRectan.ChaineColumn
                            && WorkRectan.NbrPart== NbrPart

                       )
                            b = false;
                    }
                       
                     if (b)
                        SetupEnfilageWorkspace(sumNbrFil);
                }
        }

        private void Comp2_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _DB2.UpdateCompoPoids(Comp2);
            }
            else if (e.PropertyName.Equals("EnfNbrFil"))
            {
                _DB2.UpdateCompoEnfNbr(Comp2);
            }
            else if (e.PropertyName.Equals("NbrFil"))
            {
                _DB2.UpdateCompoNbrFil(Comp2);
                CheckWorkspaceReadiness();
            }
            else if (e.PropertyName.Equals("GetComposant"))
            {
                _DB2.UpdateCompoComposant(Comp2);
            }
            else if (e.PropertyName.Equals("NumComposant"))
            {
                _DB2.UpdateCompoNumComposant(Comp2);
            }
            else if (e.PropertyName.Equals("GetMatiere"))
            {
                _DB2.UpdateCompoMatiere(Comp2);
            }
            else if (e.PropertyName.Equals("Torsion"))
            {
                _DB2.UpdateCompoTorsion(Comp2);
            }
            else if (e.PropertyName.Equals("Enfilage"))
            {
                _DB2.UpdateCompoEnfilage(Comp2);
            }
            else if (e.PropertyName.Equals("Emb"))
            {
                _DB2.UpdateCompoEmb(Comp2);
            }
            else if (e.PropertyName.Equals("Num"))
            {
                _DB2.UpdateCompoNum(Comp2);
            }
            else if (e.PropertyName.Equals("Observation"))
            {
                _DB2.UpdateCompoObservation(Comp2);
            }
        }

        private void Comp3_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _DB2.UpdateCompoPoids(Comp3);
            }
            else if (e.PropertyName.Equals("EnfNbrFil"))
            {
                _DB2.UpdateCompoEnfNbr(Comp3);
            }
            else if (e.PropertyName.Equals("NbrFil"))
            {
                _DB2.UpdateCompoNbrFil(Comp3);
                CheckWorkspaceReadiness();
            }
            else if (e.PropertyName.Equals("GetComposant"))
            {
                _DB2.UpdateCompoComposant(Comp3);
            }
            else if (e.PropertyName.Equals("NumComposant"))
            {
                _DB2.UpdateCompoNumComposant(Comp3);
            }
            else if (e.PropertyName.Equals("GetMatiere"))
            {
                _DB2.UpdateCompoMatiere(Comp3);
            }
            else if (e.PropertyName.Equals("Torsion"))
            {
                _DB2.UpdateCompoTorsion(Comp3);
            }
            else if (e.PropertyName.Equals("Enfilage"))
            {
                _DB2.UpdateCompoEnfilage(Comp3);
            }
            else if (e.PropertyName.Equals("Emb"))
            {
                _DB2.UpdateCompoEmb(Comp3);
            }
            else if (e.PropertyName.Equals("Num"))
            {
                _DB2.UpdateCompoNum(Comp3);
            }
            else if (e.PropertyName.Equals("Observation"))
            {
                _DB2.UpdateCompoObservation(Comp3);
            }
        }

        private void Comp4_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _DB2.UpdateCompoPoids(Comp4);
            }
            else if (e.PropertyName.Equals("EnfNbrFil"))
            {
                _DB2.UpdateCompoEnfNbr(Comp4);
            }
            else if (e.PropertyName.Equals("NbrFil"))
            {
                _DB2.UpdateCompoNbrFil(Comp4);
                CheckWorkspaceReadiness();
            }
            else if (e.PropertyName.Equals("GetComposant"))
            {
                _DB2.UpdateCompoComposant(Comp4);
            }
            else if (e.PropertyName.Equals("NumComposant"))
            {
                _DB2.UpdateCompoNumComposant(Comp4);
            }
            else if (e.PropertyName.Equals("GetMatiere"))
            {
                _DB2.UpdateCompoMatiere(Comp4);
            }
            else if (e.PropertyName.Equals("Torsion"))
            {
                _DB2.UpdateCompoTorsion(Comp4);
            }
            else if (e.PropertyName.Equals("Enfilage"))
            {
                _DB2.UpdateCompoEnfilage(Comp4);
            }
            else if (e.PropertyName.Equals("Emb"))
            {
                _DB2.UpdateCompoEmb(Comp4);
            }
            else if (e.PropertyName.Equals("Num"))
            {
                _DB2.UpdateCompoNum(Comp4);
            }
            else if (e.PropertyName.Equals("Observation"))
            {
                _DB2.UpdateCompoObservation(Comp4);
            }
        }

        private void Comp5_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _DB2.UpdateCompoPoids(Comp5);
            }
            else if (e.PropertyName.Equals("EnfNbrFil"))
            {
                _DB2.UpdateCompoEnfNbr(Comp5);
            }
            else if (e.PropertyName.Equals("NbrFil"))
            {
                _DB2.UpdateCompoNbrFil(Comp5);
                CheckWorkspaceReadiness();
            }
            else if (e.PropertyName.Equals("GetComposant"))
            {
                _DB2.UpdateCompoComposant(Comp5);
            }
            else if (e.PropertyName.Equals("NumComposant"))
            {
                _DB2.UpdateCompoNumComposant(Comp5);
            }
            else if (e.PropertyName.Equals("GetMatiere"))
            {
                _DB2.UpdateCompoMatiere(Comp5);
            }
            else if (e.PropertyName.Equals("Torsion"))
            {
                _DB2.UpdateCompoTorsion(Comp5);
            }
            else if (e.PropertyName.Equals("Enfilage"))
            {
                _DB2.UpdateCompoEnfilage(Comp5);
            }
            else if (e.PropertyName.Equals("Emb"))
            {
                _DB2.UpdateCompoEmb(Comp5);
            }
            else if (e.PropertyName.Equals("Num"))
            {
                _DB2.UpdateCompoNum(Comp5);
            }
            else if (e.PropertyName.Equals("Observation"))
            {
                _DB2.UpdateCompoObservation(Comp5);
            }
        }

        private void Comp6_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _DB2.UpdateCompoPoids(Comp6);
            }
            else if (e.PropertyName.Equals("EnfNbrFil"))
            {
                _DB2.UpdateCompoEnfNbr(Comp6);
            }
            else if (e.PropertyName.Equals("NbrFil"))
            {
                _DB2.UpdateCompoNbrFil(Comp6);
                CheckWorkspaceReadiness();
            }
            else if (e.PropertyName.Equals("GetComposant"))
            {
                _DB2.UpdateCompoComposant(Comp6);
            }
            else if (e.PropertyName.Equals("NumComposant"))
            {
                _DB2.UpdateCompoNumComposant(Comp6);
            }
            else if (e.PropertyName.Equals("GetMatiere"))
            {
                _DB2.UpdateCompoMatiere(Comp6);
            }
            else if (e.PropertyName.Equals("Torsion"))
            {
                _DB2.UpdateCompoTorsion(Comp6);
            }
            else if (e.PropertyName.Equals("Enfilage"))
            {
                _DB2.UpdateCompoEnfilage(Comp6);
            }
            else if (e.PropertyName.Equals("Num"))
            {
                _DB2.UpdateCompoNum(Comp6);
            }
            else if (e.PropertyName.Equals("Emb"))
            {
                _DB2.UpdateCompoEmb(Comp6);
            }
            else if (e.PropertyName.Equals("Observation"))
            {
                _DB2.UpdateCompoObservation(Comp6);
            }
        }

        private void Comp7_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _DB2.UpdateCompoPoids(Comp7);
            }
            else if (e.PropertyName.Equals("EnfNbrFil"))
            {
                _DB2.UpdateCompoEnfNbr(Comp7);
            }
            else if (e.PropertyName.Equals("NbrFil"))
            {
                _DB2.UpdateCompoNbrFil(Comp7);
                CheckWorkspaceReadiness();
            }
            else if (e.PropertyName.Equals("GetComposant"))
            {
                _DB2.UpdateCompoComposant(Comp7);
            }
            else if (e.PropertyName.Equals("NumComposant"))
            {
                _DB2.UpdateCompoNumComposant(Comp7);
            }
            else if (e.PropertyName.Equals("GetMatiere"))
            {
                _DB2.UpdateCompoMatiere(Comp7);
            }
            else if (e.PropertyName.Equals("Torsion"))
            {
                _DB2.UpdateCompoTorsion(Comp7);
            }
            else if (e.PropertyName.Equals("Num"))
            {
                _DB2.UpdateCompoNum(Comp7);
            }
            else if (e.PropertyName.Equals("Enfilage"))
            {
                _DB2.UpdateCompoEnfilage(Comp7);
            }
            else if (e.PropertyName.Equals("Emb"))
            {
                _DB2.UpdateCompoEmb(Comp7);
            }
            else if (e.PropertyName.Equals("Observation"))
            {
                _DB2.UpdateCompoObservation(Comp7);
            }
        }

        private void Comp8_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _DB2.UpdateCompoPoids(Comp8);
            }
            else if (e.PropertyName.Equals("EnfNbrFil"))
            {
                _DB2.UpdateCompoEnfNbr(Comp8);
            }
            else if (e.PropertyName.Equals("NbrFil"))
            {
                _DB2.UpdateCompoNbrFil(Comp8);
            }
            else if (e.PropertyName.Equals("GetComposant"))
            {
                _DB2.UpdateCompoComposant(Comp8);
            }
            else if (e.PropertyName.Equals("NumComposant"))
            {
                _DB2.UpdateCompoNumComposant(Comp8);
            }
            else if (e.PropertyName.Equals("GetMatiere"))
            {
                _DB2.UpdateCompoMatiere(Comp8);
            }
            else if (e.PropertyName.Equals("Torsion"))
            {
                _DB2.UpdateCompoTorsion(Comp8);
            }
            else if (e.PropertyName.Equals("Enfilage"))
            {
                _DB2.UpdateCompoEnfilage(Comp8);
            }
            else if (e.PropertyName.Equals("Emb"))
            {
                _DB2.UpdateCompoEmb(Comp8);
            }
            else if (e.PropertyName.Equals("Observation"))
            {
                _DB2.UpdateCompoObservation(Comp8);
            }
        }

        private void Comp9_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Did the property cash change?
            if (e.PropertyName.Equals("Poids"))
            {
                CalculateTotalWeight();
                _DB2.UpdateCompoPoids(Comp9);
            }
            else if (e.PropertyName.Equals("EnfNbrFil"))
            {
                _DB2.UpdateCompoEnfNbr(Comp9);
            }
            else if (e.PropertyName.Equals("NbrFil"))
            {
                _DB2.UpdateCompoNbrFil(Comp9);
            }
            else if (e.PropertyName.Equals("GetComposant"))
            {
                _DB2.UpdateCompoComposant(Comp9);
            }
            else if (e.PropertyName.Equals("NumComposant"))
            {
                _DB2.UpdateCompoNumComposant(Comp9);
            }
            else if (e.PropertyName.Equals("GetMatiere"))
            {
                _DB2.UpdateCompoMatiere(Comp9);
            }
            else if (e.PropertyName.Equals("Torsion"))
            {
                _DB2.UpdateCompoTorsion(Comp9);
            }
            else if (e.PropertyName.Equals("Enfilage"))
            {
                _DB2.UpdateCompoEnfilage(Comp9);
            }
            else if (e.PropertyName.Equals("Emb"))
            {
                _DB2.UpdateCompoEmb(Comp9);
            }
            else if (e.PropertyName.Equals("Observation"))
            {
                _DB2.UpdateCompoObservation(Comp9);
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
                SchCompList.Add(Comp1);
            else if (CompNum == 2)
                SchCompList.Add(Comp2);
            else if (CompNum == 3)
                SchCompList.Add(Comp3);
            else if (CompNum == 4)
                SchCompList.Add(Comp4);
            else if (CompNum == 5)
                SchCompList.Add(Comp5);
            else if (CompNum == 6)
                SchCompList.Add(Comp6);
            else if (CompNum == 7) SchCompList.Add(Comp7);
            //SchCompList=new MvxObservableCollection<Composition>(SchCompList.OrderBy(c => c.Num));
        }

        public void RemoveLegend(int CompNum)
        {
            if (CompNum == 1)
                SchCompList.Remove(Comp1);
            else if (CompNum == 2)
                SchCompList.Remove(Comp2);
            else if (CompNum == 3)
                SchCompList.Remove(Comp3);
            else if (CompNum == 4)
                SchCompList.Remove(Comp4);
            else if (CompNum == 5)
                SchCompList.Remove(Comp5);
            else if (CompNum == 6)
                SchCompList.Remove(Comp6);
            else if (CompNum == 7) SchCompList.Remove(Comp7);
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
                Comp1.NumComposant = 1;
            }
            else if (CompNum == 2)
            {
                Comp2.NumComposant = 2;
            }
            else if (CompNum == 3)
            {
                Comp3.NumComposant = 3;
            }
            else if (CompNum == 4)
            {
                Comp4.NumComposant = 4;
            }
            else if (CompNum == 5)
            {
                Comp5.NumComposant = 5;
            }
            else if (CompNum == 6)
            {
                Comp6.NumComposant = 6;
            }
            else if (CompNum == 7)
            {
                Comp7.NumComposant = 7;
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
                        if (IsEnfilage) AddLegend(1);

                        Comp1.NumComposant = 1;
                        Comp1.ProdID = NewProd;
                        var TempoComp = new Composition();
                        TempoComp = (Composition)Comp1.Clone();
                        var tempID = _DB2.AddProdCompo(TempoComp);
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
                                    _DB2.RemoveProdCompo(Comp1);
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
                        if (IsEnfilage) AddLegend(2);
                        Comp2.ProdID = NewProd;
                        Comp2.NumComposant = 2;
                        Comp2.ID = _DB2.AddProdCompo((Composition)Comp2.Clone());
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
                                    _DB2.RemoveProdCompo(Comp2);
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
                        if (IsEnfilage) AddLegend(3);
                        Comp3.ProdID = NewProd;
                        Comp3.NumComposant = 3;
                        Comp3.ID = _DB2.AddProdCompo((Composition)Comp3.Clone());
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
                                    _DB2.RemoveProdCompo(Comp3);
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
                        if (IsEnfilage) AddLegend(4);
                        Comp4.ProdID = NewProd;
                        Comp4.NumComposant = 4;
                        Comp4.ID = _DB2.AddProdCompo((Composition)Comp4.Clone());
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
                                    _DB2.RemoveProdCompo(Comp4);
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
                        if (IsEnfilage) AddLegend(5);
                        Comp5.ProdID = NewProd;
                        Comp5.NumComposant = 5;
                        Comp5.ID = _DB2.AddProdCompo((Composition)Comp5.Clone());
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
                                    _DB2.RemoveProdCompo(Comp5);
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
                        if (IsEnfilage) AddLegend(6);
                        Comp6.ProdID = NewProd;
                        Comp6.NumComposant = 6;
                        Comp6.ID = _DB2.AddProdCompo((Composition)Comp6.Clone());
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
                                    _DB2.RemoveProdCompo(Comp6);
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
                        if (IsEnfilage) AddLegend(7);
                        Comp7.ProdID = NewProd;
                        Comp7.NumComposant = 7;
                        Comp7.ID = _DB2.AddProdCompo((Composition)Comp7.Clone());
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
                                    _DB2.RemoveProdCompo(Comp7);
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
                        Comp8.ID = _DB2.AddProdCompo((Composition)Comp8.Clone());
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
                                    _DB2.RemoveProdCompo(Comp8);
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
                        Comp9.ID = _DB2.AddProdCompo((Composition)Comp9.Clone());
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
                                    _DB2.RemoveProdCompo(Comp9);
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

        private bool IsAllFT = true;
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
                if (value != null) UpdateListTitrage1();

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
                if (value != null) UpdateCouleurList2();

                RaisePropertyChanged();
            }
        }

        public Titrage SelectedTitrage3
        {
            get => _SelectedTitrage3;
            set
            {
                _SelectedTitrage3 = value;
                if (value != null) UpdateCouleurList3();

                RaisePropertyChanged();
            }
        }

        public Titrage SelectedTitrage4
        {
            get => _SelectedTitrage4;
            set
            {
                _SelectedTitrage4 = value;
                if (value != null) UpdateCouleurList4();

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
                    UpdateCouleurList5();
                    ;
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
                if (value != null) UpdateCouleurList6();

                RaisePropertyChanged();
            }
        }

        public Titrage SelectedTitrage7
        {
            get => _SelectedTitrage7;
            set
            {
                _SelectedTitrage7 = value;
                if (value != null) UpdateCouleurList7();

                RaisePropertyChanged();
            }
        }

        private MvxObservableCollection<DuitageGomme> _DuitageGoList;


        private MvxObservableCollection<chaine> _ChaineNameList;

        private bool _IsFicheUniDuitage = true;

        private bool _IsFicheNormal = true;

        private bool _IsFicheEHC;


        public Titrage SelectedTitrage8
        {
            get => _SelectedTitrage8;
            set
            {
                _SelectedTitrage8 = value;
                if (value != null) UpdateCouleurList8();

                RaisePropertyChanged();
            }
        }

        public Titrage SelectedTitrage9
        {
            get => _SelectedTitrage9;
            set
            {
                _SelectedTitrage9 = value;
                if (value != null) UpdateCouleurList9();

                RaisePropertyChanged();
            }
        }


        public Couleur SelectedColor1
        {
            get => _SelectedColor1;
            set
            {
                _SelectedColor1 = value;
                if (value != null) UpdateMatiareComp1();

                RaisePropertyChanged();
            }
        }

        public void UpdateMatiareComp1()
        {
            if (SelectedColor1 != null && SelectedTitrage1 != null)
                Comp1.GetMatiere = _DB2.GetMatiere(SelectedTitrage1, SelectedColor1);
        }

        public void UpdateMatiareComp2()
        {
            if (SelectedColor2 != null && SelectedTitrage2 != null)
                Comp2.GetMatiere = _DB2.GetMatiere(SelectedTitrage2, SelectedColor2);
        }

        public void UpdateMatiareComp3()
        {
            if (SelectedColor3 != null && SelectedTitrage3 != null)
                Comp3.GetMatiere = _DB2.GetMatiere(SelectedTitrage3, SelectedColor3);
        }

        public void UpdateMatiareComp4()
        {
            if (SelectedColor4 != null && SelectedTitrage4 != null)
                Comp4.GetMatiere = _DB2.GetMatiere(SelectedTitrage4, SelectedColor4);
        }

        public void UpdateMatiareComp5()
        {
            if (SelectedColor5 != null && SelectedTitrage5 != null)
                Comp5.GetMatiere = _DB2.GetMatiere(SelectedTitrage5, SelectedColor5);
        }

        public void UpdateMatiareComp6()
        {
            if (SelectedColor6 != null && SelectedTitrage6 != null)
                Comp6.GetMatiere = _DB2.GetMatiere(SelectedTitrage6, SelectedColor6);
        }

        public void UpdateMatiareComp7()
        {
            if (SelectedColor7 != null && SelectedTitrage7 != null)
                Comp7.GetMatiere = _DB2.GetMatiere(SelectedTitrage7, SelectedColor7);
        }

        public void UpdateMatiareComp8()
        {
            if (SelectedColor8 != null && SelectedTitrage8 != null)
                Comp8.GetMatiere = _DB2.GetMatiere(SelectedTitrage8, SelectedColor8);
        }

        public void UpdateMatiareComp9()
        {
            if (SelectedColor9 != null && SelectedTitrage9 != null)
                Comp9.GetMatiere = _DB2.GetMatiere(SelectedTitrage9, SelectedColor9);
        }

        public Titrage SelectedTitrage1
        {
            get => _SelectedTitrage1;
            set
            {
                _SelectedTitrage1 = value;
                if (value != null) UpdateCouleurList1();

                RaisePropertyChanged();
            }
        }

        public ChaineBoardStructure ChaineBoard { get; set; }

        public ChaineBoardStructure ChaineBoard2 { get; set; }

        public int ChaineColumns
        {
            get => _ChaineColumns;
            set
            {
                _ChaineColumns = value;
                if (EnableChaineCreation) RefreshChaine(ChaineColumns, ChRowSum);

                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ChColComp> _ChcolList;


        public ObservableCollection<ChColComp> ChcolList
        {
            get
            {
                return _ChcolList;
            }
            set
            {
                _ChcolList = value;
                RaisePropertyChanged();
            }
        }

        

        public int ChRowSum
        {
            get => _ChRowSum;
            set
            {
                _ChRowSum = value;
                if (EnableChaineCreation) RefreshChaine(ChaineColumns, ChRowSum);

                RaisePropertyChanged();
            }
        }

        private int _ChaineRows = 8;

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

        public TrulyObservableCollection<MatrixElement> EnfilageList
        {
            get => _EnfilageList;
            set
            {
                _EnfilageList = value;
                RaisePropertyChanged();
            }
        }

        public BoardStructure EnfilageBoard { get; set; }

        public int EnfilageColumns { get; set; } = 83;

        public int EnfilageRow { get; set; } = 58;

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


        private bool _IsFicheCrochetage;

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
            set
            {
                _IsAddEnabled = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsTempo = true;

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
            get => _Compolist;
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
                if (value)
                {
                   
                    
                   
                    
                    ChcolList = new ObservableCollection<ChColComp>();
                    Compolist = new List<Composant>(_DB2.GetComposants());
                    RefreshChaine(4, 8);
                    ChaineColumns = 4;
                    ChRowSum = 8;
                    EnableChaineCreation = true;
                    UseCreatedChaine = false;

                    SelectedChaine = null;
                    DesignationChaine = "Chaine " + ChaineNameList.Count;
                    if (NewProd.EnfilageID.GetChaine != null)
                    {
                        _DB2.RemoveEnfilageChaine(NewProd.EnfilageID);
                        NewProd.EnfilageID.GetChaine = null;
                    }
                }
                else
                {
                  
                    
                    EnableChaineCreation = false;
                    UseCreatedChaine = true;
                    RefreshChaine(4, 8);
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

        public chaine OldSelectedChaine;

        public chaine SelectedChaine
        {
            get => _SelectedChaine;
            set
            {
                OldSelectedChaine = _SelectedChaine;
                //ResetChaineInaccessible(_SelectedChaine);
                _SelectedChaine = value;
                RaisePropertyChanged();
                if (SelectedChaine != null)
                {
                    SetupChaine(SelectedChaine.Colonne, SelectedChaine.Ligne, SelectedChaine.ChMatrix);
                    SetInaccessibleCompCells();

                    if (IsDentFilVis) CheckWorkspaceReadiness();
                }
            }
        }

        private int _StartChaineRow;

        public int StartChaineRow
        {
            get => _StartChaineRow;
            set
            {
                _StartChaineRow = value;
                RaisePropertyChanged();
            }
        }

        private int _StartLegendRow;

        public int StartLegendRow
        {
            get => _StartLegendRow;
            set
            {
                _StartLegendRow = value;
                RaisePropertyChanged();
            }
        }

        public void ResetChaineInaccessible(chaine OldChaine)
        {
            if (IsDentFilVis)
                if (OldChaine != null)
                {
                    if (OldChaine.Ligne >= 78)
                    {
                        var NumRow = OldChaine.Colonne * 2 + 1;

                        var lastcol = 4 + 78;
                        if (OldChaine.Colonne <= 8) NumRow = 8 * 2 + 1;

                        StartChaineRow = 58 - NumRow;

                        for (var j = StartChaineRow; j < 58; j++)
                        for (var i = 0; i < lastcol; i++)
                        {
                            var ij = j * 83 + i;
                            EnfilageList[ij].SetBoxType(MatrixElement.BoxType.OutRange);
                        }
                    }
                    else if (OldChaine.Ligne <= 26)
                    {
                        var NumRow = 4 + OldChaine.Ligne;
                        StartChaineRow = 58 - NumRow;
                        var lastcol = OldChaine.Colonne;
                        if (OldChaine.Colonne <= 8) lastcol = 8;

                        for (var j = StartChaineRow; j < 58; j++)
                        for (var i = 0; i < lastcol; i++)
                        {
                            var ij = j * 83 + i;
                            EnfilageList[ij].SetBoxType(MatrixElement.BoxType.OutRange);
                        }
                    }
                    else if (OldChaine.Ligne > 26)
                    {
                        var NumRow = OldChaine.Colonne + 1;

                        var lastcol = 4 + OldChaine.Ligne;
                        if (OldChaine.Colonne <= 8) NumRow = 8 + 1;
                        StartChaineRow = 58 - NumRow;
                        for (var j = StartChaineRow; j < 58; j++)
                        for (var i = 0; i < lastcol; i++)
                        {
                            var ij = j * 83 + i;
                            EnfilageList[ij].SetBoxType(MatrixElement.BoxType.OutRange);
                        }
                    }
                }
        }

        private ProhibitedRectangle _ProhibitArea;

        public ProhibitedRectangle ProhibitArea
        {
            get => _ProhibitArea;
            set
            {
                _ProhibitArea = value;
                RaisePropertyChanged();
            }
        }

        public void SetInaccessibleCompCells()
        {
            if (IsDentFilVis)
            {
                var lastcol = 0;
                if (SelectedChaine != null && ChRowSum >= 78)
                {
                    var NumRow = ChaineColumns * 2 + 1;

                    lastcol = 5 + ChaineRows;
                    if (ChaineColumns <= 8) NumRow = 8 * 2 + 1;
                    StartChaineRow = 59 - NumRow;
                    SecondRect = new SecRectangle(StartChaineRow, 0, 0);
                }
                else
                {
                    if (SchCompList.Count > 0)
                    {
                        //var NumCol = Convert.ToInt32(Math.Ceiling(SchCompList.Count * 18 / (double)12));
                        StartLegendRow = SchCompList.Count ;
                    }

                    if (SelectedChaine != null)
                    {
                        if (SelectedChaine.Ligne <= 26)
                        {
                            var NumRow = 3 + SelectedChaine.Ligne;
                            StartChaineRow = 59 - NumRow;
                            lastcol = SelectedChaine.Colonne;
                            if (SelectedChaine.Colonne <= 8) lastcol = 8;
                        }
                        else if (ChRowSum > 26)
                        {
                            var NumRow = ChaineColumns + 1;

                            lastcol = 4 + ChaineRows;
                            if (ChaineColumns <= 8) NumRow = 8 + 1;
                            StartChaineRow = 59 - NumRow;
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
                        SecondRect = new SecRectangle(StartChaineRow, 82 - StartWidth,6);
                    else
                        SecondRect = new SecRectangle(StartLegendRow,82 - StartWidth, 6);
                }

                ProhibitArea = new ProhibitedRectangle(StartLegendRow, StartChaineRow, lastcol);
            }
        }

        private bool _IsProduction;

        public bool IsProduction
        {
            get => _IsProduction;
            set
            {
                _IsProduction = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsEnfilage = true;
        public bool IsEnfilage
        {
            get => _IsEnfilage;
            set
            {
                _IsEnfilage = value;
                RaisePropertyChanged();
                if (EnableEditing && NewProd != null)
                {
                    if (IsEnfilage)
                    {
                        NewProd.IsEnfilage = 1;
                    }
                    else
                    {
                        NewProd.IsEnfilage = 0;
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
                    _DB2.UpdateEnfilageElement(AddedElement.EnfElement.X, AddedElement.EnfElement.Y,
                        NewProd.EnfilageID.ID, AddedElement.Content);
                }
                else if (AddedElement.AddElement)
                {
                    var LastMatrixID = _DB2.GetLatestEnfilageMatrixID();
                    var EnfMatrix = new EnfilageMatrix();
                    EnfMatrix.ID = LastMatrixID;
                    EnfMatrix.x = AddedElement.EnfElement.X;
                    EnfMatrix.y = AddedElement.EnfElement.Y;
                    EnfMatrix.DentFil = AddedElement.EnfElement.DentFil;
                    var TempCompo = _DB2.GetCompositions(NewProd.Id, NewProd.Version);
                    if (AddedElement.EnfElement.DentFil == 2)
                    {
                        EnfMatrix.value = null;
                        EnfMatrix.Enf = NewProd.EnfilageID;
                        _DB2.AddEnfilageMultiplier(EnfMatrix);
                    }
                    else
                    {
                        EnfMatrix.value = TempCompo.First(co => co.ID == AddedElement.EnfElement.Content.ID);
                        EnfMatrix.Enf = NewProd.EnfilageID;
                        _DB2.AddNewEnfilageMatrix(EnfMatrix);
                    }
                    
                }
                else if (AddedElement.AddElement == false)
                {
                    _DB2.DeleteEnfilageElement(AddedElement.EnfElement.X, AddedElement.EnfElement.Y,
                        NewProd.EnfilageID.ID);
                }
            }
        }

        public void ClearChaineContents()
        {
            foreach (var ch in ChaineList) ch.IsContent = false;
            foreach (var ch in ChaineList2) ch.IsContent = false;
        }

        public void DesignChain(List<ChaineMatrix> chmat)
        {
            foreach (var ch in ChaineList)
            {
                var exist = chmat.SingleOrDefault(chi => chi.x == ch.X && chi.y == ch.Y);
                if (exist != null)
                {
                    if (ch.CellState != ChaineMatrixElement.ComponentState.Occupied) ch.CellState = ChaineMatrixElement.ComponentState.Occupied;
                }
                else
                {
                    ch.CellState = ChaineMatrixElement.ComponentState.Vacant;
                }
            }

            foreach (var ch in ChaineList2)
            {
                var exist = chmat.SingleOrDefault(chi => chi.x == ch.X && chi.y == ch.Y + 78);
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

        public bool IsChainDimensionDifferent(int ChaineColumn,int ChaineRow)
        {
          
            return ChaineColumn != ChaineColumns || ChaineRow != ChRowSum || ChaineList.Count == 0;

        }

        public void SetFicheTechniqueChaine()
        {

            if (SelectedChaine != null && (NewProd.EnfilageID.GetChaine == null || NewProd.EnfilageID.GetChaine.ID != SelectedChaine.ID))
            {
                NewProd.EnfilageID.GetChaine = SelectedChaine;
                ChcolList = new ObservableCollection<ChColComp>(SelectedChaine.ChaineCompos);
                _DB2.UpdateEnfilageChaine(NewProd.EnfilageID);
            }
        }

        public void SetupChaine(int ChCol, int ChRow, List<ChaineMatrix> Chmat)
        {
            try
            {
                if (IsChainDimensionDifferent(ChCol,ChRow))
                {
                    RefreshChaine(ChCol, ChRow);
                    ChaineColumns = ChCol;
                    ChRowSum = ChRow;
                }

                DesignChain(Chmat);
                SetFicheTechniqueChaine();

              
            }
            catch(Exception ex)
            {
                SendNotification.Raise(ex.ToString());
            }
           
        }
        public bool IsEditWorkspace(int nbrPart)
        {
            bool IsEdit = false;
            if (WorkRectan == null || nbrPart != WorkRectan.NbrPart) IsEdit = true;
            if (OldSelectedChaine != null && OldSelectedChaine.Colonne != SelectedChaine.Colonne) IsEdit = true;
            if (OldSelectedChaine != null && OldSelectedChaine.Ligne != SelectedChaine.Ligne) IsEdit = true;


            return IsEdit;
        }

        public void SetupEnfilageWorkspace(int sumNbrFil)
        {
            var pn = decimal.Divide(sumNbrFil, EnfilageColumns - 2);
            var NbrPart = Convert.ToInt32(Math.Ceiling(pn));
            var workRect = new WorkRectangle();
            if (IsEditWorkspace(NbrPart))
            {
                

                var PartHeight = 4 + SelectedChaine.Colonne;
                workRect.ChaineRow = SelectedChaine.Ligne;
                workRect.ChaineColumn= SelectedChaine.Colonne;

                workRect.SecEmptyLen = 1;
                int AcceptablePartNum = 0;
              
                if (SecondRect.StartHeight / PartHeight < NbrPart)
                {
                    pn = decimal.Divide(sumNbrFil, EnfilageColumns);
                    NbrPart = Convert.ToInt32(Math.Ceiling(pn));
                    workRect.PartsWidthStart = 0;
                    workRect.PartsWidthEnd = 82;
                    
                    if (SecondRect.StartHeight / (PartHeight - 1) < NbrPart)
                    {
                        PartHeight = 6 + SelectedChaine.Colonne;
                        workRect.SecEmptyLen = 3;
                    }
                    else
                    {
                        PartHeight=3+SelectedChaine.Colonne;
                        AcceptablePartNum = NbrPart;
                        workRect.SecEmptyLen = 0;
                    }
                    if (SelectedChaine.Ligne > 26)
                    {
                        AcceptablePartNum = SecondRect.StartHeight/ PartHeight;
                    }
                    else
                    {

                        AcceptablePartNum =(int) EnfilageRow / PartHeight;
                    }

                   
                       
                    
                }
                else
                {
                   
                        AcceptablePartNum = NbrPart;
                  
                    workRect.PartsWidthStart = 1;
                    workRect.PartsWidthEnd = 81;
                }

                workRect.LisseLen = + SelectedChaine.Colonne;

                workRect.FirstEmptyLen = workRect.LisseLen + 1;
                workRect.DentLen = workRect.FirstEmptyLen + 1;
                workRect.NbrPart = NbrPart;
                workRect.PartsHeight = PartHeight * NbrPart;
                
                workRect.PartsWidth = workRect.PartsWidthEnd + 1 - workRect.PartsWidthStart;
                if (workRect.PartsHeight >=SecondRect.StartHeight )
                {
                    workRect.PartsHeightStart = 0;
                    workRect.PartsHeightEnd = AcceptablePartNum*PartHeight-1;
                }
                else
                {
                    workRect.PartsHeightStart = (SecondRect.StartHeight - workRect.PartsHeight) / 2;
                    ;
                    workRect.PartsHeightEnd = workRect.PartsHeightStart + AcceptablePartNum*PartHeight-1;
                }

                workRect.PartHeight = PartHeight;

                workRect.MaxNbrFil = workRect.PartsWidth * workRect.NbrPart;

                WorkRectan = workRect;
            }
        }

        private WorkRectangle _WorkRectan;

        public WorkRectangle WorkRectan
        {
            get => _WorkRectan;
            set
            {
                _WorkRectan = value;
                RaisePropertyChanged();
            }
        }

        private chaine _SelectedChaine;

        public void ValidationChaine()
        {
            if (ChcolList.Count == ChaineColumns && ChaineList.FirstOrDefault(ch=>ch.CellState==ChaineMatrixElement.ComponentState.Occupied)!=null)
            {
                var NewChaine = new chaine();
                NewChaine.Colonne = ChaineColumns;
                NewChaine.Ligne = ChRowSum;
                NewChaine.Nom = DesignationChaine;
                NewChaine.ID = _DB2.AddNewChaine(NewChaine);
                foreach (var Ch in ChaineList)
                    if (Ch.CellState==ChaineMatrixElement.ComponentState.Occupied)
                    {
                        var ChMatrix = new ChaineMatrix();
                        ChMatrix.x = Ch.X;
                        ChMatrix.y = Ch.Y;
                        ChMatrix.Chaine = NewChaine;
                        _DB2.AddNewChaineElement(ChMatrix);
                    }

                foreach (var Ch in ChaineList2)
                    if (Ch.CellState==ChaineMatrixElement.ComponentState.Occupied)
                    {
                        var ChMatrix = new ChaineMatrix();
                        ChMatrix.x = Ch.X;
                        ChMatrix.y = Ch.Y + 78;
                        ChMatrix.Chaine = NewChaine;
                        _DB2.AddNewChaineElement(ChMatrix);
                    }

                IsCreateChaine = false;
               

                foreach (var chcol in ChcolList)
                {
                    chcol.ChaineID = NewChaine.ID;
                    _DB2.AddChColComp(chcol);
                }

                ChaineNameList = new MvxObservableCollection<chaine>(_DB2.GetChaines());
                SelectedChaine = ChaineNameList.First(ch => ch.ID == NewChaine.ID);
            }
            else
            {
                if(_ChcolList.Count != ChaineColumns)
                 SendNotification.Raise("Vérifier les composants de chaque colonne");
                else
                {
                    SendNotification.Raise("La chaine est vide");
                }
            }
        }

        private MvxObservableCollection<Catalogue> _CategorieList;

        public MvxObservableCollection<Catalogue> CategorieList
        {
            get => _CategorieList;
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
                    if (ft.Catalog != null && ft.Catalog.ID == SelectedCatSearch.ID)
                        FullFTByCatList.Add(ft);


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
            get => _SelectedCatSearch;
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
            get => _SelectedCategorie;
            set
            {
                if (_SelectedCategorie != null) _PrevCat = _SelectedCategorie;

                _SelectedCategorie = value;
                RaisePropertyChanged();
                SetCategorie();
            }
        }

        private Catalogue _PrevCat;
        private string _Ordre;

        public string Ordre
        {
            get => _Ordre;
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
                _DB2.UpdateFicheTechniqueCategorie(NewProd.FicheId, SelectedCategorie);
                if (mf != null && mf.IsEchantillon == false)
                {
                    if (_PrevCat != null) _DB2.AssignOrderToFicheTechnique(_PrevCat.ID);


                    var ord = _DB2.AssignOrderToFicheTechnique(NewProd.FicheId, SelectedCategorie.ID);
                    Ordre = ord.ToString();
                }
            }
        }

        private string _CategorieName;

        public string CategorieName
        {
            get => _CategorieName;
            set
            {
                _CategorieName = value;
                RaisePropertyChanged();
            }
        }

        private bool _tempo;

        public bool tempo
        {
            get => _tempo;
            set
            {
                _tempo = value;
                RaisePropertyChanged();
            }
        }

        private bool _SuperUser;

        public bool SuperUser
        {
            get => _SuperUser;
            set
            {
                _SuperUser = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsVerificateur;

        public bool IsVerificateur
        {
            get => _IsVerificateur;
            set
            {
                _IsVerificateur = value;
                RaisePropertyChanged();
            }
        }

        private MvxObservableCollection<Reed> _ListPeigne;

        public MvxObservableCollection<Reed> ListPeigne
        {
            get => _ListPeigne;
            set
            {
                _ListPeigne = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsRedacteur;

        public bool IsRedacteur
        {
            get => _IsRedacteur;
            set
            {
                _IsRedacteur = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsConfirm;

        public bool IsConfirm
        {
            get => _IsConfirm;
            set
            {
                _IsConfirm = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsPerm;

        public bool IsPerm
        {
            get => _IsPerm;
            set
            {
                _IsPerm = value;
                RaisePropertyChanged();
            }
        }

        private EnfilageElement _AddedElement;

        private bool _UseCreatedChaine = true;

        private bool _EnableChaineCreation;

        #endregion
    }
}