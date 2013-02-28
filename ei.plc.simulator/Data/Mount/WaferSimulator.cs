using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EI.Plc
{
    class WaferSimulator
    {
        #region private fields

        private readonly PlcMemory _memory;
        private int _offsetWafer;

        #endregion

        #region constructors

        public WaferSimulator(PlcMemory memory, int waferNumber)
        {
            _memory = memory;
            _offsetWafer = waferNumber;
            CreateObjects();
        }

        #endregion

        #region data members

        private void CreateObjects()
        {
            _waferCassetteNumber = _memory.CreateIntMemory(0x200 + (_offsetWafer * 2));
            _waferSlotNumber = _memory.CreateIntMemory(0x201 + (_offsetWafer * 2));
        }

        private IntMemory _waferCassetteNumber;
        public int WaferCassetteNumber
        {
            get { return _waferCassetteNumber.Value; }
            set { _waferCassetteNumber.Value = value; }
        }

        private IntMemory _waferSlotNumber;
        public int WaferSlotNumber
        {
            get { return _waferSlotNumber.Value; }
            set { _waferSlotNumber.Value = value; }
        }

        #endregion
    }
}
