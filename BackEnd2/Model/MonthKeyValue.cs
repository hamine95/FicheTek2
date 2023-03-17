namespace BackEnd2.Model
{
    public class MonthKeyValue
    {

        public MonthKeyValue(int Num,string Name)
        {
            this.Num = Num;
            this.Name = Name;
        }
        
        private int _Num;
        private string _Name;

        public int Num
        {
            get
            {
                return _Num;
            }
            set
            {
                _Num = value;
                
            }
        }
        
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                
            }
        }
    }
}