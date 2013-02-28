namespace EI.Business
{
    public interface IStockerStatus
    {
        bool IsCarrierPlateArrived { get; }
        CarrierPlateRouting CarrierPlateRouting { get; }
        IMagazineChangeRequest MagazineChangeRequest { get; }
        bool IsMagazineChangeStarted { get; }
        bool IsInputMagazineBarcodeOk { get; }
        IMagazineRequest MagazineRequest { get; }
        StockerInventory StockerInventory { get; }
        MagazineSelection MagazineSelection { get; }
        bool IsMagazineArrived { get; }
    }
}