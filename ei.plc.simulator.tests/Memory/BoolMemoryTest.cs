using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class BoolMemoryTest
    {
        #region MemoryToObject method tests

        [TestMethod()]
        public void MemoryToObject1Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));
            target._memory[3] = 1;

            BoolMemory memory = privateTarget.CreateBoolMemory(3, 1);
            Assert.AreEqual<bool>(true, memory.Value);
        }

        [TestMethod()]
        public void MemoryToObject2Test()
        {
            PlcMemory privateTarget = new PlcMemory();

            BoolMemory memory = privateTarget.CreateBoolMemory(2, 1);
            Assert.AreEqual<bool>(false, memory.Value);
        }

        [TestMethod()]
        public void MemoryToObject3Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));
            target._memory[10] = 1;

            BoolMemory memory = privateTarget.CreateBoolMemory(10, 0);
            Assert.AreEqual<bool>(true, memory.Value);
        }

        [TestMethod()]
        public void MemoryToObject4Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));
            target._memory[33] = 2;

            BoolMemory memory = privateTarget.CreateBoolMemory(33, 0);
            Assert.AreEqual<bool>(false, memory.Value);
        }

        #endregion

        #region ObjectToMemory method tests

        [TestMethod()]
        public void ObjectToMemory1Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));
            
            privateTarget.CreateBoolMemory(56, 1).Value = true;
            Assert.AreEqual<ushort>(1, target._memory[56]);
        }

        [TestMethod()]
        public void ObjectToMemory2Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            privateTarget.CreateBoolMemory(349, 1).Value = false;
            Assert.AreEqual<ushort>(0, target._memory[349]);
        }

        [TestMethod()]
        public void ObjectToMemory3Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            privateTarget.CreateBoolMemory(143, 0).Value = true;
            Assert.AreEqual<ushort>(1, target._memory[143]);
        }

        [TestMethod()]
        public void ObjectToMemory4Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            privateTarget.CreateBoolMemory(973, 0).Value = false;
            Assert.AreEqual<ushort>(2, target._memory[973]);
        }

        #endregion
    }
}
