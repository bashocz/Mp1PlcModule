namespace EI.Business
{
    public enum PolisherState
    {
        None = 0,
        AutoProcess,
        AutoWait,
        AutoLoad,
        AutoUnload,
        Pause,
        Alarm,
        EmergencyStop
    }
}
