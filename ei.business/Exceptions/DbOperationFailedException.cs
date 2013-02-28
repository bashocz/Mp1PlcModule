using System;
using System.Runtime.Serialization;

namespace EI.Business
{
    [Serializable]
    public class DbOperationFailedException : Exception
    {
        #region constructors

        protected DbOperationFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public DbOperationFailedException()
            : base() { }

        public DbOperationFailedException(string message)
            : base(message) { }

        public DbOperationFailedException(string message, Exception innerException)
            : base(message, innerException) { }

        #endregion
    }
}