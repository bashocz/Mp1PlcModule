using System;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    class LotDataTransmissionToStreamConverter : BaseToStreamConverter<LotDataTransmission>
    {
        #region constructors

        public LotDataTransmissionToStreamConverter() { }

        #endregion

        #region conversion methods

        private string LotDataTransmissionToStream(LotDataTransmission transmission)
        {
            switch (transmission)
            {
                case LotDataTransmission.Cleared:
                    return "0000";
                case LotDataTransmission.BeforeWritingCassetteInfo:
                    return "0001";
                case LotDataTransmission.BeforeWritingWaferInfo:
                    return "0002";
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type LotDataTransmission, actual value is '{0}'.", transmission), "transmission");
            }
        }

        #endregion

        #region BaseToStreamConverter members

        protected override bool CheckParameter(LotDataTransmission transmission)
        {
            if ((transmission != LotDataTransmission.Cleared) &&
                (transmission != LotDataTransmission.BeforeWritingCassetteInfo) &&
                (transmission != LotDataTransmission.BeforeWritingWaferInfo))
                throw ThrowPlcExceptionInvalidEnumValue("transmission", transmission);
            return true;
        }

        protected override int GetLength(LotDataTransmission transmission)
        {
            return 1;
        }

        protected override string GetStream(LotDataTransmission transmission)
        {
            return LotDataTransmissionToStream(transmission);
        }

        #endregion
    }
}
