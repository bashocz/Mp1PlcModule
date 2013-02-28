namespace EI.Business
{
    public interface IMagazineChangeRequest
    {
        bool IsMagazineFull { get; }
        bool IsOperatorChangeRequest { get; }
    }
}
