using System.Collections.Generic;

namespace EI.Business
{
    public interface IDemountStatus
    {
        bool IsCarrierPlateArrived { get; }
        bool IsCarrierPlateDemountStarted { get; }
        IDemountInfo DemountInfo { get; }
        DemountCassetteHopper CanReadCassetteBarcode { get; }
        IList<bool> AreCassettes { get; }
        DemountState State { get; }
    }
}
