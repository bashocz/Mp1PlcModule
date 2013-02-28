using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PolisherStatusSimulatorTest
    {
        #region construtor tests

        [TestMethod()]
        public void Ctor1Test()
        {
            int polisherNumber = 0;
            PlcMemory memory = new PlcMemory();

            PolisherStatusSimulator privateTarget = new PolisherStatusSimulator(memory, polisherNumber);
            PolisherStatusSimulator_Accessor target = new PolisherStatusSimulator_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusSimulator))));

            target.HighPressure = true;
            target.State = PolisherState.AutoLoad;

            target.MagazineId = "FYRQ2S8P";
            List<PolisherPlateSimulator> plates = new List<PolisherPlateSimulator>();
            plates.Add(new PolisherPlateSimulator(memory, polisherNumber, 0) { Id = "IDQ2SC4F" });
            plates.Add(new PolisherPlateSimulator(memory, polisherNumber, 1) { Id = "KFV1FE3P" });
            plates.Add(new PolisherPlateSimulator(memory, polisherNumber, 2) { Id = "YQA2XS3X" });
            plates.Add(new PolisherPlateSimulator(memory, polisherNumber, 3) { Id = "BXLA3QW8" });

            target.HighPressureDuration = TimeSpan.FromMilliseconds(10);

            SimulatorHelper.GetPolisherLiquidSimulator(memory, polisherNumber, 41.2, 34.8, 98.4, 74.2, 93.2, 82.1, 58.7, 97.4, 36.4);
            
            SimulatorHelper.GetPolisherHeadSimulator(memory, polisherNumber, 0, 26, 12.643, 74.12, 123, 45.6);
            SimulatorHelper.GetPolisherHeadSimulator(memory, polisherNumber, 1, 74, 54.485, 24.36, 467, 49.3);
            SimulatorHelper.GetPolisherHeadSimulator(memory, polisherNumber, 2, 82, 46.687, 49.31, 197, 37.9);
            SimulatorHelper.GetPolisherHeadSimulator(memory, polisherNumber, 3, 93, 27.937, 45.31, 781, 84.6);

            target.PlateRpm = 2643;
            target.PlateLoadCurrent = 57.6;
            target.PadUsedTime = TimeSpan.FromMilliseconds(24657);
            target.PadUsedCount = 40;

            // High Pressure Global (from short status) Polishing Flag
            Assert.AreEqual<ushort>(1, target._memory._memory[0x140]);

            // Polishing Machine Status
            Assert.AreEqual<ushort>(3, target._memory._memory[0x143]);

            // Magazine ID
            Assert.AreEqual<ushort>(0x5946, target._memory._memory[0x150]);
            Assert.AreEqual<ushort>(0x5152, target._memory._memory[0x151]);
            Assert.AreEqual<ushort>(0x5332, target._memory._memory[0x152]);
            Assert.AreEqual<ushort>(0x5038, target._memory._memory[0x153]);
            // Plates
            Assert.AreEqual<ushort>(0x4449, target._memory._memory[0x154]);
            Assert.AreEqual<ushort>(0x3251, target._memory._memory[0x155]);
            Assert.AreEqual<ushort>(0x4353, target._memory._memory[0x156]);
            Assert.AreEqual<ushort>(0x4634, target._memory._memory[0x157]);

            Assert.AreEqual<ushort>(0x464B, target._memory._memory[0x158]);
            Assert.AreEqual<ushort>(0x3156, target._memory._memory[0x159]);
            Assert.AreEqual<ushort>(0x4546, target._memory._memory[0x15A]);
            Assert.AreEqual<ushort>(0x5033, target._memory._memory[0x15B]);

            Assert.AreEqual<ushort>(0x5159, target._memory._memory[0x15C]);
            Assert.AreEqual<ushort>(0x3241, target._memory._memory[0x15D]);
            Assert.AreEqual<ushort>(0x5358, target._memory._memory[0x15E]);
            Assert.AreEqual<ushort>(0x5833, target._memory._memory[0x15F]);

            Assert.AreEqual<ushort>(0x5842, target._memory._memory[0x160]);
            Assert.AreEqual<ushort>(0x414C, target._memory._memory[0x161]);
            Assert.AreEqual<ushort>(0x5133, target._memory._memory[0x162]);
            Assert.AreEqual<ushort>(0x3857, target._memory._memory[0x163]);

            // High Pressure Polishing Flag
            Assert.AreEqual<ushort>(1, target._memory._memory[0x164]);

            // High Pressure Duration
            Assert.AreEqual<ushort>(10, target._memory._memory[0x165]);

            // Polishing Liquids
            Assert.AreEqual<ushort>(412, target._memory._memory[0x16A]);
            Assert.AreEqual<ushort>(348, target._memory._memory[0x16B]);
            Assert.AreEqual<ushort>(984, target._memory._memory[0x16C]);
            Assert.AreEqual<ushort>(742, target._memory._memory[0x16D]);
            Assert.AreEqual<ushort>(932, target._memory._memory[0x16E]);
            Assert.AreEqual<ushort>(821, target._memory._memory[0x16F]);
            Assert.AreEqual<ushort>(587, target._memory._memory[0x170]);
            Assert.AreEqual<ushort>(974, target._memory._memory[0x171]);
            Assert.AreEqual<ushort>(364, target._memory._memory[0x172]);

            // Polishing Heads
            // Force
            Assert.AreEqual<ushort>(26, target._memory._memory[0x166]);
            Assert.AreEqual<ushort>(74, target._memory._memory[0x167]);
            Assert.AreEqual<ushort>(82, target._memory._memory[0x168]);
            Assert.AreEqual<ushort>(93, target._memory._memory[0x169]);

            // Pressure
            Assert.AreEqual<ushort>(12643, target._memory._memory[0x173]);
            Assert.AreEqual<ushort>(54485, target._memory._memory[0x174]);
            Assert.AreEqual<ushort>(46687, target._memory._memory[0x175]);
            Assert.AreEqual<ushort>(27937, target._memory._memory[0x176]);
            
            // Back Pressure
            Assert.AreEqual<ushort>(7412, target._memory._memory[0x177]);
            Assert.AreEqual<ushort>(2436, target._memory._memory[0x178]);
            Assert.AreEqual<ushort>(4931, target._memory._memory[0x179]);
            Assert.AreEqual<ushort>(4531, target._memory._memory[0x17A]);

            // Rpm
            Assert.AreEqual<ushort>(123, target._memory._memory[0x17C]);
            Assert.AreEqual<ushort>(467, target._memory._memory[0x17D]);
            Assert.AreEqual<ushort>(197, target._memory._memory[0x17E]);
            Assert.AreEqual<ushort>(781, target._memory._memory[0x17F]);

            // Load Current
            Assert.AreEqual<ushort>(456, target._memory._memory[0x181]);
            Assert.AreEqual<ushort>(493, target._memory._memory[0x182]);
            Assert.AreEqual<ushort>(379, target._memory._memory[0x183]);
            Assert.AreEqual<ushort>(846, target._memory._memory[0x184]);

            // Plate Rpm
            Assert.AreEqual<ushort>(2643, target._memory._memory[0x17B]);

            // Plate Load Current
            Assert.AreEqual<ushort>(576, target._memory._memory[0x180]);

            // Pad Used Time
            Assert.AreEqual<ushort>(24657, target._memory._memory[0x185]);

            // Pad USed Count
            Assert.AreEqual<ushort>(40, target._memory._memory[0x186]);
        }

        [TestMethod()]
        public void Ctor2Test()
        {
            int polisherNumber = 1;
            PlcMemory memory = new PlcMemory();

            PolisherStatusSimulator privateTarget = new PolisherStatusSimulator(memory, polisherNumber);
            PolisherStatusSimulator_Accessor target = new PolisherStatusSimulator_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusSimulator))));

            target.HighPressure = false;
            target.State = PolisherState.AutoWait;

            target.MagazineId = "IRYX3DF8";
            List<PolisherPlateSimulator> plates = new List<PolisherPlateSimulator>();
            plates.Add(new PolisherPlateSimulator(memory, polisherNumber, 0) { Id = "IDUEQ3C5" });
            plates.Add(new PolisherPlateSimulator(memory, polisherNumber, 1) { Id = "PXMCHWTF" });
            plates.Add(new PolisherPlateSimulator(memory, polisherNumber, 2) { Id = "IUQCA3FC" });
            plates.Add(new PolisherPlateSimulator(memory, polisherNumber, 3) { Id = "BAGQTDE1" });

            target.HighPressureDuration = TimeSpan.FromMilliseconds(6);

            SimulatorHelper.GetPolisherLiquidSimulator(memory, polisherNumber, 57.3, 64.5, 28.9, 76.8, 32.4, 120.3, 984.2, 0, 12.4);

            SimulatorHelper.GetPolisherHeadSimulator(memory, polisherNumber, 0, 34, 54.678, 74.98, 749, 64.2);
            SimulatorHelper.GetPolisherHeadSimulator(memory, polisherNumber, 1, 94, 47.365, 12.54, 643, 21.4);
            SimulatorHelper.GetPolisherHeadSimulator(memory, polisherNumber, 2, 42, 34.654, 98.74, 653, 14.2);
            SimulatorHelper.GetPolisherHeadSimulator(memory, polisherNumber, 3, 96, 57.635, 85.21, 784, 37.5);

            target.PlateRpm = 6743;
            target.PlateLoadCurrent = 67.8;
            target.PadUsedTime = TimeSpan.FromMilliseconds(650);
            target.PadUsedCount = 120;

            // High Pressure Global (from short status) Polishing Flag
            Assert.AreEqual<ushort>(0, target._memory._memory[0x141]);

            // Polishing Machine Status
            Assert.AreEqual<ushort>(2, target._memory._memory[0x144]);

            // Magazine ID
            Assert.AreEqual<ushort>(0x5249, target._memory._memory[0x190]);
            Assert.AreEqual<ushort>(0x5859, target._memory._memory[0x191]);
            Assert.AreEqual<ushort>(0x4433, target._memory._memory[0x192]);
            Assert.AreEqual<ushort>(0x3846, target._memory._memory[0x193]);
            // Plates
            Assert.AreEqual<ushort>(0x4449, target._memory._memory[0x194]);
            Assert.AreEqual<ushort>(0x4555, target._memory._memory[0x195]);
            Assert.AreEqual<ushort>(0x3351, target._memory._memory[0x196]);
            Assert.AreEqual<ushort>(0x3543, target._memory._memory[0x197]);

            Assert.AreEqual<ushort>(0x5850, target._memory._memory[0x198]);
            Assert.AreEqual<ushort>(0x434D, target._memory._memory[0x199]);
            Assert.AreEqual<ushort>(0x5748, target._memory._memory[0x19A]);
            Assert.AreEqual<ushort>(0x4654, target._memory._memory[0x19B]);

            Assert.AreEqual<ushort>(0x5549, target._memory._memory[0x19C]);
            Assert.AreEqual<ushort>(0x4351, target._memory._memory[0x19D]);
            Assert.AreEqual<ushort>(0x3341, target._memory._memory[0x19E]);
            Assert.AreEqual<ushort>(0x4346, target._memory._memory[0x19F]);

            Assert.AreEqual<ushort>(0x4142, target._memory._memory[0x1A0]);
            Assert.AreEqual<ushort>(0x5147, target._memory._memory[0x1A1]);
            Assert.AreEqual<ushort>(0x4454, target._memory._memory[0x1A2]);
            Assert.AreEqual<ushort>(0x3145, target._memory._memory[0x1A3]);

            // High Pressure Polishing Flag
            Assert.AreEqual<ushort>(0, target._memory._memory[0x1A4]);

            // High Pressure Duration
            Assert.AreEqual<ushort>(6, target._memory._memory[0x1A5]);

            // Polishing Liquids
            Assert.AreEqual<ushort>(573, target._memory._memory[0x1AA]);
            Assert.AreEqual<ushort>(645, target._memory._memory[0x1AB]);
            Assert.AreEqual<ushort>(289, target._memory._memory[0x1AC]);
            Assert.AreEqual<ushort>(768, target._memory._memory[0x1AD]);
            Assert.AreEqual<ushort>(324, target._memory._memory[0x1AE]);
            Assert.AreEqual<ushort>(1203, target._memory._memory[0x1AF]);
            Assert.AreEqual<ushort>(9842, target._memory._memory[0x1B0]);
            Assert.AreEqual<ushort>(0, target._memory._memory[0x1B1]);
            Assert.AreEqual<ushort>(124, target._memory._memory[0x1B2]);

            // Polishing Heads
            // Force
            Assert.AreEqual<ushort>(34, target._memory._memory[0x1A6]);
            Assert.AreEqual<ushort>(94, target._memory._memory[0x1A7]);
            Assert.AreEqual<ushort>(42, target._memory._memory[0x1A8]);
            Assert.AreEqual<ushort>(96, target._memory._memory[0x1A9]);

            // Pressure
            Assert.AreEqual<ushort>(54678, target._memory._memory[0x1B3]);
            Assert.AreEqual<ushort>(47365, target._memory._memory[0x1B4]);
            Assert.AreEqual<ushort>(34654, target._memory._memory[0x1B5]);
            Assert.AreEqual<ushort>(57635, target._memory._memory[0x1B6]);

            // Back Pressure
            Assert.AreEqual<ushort>(7498, target._memory._memory[0x1B7]);
            Assert.AreEqual<ushort>(1254, target._memory._memory[0x1B8]);
            Assert.AreEqual<ushort>(9874, target._memory._memory[0x1B9]);
            Assert.AreEqual<ushort>(8521, target._memory._memory[0x1BA]);

            // Rpm
            Assert.AreEqual<ushort>(749, target._memory._memory[0x1BC]);
            Assert.AreEqual<ushort>(643, target._memory._memory[0x1BD]);
            Assert.AreEqual<ushort>(653, target._memory._memory[0x1BE]);
            Assert.AreEqual<ushort>(784, target._memory._memory[0x1BF]);

            // Load Current
            Assert.AreEqual<ushort>(642, target._memory._memory[0x1C1]);
            Assert.AreEqual<ushort>(214, target._memory._memory[0x1C2]);
            Assert.AreEqual<ushort>(142, target._memory._memory[0x1C3]);
            Assert.AreEqual<ushort>(375, target._memory._memory[0x1C4]);

            // Plate Rpm
            Assert.AreEqual<ushort>(6743, target._memory._memory[0x1BB]);

            // Plate Load Current
            Assert.AreEqual<ushort>(678, target._memory._memory[0x1C0]);

            // Pad Used Time
            Assert.AreEqual<ushort>(650, target._memory._memory[0x1C5]);

            // Pad USed Count
            Assert.AreEqual<ushort>(120, target._memory._memory[0x1C6]);
        }


        [TestMethod()]
        public void Ctor3Test()
        {
            int polisherNumber = 2;
            PlcMemory memory = new PlcMemory();

            PolisherStatusSimulator privateTarget = new PolisherStatusSimulator(memory, polisherNumber);
            PolisherStatusSimulator_Accessor target = new PolisherStatusSimulator_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusSimulator))));

            target.HighPressure = true;
            target.State = PolisherState.Pause;

            target.MagazineId = "MCKDYQS2";
            List<PolisherPlateSimulator> plates = new List<PolisherPlateSimulator>();
            plates.Add(new PolisherPlateSimulator(memory, polisherNumber, 0) { Id = "IOWUD3S8" });
            plates.Add(new PolisherPlateSimulator(memory, polisherNumber, 1) { Id = "MVUDFJW2" });
            plates.Add(new PolisherPlateSimulator(memory, polisherNumber, 2) { Id = "ARQESBCF" });
            plates.Add(new PolisherPlateSimulator(memory, polisherNumber, 3) { Id = "SDHD54DV" });

            target.HighPressureDuration = TimeSpan.FromMilliseconds(14);

            SimulatorHelper.GetPolisherLiquidSimulator(memory, polisherNumber, 875.9, 3468.2, 958.1, 42.8, 38.2, 312.7, 6497.3, 1397.4, 4587.6);

            SimulatorHelper.GetPolisherHeadSimulator(memory, polisherNumber, 0, 547, 35.421, 85.74, 321, 74.1);
            SimulatorHelper.GetPolisherHeadSimulator(memory, polisherNumber, 1, 963, 64.325, 74.24, 254, 84.2);
            SimulatorHelper.GetPolisherHeadSimulator(memory, polisherNumber, 2, 741, 57.674, 54.96, 985, 74.9);
            SimulatorHelper.GetPolisherHeadSimulator(memory, polisherNumber, 3, 357, 47.968, 85.64, 384, 96.8);

            target.PlateRpm = 3650;
            target.PlateLoadCurrent = 84.7;
            target.PadUsedTime = TimeSpan.FromMilliseconds(1042);
            target.PadUsedCount = 240;

            // High Pressure Global (from short status) Polishing Flag
            Assert.AreEqual<ushort>(1, target._memory._memory[0x142]);

            // Polishing Machine Status
            Assert.AreEqual<ushort>(5, target._memory._memory[0x145]);
            
            // Magazine ID
            Assert.AreEqual<ushort>(0x434D, target._memory._memory[0x1D0]);
            Assert.AreEqual<ushort>(0x444B, target._memory._memory[0x1D1]);
            Assert.AreEqual<ushort>(0x5159, target._memory._memory[0x1D2]);
            Assert.AreEqual<ushort>(0x3253, target._memory._memory[0x1D3]);
            // Plates
            Assert.AreEqual<ushort>(0x4F49, target._memory._memory[0x1D4]);
            Assert.AreEqual<ushort>(0x5557, target._memory._memory[0x1D5]);
            Assert.AreEqual<ushort>(0x3344, target._memory._memory[0x1D6]);
            Assert.AreEqual<ushort>(0x3853, target._memory._memory[0x1D7]);

            Assert.AreEqual<ushort>(0x564D, target._memory._memory[0x1D8]);
            Assert.AreEqual<ushort>(0x4455, target._memory._memory[0x1D9]);
            Assert.AreEqual<ushort>(0x4A46, target._memory._memory[0x1DA]);
            Assert.AreEqual<ushort>(0x3257, target._memory._memory[0x1DB]);

            Assert.AreEqual<ushort>(0x5241, target._memory._memory[0x1DC]);
            Assert.AreEqual<ushort>(0x4551, target._memory._memory[0x1DD]);
            Assert.AreEqual<ushort>(0x4253, target._memory._memory[0x1DE]);
            Assert.AreEqual<ushort>(0x4643, target._memory._memory[0x1DF]);

            Assert.AreEqual<ushort>(0x4453, target._memory._memory[0x1E0]);
            Assert.AreEqual<ushort>(0x4448, target._memory._memory[0x1E1]);
            Assert.AreEqual<ushort>(0x3435, target._memory._memory[0x1E2]);
            Assert.AreEqual<ushort>(0x5644, target._memory._memory[0x1E3]);

            // High Pressure Polishing Flag
            Assert.AreEqual<ushort>(1, target._memory._memory[0x1E4]);

            // High Pressure Duration
            Assert.AreEqual<ushort>(14, target._memory._memory[0x1E5]);

            // Polishing Liquids
            Assert.AreEqual<ushort>(8759, target._memory._memory[0x1EA]);
            Assert.AreEqual<ushort>(34682, target._memory._memory[0x1EB]);
            Assert.AreEqual<ushort>(9581, target._memory._memory[0x1EC]);
            Assert.AreEqual<ushort>(428, target._memory._memory[0x1ED]);
            Assert.AreEqual<ushort>(382, target._memory._memory[0x1EE]);
            Assert.AreEqual<ushort>(3127, target._memory._memory[0x1EF]);
            Assert.AreEqual<ushort>(64973, target._memory._memory[0x1F0]);
            Assert.AreEqual<ushort>(13974, target._memory._memory[0x1F1]);
            Assert.AreEqual<ushort>(45876, target._memory._memory[0x1F2]);

            // Polishing Heads
            // Force
            Assert.AreEqual<ushort>(547, target._memory._memory[0x1E6]);
            Assert.AreEqual<ushort>(963, target._memory._memory[0x1E7]);
            Assert.AreEqual<ushort>(741, target._memory._memory[0x1E8]);
            Assert.AreEqual<ushort>(357, target._memory._memory[0x1E9]);

            // Pressure
            Assert.AreEqual<ushort>(35421, target._memory._memory[0x1F3]);
            Assert.AreEqual<ushort>(64325, target._memory._memory[0x1F4]);
            Assert.AreEqual<ushort>(57674, target._memory._memory[0x1F5]);
            Assert.AreEqual<ushort>(47968, target._memory._memory[0x1F6]);

            // Back Pressure
            Assert.AreEqual<ushort>(8574, target._memory._memory[0x1F7]);
            Assert.AreEqual<ushort>(7424, target._memory._memory[0x1F8]);
            Assert.AreEqual<ushort>(5496, target._memory._memory[0x1F9]);
            Assert.AreEqual<ushort>(8564, target._memory._memory[0x1FA]);

            // Rpm
            Assert.AreEqual<ushort>(321, target._memory._memory[0x1FC]);
            Assert.AreEqual<ushort>(254, target._memory._memory[0x1FD]);
            Assert.AreEqual<ushort>(985, target._memory._memory[0x1FE]);
            Assert.AreEqual<ushort>(384, target._memory._memory[0x1FF]);

            // Load Current
            Assert.AreEqual<ushort>(741, target._memory._memory[0x201]);
            Assert.AreEqual<ushort>(842, target._memory._memory[0x202]);
            Assert.AreEqual<ushort>(749, target._memory._memory[0x203]);
            Assert.AreEqual<ushort>(968, target._memory._memory[0x204]);

            // Plate Rpm
            Assert.AreEqual<ushort>(3650, target._memory._memory[0x1FB]);

            // Plate Load Current
            Assert.AreEqual<ushort>(847, target._memory._memory[0x200]);

            // Pad Used Time
            Assert.AreEqual<ushort>(1042, target._memory._memory[0x205]);

            //Pad USed Count
            Assert.AreEqual<ushort>(240, target._memory._memory[0x206]);

        }
        #endregion
    }
}
