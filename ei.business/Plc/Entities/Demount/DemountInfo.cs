namespace EI.Business
{
    public class DemountInfo : IDemountInfo
    {
        #region IDemountInfo members

        public bool Completed { get; set; }
        public int DemountedWaferCount { get; set; }

        #endregion
    }
}
