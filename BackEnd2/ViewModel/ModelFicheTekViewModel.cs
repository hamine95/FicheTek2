using BackEnd2.CustomClass;
using BackEnd2.Model;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class ModelFicheTekViewModel : MvxViewModel<ModelFiche, ModelFiche>
    {

        private readonly IMvxNavigationService _navigationService;
        public ModelFicheTekViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
        }

        public ModelFiche _model;
        public override void Prepare(ModelFiche parameter)
        {
            _model = parameter;
        }


        private IMvxCommand _AnnulerCmd;


        #region Properties
        

        private bool _IsEchantillon;
        public bool IsEchantillon
        {
            get => _IsEchantillon;
            set
            {
                _IsEchantillon = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsEhc;
        public bool IsEhc
        {
            get => _IsEhc;
            set
            {
                _IsEhc = value;
                RaisePropertyChanged();
            }
        }


        private bool _IsTissage=true;

        public bool IsTissage
        {
            get { return _IsTissage; }
            set
            {
                _IsTissage = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsTressage;
        public bool IsTressage
        {
            get => _IsTressage;
            set
            {
                _IsTressage = value;
                RaisePropertyChanged();
            }
        }

        private bool _IsCrochetage;
        public bool IsCrochetage
        {
            get => _IsCrochetage;
            set
            {
                _IsCrochetage = value;
                RaisePropertyChanged();
            }
        }


        #endregion


        #region Commands
        public IMvxCommand AnnulerCmd
        {
            get
            {
                _AnnulerCmd = new MvxCommand(AnnulerCommand);
                return _AnnulerCmd;
            }
        }
        private IMvxCommand _ValiderCmd;
        public IMvxCommand ValiderCmd
        {
            get
            {
                _ValiderCmd = new MvxCommand(ValiderCommand);
                return _ValiderCmd;
            }
        }
        #endregion

        #region Methods
        public void ClosingView(ModelFiche mModel)
        {
            _navigationService.Close(this, mModel);
        }
        public void AnnulerCommand()
        {
            ClosingView(null);
        }
        public void ValiderCommand()
        {
            _model.IsEchantillon = IsEchantillon;
            if (IsEhc)
            {
                if (IsTissage)
                {
                    _model.model = ModelFiche.ModelFicheTek.EHCTissage;
                }
                else if (IsTressage)
                {
                    _model.model = ModelFiche.ModelFicheTek.EHCTressage;
                }
                else
                {
                    _model.model = ModelFiche.ModelFicheTek.EHCCrochetage;
                }
            }
            else
            {
                if(IsTissage)
                {
                    _model.model = ModelFiche.ModelFicheTek.AutreTissage;
                }
                else if(IsTressage)
                {
                    _model.model = ModelFiche.ModelFicheTek.AutreTressage;
                }
                else
                {
                    _model.model = ModelFiche.ModelFicheTek.AutreCrochetage;
                }
            }
            ClosingView(_model);
            //SendNotification.Raise("Aucun Modèle séléctionné");
            
        }

        #endregion



       

        #region Events
        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();
        public MvxInteraction<YesNoQuestion> ConfirmAction { get; } = new MvxInteraction<YesNoQuestion>();
        #endregion















       
    }
}