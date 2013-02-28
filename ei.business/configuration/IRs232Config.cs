using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace EI.Business
{
    public interface IRs232Config
    {
        string PortName { get; }
        int BaudRate { get; }
        Parity Parity { get; }
        int DataBits { get; }
        StopBits StopBits { get; }
        string NewLine { get; }

        int ReadTimeout { get; }
        int WriteTimeout { get; }
    }
}
