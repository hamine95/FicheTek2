using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static BackEnd2.Model.MatrixElement;

namespace BackEnd2.Model
{
    public class ComponentDepiction
    {
        private string _PrintVisual;

        public string PrintVisual
        {
            get { return _PrintVisual; }
            set { _PrintVisual = value; }
        }

        private string _NormalVisual;

        public string NormalVisual
        {
            get { return _NormalVisual; }
            set { _NormalVisual = value; }
        }
        public string ImagePath(CompImages val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
        public void GetImagePath(int num,bool IsNormal)
        {
            if(IsNormal)
            {
                if (num == 1)
                    NormalVisual = ImagePath(MatrixElement.CompImages.comp1);
                else if (num == 2)
                    NormalVisual = ImagePath(MatrixElement.CompImages.comp2);
                else if (num == 3)
                    NormalVisual = ImagePath(MatrixElement.CompImages.comp3);
                else if (num == 4)
                    NormalVisual = ImagePath(MatrixElement.CompImages.comp4);
                else if (num == 5)
                    NormalVisual = ImagePath(MatrixElement.CompImages.comp5);
                else if (num == 6)
                    NormalVisual = ImagePath(MatrixElement.CompImages.comp6);
                else if (num == 7)
                    NormalVisual = ImagePath(MatrixElement.CompImages.comp7);
            }
            else
            {

                if (num == 1)
                    PrintVisual = ImagePath(MatrixElement.CompImages.comp1Print);
                else if (num == 2)
                    PrintVisual = ImagePath(MatrixElement.CompImages.comp2Print);
                else if (num == 3)
                    PrintVisual = ImagePath(MatrixElement.CompImages.comp3Print);
                else if (num == 4)
                    PrintVisual = ImagePath(MatrixElement.CompImages.comp4Print);
                else if (num == 5)
                    PrintVisual = ImagePath(MatrixElement.CompImages.comp5Print);
                else if (num == 6)
                    PrintVisual = ImagePath(MatrixElement.CompImages.comp6Print);
                else if (num == 7)
                    PrintVisual = ImagePath(MatrixElement.CompImages.comp7Print);
            }
            

        }
        public ComponentDepiction(int PrintNum, string NormVis)
        {
             GetImagePath(PrintNum,false);
            NormalVisual = NormVis;
        }
        public ComponentDepiction(string PrintVis,string NormVis)
        {
            PrintVisual = PrintVis;
            NormalVisual = NormVis;
        }
    }
}
