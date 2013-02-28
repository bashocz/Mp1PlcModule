namespace EI.Business
{
    public interface IPolisherHead
    {
        int Force { get; }
        double Pressure { get; }
        double Backpressure { get; }
        int Rpm { get; }
        double LoadCurrent { get; }
    }
}
