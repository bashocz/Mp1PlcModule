using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class BaseMemoryTest
    {
        #region constructor tests

        [TestMethod()]
        public void Ctor1Test()
        {
            PlcMemory_Accessor memory = new PlcMemory_Accessor();
            memory.WriteMemory(0x5, "56ABABCD");

            Mock<BaseMemory> privateTarget = new Mock<BaseMemory>(memory._memory, 0x5, 2);
            BaseMemory_Accessor target = new BaseMemory_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseMemory))));

            Assert.AreEqual<ushort>(target._memory[0x5], 0x56AB);
            Assert.AreEqual<ushort>(target._memory[0x6], 0xABCD);
            Assert.AreEqual<int>(target._address, 0x5);
            Assert.AreEqual<int>(target._length, 2);
        }

        [TestMethod()]
        public void Ctor2Test()
        {
            PlcMemory_Accessor memory = new PlcMemory_Accessor();
            memory.WriteMemory(0x7EE, "0123456789ABCDEF");

            Mock<BaseMemory> privateTarget = new Mock<BaseMemory>(memory._memory, 0x7EE, 4);
            BaseMemory_Accessor target = new BaseMemory_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseMemory))));

            Assert.AreEqual<ushort>(target._memory[0x7EE], 0x0123);
            Assert.AreEqual<ushort>(target._memory[0x7EF], 0x4567);
            Assert.AreEqual<ushort>(target._memory[0x7F0], 0x89AB);
            Assert.AreEqual<ushort>(target._memory[0x7F1], 0xCDEF);
            Assert.AreEqual<int>(target._address, 0x7EE);
            Assert.AreEqual<int>(target._length, 4);
        }

        #endregion

        #region ReadMemory method tests

        [TestMethod()]
        public void ReadMemory1Test()
        {
            PlcMemory_Accessor memory = new PlcMemory_Accessor();
            memory.WriteMemory(0x5, "56ABABCD");

            Mock<BaseMemory> privateTarget = new Mock<BaseMemory>(memory._memory, 0x6, 1);
            BaseMemory_Accessor target = new BaseMemory_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseMemory))));

            ushort[] array = target.ReadMemory();
            Assert.AreEqual<ushort>(0xABCD, array[0x0]);
            Assert.AreEqual<int>(1, array.Length);
        }

        [TestMethod()]
        public void ReadMemory2Test()
        {
            PlcMemory_Accessor memory = new PlcMemory_Accessor();
            memory.WriteMemory(0x7EE, "0123456789ABCDEF");

            Mock<BaseMemory> privateTarget = new Mock<BaseMemory>(memory._memory, 0x7EF, 2);
            BaseMemory_Accessor target = new BaseMemory_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseMemory))));

            ushort[] array = target.ReadMemory();
            Assert.AreEqual<ushort>(0x4567, array[0x0]);
            Assert.AreEqual<ushort>(0x89AB, array[0x1]);
            Assert.AreEqual<int>(2, array.Length);
        }

        #endregion

        #region WriteMemory method tests

        [TestMethod()]
        public void WriteMemory1Test()
        {
            PlcMemory_Accessor memory = new PlcMemory_Accessor();

            Mock<BaseMemory> privateTarget = new Mock<BaseMemory>(memory._memory, 0x5, 4);
            BaseMemory_Accessor target = new BaseMemory_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseMemory))));

            ushort[] array = new ushort[] { 0x4567, 0x89AB, 0xCDEF, 0x1234 };
            target.WriteMemory(array);

            Assert.AreEqual<ushort>(0x4567, target._memory[0x5]);
            Assert.AreEqual<ushort>(0x89AB, target._memory[0x6]);
            Assert.AreEqual<ushort>(0xCDEF, target._memory[0x7]);
            Assert.AreEqual<ushort>(0x1234, target._memory[0x8]);
        }

        [TestMethod()]
        public void WriteMemory2Test()
        {
            PlcMemory_Accessor memory = new PlcMemory_Accessor();

            Mock<BaseMemory> privateTarget = new Mock<BaseMemory>(memory._memory, 0x2, 2);
            BaseMemory_Accessor target = new BaseMemory_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseMemory))));

            ushort[] array = new ushort[] { 0xCDEF, 0x1234 };
            target.WriteMemory(array);

            Assert.AreEqual<ushort>(0xCDEF, target._memory[0x2]);
            Assert.AreEqual<ushort>(0x1234, target._memory[0x3]);
        }

        #endregion

        #region ToObject method tests

        [TestMethod()]
        public void ToObject1Test()
        {
            // int memory example
            PlcMemory_Accessor memory = new PlcMemory_Accessor();
            memory.WriteMemory(0x5, "3BEE");

            Mock<BaseMemory> privateTarget = new Mock<BaseMemory>(memory._memory, 0x5, 1);
            privateTarget.Protected().Setup<object>("MemoryToObject", ItExpr.IsAny<ushort[]>()).Returns(new ushort[] { 0x3BEE });
            BaseMemory_Accessor target = new BaseMemory_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseMemory))));

            Assert.AreEqual<int>(0x3BEE, (int)((ushort[])target.ToObject<object>())[0]);
        }

        [TestMethod()]
        public void ToObject2Test()
        {
            // bool memory example
            PlcMemory_Accessor memory = new PlcMemory_Accessor();
            memory.WriteMemory(0x5, "0001");

            Mock<BaseMemory> privateTarget = new Mock<BaseMemory>(memory._memory, 0x5, 1);
            privateTarget.Protected().Setup<object>("MemoryToObject", ItExpr.IsAny<ushort[]>()).Returns(true);
            BaseMemory_Accessor target = new BaseMemory_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseMemory))));

            Assert.AreEqual<bool>(true, (bool)target.ToObject<object>());
        }

        #endregion

        #region ToMemory method tests

        [TestMethod()]
        public void ToMemory1Test()
        {
            // bool memory example
            PlcMemory_Accessor memory = new PlcMemory_Accessor();

            Mock<BaseMemory> privateTarget = new Mock<BaseMemory>(memory._memory, 0x5, 1);
            privateTarget.Protected().Setup<ushort[]>("ObjectToMemory", ItExpr.IsAny<object>()).Returns(new ushort[] { 0x0001 });
            BaseMemory_Accessor target = new BaseMemory_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseMemory))));

            target.ToMemory<object>(true);
            Assert.AreEqual<ushort>(0x0001, target._memory[0x5]); 
        }

        [TestMethod()]
        public void ToMemory2Test()
        {
            // int memory example
            PlcMemory_Accessor memory = new PlcMemory_Accessor();

            Mock<BaseMemory> privateTarget = new Mock<BaseMemory>(memory._memory, 0xA, 1);
            privateTarget.Protected().Setup<ushort[]>("ObjectToMemory", ItExpr.IsAny<object>()).Returns(new ushort[] { 0x86D9 });
            BaseMemory_Accessor target = new BaseMemory_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseMemory))));

            target.ToMemory<object>(0x000086D9);
            Assert.AreEqual<ushort>(0x86D9, target._memory[0xA]); 
        }

        #endregion
    }
}
