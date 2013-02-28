using System;
using System.Globalization;

namespace EI.Plc
{
    class PlcMemoryReadCommand : PlcMemoryCommand
    {
        #region data members

        public int Length { get; private set; }
        public PlcMemoryReadData Data { get; set; }

        #endregion

        #region constructors

        public PlcMemoryReadCommand(IPlcAddressSpace addressSpace, int address, int length)
            : base(addressSpace, PlcControlChar.ENQ, address)
        {
            if (!AddressSpace.CheckAddress(Address, length))
                throw new ArgumentOutOfRangeException("length", "Argument is out of range.");

            Length = length;
        }

        #endregion

        #region conversion methods

        public override string CommandToString()
        {
            return BeginCharToString() +
                PlcStream.AddCheckSum(
                    IdsToString() +
                    "CR" +
                    MessageWaitTimeToString() +
                    AddressToString() +
                    Length.ToString("X2", CultureInfo.InvariantCulture)
                );
        }

        #endregion
    }
}
