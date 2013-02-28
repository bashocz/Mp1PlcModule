using System;
using System.Globalization;
using EI.Business;

namespace EI.Plc
{
    class PolishLinePlc : BasePlc, IPolishLinePlc
    {
        #region headAddress constants

        const int haMagazineArrivalFlag = 0x120;
        const int haBarcodeError = 0x121;
        const int haProcessRecipe = 0x122;
        const int haShortStatus = 0x140;
        const int haMachinesStatus = 0x150;

        #endregion

        #region constructors

        public PolishLinePlc(ICommunication client)
            : base(client) { }

        #endregion

        #region conversion methods

        public int ConvertPolisherToInt(Polisher polisher)
        {
            switch (polisher)
            {
                case Polisher.Polisher1:
                    return 0;
                case Polisher.Polisher2:
                    return 1;
                case Polisher.Polisher3:
                    return 2;
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument does not match enumeration type Polisher, actual value is '{0}'.", polisher));
            }
        }

        #endregion

        #region IPolishLinePlc members

        public bool IsMagazineArrived()
        {
            return ReadMemory<BoolFromStreamConverter, bool>(haMagazineArrivalFlag, 1);
        }

        public IPolishingShortStatus GetShortStatus()
        {
            PolishingShortStatus status = ReadMemory<PolishingStatusFromStreamConverter, PolishingShortStatus>(haShortStatus, 6);
            status.IsMagazineArrived = ReadMemory<BoolFromStreamConverter, bool>(haMagazineArrivalFlag, 1);

            return status;
        }

        public IMagazine GetMagazine(Polisher polisher)
        {
            return ReadMemory<MagazineFromStreamConverter, Magazine>(haMachinesStatus + ConvertPolisherToInt(polisher) * 64, 20);
        }

        public IPolishingFullStatus GetFullStatus()
        {
            PolisherFullStatus[] statuses = new PolisherFullStatus[3];
            for (int idx = 0; idx < 3; idx++)
                statuses[idx] = ReadMemory<PolisherStatusFromStreamConverter, PolisherFullStatus>(haMachinesStatus + idx * 64, 55);
            PolishingShortStatus status = ReadMemory<PolishingStatusFromStreamConverter, PolishingShortStatus>(haShortStatus, 6);
            for (int idx = 0; idx < 3; idx++)
            {
                statuses[idx].HighPressure = status.Status[idx].HighPressure;
                statuses[idx].State = status.Status[idx].State;
            }
            PolishingFullStatus result = new PolishingFullStatus();
            result.NewStatus(statuses);
            return result;
        }

        public void ProcessRecipe(IMagazine magazine)
        {
            WriteMemory<ProcessRecipeToStreamConverter, IMagazine>(haProcessRecipe, magazine);
        }

        public void WriteBarcodeError(bool error)
        {
            WriteMemory<BoolToStreamConverter, bool>(haBarcodeError, error);
        }

        #endregion

        #region IPlcAddressSpace members

        protected override PlcAddressRange[] GetAddressRanges()
        {
            return new PlcAddressRange[]{
                new PlcAddressRange(0x120, 0x139),
                new PlcAddressRange(0x140, 0x145),

                new PlcAddressRange(0x150, 0x186),
                new PlcAddressRange(0x190, 0x1C6),
                new PlcAddressRange(0x1D0, 0x206)
            };
        }

        #endregion
    }
}