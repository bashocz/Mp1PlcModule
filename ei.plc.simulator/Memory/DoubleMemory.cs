using System;
namespace EI.Plc
{
    class DoubleMemory : BaseMemory
    {
        #region private members

        private readonly double _modifier;

        #endregion

        #region constructors

        public DoubleMemory(ushort[] memory, int address, int decDigits)
            : base(memory, address, 1)
        {
            _modifier = Math.Pow(10, decDigits);
        }

        #endregion

        #region BaseMemory members

        protected override object MemoryToObject(ushort[] memory)
        {
            return ((double)memory[0] / _modifier);
        }

        protected override ushort[] ObjectToMemory(object obj)
        {
            return new ushort[] { (ushort)Math.Round((double)obj * _modifier) };
        }

        #endregion

        #region value members

        public double Value
        {
            get { return ToObject<double>(); }
            set { ToMemory<double>(value); }
        }

        #endregion
    }
}
