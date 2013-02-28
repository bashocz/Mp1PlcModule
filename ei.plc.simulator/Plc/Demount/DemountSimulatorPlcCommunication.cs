using System;
using System.Globalization;
using System.Collections.Generic;
using EI.Business;

namespace EI.Plc
{
    class DemountSimulatorPlcCommunication : BaseSimulatorPlcCommunication
    {
        #region constructors

        private DemountSimulatorPlcCommunication()
            : base()
        {
            CreateObjects();
        }

        public static DemountSimulatorPlcCommunication Create()
        {
            DemountSimulatorPlcCommunication plc = new DemountSimulatorPlcCommunication();
            plc.InitializeMemory();
            return plc;
        }

        #endregion

        #region data members

        private void CreateObjects()
        {
            _isCarrierPlateArrived = _memory.CreateBoolMemory(0x120, 1);
            _isInformationSystemError = _memory.CreateBoolMemory(0x121, 1);
            _isCarrierPlateBarcodeReadedOk = _memory.CreateBoolMemory(0x122, 1);
            _isCarrierPlateDemountStarted = _memory.CreateBoolMemory(0x123, 1);
            _carrierPlateWaferSize = _memory.CreateIntMemory(0x124);
            _carrierPlateWaferCount = _memory.CreateIntMemory(0x125);
            _demountCassetteStation = _memory.CreateIntMemory(0x126);
            _waferDemountCounter = _memory.CreateIntMemory(0x127);
            _isCarrierPlateDemounted = _memory.CreateBoolMemory(0x128, 1);
            _carrierPlateRoutingType = _memory.CreateIntMemory(0x129);
            _removeCassetteFromDemountStation = _memory.CreateIntMemory(0x130);
            _cassetteWaferSize = _memory.CreateIntMemory(0x131);
            _destinationStation = _memory.CreateIntMemory(0x132);
            _canReadCassetteBarcode = _memory.CreateIntMemory(0x133);
            _isCassetteBarcodeReadedOk = _memory.CreateBoolMemory(0x134, 1);
            _shouldInspectSpatula = _memory.CreateBoolMemory(0x140, 1);
            
            _isCassette = new List<CassetteSensorSimulator>();
            _isCassette.Add(new CassetteSensorSimulator(_memory, 0));
            _isCassette.Add(new CassetteSensorSimulator(_memory, 1));
            _isCassette.Add(new CassetteSensorSimulator(_memory, 2));
            _isCassette.Add(new CassetteSensorSimulator(_memory, 3));
            
            _state = _memory.CreateIntMemory(0x145);
        }

        private BoolMemory _isCarrierPlateArrived;
        public bool IsCarrierPlateArrived
        {
            get { return _isCarrierPlateArrived.Value; }
            set { _isCarrierPlateArrived.Value = value; }
        }

        private BoolMemory _isInformationSystemError;
        public bool IsInformationSystemError
        {
            get { return _isInformationSystemError.Value; }
            set { _isInformationSystemError.Value = value; }
        }

        private BoolMemory _isCarrierPlateBarcodeReadedOk;
        public bool IsCarrierPlateBarcodeReadedOk
        {
            get { return _isCarrierPlateBarcodeReadedOk.Value; }
            set { _isCarrierPlateBarcodeReadedOk.Value = value; }
        }

        private BoolMemory _isCarrierPlateDemountStarted;
        public bool IsCarrierPlateDemountStarted
        {
            get { return _isCarrierPlateDemountStarted.Value; }
            set { _isCarrierPlateDemountStarted.Value = value; }
        }

