using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    #region constructor tests

    [TestClass()]
    public class PolisherLiquidSimulatorTest
    {
        [TestMethod()]
        public void Ctor1Test()
        {
            PolisherLiquidSimulator_Accessor target = SimulatorHelper.GetPolisherLiquidSimulator(new PlcMemory(), 0,
                                                                                41.2, 34.8, 98.4, 74.2, 93.2, 82.1, 58.7, 97.4, 36.4);
            
            Assert.AreEqual<ushort>(412, target._memory._memory[0x16A]);
            Assert.AreEqual<ushort>(348, target._memory._memory[0x16B]);
            Assert.AreEqual<ushort>(984, target._memory._memory[0x16C]);
            Assert.AreEqual<ushort>(742, target._memory._memory[0x16D]);
            Assert.AreEqual<ushort>(932, target._memory._memory[0x16E]);
            Assert.AreEqual<ushort>(821, target._memory._memory[0x16F]);
            Assert.AreEqual<ushort>(587, target._memory._memory[0x170]);
            Assert.AreEqual<ushort>(974, target._memory._memory[0x171]);
            Assert.AreEqual<ushort>(364, target._memory._memory[0x172]);
        }

        [TestMethod()]
        public void Ctor2Test()
        {
            PolisherLiquidSimulator_Accessor target = SimulatorHelper.GetPolisherLiquidSimulator(new PlcMemory(), 1,
                                                                                 57.3, 64.5, 28.9, 76.8, 32.4, 120.3, 984.2, 0, 12.4);

            Assert.AreEqual<ushort>(573, target._memory._memory[0x1AA]);
            Assert.AreEqual<ushort>(645, target._memory._memory[0x1AB]);
            Assert.AreEqual<ushort>(289, target._memory._memory[0x1AC]);
            Assert.AreEqual<ushort>(768, target._memory._memory[0x1AD]);
            Assert.AreEqual<ushort>(324, target._memory._memory[0x1AE]);
            Assert.AreEqual<ushort>(1203, target._memory._memory[0x1AF]);
            Assert.AreEqual<ushort>(9842, target._memory._memory[0x1B0]);
            Assert.AreEqual<ushort>(0, target._memory._memory[0x1B1]);
            Assert.AreEqual<ushort>(124, target._memory._memory[0x1B2]);
        }

        [TestMethod()]
        public void Ctor3Test()
        {
            PolisherLiquidSimulator_Accessor target = SimulatorHelper.GetPolisherLiquidSimulator(new PlcMemory(), 2,
                                                                    875.9, 3468.2, 958.1, 42.8, 38.2, 312.7, 6497.3, 1397.40, 4587.6);

            Assert.AreEqual<ushort>(8759, target._memory._memory[0x1EA]);
            Assert.AreEqual<ushort>(34682, target._memory._memory[0x1EB]);
            Assert.AreEqual<ushort>(9581, target._memory._memory[0x1EC]);
            Assert.AreEqual<ushort>(428, target._memory._memory[0x1ED]);
            Assert.AreEqual<ushort>(382, target._memory._memory[0x1EE]);
            Assert.AreEqual<ushort>(3127, target._memory._memory[0x1EF]);
            Assert.AreEqual<ushort>(64973, target._memory._memory[0x1F0]);
            Assert.AreEqual<ushort>(13974, target._memory._memory[0x1F1]);
            Assert.AreEqual<ushort>(45876, target._memory._memory[0x1F2]);
        }
    }
    #endregion
}
