using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PolishingPlateSimulatorTest
    {
        #region constructors tests

        [TestMethod()]
        public void Ctor1Test()
        {
            PolishingPlateSimulator_Accessor target = SimulatorHelper.GetPolishingPlateSimulator(new PlcMemory(), 0, "IDUYR5D8", 1);

            Assert.AreEqual<ushort>(0x4449, target._memory._memory[0x126]);
            Assert.AreEqual<ushort>(0x5955, target._memory._memory[0x127]);
            Assert.AreEqual<ushort>(0x3552, target._memory._memory[0x128]);
            Assert.AreEqual<ushort>(0x3844, target._memory._memory[0x129]);
            Assert.AreEqual<ushort>(1, target._memory._memory[0x136]);
        }

        [TestMethod()]
        public void Ctor2Test()
        {
            PolishingPlateSimulator_Accessor target = SimulatorHelper.GetPolishingPlateSimulator(new PlcMemory(), 1, "PSUR5EV8", 74);

            Assert.AreEqual<ushort>(0x5350, target._memory._memory[0x12A]);
            Assert.AreEqual<ushort>(0x5255, target._memory._memory[0x12B]);
            Assert.AreEqual<ushort>(0x4535, target._memory._memory[0x12C]);
            Assert.AreEqual<ushort>(0x3856, target._memory._memory[0x12D]);
            Assert.AreEqual<ushort>(74, target._memory._memory[0x137]);
        }

        [TestMethod()]
        public void Ctor3Test()
        {
            PolishingPlateSimulator_Accessor target = SimulatorHelper.GetPolishingPlateSimulator(new PlcMemory(), 2, "NDYW2SFE", 64);

            Assert.AreEqual<ushort>(0x444E, target._memory._memory[0x12E]);
            Assert.AreEqual<ushort>(0x5759, target._memory._memory[0x12F]);
            Assert.AreEqual<ushort>(0x5332, target._memory._memory[0x130]);
            Assert.AreEqual<ushort>(0x4546, target._memory._memory[0x131]);
            Assert.AreEqual<ushort>(64, target._memory._memory[0x138]);
        }

        [TestMethod()]
        public void Ctor4Test()
        {
            PolishingPlateSimulator_Accessor target = SimulatorHelper.GetPolishingPlateSimulator(new PlcMemory(), 3, "YETR3SQW", 25);

            Assert.AreEqual<ushort>(0x4559, target._memory._memory[0x132]);
            Assert.AreEqual<ushort>(0x5254, target._memory._memory[0x133]);
            Assert.AreEqual<ushort>(0x5333, target._memory._memory[0x134]);
            Assert.AreEqual<ushort>(0x5751, target._memory._memory[0x135]);
            Assert.AreEqual<ushort>(25, target._memory._memory[0x139]);
        }

        #endregion
    }
}
