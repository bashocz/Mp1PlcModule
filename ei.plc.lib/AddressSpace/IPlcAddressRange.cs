namespace EI.Plc
{
    interface IPlcAddressRange
    {
        bool CheckAddress(int address);
        bool CheckAddress(int address, int length);
    }
}
