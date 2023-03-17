namespace BackEnd2.CustomClass
{
    public class SecRectangle
    {
        public SecRectangle(int SHeight, int EWidth, int SWidth)
        {
            StartHeight = SHeight;
            EndWidth = EWidth;
            StartWidth = SWidth;
        }

        public int StartHeight { get; set; }

        public int StartWidth { get; set; }

        public int EndWidth { get; set; }
    }
}