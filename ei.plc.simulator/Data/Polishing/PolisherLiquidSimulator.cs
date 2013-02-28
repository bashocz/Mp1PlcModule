using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EI.Plc
{
    class PolisherLiquidSimulator
    {
        #region private fields

        private readonly PlcMemory _memory;
        private int _offsetPolisher;

        #endregion

        #region constructors

        public PolisherLiquidSimulator(PlcMemory memory, int polisherNumber)
        {
            _memory = memory;
            _offsetPolisher = polisherNumber;
            CreateObjects();
        }

        #endregion

        #region data members

        private void CreateObjects()
        {
            _padTempMemory = _memory.CreateDoubleMemory(0x16A + (_offsetPolisher * 64), 1);
            _coolingWaterInTemp = _memory.CreateDoubleMemory(0x16B + (_offsetPolisher * 64), 1);
            _coolingWaterOutTemp = _memory.CreateDoubleMemory(0x16C + (_offsetPolisher * 64), 1);
            _slurryInTemp = _memory.CreateDoubleMemory(0x16D + (_offsetPolisher * 64), 1);
            _slurryOutTemp = _memory.CreateDoubleMemory(0x16E + (_offsetPolisher * 64), 1);
            _rinseTemp = _memory.CreateDoubleMemory(0x16F + (_offsetPolisher * 64), 1);
            _coolingWaterAmount = _memory.CreateDoubleMemory(0x170 + (_offsetPolisher * 64), 1);
            _slurryAmount = _memory.CreateDoubleMemory(0x171 + (_offsetPolisher * 64), 1);
            _rinseAmount = _memory.CreateDoubleMemory(0x172 + (_offsetPolisher * 64), 1);
        }

        private DoubleMemory _padTempMemory;
        public double PadTemp
        {
            get { return _padTempMemory.Value; }
            set { _padTempMemory.Value = value; }
        }

        private DoubleMemory _coolingWaterInTemp;
        public double CoolingWaterInTemp
        {
            get { return _coolingWaterInTemp.Value; }
            set { _coolingWaterInTemp.Value = value; }
        }

        private DoubleMemory _coolingWaterOutTemp;
        public double CoolingWaterOutTemp
        {
            get { return _coolingWaterOutTemp.Value; }
            set { _coolingWaterOutTemp.Value = value; }
        }

        private DoubleMemory _slurryInTemp;
        public double SlurryInTemp
        {
            get { return _slurryInTemp.Value; }
            set { _slurryInTemp.Value = value; }
        }

        private DoubleMemory _slurryOutTemp;
        public double SlurryOutTemp
        {
            get { return _slurryOutTemp.Value; }
            set { _slurryOutTemp.Value = value; }
        }

        private DoubleMemory _rinseTemp;
        public double RinseTemp
        {
            get { return _rinseTemp.Value; }
            set { _rinseTemp.Value = value; }
        }

        private DoubleMemory _coolingWaterAmount;
        public double CoolingWaterAmount
        {
            get { return _coolingWaterAmount.Value; }
            set { _coolingWaterAmount.Value = value; }
        }

        private DoubleMemory _slurryAmount;
        public double SlurryAmount
        {
            get { return _slurryAmount.Value; }
            set { _slurryAmount.Value = value; }
        }

        private DoubleMemory _rinseAmount;
        public double RinseAmount
        {
            get { return _rinseAmount.Value; }
            set { _rinseAmount.Value = value; }
        }

        #endregion
    }
}