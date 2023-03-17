using System;
using System.Linq;
using BackEnd2.Model;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class AnnualReportPrintViewModel:MvxViewModel<MvxObservableCollection<AnnualReport>>
    {
        
        
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
        
        public override void Prepare(MvxObservableCollection<AnnualReport> parameter)
        {
            _AnnulReportList = parameter;
            
            TotalCommande = AnnulReportList.Sum(rep => rep.Commande).ToString();
            TotalEchantillon = AnnulReportList.Sum(rep => rep.Echantillon).ToString();
            TotalCree = (Convert.ToInt32(TotalCommande)+Convert.ToInt32(TotalEchantillon)).ToString();
            
            TotalUpdate = AnnulReportList.Sum(rep => rep.miseajour).ToString();
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
    }
}