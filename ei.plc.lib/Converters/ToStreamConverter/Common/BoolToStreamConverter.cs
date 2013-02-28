namespace EI.Plc
{
    class BoolToStreamConverter : BaseToStreamConverter<bool>
    {
        #region constructors

        public BoolToStreamConverter() { }

        #endregion

        #region BaseToStreamConverter members

        protected override bool CheckParameter(bool value)
        {
            return true;
        }

        protected override int GetLength(bool value)
        {
            return 1;
        }

        protected override string GetStream(bool value)
        {
            return BoolToStream(value, "Value");
        }

        #endregion
    }
}