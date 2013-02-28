using System;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using EI.Business;

namespace EI.Communication
{
    public class SerialCommunication : IRs232Communication, IDisposable
    {
        #region constructors

        public SerialCommunication(IRs232Config config)
        {
            _config = config;

            if (_config == null)
                throw new ArgumentNullException("config", "Configuration wasn't initialized.");
        }

        #endregion

        #region IDisposable members

        public void Dispose()
        {
            if (Connected)
                Close();
        }

        #endregion

        #region SerialPort

        private readonly IRs232Config _config;

        private SerialPort GetSerialPort()
        {
            SerialPort port = new SerialPort(_config.PortName, _config.BaudRate, _config.Parity, _config.DataBits, _config.StopBits);
            port.ReadTimeout = _config.ReadTimeout;
            port.WriteTimeout = _config.WriteTimeout;
            if (!string.IsNullOrEmpty(_config.NewLine))
                port.NewLine = _config.NewLine;
            return port;
        }

        private SerialPort _serial;
        private SerialPort Serial
        {
            get { return _serial ?? (_serial = GetSerialPort()); }
        }

        #endregion

        #region private members

        private string GetMessageLine(string data)
        {
            if (string.IsNullOrEmpty(_config.NewLine))
                return data;
            return data + _config.NewLine;
        }

        private string ParseMessageLine(string line)
        {
            int pos;
            
            if (!string.IsNullOrEmpty(_config.NewLine) && ((pos = line.IndexOf(_config.NewLine)) >= 0))
                return line.Remove(pos);
            
            return line;
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
                Serial.Write(GetMessageLine(data));
        }

        public string Read()
        {
            string line = "";
            if (Connected)
                line = ParseMessageLine(Serial.ReadLine());

            return line;
        }

        public void WriteBytes(byte[] data)
        {
            if (Connected)
                Serial.Write(data, 0, data.Length);
        }

        public byte[] ReadBytes()
        {
            byte[] result = null;
            if (Connected)
            {
                result = new byte[Serial.BytesToRead];

                Serial.Read(result, 0, Serial.BytesToRead);
            }
            return result;
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
