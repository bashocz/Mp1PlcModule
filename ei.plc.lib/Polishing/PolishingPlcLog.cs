using System;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    class PolishLinePlcLog : PolishLinePlc, IPolishLinePlc
    {
        #region private members
        private ILogger _logger;
        private string _moduleName = "Mp1Plc";
        #endregion

        #region constructors

        public PolishLinePlcLog(ICommunication client, ILogger logger)
            : base(client) 
        {
            if (logger == null)
                throw new ArgumentNullException(string.Format("{0} wasn't initialized in class {1}.", typeof(ILogger).FullName, this.GetType().FullName));

            _logger = logger;
        }

        #endregion

        #region IPolishLinePlc members

        public new bool IsMagazineArrived()
        {
            _logger.Debug(_moduleName, string.Format("{0}.IsMagazineArrived()", typeof(PolishLinePlc).Name));
            bool ret = base.IsMagazineArrived();
            _logger.Debug(_moduleName, string.Format("{0}.IsMagazineArrived() result:{1}", typeof(PolishLinePlc).Name, ret));

            return ret;
        }

        public new IPolishingShortStatus GetShortStatus()
        {
            _logger.Debug(_moduleName, string.Format("{0}.GetShortStatus()", typeof(PolishLinePlc).Name));
            IPolishingShortStatus status = base.GetShortStatus();
            _logger.Debug(_moduleName, string.Format("{0}.GetShortStatus() result: {1}", typeof(PolishLinePlc).Name, status));
            
            return status;
        }

        public new IMagazine GetMagazine(Polisher polisher)
        {
            _logger.Debug(_moduleName, string.Format("{0}.GetMagazine({1})", typeof(PolishLinePlc).Name, polisher));
            IMagazine ret = base.GetMagazine(polisher);
            _logger.Debug(_moduleName, string.Format("{0}.GetMagazine({1} result: {2})", typeof(PolishLinePlc).Name, polisher, ret.ToString()));

            return ret;
        }

        public new IPolishingFullStatus GetFullStatus()
        {
            _logger.Debug(_moduleName, string.Format("{0}.GetFullStatus()", typeof(PolishLinePlc).Name));
            IPolishingFullStatus ret = base.GetFullStatus();
            _logger.Debug(_moduleName, string.Format("{0}.GetFullStatus() result: {1}", typeof(PolishLinePlc).Name, ret.ToString()));

            return ret;
        }

        public new void ProcessRecipe(IMagazine magazine)
        {
            _logger.Debug(_moduleName, string.Format("{0}.ProcessRecipe({1})", typeof(PolishLinePlc).Name, magazine.ToString()));
            base.ProcessRecipe(magazine);
        }

        public new void WriteBarcodeError(bool error)
        {
            _logger.Debug(_moduleName, string.Format("{0}.WriteBarcodeError({1})", typeof(PolishLinePlc).Name, error));
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