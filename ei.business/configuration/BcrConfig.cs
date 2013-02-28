using System;
using System.Configuration;

namespace EI.Business
{
    public class BcrConfig : IBcrConfig
    {
        public string BarcodeReaderName { get; set; }
        public int WaitTimeForReadingBc { get; set; }
        public int LaserOnTime { get; set; }
        public IRs232Config Rs232 { get; set; }

        public override string ToString()
        {
            return string.Format("{0}@[{1}],WT:{2}s,LOT:{3}s", BarcodeReaderName, Rs232.ToString(), WaitTimeForReadingBc, LaserOnTime);
        }
    }
}
