using System.Threading.Tasks;
using BackEnd2.CustomClass;
using BackEnd2.Model;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BackEnd2.ViewModel
{
    public class ModelFicheTekViewModel:MvxViewModel<ModelFiche,ModelFiche>
    {
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
        
        private IMvxNavigationService _navigationService;
        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();
        public MvxInteraction<YesNoQuestion> ConfirmAction { get; } = new MvxInteraction<YesNoQuestion>();

        public ModelFiche _model;
        
        public ModelFicheTekViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
           
        }
        public void ClosingView(ModelFiche mModel)
        {
             _navigationService.Close(this, mModel);
        }
        public override void Prepare(ModelFiche parameter)
        {
            _model = parameter;
        }

        private bool _IsEhc=false;
        private bool _IsNormal=false;
        private bool _IsCrochet=false;

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

        private IMvxCommand _AnnulerCmd;

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
        
        private IMvxCommand _NormaleCmd;

        public IMvxCommand NormaleCmd
        {
            get
            {
                _NormaleCmd = new MvxCommand(NormalCommand);
                return _NormaleCmd;
            }
        }
        
        private IMvxCommand _CorchetCmd;

        public IMvxCommand CorchetCmd
        {
            get
            {
                _CorchetCmd = new MvxCommand(CrochetCommand);
                return _CorchetCmd;
            }
        }
        private IMvxCommand _EhcCmd;

        public IMvxCommand EhcCmd
        {
            get
            {
                _EhcCmd = new MvxCommand(EhcCommand);
                return _EhcCmd;
            }
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
                {
                    _model.model = ModelFiche.ModelFicheTek.FicheTekCrochetage;
                }
                else if(IsEhc)
                {
                    _model.model = ModelFiche.ModelFicheTek.FicheTekEHC;
                }
                else
                {
                    _model.model = ModelFiche.ModelFicheTek.FicheTekNormal;
                }

                
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