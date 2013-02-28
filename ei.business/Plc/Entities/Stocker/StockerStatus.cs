using System;

namespace EI.Business
{
    public class StockerStatus : IStockerStatus
    {
        #region IStockerStatus members

        public bool IsCarrierPlateArrived { get; set; }
        public CarrierPlateRouting CarrierPlateRouting { get; set; }
        public IMagazineChangeRequest MagazineChangeRequest { get; set; }
        public bool IsMagazineChangeStarted { get; set; }
        public bool IsInputMagazineBarcodeOk { get; set; }
        public IMagazineRequest MagazineRequest { get; set; }
        public StockerInventory StockerInventory { get; set; }
        public MagazineSelection MagazineSelection { get; set; }
        public bool IsMagazineArrived { get; set; }

        public override string ToString()
        {
            return string.Format("IsCpArrived:{0},CpRouting:{1},MagChngRqst:{2},IsMagChngStarted:{3},IsInputMagBcOk:{4},MagRqst:{5},StockerInv:{6},MagSel:{7},IsMagArrived:{8}",
                IsCarrierPlateArrived,
                CarrierPlateRouting,
                MagazineChangeRequest.ToString(),
                IsMagazineChangeStarted,
                IsInputMagazineBarcodeOk,
                MagazineRequest.ToString(),
                StockerInventory.ToString(),
                MagazineSelection.ToString(),
                IsMagazineArrived);
        }

        #endregion
    }
}
