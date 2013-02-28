namespace EI.Business
{
    public interface IMagazineRequest
    {
        WaferSize WaferSize { get; }
        bool IsRequested { get; }
        int PolishLineNumber { get; }
    }
}
