using System;
using System.Globalization;
using System.Collections.Generic;
using EI.Business;

namespace EI.Plc
{
    class MountSimulatorPlcCommunication : BaseSimulatorPlcCommunication
    {
        #region constructors

        private MountSimulatorPlcCommunication()
            : base()
        {
            CreateObjects();
        }

        public static MountSimulatorPlcCommunication Create()
        {
            MountSimulatorPlcCommunication plc = new MountSimulatorPlcCommunication();
            plc.InitializeMemory();
            return plc;
        }

        #endregion

        #region data members

        private void CreateObjects()
        {
            _cassetteId = _memory.CreateStringMemory(0x120, 4);
            _isCassetteId = _memory.CreateBoolMemory(0x124, 1);
            _isCassetteIdError = _memory.CreateBoolMemory(0x125, 1);
            _isLotDataTimeout = _memory.CreateBoolMemory(0x126, 1);
            _newLotId = _memory.CreateStringMemory(0x127, 7);
            _line = _memory.CreateIntMemory(0x12E);
            _isLotStarted = _memory.CreateBoolMemory(0x12F, 1);
            _isCarrierPlateArrived = _memory.CreateBoolMemory(0x130, 1);
            _isBarcodeReadOk = _memory.CreateBoolMemory(0x131, 1);
            _isCarrierPlateMountingReady = _memory.CreateBoolMemory(0x132, 1);
            _waferBreakNumber = _memory.CreateIntMemory(0x133);
            _isWaferBreakInfoOk = _memory.CreateBoolMemory(0x134, 1);
            _isMountingErrorCarrierPlate = _memory.CreateBoolMemory(0x135, 1);
            _isLotEnded = _memory.CreateBoolMemory(0x136, 1);
            _isReservationLotCanceled = _memory.CreateBoolMemory(0x137, 1);
            _state = _memory.CreateIntMemory(0x140);
            _informationSystemError = _memory.CreateBoolMemory(0x141, 1);
            _lotId = _memory.CreateStringMemory(0x150, 7);
            
            _cassettes = new List<CassetteSimulator>();
            for (int cassetteNumber = 0; cassetteNumber < 12; cassetteNumber++)
            {
                _cassettes.Add(new CassetteSimulator(_memory, cassetteNumber));
            }
            _cassetteQuantityInLot = _memory.CreateIntMemory(0x187);           
            _lotDataTransmission = _memory.CreateIntMemory(0x188);
            _waferQuantity = _memory.CreateIntMemory(0x189);
            _notGoodWaferQuantity = _memory.CreateIntMemory(0x18A);
            _size = _memory.CreateIntMemory(0x18B);
            _type = _memory.CreateIntMemory(0x18C);
            _division = _memory.CreateIntMemory(0x18D);
            _carrierPlateQuantity1 = _memory.CreateIntMemory(0x18E);
            _waferQuantity1 = _memory.CreateIntMemory(0x18F);
            _carrierPlateQuantity2 = _memory.CreateIntMemory(0x190);
            _waferQuantity2 = _memory.CreateIntMemory(0x191);

            _wafers = new List<WaferSimulator>();
            for (int waferNumber = 0; waferNumber < 300; waferNumber++)
            {
                _wafers.Add(new WaferSimulator(_memory, waferNumber));
            }
        }

        private StringMemory _cassetteId;
        public string CassetteId
        {
            get { return _cassetteId.Value; }
            set { _cassetteId.Value = value; }
        }

        private BoolMemory _isCassetteId;
        public bool IsCassetteId
        {
            get { return _isCassetteId.Value; }
            set { _isCassetteId.Value = value; }
        }

        private BoolMemory _isCassetteIdError;
        public bool IsCassetteIdError
        {
            get { return _isCassetteIdError.Value; }
            set { _isCassetteIdError.Value = value; }
        }

        private BoolMemory _isLotDataTimeout;
        public bool IsLotDataTimeout
        {
            get { return _isLotDataTimeout.Value; }
            set { _isLotDataTimeout.Value = value; }
        }

        private StringMemory _newLotId;
        public string NewLotId
        {
            get { return _newLotId.Value; }
            set { _newLotId.Value = value; }
        }

