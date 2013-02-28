using EI.Business;
namespace EI.Plc
{
    class StockerPlc : BasePlc, IStockerPlc
    {
        #region headAddress constants

        const int haStatus                              = 0x120;
        const int haBarcodeError                        = 0x121;
        const int haCarrierPlateRouting                 = 0x122;
        const int haMagazineChangeFlag                  = 0x125;
        const int haInputMagazineBarcodeOKFlag          = 0x127;
        const int haStockerInventory                    = 0x132;
        const int haMagazineSelection                   = 0x134;
        
        #endregion

        #region constructors

        public StockerPlc(ICommunication client)
            : base(client) { }

        #endregion

        #region IStockerPlc members

        public IStockerStatus GetStatus()
        {
            return ReadMemory<StockerStatusFromStreamConverter, StockerStatus>(haStatus, 22);
        }

        public void AcceptCarrierPlate(CarrierPlateRouting rounting)
        {
            WriteMemory<CarrierPlateRoutingToStreamConverter, CarrierPlateRouting>(haCarrierPlateRouting, rounting);
        }

        public void MagazineChange()
        {
            WriteMemory<BoolToStreamConverter, bool>(haMagazineChangeFlag, true);
        }

        public void MagazineBarcodeSuccesfullyReaded()
        {
            WriteMemory<BoolToStreamConverter, bool>(haInputMagazineBarcodeOKFlag, true);  
        }

        public void SetWaferSizeAvailable(StockerInventory inventory)
        {
            WriteMemory<StockerInventoryToStreamConverter, StockerInventory>(haStockerInventory, inventory);
        }

        public void AcceptMagazine(MagazineSelection selection)
        {
            WriteMemory<MagazineSelectionToStreamConverter, MagazineSelection>(haMagazineSelection, selection);
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
                new PlcAddressRange(0x120, 0x135)
            };
        }

        #endregion
    }
}
