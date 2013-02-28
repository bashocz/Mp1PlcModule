using System;
using System.IO.Ports;
using System.Threading;
using EI.Business;

namespace EI.Communication
{
    public class SerialCommunicationPlc : IRs232Communication
    {
        #region constructors

        public SerialCommunicationPlc(IRs232Config config)
        {
            _config = config;

            if (_config == null)
                throw new ArgumentNullException("config", "Configuration wasn't initialized.");
        }

        #endregion

        #region SerialPort

        private readonly IRs232Config _config;

        private SerialPort GetSerialPort()
        {
            SerialPort port = new SerialPort(_config.PortName, _config.BaudRate, _config.Parity, _config.DataBits, _config.StopBits);
            port.ReadTimeout = _config.ReadTimeout;
            port.WriteTimeout = _config.WriteTimeout;

            port.DtrEnable = true;
            port.RtsEnable = true;
            port.Handshake = Handshake.None;

            return port;
        }

        private SerialPort _serial;
        private SerialPort Serial
        {
            get { return _serial ?? (_serial = GetSerialPort()); }
        }

        #endregion

        #region IDisposable members

        public void Dispose()
        {
            if (Connected)
                Close();
        }

        #endregion

        #region ICommunication members

        public bool Open()
        {
            if (!Connected)
                Serial.Open();

            return Serial.IsOpen;
        }

        public void Close()
        {
            if (Connected)
                Serial.Close();
        }

        public void Write(string data)
        {
            if (Connected)
                Serial.Write(data);
        }

        public void WriteBytes(byte[] data)
        {
            if (Connected)
                Serial.Write(data, 0, data.Length);
        }

        public byte[] ReadBytes()
        {
            throw new NotImplementedException();
        }

        public string Read()
        {
            string line = "";
            if (Connected)
            {
                // wait maximum 'readtimeout' ms for message
                int count = _config.ReadTimeout / 50; // how many 50ms?
                int idx=0;
                while ((!HasDataToRead()) || (idx < count))
                {
                    Thread.Sleep(50);
                    idx++;
                }

                // read message
                while (HasDataToRead())
                {
                    line += Serial.ReadExisting();
                    Thread.Sleep(200);
                }
            }
            return line;
        }

        public bool HasDataToRead()
        {
            return Serial.BytesToRead > 0;
        }

        public bool CancelOperation()
        {
            throw new NotImplementedException();
        }

        public bool Connected
        {
            get { return Serial.IsOpen; }
        }

        public bool RtsEnable
        {
            get { return Serial.RtsEnable; }
            set { Serial.RtsEnable = value; }
        }

        public string GetConfigurationInfo()
        {
            return string.Format("{0},BaudRate:{1},Parity:{2},StopBits:{3},DataBits:{4}", _config.PortName, _config.BaudRate, _config.Parity, _config.StopBits, _config.DataBits);
        }

        #endregion
    }
}
