using System;
using System.Globalization;

namespace EI.Business
{
    public class MountStatus : IMountStatus
    {
        #region IMountStatus members

        public MountState State { get; set; }
        public INewLotCassette NewLotCassette { get; set; }
        public bool IsLotDataTimeout { get; set; }
        public INewLotStart NewLotStarted { get; set; }
        public bool IsCarrierPlateArrived { get; set; }
        public bool IsCarrierPlateMountingReady { get; set; }
        public int WaferBreakNumber { get; set; }
        public bool IsMountingErrorCarrierPlate { get; set; }
        public bool IsEndLot { get; set; }
        public bool IsReservationLotCanceled { get; set; }

        public override string ToString()
        {
            return string.Format("State:{0},NewLotCassette:{1},IsLotDataTimeout:{2},NewLotStarted:{3},IsCpArrived:{4},IsCpMountingReady:{5},WaferBreakNumber:{6},IsMountingErrCp:{7},IsEndLot:{8},IsReservationLotCanceled:{9}",
                State,
                NewLotCassette.CassetteId,
                IsLotDataTimeout,
                NewLotStarted,
                IsCarrierPlateArrived,
                IsCarrierPlateMountingReady,
                WaferBreakNumber,
                IsMountingErrorCarrierPlate,
                IsEndLot,
                IsReservationLotCanceled);
        }

        #endregion
    }
}
