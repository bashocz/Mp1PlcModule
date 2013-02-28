using System;
using EI.Business;
namespace EI.Plc
{
    class DemountPlcLog : DemountPlc, IDemountPlc
    {
        #region private members

        private ILogger _logger;
        private string _moduleName = "Mp1Plc";

        #endregion

        #region constructors

        public DemountPlcLog(ICommunication client, ILogger logger)
            : base(client) 
        {
            if (logger == null)
                throw new ArgumentNullException(string.Format("{0} wasn't initialized in class {1}.", typeof(ILogger).FullName, this.GetType().FullName));

            _logger = logger;
        }

        #endregion

        #region IDemountPlc members

        public new IDemountStatus GetStatus()
        {
            _logger.Debug(_moduleName, string.Format("{0}.GetStatus()", typeof(DemountPlc).Name));
            IDemountStatus ret = base.GetStatus();
            _logger.Debug(_moduleName, string.Format("{0}.GetStatus() result: {1}", typeof(DemountPlc).Name, ret.ToString()));

            return ret;
        }

        public new void CarrierPlateBarcodeSuccesfullyReaded()
        {
            _logger.Debug(_moduleName, string.Format("{0}.CarrierPlateBarcodeSuccesfullyReaded()", typeof(DemountPlc).Name));
            base.CarrierPlateBarcodeSuccesfullyReaded();
        }

        public new void StartDemounting(WaferSize waferSize, int waferCount, DemountCassetteStation station)
        {
            _logger.Debug(_moduleName, string.Format("{0}.StartDemounting({1}, {2}, {3})", typeof(DemountPlc).Name, waferSize, waferCount, station));
            base.StartDemounting(waferSize, waferCount, station);
        }

        public new void CarrierPlateRouting(CarrierPlateRoutingType type)
        {
            _logger.Debug(_moduleName, string.Format("{0}.CarrierPlateRouting({1})", typeof(DemountPlc).Name, type));
            base.CarrierPlateRouting(type);
        }

        public new void RemoveCassette(DemountCassetteStation from)
        {
            _logger.Debug(_moduleName, string.Format("{0}.RemoveCassette({1})", typeof(DemountPlc).Name, from));
            base.RemoveCassette(from);
        }

        public new void LoadCassette(WaferSize waferSize, DemountCassetteHopper destination)
        {
            _logger.Debug(_moduleName, string.Format("{0}.LoadCassette({1}, {2})", typeof(DemountPlc).Name, waferSize, destination));
            base.LoadCassette(waferSize, destination);
        }

        public new void ChangeCassette(DemountCassetteStation from, WaferSize waferSize, DemountCassetteHopper destination)
        {
            _logger.Debug(_moduleName, string.Format("{0}.ChangeCassette({1}, {2}, {3})", typeof(DemountPlc).Name, from, waferSize, destination));
            base.ChangeCassette(from, waferSize, destination);
        }

        public new void CassetteBarcodeSuccesfullyRead()
        {
            _logger.Debug(_moduleName, string.Format("{0}.CassetteBarcodeSuccesfullyRead()", typeof(DemountPlc).Name));
            base.CassetteBarcodeSuccesfullyRead();
        }

        public new void SpatulaInspectionRequired()
        {
            _logger.Debug(_moduleName, string.Format("{0}.SpatulaInspectionRequired()", typeof(DemountPlc).Name));
            base.SpatulaInspectionRequired();
        }

        public new void WriteBarcodeError(bool error)
        {
            _logger.Debug(_moduleName, string.Format("{0}.WriteBarcodeError({1})", typeof(DemountPlc).Name, error));
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