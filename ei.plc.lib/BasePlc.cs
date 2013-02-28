using System;
using System.Diagnostics;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    abstract class BasePlc : IBasePlc
    {
        #region constructors and finalizer

        public BasePlc(ICommunication client)
        {
            if (client == null)
                throw new ArgumentNullException("client", "Client wasn't initialized.");
            _client = client;
        }

        #endregion

        #region address space

        private PlcAddressSpace _addressSpace;
        protected PlcAddressSpace AddressSpace
        {
            get { return _addressSpace ?? (_addressSpace = new PlcAddressSpace(GetAddressRanges())); }
        }
        protected abstract PlcAddressRange[] GetAddressRanges();

        #endregion

        #region concurency support

        private readonly int _timeout = 100;
        private Stopwatch _stopwatch;
        private void EnterOperation()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        private void ExitOperation()
        {
            _stopwatch.Stop();
            _stopwatch = null;
        }

        #endregion

        #region communications

        private readonly ICommunication _client;

        private void Write(string message)
        {
            try
            {
                if (!_client.Connected)
                    throw new InvalidOperationException("ICommunication client is not connected.");
                if (message == null)
                    throw new ArgumentNullException("message", "Argument can't be null.");
                _client.Write(message);
            }
            catch (Exception ex)
            {
                throw new PlcException(PlcErrorCode.Write, string.Format(CultureInfo.InvariantCulture, "{0}: Write data to PLC exception.", GetType().Name), ex);
            }
        }

        private string Read()
        {
            try
            {
                if (!_client.Connected)
                    throw new InvalidOperationException("ICommunication client is not connected.");
                return _client.Read();
            }
            catch (Exception ex)
            {
                throw new PlcException(PlcErrorCode.Read, string.Format(CultureInfo.InvariantCulture, "{0}: Read data from PLC exception.", GetType().Name), ex);
            }
        }

        private PlcWriteStream ParsingWriteStream<TConverter, TParam>(TParam arg)
            where TConverter : IToStreamConverter<TParam>, new()
        {
            PlcWriteStream stream = null;
            try
            {
                TConverter converter = new TConverter();
                stream = converter.TryConvert(arg);
            }
            catch (PlcException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: Unhandled parsing exception.", GetType().Name), ex);
            }
            return stream;
        }

        private string GetWriteMessage(int address, PlcWriteStream stream)
        {
            string message = null;
            try
            {
                PlcMemoryWriteCommand command = new PlcMemoryWriteCommand(AddressSpace, address, stream);
                message = command.CommandToString();
            }
            catch (ArgumentNullException ex)
            {
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: {1}", GetType().Name, ex.Message), ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: {1}", GetType().Name, ex.Message), ex);
            }
            return message;
        }

        protected bool WriteMemory<TConverter, TParam>(int address, TParam arg)
            where TConverter : IToStreamConverter<TParam>, new()
        {
            EnterOperation();
            bool result = false;

            string message = GetWriteMessage(address, ParsingWriteStream<TConverter, TParam>(arg));
            Write(message);

            string ack = Read();
            result = string.Compare((new PlcMemoryAcknowledge()).CommandToString(), ack, StringComparison.OrdinalIgnoreCase) == 0;

            ExitOperation();
            return result;
        }

        private string GetReadMessage(int address, int length)
        {
            string message = null;
            try
            {
                PlcMemoryReadCommand command = new PlcMemoryReadCommand(AddressSpace, address, length);
                message = command.CommandToString();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: {1}", GetType().Name, ex.Message), ex);
            }
            return message;
        }

        private TResult ParsingReadStream<TConverter, TResult>(PlcMemoryReadData data)
            where TConverter : IFromStreamConverter<TResult>, new()
            where TResult : new()
        {
            TResult result = default(TResult);
            try
            {
                TConverter converter = new TConverter();
                result = converter.TryConvert(data.Data);
            }
            catch (PlcException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: Unhandled parsing exception.", GetType().Name), ex);
            }
            return result;
        }

        private PlcMemoryReadData GetReadData(string stream)
        {
            PlcMemoryReadData data = null;
            try
            {
                data = PlcMemoryReadData.Create(stream);
            }
            catch (ArgumentNullException ex)
            {
                Write((new PlcMemoryNegAcknowledge()).CommandToString());
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: {1}.", GetType().Name, ex.Message), ex);
            }
            catch (ArgumentException ex)
            {
                Write((new PlcMemoryNegAcknowledge()).CommandToString());
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: {1}.", GetType().Name, ex.Message), ex);
            }
            catch (FormatException ex)
            {
                Write((new PlcMemoryNegAcknowledge()).CommandToString());
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: {1}.", GetType().Name, ex.Message), ex);
            }
            return data;
        }

        protected TResult ReadMemory<TConverter, TResult>(int address, int length)
            where TConverter : IFromStreamConverter<TResult>, new()
            where TResult : new()
        {
            EnterOperation();

            string message = GetReadMessage(address, length);
            Write(message);

            string stream = Read();
            PlcMemoryReadData data = GetReadData(stream);

            Write((new PlcMemoryAcknowledge()).CommandToString());
            if (data.IsError)
                throw new PlcException(PlcErrorCode.ReadParsing, string.Format(CultureInfo.InvariantCulture, "{0}: Error code <{1}> during parsing data from read stream.", GetType().Name, data.ErrorCode));

            TResult obj = ParsingReadStream<TConverter, TResult>(data);

            ExitOperation();
            return obj;
        }

        #endregion

        #region IBasePlc members

        public bool Open()
        {
            if (_client.Connected)
                throw new InvalidOperationException();
            return _client.Open();
        }

        public void Close()
        {
            if (!_client.Connected)
                throw new InvalidOperationException();
            _client.Close();
        }

        public PlcCommunicationStatus GetCommunicationStatus()
        {
            if ((_client == null) || (!_client.Connected))
                return PlcCommunicationStatus.Unknown;
            if (_stopwatch == null)
                return PlcCommunicationStatus.Ready;
            if (_stopwatch.ElapsedMilliseconds < _timeout)
                return PlcCommunicationStatus.Communicate;
            return PlcCommunicationStatus.Timeout;
        }

        #endregion
    }
}