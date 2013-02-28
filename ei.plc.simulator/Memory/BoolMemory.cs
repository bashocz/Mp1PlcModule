using System;
using System.Globalization;
namespace EI.Plc
{
    class BoolMemory : BaseMemory
    {
        #region private members

        private int _type;

        #endregion

        #region constructors

        public BoolMemory(ushort[] memory, int address, int type)
            : base(memory, address, 1)
        {
            _type = type;
        }

        #endregion

        #region BaseMemory members

        protected override object MemoryToObject(ushort[] memory)
        {
            if (_type == 1)
            {
                if (memory[0] == 0)
                    return false;
                else if (memory[0] == 1)
                    return true;
                throw new ArgumentOutOfRangeException("memory", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <0, 1>. Actual value is {0}.", memory[0]));
            }
            else
            {
                if (memory[0] == 2)
                    return false;
                else if (memory[0] == 1)
                    return true;
                throw new ArgumentOutOfRangeException("memory", string.Format(CultureInfo.InvariantCulture, "Argument is expected as integer in range <1, 2>. Actual value is {0}.", memory[0]));
            }
        }

        protected override ushort[] ObjectToMemory(object obj)
        {
            if (_type == 1)
            {
                if ((bool)obj == false)
                    return new ushort[] { 0 };
                else if ((bool)obj == true)
                    return new ushort[] { 1 };
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument is expected as boolean value. Actual value is {0}.", (bool)obj), "obj");
            }
            else
            {
                if ((bool)obj == false)
                    return new ushort[] { 2 };
                else if ((bool)obj == true)
                    return new ushort[] { 1 };
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument is expected as boolean value. Actual value is {0}.", (bool)obj), "obj");
            }
        }

        #endregion

        #region value members

        public bool Value
        {
            get { return ToObject<bool>(); }
            set { ToMemory<bool>(value); }
        }

        #endregion
    }
}
