using System;
using EI.Business;
namespace EI.Plc
{
    class StockerPlcLog : StockerPlc, IStockerPlc
    {
        #region private members
        private ILogger _logger;
        private string _moduleName = "Mp1Plc";
        #endregion

        #region constructors

        public StockerPlcLog(ICommunication client, ILogger logger)
            : base(client) 
        {
            if (logger == null)
                throw new ArgumentNullException(string.Format("{0} wasn't initialized in class {1}.", typeof(ILogger).FullName, this.GetType().FullName));

            _logger = logger;
        }

        #endregion

        #region IStockerPlc members

        public new IStockerStatus GetStatus()
        {
            _logger.Debug(_moduleName, string.Format("{0}.GetStatus()", typeof(StockerPlc).Name));
            IStockerStatus stat = base.GetStatus();
            _logger.Debug(_moduleName, string.Format("{0}.GetStatus() result: {1}", typeof(StockerPlc).Name, stat.ToString()));

            return stat;
        }

        public new void AcceptCarrierPlate(CarrierPlateRouting rounting)
        {
            _logger.Debug(_moduleName, string.Format("{0}.AcceptCarrierPlate({1})", typeof(StockerPlc).Name, rounting));
            base.AcceptCarrierPlate(rounting);
        }

        public new void MagazineChange()
        {
            _logger.Debug(_moduleName, string.Format("{0}.MagazineChange()", typeof(StockerPlc).Name));
            base.MagazineChange();
        }

        public new void MagazineBarcodeSuccesfullyReaded()
        {
            _logger.Debug(_moduleName, string.Format("{0}.MagazineBarcodeSuccesfullyReaded()", typeof(StockerPlc).Name));
            base.MagazineBarcodeSuccesfullyReaded();
        }

        public new void SetWaferSizeAvailable(StockerInventory inventory)
        {
            _logger.Debug(_moduleName, string.Format("{0}.SetWaferSizeAvailable({1})", typeof(StockerPlc).Name, inventory));
            base.SetWaferSizeAvailable(inventory);
        }

        public new void AcceptMagazine(MagazineSelection selection)
        {
            _logger.Debug(_moduleName, string.Format("{0}.AcceptMagazine({1})", typeof(StockerPlc).Name, selection));
            base.AcceptMagazine(selection);
        }

        public new void WriteBarcodeError(bool error)
        {
            _logger.Debug(_moduleName, string.Format("{0}.WriteBarcodeError({1})", typeof(StockerPlc).Name, error));
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
