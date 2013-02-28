
namespace EI.Plc
{
    interface IToStreamConverter<TParam>
    {
        PlcWriteStream TryConvert(TParam parameter);
    }
}