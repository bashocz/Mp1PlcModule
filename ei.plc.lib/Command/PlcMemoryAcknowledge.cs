namespace EI.Plc
{
    class PlcMemoryAcknowledge : PlcCommand
    {
        #region constructors

        public PlcMemoryAcknowledge()
            : base(PlcControlChar.ACK) { }

        #endregion

        #region conversion methods

        public override string CommandToString()
        {
            return BeginCharToString() + IdsToString();
        }

        #endregion
    }
}
