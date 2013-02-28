namespace EI.Business
{
    public interface IMountStatus
    {
        MountState State { get; }
        INewLotCassette NewLotCassette { get; }
        bool IsLotDataTimeout { get; }
        INewLotStart NewLotStarted { get; }
        bool IsCarrierPlateArrived { get; }
        bool IsCarrierPlateMountingReady { get; }
        int WaferBreakNumber { get; }
        bool IsMountingErrorCarrierPlate { get; }
        bool IsEndLot { get; }
        bool IsReservationLotCanceled { get; }
    }
}
