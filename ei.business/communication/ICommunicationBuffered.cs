using System;

namespace EI.Business
{
    public interface ICommunicationBuffered : IRs232Communication
    {
        string Read(bool silent);
    }
}
