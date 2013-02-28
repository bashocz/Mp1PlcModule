using System;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    class LoadCassetteToStreamConverter : BaseToStreamConverter<LoadCassette>
    {
        #region constructors

        public LoadCassetteToStreamConverter() { }

        #endregion

        #region conversion methods

        private string CassetteHopperToStream(DemountCassetteHopper hopper)
        {
            switch (hopper)
            {
                case DemountCassetteHopper.Hopper1:
                    return "0001";
                case DemountCassetteHopper.Hopper2:
                    return "0002";
                case DemountCassetteHopper.Hopper3:
                    return "0003";
                case DemountCassetteHopper.Hopper4:
                    return "0004";
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type DemountCassetteHopper, actual value is '{0}'.", hopper), "hopper");
            }
        }

        private string WaferSizeToStream(WaferSize waferSize)
        {
            switch (waferSize)
            {
                case WaferSize.Size6Inches:
                    return "0006";
                case WaferSize.Size8Inches:
                    return "0008";
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type WaferSize, actual value is '{0}'.", waferSize), "waferSize");
            }
        }

        #endregion

        #region BaseToStreamConverter members

        protected override bool CheckParameter(LoadCassette parameter)
        {
            if ((parameter.WaferSize != WaferSize.Size6Inches) && (parameter.WaferSize != WaferSize.Size8Inches))
                throw ThrowPlcExceptionInvalidEnumValue("parameter.WaferSize", parameter.WaferSize);

            if ((parameter.Destination != DemountCassetteHopper.Hopper1) && (parameter.Destination != DemountCassetteHopper.Hopper2) &&
                (parameter.Destination != DemountCassetteHopper.Hopper3) && (parameter.Destination != DemountCassetteHopper.Hopper4))
                throw ThrowPlcExceptionInvalidEnumValue("parameter.Destination", parameter.Destination);

            return true;
        }

        protected override int GetLength(LoadCassette parameter)
        {
            return 2;
        }

        protected override string GetStream(LoadCassette parameter)
        {
            return WaferSizeToStream(parameter.WaferSize) + CassetteHopperToStream(parameter.Destination);
        }

        #endregion       
    }
}
