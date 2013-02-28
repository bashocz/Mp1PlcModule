namespace EI.Plc
{
    class CassetteSimulator
    {
        #region private fields

        private readonly PlcMemory _memory;
        private int _offsetCassette;

        #endregion

        #region constructors

        public CassetteSimulator(PlcMemory memory, int cassetteNumber)
        {
            _memory = memory;
            _offsetCassette = cassetteNumber;
            CreateObjects();
        }

        #endregion

        #region data members

        private void CreateObjects()
        {
            _id = _memory.CreateStringMemory(0x157 + (_offsetCassette * 4), 4);
        }

        private StringMemory _id;
        public string Id
        {
            get { return _id.Value; }
            set { _id.Value = value; }
        }

        #endregion
    }
}
