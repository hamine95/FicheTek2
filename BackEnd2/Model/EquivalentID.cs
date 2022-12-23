namespace BackEnd2.Model
{
    public class EquivalentID
    {
        private int _ID;

        private int _EquivID;

        public int Id
        {
            get => _ID;
            set => _ID = value;
        }

        public int EquivId
        {
            get => _EquivID;
            set => _EquivID = value;
        }
    }
}