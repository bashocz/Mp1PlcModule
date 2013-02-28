
namespace EI.Plc
{
    interface IFromStreamConverter<TResult>
    {
        TResult TryConvert(string stream);
    }
}