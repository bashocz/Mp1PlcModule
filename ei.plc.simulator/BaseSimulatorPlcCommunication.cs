using System;
using System.Globalization;
using System.Text.RegularExpressions;
using EI.Business;

namespace EI.Plc
{
    abstract class BaseSimulatorPlcCommunication : ICommunication
    {
        #region enumerations

        private enum State
        {
            READY,
            CW,
            CR,
            ACK
        }

        private enum Msg
        {
            WriteCommand,
            ReadCommand,
            SumCheckError = 0x02,
            ProtocolError = 0x03,
            CharacterAreaError = 0x06,
            CharacterError = 0x07,
            PCCPUNumberError = 0x08
        }

        #endregion

        #region private and protected members

        protected readonly PlcMemory _memory;

        private State _machineState;

        private string _answer;

        private readonly int _stationId = 0;
        private readonly int _pcId = 255;

        #endregion

        #region constructors

        protected BaseSimulatorPlcCommunication()
        {
            _machineState = State.READY;
            _memory = new PlcMemory();
        }

        public void Dispose() { }

        #endregion

        #region ICommunication Members

        public bool Open()
        {
            Connected = true;
            return true;
        }

        public void Close()
        {
            Connected = false;
        }

        public void WriteBytes(byte[] data)
        {
            Write(data.ToString());
        }

        public void Write(string data)
        {
            if (!Connected)
                throw new InvalidOperationException();

            if (!IsMyMessage(data))
                return;

            switch (_machineState)
            {
                case State.READY:
                    _machineState = ProcessReadWriteCommand(data, out _answer);
                    break;
                case State.ACK:
                    if (!CheckAcknowledgeCommand(data))
                        ErrorMethod();
                    _machineState = State.READY;
                    break;
                default:
                    ErrorMethod();
                    _machineState = State.READY;
                    break;
            }
        }

        public byte[] ReadBytes()
        {
            return System.Text.Encoding.UTF8.GetBytes(Read());
        }

        public string Read()
        {
            if (!Connected)
                throw new InvalidOperationException();

            switch (_machineState)
            {
                case State.CW:
                    _machineState = State.READY;
                    return _answer;
                case State.CR:
                    if (_answer.StartsWith("\u0015", StringComparison.OrdinalIgnoreCase))
                        _machineState = State.READY;
                    else
                        _machineState = State.ACK;
                    return _answer;
                default:
                    ErrorMethod();
                    _machineState = State.READY;
                    throw new NotImplementedException();    // temporary
            }
        }

        public bool HasDataToRead()
        {
            throw new NotImplementedException();
        }

        public bool CancelOperation()
        {
            throw new NotImplementedException();
        }

        public bool Connected { get; private set; }

		public bool RtsEnable { get; set; }

        public string GetConfigurationInfo()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region process message methods

        public virtual void ErrorMethod()
        {
        }

        private State ProcessReadWriteCommand(string data, out string answer)
        {
            answer = "\u001500FF" + ((int)Msg.ProtocolError).ToString("X2", CultureInfo.InvariantCulture);
            Msg message = CheckReadWriteCommand(data);
            switch (message)
            {
                case Msg.WriteCommand:
                    answer = ParseWriteCommand(data);
                    return State.CW;
                case Msg.ReadCommand:
                    answer = ParseReadCommand(data);
                    return State.CR;
                default:
                    answer = "\u001500FF" + ((int)message).ToString("X2", CultureInfo.InvariantCulture);
                    ErrorMethod();
                    return State.CW;
            }
        }

        private Msg CheckReadWriteCommand(string command)
        {
            if (command.Length < 17)
                return Msg.ProtocolError;
            if (command.Substring(command.Length - 2, 2) != CalculateCheckSum(command.Substring(1, command.Length - 3)))
                return Msg.SumCheckError;
            if (command.Substring(0, 1) != "\u0005")
                return Msg.ProtocolError;

            int stationID = Convert.ToInt32(command.Substring(1, 2), 16);
            string commandType = command.Substring(5, 2);
            int pcID = Convert.ToInt32(command.Substring(3, 2), 16);

            if (stationID != 0x00)
                return Msg.ProtocolError;
            if (!((commandType == "CW") || (commandType == "CR")))
                return Msg.CharacterAreaError;
            if (!(CharactersCheck(command)))
                return Msg.CharacterError;
            if (((pcID < 0) || (pcID > 64)) && (pcID != 0xFF))
                return Msg.PCCPUNumberError;

            if (commandType == "CW")
                return Msg.WriteCommand;
            if (commandType == "CR")
                return Msg.ReadCommand;

            return Msg.ProtocolError;
        }

        private bool CheckAcknowledgeCommand(string data)
        {
            return ((data.Length == 5) && (data.Substring(0, 5) == "\u000600FF"));
        }

        private string AddCheckSum(string message)
        {
            return message + CalculateCheckSum(message);
        }

        private string CalculateCheckSum(string message)
        {
            int checkSumDec = 0;
            foreach (int character in message)
            {
                checkSumDec += character;
            }
            string checkSumHex = checkSumDec.ToString("X2", CultureInfo.InvariantCulture);
            return checkSumHex.Substring(checkSumHex.Length - 2, 2);
        }

        private bool IsMyMessage(string message)
        {
            if ((Convert.ToInt32(message.Substring(1, 2), 16) == _stationId) && (Convert.ToInt32(message.Substring(3, 2), 16) == _pcId))
                return true;
            return false;
        }

        private bool CharactersCheck(string command)
        {
            Regex rg = new Regex(@"^[A-Z0-9_\u0000\u0002\u0003\u0004\u0005\u0006\u000A\u000C\u000D\u0015]*$");
            return rg.IsMatch(command);
        }

        #endregion

        #region parsing methods

        private string ParseWriteCommand(string command)
        {
            int errorCode;
            if ((CheckForErrorWriteCommand(command, out errorCode)) == false)
            {
                ErrorMethod();
                return "\u001500FF" + errorCode.ToString("X2", CultureInfo.InvariantCulture);
            }
            
            _memory.WriteMemory(Convert.ToInt32(command.Substring(8, 5), 16), command.Substring(15, command.Length - 15 - 2));
            return "\u000600FF";
        }

        private string ParseReadCommand(string command)
        {
            int errorCode;
            if ((CheckForErrorReadCommand(command, out errorCode)) == false)
            {
                ErrorMethod();
                return "\u001500FF" + errorCode.ToString("X2", CultureInfo.InvariantCulture);
            }

            return "\u0002" + AddCheckSum("00FF" + _memory.ReadMemory(Convert.ToInt32(command.Substring(8, 5), 16),
                                                                      Convert.ToInt32(command.Substring(13, 2), 16)) + "\u0003");
        }

        #endregion

        #region protected abstract methods

        public abstract bool CheckForErrorWriteCommand(string command, out int errorCode);
        public abstract bool CheckForErrorReadCommand(string command, out int errorCode);
        protected abstract void InitializeMemory();

        #endregion
    }
}