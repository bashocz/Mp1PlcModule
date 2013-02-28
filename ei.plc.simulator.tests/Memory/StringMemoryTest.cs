using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class StringMemoryTest
    {
        #region MemoryToObject method tests

        [TestMethod()]
        public void MemoryToObject1Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));
            target._memory[120] = 0x3831;
            target._memory[121] = 0x4641;
            target._memory[122] = 0x3148;

            StringMemory memory = privateTarget.CreateStringMemory(120, 3);
            Assert.AreEqual<string>("18AFH1", memory.Value);
        }

        [TestMethod()]
        public void MemoryToObject2Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));
            target._memory[56] = 0x6548;
            target._memory[57] = 0x6C6C;
            target._memory[58] = 0x206F;
            target._memory[59] = 0x6F57;
            target._memory[60] = 0x6C72;
            target._memory[61] = 0x2164;

            StringMemory memory = privateTarget.CreateStringMemory(56, 6);
            Assert.AreEqual<string>("Hello World!", memory.Value);
        }

        [TestMethod()]
        public void MemoryToObject3Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            target._memory[120] = 0x3831;
            target._memory[121] = 0x0041;
            target._memory[122] = 0x3148;

            StringMemory memory = privateTarget.CreateStringMemory(120, 3);
            Assert.AreEqual<string>("18A", memory.Value);
        }

        [TestMethod()]
        public void MemoryToObject4Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            target._memory[132] = 0x0000;
            target._memory[133] = 0x0000;
            target._memory[134] = 0x0000;
            target._memory[135] = 0x0000;

            StringMemory memory1 = privateTarget.CreateStringMemory(132, 4);
            Assert.AreEqual<string>("", memory1.Value);
        }

        #endregion

        #region ObjectToMemory method tests

        [TestMethod()]
        public void ObjectToMemory1Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            privateTarget.CreateStringMemory(24, 3).Value = "18AFH\r";
            Assert.AreEqual<ushort>(0x3831, target._memory[24]);
            Assert.AreEqual<ushort>(0x4641, target._memory[25]);
            Assert.AreEqual<ushort>(0x0D48, target._memory[26]);
        }

        [TestMethod()]
        public void ObjectToMemory2Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            privateTarget.CreateStringMemory(24, 3).Value = "18AFH";
            Assert.AreEqual<ushort>(0x3831, target._memory[24]);
            Assert.AreEqual<ushort>(0x4641, target._memory[25]);
            Assert.AreEqual<ushort>(0x0048, target._memory[26]);
        }

        [TestMethod()]
        public void ObjectToMemory3Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            privateTarget.CreateStringMemory(112, 2).Value = "Ahoj";
            Assert.AreEqual<ushort>(0x6841, target._memory[112]);
            Assert.AreEqual<ushort>(0x6A6F, target._memory[113]);
        }

        [TestMethod()]
        public void ObjectToMemory4Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            StringMemory memory = privateTarget.CreateStringMemory(56, 6);
            memory.Value = "Hello World!";

            Assert.AreEqual<string>("Hello World!", memory.Value);
            Assert.AreEqual<ushort>(0x6548, target._memory[56]);
            Assert.AreEqual<ushort>(0x6C6C, target._memory[57]);
            Assert.AreEqual<ushort>(0x206F, target._memory[58]);
            Assert.AreEqual<ushort>(0x6F57, target._memory[59]);
            Assert.AreEqual<ushort>(0x6C72, target._memory[60]);
            Assert.AreEqual<ushort>(0x2164, target._memory[61]);

            memory.Value = "XY";
            Assert.AreEqual<string>("XY", memory.Value);
            Assert.AreEqual<ushort>(0x5958, target._memory[56]);
            Assert.AreEqual<ushort>(0x0, target._memory[57]);
            Assert.AreEqual<ushort>(0x0, target._memory[58]);
            Assert.AreEqual<ushort>(0x0, target._memory[59]);
            Assert.AreEqual<ushort>(0x0, target._memory[60]);
            Assert.AreEqual<ushort>(0x0, target._memory[61]);
        }

        [TestMethod()]
        public void ObjectToMemory5Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            StringMemory memory = privateTarget.CreateStringMemory(122, 4);
            memory.Value = "ABCDEFGH";

            Assert.AreEqual<ushort>(0x4241, target._memory[122]);
            Assert.AreEqual<ushort>(0x4443, target._memory[123]);
            Assert.AreEqual<ushort>(0x4645, target._memory[124]);
            Assert.AreEqual<ushort>(0x4847, target._memory[125]);

            memory.Value = "";
            Assert.AreEqual<string>("", memory.Value);
            Assert.AreEqual<ushort>(0x0, target._memory[122]);
            Assert.AreEqual<ushort>(0x0, target._memory[123]);
            Assert.AreEqual<ushort>(0x0, target._memory[124]);
            Assert.AreEqual<ushort>(0x0, target._memory[125]);
        }

        [TestMethod()]
        public void ObjectToMemory6Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            StringMemory memory = privateTarget.CreateStringMemory(122, 4);
            memory.Value = "ABCDEFGH";

            Assert.AreEqual<string>("ABCDEFGH", memory.Value);
            Assert.AreEqual<ushort>(0x4241, target._memory[122]);
            Assert.AreEqual<ushort>(0x4443, target._memory[123]);
            Assert.AreEqual<ushort>(0x4645, target._memory[124]);
            Assert.AreEqual<ushort>(0x4847, target._memory[125]);

            memory.Value = "XY";
            Assert.AreEqual<string>("XY", memory.Value);
            Assert.AreEqual<ushort>(0x5958, target._memory[122]);
            Assert.AreEqual<ushort>(0x0, target._memory[123]);
            Assert.AreEqual<ushort>(0x0, target._memory[124]);
            Assert.AreEqual<ushort>(0x0, target._memory[125]);
        }

        [TestMethod()]
        public void ObjectToMemory7Test()
        {
            PlcMemory privateTarget = new PlcMemory();
            PlcMemory_Accessor target = new PlcMemory_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PlcMemory))));

            StringMemory memory = privateTarget.CreateStringMemory(85, 7);
            memory.Value = "JF1ETSP5HQL0ME";

            Assert.AreEqual<string>("JF1ETSP5HQL0ME", memory.Value);
            Assert.AreEqual<ushort>(0x464A, target._memory[85]);
            Assert.AreEqual<ushort>(0x4531, target._memory[86]);
            Assert.AreEqual<ushort>(0x5354, target._memory[87]);
            Assert.AreEqual<ushort>(0x3550, target._memory[88]);
            Assert.AreEqual<ushort>(0x5148, target._memory[89]);
            Assert.AreEqual<ushort>(0x304C, target._memory[90]);
            Assert.AreEqual<ushort>(0x454D, target._memory[91]);

            memory.Value = "A";
            Assert.AreEqual<string>("A", memory.Value);
            Assert.AreEqual<ushort>(0x0041, target._memory[85]);
            Assert.AreEqual<ushort>(0x0, target._memory[86]);
            Assert.AreEqual<ushort>(0x0, target._memory[87]);
            Assert.AreEqual<ushort>(0x0, target._memory[88]);
            Assert.AreEqual<ushort>(0x0, target._memory[89]);
            Assert.AreEqual<ushort>(0x0, target._memory[90]);
            Assert.AreEqual<ushort>(0x0, target._memory[91]);

            memory.Value = "ABC";
            Assert.AreEqual<string>("ABC", memory.Value);
            Assert.AreEqual<ushort>(0x4241, target._memory[85]);
            Assert.AreEqual<ushort>(0x0043, target._memory[86]);
            Assert.AreEqual<ushort>(0x0, target._memory[87]);
            Assert.AreEqual<ushort>(0x0, target._memory[88]);
            Assert.AreEqual<ushort>(0x0, target._memory[89]);
            Assert.AreEqual<ushort>(0x0, target._memory[90]);
            Assert.AreEqual<ushort>(0x0, target._memory[91]);
        }

        #endregion
    }
}
