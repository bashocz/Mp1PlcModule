using System;

namespace EI.Business
{
    public class MagazineChangeRequest : IMagazineChangeRequest
    {
        #region IMagazineChangeRequest members

        public bool IsMagazineFull { get; set; }
        public bool IsOperatorChangeRequest { get; set; }

        #endregion
    }
}
