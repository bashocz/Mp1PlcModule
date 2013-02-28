using System;
using System.Globalization;

namespace EI.Business
{
    public class PolisherShortStatus : IPolisherShortStatus
    {
        public PolisherState State { get; set; }
        public bool HighPressure { get; set; }

        public override string ToString()
        {
            return string.Format("State:{0},HighPressure:{1}", State, HighPressure);
        }
    }
}
