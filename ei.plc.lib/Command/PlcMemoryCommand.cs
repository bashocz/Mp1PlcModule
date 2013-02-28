using System;

namespace EI.Plc
{
    abstract class PlcMemoryCommand : PlcCommand
    {
        #region data members

        protected IPlcAddressSpace AddressSpace { get; private set; }
        public int Address { get; private set; }

        #endregion

        #region constructors

        public PlcMemoryCommand(IPlcAddressSpace addressSpace, PlcControlChar controlChar, int address)
            : base(controlChar)
        {
            if (addressSpace == null)
                throw new ArgumentNullException("addressSpace", "Argument wasn't initialized.");

            if (!addressSpace.CheckAddress(address))
                throw new ArgumentOutOfRangeException("address", "Argument is out of range.");

            AddressSpace = addressSpace;
            Address = address;
        }

        #endregion

        #region conversion methods

        protected string AddressToString()
        {
            return PlcStream.AddressToString(Address);
        }

        #endregion
    }
}