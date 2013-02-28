using System.Collections.Generic;
using System.Linq;
using EI.Business;

namespace EI.Plc
{
    class MountPlc : BasePlc, IMountPlc
    {
        #region headAddress constants

        const int haStatus                        = 0x120;
        const int haIsCassetteIdError             = 0x125;
        const int haNewLotStartData               = 0x127;
        const int haIsCarrierPlateBarcodeReadedOk = 0x131;
        const int haIsCarrierPlateMountingReady   = 0x132;
        const int haIsWaferBreakInfoOk            = 0x134;
        const int haIsMountingCarrierPlateError   = 0x135;
        const int haIsLotEnded                    = 0x136;
        const int haIsReservationLotCanceled      = 0x137;
        const int haIsInformationSystemError      = 0x141;
        const int haLotCassetteData               = 0x150;
        const int haLotDataTransmission           = 0x188;
        const int haLotWafferData                 = 0x189;
        const int haWafers                        = 0x200;

        #endregion

        #region constructors

        public MountPlc(ICommunication client)
            : base(client) { }

        #endregion

        #region IMountPlc members

        public IMountStatus GetStatus()
        {
            return ReadMemory<MountStatusFromStreamConverter, MountStatus>(haStatus, 33);
        }

        public void AcceptNewLot(bool accepted)
        {
            if (!accepted)
                WriteMemory<BoolToStreamConverter, bool>(haIsCassetteIdError, true);
        }

        public void SetLotData(ILotData lotData)
        {
            WriteMemory<LotDataTransmissionToStreamConverter, LotDataTransmission>(haLotDataTransmission, LotDataTransmission.BeforeWritingCassetteInfo);
            WriteMemory<LotCassetteInfoToStreamConverter, ILotData>(haLotCassetteData, lotData);
            WriteMemory<LotDataTransmissionToStreamConverter, LotDataTransmission>(haLotDataTransmission, LotDataTransmission.BeforeWritingWaferInfo);
            WriteMemory<LotWaferInfoToStreamConverter, ILotData>(haLotWafferData, lotData);

            int writeCommandCount;
            if ((lotData.Wafers.Count % 32) == 0)
                writeCommandCount = lotData.Wafers.Count / 32;
            else
                writeCommandCount = lotData.Wafers.Count / 32 + 1;

            for (int i = 0; i < writeCommandCount; i++)
            {
                WriteMemory<WafersToStreamConverter, IList<IWafer>>(haWafers + i * 64, lotData.Wafers.Skip(i * 32).Take(32).ToList());
            }

            WriteMemory<LotDataTransmissionToStreamConverter, LotDataTransmission>(haLotDataTransmission, LotDataTransmission.Cleared);
        }

        public void CarrierPlateBarcodeSuccesfullyReaded()
        {
            WriteMemory<BoolToStreamConverter, bool>(haIsCarrierPlateBarcodeReadedOk, true);
        }

        public void AcceptWaferBreakNumber()
        {
            WriteMemory<BoolToStreamConverter, bool>(haIsWaferBreakInfoOk, true);
        }

        public void ClearNewLotStartData()
        {
            WriteMemory<ClearingNewLotStartToStreamConverter, object>(haNewLotStartData, this);
        }

        public void ClearCarrierPlateMountingReadyFlag()
        {
            WriteMemory<BoolToStreamConverter, bool>(haIsCarrierPlateMountingReady, false);
        }

        public void ClearMountingErrorCarrierPlateFlag()
        {
            WriteMemory<BoolToStreamConverter, bool>(haIsMountingCarrierPlateError, false);
        }

        public void ClearLotEndFlag()
        {
            WriteMemory<BoolToStreamConverter, bool>(haIsLotEnded, false);
        }

        public void ClearReservationLotCancelFlag()
        {
            WriteMemory<BoolToStreamConverter, bool>(haIsReservationLotCanceled, false);
        }

        public void WriteBarcodeError(bool error)
        {
            WriteMemory<BoolToStreamConverter, bool>(haIsInformationSystemError, error);
        }

        #endregion

        #region IPlcAddressSpace members

        protected override PlcAddressRange[] GetAddressRanges()
        {
            return new PlcAddressRange[]{
                new PlcAddressRange(0x120, 0x141),

                new PlcAddressRange(0x150, 0x191),

                new PlcAddressRange(0x200, 0x457)
            };
        }

        #endregion
    }
}