using System;
using System.Collections.Generic;

namespace EI.Business
{
    public interface IPolisherFullStatus : IPolisherShortStatus
    {
        IMagazine Magazine { get; }

        TimeSpan HighPressureDuration { get; }

        int PlateRpm { get; }
        double PlateLoadCurrent { get; }
        IList<IPolisherHead> PolisherHeads { get; }
        IPolisherLiquid PolisherLiquid { get; }

        TimeSpan PadUsedTime { get; }
        int PadUsedCount { get; }
    }
}
