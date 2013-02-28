using System.Collections.Generic;

namespace EI.Business
{
    public class PolishingFullStatus : IPolishingFullStatus
    {
        #region IPolishingFullStatus members

        private List<IPolisherFullStatus> _status = new List<IPolisherFullStatus>();
        public IList<IPolisherFullStatus> Status
        {
            get { return _status.AsReadOnly(); }
        }

        public void NewStatus(ICollection<IPolisherFullStatus> status)
        {
            _status.Clear();
            _status.AddRange(status);
        }

        public override string ToString()
        {
            string ret = "PolishingFullStatus:";
            foreach (IPolisherFullStatus stat in _status)
            {
                ret += "{" + stat.ToString() + "}";
            }

            return ret;
        }

        #endregion
    }
}
