using System.Collections.Generic;

namespace EI.Business
{
    public class Magazine : IMagazine
    {
        #region IMagazine members

        public string Id { get; set; }
        public int Capacity { get; set; }

        private List<ICarrierPlate> _plates = new List<ICarrierPlate>();
        public IList<ICarrierPlate> Plates
        {
            get { return _plates; }
        }

        public void NewPlates(ICollection<ICarrierPlate> plates)
        {
            _plates.Clear();
            _plates.AddRange(plates);
        }

        #endregion
    }
}
