using System;
using System.Collections.Generic;

namespace EI.Plc
{
    class PlcAddressSpace: IPlcAddressSpace
    {
        #region constructors

        public PlcAddressSpace(params IPlcAddressRange[] ranges)
        {
            if (ranges == null)
                throw new ArgumentNullException("ranges", "Argument wasn't initialized.");

            _ranges = new List<IPlcAddressRange>(ranges);
        }

        #endregion

        #region IPlcAddressSpace members

        private List<IPlcAddressRange> _ranges;

        public bool CheckAddress(int address)
        {
            foreach (IPlcAddressRange range in _ranges)
                if (range.CheckAddress(address))
                    return true;
            return false;
        }

        public bool CheckAddress(int address, int length)
        {
            foreach (IPlcAddressRange range in _ranges)
                if (range.CheckAddress(address, length))
                    return true;
            return false;
        }

        #endregion
    }
}
