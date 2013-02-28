using System.Collections.Generic;

namespace EI.Business
{
    public interface IPolishingFullStatus
    {
        IList<IPolisherFullStatus> Status { get; }
    }
}
