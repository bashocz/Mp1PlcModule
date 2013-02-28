using System;
using System.Globalization;

namespace EI.Plc
{
    class StringMemory : BaseMemory
    {
        #region constructors

        public StringMemory(ushort[] memory, int address, int length)
            : base(memory, address, length) { }

        #endregion

        #region BaseMemory members

        protected override object MemoryToObject(ushort[] memory)
        {
            byte[] bytes = new byte[memory.Length * sizeof(ushort)];
            Buffer.BlockCopy(memory, 0, bytes, 0, memory.Length * sizeof(ushort));

            string result = string.Empty;
            foreach (byte _byte in bytes)
            {
                char character = Convert.ToChar(_byte);
                if (character == '\0')
                    return result;
                result += character.ToString();
            }
            return result;
        }

        protected override ushort[] ObjectToMemory(object obj)
        {
            string str = (string)obj;
            if (string.IsNullOrEmpty(str))
                return new ushort[_length];

            byte[] bytes = new byte[str.Length];
            for (int idx = 0; idx < str.Length; idx++)
            {
                bytes[idx] = (byte)Convert.ToChar(str.Substring(idx, 1), CultureInfo.InvariantCulture);
            }

            ushort[] result = new ushort[_length];
            Buffer.BlockCopy(bytes, 0, result, 0, bytes.Length);
            return result;
        }

        #endregion

        #region value members

        public string Value
        {
            get { return ToObject<string>(); }
            set { ToMemory<string>(value); }
        }

        #endregion
    }
}
