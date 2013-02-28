namespace EI.Plc
{
    class BoolFromStreamConverter : BaseFromStreamConverter<bool>
    {
        #region BaseFromStreamConverter members

        protected override bool CheckParameter(string stream)
        {
            if (stream.Length < 4)
                throw GetPlcExceptionInvalidLength(4, stream.Length);
            return true;
        }

        protected override bool GetObject(string stream)
        {
            return ParseBool(stream, "Value");
        }

        #endregion
    }
}