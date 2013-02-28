using System;
using System.Globalization;

namespace EI.Business
{
    public class MagazineRequest : IMagazineRequest
    {
        #region IMagazineRequest members

        public WaferSize WaferSize { get; set; }
        public bool IsRequested { get; set; }
        public int PolishLineNumber { get; set; }

        #endregion
    }
}
