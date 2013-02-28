using MP1.Foundation;

namespace MP1.Communication
{
    interface IPlcMemoryDataWriter
    {
        int Length { get; }
        string ToStream();  
    }
}
