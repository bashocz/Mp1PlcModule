using EI.Business;
namespace EI.Plc
{
    class PolishingStatusFromStreamConverter : BaseFromStreamConverter<PolishingShortStatus>
    {
        #region conversion methods

        private PolisherState ParsePolisherState(string stream)
        {
            int status = ParseHexInt(stream, "PolisherState");
            switch (status)
            {
                case 1:
                    return PolisherState.AutoProcess;
                case 2:
                    return PolisherState.AutoWait;
                case 3:
                    return PolisherState.AutoLoad;
                case 4:
                    return PolisherState.AutoUnload;
                case 5:
                    return PolisherState.Pause;
                case 6:
                    return PolisherState.Alarm;
                case 7:
                    return PolisherState.EmergencyStop;
                default:
                    throw GetPlcExceptionInvalidStreamForEnumValue("PolisherState", "0001, 0007", stream);
            }
        }

        #endregion

        #region BaseFromStreamConverter members

        protected override bool CheckParameter(string stream)
        {
            if (stream.Length < 24)
                throw GetPlcExceptionInvalidLength(24, stream.Length);
            return true;
        }

        protected override PolishingShortStatus GetObject(string stream)
        {
            PolishingShortStatus result = new PolishingShortStatus();
            result.NewStatus(new PolisherShortStatus[]{
                        new PolisherShortStatus{
                            State = ParsePolisherState(stream.Substring(12 + 0 * 4, 4)),
                            HighPressure = ParseBool(stream.Substring(0 + 0 * 4, 4), "HighPressure"),
                        },
                        new PolisherShortStatus{
                            State = ParsePolisherState(stream.Substring(12 + 1 * 4, 4)),
                            HighPressure = ParseBool(stream.Substring(0 + 1 * 4, 4), "HighPressure"),
                        },
                        new PolisherShortStatus{
                            State = ParsePolisherState(stream.Substring(12 + 2 * 4, 4)),
                            HighPressure = ParseBool(stream.Substring(0 + 2 * 4, 4), "HighPressure"),
                        }
                    });
            return result;
        }

        #endregion
    }
}
