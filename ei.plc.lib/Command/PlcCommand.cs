namespace EI.Plc
{
    abstract class PlcCommand : IPlcCommand
    {
        #region data members

        private readonly int stationId = 0;
        private readonly int pcId = 255;
        private readonly int messageWaitTime = 0;

        public PlcControlChar BeginChar { get; private set; }

        #endregion

        #region constructors

        public PlcCommand(PlcControlChar controlChar)
        {
            BeginChar = controlChar;
        }

        #endregion

        #region conversion methods

        public abstract string CommandToString();

        protected string BeginCharToString()
        {
            return PlcStream.ControlCharToString(BeginChar);
        }

        protected string IdsToString()
        {
            return PlcStream.IdsToString(stationId, pcId);
        }

        protected string MessageWaitTimeToString()
        {
            return PlcStream.WaitMsToString(messageWaitTime);
        }

        #endregion
    }
}
