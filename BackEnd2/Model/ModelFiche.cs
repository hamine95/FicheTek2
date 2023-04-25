﻿namespace BackEnd2.Model
{
    public class ModelFiche
    {
        public enum ModelFicheTek
        {
            FicheTekNormal,
            FicheTekEHC,
            FicheTekCrochetage
        }

        public bool IsEchantillon;


        public ModelFicheTek model;

        private int _num;

        public int num
        {
            get { return _num; }
            set { _num = value; }
        }

        private string _name;

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }


    }
}