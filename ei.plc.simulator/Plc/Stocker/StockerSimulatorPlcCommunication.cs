using System;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    class StockerSimulatorPlcCommunication : BaseSimulatorPlcCommunication
    {
        #region constructors

        private StockerSimulatorPlcCommunication()
            : base()
        {
            CreateObjects();
        }

        public static StockerSimulatorPlcCommunication Create()
        {
            StockerSimulatorPlcCommunication plc = new StockerSimulatorPlcCommunication();
            plc.InitializeMemory();
            return plc;
        }

        #endregion

        #region data members

        private void CreateObjects()
        {
            _isCarrierPlateArrived = _memory.CreateBoolMemory(0x120, 1);
            _barcodeErrorMemory = _memory.CreateBoolMemory(0x121, 1);
            _carrierPlateRouting = _memory.CreateIntMemory(0x122);
            _isMagazineFull = _memory.CreateBoolMemory(0x123, 1);
            _isOperatorChangeRequest = _memory.CreateBoolMemory(0x124, 1);
            _isMagazineChange = _memory.CreateBoolMemory(0x125, 1);
            _isMagazineChangeStarted = _memory.CreateBoolMemory(0x126, 1);
            _isInputMagazineBarcodeOK = _memory.CreateBoolMemory(0x127, 1);
            _waferSize = _memory.CreateIntMemory(0x130);
            _isMagazineRequested = _memory.CreateBoolMemory(0x131, 1);
            _stockerInventory = _memory.CreateIntMemory(0x132);
            _isMagazineArrived = _memory.CreateBoolMemory(0x133, 1);
            _magazineSelection = _memory.CreateIntMemory(0x134);
        }

        private BoolMemory _isCarrierPlateArrived;
        public bool IsCarrierPlateArrived
        {
            get { return _isCarrierPlateArrived.Value; }
            set { _isCarrierPlateArrived.Value = value; }
        }

        private BoolMemory _barcodeErrorMemory;
        public bool BarcodeError
        {
            get { return _barcodeErrorMemory.Value; }
            set { _barcodeErrorMemory.Value = value; }
        }

        private IntMemory _carrierPlateRouting;
        public CarrierPlateRouting CarrierPlateRouting
        {
            get
            {
                switch (_carrierPlateRouting.Value)
                {
                    case 0:
                        return CarrierPlateRouting.Cleared;
                    case 1:
                        return CarrierPlateRouting.InsertIntoMagazine;
                    case 2:
                        return CarrierPlateRouting.Reject;
                    default:
                        throw new ArgumentOutOfRangeException("_carrierPlateRouting.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <0, 2>. Actual value is {0}", _carrierPlateRouting.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case CarrierPlateRouting.Cleared:
                        _carrierPlateRouting.Value = 0;
                        break;
                    case CarrierPlateRouting.InsertIntoMagazine:
                        _carrierPlateRouting.Value = 1;
                        break;
                    case CarrierPlateRouting.Reject:
                        _carrierPlateRouting.Value = 2;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type CarrierPlateRouting, actual value is '{0}'.", value));
                }
            }
        }

        private BoolMemory _isMagazineFull;
        public bool IsMagazineFull
        {
            get { return _isMagazineFull.Value; }
            set { _isMagazineFull.Value = value; }
        }

        private BoolMemory _isOperatorChangeRequest;
        public bool IsOperatorChangeRequest
        {
            get { return _isOperatorChangeRequest.Value; }
            set { _isOperatorChangeRequest.Value = value; }
        }

        private BoolMemory _isMagazineChange;
        public bool IsMagazineChange
        {
            get { return _isMagazineChange.Value; }
            set { _isMagazineChange.Value = value; }
        }

        private BoolMemory _isMagazineChangeStarted;
        public bool IsMagazineChangeStarted
        {
            get { return _isMagazineChangeStarted.Value; }
            set { _isMagazineChangeStarted.Value = value; }
        }

        private BoolMemory _isInputMagazineBarcodeOK;
        public bool IsInputMagazineBarcodeOK
        {
            get { return _isInputMagazineBarcodeOK.Value; }
            set { _isInputMagazineBarcodeOK.Value = value; }
        }

        private IntMemory _waferSize;
        public WaferSize WaferSize
        {
            get
            {
                switch (_waferSize.Value)
                {
                    case 0:
                        return WaferSize.AnySize;
                    case 6:
                        return WaferSize.Size6Inches;
                    case 8:
                        return WaferSize.Size8Inches;
                    default:
                        throw new ArgumentOutOfRangeException("_waferSize.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer 0, 6 or 8. Actual value is {0}", _waferSize.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case WaferSize.AnySize:
                        _waferSize.Value = 0;
                        break;
                    case WaferSize.Size6Inches:
                        _waferSize.Value = 6;
                        break;
                    case WaferSize.Size8Inches:
                        _waferSize.Value = 8;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type WaferSize, actual value is '{0}'.", value));
                }
            }
        }

        private BoolMemory _isMagazineRequested;
        public bool IsMagazineRequested
        {
            get { return _isMagazineRequested.Value; }
            set { _isMagazineRequested.Value = value; }
        }

        private IntMemory _stockerInventory;
        public StockerInventory StockerInventory
        {
            get
            {
                switch (_stockerInventory.Value)
                {
                    case 0:
                        return StockerInventory.Cleared;
                    case 1:
                        return StockerInventory.SizeAvailable;
                    case 2:
                        return StockerInventory.SizeNotInStocker;
                    default:
                        throw new ArgumentOutOfRangeException("_stockerInventory.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <0, 2>. Actual value is {0}", _stockerInventory.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case StockerInventory.Cleared:
                        _stockerInventory.Value = 0;
                        break;
                    case StockerInventory.SizeAvailable:
                        _stockerInventory.Value = 1;
                        break;
                    case StockerInventory.SizeNotInStocker:
                        _stockerInventory.Value = 2;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type StockerInvertory, actual value is '{0}'.", value));
                }
            }
        }

        private BoolMemory _isMagazineArrived;
        public bool IsMagazineArrived
        {
            get { return _isMagazineArrived.Value; }
            set { _isMagazineArrived.Value = value; }
        }


        private IntMemory _magazineSelection;
        public MagazineSelection Selection
        {
            get
            {
                switch (_magazineSelection.Value)
                {
                    case 0:
                        return MagazineSelection.Cleared;
                    case 1:
                        return MagazineSelection.HasRequestedSize;
                    case 2:
                        return MagazineSelection.DoesNotHaveRequestedSize;
                    default:
                        throw new ArgumentOutOfRangeException("_magazineSelection.Value", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <0, 2>. Actual value is {0}", _magazineSelection.Value));
                }
            }
            set
            {
                switch (value)
                {
                    case MagazineSelection.Cleared:
                        _magazineSelection.Value = 0;
                        break;
                    case MagazineSelection.HasRequestedSize:
                        _magazineSelection.Value = 1;
                        break;
                    case MagazineSelection.DoesNotHaveRequestedSize:
                        _magazineSelection.Value = 2;
                        break;
                    default:
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type MagazineSelection, actual value is '{0}'.", value));
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
            CarrierPlateRouting = CarrierPlateRouting.Cleared;
            WaferSize = WaferSize.AnySize;
            StockerInventory = StockerInventory.Cleared;
            Selection = MagazineSelection.Cleared;
        }

        #endregion

        #region private members

        private bool CheckAddress(int address, int length, out int errorCode)
        {
            if ((address < 0x120) || (address > 0x134))
            {
                errorCode = 0x06;
                return false;
            }

            if ((address + (length - 1) < 0x120) || (address + (length - 1) > 0x134))
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
