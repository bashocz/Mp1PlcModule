using System;
using EI.Business;

namespace EI.Communication
{
    public class SerialCommunicationBufferedLog : BinaryLog, ICommunicationBuffered
    {
        #region private members

        private ILogger _logger;
        private string _moduleName = "RS232";
        private ICommunicationBuffered _obj;

        #endregion

        #region constructors

        public SerialCommunicationBufferedLog(ICommunicationBuffered obj, ILogger logger)
        {
            if (obj == null)
                throw new ArgumentNullException("obj", "SerialCommunicationBuffered object wasn't initialized.");

            if (logger == null)
                throw new ArgumentNullException("logger", "Logging object wasn't initialized.");

            _obj = obj;
            _logger = logger;
        }

        #endregion

        #region ICommunicationBufferd members

        public string Read(bool silent)
        {
            string ret = _obj.Read(silent);
            _logger.Debug(_moduleName, string.Format("RS({0}):{1}", silent, LogFormat(ret)));

            return ret;
        }

        #endregion

        #region IRs232Communication Members

        public bool RtsEnable
        {
            get
            {
                return _obj.RtsEnable;
            }
            set
            {
                _logger.Debug(_moduleName, string.Format("RtsEnable:{0}", value));
                _obj.RtsEnable = value;
            }
        }

        #endregion

        #region ICommunication Members

        public bool Open()
        {
            _logger.Debug(_moduleName, string.Format("Open:{0}", _obj.GetConfigurationInfo()));
            bool ret = _obj.Open();
            _logger.Debug(_moduleName, string.Format("Open:{0}", ret));

            return ret;
        }

        public void Close()
        {
            _logger.Debug(_moduleName, string.Format("Close:{0}", _obj.GetConfigurationInfo()));
            _obj.Close();
        }

        public void Write(string data)
        {
            _logger.Debug(_moduleName, string.Format("W:{0}", LogFormat(data)));
            _obj.Write(data);
        }

        public void WriteBytes(byte[] data)
        {
            _logger.Debug(_moduleName, string.Format("WB:{0}", LogFormat(data)));
            _obj.WriteBytes(data);
        }

        public string Read()
        {
            _logger.Debug(_moduleName, "R");
            string ret = _obj.Read();
            _logger.Debug(_moduleName, string.Format("R:{0}", LogFormat(ret)));

            return ret;
        }

        public byte[] ReadBytes()
        {
            _logger.Debug(_moduleName, "RB");
            byte[] ret = _obj.ReadBytes();
            _logger.Debug(_moduleName, string.Format("RB:{0}", LogFormat(ret)));

            return ret;
        }

        public bool HasDataToRead()
        {
            bool ret = _obj.HasDataToRead();
            return ret;
        }

        public bool CancelOperation()
        {
            _logger.Debug(_moduleName, "CancelOperation");
            bool ret = _obj.CancelOperation();
            _logger.Debug(_moduleName, string.Format("CancelOperation:{0}", ret));

            return ret;
        }

        public bool Connected
        {
            get { return _obj.Connected; }
        }

        public string GetConfigurationInfo()
        {
            return _obj.GetConfigurationInfo();
        }

        #endregion
    }
}
