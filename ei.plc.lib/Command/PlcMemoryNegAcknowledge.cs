namespace EI.Plc
{
    class PlcMemoryNegAcknowledge : PlcCommand
    {
        #region constructors

        public PlcMemoryNegAcknowledge()
            : base(PlcControlChar.NAK) { }

        #endregion

        #region conversion methods

        public override string CommandToString()
        {
            return BeginCharToString() + IdsToString();
        }

        #endregion
    }
}
