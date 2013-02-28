using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    class PolisherStatusSimulator
    {
        #region private fields

        private readonly PlcMemory _memory;
        private int _polisherNumber;

        #endregion

        #region constructors

        public PolisherStatusSimulator(PlcMemory memory, int polisherNumber)
        {
            _memory = memory;
            _polisherNumber = polisherNumber;
            CreateObjects();
        }

        #endregion

        #region data members

        private void CreateObjects()
        {
            _state = _memory.CreateIntMemory(0x143 + _polisherNumber);

            _magazineId = _memory.CreateStringMemory(0x150 + ((_polisherNumber) * 64), 4);
            _plates = new List<PolisherPlateSimulator>();
            _plates.Add(new PolisherPlateSimulator(_memory, _polisherNumber, 0));
            _plates.Add(new PolisherPlateSimulator(_memory, _polisherNumber, 1));
            _plates.Add(new PolisherPlateSimulator(_memory, _polisherNumber, 2));
            _plates.Add(new PolisherPlateSimulator(_memory, _polisherNumber, 3));

            _highPressureGlobal = _memory.CreateBoolMemory(0x140 + _polisherNumber, 1);
            _highPressure = _memory.CreateBoolMemory(0x164 + (_polisherNumber * 64), 1);
            _highPressureDuration = _memory.CreateIntMemory(0x165 + (_polisherNumber * 64));
            _liquid = new PolisherLiquidSimulator(_memory, _polisherNumber);

            _heads = new List<PolisherHeadSimulator>();
            _heads.Add(new PolisherHeadSimulator(_memory, _polisherNumber, 0));
            _heads.Add(new PolisherHeadSimulator(_memory, _polisherNumber, 1));
            _heads.Add(new PolisherHeadSimulator(_memory, _polisherNumber, 2));
            _heads.Add(new PolisherHeadSimulator(_memory, _polisherNumber, 3));

            _plateRpm = _memory.CreateIntMemory(0x17B + (_polisherNumber * 64));
            _plateLoadCurrent = _memory.CreateDoubleMemory(0x180 + (_polisherNumber * 64), 1);

            _padUsedTime = _memory.CreateIntMemory(0x185 + (_polisherNumber * 64));
            _padUsedCount = _memory.CreateIntMemory(0x186 + (_polisherNumber * 64));
        }

        private IntMemory _state;
        public PolisherState State
        {
            get
            {
                switch (_state.Value)
                {
                    case 1:
                        return PolisherState.AutoProcess;
                    case 2:
                        return PolisherState.AutoWait;
                    case 3:
                        return PolisherState.AutoLoad;
                    case 4:
                        return PolisherState.AutoUnload;
                    case 5:
                        return PolisherState.Pause;
                    case 6:
                        return PolisherState.Alarm;
                    case 7:
                        return PolisherState.EmergencyStop;
                    default:
                        throw new ArgumentOutOfRangeException("_stateMemory.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <1, 7>. Actual value is {0}", _state.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case PolisherState.AutoProcess:
                        _state.Value = 1;
                        break;
                    case PolisherState.AutoWait:
                        _state.Value = 2;
                        break;
                    case PolisherState.AutoLoad:
                        _state.Value = 3;
                        break;
                    case PolisherState.AutoUnload:
                        _state.Value = 4;
                        break;
                    case PolisherState.Pause:
                        _state.Value = 5;
                        break;
                    case PolisherState.Alarm:
                        _state.Value = 6;
                        break;
                    case PolisherState.EmergencyStop:
                        _state.Value = 7;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type PolisherState, actual value is '{0}'.", value));
                }
            }
        }

        private StringMemory _magazineId;
        public string MagazineId
        {
            get { return _magazineId.Value; }
            set { _magazineId.Value = value; }
        }

        private List<PolisherPlateSimulator> _plates;
        public IList<PolisherPlateSimulator> Plates
        {
            get { return _plates.AsReadOnly(); }
        }

        private BoolMemory _highPressureGlobal;
        public bool HighPressureGlobal
        {
            get { return _highPressureGlobal.Value; }
        }

        private BoolMemory _highPressure;
        public bool HighPressure
        {
            get { return _highPressure.Value; }
            set
            {
                _highPressure.Value = value;
                _highPressureGlobal.Value = value;
            }
        }

        private IntMemory _highPressureDuration;
        public TimeSpan HighPressureDuration
        {
            get { return TimeSpan.FromMilliseconds(_highPressureDuration.Value); }
            set { _highPressureDuration.Value = (ushort)value.TotalMilliseconds; }
        }

        private IntMemory _plateRpm;
        public int PlateRpm
        {
            get { return _plateRpm.Value; }
            set { _plateRpm.Value = value; }
        }

        private DoubleMemory _plateLoadCurrent;
        public double PlateLoadCurrent
        {
            get { return _plateLoadCurrent.Value; }
            set { _plateLoadCurrent.Value = value; }
        }

        private List<PolisherHeadSimulator> _heads;
        public IList<PolisherHeadSimulator> Heads
        {
            get { return _heads.AsReadOnly(); }
        }

        private PolisherLiquidSimulator _liquid;
        public PolisherLiquidSimulator Liquid
        {
            get { return _liquid; }
        }

        private IntMemory _padUsedTime;
        public TimeSpan PadUsedTime
        {
            get { return TimeSpan.FromMilliseconds(_padUsedTime.Value); }
            set { _padUsedTime.Value = (ushort)value.TotalMilliseconds; }
        }

        private IntMemory _padUsedCount;
        public int PadUsedCount
        {
            get { return _padUsedCount.Value; }
            set { _padUsedCount.Value = value; }
        }

        #endregion
    }
}
