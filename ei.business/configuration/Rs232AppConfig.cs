using System;
using System.Configuration;
using System.IO.Ports;

namespace EI.Business
{
    public class Rs232AppConfig : IRs232Config
    {
        #region IRs232Config members

        public string PortName
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["RS232_PORT_NAME"]); }
        }

        public int BaudRate
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["RS232_BAUD_RATE"]); }
        }

        public Parity Parity
        {
            get { return StringToParity(ConfigurationManager.AppSettings["RS232_PARITY"]); }
        }

        public int DataBits
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["RS232_DATA_BITS"]); }
        }

        public StopBits StopBits
        {
            get { return StringToStopBits(ConfigurationManager.AppSettings["RS232_STOP_BITS"]); }
        }

        public string NewLine
        {
            get { return ParseNewLine(ConfigurationManager.AppSettings["RS232_NEW_LINE"]); }
        }

        public int ReadTimeout
        {
            get { return 1000; }
        }

        public int WriteTimeout
        {
            get { return 1000; }
        }

        #endregion

        #region private methods

        private StopBits StringToStopBits(string value)
        {
            switch (value)
            {
                case "0": return StopBits.None;
                case "1": return StopBits.One;
                case "2": return StopBits.Two;
                case "3": return StopBits.OnePointFive;

                default: return StopBits.None;
            }
        }

        private Parity StringToParity(string value)
        {
            switch (value)
            {
                case "0": return Parity.None;
                case "1": return Parity.Odd;
                case "2": return Parity.Even;
                case "3": return Parity.Mark;
                case "4": return Parity.Space;

                default: return Parity.None;
            }
        }

        // '03' / '1013' ...
        private string ParseNewLine(string value)
        {
            if (value == null)
                return "";

            string newLine = "";

            string oneByte = "";
            for (int i = 0; i < value.Length; i++)
            {
                oneByte += value[i];

                if (oneByte.Length == 2)
                {
                    newLine += (Char)Convert.ToInt32(oneByte);
                    oneByte = "";
                }
            }

            return newLine;
        }

        #endregion
    }
}