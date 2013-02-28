using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EI.Business
{
    public enum PlcErrorCode
    {
        Unknown = 0,
        Write,
        WriteParsing,
        Read,
        ReadParsing,
    }
}
