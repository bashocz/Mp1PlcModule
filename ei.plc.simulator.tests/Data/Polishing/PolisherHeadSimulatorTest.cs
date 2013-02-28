using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PolisherHeadSimulatorTest
    {
        #region constructors tests

        [TestMethod()]
        public void Ctor1Test()
        {
            PolisherHeadSimulator_Accessor target = SimulatorHelper.GetPolisherHeadSimulator(new PlcMemory(), 0, 0,
                                                                                             26, 12.643, 74.12, 123, 45.6);

            Assert.AreEqual<ushort>(26, target._memory._memory[0x166]);
            Assert.AreEqual<ushort>(12643, target._memory._memory[0x173]);
            Assert.AreEqual<ushort>(7412, target._memory._memory[0x177]);
            Assert.AreEqual<ushort>(123, target._memory._memory[0x17C]);
            Assert.AreEqual<ushort>(456, target._memory._memory[0x181]);
        }

        [TestMethod()]
        public void Ctor2Test()
        {          
            PolisherHeadSimulator_Accessor target = SimulatorHelper.GetPolisherHeadSimulator(new PlcMemory(), 0, 1,
                                                                                             74, 54.485, 24.36, 467, 49.3);

            Assert.AreEqual<ushort>(74, target._memory._memory[0x167]);
            Assert.AreEqual<ushort>(54485, target._memory._memory[0x174]);
            Assert.AreEqual<ushort>(2436, target._memory._memory[0x178]);
            Assert.AreEqual<ushort>(467, target._memory._memory[0x17D]);
            Assert.AreEqual<ushort>(493, target._memory._memory[0x182]);
        }

        [TestMethod()]
        public void Ctor3Test()
        {
            PolisherHeadSimulator_Accessor target = SimulatorHelper.GetPolisherHeadSimulator(new PlcMemory(), 0, 2,
                                                                                             82, 46.687, 49.31, 197, 37.9);

            Assert.AreEqual<ushort>(82, target._memory._memory[0x168]);
            Assert.AreEqual<ushort>(46687, target._memory._memory[0x175]);
            Assert.AreEqual<ushort>(4931, target._memory._memory[0x179]);
            Assert.AreEqual<ushort>(197, target._memory._memory[0x17E]);
            Assert.AreEqual<ushort>(379, target._memory._memory[0x183]);
        }

        [TestMethod()]
        public void Ctor4Test()
        {
            PolisherHeadSimulator_Accessor target = SimulatorHelper.GetPolisherHeadSimulator(new PlcMemory(), 0, 3,
                                                                                             93, 27.937, 45.31, 781, 84.6);

            Assert.AreEqual<ushort>(93, target._memory._memory[0x169]);
            Assert.AreEqual<ushort>(27937, target._memory._memory[0x176]);
            Assert.AreEqual<ushort>(4531, target._memory._memory[0x17A]);
            Assert.AreEqual<ushort>(781, target._memory._memory[0x17F]);
            Assert.AreEqual<ushort>(846, target._memory._memory[0x184]);
        }

        [TestMethod()]
        public void Ctor5Test()
        {
            PolisherHeadSimulator_Accessor target = SimulatorHelper.GetPolisherHeadSimulator(new PlcMemory(), 1, 0,
                                                                                             34, 54.678, 74.98, 749, 64.2);

            Assert.AreEqual<ushort>(34, target._memory._memory[0x1A6]);
            Assert.AreEqual<ushort>(54678, target._memory._memory[0x1B3]);
            Assert.AreEqual<ushort>(7498, target._memory._memory[0x1B7]);
            Assert.AreEqual<ushort>(749, target._memory._memory[0x1BC]);
            Assert.AreEqual<ushort>(642, target._memory._memory[0x1C1]);
        }

        [TestMethod()]
        public void Ctor6Test()
        {
            PolisherHeadSimulator_Accessor target = SimulatorHelper.GetPolisherHeadSimulator(new PlcMemory(), 1, 1,
                                                                                             94, 47.365, 12.54, 643, 21.4);

            Assert.AreEqual<ushort>(94, target._memory._memory[0x1A7]);
            Assert.AreEqual<ushort>(47365, target._memory._memory[0x1B4]);
            Assert.AreEqual<ushort>(1254, target._memory._memory[0x1B8]);
            Assert.AreEqual<ushort>(643, target._memory._memory[0x1BD]);
            Assert.AreEqual<ushort>(214, target._memory._memory[0x1C2]);
        }

        [TestMethod()]
        public void Ctor7Test()
        {
            PolisherHeadSimulator_Accessor target = SimulatorHelper.GetPolisherHeadSimulator(new PlcMemory(), 1, 2,
                                                                                             42, 34.654, 98.74, 653, 14.2);

            Assert.AreEqual<ushort>(42, target._memory._memory[0x1A8]);
            Assert.AreEqual<ushort>(34654, target._memory._memory[0x1B5]);
            Assert.AreEqual<ushort>(9874, target._memory._memory[0x1B9]);
            Assert.AreEqual<ushort>(653, target._memory._memory[0x1BE]);
            Assert.AreEqual<ushort>(142, target._memory._memory[0x1C3]);
        }

        [TestMethod()]
        public void Ctor8Test()
        {
            PolisherHeadSimulator_Accessor target = SimulatorHelper.GetPolisherHeadSimulator(new PlcMemory(), 1, 3,
                                                                                             96, 57.635, 85.21, 784, 37.5);

            Assert.AreEqual<ushort>(96, target._memory._memory[0x1A9]);
            Assert.AreEqual<ushort>(57635, target._memory._memory[0x1B6]);
            Assert.AreEqual<ushort>(8521, target._memory._memory[0x1BA]);
            Assert.AreEqual<ushort>(784, target._memory._memory[0x1BF]);
            Assert.AreEqual<ushort>(375, target._memory._memory[0x1C4]);
        }

        [TestMethod()]
        public void Ctor9Test()
        {
            PolisherHeadSimulator_Accessor target = SimulatorHelper.GetPolisherHeadSimulator(new PlcMemory(), 2, 0,
                                                                                             547, 35.421, 85.74, 321, 74.1);

            Assert.AreEqual<ushort>(547, target._memory._memory[0x1E6]);
            Assert.AreEqual<ushort>(35421, target._memory._memory[0x1F3]);
            Assert.AreEqual<ushort>(8574, target._memory._memory[0x1F7]);
            Assert.AreEqual<ushort>(321, target._memory._memory[0x1FC]);
            Assert.AreEqual<ushort>(741, target._memory._memory[0x201]);
        }

        [TestMethod()]
        public void Ctor10Test()
        {
            PolisherHeadSimulator_Accessor target = SimulatorHelper.GetPolisherHeadSimulator(new PlcMemory(), 2, 1,
                                                                                             963, 64.325, 74.24, 254, 84.2);

            Assert.AreEqual<ushort>(963, target._memory._memory[0x1E7]);
            Assert.AreEqual<ushort>(64325, target._memory._memory[0x1F4]);
            Assert.AreEqual<ushort>(7424, target._memory._memory[0x1F8]);
            Assert.AreEqual<ushort>(254, target._memory._memory[0x1FD]);
            Assert.AreEqual<ushort>(842, target._memory._memory[0x202]);
        }

        [TestMethod()]
        public void Ctor11Test()
        {
            PolisherHeadSimulator_Accessor target = SimulatorHelper.GetPolisherHeadSimulator(new PlcMemory(), 2, 2,
                                                                                             741, 57.674, 54.96, 985, 74.9);

            Assert.AreEqual<ushort>(741, target._memory._memory[0x1E8]);
            Assert.AreEqual<ushort>(57674, target._memory._memory[0x1F5]);
            Assert.AreEqual<ushort>(5496, target._memory._memory[0x1F9]);
            Assert.AreEqual<ushort>(985, target._memory._memory[0x1FE]);
            Assert.AreEqual<ushort>(749, target._memory._memory[0x203]);
        }

        [TestMethod()]
        public void Ctor12Test()
        {
            PolisherHeadSimulator_Accessor target = SimulatorHelper.GetPolisherHeadSimulator(new PlcMemory(), 2, 3,
                                                                                             357, 47.968, 85.64, 384, 96.8);

            Assert.AreEqual<ushort>(357, target._memory._memory[0x1E9]);
            Assert.AreEqual<ushort>(47968, target._memory._memory[0x1F6]);
            Assert.AreEqual<ushort>(8564, target._memory._memory[0x1FA]);
            Assert.AreEqual<ushort>(384, target._memory._memory[0x1FF]);
            Assert.AreEqual<ushort>(968, target._memory._memory[0x204]);
        }

        #endregion
    }
}
