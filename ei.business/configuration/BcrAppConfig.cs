using System;
using System.Configuration;

namespace EI.Business
{
    public class BcrAppConfig : IBcrConfig
    {
        public string BarcodeReaderName
        {
            get { return ConfigurationManager.AppSettings["BARCODE_READER_NAME"]; }
        }

        public int WaitTimeForReadingBc
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["WAIT_TIME_FOR_READING_BC"]); }
        }
        public int LaserOnTime
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["LASER_ON_TIME"]); }
        }

        public IRs232Config Rs232 { get; set; }
    }
}
