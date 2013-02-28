using System.Collections.Generic;

namespace EI.Business
{
    public interface IMagazine
    {
        string Id { get; }

        int Capacity { get; }
        IList<ICarrierPlate> Plates { get; }
    }
}