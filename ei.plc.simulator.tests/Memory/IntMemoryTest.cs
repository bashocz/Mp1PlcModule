using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class IntMemoryTest
    {
        #region MemoryToObject method tests

        [TestMethod()]
        public void MemoryToObject1Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));
            target._memory[5] = 25;

            IntMemory memory = privateTarget.CreateIntMemory(5);
            Assert.AreEqual<int>(25, memory.Value);
        }

        [TestMethod()]
        public void MemoryToObject2Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));
            target._memory[26] = 10289;

            IntMemory memory = privateTarget.CreateIntMemory(26);
            Assert.AreEqual<int>(10289, memory.Value);
        }

        #endregion

        #region ObjectToMemory method tests

        [TestMethod()]
        public void ObjectToMemory1Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            privateTarget.CreateIntMemory(13).Value = 25;
            Assert.AreEqual<ushort>(25, target._memory[13]);
        }

        [TestMethod()]
        public void ObjectToMemory2Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            privateTarget.CreateIntMemory(564).Value = 12847;
            Assert.AreEqual<ushort>(12847, target._memory[564]);
        }

        #endregion
    }
}
