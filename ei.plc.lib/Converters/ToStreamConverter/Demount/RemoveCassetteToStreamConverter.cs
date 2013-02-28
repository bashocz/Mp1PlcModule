using System;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    class RemoveCassetteToStreamConverter : BaseToStreamConverter<DemountCassetteStation>
    {
        #region constructors

        public RemoveCassetteToStreamConverter() { }

        #endregion

        #region conversion methods

        private string DemountCassetteStationToStream(DemountCassetteStation station)
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

        #endregion

        #region BaseToStreamConverter members

        protected override bool CheckParameter(DemountCassetteStation parameter)
        {
            if ((parameter != DemountCassetteStation.Station1) && (parameter != DemountCassetteStation.Station2) &&
                (parameter != DemountCassetteStation.Station3) && (parameter != DemountCassetteStation.Station4))
                throw ThrowPlcExceptionInvalidEnumValue("parameter", parameter);

            return true;
        }

        protected override int GetLength(DemountCassetteStation parameter)
        {
            return 1;
        }

        protected override string GetStream(DemountCassetteStation parameter)
        {
            return DemountCassetteStationToStream(parameter);
        }

        #endregion      
    }
}
