using EI.Business;
namespace EI.Plc
{
    public interface IMountPlc : IBasePlc
    {
        IMountStatus GetStatus();
        void AcceptNewLot(bool accepted);
        void SetLotData(ILotData lot);
        void CarrierPlateBarcodeSuccesfullyReaded();
        void AcceptWaferBreakNumber();
        void ClearNewLotStartData();
        void ClearCarrierPlateMountingReadyFlag();
        void ClearMountingErrorCarrierPlateFlag();
        void ClearLotEndFlag();
        void ClearReservationLotCancelFlag();
        void WriteBarcodeError(bool error);
    }
}
