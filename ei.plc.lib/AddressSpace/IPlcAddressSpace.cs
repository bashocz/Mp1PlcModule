namespace EI.Plc
{
    interface IPlcAddressSpace
    {
        bool CheckAddress(int address);
        bool CheckAddress(int address, int length);
    }
}
