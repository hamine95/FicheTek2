using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BackEnd2.Model
{
    public class chaine: INotifyPropertyChanged
    {

        private int id;
        private string _Nom;
        private int _Col;
        private int _lign;


        private List<ChaineMatrix> _ChMatrix;


        public string Nom
        {
            get
            {
                return _Nom;
            }
            set
            {
                _Nom = value;
                NotifyPropertyChanged();
            }
        }

        public int Colonne
        {
            get
            {
                return _Col;
            }
            set
            {
                _Col = value;
                NotifyPropertyChanged();
            }
        }

        public int Ligne
        {
            get
            {
                return _lign;
            }
            set
            {
                _lign = value;
                NotifyPropertyChanged();
            }
        }

        [Key]
        public int ID
        {
            get => id;
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }

        private List<ChColComp> _ChaineCompos;

        public List<ChColComp> ChaineCompos
        {
            get
            {
                return _ChaineCompos;
            }
            set
            {
                _ChaineCompos = value;
                NotifyPropertyChanged();
            }
        }
        public List<ChaineMatrix> ChMatrix
        {
            get => _ChMatrix;
            set => _ChMatrix = value;
        }


        public event PropertyChangedEventHandler PropertyChanged;

  
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}