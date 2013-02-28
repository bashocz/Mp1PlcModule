using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EI.Plc
{
    class PolisherHeadSimulator
    {
        #region private fields

        private readonly PlcMemory _memory;
        private int _offsetHead;
        private int _offsetPolisher;

        #endregion

        #region constructors

        public PolisherHeadSimulator(PlcMemory memory, int polisherNumber, int headNumber)
        {
            _memory = memory;
            _offsetPolisher = polisherNumber;
            _offsetHead = headNumber;           
            CreateObjects();
        }

        #endregion

        #region data members

        private void CreateObjects()
        {
            _force = _memory.CreateIntMemory(0x166 + (_offsetPolisher * 64) + _offsetHead);
            _pressure = _memory.CreateDoubleMemory(0x173 + (_offsetPolisher * 64) + _offsetHead, 3);
            _backpressure = _memory.CreateDoubleMemory(0x177 + (_offsetPolisher * 64) + _offsetHead, 2);
            _rpm = _memory.CreateIntMemory(0x17C + (_offsetPolisher * 64) + _offsetHead);
            _loadCurrent = _memory.CreateDoubleMemory(0x181 + (_offsetPolisher * 64) + _offsetHead, 1);
        }

        private IntMemory _force;
        public int Force
        {
            get { return _force.Value; }
            set { _force.Value = value; }
        }

        private DoubleMemory _pressure;
        public double Pressure
        {
            get { return _pressure.Value; }
            set { _pressure.Value = value; }
        }

        private DoubleMemory _backpressure;
        public double Backpressure
        {
            get { return _backpressure.Value; }
            set { _backpressure.Value = value; }
        }

        private IntMemory _rpm;
        public int Rpm
        {
            get { return _rpm.Value; }
            set { _rpm.Value = value; }
        }

        private DoubleMemory _loadCurrent;
        public double LoadCurrent
        {
            get { return _loadCurrent.Value; }
            set { _loadCurrent.Value = value; }
        }

        #endregion
    }
}
