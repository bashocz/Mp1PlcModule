using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PlcMemoryTest
    {
        #region WriteMemory method tests

        [TestMethod()]
        public void WriteMemory1Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            target.WriteMemory(120, "383146410d48");
            Assert.AreEqual<ushort>(0x3831, target._memory[120]);
            Assert.AreEqual<ushort>(0x4641, target._memory[121]);
            Assert.AreEqual<ushort>(0x0d48, target._memory[122]);
        }

        [TestMethod()]
        public void WriteMemory2Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            target.WriteMemory(241, "56ABABCD");
            Assert.AreEqual<ushort>(0x56AB, target._memory[241]);
            Assert.AreEqual<ushort>(0xABCD, target._memory[242]);
        }
        [TestMethod()]
        public void WriteMemory3Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            target.WriteMemory(312, "0123456789ABCDEF");
            Assert.AreEqual<ushort>(0x0123, target._memory[312]);
            Assert.AreEqual<ushort>(0x4567, target._memory[313]);
            Assert.AreEqual<ushort>(0x89AB, target._memory[314]);
            Assert.AreEqual<ushort>(0xCDEF, target._memory[315]);
        }

        #endregion

        #region ReadMemory method tests

        [TestMethod()]
        public void ReadMemory1Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            target.WriteMemory(10, "56AB");
            Assert.AreEqual<string>("56AB", target.ReadMemory(10, 1));
        }

        [TestMethod()]
        public void ReadMemory2Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            target.WriteMemory(3, "56ABABCD");
            Assert.AreEqual<string>("56ABABCD", target.ReadMemory(3, 2));
        }

        [TestMethod()]
        public void ReadMemory3Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            target.WriteMemory(264, "0123456789ABCDEF");
            Assert.AreEqual<string>("0123456789ABCDEF", target.ReadMemory(264, 4));
        }

        #endregion
    }
}
