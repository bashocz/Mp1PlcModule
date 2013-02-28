using System.Collections.Generic;

namespace EI.Business
{
    public class PolishingShortStatus : IPolishingShortStatus
    {
        #region IPolishingShortStatus members

        public bool IsMagazineArrived { get; set; }

        private List<IPolisherShortStatus> _status = new List<IPolisherShortStatus>();
        public IList<IPolisherShortStatus> Status
        {
            get { return _status.AsReadOnly(); }
        }

        public void NewStatus(ICollection<IPolisherShortStatus> status)
        {
            _status.Clear();
            _status.AddRange(status);
        }

        #endregion

        public override string ToString()
        {
            string ret = "";
            foreach (IPolisherShortStatus stat in _status)
            {
                ret += "{" + stat.ToString() + "}";
            }
            return ret;
        }
    }
}
