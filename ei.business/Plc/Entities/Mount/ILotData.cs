using System.Collections.Generic;

namespace EI.Business
{
    public interface ILotData
    {
        string LotId { get; }

        IList<ICassette> Cassettes { get; }

        WaferSize WaferSize { get; }
        OfType OfType { get; }
        PolishDivision PolishDivision { get; }

        IWaferAssembly Assembly1 { get; }
        IWaferAssembly Assembly2 { get; }

        IList<IWafer> Wafers { get; }
        int NGWaferCount { get; }
    }
}
