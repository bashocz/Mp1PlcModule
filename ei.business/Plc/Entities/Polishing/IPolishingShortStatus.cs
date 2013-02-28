using System.Collections.Generic;

namespace EI.Business
{
    public interface IPolishingShortStatus
    {
        bool IsMagazineArrived { get; }
        IList<IPolisherShortStatus> Status { get; }
    }
}
