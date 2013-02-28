using System.IO.Ports;

namespace EI.Business
{
    public class Rs232Config : IRs232Config
    {
        public string PortName { get; set; }

        private int _baudRate = 9600;
        public int BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        private Parity _parity = Parity.None;
        public Parity Parity
        {
            get { return _parity; }
            set { _parity = value; }
        }

        private int _dataBits = 8;
        public int DataBits
        {
            get { return _dataBits; }
            set { _dataBits = value; }
        }

        private StopBits _stopBits = StopBits.One;
        public StopBits StopBits
        {
            get { return _stopBits; }
            set { _stopBits = value; }
        }

        public string NewLine { get; set; }

        private int _readTimeout = 1000;
        public int ReadTimeout
        {
            get { return _readTimeout; }
            set { _readTimeout = value; }
        }

        private int _writeTimeout = 1000;
        public int WriteTimeout
        {
            get { return _writeTimeout; }
            set { _writeTimeout = value; }
        }

        public override string ToString()
        {
            return string.Format("{0},BR:{1},PA:{2},DB:{3},SB:{4},RT:{5},WT:{6}", 
                PortName, 
                BaudRate, 
                Parity, 
                DataBits, 
                StopBits, 
                ReadTimeout, 
                WriteTimeout);
        }
    }
}
