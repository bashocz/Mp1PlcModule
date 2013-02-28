
using EI.Business;
namespace EI.Plc
{
    class MountStatusFromStreamConverter : BaseFromStreamConverter<MountStatus>
    {
        #region conversion methods

        private MountLine ParseMountLine(string stream)
        {
            switch (stream)
            {
                case "0000":
                    return MountLine.Cleared;
                case "0001":
                    return MountLine.Right;
                case "0002":
                    return MountLine.Left;
                case "0003":
                    return MountLine.Both;
                default:
                    throw GetPlcExceptionInvalidStreamForEnumValue("MountLine", "0000, 0003", stream);
            }
        }

        private MountState ParseMountState(string stream)
        {
            switch (stream)
            {
                case "0001":
                    return MountState.AutoMount;
                case "0002":
                    return MountState.Stop;
                case "0003":
                    return MountState.AutoMountAlarm;
                case "0004":
                    return MountState.StopAlarm;
                default:
                    throw GetPlcExceptionInvalidStreamForEnumValue("MountState", "0001, 0004", stream);
            }
        }

        #endregion

        #region BaseFromStreamConverter members

        protected override bool CheckParameter(string stream)
        {
            if (stream.Length < 132)
                throw GetPlcExceptionInvalidLength(132, stream.Length);
            return true;
        }

        protected override MountStatus GetObject(string stream)
        {
            return new MountStatus
            {
                NewLotCassette = new NewLotCassette
                {
                    CassetteId = ParseString(stream.Substring(0, 16), "CassetteId").Trim(),
                    IsCassetteId = ParseBool(stream.Substring(16, 4), "IsCassetteId")
                },
                IsLotDataTimeout = ParseBool(stream.Substring(24, 4), "IsLotDataTimeout"),
                NewLotStarted = new NewLotStart
                {
                    LotId = ParseString(stream.Substring(28, 28), "LotId"),
                    Line = ParseMountLine(stream.Substring(56, 4)),
                    IsLotStarted = ParseBool(stream.Substring(60, 4), "IsLotStarted")
                },
                IsCarrierPlateArrived = ParseBool(stream.Substring(64, 4), "IsCarrierPlateArrived"),
                IsCarrierPlateMountingReady = ParseBool(stream.Substring(72, 4), "IsCarrierPlateMountingReady"),
                WaferBreakNumber = ParseInt(stream.Substring(76, 4), "WaferBreakNumber"),
                IsMountingErrorCarrierPlate = ParseBool(stream.Substring(84, 4), "IsMountingErrorCarrierPlate"),
                IsEndLot = ParseBool(stream.Substring(88, 4), "IsEndLot"),
                IsReservationLotCanceled = ParseBool(stream.Substring(92, 4), "IsReservationLotCanceled"),
                State = ParseMountState(stream.Substring(128, 4))
            };
        }

        #endregion
    }
}
