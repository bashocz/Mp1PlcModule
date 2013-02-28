namespace EI.Plc
{
    interface IStreamConverter<T>
    {
        T ToObject(string stream);
        string ToStream(T obj);
    }
}
