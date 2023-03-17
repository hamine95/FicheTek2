using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using BackEnd2.Data;
using BackEnd2.Database;
using BackEnd2.Model;
using MvvmCross;
using MvvmCross.Binding.Extensions;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class RapportViewModel:MvxViewModel<user>
    {
        private SqliteData _Db;
        private user UserSession;

        private MvxObservableCollection<ReportProduct> _ProductList;

        public MvxObservableCollection<ReportProduct> ProductList
        {
            get
            {
                return _ProductList;
            }
            set
            {
                _ProductList = value;
                RaisePropertyChanged();
            }
        }

      

        private int _CurrentYear;

        public int CurrentYear
        {
            get
            {
                return _CurrentYear;
            }
            set
            {
                _CurrentYear = value;
                RaisePropertyChanged();
            }
        }
        private int _CurrentYearAnnual;

        public int CurrentYearAnnual
        {
            get
            {
                return _CurrentYearAnnual;
            }
            set
            {
                _CurrentYearAnnual = value;
                RaisePropertyChanged();
            }
        }

        private IMvxCommand _PrintAnnulReportCmd;

        public IMvxCommand PrintAnnulReportCmd
        {
            get
            {
                _PrintAnnulReportCmd = new MvxCommand(NavigateToPrintAnnualReport);
                return _PrintAnnulReportCmd;
            }
        }

        public void NavigateToPrintAnnualReport()
        {
            _navigationService.Navigate<AnnualReportPrintViewModel, MvxObservableCollection<AnnualReport>>(AnnulReportList);
        }
        private IMvxCommand _PrintCmd;

        public IMvxCommand PrintCmd
        {
            get
            {
                _PrintCmd = new MvxCommand(NavigateToPrintView);
                return _PrintCmd;
            }
        }

        public void NavigateToPrintView()
        {
            _navigationService.Navigate<MonthlyReportPrintViewModel, MvxObservableCollection<ReportProduct>>(ProductList);
        }

        private IMvxCommand _DisplayCmd;

        public IMvxCommand DisplayCmd
        {
            get
            {
                 _DisplayCmd=new MvxCommand(SetDisplayReport);
                 return _DisplayCmd;
            }
        }

        public void SetDisplayReport()
        {
            if(SelectedReport==null)
                return;

            ProductList = new MvxObservableCollection<ReportProduct>(_Db.GetReportArticles(SelectedReport.id));
            ArticleControlled= ProductList.Count();
            ArticleUpdate= ProductList.Where(pr =>Convert.ToInt32(pr.Version) > 1 && pr.nonConforme==false).Count();
            ArticleCreated=ProductList.Where(pr => Convert.ToInt32(pr.Version) <= 1  && pr.nonConforme==false).Count();
            ArticleOrdre=ProductList.Where(pr => Convert.ToInt32(pr.Version) == 1 && pr.nonConforme==false).Count();
            ArticleNonConforme=ProductList.Where(pr =>pr.nonConforme==true).Count();
            ProductList=new MvxObservableCollection<ReportProduct>(ProductList.OrderBy(pr => pr.DateProd).ToList())  ;
        }
        
        private IMvxCommand _SaveCmd;

        public IMvxCommand SaveCmd
        {
            get
            {
                _SaveCmd = new MvxCommand(SaveReport);
                return _SaveCmd;
            }
        }

        public void SaveReport()
        {
            if(ProductList==null || ProductList.Count()==0)
                return;

           
            var rep = new MonthlyReport();
            rep.month = ((DateTime)ProductList[0].DateProd).Month;
            rep.year = ((DateTime)ProductList[0].DateProd).Year.ToString();

            int id = 0;
            if (ReportList.FirstOrDefault(repo => repo.month == rep.month && repo.year == rep.year) == null)
            {
                 id= _Db.AddNewReport(rep);
            }
            else
            {
                id = ReportList.FirstOrDefault(repo => repo.month == rep.month && repo.year == rep.year).id;
            }
           
           foreach (var element in ProductList)
           {
               element.repID = id;
               if (_Db.GetReportArticle(element.repID,element.Ref) != null)
               {
                   element.id = _Db.GetReportArticle(element.repID, element.Ref).id;
                   _Db.UpdateMonthlyReportArticle(element);
               }
               else
               {
                   _Db.AddNewReportArticles(element);
               }
              
              
           }
           
           GetReportList();
        }

        private MvxObservableCollection<MonthlyReport> _ReportList;

        public MvxObservableCollection<MonthlyReport> ReportList
        {
            get
            {
                return _ReportList;
            }
            set
            {
                _ReportList = value;
                RaisePropertyChanged();
            }
        }
 
        private IMvxCommand _AppliquerCmd;

        public IMvxCommand AppliquerCmd
        {
            get
            {
                _AppliquerCmd = new MvxCommand(GetFicheTechniqueByDateAndYear);
                return _AppliquerCmd;
            }
        }

        private MvxObservableCollection<AnnualReport> _AnnulReportList;

        public MvxObservableCollection<AnnualReport> AnnulReportList
        {
            get
            {
                return _AnnulReportList;
            }
            set
            {
                _AnnulReportList = value;
                RaisePropertyChanged();
            }
        }


        private IMvxCommand _AppliquerAnnualCmd;

        public IMvxCommand AppliquerAnnualCmd
        {
            get
            {
                _AppliquerAnnualCmd = new MvxCommand(getReportByYear);
                return _AppliquerAnnualCmd;
            }
        }

        public void GetAnnualReportTemplate()
        {
            AnnulReportList = new MvxObservableCollection<AnnualReport>();
            var catlist = _Db.GetCategoriesWithoutChildren();
            foreach (var element in catlist)
            {
                var AnnualRep = new AnnualReport();
                AnnualRep.categorie = element.Designation;
                
                AnnulReportList.Add(AnnualRep);
            }
        }
        
        public void getReportByYear()
        {
            if(CurrentYearAnnual==0)
                return;
            var RepList = ReportList.Where(rep => Convert.ToInt32(rep.year) == CurrentYearAnnual).ToList();
            var RepArticlesList = new List<ReportProduct>();
            foreach (var rep in RepList)
            {
                RepArticlesList.AddRange( _Db.GetReportArticles(rep.id));  
            }

            AnnulReportList = new MvxObservableCollection<AnnualReport>();

            var catlist = _Db.GetCategoriesWithoutChildren();
            foreach (var element in catlist)
            {
                var catArtList = RepArticlesList.Where(repart => repart.categorie!=null && repart.categorie.Equals(element.Designation));
                var AnnualRep = new AnnualReport();
                AnnualRep.categorie = element.Designation;
                if (catArtList.Count() > 0)
                {
                    AnnualRep.Commande = catArtList.Where(prod =>Convert.ToInt32(prod.Version) == 1 && prod.nonConforme==false).Count();
                    AnnualRep.Echantillon = catArtList.Where(prod => Convert.ToInt32(prod.Version) == 0 && prod.nonConforme==false).Count();
                    AnnualRep.miseajour = catArtList.Where(prod => Convert.ToInt32(prod.Version) > 1 && prod.nonConforme==false).Count();
                    AnnualRep.Conforme = catArtList.Where(prod => prod.nonConforme).Count();
                    
                }
                AnnulReportList.Add(AnnualRep);
            }

            TotalCommande = AnnulReportList.Sum(rep => rep.Commande).ToString();
            TotalEchantillon = AnnulReportList.Sum(rep => rep.Echantillon).ToString();
            TotalCree = (Convert.ToInt32(TotalCommande)+Convert.ToInt32(TotalEchantillon)).ToString();
            
            TotalUpdate = AnnulReportList.Sum(rep => rep.miseajour ).ToString();
            TotalNonConforme = AnnulReportList.Sum(rep => rep.Conforme).ToString();
            TotalControle = (Convert.ToInt32(TotalUpdate)+Convert.ToInt32(TotalNonConforme)).ToString();

            if(Convert.ToInt32(TotalCree)==0)
                return;
            PourcentageCommande = Math.Round((double)Convert.ToInt32(TotalCommande) / Convert.ToInt32(TotalCree) * 100)
                .ToString()+" %";
            PourcentageEchantillon = Math.Round((double)Convert.ToInt32(TotalEchantillon) / Convert.ToInt32(TotalCree) * 100)
                .ToString()+" %";

            if (Convert.ToInt32(TotalControle) == 0)
            {
                PourcentageUpdate = "0 %";
                PourcentageNonConforme = "0 %";
                return;
            }
               
            PourcentageUpdate = Math.Round((double)Convert.ToInt32(TotalUpdate) / Convert.ToInt32(TotalControle) * 100)
                .ToString()+" %";
            PourcentageNonConforme = Math.Round((double)Convert.ToInt32(TotalNonConforme) / Convert.ToInt32(TotalControle) * 100)
                .ToString()+" %";
            
        }

        public void GetFicheTechniqueByDateAndYear()
        {
            ProductList = new MvxObservableCollection<ReportProduct>();
            if(SelectedMonth==null || CurrentYear==0)
                return;
            DateTime StartDate=new DateTime(CurrentYear,SelectedMonth.Num , 1);
            DateTime EndDate=new DateTime(CurrentYear,SelectedMonth.Num, DateTime.DaysInMonth(CurrentYear, SelectedMonth.Num));
            List<Produit> CreationProdlist = _Db.GetProductByCreationDate(StartDate, EndDate);
            ProductList=new MvxObservableCollection<ReportProduct>() ;
            foreach (var element in CreationProdlist)
            {
                var prod = new ReportProduct();
                prod.DateProd = element.DateCreation.Value;
                prod.Ref = element.Ref;
                prod.Designation = element.Name;
                prod.Version = element.Version.ToString();
                prod.categorie =_Db.GetFichetechniqueCategorie(element.FicheId);
                
                if (element.Version <= 1)
                {
                    prod.Creation = true;
                }
                ProductList.Add(prod);
            }
            List<Produit> UpdateProdList = _Db.GetProductByUpdateDate(StartDate, EndDate);
            foreach (var element in UpdateProdList)
            {
                var prod = new ReportProduct();
                prod.DateProd = element.MiseAJour.Value;
                prod.Ref = element.Ref;
                prod.Designation = element.Name;
                prod.Version = element.Version.ToString();
                if (element.Version >= 1)
                {
                    prod.miseajour = true;
                }
                prod.categorie =_Db.GetFichetechniqueCategorie(element.FicheId);
                ProductList.Add(prod);
            }
            ArticleControlled= ProductList.Count();
           ArticleUpdate= ProductList.Where(pr => Convert.ToInt32(pr.Version) > 1 && pr.nonConforme==false).Count();
           ArticleCreated=ProductList.Where(pr => Convert.ToInt32(pr.Version) <= 1 && pr.nonConforme==false).Count();
           ArticleOrdre=ProductList.Where(pr => Convert.ToInt32(pr.Version) == 1 && pr.nonConforme==false).Count();
           ArticleNonConforme=ProductList.Where(pr => pr.nonConforme ).Count();
           ProductList=new MvxObservableCollection<ReportProduct>(ProductList.OrderBy(pr => pr.DateProd).ToList())  ;
        }

        private IMvxCommand _AddMonthRepCmd;

        public IMvxCommand AddMonthRepCmd
        {
            get
            {
                _AddMonthRepCmd = new MvxAsyncCommand(AddNewArticleToMonthReport);
                return _AddMonthRepCmd;
            }
        }

        public async Task AddNewArticleToMonthReport()
        {
            var NewProd= await  _navigationService.Navigate<AddMonthReportViewModel,bool,ReportProduct>(true);
            if(NewProd==null)
                return;
            ProductList.Add(NewProd);
            ArticleControlled= ProductList.Count();
            ArticleUpdate= ProductList.Where(pr => Convert.ToInt32(pr.Version) > 1 && pr.nonConforme==false).Count();
            ArticleCreated=ProductList.Where(pr => Convert.ToInt32(pr.Version) <= 1 && pr.nonConforme==false).Count();
            ArticleOrdre=ProductList.Where(pr => Convert.ToInt32(pr.Version) == 1 && pr.nonConforme==false).Count();
            ArticleNonConforme=ProductList.Where(pr => pr.nonConforme ).Count();
        }
        
        private IMvxCommand _EditMonthRepCmd;

        public IMvxCommand EditMonthRepCmd
        {
            get
            {
                _EditMonthRepCmd = new MvxAsyncCommand(EditReportProduct);
                return _EditMonthRepCmd;
            }
        }


        private ReportProduct _SelectedProduct;

        public ReportProduct SelectedProduct
        {
            get
            {
                return _SelectedProduct;
            }
            set
            {
                _SelectedProduct = value;
                RaisePropertyChanged();
            }
        }
        
        public async Task EditReportProduct()
        {
            if (SelectedProduct == null)
                return;
         var ProdChanges= await  _navigationService.Navigate<EditMonthReportViewModel, ReportProduct,ReportProduct>(SelectedProduct);
         if(ProdChanges==null)
             return;
         ArticleControlled= ProductList.Count();
         ArticleUpdate= ProductList.Where(pr => Convert.ToInt32(pr.Version) > 1 && pr.nonConforme==false).Count();
         ArticleCreated=ProductList.Where(pr => Convert.ToInt32(pr.Version) <= 1 && pr.nonConforme==false).Count();
         ArticleOrdre=ProductList.Where(pr => Convert.ToInt32(pr.Version) == 1 && pr.nonConforme==false).Count();
         ArticleNonConforme=ProductList.Where(pr => pr.nonConforme ).Count();
         
        }

        private int _ArticleControlled;

        private int _ArticleUpdate;

        private int _ArticleCreated;

        private int _ArticleNonConforme;
        
        private int _ArticleOrdre;
        
        public int ArticleControlled
        {
            get
            {
                return _ArticleControlled;
            }
            set
            {
                _ArticleControlled = value;
                RaisePropertyChanged();
            }
        }

        public int ArticleUpdate
        {
            get
            {
                return _ArticleUpdate;
            }
            set
            {
                _ArticleUpdate = value;
                RaisePropertyChanged();
            }
        }

        public int ArticleCreated
        {
            get
            {
                return _ArticleCreated;
            }
            set
            {
                _ArticleCreated = value;
                RaisePropertyChanged();
            }
        }

        public int ArticleNonConforme
        {
            get
            {
                return _ArticleNonConforme;
            }
            set
            {
                _ArticleNonConforme = value;
                RaisePropertyChanged();
            }
        }
        
        

        public int ArticleOrdre
        {
            get
            {
                return _ArticleOrdre;
            }
            set
            {
                _ArticleOrdre = value;
                RaisePropertyChanged();
            }
        }
        private readonly IMvxNavigationService _navigationService;

        public RapportViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
            CurrentYear = DateTime.Now.Year;
            CurrentYearAnnual = DateTime.Now.Year;
            MonthList = new MvxObservableCollection<MonthKeyValue>();
            MonthList.Add(new MonthKeyValue(1,"Janvier")
                );
            MonthList.Add(new MonthKeyValue(2,"Février"));
            MonthList.Add(new MonthKeyValue(3,"Mars"));
            MonthList.Add(new MonthKeyValue(4,"Avril"));
            MonthList.Add(new MonthKeyValue(5,"Mai"));
            MonthList.Add(new MonthKeyValue(6,"Juin"));
            MonthList.Add(new MonthKeyValue(7,"Juillet"));
            MonthList.Add(new MonthKeyValue(8,"Aout"));
            MonthList.Add(new MonthKeyValue(9,"Septembre"));
            MonthList.Add(new MonthKeyValue(10,"Octobre"));
            MonthList.Add(new MonthKeyValue(11,"Novembre"));
            MonthList.Add(new MonthKeyValue(12,"Décembre"));
            
            
           
        }


        public void GetReportList()
        {
            ReportList = new MvxObservableCollection<MonthlyReport>(_Db.GetReports());
        }

        private MonthlyReport _SelectedReport;

        public MonthlyReport SelectedReport
        {
            get
            {
                return _SelectedReport;
            }
            set
            {
                _SelectedReport = value;
                RaisePropertyChanged();
            }
        }

        private string _TotalCommande;

        public string TotalCommande
        {
            get
            {
                return _TotalCommande;
            }
            set
            {
                _TotalCommande = value;
                RaisePropertyChanged();
            }
        }
        private string _TotalEchantillon;

        public string TotalEchantillon
        {
            get
            {
                return _TotalEchantillon;
            }
            set
            {
                _TotalEchantillon = value;
                RaisePropertyChanged();
            }
        }
        private string _TotalCree;

        public string TotalCree
        {
            get
            {
                return _TotalCree;
            }
            set
            {
                _TotalCree = value;
                RaisePropertyChanged();
            }
        }
        private string _PourcentageCommande;

        public string PourcentageCommande
        {
            get
            {
                return _PourcentageCommande;
            }
            set
            {
                _PourcentageCommande = value;
                RaisePropertyChanged();
            }
        }
        private string _PourcentageEchantillon;

        public string PourcentageEchantillon
        {
            get
            {
                return _PourcentageEchantillon;
            }
            set
            {
                _PourcentageEchantillon = value;
                RaisePropertyChanged();
            }
        }
        
        private string _TotalUpdate;

        public string TotalUpdate
        {
            get
            {
                return _TotalUpdate;
            }
            set
            {
                _TotalUpdate = value;
                RaisePropertyChanged();
            }
        }
        private string _TotalNonConforme;

        public string TotalNonConforme
        {
            get
            {
                return _TotalNonConforme;
            }
            set
            {
                _TotalNonConforme = value;
                RaisePropertyChanged();
            }
        }
        private string _TotalControle;

        public string TotalControle
        {
            get
            {
                return _TotalControle;
            }
            set
            {
                _TotalControle = value;
                RaisePropertyChanged();
            }
        }
        private string _PourcentageUpdate;

        public string PourcentageUpdate
        {
            get
            {
                return _PourcentageUpdate;
            }
            set
            {
                _PourcentageUpdate = value;
                RaisePropertyChanged();
            }
        }
        private string _PourcentageNonConforme;

        public string PourcentageNonConforme
        {
            get
            {
                return _PourcentageNonConforme;
            }
            set
            {
                _PourcentageNonConforme = value;
                RaisePropertyChanged();
            }
        }
        
        private MonthKeyValue _SelectedMonth;

        public MonthKeyValue SelectedMonth
        {
            get
            {
                return _SelectedMonth;
            }
            set
            {
                _SelectedMonth = value;
                RaisePropertyChanged();
            }
        }

        private MvxObservableCollection<MonthKeyValue> _MonthList;

        public MvxObservableCollection<MonthKeyValue> MonthList
        {
            get
            {
                return _MonthList;
            }
            set
            {
                _MonthList = value;
                RaisePropertyChanged();
            }
        }

        public override void Prepare(user parameter)
        {
            _Db = Mvx.IoCProvider.Resolve<SqliteData>();
            UserSession = parameter;
            
            GetReportList();
            GetAnnualReportTemplate();
        }
    }
}