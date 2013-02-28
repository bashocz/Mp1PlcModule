namespace EI.Plc
{
    class ClearingNewLotStartToStreamConverter : BaseToStreamConverter<object>
    {
        #region BaseToStreamConverter members

        protected override bool CheckParameter(object parameter)
        {
            return true;
        }

        protected override int GetLength(object parameter)
        {
            return 9;
        }

        protected override string GetStream(object parameter)
        {
            return "000000000000000000000000000000000000";
        }

        #endregion
    }
}
