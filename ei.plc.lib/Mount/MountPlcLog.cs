using System.Collections.Generic;
using System.Linq;
using System;
using EI.Business;

namespace EI.Plc
{
    class MountPlcLog : MountPlc, IMountPlc
    {
        #region private members
        private ILogger _logger;
        private string _moduleName = "Mp1Plc";
        #endregion

        #region constructors

        public MountPlcLog(ICommunication client, ILogger logger) 
            : base(client) 
        {
            if (logger == null)
                throw new ArgumentNullException(string.Format("{0} wasn't initialized in class {1}.", typeof(ILogger).FullName, this.GetType().FullName));

            _logger = logger;
        }

        #endregion

        #region IMountPlc members

        public new IMountStatus GetStatus()
        {
            _logger.Debug(_moduleName, string.Format("{0}.GetStatus()", typeof(MountPlc).Name));
            IMountStatus stat = base.GetStatus();
            _logger.Debug(_moduleName, string.Format("{0}.GetStatus() result:{1}", typeof(MountPlc).Name, stat.ToString()));

            return stat;
        }

        public new void AcceptNewLot(bool accepted)
        {
            _logger.Debug(_moduleName, string.Format("{0}.AcceptNewLot({1})", typeof(MountPlc).Name, accepted));
            base.AcceptNewLot(accepted);
        }

        public new void SetLotData(ILotData lotData)
        {
            _logger.Debug(_moduleName, string.Format("{0}.SetLotData({1})", typeof(MountPlc).Name, (lotData != null) ? lotData.ToString() : "null"));
            base.SetLotData(lotData);
        }

        public new void CarrierPlateBarcodeSuccesfullyReaded()
        {
            _logger.Debug(_moduleName, string.Format("{0}.CarrierPlateBarcodeSuccesfullyReaded()", typeof(MountPlc).Name));
            base.CarrierPlateBarcodeSuccesfullyReaded();
        }

        public new void AcceptWaferBreakNumber()
        {
            _logger.Debug(_moduleName, string.Format("{0}.AcceptWaferBreakNumber()", typeof(MountPlc).Name));
            base.AcceptWaferBreakNumber();
        }

        public new void ClearNewLotStartData()
        {
            _logger.Debug(_moduleName, string.Format("{0}.ClearNewLotStartData()", typeof(MountPlc).Name));
            base.ClearNewLotStartData();
        }

        public new void ClearCarrierPlateMountingReadyFlag()
        {
            _logger.Debug(_moduleName, string.Format("{0}.ClearCarrierPlateMountingReadyFlag()", typeof(MountPlc).Name));
            base.ClearCarrierPlateMountingReadyFlag();
        }

        public new void ClearMountingErrorCarrierPlateFlag()
        {
            _logger.Debug(_moduleName, string.Format("{0}.ClearMountingErrorCarrierPlateFlag()", typeof(MountPlc).Name));
            base.ClearMountingErrorCarrierPlateFlag();
        }

        public new void ClearLotEndFlag()
        {
            _logger.Debug(_moduleName, string.Format("{0}.ClearLotEndFlag()", typeof(MountPlc).Name));
            base.ClearLotEndFlag();
        }

        public new void ClearReservationLotCancelFlag()
        {
            _logger.Debug(_moduleName, string.Format("{0}.ClearReservationLotCancelFlag()", typeof(MountPlc).Name));
            base.ClearReservationLotCancelFlag();
        }

        public new void WriteBarcodeError(bool error)
        {
            _logger.Debug(_moduleName, string.Format("{0}.WriteBarcodeError({1})", typeof(MountPlc).Name, error));
            base.WriteBarcodeError(error);
        }

        #endregion

        #region IBasePlc members
        public new bool Open()
        {
            _logger.Debug(_moduleName, string.Format("{0}.Open()", typeof(BasePlc).Name));
            bool ret = base.Open();
            _logger.Debug(_moduleName, string.Format("{0}.Open() result: {1}", typeof(BasePlc).Name, ret));

            return ret;
        }

        public new void Close()
        {
            _logger.Debug(_moduleName, string.Format("{0}.Close()", typeof(BasePlc).Name));
            base.Close();
        }
        #endregion
    }
}