using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EI.Plc
{
    class CassetteSensorSimulator
    {
        #region private fields

        private readonly PlcMemory _memory;
        private int _offsetStation;

        #endregion

        #region constructors

        public CassetteSensorSimulator(PlcMemory memory, int stationNumber)
        {
            _memory = memory;
            _offsetStation = stationNumber;
            CreateObjects();
        }

        #endregion

        #region data members

        private void CreateObjects()
        {
            _isCassettePositioned = _memory.CreateBoolMemory(0x141 + _offsetStation, 1);
        }

        private BoolMemory _isCassettePositioned;
        public bool IsCassettePositioned
        {
            get { return _isCassettePositioned.Value; }
            set { _isCassettePositioned.Value = value; }
        }

        #endregion
    }
}
