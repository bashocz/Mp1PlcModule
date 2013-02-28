using System.Collections.Generic;

namespace EI.Business
{
    public class LotData : ILotData
    {
        #region ILotData members

        public string LotId { get; set; }
        public WaferSize WaferSize { get; set; }
        public OfType OfType { get; set; }
        public PolishDivision PolishDivision { get; set; }
        public IWaferAssembly Assembly1 { get; set; }
        public IWaferAssembly Assembly2 { get; set; }
        public int NGWaferCount { get; set; }

        private List<ICassette> _cassettes = new List<ICassette>();
        public IList<ICassette> Cassettes
        {
            get { return _cassettes; }
        }

        public void NewCassettes(ICollection<ICassette> cassettes)
        {
            _cassettes.Clear();
            _cassettes.AddRange(cassettes);
        }

        private List<IWafer> _wafers = new List<IWafer>();
        public IList<IWafer> Wafers
        {
            get { return _wafers; }
        }

        public void NewWafers(ICollection<IWafer> wafers)
        {
            _wafers.Clear();
            _wafers.AddRange(wafers);
        }

        #endregion
    }
}