        private IntMemory _line;
        public MountLine Line
        {
            get
            {
                switch (_line.Value)
                {
                    case 0:
                        return MountLine.Cleared;
                    case 1:
                        return MountLine.Right;
                    case 2:
                        return MountLine.Left;
                    case 3:
                        return MountLine.Both;
                    default:
                        throw new ArgumentOutOfRangeException("_line.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <0, 3>. Actual value is {0}", _line.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case MountLine.Cleared:
                        _line.Value = 0;
                        break;
                    case MountLine.Right:
                        _line.Value = 1;
                        break;
                    case MountLine.Left:
                        _line.Value = 2;
                        break;
                    case MountLine.Both:
                        _line.Value = 3;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type MountLine, actual value is '{0}'.", value));
                }
            }
        }

        private BoolMemory _isLotStarted;
        public bool IsLotStarted
        {
            get { return _isLotStarted.Value; }
            set { _isLotStarted.Value = value; }
        }

        private BoolMemory _isCarrierPlateArrived;
        public bool IsCarrierPlateArrived
        {
            get { return _isCarrierPlateArrived.Value; }
            set { _isCarrierPlateArrived.Value = value; }
        }

        private BoolMemory _isBarcodeReadOk;
        public bool IsBarcodeReadedOk
        {
            get { return _isBarcodeReadOk.Value; }
            set { _isBarcodeReadOk.Value = value; }
        }

        private BoolMemory _isCarrierPlateMountingReady;
        public bool IsCarrierPlateMountingReady
        {
            get { return _isCarrierPlateMountingReady.Value; }
            set { _isCarrierPlateMountingReady.Value = value; }
        }

        private IntMemory _waferBreakNumber;
        public int WaferBreakNumber
        {
            get { return _waferBreakNumber.Value; }
            set { _waferBreakNumber.Value = value; }
        }

        private BoolMemory _isWaferBreakInfoOk;
        public bool IsWaferBreakInfoOk
        {
            get { return _isWaferBreakInfoOk.Value; }
            set { _isWaferBreakInfoOk.Value = value; }
        }

        private BoolMemory _isMountingErrorCarrierPlate;
        public bool IsMountingCarrierPlateError
        {
            get { return _isMountingErrorCarrierPlate.Value; }
            set { _isMountingErrorCarrierPlate.Value = value; }
        }

        private BoolMemory _isLotEnded;
        public bool IsLotEnded
        {
            get { return _isLotEnded.Value; }
            set { _isLotEnded.Value = value; }
        }

        private BoolMemory _isReservationLotCanceled;
        public bool IsReservationLotCanceled
        {
            get { return _isReservationLotCanceled.Value; }
            set { _isReservationLotCanceled.Value = value; }
        }

        private IntMemory _state;
        public MountState State
        {
            get
            {
                switch (_state.Value)
                {
                    case 1:
                        return MountState.AutoMount;
                    case 2:
                        return MountState.Stop;
                    case 3:
                        return MountState.AutoMountAlarm;
                    case 4:
                        return MountState.StopAlarm;
                    default:
                        throw new ArgumentOutOfRangeException("_state.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <1, 4>. Actual value is {0}", _state.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case MountState.AutoMount:
                        _state.Value = 1;
                        break;
                    case MountState.Stop:
                        _state.Value = 2;
                        break;
                    case MountState.AutoMountAlarm:
                        _state.Value = 3;
                        break;
                    case MountState.StopAlarm:
                        _state.Value = 4;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type MountState, actual value is '{0}'.", value));
                }
            }
        }

        private BoolMemory _informationSystemError;
        public bool InformationSystemError
        {
            get { return _informationSystemError.Value; }
            set { _informationSystemError.Value = value; }
        }

        private StringMemory _lotId;
        public string LotId
        {
            get { return _lotId.Value; }
            set { _lotId.Value = value; }
        }

        private List<CassetteSimulator> _cassettes;
        public IList<CassetteSimulator> Cassettes
        {
            get { return _cassettes.AsReadOnly(); }
        }

        private IntMemory _cassetteQuantityInLot;
        public int CassetteQuantityInLot
        {
            get { return _cassetteQuantityInLot.Value; }
            set { _cassetteQuantityInLot.Value = value; }
        }

        private IntMemory _lotDataTransmission;
        public LotDataTransmission LotDataTransmission
        {
            get
            {
                switch (_lotDataTransmission.Value)
                {
                    case 0:
                        return LotDataTransmission.Cleared;
                    case 1:
                        return LotDataTransmission.BeforeWritingCassetteInfo;
                    case 2:
                        return LotDataTransmission.BeforeWritingWaferInfo;
                    default:
                        throw new ArgumentOutOfRangeException("_lotDataTransmission.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <0, 2>. Actual value is {0}", _lotDataTransmission.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case LotDataTransmission.Cleared:
                        _state.Value = 0;
                        break;
                    case LotDataTransmission.BeforeWritingCassetteInfo:
                        _state.Value = 1;
                        break;
                    case LotDataTransmission.BeforeWritingWaferInfo:
                        _state.Value = 2;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type LotDataTransmission, actual value is '{0}'.", value));
                }
            }
        }

        private IntMemory _waferQuantity;
        public int WaferQuantity
        {
            get { return _waferQuantity.Value; }
            set { _waferQuantity.Value = value; }
        }

        private IntMemory _notGoodWaferQuantity;
        public int NotGoodWaferQuantity
        {
            get { return _notGoodWaferQuantity.Value; }
            set { _notGoodWaferQuantity.Value = value; }
        }

        private IntMemory _size;
        public WaferSize Size
        {
            get
            {
                switch (_size.Value)
                {
                    case 0:
                        return WaferSize.AnySize;
                    case 6:
                        return WaferSize.Size6Inches;
                    case 8:
                        return WaferSize.Size8Inches;
                    default:
                        throw new ArgumentOutOfRangeException("_size.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer 0, 6 or 8. Actual value is {0}", _size.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case WaferSize.AnySize:
                        _size.Value = 0;
                        break;
                    case WaferSize.Size6Inches:
                        _size.Value = 6;
                        break;
                    case WaferSize.Size8Inches:
                        _size.Value = 8;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type WaferSize, actual value is '{0}'.", value));
                }
            }
        }

        private IntMemory _type;
        public OfType Type
        {
            get
            {
                switch (_type.Value)
                {
                    case 0:
                        return OfType.Cleared;
                    case 1:
                        return OfType.OF;
                    case 2:
                        return OfType.VNotch;
                    default:
                        throw new ArgumentOutOfRangeException("_type.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <0, 2>. Actual value is {0}", _type.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case OfType.Cleared:
                        _type.Value = 0;
                        break;
                    case OfType.OF:
                        _type.Value = 1;
                        break;
                    case OfType.VNotch:
                        _type.Value = 2;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type OfType, actual value is '{0}'.", value));
                }
            }
        }

        private IntMemory _division;
        public PolishDivision Division
        {
            get
            {
                switch (_division.Value)
                {
                    case 0:
                        return PolishDivision.Cleared;
                    case 1:
                        return PolishDivision.New;
                    case 2:
                        return PolishDivision.Repolish;
                    default:
                        throw new ArgumentOutOfRangeException("_division.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <0, 2>. Actual value is {0}", _division.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case PolishDivision.Cleared:
                        _division.Value = 0;
                        break;
                    case PolishDivision.New:
                        _division.Value = 1;
                        break;
                    case PolishDivision.Repolish:
                        _division.Value = 2;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type PolishDivision, actual value is '{0}'.", value));
                }
            }
        }

        private IntMemory _carrierPlateQuantity1;
        public int CarrierPlateQuantity1
        {
            get { return _carrierPlateQuantity1.Value; }
            set { _carrierPlateQuantity1.Value = value; }
        }

        private IntMemory _waferQuantity1;
        public int WaferQuantity1
        {
            get { return _waferQuantity1.Value; }
            set { _waferQuantity1.Value = value; }
        }

        private IntMemory _carrierPlateQuantity2;
        public int CarrierPlateQuantity2
        {
            get { return _carrierPlateQuantity2.Value; }
            set { _carrierPlateQuantity2.Value = value; }
        }

        private IntMemory _waferQuantity2;
        public int WaferQuantity2
        {
            get { return _waferQuantity2.Value; }
            set { _waferQuantity2.Value = value; }
        }

        private List<WaferSimulator> _wafers;
        public IList<WaferSimulator> Wafers
        {
            get { return _wafers.AsReadOnly(); }
        }

        #endregion

        #region BaseSimulatorPlcCommunication members

        public override bool CheckForErrorWriteCommand(string command, out int errorCode)
        {
            return (CheckAddress(Convert.ToInt32(command.Substring(8, 5), 16), Convert.ToInt32(command.Substring(13, 2), 16), out errorCode));
        }

        public override bool CheckForErrorReadCommand(string command, out int errorCode)
        {
            return (CheckAddress(Convert.ToInt32(command.Substring(8, 5), 16), Convert.ToInt32(command.Substring(13, 2), 16), out errorCode));
        }

        protected override void InitializeMemory()
        {  
            State = MountState.Stop;
            Line = MountLine.Both;
            WaferQuantity1 = 3;
            WaferQuantity2 = 3;
        }

        #endregion

        #region private members

        private bool CheckAddress(int address, int length, out int errorCode)
        {
            if ((address < 0x120) || (address > 0x141)
                && ((address < 0x150) || (address > 0x191))
                && ((address < 0x200) || (address > 0x457)))
            {
                errorCode = 0x06;
                return false;
            }

            if ((address + (length - 1) < 0x120) || (address + (length - 1) > 0x141)
                && ((address + (length - 1) < 0x150) || (address + (length - 1) > 0x191))
                && ((address + (length - 1) < 0x200) || (address + (length - 1) > 0x457)))
            {
                errorCode = 0x06;
                return false;
            }

            errorCode = -1;
            return true;
        }

        #endregion
    }
}