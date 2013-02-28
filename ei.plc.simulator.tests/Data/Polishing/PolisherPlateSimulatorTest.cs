using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PolisherPlateSimulatorTest
    {
        #region constructors tests

        [TestMethod()]
        public void Ctor1Test()
        {
            PolisherPlateSimulator_Accessor target = SimulatorHelper.GetPolisherPlateSimulator(new PlcMemory(), 0, 0, "AKDY3E8P");

            Assert.AreEqual<ushort>(0x4B41, target._memory._memory[0x154]);
            Assert.AreEqual<ushort>(0x5944, target._memory._memory[0x155]);
            Assert.AreEqual<ushort>(0x4533, target._memory._memory[0x156]);
            Assert.AreEqual<ushort>(0x5038, target._memory._memory[0x157]);
        }

        [TestMethod()]
        public void Ctor2Test()
        {
            PolisherPlateSimulator_Accessor target = SimulatorHelper.GetPolisherPlateSimulator(new PlcMemory(), 0, 1, "JFH2ED3F");

            Assert.AreEqual<ushort>(0x464A, target._memory._memory[0x158]);
            Assert.AreEqual<ushort>(0x3248, target._memory._memory[0x159]);
            Assert.AreEqual<ushort>(0x4445, target._memory._memory[0x15A]);
            Assert.AreEqual<ushort>(0x4633, target._memory._memory[0x15B]);
        }

        [TestMethod()]
        public void Ctor3Test()
        {
            PolisherPlateSimulator_Accessor target = SimulatorHelper.GetPolisherPlateSimulator(new PlcMemory(), 0, 2, "NHF4RAF3");

            Assert.AreEqual<ushort>(0x484E, target._memory._memory[0x15C]);
            Assert.AreEqual<ushort>(0x3446, target._memory._memory[0x15D]);
            Assert.AreEqual<ushort>(0x4152, target._memory._memory[0x15E]);
            Assert.AreEqual<ushort>(0x3346, target._memory._memory[0x15F]);
        }

        [TestMethod()]
        public void Ctor4Test()
        {
            PolisherPlateSimulator_Accessor target = SimulatorHelper.GetPolisherPlateSimulator(new PlcMemory(), 0, 3, "TW7EYRX8");

            Assert.AreEqual<ushort>(0x5754, target._memory._memory[0x160]);
            Assert.AreEqual<ushort>(0x4537, target._memory._memory[0x161]);
            Assert.AreEqual<ushort>(0x5259, target._memory._memory[0x162]);
            Assert.AreEqual<ushort>(0x3858, target._memory._memory[0x163]);
        }

        [TestMethod()]
        public void Ctor5Test()
        {
            PolisherPlateSimulator_Accessor target = SimulatorHelper.GetPolisherPlateSimulator(new PlcMemory(), 1, 0, "AEWRQSF2");

            Assert.AreEqual<ushort>(0x4541, target._memory._memory[0x194]);
            Assert.AreEqual<ushort>(0x5257, target._memory._memory[0x195]);
            Assert.AreEqual<ushort>(0x5351, target._memory._memory[0x196]);
            Assert.AreEqual<ushort>(0x3246, target._memory._memory[0x197]);
        }

        [TestMethod()]
        public void Ctor6Test()
        {
            PolisherPlateSimulator_Accessor target = SimulatorHelper.GetPolisherPlateSimulator(new PlcMemory(), 1, 1, "PTOD8DJ3");

            Assert.AreEqual<ushort>(0x5450, target._memory._memory[0x198]);
            Assert.AreEqual<ushort>(0x444F, target._memory._memory[0x199]);
            Assert.AreEqual<ushort>(0x4438, target._memory._memory[0x19A]);
            Assert.AreEqual<ushort>(0x334A, target._memory._memory[0x19B]);
        }

        [TestMethod()]
        public void Ctor7Test()
        {
            PolisherPlateSimulator_Accessor target = SimulatorHelper.GetPolisherPlateSimulator(new PlcMemory(), 1, 2, "LFUY4EW3");

            Assert.AreEqual<ushort>(0x464C, target._memory._memory[0x19C]);
            Assert.AreEqual<ushort>(0x5955, target._memory._memory[0x19D]);
            Assert.AreEqual<ushort>(0x4534, target._memory._memory[0x19E]);
            Assert.AreEqual<ushort>(0x3357, target._memory._memory[0x19F]);
        }

        [TestMethod()]
        public void Ctor8Test()
        {
            PolisherPlateSimulator_Accessor target = SimulatorHelper.GetPolisherPlateSimulator(new PlcMemory(), 1, 3, "UFYE2DI9");

            Assert.AreEqual<ushort>(0x4655, target._memory._memory[0x1A0]);
            Assert.AreEqual<ushort>(0x4559, target._memory._memory[0x1A1]);
            Assert.AreEqual<ushort>(0x4432, target._memory._memory[0x1A2]);
            Assert.AreEqual<ushort>(0x3949, target._memory._memory[0x1A3]);
        }

        [TestMethod()]
        public void Ctor9Test()
        {
            PolisherPlateSimulator_Accessor target = SimulatorHelper.GetPolisherPlateSimulator(new PlcMemory(), 2, 0, "YFTAW8DF");

            Assert.AreEqual<ushort>(0x4659, target._memory._memory[0x1D4]);
            Assert.AreEqual<ushort>(0x4154, target._memory._memory[0x1D5]);
            Assert.AreEqual<ushort>(0x3857, target._memory._memory[0x1D6]);
            Assert.AreEqual<ushort>(0x4644, target._memory._memory[0x1D7]);
        }

        [TestMethod()]
        public void Ctor10Test()
        {
            PolisherPlateSimulator_Accessor target = SimulatorHelper.GetPolisherPlateSimulator(new PlcMemory(), 2, 1, "BXVDRQ5U");

            Assert.AreEqual<ushort>(0x5842, target._memory._memory[0x1D8]);
            Assert.AreEqual<ushort>(0x4456, target._memory._memory[0x1D9]);
            Assert.AreEqual<ushort>(0x5152, target._memory._memory[0x1DA]);
            Assert.AreEqual<ushort>(0x5535, target._memory._memory[0x1DB]);
        }

        [TestMethod()]
        public void Ctor11Test()
        {
            PolisherPlateSimulator_Accessor target = SimulatorHelper.GetPolisherPlateSimulator(new PlcMemory(), 2, 2, "UYS4DXOP");

            Assert.AreEqual<ushort>(0x5955, target._memory._memory[0x1DC]);
            Assert.AreEqual<ushort>(0x3453, target._memory._memory[0x1DD]);
            Assert.AreEqual<ushort>(0x5844, target._memory._memory[0x1DE]);
            Assert.AreEqual<ushort>(0x504F, target._memory._memory[0x1DF]);
        }

        [TestMethod()]
        public void Ctor12Test()
        {
            PolisherPlateSimulator_Accessor target = SimulatorHelper.GetPolisherPlateSimulator(new PlcMemory(), 2, 3, "HDTQSNF2");

            Assert.AreEqual<ushort>(0x4448, target._memory._memory[0x1E0]);
            Assert.AreEqual<ushort>(0x5154, target._memory._memory[0x1E1]);
            Assert.AreEqual<ushort>(0x4E53, target._memory._memory[0x1E2]);
            Assert.AreEqual<ushort>(0x3246, target._memory._memory[0x1E3]);
        }

        #endregion
    }
}
