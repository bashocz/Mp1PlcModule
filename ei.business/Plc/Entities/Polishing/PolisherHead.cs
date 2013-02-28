namespace EI.Business
{
    public class PolisherHead : IPolisherHead
    {
        #region public members

        public int Force { get; set; }
        public double Pressure { get; set; }
        public double Backpressure { get; set; }
        public int Rpm { get; set; }
        public double LoadCurrent { get; set; }

        #endregion
    }
}