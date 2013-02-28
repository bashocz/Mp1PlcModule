namespace EI.Plc
{
    class IntToStreamConverter : BaseToStreamConverter<int>
    {
        #region constructors

        public IntToStreamConverter() { }

        #endregion

        #region BaseToStreamConverter members

        protected override bool CheckParameter(int value)
        {
            return true;
        }

        protected override int GetLength(int value)
        {
            return 1;
        }

        protected override string GetStream(int value)
        {
            return IntHexToStream(value, "Value");
        }

        #endregion
    }
}
