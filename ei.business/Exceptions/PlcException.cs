using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EI.Business
{
    [Serializable]
    public class PlcException : Exception
    {
        #region ctors

        protected PlcException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public PlcException()
            : this(PlcErrorCode.Unknown, null, null) { }

        public PlcException(string message)
            : this(PlcErrorCode.Unknown, message, null) { }

        public PlcException(string message, Exception innerException)
            : this(PlcErrorCode.Unknown, message, innerException) { }

        public PlcException(PlcErrorCode errorCode, string message)
            : this(errorCode, message, null) { }

        public PlcException(PlcErrorCode errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        #endregion

        #region ISerializable members

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ErrorCode", ErrorCode);
        }

        #endregion

        #region PlcException members

        public PlcErrorCode ErrorCode { get; private set; }

        #endregion
    }
}
