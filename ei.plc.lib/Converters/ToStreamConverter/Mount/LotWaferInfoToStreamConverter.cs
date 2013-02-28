using System;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    class LotWaferInfoToStreamConverter : BaseToStreamConverter<ILotData>
    {
        #region constants

        private const int wafersCountLL = 3;
        private const int wafersCountUL = 300;
        private const int ngWafersCountLL = 0;
        private const int ngWafersCountUL = 297;
        private const int assemblyCarrierPlateCountLL = 0;
        private const int assemblyCarrierPlateCountUL = 99;
        private const int assemblyWaferCountLL = 3;
        private const int assemblyWaferCountUL = 8;

        #endregion

        #region constructors

        public LotWaferInfoToStreamConverter() { }

        #endregion

        #region conversion methods

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

        private string OfTypeToStream(OfType ofType)
        {
            switch (ofType)
            {
                case OfType.OF:
                    return "0001";
                case OfType.VNotch:
                    return "0002";
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type OfType, actual value is '{0}'.", ofType), "ofType");
            }
        }

        private string PolishDivisionToStream(PolishDivision division)
        {
            switch (division)
            {
                case PolishDivision.New:
                    return "0001";
                case PolishDivision.Repolish:
                    return "0002";
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type PolishDivision, actual value is '{0}'.", division), "division");
            }
        }

        #endregion

        #region BaseToStreamConverter members

        protected override bool CheckParameter(ILotData lotData)
        {
            if ((lotData.Wafers.Count < wafersCountLL) || (lotData.Wafers.Count > wafersCountUL))
                throw ThrowPlcExceptionOutOfRangeValue("lotData.Wafers.Count", wafersCountLL, wafersCountUL, lotData.Wafers.Count);

            if ((lotData.NGWaferCount < ngWafersCountLL) || (lotData.NGWaferCount > ngWafersCountUL))
                throw ThrowPlcExceptionOutOfRangeValue("lotData.NGWaferCount", ngWafersCountLL, ngWafersCountUL, lotData.NGWaferCount);

            if ((lotData.WaferSize != WaferSize.Size6Inches) && (lotData.WaferSize != WaferSize.Size8Inches))
                throw ThrowPlcExceptionInvalidEnumValue("lotData.WaferSize", lotData.WaferSize);

            if ((lotData.OfType != OfType.OF) && (lotData.OfType != OfType.VNotch))
                throw ThrowPlcExceptionInvalidEnumValue("lotData.OfType", lotData.OfType);

            if ((lotData.PolishDivision != PolishDivision.New) && (lotData.PolishDivision != PolishDivision.Repolish))
                throw ThrowPlcExceptionInvalidEnumValue("lotData.PolishDivision", lotData.PolishDivision);

            if ((lotData.Assembly1.CarrierPlateCount < assemblyCarrierPlateCountLL) || (lotData.Assembly1.CarrierPlateCount > assemblyCarrierPlateCountUL))
                throw ThrowPlcExceptionOutOfRangeValue("lotData.Assembly1.CarrierPlateCount", assemblyCarrierPlateCountLL, assemblyCarrierPlateCountUL, lotData.Assembly1.CarrierPlateCount);

            if ((lotData.Assembly1.WaferCount < assemblyWaferCountLL) || (lotData.Assembly1.WaferCount > assemblyWaferCountUL))
                throw ThrowPlcExceptionOutOfRangeValue("lotData.Assembly1.WaferCount", assemblyWaferCountLL, assemblyWaferCountUL, lotData.Assembly1.WaferCount);

            if ((lotData.Assembly2.CarrierPlateCount < assemblyCarrierPlateCountLL) || (lotData.Assembly2.CarrierPlateCount > assemblyCarrierPlateCountUL))
                throw ThrowPlcExceptionOutOfRangeValue("lotData.Assembly2.CarrierPlateCount", assemblyCarrierPlateCountLL, assemblyCarrierPlateCountUL, lotData.Assembly2.CarrierPlateCount);

            if ((lotData.Assembly2.WaferCount < assemblyWaferCountLL) || (lotData.Assembly2.WaferCount > assemblyWaferCountUL))
                throw ThrowPlcExceptionOutOfRangeValue("lotData.Assembly2.WaferCount", assemblyWaferCountLL, assemblyWaferCountUL, lotData.Assembly2.WaferCount);
            return true;
        }

        protected override int GetLength(ILotData lotData)
        {
            return 9;
        }

        protected override string GetStream(ILotData lotData)
        {
            return IntToStream(lotData.Wafers.Count, "WaferCount")
                 + IntToStream(lotData.NGWaferCount, "NGWaferCount")
                 + WaferSizeToStream(lotData.WaferSize)
                 + OfTypeToStream(lotData.OfType)
                 + PolishDivisionToStream(lotData.PolishDivision)
                 + IntToStream(lotData.Assembly1.CarrierPlateCount, "A1CpCount")
                 + IntToStream(lotData.Assembly1.WaferCount, "A1WaferCount")
                 + IntToStream(lotData.Assembly2.CarrierPlateCount, "A2CpCount")
                 + IntToStream(lotData.Assembly2.WaferCount, "A2WaferCount");
        }

        #endregion
    }
}