        private IntMemory _carrierPlateWaferSize;
        public WaferSize CarrierPlateWaferSize
        {
            get
            {
                switch (_carrierPlateWaferSize.Value)
                {
                    case 0:
                        return WaferSize.AnySize;
                    case 6:
                        return WaferSize.Size6Inches;
                    case 8:
                        return WaferSize.Size8Inches;
                    default:
                        throw new ArgumentOutOfRangeException("_carrierPlateWaferSize.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer 0, 6 or 8. Actual value is {0}", _carrierPlateWaferSize.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case WaferSize.AnySize:
                        _carrierPlateWaferSize.Value = 0;
                        break;
                    case WaferSize.Size6Inches:
                        _carrierPlateWaferSize.Value = 6;
                        break;
                    case WaferSize.Size8Inches:
                        _carrierPlateWaferSize.Value = 8;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type WaferSize, actual value is '{0}'.", value));
                }
            }
        }

        private IntMemory _carrierPlateWaferCount;
        public int CarrierPlateWaferCount
        {
            get { return _carrierPlateWaferCount.Value; }
            set { _carrierPlateWaferCount.Value = value; }
        }

        private IntMemory _demountCassetteStation;
        public DemountCassetteStation DemountCassetteStation
        {
            get
            {
                switch (_demountCassetteStation.Value)
                {
                    case 0:
                        return DemountCassetteStation.Cleared;
                    case 1:
                        return DemountCassetteStation.Station1;
                    case 2:
                        return DemountCassetteStation.Station2;
                    case 3:
                        return DemountCassetteStation.Station3;
                    case 4:
                        return DemountCassetteStation.Station4;
                    default:
                        throw new ArgumentOutOfRangeException("_demountCassetteStation.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <0, 4>. Actual value is {0}", _demountCassetteStation.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case DemountCassetteStation.Cleared:
                        _demountCassetteStation.Value = 0;
                        break;
                    case DemountCassetteStation.Station1:
                        _demountCassetteStation.Value = 1;
                        break;
                    case DemountCassetteStation.Station2:
                        _demountCassetteStation.Value = 2;
                        break;
                    case DemountCassetteStation.Station3:
                        _demountCassetteStation.Value = 3;
                        break;
                    case DemountCassetteStation.Station4:
                        _demountCassetteStation.Value = 4;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type DemountCassetteStation, actual value is '{0}'.", value));
                }
            }
        }

        private IntMemory _waferDemountCounter;
        public int WaferDemountCounter
        {
            get { return _waferDemountCounter.Value; }
            set { _waferDemountCounter.Value = value; }
        }

        private BoolMemory _isCarrierPlateDemounted;
        public bool IsCarrierPlateDemounted
        {
            get { return _isCarrierPlateDemounted.Value; }
            set { _isCarrierPlateDemounted.Value = value; }
        }

        private IntMemory _carrierPlateRoutingType;
        public CarrierPlateRoutingType CarrierPlateRoutingType
        {
            get
            {
                switch (_carrierPlateRoutingType.Value)
                {
                    case 0:
                        return CarrierPlateRoutingType.Cleared;
                    case 1:
                        return CarrierPlateRoutingType.BackThroughAwps;
                    case 2:
                        return CarrierPlateRoutingType.InspectionRequired;
                    default:
                        throw new ArgumentOutOfRangeException("_carrierPlateRoutingType.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <0, 2>. Actual value is {0}", _carrierPlateRoutingType.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case CarrierPlateRoutingType.Cleared:
                        _carrierPlateRoutingType.Value = 0;
                        break;
                    case CarrierPlateRoutingType.BackThroughAwps:
                        _carrierPlateRoutingType.Value = 1;
                        break;
                    case CarrierPlateRoutingType.InspectionRequired:
                        _carrierPlateRoutingType.Value = 2;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type CarrierPlateRoutingType, actual value is '{0}'.", value));
                }
            }
        }

        private IntMemory _removeCassetteFromDemountStation;
        public DemountCassetteStation RemoveCassetteFromDemountStation
        {
            get
            {
                switch (_removeCassetteFromDemountStation.Value)
                {
                    case 0:
                        return DemountCassetteStation.Cleared;
                    case 1:
                        return DemountCassetteStation.Station1;
                    case 2:
                        return DemountCassetteStation.Station2;
                    case 3:
                        return DemountCassetteStation.Station3;
                    case 4:
                        return DemountCassetteStation.Station4;
                    default:
                        throw new ArgumentOutOfRangeException("_removeCassetteFromDemountStation.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <0, 4>. Actual value is {0}", _removeCassetteFromDemountStation.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case DemountCassetteStation.Cleared:
                        _removeCassetteFromDemountStation.Value = 0;
                        break;
                    case DemountCassetteStation.Station1:
                        _removeCassetteFromDemountStation.Value = 1;
                        break;
                    case DemountCassetteStation.Station2:
                        _removeCassetteFromDemountStation.Value = 2;
                        break;
                    case DemountCassetteStation.Station3:
                        _removeCassetteFromDemountStation.Value = 3;
                        break;
                    case DemountCassetteStation.Station4:
                        _removeCassetteFromDemountStation.Value = 4;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type DemountCassetteStation, actual value is '{0}'.", value));
                }
            }
        }

        private IntMemory _cassetteWaferSize;
        public WaferSize CassetteWaferSize
        {
            get
            {
                switch (_cassetteWaferSize.Value)
                {
                    case 0:
                        return WaferSize.AnySize;
                    case 6:
                        return WaferSize.Size6Inches;
                    case 8:
                        return WaferSize.Size8Inches;
                    default:
                        throw new ArgumentOutOfRangeException("_cassetteWaferSize.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer 0, 6 or 8. Actual value is {0}", _cassetteWaferSize.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case WaferSize.AnySize:
                        _cassetteWaferSize.Value = 0;
                        break;
                    case WaferSize.Size6Inches:
                        _cassetteWaferSize.Value = 6;
                        break;
                    case WaferSize.Size8Inches:
                        _cassetteWaferSize.Value = 8;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type WaferSize, actual value is '{0}'.", value));
                }
            }
        }

        private IntMemory _destinationStation;
        public DemountCassetteHopper DestinationStation
        {
            get
            {
                switch (_destinationStation.Value)
                {
                    case 0:
                        return DemountCassetteHopper.Cleared;
                    case 1:
                        return DemountCassetteHopper.Hopper1;
                    case 2:
                        return DemountCassetteHopper.Hopper2;
                    case 3:
                        return DemountCassetteHopper.Hopper3;
                    case 4:
                        return DemountCassetteHopper.Hopper4;
                    default:
                        throw new ArgumentOutOfRangeException("_destinationStation.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <0, 4>. Actual value is {0}", _destinationStation.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case DemountCassetteHopper.Cleared:
                        _destinationStation.Value = 0;
                        break;
                    case DemountCassetteHopper.Hopper1:
                        _destinationStation.Value = 1;
                        break;
                    case DemountCassetteHopper.Hopper2:
                        _destinationStation.Value = 2;
                        break;
                    case DemountCassetteHopper.Hopper3:
                        _destinationStation.Value = 3;
                        break;
                    case DemountCassetteHopper.Hopper4:
                        _destinationStation.Value = 4;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type DemountCassetteHopper, actual value is '{0}'.", value));
                }
            }
        }

        private IntMemory _canReadCassetteBarcode;
        public DemountCassetteHopper CanReadCassetteBarcode
        {
            get
            {
                switch (_canReadCassetteBarcode.Value)
                {
                    case 0:
                        return DemountCassetteHopper.Cleared;
                    case 1:
                        return DemountCassetteHopper.Hopper1;
                    case 2:
                        return DemountCassetteHopper.Hopper2;
                    case 3:
                        return DemountCassetteHopper.Hopper3;
                    case 4:
                        return DemountCassetteHopper.Hopper4;
                    default:
                        throw new ArgumentOutOfRangeException("_canReadCassetteBarcode.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <0, 4>. Actual value is {0}", _canReadCassetteBarcode.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case DemountCassetteHopper.Cleared:
                        _canReadCassetteBarcode.Value = 0;
                        break;
                    case DemountCassetteHopper.Hopper1:
                        _canReadCassetteBarcode.Value = 1;
                        break;
                    case DemountCassetteHopper.Hopper2:
                        _canReadCassetteBarcode.Value = 2;
                        break;
                    case DemountCassetteHopper.Hopper3:
                        _canReadCassetteBarcode.Value = 3;
                        break;
                    case DemountCassetteHopper.Hopper4:
                        _canReadCassetteBarcode.Value = 4;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type DemountCassetteHopper, actual value is '{0}'.", value));
                }
            }
        }

        private BoolMemory _isCassetteBarcodeReadedOk;
        public bool IsCassetteBarcodeReadedOk
        {
            get { return _isCassetteBarcodeReadedOk.Value; }
            set { _isCassetteBarcodeReadedOk.Value = value; }
        }

        private BoolMemory _shouldInspectSpatula;
        public bool ShouldInspectSpatula
        {
            get { return _shouldInspectSpatula.Value; }
            set { _shouldInspectSpatula.Value = value; }
        }

        private List<CassetteSensorSimulator> _isCassette;
        public IList<CassetteSensorSimulator> IsCassette
        {
            get { return _isCassette.AsReadOnly(); }
        }

        private IntMemory _state;
        public DemountState State
        {
            get
            {
                switch (_state.Value)
                {
                    case 1:
                        return DemountState.AutoDemount;
                    case 2:
                        return DemountState.Standby;
                    case 3:
                        return DemountState.Stop;
                    case 4:
                        return DemountState.Alarm;
                    default:
                        throw new ArgumentOutOfRangeException("_state.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <1, 4>. Actual value is {0}", _state.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case DemountState.AutoDemount:
                        _state.Value = 1;
                        break;
                    case DemountState.Standby:
                        _state.Value = 2;
                        break;
                    case DemountState.Stop:
                        _state.Value = 3;
                        break;
                    case DemountState.Alarm:
                        _state.Value = 4;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type DemountState, actual value is '{0}'.", value));
                }
            }
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
            State = DemountState.Stop;
        }

        #endregion

        #region private members

        private bool CheckAddress(int address, int length, out int errorCode)
        {
            if ((address < 0x120) || (address > 0x145))
            {
                errorCode = 0x06;
                return false;
            }

            if ((address + (length - 1) < 0x120) || (address + (length - 1) > 0x145))
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
