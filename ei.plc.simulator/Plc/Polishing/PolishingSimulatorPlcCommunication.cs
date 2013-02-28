using System;
using System.Collections.Generic;
using EI.Business;

namespace EI.Plc
{
    class PolishingSimulatorPlcCommunication : BaseSimulatorPlcCommunication
    {
        #region constructors

        private PolishingSimulatorPlcCommunication()
            : base()
        {
            CreateObjects();
        }

        public static PolishingSimulatorPlcCommunication Create()
        {
            PolishingSimulatorPlcCommunication plc = new PolishingSimulatorPlcCommunication();
            plc.InitializeMemory();
            return plc;
        }

        #endregion

        #region data members

        private void CreateObjects()
        {
            _magazineArrived = _memory.CreateBoolMemory(0x120, 1);
            _barcodeError = _memory.CreateBoolMemory(0x121, 1);
            _magazineId = _memory.CreateStringMemory(0x122, 4);

            _plates = new List<PolishingPlateSimulator>();
            _plates.Add(new PolishingPlateSimulator(_memory, 0));
            _plates.Add(new PolishingPlateSimulator(_memory, 1));
            _plates.Add(new PolishingPlateSimulator(_memory, 2));
            _plates.Add(new PolishingPlateSimulator(_memory, 3));

            _polishers = new List<PolisherStatusSimulator>();
            _polishers.Add(new PolisherStatusSimulator(_memory, 0));
            _polishers.Add(new PolisherStatusSimulator(_memory, 1));
            _polishers.Add(new PolisherStatusSimulator(_memory, 2));
        }

        private BoolMemory _magazineArrived;
        public bool MagazineArrived
        {
            get { return _magazineArrived.Value; }
            set { _magazineArrived.Value = value; }
        }

        private BoolMemory _barcodeError;
        public bool BarcodeError
        {
            get { return _barcodeError.Value; }
            set { _barcodeError.Value = value; }
        }

        private StringMemory _magazineId;
        public string MagazineId
        {
            get { return _magazineId.Value; }
            set { _magazineId.Value = value; }
        }

        private List<PolishingPlateSimulator> _plates;
        public IList<PolishingPlateSimulator> Plates
        {
            get { return _plates.AsReadOnly(); }
        }

        private List<PolisherStatusSimulator> _polishers;
        public IList<PolisherStatusSimulator> Polishers
        {
            get { return _polishers.AsReadOnly(); }
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
            MagazineId = "";
            IList<PolishingPlateSimulator> Plates = new List<PolishingPlateSimulator>();
            Plates.Add(new PolishingPlateSimulator(_memory, 0) { Id = "", Recipe = 1 });
            Plates.Add(new PolishingPlateSimulator(_memory, 1) { Id = "", Recipe = 1 });
            Plates.Add(new PolishingPlateSimulator(_memory, 2) { Id = "", Recipe = 1 });
            Plates.Add(new PolishingPlateSimulator(_memory, 3) { Id = "", Recipe = 1 });
            IList<PolisherStatusSimulator> Polishers = new List<PolisherStatusSimulator>();
            Polishers.Add(new PolisherStatusSimulator(_memory, 0) { State = PolisherState.Pause });
            Polishers.Add(new PolisherStatusSimulator(_memory, 1) { State = PolisherState.Pause });
            Polishers.Add(new PolisherStatusSimulator(_memory, 2) { State = PolisherState.Pause });
        }

        #endregion

        #region private members

        private bool CheckAddress(int address, int length, out int errorCode)
        {
            if ((address < 0x120) || (address > 0x139)
                && ((address < 0x140) || (address > 0x145))
                && ((address < 0x150) || (address > 0x186))
                && ((address < 0x190) || (address > 0x1C6))
                && ((address < 0x1D0) || (address > 0x206)))
            {
                errorCode = 0x06;
                return false;
            }

            if ((address + (length - 1) < 0x120) || (address + (length - 1) > 0x139)
                && ((address + (length - 1) < 0x140) || (address + (length - 1) > 0x145))
                && ((address + (length - 1) < 0x150) || (address + (length - 1) > 0x186))
                && ((address + (length - 1) < 0x190) || (address + (length - 1) > 0x1C6))
                && ((address + (length - 1) < 0x1D0) || (address + (length - 1) > 0x206)))
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