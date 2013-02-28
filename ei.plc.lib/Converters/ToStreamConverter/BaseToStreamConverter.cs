using System;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    abstract class BaseToStreamConverter<TParam> : IToStreamConverter<TParam>
    {
        #region throw exception methods

        protected PlcException ThrowPlcExceptionInvalidEnumValue(string parameterName, Enum value)
        {
            return new PlcException(PlcErrorCode.WriteParsing, string.Format(CultureInfo.InvariantCulture, "{0}: Parameter '{1}' is not valid, actual value is <{2}.{3}>.", GetType().Name, parameterName, value.GetType().Name, value));
        }

        protected PlcException ThrowPlcExceptionOutOfRangeValue(string parameterName, int from, int to, int value)
        {
            return new PlcException(PlcErrorCode.WriteParsing, string.Format(CultureInfo.InvariantCulture, "{0}: Parameter '{1}' is expected in range <{2},{3}>, actual value is {4}.", GetType().Name, parameterName, from, to, value));
        }

        #endregion

        #region conversion methods

        protected string TextToStream(string value, string propertyName)
        {
            return Parse<StringConverter, string>(value, propertyName);
        }

        protected string IntHexToStream(int value, string propertyName)
        {
            return Parse<IntHexConverter, int>(value, propertyName);
        }

        protected string IntToStream(int value, string propertyName)
        {
            return Parse<IntConverter, int>(value, propertyName);
        }

        protected string BoolToStream(bool value, string propertyName)
        {
            return Parse<BoolConverter, bool>(value, propertyName);
        }

        private string Parse<U, V>(V value, string propertyName)
            where U : IStreamConverter<V>, new()
        {
            try
            {
                U converter = new U();
                return converter.ToStream(value);
            }
            catch (Exception ex)
            {
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: Parsing '{1}' exception: {2}", GetType().Name, propertyName, ex.Message), ex);
            }
        }

        #endregion

        #region BaseToStream members

        protected abstract bool CheckParameter(TParam parameter);

        protected abstract int GetLength(TParam parameter);
        protected abstract string GetStream(TParam parameter);

        #endregion

        #region IToStreamConverter members

        public PlcWriteStream TryConvert(TParam parameter)
        {
            if (parameter == null)
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: Argument can't be null.", GetType().Name));
            if (!CheckParameter(parameter))
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: Unhandled argument checking exception.", GetType().Name));

            try
            {
                return new PlcWriteStream
                {
                    Length = GetLength(parameter),
                    Stream = GetStream(parameter)
                };
            }
            catch (Exception ex)
            {
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: Unhandled parsing exception.", GetType().Name), ex);
            }
        }

        #endregion
    }
}