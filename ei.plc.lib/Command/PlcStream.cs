using System;
using System.Globalization;

namespace EI.Plc
{
    static class PlcStream
    {
        #region control character conversion methods

        public static string ControlCharToString(PlcControlChar controlChar)
        {
            return Convert.ToChar((int)controlChar).ToString();
        }

        public static PlcControlChar StringToControlChar(char controlChar)
        {
            switch (controlChar)
            {
                case '\u0002':
                    return PlcControlChar.STX;
                case '\u0003':
                    return PlcControlChar.ETX;
                case '\u0005':
                    return PlcControlChar.ENQ;
                case '\u0006':
                    return PlcControlChar.ACK;
                case '\u0015':
                    return PlcControlChar.NAK;
            }
            return PlcControlChar.Unknown;
        }

        #endregion

        #region party IDs conversion methods

        public static string IdsToString(int stationId, int pcId)
        {
            if (((stationId < 0) || (stationId > 0x1F)) && (stationId != 0xFF))
                throw new ArgumentOutOfRangeException("stationId", string.Format(CultureInfo.InvariantCulture, "Station ID is out of range <0x00, 0x1F> or 0xFF. Actual value is {0:X}", stationId));
            if (((pcId < 0) || (pcId > 0x40)) && (pcId != 0xFF))
                throw new ArgumentOutOfRangeException("pcId", string.Format(CultureInfo.InvariantCulture, "Pc ID is out of range <0x00, 0x40> or 0xFF. Actual value is {0:X}", pcId));

            return stationId.ToString("X2", CultureInfo.InvariantCulture) + pcId.ToString("X2", CultureInfo.InvariantCulture);
        }

        public static int StringToId(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id", "Argument wasn't initialized.");
            if (id.Length != 2)
                throw new FormatException("Argument 'id' is expected as string of 2 characters.");

            return Convert.ToInt32(id, 16);
        }

        #endregion

        #region wait time conversion methods

        public static string WaitMsToString(int milliseconds)
        {
            if ((milliseconds < 0) || (milliseconds > 150))
                throw new ArgumentOutOfRangeException("milliseconds", string.Format(CultureInfo.InvariantCulture, "Argument is out of range <0, 150>. Actual value is {0}.", milliseconds));
            return (milliseconds / 10).ToString("X1", CultureInfo.InvariantCulture);
        }

        #endregion

        #region address conversion methods

        public static string AddressToString(int address)
        {
            if ((address < 0) || (address > 0xfffff))
                throw new ArgumentOutOfRangeException("address", string.Format(CultureInfo.InvariantCulture, "Address is out of range <0x00000, 0xFFFFF>. Actual value is {0:X5}", address));

            return address.ToString("X5", CultureInfo.InvariantCulture);
        }

        #endregion

        #region checksum conversion methods

        public static string AddCheckSum(string message)
        {
            if (message == null)
                throw new ArgumentNullException("message", "Argument wasn't initialized.");

            return message + CalculateCheckSum(message);
        }

        public static string CalculateCheckSum(string message)
        {
            int checkSumDec = 0;
            foreach (int character in message)
            {
                checkSumDec += character;
            }
            string checkSumHex = checkSumDec.ToString("X2", CultureInfo.InvariantCulture);
            return checkSumHex.Substring(checkSumHex.Length - 2, 2);
        }

        #endregion
    }
}