namespace BackEnd2.CustomClass
{
    public class ProhibitedRectangle
    {
        public ProhibitedRectangle(int StartLeg, int StartChain, int EndCol)
        {
            StartLegendRow = StartLeg;
            StartChaineRow = StartChain;
            EndColumn = EndCol;
        }

        public int StartLegendRow { get; set; }

        public int StartChaineRow { get; set; }

        public int EndColumn { get; set; }
    }
}