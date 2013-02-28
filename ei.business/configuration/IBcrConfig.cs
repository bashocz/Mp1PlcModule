namespace EI.Business
{
    public interface IBcrConfig
    {
        string BarcodeReaderName { get; }
        int WaitTimeForReadingBc { get; }
        int LaserOnTime { get; }

        IRs232Config Rs232 { get; }
    }
}
