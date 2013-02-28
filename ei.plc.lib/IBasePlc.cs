using EI.Business;
namespace EI.Plc
{
    public interface IBasePlc
    {
        bool Open();
        void Close();

        PlcCommunicationStatus GetCommunicationStatus();
    }
}
