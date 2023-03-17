using System;
using System.Linq;
using System.Printing;
using System.Windows.Controls;
using BackEnd2.Model;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class MonthlyReportPrintViewModel:MvxViewModel<MvxObservableCollection<ReportProduct>>
    {
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

        private IMvxCommand<DocumentViewer> _PrintCmd;

        public IMvxCommand<DocumentViewer> PrintCmd
        {
            get
            {
                _PrintCmd = new MvxCommand<DocumentViewer>(PrintReport);
                return _PrintCmd;
            }
        }


        private string _SelectedPrint;

        public string SelectedPrint
        {
            get
            {
                return _SelectedPrint;
            }
            set
            {
                _SelectedPrint = value;
                RaisePropertyChanged();
            }
        }
        
        public void PrintReport(DocumentViewer doc)
        {
            if(SelectedPrint==null)
                return;
            
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
        
        public override void Prepare(MvxObservableCollection<ReportProduct> parameter)
        {
            try
            {
                if(parameter==null)
                    return;
                ProductList =new MvxObservableCollection<ReportProduct>( parameter);
                ArticleControlled= ProductList.Count();
                ArticleUpdate= ProductList.Where(pr => Convert.ToInt32(pr.Version) > 1).Count();
                ArticleCreated=ProductList.Where(pr => Convert.ToInt32(pr.Version) <= 1 ).Count();
                ArticleOrdre=ProductList.Where(pr => Convert.ToInt32(pr.Version) == 1).Count();
                for (int i = ProductList.Count; i <= 22;i++)
                {
                    ProductList.Add(new ReportProduct());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
             
        }
    }
}