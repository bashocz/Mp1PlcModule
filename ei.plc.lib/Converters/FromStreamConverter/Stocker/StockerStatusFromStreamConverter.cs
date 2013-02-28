using EI.Business;
namespace EI.Plc
{
    class StockerStatusFromStreamConverter : BaseFromStreamConverter<StockerStatus>
    {
        #region conversion methods

        private WaferSize ParseWaferSize(string stream)
        {
            int size = ParseHexInt(stream, "WaferSize");
            switch (size)
            {
                case 0:
                    return WaferSize.AnySize;
                case 6:
                    return WaferSize.Size6Inches;
                case 8:
                    return WaferSize.Size8Inches;
                default:
                    throw GetPlcExceptionInvalidStreamForEnumValue("WaferSize", "0006 and 0008", stream);
            }
        }

        private CarrierPlateRouting ParseCarrierPlateRouting(string stream)
        {
            int routing = ParseHexInt(stream, "CarrierPlateRouting");
            switch (routing)
            {
                case 0:
                    return CarrierPlateRouting.Cleared;
                case 1:
                    return CarrierPlateRouting.InsertIntoMagazine;
                case 2:
                    return CarrierPlateRouting.Reject;              
                default:
                    throw GetPlcExceptionInvalidStreamForEnumValue("CarrierPlateRouting", "0000 or 0001 or 0002", stream);
            }
        }

        private StockerInventory ParseStockerInventory(string stream)
        {
            int inventory = ParseHexInt(stream, "StockerInventory");
            switch (inventory)
            {
                case 0:
                    return StockerInventory.Cleared;
                case 1:
                    return StockerInventory.SizeAvailable;
                case 2:
                    return StockerInventory.SizeNotInStocker;
                default:
                    throw GetPlcExceptionInvalidStreamForEnumValue("CarrierPlateRouting", "0000 or 0001 or 0002", stream);
            }
        }

        private MagazineSelection ParseMagazineSelection(string stream)
        {
            int selection = ParseHexInt(stream, "MagazineSelection");
            switch (selection)
            {
                case 0:
                    return MagazineSelection.Cleared;
                case 1:
                    return MagazineSelection.HasRequestedSize;
                case 2:
                    return MagazineSelection.DoesNotHaveRequestedSize;
                default:
                    throw GetPlcExceptionInvalidStreamForEnumValue("CarrierPlateRouting", "0000 or 0001 or 0002", stream);
            }
        }

        #endregion

        #region BaseFromStreamConverter members

        protected override bool CheckParameter(string stream)
        {
            if (stream.Length < 88)
                throw GetPlcExceptionInvalidLength(88, stream.Length);
            return true;
        }

        protected override StockerStatus GetObject(string stream)
        {
            return new StockerStatus
            {
                IsCarrierPlateArrived = ParseBool(stream.Substring(0, 4), "IsCarrierPlateArrived"),
                CarrierPlateRouting = ParseCarrierPlateRouting(stream.Substring(8, 4)),
                MagazineChangeRequest = new MagazineChangeRequest
                {
                    IsMagazineFull = ParseBool(stream.Substring(12, 4), "IsMagazineFull"),
                    IsOperatorChangeRequest = ParseBool(stream.Substring(16, 4), "IsOperatorChangeRequest")
                },
                IsMagazineChangeStarted = ParseBool(stream.Substring(24, 4), "IsMagazineChangeStarted"),
                IsInputMagazineBarcodeOk = ParseBool(stream.Substring(28, 4), "IsInputMagazineBarcodeOk"),
                MagazineRequest = new MagazineRequest
                {
                    IsRequested = ParseBool(stream.Substring(68, 4), "IsRequested"),
                    WaferSize = ParseWaferSize(stream.Substring(64, 4)),
                    PolishLineNumber = ParseHexInt(stream.Substring(84, 4), "PolishLineNumber")
                },
                StockerInventory = ParseStockerInventory(stream.Substring(72, 4)),
                MagazineSelection = ParseMagazineSelection(stream.Substring(80, 4)),
                IsMagazineArrived = ParseBool(stream.Substring(76, 4), "IsMagazineArrived")
            };
        }

        #endregion
    }
}