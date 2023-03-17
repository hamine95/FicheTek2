namespace BackEnd2.Model
{
    public class MonthlyReport
    {
        private int _id;
        private string _year;
        private int _month;
        private string _MonthName;

        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                
            }
        }
        public string MonthName
        {
            get
            {
                return _MonthName;
            }
            set
            {
                _MonthName = value;
                
            }
        }
        public string year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
                
            }
        }
        
        public int month
        {
            get
            {
                return _month;
            }
            set
            {
                _month = value;
                if (_month > 0)
                    SetMonthName();

            }
        }

        public void SetMonthName()
        {
            if (month == 1)
            {
                MonthName = "Janvier";
            }else if (month == 2)
            {
                MonthName = "Février";
            }else if (month == 3)
            {
                MonthName = "Mars";
            }else if (month == 4)
            {
                MonthName = "Avril";
            }else if (month == 5)
            {
                MonthName = "Mai";
            }else if (month == 6)
            {
                MonthName = "Juin";
            }else if (month == 7)
            {
                MonthName = "Juillet";
            }else if (month == 8)
            {
                MonthName = "Aout";
            }else if (month == 9)
            {
                MonthName = "Septembre";
            }else if (month == 10)
            {
                MonthName = "Octobre";
            }else if (month == 11)
            {
                MonthName = "Novembre";
            }else if (month == 12)
            {
                MonthName = "Décembre";
            }
               
            
        }
    }
}