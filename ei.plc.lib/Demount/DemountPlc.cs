using EI.Business;
namespace EI.Plc
{
    class DemountPlc : BasePlc, IDemountPlc
    {
        #region headAddress constants

        const int haStatus = 0x120;
        const int haBarcodeError = 0x121;
        const int haIsCarrierPlateBarcodeReadedOk = 0x122;

        const int haStartDemounting = 0x124;
        const int haCarrierPlateRouting = 0x129;
        const int haRemoveCassette = 0x130;
        const int haLoadCassette = 0x131;

        const int haIsCassetteBarcodeReadedOk = 0x134;
        const int haSpatulaCheckFlag = 0x140;

        #endregion

        #region constructors

        public DemountPlc(ICommunication client)
            : base(client) { }

        #endregion

        #region IDemountPlc members

        public IDemountStatus GetStatus()
        {
            return ReadMemory<DemountStatusFromStreamConverter, DemountStatus>(haStatus, 38);
        }

        public void CarrierPlateBarcodeSuccesfullyReaded()
        {
            WriteMemory<BoolToStreamConverter, bool>(haIsCarrierPlateBarcodeReadedOk, true);
        }

        public void StartDemounting(WaferSize waferSize, int waferCount, DemountCassetteStation station)
        {
            WriteMemory<StartDemountingToStreamConverter, StartDemounting>(haStartDemounting, new StartDemounting { Size = waferSize, Count = waferCount, Station = station });
        }

        public void CarrierPlateRouting(CarrierPlateRoutingType type)
        {
            WriteMemory<EmptyCarrierPlateRoutingToStreamConverter, CarrierPlateRoutingType>(haCarrierPlateRouting, type);
        }

        public void RemoveCassette(DemountCassetteStation from)
        {
            WriteMemory<RemoveCassetteToStreamConverter, DemountCassetteStation>(haRemoveCassette, from);
        }

        public void LoadCassette(WaferSize waferSize, DemountCassetteHopper destination)
        {
            WriteMemory<LoadCassetteToStreamConverter, LoadCassette>(haLoadCassette, new LoadCassette { WaferSize = waferSize, Destination = destination });
        }

        public void ChangeCassette(DemountCassetteStation from, WaferSize waferSize, DemountCassetteHopper destination)
        {
            WriteMemory<ChangeCassetteToStreamConverter, ChangeCassette>(haRemoveCassette, new ChangeCassette { Source = from, WaferSize = waferSize, Destination = destination });
        }

        public void CassetteBarcodeSuccesfullyRead()
        {
            WriteMemory<BoolToStreamConverter, bool>(haIsCassetteBarcodeReadedOk, true);
        }

        public void SpatulaInspectionRequired()
        {
            WriteMemory<BoolToStreamConverter, bool>(haSpatulaCheckFlag, true);
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
                new PlcAddressRange(0x120, 0x145)
            };
        }

        #endregion
    }
}