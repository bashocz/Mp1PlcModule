using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class DoubleMemoryTest
    {
        #region MemoryToObject method tests

        [TestMethod()]
        public void MemoryToObject1Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));
            target._memory[9] = 246;

            DoubleMemory memory = privateTarget.CreateDoubleMemory(9, 1);
            Assert.AreEqual<double>(24.6, memory.Value);
        }

        [TestMethod()]
        public void MemoryToObject2Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));
            target._memory[579] = 1486;

            DoubleMemory memory = privateTarget.CreateDoubleMemory(579, 2);
            Assert.AreEqual<double>(14.86, memory.Value);
        }

        [TestMethod()]
        public void MemoryToObject3Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));
            target._memory[279] = 12759;

            DoubleMemory memory = privateTarget.CreateDoubleMemory(279, 3);
            Assert.AreEqual<double>(12.759, memory.Value);
        }

        #endregion

        #region ObjectToMemory method tests

        [TestMethod()]
        public void ObjectToMemory1Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            privateTarget.CreateDoubleMemory(87, 1).Value = 42.6;
            Assert.AreEqual<ushort>(426, target._memory[87]);
        }

        [TestMethod()]
        public void ObjectToMemory2Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            privateTarget.CreateDoubleMemory(312, 2).Value = 145.67;
            Assert.AreEqual<ushort>(14567, target._memory[312]);
        }

        [TestMethod()]
        public void ObjectToMemory3Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            privateTarget.CreateDoubleMemory(584, 3).Value = 45.687;
            Assert.AreEqual<ushort>(45687, target._memory[584]);
        }

        #endregion
    }
}
