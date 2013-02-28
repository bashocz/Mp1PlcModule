using System.Collections.Generic;

namespace EI.Business
{
    public class DemountStatus : IDemountStatus
    {
        #region IDemountStatus members

        public bool IsCarrierPlateArrived { get; set; }
        public bool IsCarrierPlateDemountStarted { get; set; }
        public IDemountInfo DemountInfo { get; set; }
        public DemountCassetteHopper CanReadCassetteBarcode { get; set; }
        public DemountState State { get; set; }

        private List<bool> _areCassettes = new List<bool>();
        public IList<bool> AreCassettes
        {
            get { return _areCassettes; }
        }

        public void NewAreCassettes(ICollection<bool> areCassettes)
        {
            _areCassettes.Clear();
            _areCassettes.AddRange(areCassettes);
        }

        #endregion

        public override string ToString()
        {
            string areCassToString = "{";
            foreach (bool cas in _areCassettes)
            {
                areCassToString += string.Format("{0},", cas);
            }
            areCassToString += "}";

            return string.Format("IsCpArrived:{0},IsCpDmntStarted:{1},DmntInfo:{2},CanReadCassBc:{3},State:{4},AreCassettes:{5}",
                IsCarrierPlateArrived,
                IsCarrierPlateDemountStarted,
                DemountInfo.ToString(),
                CanReadCassetteBarcode,
                State,
                areCassToString);
        }
    }
}