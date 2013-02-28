using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class MountPlcTest
    {
        #region constants

        const int slotId = 1;

        #endregion

        #region private members

        private string GetWriteCommandClearNewLotStartDataString()
        {
            return (new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(), 0x127, new ClearingNewLotStartToStreamConverter().TryConvert(this))).CommandToString();
        }
        
        private string GetWriteCommandSetLotCassetteDataString(ILotData lotData)
        {
            return (new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(), 0x150, new LotCassetteInfoToStreamConverter().TryConvert(lotData))).CommandToString();
        }

        private string GetWriteCommandSetLotWaferDataString(ILotData lotData)
        {
            return (new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(), 0x189, new LotWaferInfoToStreamConverter().TryConvert(lotData))).CommandToString();
        }

        private string GetWriteCommandWafersDataString(IList<IWafer> wafers, int address)
        {
            return (new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(), address, new WafersToStreamConverter().TryConvert(wafers))).CommandToString();
        }

        private string GetWriteCommandLotDataTransmissionString(LotDataTransmission transmission)
        {
            return (new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(), 0x188, new LotDataTransmissionToStreamConverter().TryConvert(transmission))).CommandToString();
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
                if (s.StartsWith("\u000500FFCR0001202107"))
                    read = "\u0002" + PlcHelper.GetMountStatusStream(0) + PlcStream.CalculateCheckSum(PlcHelper.GetMountStatusStream(0));
            });

            MountPlc target = new MountPlc(plcComm.Object);
            target.Open();

            IMountStatus target1 = target.GetStatus();

            Assert.AreEqual<MountState>(MountState.AutoMount, target1.State);
            Assert.AreEqual<string>("ABCDEFGH", target1.NewLotCassette.CassetteId);
            Assert.AreEqual<bool>(true, target1.NewLotCassette.IsCassetteId);         
            Assert.AreEqual<bool>(false, target1.IsLotDataTimeout);
            Assert.AreEqual<string>("ABCDEFGHIJKLMN", target1.NewLotStarted.LotId);
            Assert.AreEqual<MountLine>(MountLine.Both, target1.NewLotStarted.Line);
            Assert.AreEqual<bool>(false, target1.NewLotStarted.IsLotStarted);
            Assert.AreEqual<bool>(true, target1.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(false, target1.IsCarrierPlateMountingReady);
            Assert.AreEqual<int>(10, target1.WaferBreakNumber);
            Assert.AreEqual<bool>(false, target1.IsMountingErrorCarrierPlate);
            Assert.AreEqual<bool>(false, target1.IsEndLot);
            Assert.AreEqual<bool>(false, target1.IsReservationLotCanceled);
        }

        [TestMethod()]
        public void GetStatus2Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            string read = string.Empty;
            plcComm.Setup(x => x.Read()).Returns(() => { return read; });
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback<string>((s) =>
            {
                if (s.StartsWith("\u000500FFCR0001202107"))
                    read = "\u0002" + PlcHelper.GetMountStatusStream(1) + PlcStream.CalculateCheckSum(PlcHelper.GetMountStatusStream(1));
            });

            MountPlc target = new MountPlc(plcComm.Object);
            target.Open();

            IMountStatus target1 = target.GetStatus();

            Assert.AreEqual<MountState>(MountState.AutoMountAlarm, target1.State);
            Assert.AreEqual<string>("IJKLMNOP", target1.NewLotCassette.CassetteId);
            Assert.AreEqual<bool>(false, target1.NewLotCassette.IsCassetteId);
            Assert.AreEqual<bool>(true, target1.IsLotDataTimeout);
            Assert.AreEqual<string>("KCGE8PAQ1HC4HF", target1.NewLotStarted.LotId);
            Assert.AreEqual<MountLine>(MountLine.Left, target1.NewLotStarted.Line);
            Assert.AreEqual<bool>(true, target1.NewLotStarted.IsLotStarted);
            Assert.AreEqual<bool>(false, target1.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(true, target1.IsCarrierPlateMountingReady);
            Assert.AreEqual<int>(15, target1.WaferBreakNumber);
            Assert.AreEqual<bool>(true, target1.IsMountingErrorCarrierPlate);
            Assert.AreEqual<bool>(true, target1.IsEndLot);
            Assert.AreEqual<bool>(true, target1.IsReservationLotCanceled);
        }

        [TestMethod()]
        public void GetStatus3Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            string read = string.Empty;
            plcComm.Setup(x => x.Read()).Returns(() => { return read; });
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback<string>((s) =>
            {
                if (s.StartsWith("\u000500FFCR0001202107"))
                    read = "\u0002" + PlcHelper.GetMountStatusStream(2) + PlcStream.CalculateCheckSum(PlcHelper.GetMountStatusStream(2));
            });

            MountPlc target = new MountPlc(plcComm.Object);
            target.Open();

            IMountStatus target1 = target.GetStatus();

            Assert.AreEqual<MountState>(MountState.StopAlarm, target1.State);
            Assert.AreEqual<string>("LDPW8X2D", target1.NewLotCassette.CassetteId);
            Assert.AreEqual<bool>(true, target1.NewLotCassette.IsCassetteId);
            Assert.AreEqual<bool>(false, target1.IsLotDataTimeout);
            Assert.AreEqual<string>("MZQPALJFIR2JCS", target1.NewLotStarted.LotId);
            Assert.AreEqual<MountLine>(MountLine.Right, target1.NewLotStarted.Line);
            Assert.AreEqual<bool>(false, target1.NewLotStarted.IsLotStarted);
            Assert.AreEqual<bool>(true, target1.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(true, target1.IsCarrierPlateMountingReady);
            Assert.AreEqual<int>(0, target1.WaferBreakNumber);
            Assert.AreEqual<bool>(false, target1.IsMountingErrorCarrierPlate);
            Assert.AreEqual<bool>(false, target1.IsEndLot);
            Assert.AreEqual<bool>(true, target1.IsReservationLotCanceled);
        }

        #endregion

        #region AcceptNewLot method tests

        [TestMethod()]
        public void AcceptNewLotTest()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            MountPlc target = new MountPlc(plcComm.Object);

            target.Open();
            target.AcceptNewLot(false);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(true, 0x125)), Times.Exactly(1));
        }

        #endregion

        #region SetLotData method tests

        [TestMethod()]
        public void SetLotData1Test()
        {
            List<IWafer> listOfWafers = PlcHelper.GetWaferList(3, 1);

            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            MountPlc target = new MountPlc(plcComm.Object);
            ILotData lotData = PlcHelper.GetLotData("ABCDEFGHIJKLMN", PlcHelper.GetCassetteList(1), listOfWafers);

            target.Open();
            target.SetLotData(lotData);
            
            plcComm.Verify(x => x.Write(GetWriteCommandLotDataTransmissionString(LotDataTransmission.BeforeWritingCassetteInfo)), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetWriteCommandSetLotCassetteDataString(lotData)), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetWriteCommandLotDataTransmissionString(LotDataTransmission.BeforeWritingWaferInfo)), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetWriteCommandSetLotWaferDataString(lotData)), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetWriteCommandWafersDataString(PlcHelper.GetWaferSubList(listOfWafers, 0, 32), 0x200)), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetWriteCommandLotDataTransmissionString(LotDataTransmission.Cleared)), Times.Exactly(1));
        }

        [TestMethod()]
        public void SetLotData2Test()
        {
            List<IWafer> listOfWafers = PlcHelper.GetWaferList(72, 3);

            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            MountPlc target = new MountPlc(plcComm.Object);
            ILotData lotData = PlcHelper.GetLotData("ABCDEFGHIJKLMN", PlcHelper.GetCassetteList(3), listOfWafers);

            target.Open();
            target.SetLotData(lotData);

            plcComm.Verify(x => x.Write(PlcHelper.GetIntWriteCommand(1, 0x188)), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetWriteCommandSetLotCassetteDataString(lotData)), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetIntWriteCommand(2, 0x188)), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetWriteCommandSetLotWaferDataString(lotData)), Times.Exactly(1));
            for (int i = 0; i < 3; i++)
            {
                plcComm.Verify(x => x.Write(GetWriteCommandWafersDataString(PlcHelper.GetWaferSubList(listOfWafers, 0 + i * 32, 32), 0x200 + i * 0x40)), Times.Exactly(1));
            }
            plcComm.Verify(x => x.Write(PlcHelper.GetIntWriteCommand(0, 0x188)), Times.Exactly(1));
        }

        [TestMethod()]
        public void SetLotData3Test()
        {
            List<IWafer> listOfWafers = PlcHelper.GetWaferList(300, 12);

            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            MountPlc target = new MountPlc(plcComm.Object);
            ILotData lotData = PlcHelper.GetLotData("ABCDEFGHIJKLMN", PlcHelper.GetCassetteList(12), listOfWafers);

            target.Open();
            target.SetLotData(lotData);

            plcComm.Verify(x => x.Write(PlcHelper.GetIntWriteCommand(1, 0x188)), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetWriteCommandSetLotCassetteDataString(lotData)), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetIntWriteCommand(2, 0x188)), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetWriteCommandSetLotWaferDataString(lotData)), Times.Exactly(1));
            for (int i = 0; i < 10; i++)
            {
                plcComm.Verify(x => x.Write(GetWriteCommandWafersDataString(PlcHelper.GetWaferSubList(listOfWafers, 0 + i * 32, 32), 0x200 + i * 0x40)), Times.Exactly(1));
            }
            plcComm.Verify(x => x.Write(PlcHelper.GetIntWriteCommand(0, 0x188)), Times.Exactly(1));
        }

        #endregion

        #region CarrierPlateBarcodeSuccesfullyReaded method tests

        [TestMethod()]
        public void CarrierPlateBarcodeSuccesfullyReaded1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            MountPlc target = new MountPlc(plcComm.Object);

            target.Open();
            target.CarrierPlateBarcodeSuccesfullyReaded();

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(true, 0x131)), Times.Exactly(1));
        }

        #endregion

        #region AcceptWaferBreakNumber method tests

        [TestMethod()]
        public void AcceptWaferBreakNumber1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            MountPlc target = new MountPlc(plcComm.Object);

            target.Open();
            target.AcceptWaferBreakNumber();

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(true, 0x134)), Times.Exactly(1));
        }

        #endregion

        #region ClearNewLotStartData method tests

        [TestMethod()]
        public void ClearNewLotStartData1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            MountPlc target = new MountPlc(plcComm.Object);

            target.Open();
            target.ClearNewLotStartData();

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetWriteCommandClearNewLotStartDataString()), Times.Exactly(1));
        }

        #endregion

        #region ClearCarrierPlateMountingReadyFlag method tests

        [TestMethod()]
        public void ClearCarrierPlateMountingReadyFlag1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            MountPlc target = new MountPlc(plcComm.Object);

            target.Open();
            target.ClearCarrierPlateMountingReadyFlag();

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(false, 0x132)), Times.Exactly(1));
        }

        #endregion

        #region ClearMountingErrorCarrierPlateFlag method tests

        [TestMethod()]
        public void ClearMountingErrorCarrierPlateFlag1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            MountPlc target = new MountPlc(plcComm.Object);

            target.Open();
            target.ClearMountingErrorCarrierPlateFlag();

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(false, 0x135)), Times.Exactly(1));
        }

        #endregion

        #region ClearLotEndFlag method tests

        [TestMethod()]
        public void ClearLotEndFlag1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            MountPlc target = new MountPlc(plcComm.Object);

            target.Open();
            target.ClearLotEndFlag();

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(false, 0x136)), Times.Exactly(1));
        }

        #endregion

        #region ClearReservationLotCancelFlag method tests

        [TestMethod()]
        public void ClearReservationLotCancelFlag1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            MountPlc target = new MountPlc(plcComm.Object);

            target.Open();
            target.ClearReservationLotCancelFlag();

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(false, 0x137)), Times.Exactly(1));
        }

        #endregion

        #region WriteBarcodeError method tests

        [TestMethod()]
        public void WriteBarcodeError1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            MountPlc target = new MountPlc(plcComm.Object);

            target.Open();
            target.WriteBarcodeError(true);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(true, 0x141)), Times.Exactly(1));
        }

        [TestMethod()]
        public void WriteBarcodeError2Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            MountPlc target = new MountPlc(plcComm.Object);

            target.Open();
            target.WriteBarcodeError(false);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(false, 0x141)), Times.Exactly(1));
        }

        #endregion
    }
}
