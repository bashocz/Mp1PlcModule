using System;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    class StartDemountingToStreamConverter : BaseToStreamConverter<StartDemounting>
    {
        #region constants

        private const int sixInchwafersCountLL = 3;
        private const int sixInchwafersCountUL = 8;
        private const int eightInchwafersCountLL = 3;
        private const int eightInchwafersCountUL = 5;

        #endregion

        #region constructors

        public StartDemountingToStreamConverter() { }

        #endregion

        #region conversion methods

        private string CassetteStationToStream(DemountCassetteStation station)
        {
            switch (station)
            {
                case DemountCassetteStation.Station1:
                    return "0001";
                case DemountCassetteStation.Station2:
                    return "0002";
                case DemountCassetteStation.Station3:
                    return "0003";
                case DemountCassetteStation.Station4:
                    return "0004";
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type DemountCassetteStation, actual value is '{0}'.", station), "station");
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

        protected override bool CheckParameter(StartDemounting parameter)
        {
            if ((parameter.Size != WaferSize.Size6Inches) && (parameter.Size != WaferSize.Size8Inches))
                throw ThrowPlcExceptionInvalidEnumValue("parameter.Size", parameter.Size);

            if ((parameter.Size == WaferSize.Size6Inches) && ((parameter.Count < 3) || (parameter.Count > 8)))
                throw ThrowPlcExceptionOutOfRangeValue("parameter.Count", sixInchwafersCountLL, sixInchwafersCountUL, parameter.Count);

            if ((parameter.Size == WaferSize.Size8Inches) && ((parameter.Count < 3) || (parameter.Count > 5)))
                throw ThrowPlcExceptionOutOfRangeValue("parameter.Count", eightInchwafersCountLL, eightInchwafersCountUL, parameter.Count);

            if ((parameter.Station != DemountCassetteStation.Station1) && (parameter.Station != DemountCassetteStation.Station2) &&
                (parameter.Station != DemountCassetteStation.Station3) && (parameter.Station != DemountCassetteStation.Station4))
                throw ThrowPlcExceptionInvalidEnumValue("parameter.Station", parameter.Station);

            return true;
        }

        protected override int GetLength(StartDemounting parameter)
        {
            return 3;
        }

        protected override string GetStream(StartDemounting parameter)
        {
            return WaferSizeToStream(parameter.Size)
                + IntHexToStream(parameter.Count, "WaferCount")
                + CassetteStationToStream(parameter.Station);
        }

        #endregion
    }
}
