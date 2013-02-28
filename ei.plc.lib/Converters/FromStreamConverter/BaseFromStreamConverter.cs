using System;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    abstract class BaseFromStreamConverter<TResult> : IFromStreamConverter<TResult>
    {
        #region throw exception methods

        protected PlcException GetPlcExceptionInvalidStreamForEnumValue(string propertyName, string range, string value)
        {
            return new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: Property '{1}' is expected as string in range <{2}>, actual value is {3}", GetType().Name, propertyName, range, value));
        }

        protected PlcException GetPlcExceptionInvalidLength(int min, int length)
        {
            return new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "Argument 'stream' musts be at least {0} characters, actual <{1}>.", min, length));
        }

        #endregion

        #region conversion methods

        protected string ParseString(string stream, string propertyName)
        {
            return Parse<StringConverter, string>(stream, propertyName);
        }

        protected int ParseHexInt(string stream, string propertyName)
        {
            return Parse<IntHexConverter, int>(stream, propertyName);
        }

        protected int ParseInt(string stream, string propertyName)
        {
            return Parse<IntConverter, int>(stream, propertyName);
        }

        protected bool ParseBool(string stream, string propertyName)
        {
            return Parse<BoolConverter, bool>(stream, propertyName);
        }

        private V Parse<U, V>(string stream, string propertyName)
            where U : IStreamConverter<V>, new()
        {
            try
            {
                U converter = new U();
                return converter.ToObject(stream);
            }
            catch (Exception ex)
            {
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: Parsing '{1}' exception: {2}", GetType().Name, propertyName, ex.Message), ex);
            }
        }

        #endregion

        #region BaseFromStreamConverter members

        protected abstract bool CheckParameter(string stream);

        protected abstract TResult GetObject(string stream);

        #endregion

        #region IFromStreamConverter members

        public TResult TryConvert(string stream)
        {
            if (string.IsNullOrEmpty(stream))
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: Argument can't be null or empty.", GetType().Name));
            if (!CheckParameter(stream))
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: Unhandled argument checking exception.", GetType().Name));

            TResult result = default(TResult);
            try
            {
                result = GetObject(stream);
            }
            catch (Exception ex)
            {
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: Parsing exception.", GetType().Name), ex);
            }
            return result;
        }

        #endregion
    }
}