using System;
using System.Linq;
using BackEnd2.Data;
using BackEnd2.Database;
using BackEnd2.Model;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class AddMonthReportViewModel:MvxViewModel<bool,ReportProduct>
    {
        private IMvxNavigationService _NavigationService;

        public AddMonthReportViewModel(IMvxNavigationService _navser)
        {
            _NavigationService = _navser;
        }


        private IMvxCommand _SaveCmd;

        public IMvxCommand SaveCmd
        {
            get
            {
                _SaveCmd = new MvxCommand(SaveNewArticle);
                return _SaveCmd;
            }
        }
        private IMvxCommand _CancelCmd;

        public IMvxCommand CancelCmd
        {
            get
            {
                _CancelCmd = new MvxCommand(CloseView);
                return _CancelCmd;
            }
        }
        private bool _Etat;

        public bool Etat
        {
            get
            {
                return _Etat;
            }
            set
            {
                _Etat = value;
                RaisePropertyChanged();
            }
        }
        private int _Version;

        public int Version
        {
            get
            {
                return _Version;
            }
            set
            {
                _Version = value;
                RaisePropertyChanged();
            }
        }
        private string _Designation;

        public string Designation
        {
            get
            {
                return _Designation;
            }
            set
            {
                _Designation = value;
                RaisePropertyChanged();
            }
        }
        private string _RefProd;

        public string RefProd
        {
            get
            {
                return _RefProd;
            }
            set
            {
                _RefProd = value;
                RaisePropertyChanged();
            }
        }
        private string _DateProd;

        public string DateProd
        {
            get
            {
                return _DateProd;
            }
            set
            {
                _DateProd = value;
                RaisePropertyChanged();
            }
        }
        private string _Remarque;

        public string Remarque
        {
            get
            {
                return _Remarque;
            }
            set
            {
                _Remarque = value;
                RaisePropertyChanged();
            }
        }

        private MvxObservableCollection<FicheTechnique> _FicheTechniqueList;

        public MvxObservableCollection<FicheTechnique> FicheTechniqueList
        {
            get
            {
                return _FicheTechniqueList;
            }
            set
            {
                _FicheTechniqueList = value;
                RaisePropertyChanged();
            }
        }
        
        private FicheTechnique _SelectedFicheTechnique;

        public FicheTechnique SelectedFicheTechnique
        {
            get
            {
                return _SelectedFicheTechnique;
            }
            set
            {
                _SelectedFicheTechnique = value;
                RaisePropertyChanged();
                if(value==null)
                    return;
                SetSelectedProduct();

            }
        }

        public void SetSelectedProduct()
        {
            var ProdList = _db.GetProductsByFicheTechnique(SelectedFicheTechnique.ID);
            
            if(ProdList==null || ProdList.Count==0 )
                return;
            int lastVersNum= ProdList.Count - 1;
            RefProd =ProdList[lastVersNum].Ref;
            Designation=ProdList[lastVersNum].Name;
            
            if (ProdList[lastVersNum].Version > 1)
            {
                DateProd = ProdList[lastVersNum].MiseAJour.ToString();
            }
            else
            {
                DateProd = ProdList[lastVersNum].DateCreation.ToString();
            }

            VerList = new MvxObservableCollection<int>();
            foreach (var product in ProdList)
            {
                VerList.Add(product.Version);
            }

            Version = VerList.Last();

        }

        private MvxObservableCollection<int> _VerList;

        public MvxObservableCollection<int> VerList
        {
            get
            {
                return _VerList;
            }
            set
            {
                _VerList = value;
                RaisePropertyChanged();
            }
        }

        private ReportProduct RepReport;
        
        public void CloseView()
        {
            _NavigationService.Close(this,null);
        }

        public void SaveNewArticle()
        {
            if (SelectedFicheTechnique == null)
            {
                _NavigationService.Close(this,null);
                return;
            }

            RepReport = new ReportProduct();
            RepReport.DateProd =Convert.ToDateTime(DateProd);
            RepReport.Ref = RefProd;
            RepReport.Designation = Designation;
            RepReport.Version = Version.ToString();
            RepReport.nonConforme = Etat;
            RepReport.Remarque = Remarque;
            RepReport.categorie = _db.GetFichetechniqueCategorie(SelectedFicheTechnique.ID);
            
            _NavigationService.Close(this,RepReport);
        }

        private SqliteData _db;
        public override void Prepare(bool parameter)
        {
            _db = Mvx.IoCProvider.Resolve<SqliteData>();
           FicheTechniqueList = new MvxObservableCollection<FicheTechnique>(_db.GetFicheTechniques());
        }
    }
}