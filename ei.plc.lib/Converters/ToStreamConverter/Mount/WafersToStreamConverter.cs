using System.Collections.Generic;
using System.Linq;
using EI.Business;

namespace EI.Plc
{
    class WafersToStreamConverter : BaseToStreamConverter<IList<IWafer>>
    {
        #region constants

        private const int wafersCountLL = 1;
        private const int wafersCountUL = 32;
        private const int cassetteNumberLL = 1;
        private const int cassetteNumberUL = 12;
        private const int slotNumberLL = 1;
        private const int slotNumberUL = 25;

        #endregion

        #region constructors

        public WafersToStreamConverter() { }

        #endregion

        #region conversion methods

        private string WafersToStream(IList<IWafer> wafers)
        {
            string stream = string.Empty;
            foreach (IWafer wafer in wafers)
            {
                stream += IntToStream(wafer.CassetteNumber, "CassetteNumber") + IntToStream(wafer.SlotNumber, "SlotNumber");
            }
            return stream;
        }

        #endregion

        #region BaseToStreamConverter members

        protected override bool CheckParameter(IList<IWafer> wafers)
        {
            if ((wafers.Count() < wafersCountLL) || (wafers.Count() > wafersCountUL))
                throw ThrowPlcExceptionOutOfRangeValue("wafers.Count", wafersCountLL, wafersCountUL, wafers.Count());

            foreach (IWafer wafer in wafers)
            {
                if ((wafer.CassetteNumber < cassetteNumberLL) || (wafer.CassetteNumber > cassetteNumberUL))
                    throw ThrowPlcExceptionOutOfRangeValue("wafer.CassetteNumber", cassetteNumberLL, cassetteNumberUL, wafer.CassetteNumber);

                if ((wafer.SlotNumber < slotNumberLL) || (wafer.SlotNumber > slotNumberUL))
                    throw ThrowPlcExceptionOutOfRangeValue("wafer.SlotNumber", slotNumberLL, slotNumberUL, wafer.SlotNumber);
            }

            return true;
        }

        protected override int GetLength(IList<IWafer> wafers)
        {
            return wafers.Count() * 2;
        }

        protected override string GetStream(IList<IWafer> wafers)
        {
            return WafersToStream(wafers);
        }

        #endregion
    }
}
