using System;
using EI.Business;

namespace EI.Communication
{
    public class SerialCommunicationBuffered : SerialCommunication, ICommunicationBuffered
    {
        string _buffer;

        public SerialCommunicationBuffered(IRs232Config config)
            : base(config)
        {
        }

        public new string Read()
        {
            string line;

            if (string.IsNullOrEmpty(_buffer))
                line=base.Read();
            else {
                line=_buffer;
                _buffer=null;
            }

            return line;
        }

        public string Read(bool silent)
        {
            if (!silent)
                return Read();
            
            if (string.IsNullOrEmpty(_buffer))
                _buffer = base.Read();

            return _buffer;
        }
    }
}
