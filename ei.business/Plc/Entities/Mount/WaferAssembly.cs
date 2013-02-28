namespace EI.Business
{
    public class WaferAssembly : IWaferAssembly
    {
        #region IWaferAssembly members

        public int CarrierPlateCount { get; set; }
        public int WaferCount { get; set; }

        #endregion
    }
}
