using System;
using System.Runtime.Serialization;

namespace EI.Business
{
    [Serializable]
    public class WcfException : Exception
    {
        #region constructors

        protected WcfException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public WcfException()
            : base() { }

        public WcfException(string message)
            : base(message) { }

        public WcfException(string message, Exception innerException)
            : base(message, innerException) { }

        #endregion
    }
}
