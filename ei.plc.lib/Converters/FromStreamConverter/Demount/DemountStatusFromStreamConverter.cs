using System.Collections.Generic;
using EI.Business;

namespace EI.Plc
{
    class DemountStatusFromStreamConverter : BaseFromStreamConverter<DemountStatus>
    {
        #region conversion methods

        private DemountCassetteHopper ParseCassetteHopper(string stream)
        {
            switch (stream)
            {
                case "0000":
                    return DemountCassetteHopper.Cleared;
                case "0001":
                    return DemountCassetteHopper.Hopper1;
                case "0002":
                    return DemountCassetteHopper.Hopper2;
                case "0003":
                    return DemountCassetteHopper.Hopper3;
                case "0004":
                    return DemountCassetteHopper.Hopper4;
                default:
                    throw GetPlcExceptionInvalidStreamForEnumValue("CassetteHopper", "0000, 0004", stream);
            }
        }

        private List<bool> ParseAreCassettes(string stream)
        {
            List<bool> flags = new List<bool>();

            for (int idx = 0; idx < 4; idx++)
                flags.Add(ParseBool(stream.Substring(idx * 4, 4), "AreCassettes"));
            return flags;
        }

        private DemountState ParseDemountState(string stream)
        {
            switch (stream)
            {
                case "0001":
                    return DemountState.AutoDemount;
                case "0002":
                    return DemountState.Standby;
                case "0003":
                    return DemountState.Stop;
                case "0004":
                    return DemountState.Alarm;
                default:
                    throw GetPlcExceptionInvalidStreamForEnumValue("DemountState", "0001, 0004", stream);
            }
        }

        #endregion

        #region BaseFromStreamConverter members

        protected override bool CheckParameter(string stream)
        {
            if (stream.Length < 152)
                throw GetPlcExceptionInvalidLength(152, stream.Length);
            return true;
        }

        protected override DemountStatus GetObject(string stream)
        {
            DemountStatus status = new DemountStatus
            {
                IsCarrierPlateArrived = ParseBool(stream.Substring(0, 4), "IsCarrierPlateArrived"),
                IsCarrierPlateDemountStarted = ParseBool(stream.Substring(12, 4), "IsCarrierPlateDemountStarted"),
                DemountInfo = new DemountInfo
                {
                    DemountedWaferCount = ParseHexInt(stream.Substring(28, 4), "DemountedWaferCount"),
                    Completed = ParseBool(stream.Substring(32, 4), "Completed")
                },
                CanReadCassetteBarcode = ParseCassetteHopper(stream.Substring(76, 4)),                
                State = ParseDemountState(stream.Substring(148, 4))
            };
            status.NewAreCassettes(ParseAreCassettes(stream.Substring(132, 16)));
            return status;
        }

        #endregion
    }
}
