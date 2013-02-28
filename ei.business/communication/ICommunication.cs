using System;

namespace EI.Business
{
    public interface ICommunication
    {
        bool Open();
        void Close();

        void Write(string data);
        string Read();

        void WriteBytes(byte[] data);
        byte[] ReadBytes();

        bool HasDataToRead();

        bool CancelOperation();

        bool Connected { get; }

        string GetConfigurationInfo();
    }
}
