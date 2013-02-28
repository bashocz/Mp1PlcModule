using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EI.Plc
{
    class PolisherPlateSimulator
    {
        #region private fields

        private readonly PlcMemory _memory;
        private int _offsetPolisher;
        private int _offsetPlate;

        #endregion

        #region constructors

        public PolisherPlateSimulator(PlcMemory memory, int polisherNumber, int plateNumber)
        {
            _memory = memory;
            _offsetPolisher = polisherNumber;
            _offsetPlate = plateNumber;
            CreateObjects();
        }

        #endregion

        #region data members

        private void CreateObjects()
        {
            _id = _memory.CreateStringMemory(0x154 + (_offsetPolisher * 64) + (_offsetPlate * 4), 4);
        }

        private StringMemory _id;
        public string Id
        {
            get { return _id.Value; }
            set { _id.Value = value; }
        }

        public int Recipe { get; set; }

        #endregion
    }
}
