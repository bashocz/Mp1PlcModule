namespace EI.Plc
{
    class PolishingPlateSimulator
    {
        #region private fields

        private readonly PlcMemory _memory;
        private int _offsetPlate;

        #endregion

        #region constructors

        public PolishingPlateSimulator(PlcMemory memory, int plateNumber)
        {
            _memory = memory;
            _offsetPlate = plateNumber;
            CreateObjects();
        }

        #endregion

        #region data members

        private void CreateObjects()
        {
            _id = _memory.CreateStringMemory(0x126 + (_offsetPlate * 4), 4);
            _recipe = _memory.CreateIntMemory(0x136 + _offsetPlate);
        }

        private StringMemory _id;
        public string Id
        {
            get { return _id.Value; }
            set { _id.Value = value; }
        }

        private IntMemory _recipe;
        public int Recipe
        {
            get { return _recipe.Value; }
            set { _recipe.Value = value; }
        }

        #endregion
    }
}
