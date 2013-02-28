namespace EI.Business
{
    public class NewLotCassette : INewLotCassette
    {
        #region INewLotCassette members

        public bool IsCassetteId { get; set; }
        public string CassetteId { get; set; }

        #endregion
    }
}