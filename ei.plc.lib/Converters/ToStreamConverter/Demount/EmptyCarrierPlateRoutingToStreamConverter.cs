using System;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    class EmptyCarrierPlateRoutingToStreamConverter : BaseToStreamConverter<CarrierPlateRoutingType>
    {
        #region constructors

        public EmptyCarrierPlateRoutingToStreamConverter() { }

        #endregion

        #region conversion methods

        private string CarrierPlateRoutingTypeToStream(CarrierPlateRoutingType type)
        {
            switch (type)
            {
                case CarrierPlateRoutingType.BackThroughAwps:
                    return "0001";
                case CarrierPlateRoutingType.InspectionRequired:
                    return "0002";
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type CarrierPlateRoutingType, actual value is '{0}'.", type), "type");
            }
        }

        #endregion

        #region BaseToStreamConverter members

        protected override bool CheckParameter(CarrierPlateRoutingType type)
        {
            if ((type != CarrierPlateRoutingType.BackThroughAwps) && (type != CarrierPlateRoutingType.InspectionRequired))
                throw ThrowPlcExceptionInvalidEnumValue("type", type);
            return true;
        }

        protected override int GetLength(CarrierPlateRoutingType type)
        {
            return 1;
        }

        protected override string GetStream(CarrierPlateRoutingType type)
        {
            return CarrierPlateRoutingTypeToStream(type);
        }

        #endregion
    }
}
