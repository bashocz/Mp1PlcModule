using System;
using System.Globalization;

namespace EI.Business
{
    public class CarrierPlate : ICarrierPlate
    {
        #region ICarrierPlate members

        public string Id { get; set; }
        public int Capacity { get; set; }
        public int Recipe { get; set; }

        #endregion
    }
}
