﻿using System;
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
    public class CouleurViewModel : MvxViewModel<user>

    {
        private IMvxCommand _AjouterNovCol;

        private IMvxCommand _CancelCmd;
        private string _CouleurNom;

        private int _EditColorId;
        private bool _IsEditEnabled;
        private MvxObservableCollection<Couleur> _ListCouleur;

        private MvxNotifyTask _LoadTask;

        private IMvxCommand _Modifier;


        private IMvxNavigationService _navigationService;

        private string _NovCouleurNom;
        private string _NovNumero;
        private string _Numero;

        private IMvxCommand _SaveChange;

        private Couleur _SelectedColor;

        private IMvxCommand _Supprimer;
        private SqliteData db;

        public CouleurViewModel(IMvxNavigationService _navSer)
        {
            _navigationService = _navSer;
        }

        public MvxNotifyTask LoadTask
        {
            get => _LoadTask;
            private set => SetProperty(ref _LoadTask, value);
        }

        public MvxInteraction<YesNoQuestion> ConfirmAction { get; } = new MvxInteraction<YesNoQuestion>();

        public MvxInteraction<string> SendNotification { get; } = new MvxInteraction<string>();

        public int EditColorId
        {
            get => _EditColorId;
            set
            {
                _EditColorId = value;
                RaisePropertyChanged();
            }
        }

        public bool IsEditEnabled
        {
            get => _IsEditEnabled;
            set
            {
                _IsEditEnabled = value;
                RaisePropertyChanged();
            }
        }

        public IMvxCommand Modifier
        {
            get
            {
                _Modifier = new MvxCommand(ModifierColor);
                return _Modifier;
            }
        }

        public IMvxCommand Supprimer
        {
            get
            {
                _Supprimer = new MvxCommand(SupprimerColor);
                return _Supprimer;
            }
        }

        public IMvxCommand AjouterNovCol
        {
            get
            {
                _AjouterNovCol = new MvxAsyncCommand(async () => { await AjouterNovColor(); });

                return _AjouterNovCol;
            }
        }

        public IMvxCommand CancelCmd
        {
            get
            {
                _CancelCmd = new MvxCommand(CancelEdit);
                return _CancelCmd;
            }
        }

        public IMvxCommand SaveChange
        {
            get
            {
                _SaveChange = new MvxCommand(SaveEditChange);
                return _SaveChange;
            }
        }

        public MvxObservableCollection<Couleur> ListCouleur
        {
            get => _ListCouleur;
            set
            {
                _ListCouleur = value;
                RaisePropertyChanged();
            }
        }

        public Couleur SelectedColor
        {
            get => _SelectedColor;
            set
            {
                _SelectedColor = value;
                RaisePropertyChanged();
            }
        }

        public string Numero
        {
            get => _Numero;
            set
            {
                _Numero = value;
                RaisePropertyChanged();
            }
        }

        public string CouleurNom
        {
            get => _CouleurNom;
            set
            {
                _CouleurNom = value;
                RaisePropertyChanged();
            }
        }

        public string NovNumero
        {
            get => _NovNumero;
            set
            {
                _NovNumero = value;
                RaisePropertyChanged();
            }
        }

        public string NovCouleurNom
        {
            get => _NovCouleurNom;
            set
            {
                _NovCouleurNom = value;
                RaisePropertyChanged();
            }
        }


        public override void Prepare(user parameter)
        {
            db = Mvx.IoCProvider.Resolve<SqliteData>();
        }


        public override Task Initialize()
        {
            LoadTask = MvxNotifyTask.Create(UpdateColorList);
            return base.Initialize();
        }

        public void SaveEditChange()
        {
            if (!IsEditFieldsEmpty() && IsNumero(NovNumero) &&
                db.CheckCouleurUnique(Convert.ToInt32(NovNumero), NovCouleurNom) == null)
            {
                var NewColour = new Couleur();
                NewColour.ID = EditColorId;
                NewColour.Nbr = Convert.ToInt32(NovNumero);
                NewColour.Name = NovCouleurNom;
                db.EditColor(NewColour);
                UpdateColorList();
                CancelEdit();
            }
            else if (IsEditFieldsEmpty())
            {
                SendNotification.Raise("Remplit tout les champs");
            }
            else if (!IsNumero(NovNumero))
            {
                SendNotification.Raise("Choisir un numero correct");
            }
            else
            {
                SendNotification.Raise("Couleur existe déja");
            }
        }

        public async Task UpdateColorList()
        {
            var listColor =  db.GetCouleurs();
            foreach (var col in listColor) col.Name = col.Name.ToUpper();


            ListCouleur = new MvxObservableCollection<Couleur>(listColor.ToList());
        }

        public void CancelEdit()
        {
            IsEditEnabled = false;
            NovCouleurNom = "";
            NovNumero = "";
        }

        public void ModifierColor()
        {
            if (SelectedColor != null)
            {
                IsEditEnabled = true;
                NovCouleurNom = SelectedColor.Name;
                NovNumero = SelectedColor.Nbr.ToString();
                EditColorId = SelectedColor.ID;
            }
            else
            {
                SendNotification.Raise("S.V.P Séléctionnez une couleur");
            }
        }

        public void SupprimerColor()
        {
            if (SelectedColor != null)
            {
                var req = new YesNoQuestion
                {
                    Question = "êtes-vous sûr de vouloir supprimer cette Couleur séléctionnée ?",
                    UploadCallback = ok =>
                    {
                        if (ok)
                        {
                            db.DeleteColor(SelectedColor);
                            UpdateColorList();
                        }
                    }
                };
                ConfirmAction.Raise(req);
            }
            else
            {
                SendNotification.Raise("S.V.P Séléctionnez une couleur");
            }
        }

        public async Task AjouterNovColor()
        {
            await Task.Run(async () =>
                {
                 
                    if (!IsAddFieldsEmpty() && IsNumero(Numero) &&
                        db.CheckCouleurUnique(Convert.ToInt32(Numero), CouleurNom) == null)
                    {
                        var NewColour = new Couleur();
                        NewColour.Nbr = Convert.ToInt32(Numero);
                        NewColour.Name = CouleurNom;
                        db.AddNewColor(NewColour);
                        await UpdateColorList();
                        Numero = "";
                        CouleurNom = "";
                    }
                    else if (IsAddFieldsEmpty())
                    {
                        SendNotification.Raise("Remplit tout les champs");
                    }
                    else if (!IsNumero(Numero))
                    {
                        SendNotification.Raise("Choisir un numero correct");
                    }
                    else
                    {
                        SendNotification.Raise("Couleur existe déja");
                    }
                }
            );
        }

        public bool IsNumero(string Num)
        {
            try
            {
                Convert.ToInt32(Num);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IsAddFieldsEmpty()
        {
            return Numero == null || CouleurNom == null || string.IsNullOrWhiteSpace(Numero) ||
                   string.IsNullOrWhiteSpace(CouleurNom);
        }

        public bool IsEditFieldsEmpty()
        {
            return NovNumero == null || NovCouleurNom == null || string.IsNullOrWhiteSpace(NovNumero) ||
                   string.IsNullOrWhiteSpace(NovCouleurNom);
        }
    }
}