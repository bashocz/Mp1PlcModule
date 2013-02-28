using System;
using System.Collections.Generic;

namespace EI.Business
{
    public interface IPolisherShortStatus
    {
        PolisherState State { get; }
        bool HighPressure { get; }
    }
}
