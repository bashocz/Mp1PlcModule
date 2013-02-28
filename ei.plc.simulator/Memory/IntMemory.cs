namespace EI.Plc
{
    class IntMemory : BaseMemory
    {
        #region constructors

        public IntMemory(ushort[] memory, int address)
            : base(memory, address, 1) { }

        #endregion

        #region BaseMemory members

        protected override object MemoryToObject(ushort[] memory)
        {
            return (int)memory[0];
        }

        protected override ushort[] ObjectToMemory(object obj)
        {    
            return new ushort[] { (ushort)(int)obj };
        }

        #endregion

        #region value members

        public int Value
        {
            get { return ToObject<int>(); }
            set { ToMemory<int>(value); }
        }

        #endregion
    }
}
