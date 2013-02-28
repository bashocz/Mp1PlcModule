using System;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    class CarrierPlateRoutingToStreamConverter : BaseToStreamConverter<CarrierPlateRouting>
    {
        #region constructors

        public CarrierPlateRoutingToStreamConverter() { }

        #endregion

        #region conversion methods

        private string CarrierPlateRoutingToStream(CarrierPlateRouting routing)
        {
            switch (routing)
            {
                case CarrierPlateRouting.InsertIntoMagazine:
                    return "0001";
                case CarrierPlateRouting.Reject:
                    return "0002";
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type CarrierPlateRouting, actual value is '{0}'.", routing), "routing");
            }
        }

        #endregion

        #region BaseToStreamConverter members

        protected override bool CheckParameter(CarrierPlateRouting routing)
        {
            if ((routing != CarrierPlateRouting.InsertIntoMagazine) && (routing != CarrierPlateRouting.Reject))
                throw ThrowPlcExceptionInvalidEnumValue("routing", routing);
            return true;
        }

        protected override int GetLength(CarrierPlateRouting routing)
        {
            return 1;
        }

        protected override string GetStream(CarrierPlateRouting routing)
        {
            return CarrierPlateRoutingToStream(routing);
        }

        #endregion
    }
}
