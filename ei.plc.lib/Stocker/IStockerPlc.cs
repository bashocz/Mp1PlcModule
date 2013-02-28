using EI.Business;
namespace EI.Plc
{
    public interface IStockerPlc : IBasePlc
    {
        IStockerStatus GetStatus();
        void AcceptCarrierPlate(CarrierPlateRouting rounting);
        void MagazineChange();
        void MagazineBarcodeSuccesfullyReaded();
        void SetWaferSizeAvailable(StockerInventory inventory);
        void AcceptMagazine(MagazineSelection selection);
        void WriteBarcodeError(bool error);
    }
}
