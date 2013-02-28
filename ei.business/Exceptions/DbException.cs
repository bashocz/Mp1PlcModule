using System;
using System.Runtime.Serialization;

namespace EI.Business
{
    [Serializable]
    public class DbException : Exception
    {
        #region constructors

        protected DbException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public DbException()
            : base() { }

        public DbException(string message)
            : base(message) { }

        public DbException(string message, Exception innerException)
            : base(message, innerException) { }

        #endregion
    }
}