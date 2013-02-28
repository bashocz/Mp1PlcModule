using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class DemountPlcTest
    {
        #region private members

        private string GetWriteCommandStartDemountingString(WaferSize waferSize, int waferCount, DemountCassetteStation station)
        {
            return (new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(), 0x124, new StartDemountingToStreamConverter().TryConvert(new StartDemounting { Size = waferSize, Count = waferCount, Station = station }))).CommandToString();
        }

        private string GetWriteCommandCarrierPlateRouting(CarrierPlateRoutingType type)
        {
            return (new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(), 0x129, new EmptyCarrierPlateRoutingToStreamConverter().TryConvert(type))).CommandToString();
        }

        private string GetWriteCommandChangeCassette(DemountCassetteStation from, WaferSize waferSize, DemountCassetteHopper destination)
        {
            return (new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(), 0x130, new ChangeCassetteToStreamConverter().TryConvert(new ChangeCassette { Source = from, WaferSize = waferSize, Destination = destination }))).CommandToString();
        }

        #endregion

        #region GetStatus method tests

        [TestMethod()]
        public void GetStatus1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            string read = string.Empty;
            plcComm.Setup(x => x.Read()).Returns(() => { return read; });
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback<string>((s) =>
            {
                if (s.StartsWith("\u000500FFCR000120260C"))
                    read = "\u0002" + PlcHelper.GetDemountStatusStream(0)
                                    + PlcStream.CalculateCheckSum(PlcHelper.GetDemountStatusStream(0));
            });

            DemountPlc target = new DemountPlc(plcComm.Object);
            target.Open();

            IDemountStatus target1 = target.GetStatus();

            Assert.AreEqual<bool>(false, target1.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(false, target1.IsCarrierPlateDemountStarted);
            Assert.AreEqual<int>(2, target1.DemountInfo.DemountedWaferCount);
            Assert.AreEqual<bool>(false, target1.DemountInfo.Completed);
            Assert.AreEqual<DemountCassetteHopper>(DemountCassetteHopper.Hopper4, target1.CanReadCassetteBarcode);
            Assert.AreEqual<bool>(true, target1.AreCassettes[0]);
            Assert.AreEqual<bool>(false, target1.AreCassettes[1]);
            Assert.AreEqual<bool>(true, target1.AreCassettes[2]);
            Assert.AreEqual<bool>(false, target1.AreCassettes[3]);
            Assert.AreEqual<DemountState>(DemountState.AutoDemount, target1.State);
        }

        [TestMethod()]
        public void GetStatus2Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            string read = string.Empty;
            plcComm.Setup(x => x.Read()).Returns(() => { return read; });
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback<string>((s) =>
            {
                if (s.StartsWith("\u000500FFCR000120260C"))
                    read = "\u0002" + PlcHelper.GetDemountStatusStream(1)
                                    + PlcStream.CalculateCheckSum(PlcHelper.GetDemountStatusStream(1));
            });

            DemountPlc target = new DemountPlc(plcComm.Object);
            target.Open();

            IDemountStatus target1 = target.GetStatus();

            Assert.AreEqual<bool>(true, target1.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(true, target1.IsCarrierPlateDemountStarted);
            Assert.AreEqual<int>(3, target1.DemountInfo.DemountedWaferCount);
            Assert.AreEqual<bool>(true, target1.DemountInfo.Completed);
            Assert.AreEqual<DemountCassetteHopper>(DemountCassetteHopper.Hopper3, target1.CanReadCassetteBarcode);
            Assert.AreEqual<bool>(false, target1.AreCassettes[0]);
            Assert.AreEqual<bool>(true, target1.AreCassettes[1]);
            Assert.AreEqual<bool>(false, target1.AreCassettes[2]);
            Assert.AreEqual<bool>(false, target1.AreCassettes[3]);
            Assert.AreEqual<DemountState>(DemountState.Standby, target1.State);
        }

        [TestMethod()]
        public void GetStatus3Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            string read = string.Empty;
            plcComm.Setup(x => x.Read()).Returns(() => { return read; });
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback<string>((s) =>
            {
                if (s.StartsWith("\u000500FFCR000120260C"))
                    read = "\u0002" + PlcHelper.GetDemountStatusStream(2)
                                    + PlcStream.CalculateCheckSum(PlcHelper.GetDemountStatusStream(2));
            });

            DemountPlc target = new DemountPlc(plcComm.Object);
            target.Open();

            IDemountStatus target1 = target.GetStatus();

            Assert.AreEqual<bool>(false, target1.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(false, target1.IsCarrierPlateDemountStarted);
            Assert.AreEqual<int>(1, target1.DemountInfo.DemountedWaferCount);
            Assert.AreEqual<bool>(true, target1.DemountInfo.Completed);
            Assert.AreEqual<DemountCassetteHopper>(DemountCassetteHopper.Hopper2, target1.CanReadCassetteBarcode);
            Assert.AreEqual<bool>(true, target1.AreCassettes[0]);
            Assert.AreEqual<bool>(false, target1.AreCassettes[1]);
            Assert.AreEqual<bool>(true, target1.AreCassettes[2]);
            Assert.AreEqual<bool>(true, target1.AreCassettes[3]);
            Assert.AreEqual<DemountState>(DemountState.Stop, target1.State);
        }

        #endregion


        #region CarrierPlateBarcodeSuccesfullyReaded method tests

        [TestMethod()]
        public void CarrierPlateBarcodeSuccesfullyReaded1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            DemountPlc target = new DemountPlc(plcComm.Object);

            target.Open();
            target.CarrierPlateBarcodeSuccesfullyReaded();

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(true, 0x122)), Times.Exactly(1));
        }

        #endregion

        #region StartDemounting method tests

        [TestMethod()]
        public void StartDemountingTest()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            DemountPlc privateTarget = new DemountPlc(plcComm.Object);
            DemountPlc_Accessor target = new DemountPlc_Accessor(new PrivateObject(privateTarget,
                                                                 new PrivateType(typeof(DemountPlc))));

            target.Open();
            target.StartDemounting(WaferSize.Size8Inches, 4, DemountCassetteStation.Station1);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetWriteCommandStartDemountingString(WaferSize.Size8Inches, 4, DemountCassetteStation.Station1)), Times.Exactly(1));
        }

        #endregion


        #region CarrierPlateRouting method tests

        [TestMethod()]
        public void CarrierPlateRoutingTest()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            DemountPlc privateTarget = new DemountPlc(plcComm.Object);
            DemountPlc_Accessor target = new DemountPlc_Accessor(new PrivateObject(privateTarget,
                                                                 new PrivateType(typeof(DemountPlc))));

            target.Open();
            target.CarrierPlateRouting(CarrierPlateRoutingType.BackThroughAwps);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetWriteCommandCarrierPlateRouting(CarrierPlateRoutingType.BackThroughAwps)), Times.Exactly(1));
        }

        #endregion


        #region ChangeCassette method tests

        [TestMethod()]
        public void ChangeCassetteTest()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            DemountPlc privateTarget = new DemountPlc(plcComm.Object);
            DemountPlc_Accessor target = new DemountPlc_Accessor(new PrivateObject(privateTarget,
                                                                 new PrivateType(typeof(DemountPlc))));

            target.Open();
            target.ChangeCassette(DemountCassetteStation.Station1, WaferSize.Size8Inches, DemountCassetteHopper.Hopper1);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetWriteCommandChangeCassette(DemountCassetteStation.Station1, WaferSize.Size8Inches, DemountCassetteHopper.Hopper1)), Times.Exactly(1));
        }

        #endregion

        #region CassetteBarcodeSuccesfullyReaded method tests

        [TestMethod()]
        public void CassetteBarcodeSuccesfullyReaded1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            DemountPlc target = new DemountPlc(plcComm.Object);

            target.Open();
            target.CassetteBarcodeSuccesfullyRead();

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(true, 0x134)), Times.Exactly(1));
        }

        #endregion


        #region SpatulaInspectionRequired method tests

        [TestMethod()]
        public void SpatulaInspectionRequired1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            DemountPlc privateTarget = new DemountPlc(plcComm.Object);
            DemountPlc_Accessor target = new DemountPlc_Accessor(new PrivateObject(privateTarget,
                                                                 new PrivateType(typeof(DemountPlc))));

            target.Open();
            target.SpatulaInspectionRequired();

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(true, 0x140)), Times.Exactly(1));
        }

        #endregion

        #region WriteBarcodeError method tests

        [TestMethod()]
        public void WriteBarcodeError1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            DemountPlc_Accessor target = new DemountPlc_Accessor(plcComm.Object);

            target.Open();
            target.WriteBarcodeError(true);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(true, 0x121)), Times.Exactly(1));
        }

        [TestMethod()]
        public void WriteBarcodeError2Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            DemountPlc_Accessor target = new DemountPlc_Accessor(plcComm.Object);

            target.Open();
            target.WriteBarcodeError(false);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(false, 0x121)), Times.Exactly(1));
        }

        #endregion
    }
}
