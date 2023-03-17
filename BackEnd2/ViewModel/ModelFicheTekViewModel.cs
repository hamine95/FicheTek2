using BackEnd2.CustomClass;
using BackEnd2.Model;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class ModelFicheTekViewModel : MvxViewModel<ModelFiche, ModelFiche>
    {
        private IMvxCommand _AnnulerCmd;

        private IMvxCommand _CorchetCmd;
        private IMvxCommand _EhcCmd;
        private bool _IsCrochet;
        private bool _IsEchantillon;

        private bool _IsEhc;
        private bool _IsNormal;

        public ModelFiche _model;

        private readonly IMvxNavigationService _navigationService;

        private IMvxCommand _NormaleCmd;
        private IMvxCommand _ValiderCmd;

        public ModelFicheTekViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
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

        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();
        public MvxInteraction<YesNoQuestion> ConfirmAction { get; } = new MvxInteraction<YesNoQuestion>();

        public bool IsEhc
        {
            get => _IsEhc;
            set
            {
                _IsEhc = value;
                RaisePropertyChanged();
            }
        }

        public bool IsNormal
        {
            get => _IsNormal;
            set
            {
                _IsNormal = value;
                RaisePropertyChanged();
            }
        }

        public bool IsCrochet
        {
            get => _IsCrochet;
            set
            {
                _IsCrochet = value;
                RaisePropertyChanged();
            }
        }

        public IMvxCommand AnnulerCmd
        {
            get
            {
                _AnnulerCmd = new MvxCommand(AnnulerCommand);
                return _AnnulerCmd;
            }
        }

        public IMvxCommand ValiderCmd
        {
            get
            {
                _ValiderCmd = new MvxCommand(ValiderCommand);
                return _ValiderCmd;
            }
        }

        public IMvxCommand NormaleCmd
        {
            get
            {
                _NormaleCmd = new MvxCommand(NormalCommand);
                return _NormaleCmd;
            }
        }

        public IMvxCommand CorchetCmd
        {
            get
            {
                _CorchetCmd = new MvxCommand(CrochetCommand);
                return _CorchetCmd;
            }
        }

        public IMvxCommand EhcCmd
        {
            get
            {
                _EhcCmd = new MvxCommand(EhcCommand);
                return _EhcCmd;
            }
        }

        public void ClosingView(ModelFiche mModel)
        {
            _navigationService.Close(this, mModel);
        }

        public override void Prepare(ModelFiche parameter)
        {
            _model = parameter;
        }

        public void CrochetCommand()
        {
            IsCrochet = true;
        }

        public void NormalCommand()
        {
            IsNormal = true;
        }

        public void EhcCommand()
        {
            IsEhc = true;
        }

        public void ValiderCommand()
        {
            if (IsCrochet || IsEhc || IsNormal)
            {
                if (IsCrochet)
                    _model.model = ModelFiche.ModelFicheTek.FicheTekCrochetage;
                else if (IsEhc)
                    _model.model = ModelFiche.ModelFicheTek.FicheTekEHC;
                else
                    _model.model = ModelFiche.ModelFicheTek.FicheTekNormal;


                _model.IsEchantillon = IsEchantillon;


                ClosingView(_model);
            }
            else
            {
                SendNotification.Raise("Aucun Modèle séléctionné");
            }
        }

        public void AnnulerCommand()
        {
            ClosingView(null);
        }
    }
}