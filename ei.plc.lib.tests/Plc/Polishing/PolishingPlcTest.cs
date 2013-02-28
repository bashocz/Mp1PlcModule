using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PolishingPlcTest
    {
        #region private members

        private string GetWriteCommandProcessRecipeString(IMagazine magazine)
        {
            return (new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(), 0x122, new ProcessRecipeToStreamConverter().TryConvert(magazine))).CommandToString();
        }

        #endregion

        #region IsMagazineArrived method test

        [TestMethod()]
        public void IsMagazineArrived1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            string read = string.Empty;
            plcComm.Setup(x => x.Read()).Returns(() => { return read; });
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback<string>((s) =>
            {
                if (s.StartsWith("\u000500FFCR0001200105"))
                    read = "\u000200FF0001\u0003" + PlcStream.CalculateCheckSum("00FF0001\u0003");
            });

            PolishLinePlc target = new PolishLinePlc(plcComm.Object);
            target.Open();

            Assert.AreEqual(true, target.IsMagazineArrived());
        }

        [TestMethod()]
        public void IsMagazineArrived2Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            string read = string.Empty;
            plcComm.Setup(x => x.Read()).Returns(() => { return read; });
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback<string>((s) =>
            {
                if (s.StartsWith("\u000500FFCR0001200105"))
                    read = "\u000200FF0000\u0003" + PlcStream.CalculateCheckSum("00FF0000\u0003");
            });

            PolishLinePlc target = new PolishLinePlc(plcComm.Object);
            target.Open();

            Assert.AreEqual(false, target.IsMagazineArrived());
        }

        #endregion

        #region GetShortStatus method tests

        [TestMethod()]
        public void GetShortStatus1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            string read = string.Empty;
            plcComm.Setup(x => x.Read()).Returns(() => { return read; });
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback<string>((s) =>
            {
                if (s.StartsWith("\u000500FFCR000140060C"))
                    read = "\u000200FF000100000001000100050003\u0003" + PlcStream.CalculateCheckSum("00FF000100000001000100050003\u0003");
                if (s.StartsWith("\u000500FFCR0001200105"))
                    read = "\u000200FF0001\u0003" + PlcStream.CalculateCheckSum("00FF0001\u0003");
            });

            PolishLinePlc target = new PolishLinePlc(plcComm.Object);
            target.Open();

            IPolishingShortStatus target1 = target.GetShortStatus();

            // Status
            Assert.AreEqual<PolisherState>(PolisherState.AutoProcess, target1.Status[0].State);
            Assert.AreEqual<PolisherState>(PolisherState.Pause, target1.Status[1].State);
            Assert.AreEqual<PolisherState>(PolisherState.AutoLoad, target1.Status[2].State);

            // HighPressure
            Assert.AreEqual<bool>(true, target1.Status[0].HighPressure);
            Assert.AreEqual<bool>(false, target1.Status[1].HighPressure);
            Assert.AreEqual<bool>(true, target1.Status[2].HighPressure);

            // IsMagazineArrived
            Assert.IsTrue(target1.IsMagazineArrived);
        }

        [TestMethod()]
        public void GetShortStatus2Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            string read = string.Empty;
            plcComm.Setup(x => x.Read()).Returns(() => { return read; });
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback<string>((s) =>
            {
                if (s.StartsWith("\u000500FFCR000140060C"))
                    read = "\u000200FF000000010001000700020006\u0003" + PlcStream.CalculateCheckSum("00FF000000010001000700020006\u0003");
                if (s.StartsWith("\u000500FFCR0001200105"))
                    read = "\u000200FF0000\u0003" + PlcStream.CalculateCheckSum("00FF0000\u0003");
            });

            PolishLinePlc target = new PolishLinePlc(plcComm.Object);
            target.Open();

            IPolishingShortStatus target1 = target.GetShortStatus();

            // Status
            Assert.AreEqual<PolisherState>(PolisherState.EmergencyStop, target1.Status[0].State);
            Assert.AreEqual<PolisherState>(PolisherState.AutoWait, target1.Status[1].State);
            Assert.AreEqual<PolisherState>(PolisherState.Alarm, target1.Status[2].State);

            // HighPressure
            Assert.AreEqual<bool>(false, target1.Status[0].HighPressure);
            Assert.AreEqual<bool>(true, target1.Status[1].HighPressure);
            Assert.AreEqual<bool>(true, target1.Status[2].HighPressure);

            // IsMagazineArrived
            Assert.IsFalse(target1.IsMagazineArrived);
        }

        #endregion

        #region GetFullStatus method tests

        [TestMethod()]
        public void GetFullStatusTest()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            string read = string.Empty;
            plcComm.Setup(x => x.Read()).Returns(() => { return read; });
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback<string>((s) =>
            {
                if (s.StartsWith("\u000500FFCR000140060C"))
                    read = "\u000200FF000100000001000100050003\u0003" + PlcStream.CalculateCheckSum("00FF000100000001000100050003\u0003");
                else if (s.StartsWith("\u000500FFCR0001503711"))
                    read = "\u0002" + PlcHelper.GetPolishingStream(0) + PlcStream.CalculateCheckSum(PlcHelper.GetPolishingStream(0));
                else if (s.StartsWith("\u000500FFCR0001903715"))
                    read = "\u0002" + PlcHelper.GetPolishingStream(1) + PlcStream.CalculateCheckSum(PlcHelper.GetPolishingStream(1));
                else if (s.StartsWith("\u000500FFCR0001D03720"))
                    read = "\u0002" + PlcHelper.GetPolishingStream(2) + PlcStream.CalculateCheckSum(PlcHelper.GetPolishingStream(2));
            });

            PolishLinePlc target = new PolishLinePlc(plcComm.Object);
            target.Open();

            IPolishingFullStatus target1 = target.GetFullStatus();
            
            // Status
            Assert.AreEqual<PolisherState>(PolisherState.AutoProcess, target1.Status[0].State);
            Assert.AreEqual<PolisherState>(PolisherState.Pause, target1.Status[1].State);
            Assert.AreEqual<PolisherState>(PolisherState.AutoLoad, target1.Status[2].State);
            
            // Magazine
            // Magazine's ID
            Assert.AreEqual<string>("DHFTPR8Q", target1.Status[0].Magazine.Id);
            Assert.AreEqual<string>("RTG8PS1P", target1.Status[1].Magazine.Id);
            Assert.AreEqual<string>("SDJR8ZS6", target1.Status[2].Magazine.Id);
           
            // Plates's ID
            Assert.AreEqual<string>("NDFH8PLF", target1.Status[0].Magazine.Plates[0].Id);
            Assert.AreEqual<string>("DKFYQ8EL", target1.Status[0].Magazine.Plates[1].Id);
            Assert.AreEqual<string>("WYEJF8F3", target1.Status[0].Magazine.Plates[2].Id);
            Assert.AreEqual<string>("PRYDGHF4", target1.Status[0].Magazine.Plates[3].Id);
            
            Assert.AreEqual<string>("VFYEQ41C", target1.Status[1].Magazine.Plates[0].Id);
            Assert.AreEqual<string>("APSD2EKJ", target1.Status[1].Magazine.Plates[1].Id);
            Assert.AreEqual<string>("JHDER7PM", target1.Status[1].Magazine.Plates[2].Id);
            Assert.AreEqual<string>("WKD95UAS", target1.Status[1].Magazine.Plates[3].Id);

            Assert.AreEqual<string>("AP5KFHEM", target1.Status[2].Magazine.Plates[0].Id);
            Assert.AreEqual<string>("VES7KGFT", target1.Status[2].Magazine.Plates[1].Id);
            Assert.AreEqual<string>("CIFGP3KJ", target1.Status[2].Magazine.Plates[2].Id);
            Assert.AreEqual<string>("WKD95UAS", target1.Status[2].Magazine.Plates[3].Id);

            // HighPressure
            Assert.AreEqual<bool>(true, target1.Status[0].HighPressure);
            Assert.AreEqual<bool>(false, target1.Status[1].HighPressure);
            Assert.AreEqual<bool>(true, target1.Status[2].HighPressure);

            // HighPressureDuration
            Assert.AreEqual<int>(0x38, target1.Status[0].HighPressureDuration.Milliseconds);
            Assert.AreEqual<int>(0x55, target1.Status[1].HighPressureDuration.Milliseconds);
            Assert.AreEqual<int>(0x4F, target1.Status[2].HighPressureDuration.Milliseconds);

            // PlateRpm
            Assert.AreEqual<int>(0x58F, target1.Status[0].PlateRpm);
            Assert.AreEqual<int>(0x694, target1.Status[1].PlateRpm);
            Assert.AreEqual<int>(0x5FE, target1.Status[2].PlateRpm);
            
            // PlateLoadCurrent
            Assert.AreEqual<double>(4.5, target1.Status[0].PlateLoadCurrent);
            Assert.AreEqual<double>(65.8, target1.Status[1].PlateLoadCurrent);
            Assert.AreEqual<double>(0.7, target1.Status[2].PlateLoadCurrent);

            // PolishingHeads
            // Force
            Assert.AreEqual<int>(0xA, target1.Status[0].PolisherHeads[0].Force);
            Assert.AreEqual<int>(0x14, target1.Status[0].PolisherHeads[1].Force);
            Assert.AreEqual<int>(0x1E, target1.Status[0].PolisherHeads[2].Force);
            Assert.AreEqual<int>(0x28, target1.Status[0].PolisherHeads[3].Force);

            Assert.AreEqual<int>(0x32, target1.Status[1].PolisherHeads[0].Force);
            Assert.AreEqual<int>(0x3C, target1.Status[1].PolisherHeads[1].Force);
            Assert.AreEqual<int>(0x46, target1.Status[1].PolisherHeads[2].Force);
            Assert.AreEqual<int>(0x50, target1.Status[1].PolisherHeads[3].Force);

            Assert.AreEqual<int>(0x5A, target1.Status[2].PolisherHeads[0].Force);
            Assert.AreEqual<int>(0x64, target1.Status[2].PolisherHeads[1].Force);
            Assert.AreEqual<int>(0x6E, target1.Status[2].PolisherHeads[2].Force);
            Assert.AreEqual<int>(0x78, target1.Status[2].PolisherHeads[3].Force);

            // Pressure
            Assert.AreEqual<double>(12.453, target1.Status[0].PolisherHeads[0].Pressure);
            Assert.AreEqual<double>(7.532, target1.Status[0].PolisherHeads[1].Pressure);
            Assert.AreEqual<double>(5, target1.Status[0].PolisherHeads[2].Pressure);
            Assert.AreEqual<double>(0.254, target1.Status[0].PolisherHeads[3].Pressure);

            Assert.AreEqual<double>(5.481, target1.Status[1].PolisherHeads[0].Pressure);
            Assert.AreEqual<double>(0.056, target1.Status[1].PolisherHeads[1].Pressure);
            Assert.AreEqual<double>(0.874, target1.Status[1].PolisherHeads[2].Pressure);
            Assert.AreEqual<double>(1, target1.Status[1].PolisherHeads[3].Pressure);

            Assert.AreEqual<double>(12.543, target1.Status[2].PolisherHeads[0].Pressure);
            Assert.AreEqual<double>(22.684, target1.Status[2].PolisherHeads[1].Pressure);
            Assert.AreEqual<double>(0.001, target1.Status[2].PolisherHeads[2].Pressure);
            Assert.AreEqual<double>(1.425, target1.Status[2].PolisherHeads[3].Pressure);

            // Backpressure
            Assert.AreEqual<double>(5.47, target1.Status[0].PolisherHeads[0].Backpressure);
            Assert.AreEqual<double>(0.13, target1.Status[0].PolisherHeads[1].Backpressure);
            Assert.AreEqual<double>(58.74, target1.Status[0].PolisherHeads[2].Backpressure);
            Assert.AreEqual<double>(3.25, target1.Status[0].PolisherHeads[3].Backpressure);

            Assert.AreEqual<double>(0.05, target1.Status[1].PolisherHeads[0].Backpressure);
            Assert.AreEqual<double>(413.54, target1.Status[1].PolisherHeads[1].Backpressure);
            Assert.AreEqual<double>(95.43, target1.Status[1].PolisherHeads[2].Backpressure);
            Assert.AreEqual<double>(1.62, target1.Status[1].PolisherHeads[3].Backpressure);

            Assert.AreEqual<double>(0.99, target1.Status[2].PolisherHeads[0].Backpressure);
            Assert.AreEqual<double>(9.34, target1.Status[2].PolisherHeads[1].Backpressure);
            Assert.AreEqual<double>(67.49, target1.Status[2].PolisherHeads[2].Backpressure);
            Assert.AreEqual<double>(0, target1.Status[2].PolisherHeads[3].Backpressure);

            // Rpm
            Assert.AreEqual<int>(0x4B0, target1.Status[0].PolisherHeads[0].Rpm);
            Assert.AreEqual<int>(0x5DC, target1.Status[0].PolisherHeads[1].Rpm);
            Assert.AreEqual<int>(0x708, target1.Status[0].PolisherHeads[2].Rpm);
            Assert.AreEqual<int>(0x834, target1.Status[0].PolisherHeads[3].Rpm);

            Assert.AreEqual<int>(0x960, target1.Status[1].PolisherHeads[0].Rpm);
            Assert.AreEqual<int>(0xA8C, target1.Status[1].PolisherHeads[1].Rpm);
            Assert.AreEqual<int>(0xBB8, target1.Status[1].PolisherHeads[2].Rpm);
            Assert.AreEqual<int>(0xCE4, target1.Status[1].PolisherHeads[3].Rpm);

            Assert.AreEqual<int>(0xE10, target1.Status[2].PolisherHeads[0].Rpm);
            Assert.AreEqual<int>(0xF3C, target1.Status[2].PolisherHeads[1].Rpm);
            Assert.AreEqual<int>(0xEA6, target1.Status[2].PolisherHeads[2].Rpm);
            Assert.AreEqual<int>(0xDDE, target1.Status[2].PolisherHeads[3].Rpm);

            // LoadCurrent
            Assert.AreEqual<double>(0.8, target1.Status[0].PolisherHeads[0].LoadCurrent);
            Assert.AreEqual<double>(6.8, target1.Status[0].PolisherHeads[1].LoadCurrent);
            Assert.AreEqual<double>(13.8, target1.Status[0].PolisherHeads[2].LoadCurrent);
            Assert.AreEqual<double>(94.8, target1.Status[0].PolisherHeads[3].LoadCurrent);

            Assert.AreEqual<double>(2, target1.Status[1].PolisherHeads[0].LoadCurrent);
            Assert.AreEqual<double>(0, target1.Status[1].PolisherHeads[1].LoadCurrent);
            Assert.AreEqual<double>(15.6, target1.Status[1].PolisherHeads[2].LoadCurrent);
            Assert.AreEqual<double>(1654.7, target1.Status[1].PolisherHeads[3].LoadCurrent);

            Assert.AreEqual<double>(1, target1.Status[2].PolisherHeads[0].LoadCurrent);
            Assert.AreEqual<double>(42.8, target1.Status[2].PolisherHeads[1].LoadCurrent);
            Assert.AreEqual<double>(91, target1.Status[2].PolisherHeads[2].LoadCurrent);
            Assert.AreEqual<double>(0.1, target1.Status[2].PolisherHeads[3].LoadCurrent);
            
            // PolishingLiquid
            Assert.AreEqual<double>(13.2, target1.Status[0].PolisherLiquid.PadTemp);
            Assert.AreEqual<double>(18.9, target1.Status[1].PolisherLiquid.PadTemp);
            Assert.AreEqual<double>(21.3, target1.Status[2].PolisherLiquid.PadTemp);

            Assert.AreEqual<double>(9.8, target1.Status[0].PolisherLiquid.CoolingWaterInTemp);
            Assert.AreEqual<double>(11.1, target1.Status[1].PolisherLiquid.CoolingWaterInTemp);
            Assert.AreEqual<double>(35.8, target1.Status[2].PolisherLiquid.CoolingWaterInTemp);

            Assert.AreEqual<double>(14.7, target1.Status[0].PolisherLiquid.CoolingWaterOutTemp);
            Assert.AreEqual<double>(22.2, target1.Status[1].PolisherLiquid.CoolingWaterOutTemp);
            Assert.AreEqual<double>(74.1, target1.Status[2].PolisherLiquid.CoolingWaterOutTemp);

            Assert.AreEqual<double>(15.9, target1.Status[0].PolisherLiquid.SlurryInTemp);
            Assert.AreEqual<double>(34.1, target1.Status[1].PolisherLiquid.SlurryInTemp);
            Assert.AreEqual<double>(36.0, target1.Status[2].PolisherLiquid.SlurryInTemp);

            Assert.AreEqual<double>(35.7, target1.Status[0].PolisherLiquid.SlurryOutTemp);
            Assert.AreEqual<double>(42.5, target1.Status[1].PolisherLiquid.SlurryOutTemp);
            Assert.AreEqual<double>(0.1, target1.Status[2].PolisherLiquid.SlurryOutTemp);

            Assert.AreEqual<double>(45.6, target1.Status[0].PolisherLiquid.RinseTemp);
            Assert.AreEqual<double>(63.2, target1.Status[1].PolisherLiquid.RinseTemp);
            Assert.AreEqual<double>(80.3, target1.Status[2].PolisherLiquid.RinseTemp);

            Assert.AreEqual<double>(32.4, target1.Status[0].PolisherLiquid.CoolingWaterAmount);
            Assert.AreEqual<double>(25.8, target1.Status[1].PolisherLiquid.CoolingWaterAmount);
            Assert.AreEqual<double>(8.7, target1.Status[2].PolisherLiquid.CoolingWaterAmount);

            Assert.AreEqual<double>(26.7, target1.Status[0].PolisherLiquid.SlurryAmount);
            Assert.AreEqual<double>(0.6, target1.Status[1].PolisherLiquid.SlurryAmount);
            Assert.AreEqual<double>(13.5, target1.Status[2].PolisherLiquid.SlurryAmount);

            Assert.AreEqual<double>(65.2, target1.Status[0].PolisherLiquid.RinseAmount);
            Assert.AreEqual<double>(1.0, target1.Status[1].PolisherLiquid.RinseAmount);
            Assert.AreEqual<double>(96.3, target1.Status[2].PolisherLiquid.RinseAmount);

            // PadUsedTime
            Assert.AreEqual<int>(0x3DE, target1.Status[0].PadUsedTime.Milliseconds);
            Assert.AreEqual<int>(0x36B, target1.Status[1].PadUsedTime.Milliseconds);
            Assert.AreEqual<int>(0x3D0, target1.Status[2].PadUsedTime.Milliseconds);

            // PadUsedCount
            Assert.AreEqual<int>(0x233, target1.Status[0].PadUsedCount);
            Assert.AreEqual<int>(0x281, target1.Status[1].PadUsedCount);
            Assert.AreEqual<int>(0x247, target1.Status[2].PadUsedCount);
        }

        #endregion

        #region ProcessRecipe method tests

        [TestMethod()]
        public void ProcessRecipe1Test()
        {
            Mock<IMagazine> magazine = new Mock<IMagazine>();
            magazine.Setup(x => x.Id).Returns("FHT5E");
            magazine.Setup(x => x.Plates).Returns(PlcHelper.GetPlateList(1, "UFH4E", "ERT7A", "CP7F6", "QAKGT"));

            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            PolishLinePlc target = new PolishLinePlc(plcComm.Object);

            target.Open();
            target.ProcessRecipe(magazine.Object);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetWriteCommandProcessRecipeString(magazine.Object)), Times.Exactly(1));
        }

        #endregion

        #region WriteBarcodeError method tests

        [TestMethod()]
        public void WriteBarcodeError1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            PolishLinePlc target = new PolishLinePlc(plcComm.Object);

            target.Open();
            target.WriteBarcodeError(true);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(true, 0x121)), Times.Exactly(1));
        }

        [TestMethod()]
        public void WriteBarcodeError2Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            PolishLinePlc target = new PolishLinePlc(plcComm.Object);

            target.Open();
            target.WriteBarcodeError(false);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(false, 0x121)), Times.Exactly(1));
        }

        #endregion
    }
}