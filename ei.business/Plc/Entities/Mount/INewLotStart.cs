namespace EI.Business
{
    public interface INewLotStart
    {
        string LotId { get; }
        MountLine Line { get; }
        bool IsLotStarted { get; }
    }
}
