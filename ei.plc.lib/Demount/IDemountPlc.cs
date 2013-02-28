using EI.Business;
namespace EI.Plc
{
    public interface IDemountPlc : IBasePlc
    {
        IDemountStatus GetStatus();
        void CarrierPlateBarcodeSuccesfullyReaded();
        void StartDemounting(WaferSize waferSize, int waferCount, DemountCassetteStation station);
        void CarrierPlateRouting(CarrierPlateRoutingType type);
        void RemoveCassette(DemountCassetteStation from);
        void LoadCassette(WaferSize waferSize, DemountCassetteHopper destination);
        void ChangeCassette(DemountCassetteStation from, WaferSize waferSize, DemountCassetteHopper destination);
        void CassetteBarcodeSuccesfullyRead();
        void SpatulaInspectionRequired();
        void WriteBarcodeError(bool error);
    }
}
