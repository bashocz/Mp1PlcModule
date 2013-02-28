using System;
using System.Globalization;

namespace EI.Plc
{
    class IntHexConverter : IStreamConverter<int>
    {
        #region IStreamConverter methods

        public int ToObject(string stream)
        {
            if (stream == null)
                throw new ArgumentNullException("number", string.Format(CultureInfo.InvariantCulture, "{0}: Parameter can't be null.", GetType().Name));
            return Convert.ToInt32(stream, 16);
        }

        public string ToStream(int number)
        {
            if ((number < 0) || (number > 65535))
                throw new ArgumentOutOfRangeException("number", string.Format(CultureInfo.InvariantCulture, "{0}: Parameter 'number' is expected in range <0, 65535>, actual value is {1}.", GetType().Name, number));
            return number.ToString("X4", CultureInfo.InvariantCulture);
        }

        #endregion
    }
}