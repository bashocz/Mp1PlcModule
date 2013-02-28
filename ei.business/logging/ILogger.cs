using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EI.Business
{
    public interface ILogger
    {
        void Fatal(string componentName, string message, Exception exception);
        void Error(string componentName, string message, Exception exception);
        void Fatal(string componentName, string message);
        void Error(string componentName, string message);
        void Warn(string componentName, string message);
        void Info(string componentName, string message);
        void Debug(string componentName, string message);
    }
}
