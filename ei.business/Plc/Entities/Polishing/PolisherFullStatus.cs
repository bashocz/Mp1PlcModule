using System;
using System.Collections.Generic;

namespace EI.Business
{
    public class PolisherFullStatus : IPolisherFullStatus
    {
        #region IPolisherStatus members

        public PolisherState State { get; set; }
        public IMagazine Magazine { get; set; }
        public bool HighPressure { get; set; }
        public TimeSpan HighPressureDuration { get; set; }
        public int PlateRpm { get; set; }
        public double PlateLoadCurrent { get; set; }
        public IPolisherLiquid PolisherLiquid { get; set; }
        public TimeSpan PadUsedTime { get; set; }
        public int PadUsedCount { get; set; }

        private List<IPolisherHead> _polisherHeads = new List<IPolisherHead>();
        public IList<IPolisherHead> PolisherHeads
        {
            get { return _polisherHeads; }
        }

        public void NewPolisherHeads(ICollection<IPolisherHead> polisherHeads)
        {
            _polisherHeads.Clear();
            _polisherHeads.AddRange(polisherHeads);
        }

        public override string ToString()
        {
            return string.Format("State:{0},Mag:{1},HP:{2},HPD:{3},PlateRpm:{4},PlateLoadCurrent:{5},PoliLiquid:{6},PadUsedTime:{7},PadUsedCount:{8}",
                State,
                Magazine.Id,
                HighPressure,
                HighPressureDuration,
                PlateRpm,
                PlateLoadCurrent,
                PolisherLiquid.ToString(),
                PadUsedTime,
                PadUsedCount);
        }

        #endregion
    }
}
