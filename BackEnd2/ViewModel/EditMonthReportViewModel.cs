using System;
using BackEnd2.Model;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class EditMonthReportViewModel:MvxViewModel<ReportProduct,ReportProduct>
    {


        private IMvxNavigationService _NavigationService;

        private ReportProduct ProductRep;
        
        public EditMonthReportViewModel(IMvxNavigationService navser)
        {
            _NavigationService = navser;
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
        private string _Version;

        public string Version
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

        private IMvxCommand _CancelCmd;

        public IMvxCommand CancelCmd
        {
            get
            {
                _CancelCmd = new MvxCommand(CloseView);
                return _CancelCmd;
            }
        }

        private IMvxCommand _SaveCmd;

        public IMvxCommand SaveCmd
        {
            get
            {
                _SaveCmd = new MvxCommand(SaveChanges);
                return _SaveCmd;
            }
        }

        
        
        public void CloseView()
        {
            _NavigationService.Close(this,null);
        }

        public void SaveChanges()
        {
            ProductRep.nonConforme = Etat;
            if (ProductRep.nonConforme)
            {
                ProductRep.miseajour = false;
                ProductRep.Creation = false;
            }
            else
            {
                if (Convert.ToInt32(ProductRep.Version) > 1)
                {
                    ProductRep.miseajour = true;
                }
                else
                {
                    ProductRep.Creation = true;
                }
            }

            ProductRep.Remarque = Remarque;
            _NavigationService.Close(this, ProductRep);

        }

        public override void Prepare(ReportProduct parameter)
        {
            ProductRep=parameter;
            DateProd = ProductRep.DateProd.ToString();
            RefProd = ProductRep.Ref;
            Designation = ProductRep.Designation;
            Version = ProductRep.Version;
            Etat = ProductRep.nonConforme;
            Remarque = ProductRep.Remarque;
        }
    }
}