using System;

namespace EI.Plc
{
    abstract class BaseMemory
    {
        #region protected members

        private readonly int _address;
        protected readonly int _length;

        private readonly ushort[] _memory;

        #endregion

        #region constructors

        public BaseMemory(ushort[] memory, int address, int length)
        {
            _memory = memory;
            _address = address;
            _length = length;
        }

        #endregion

        #region memory operation

        private ushort[] ReadMemory()
        {
            ushort[] memorySegment = new ushort[_length];
            Buffer.BlockCopy(_memory, _address * sizeof(ushort), memorySegment, 0, _length * sizeof(ushort));
            return memorySegment;
        }

        private void WriteMemory(ushort[] memorySegment)
        {
            Buffer.BlockCopy(memorySegment, 0, _memory, _address * sizeof(ushort), memorySegment.Length * sizeof(ushort));
        }

        protected abstract object MemoryToObject(ushort[] memory);
        protected abstract ushort[] ObjectToMemory(object obj);

        protected T ToObject<T>()
        {
            return (T)MemoryToObject(ReadMemory());
        }

        protected void ToMemory<T>(T value)
        {
            WriteMemory(ObjectToMemory(value));
        }

        #endregion
    }
}
