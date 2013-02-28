using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace EI.Plc
{
    class PlcMemory
    {
        #region private fields

        private readonly ushort[] _memory = new ushort[2048];

        #endregion

        #region property members

        public BoolMemory CreateBoolMemory(int address, int type)
        {
            return new BoolMemory(_memory, address, type);
        }

        public DoubleMemory CreateDoubleMemory(int address, int decDigits)
        {
            return new DoubleMemory(_memory, address, decDigits);
        }

        public IntMemory CreateIntMemory(int address)
        {
            return new IntMemory(_memory, address);
        }

        public StringMemory CreateStringMemory(int address, int length)
        {
            return new StringMemory(_memory, address, length);
        }

        #endregion

        #region memory members

        public void WriteMemory(int address, string data)
        {
            int idx = 0;
            while (data.Length > idx)
            {
                _memory[address++] = Convert.ToUInt16(data.Substring(idx + 0, 4), 16);          
                idx += 4;
            }
        }

        public string ReadMemory(int address, int length)
        {
            string result = string.Empty;
            for (int idx = 0; idx < length; idx++)
            {
                result += _memory[address + idx].ToString("X4", CultureInfo.InvariantCulture);
            }
            return result;
        }

        #endregion
    }
}
