using BackEnd2.Data;
using BackEnd2.Model;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd2.ViewModel
{

   
    public class EnfilageCorrectionViewModel:MvxViewModel<Produit>
    {
        private IMvxNavigationService _NavigationService;
        public EnfilageCorrectionViewModel(IMvxNavigationService _navSer)
        {
            _NavigationService = _navSer;
        }


        #region Properties
        private Produit _product;

        public Produit product
        {
            get { return _product; }
            set { _product = value;
                RaisePropertyChanged();
            }
        }

        private MvxObservableCollection<Composition> _ComposantList;

        public MvxObservableCollection<Composition> ComposantList
        {
            get { return _ComposantList; }
            set { _ComposantList = value; }
        }


        private int _NumDent;

        public int NumDent
        {
            get { return _NumDent; }
            set { _NumDent = value;
                RaisePropertyChanged();
            }
        }

        #endregion
        #region Commands

        #endregion
        #region Methods

        public void SaveChange()
        {
            product.EnfDent = NumDent;
            _db.UpdateEnfDent(product);
            foreach(var comp in ComposantList)
            {
                _db.UpdateCompoEnfNbr(comp);
            }
            _NavigationService.Close(this);

        }
        public void Cancel()
        {
            _NavigationService.Close(this);
        }
        private SqliteData _db;
        public override void Prepare(Produit parameter)
        {
            _db = Mvx.IoCProvider.Resolve<SqliteData>();
            product = parameter;
            NumDent = product.EnfDent;
            var EnfilageComposition = product.GetComposition.Where(comp => !comp.GetComposant.Name.ToLower().Equals("trame") && !comp.GetComposant.Name.ToLower().Equals("apport"));
            ComposantList =new MvxObservableCollection<Composition>(EnfilageComposition);
        }
        #endregion
        #region Events

        #endregion
    }
}
