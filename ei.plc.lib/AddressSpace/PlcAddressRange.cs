using System;

namespace EI.Plc
{
    class PlcAddressRange : Tuple<int, int>, IPlcAddressRange
    {
        #region constructors

        public PlcAddressRange(int from, int to)
            : base(from, to) { }

        #endregion

        #region IPlcAddressRange members

        public bool CheckAddress(int address)
        {
            if ((address < Item1) || (address > Item2))
                return false;
            return true;
        }

        public bool CheckAddress(int address, int length)
        {
            if ((address < Item1) || (length < 1) || (address + (length - 1) > Item2))
                return false;
            return true;
        }

        #endregion
    }
}