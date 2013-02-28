namespace EI.Business
{
    public class NewLotStart : INewLotStart
    {
        #region INewLotStart members

        public string LotId { get; set; }
        public MountLine Line { get; set; }
        public bool IsLotStarted { get; set; }

        #endregion
    }
}