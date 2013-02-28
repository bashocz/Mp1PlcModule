using System;

namespace EI.Business
{
    public interface IRs232Communication : ICommunication
    {
        bool RtsEnable { get; set; }
    }
}
