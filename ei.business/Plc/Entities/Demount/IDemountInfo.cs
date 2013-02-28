namespace EI.Business
{
    public interface IDemountInfo
    {       
        int DemountedWaferCount { get; }
        bool Completed { get; }
    }
}
