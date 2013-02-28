using System;
using System.Globalization;

namespace EI.Plc
{
    class BoolConverter : IStreamConverter<bool>
    {
        #region IStreamConverter methods

        public bool ToObject(string stream)
        {
            switch (stream)
            {
                case "0000":
                    return false;
                case "0001":
                    return true;
                default:
                    throw new FormatException(string.Format(CultureInfo.InvariantCulture, "Argument 'flag' isn't in correct format. Expected strings are 0000 or 0001. Actual string is {0}", stream));
            }
        }

        public string ToStream(bool flag)
        {
            return (flag == false) ? "0000" : "0001";
        }

        #endregion
    }
}