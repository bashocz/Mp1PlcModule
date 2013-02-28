using System;
using System.Globalization;

namespace EI.Plc
{
    class PlcMemoryReadData
    {
        #region data members

        public int StationNo { get; private set; }
        public int PcNo { get; private set; }

        public string Data { get; private set; }

        public bool IsError { get; private set; }
        public int ErrorCode { get; private set; }

        #endregion

        #region constructors

        private PlcMemoryReadData() { }

        public static PlcMemoryReadData Create(string read)
        {
            PlcMemoryReadData data = new PlcMemoryReadData();
            data.ParseMessage(read);
            return data;
        }

        #endregion

        #region conversion methods

        private void ParseMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("message", "Argument wasn't initialized.");
            if (message.Length < 7)
                throw new FormatException("Length of argument 'message' is less than 7 characters.");

            PlcControlChar ctrl = PlcStream.StringToControlChar(message[0]);
            if (ctrl == PlcControlChar.STX)
            {
                if (PlcStream.StringToControlChar(message[message.Length - 3]) != PlcControlChar.ETX)
                    throw new FormatException("Argument 'message' isn't in correct format.");
                string expectedChecksum = PlcStream.CalculateCheckSum(message.Substring(1, message.Length - 3));
                string actualChecksum = message.Substring(message.Length - 2);
                if (string.Compare(expectedChecksum, actualChecksum, StringComparison.OrdinalIgnoreCase) != 0)
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Expected checksum <{0}>, actual checksum <{1}>.", expectedChecksum, actualChecksum), "message");

                ParseIds(message);
                ParseData(message);
            }
            else if (ctrl == PlcControlChar.NAK)
            {
                ParseIds(message);
                ParseErrorCode(message);
            }
            else
                throw new FormatException("Argument 'message' isn't in correct format.");
        }

        private void ParseIds(string message)
        {
            StationNo = PlcStream.StringToId(message.Substring(1, 2));
            PcNo = PlcStream.StringToId(message.Substring(3, 2));
        }

        private void ParseData(string message)
        {
            Data = message.Substring(5, message.Length - 8);
        }

        private void ParseErrorCode(string message)
        {
            IsError = true;
            ErrorCode = Convert.ToInt32(message.Substring(5, 2), 16);
        }

        #endregion
    }
}
