using System;
using System.Globalization;

namespace EI.Plc
{
    class PlcMemoryWriteCommand : PlcMemoryCommand
    {
        #region data members

        private PlcWriteStream _stream;

        #endregion

        #region constructors

        public PlcMemoryWriteCommand(IPlcAddressSpace addressSpace, int address, PlcWriteStream stream)
            : base(addressSpace, PlcControlChar.ENQ, address)
        {
            if (stream == null)
                throw new ArgumentNullException("stream", "Argument can't be null.");

            _stream = stream;
        }

        #endregion

        #region conversion methods

        private string GetCommand(int length, string stream)
        {
            if (!AddressSpace.CheckAddress(Address, length))
                throw new InvalidOperationException("The address + length of data is out of range");

            return BeginCharToString() +
                PlcStream.AddCheckSum(
                    IdsToString() +
                    "CW" +
                    MessageWaitTimeToString() +
                    AddressToString() +
                    length.ToString("X2", CultureInfo.InvariantCulture) +
                    stream
                );
        }

        public override string CommandToString()
        {
            return GetCommand(_stream.Length, _stream.Stream);
        }

        #endregion
    }
}
