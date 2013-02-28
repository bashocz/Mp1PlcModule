using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class StockerPlcTest
    {
        #region private members

        public static string GetAcceptCarrierPlateCommand(CarrierPlateRouting routing)
        {
            return (new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(), 0x122, new CarrierPlateRoutingToStreamConverter().TryConvert(routing))).CommandToString();
        }

        public static string GetSetWaferSizeAvailableCommand(StockerInventory inventory)
        {
            return (new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(), 0x132, new StockerInventoryToStreamConverter().TryConvert(inventory))).CommandToString();
        }

        public static string GetAcceptMagazineCommand(MagazineSelection selection)
        {
            return (new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(), 0x134, new MagazineSelectionToStreamConverter().TryConvert(selection))).CommandToString();
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
                if (s.StartsWith("\u000500FFCR00012016"))
                    read = "\u0002" + PlcHelper.GetStockerStatusStream(0) + PlcStream.CalculateCheckSum(PlcHelper.GetStockerStatusStream(0));
            });

            StockerPlc target = new StockerPlc(plcComm.Object);
            target.Open();

            IStockerStatus target1 = target.GetStatus();

            Assert.AreEqual<bool>(true, target1.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(false, target1.MagazineChangeRequest.IsMagazineFull);
            Assert.AreEqual<bool>(false, target1.MagazineChangeRequest.IsOperatorChangeRequest);
            Assert.AreEqual<bool>(true, target1.IsMagazineChangeStarted);
            Assert.AreEqual<WaferSize>(WaferSize.Size6Inches, target1.MagazineRequest.WaferSize);
            Assert.AreEqual<bool>(true, target1.MagazineRequest.IsRequested);
            Assert.AreEqual<int>(1, target1.MagazineRequest.PolishLineNumber);
            Assert.AreEqual<bool>(true, target1.IsMagazineArrived);
            Assert.AreEqual<MagazineSelection>(MagazineSelection.Cleared, target1.MagazineSelection);
        }

        [TestMethod()]
        public void GetStatus2Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            string read = string.Empty;
            plcComm.Setup(x => x.Read()).Returns(() => { return read; });
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback<string>((s) =>
            {
                if (s.StartsWith("\u000500FFCR00012016"))
                    read = "\u0002" + PlcHelper.GetStockerStatusStream(1) + PlcStream.CalculateCheckSum(PlcHelper.GetStockerStatusStream(1));
            });

            StockerPlc target = new StockerPlc(plcComm.Object);
            target.Open();

            IStockerStatus target1 = target.GetStatus();

            Assert.AreEqual<bool>(false, target1.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(true, target1.MagazineChangeRequest.IsMagazineFull);
            Assert.AreEqual<bool>(false, target1.MagazineChangeRequest.IsOperatorChangeRequest);
            Assert.AreEqual<bool>(false, target1.IsMagazineChangeStarted);
            Assert.AreEqual<WaferSize>(WaferSize.Size8Inches, target1.MagazineRequest.WaferSize);
            Assert.AreEqual<int>(2, target1.MagazineRequest.PolishLineNumber);
            Assert.AreEqual<bool>(true, target1.MagazineRequest.IsRequested);
            Assert.AreEqual<bool>(false, target1.IsMagazineArrived);
            Assert.AreEqual<MagazineSelection>(MagazineSelection.HasRequestedSize, target1.MagazineSelection);
        }

        [TestMethod()]
        public void GetStatus3Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            string read = string.Empty;
            plcComm.Setup(x => x.Read()).Returns(() => { return read; });
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback<string>((s) =>
            {
                if (s.StartsWith("\u000500FFCR00012016"))
                    read = "\u0002" + PlcHelper.GetStockerStatusStream(2) + PlcStream.CalculateCheckSum(PlcHelper.GetStockerStatusStream(2));
            });

            StockerPlc target = new StockerPlc(plcComm.Object);
            target.Open();

            IStockerStatus target1 = target.GetStatus();

            Assert.AreEqual<bool>(true, target1.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(false, target1.MagazineChangeRequest.IsMagazineFull);
            Assert.AreEqual<bool>(true, target1.MagazineChangeRequest.IsOperatorChangeRequest);
            Assert.AreEqual<bool>(true, target1.IsMagazineChangeStarted);
            Assert.AreEqual<WaferSize>(WaferSize.Size6Inches, target1.MagazineRequest.WaferSize);
            Assert.AreEqual<int>(3, target1.MagazineRequest.PolishLineNumber);
            Assert.AreEqual<bool>(false, target1.MagazineRequest.IsRequested);
            Assert.AreEqual<bool>(false, target1.IsMagazineArrived);
            Assert.AreEqual<MagazineSelection>(MagazineSelection.DoesNotHaveRequestedSize, target1.MagazineSelection);
        }

        #endregion

        #region AcceptCarrierPlate method tests

        [TestMethod()]
        public void AcceptCarrierPlate1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            StockerPlc target = new StockerPlc(plcComm.Object);

            target.Open();
            target.AcceptCarrierPlate(CarrierPlateRouting.InsertIntoMagazine);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetAcceptCarrierPlateCommand(CarrierPlateRouting.InsertIntoMagazine)), Times.Exactly(1));
        }

        [TestMethod()]
        public void AcceptCarrierPlate2Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            StockerPlc target = new StockerPlc(plcComm.Object);

            target.Open();
            target.AcceptCarrierPlate(CarrierPlateRouting.Reject);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetAcceptCarrierPlateCommand(CarrierPlateRouting.Reject)), Times.Exactly(1));
        }

        #endregion

        #region MagazineChange method tests

        [TestMethod()]
        public void MagazineChangeTest()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            StockerPlc target = new StockerPlc(plcComm.Object);

            target.Open();
            target.MagazineChange();

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(true, 0x125)), Times.Exactly(1));
        }

        #endregion

        #region MagazineBarcodeSuccesfullyReaded method tests

        [TestMethod()]
        public void MagazineBarcodeSuccesfullyReaded1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            StockerPlc target = new StockerPlc(plcComm.Object);

            target.Open();
            target.MagazineBarcodeSuccesfullyReaded();

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(true, 0x127)), Times.Exactly(1));
        }

        #endregion

        #region SetWaferSizeAvailable method tests

        [TestMethod()]
        public void SetWaferSizeAvailable1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            StockerPlc target = new StockerPlc(plcComm.Object);

            target.Open();
            target.SetWaferSizeAvailable(StockerInventory.SizeAvailable);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetSetWaferSizeAvailableCommand(StockerInventory.SizeAvailable)), Times.Exactly(1));
        }

        [TestMethod()]
        public void SetWaferSizeAvailable2Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            StockerPlc target = new StockerPlc(plcComm.Object);

            target.Open();
            target.SetWaferSizeAvailable(StockerInventory.SizeNotInStocker);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetSetWaferSizeAvailableCommand(StockerInventory.SizeNotInStocker)), Times.Exactly(1));
        }

        #endregion

        #region AcceptMagazine method tests

        [TestMethod()]
        public void AcceptMagazine1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            StockerPlc target = new StockerPlc(plcComm.Object);

            target.Open();
            target.AcceptMagazine(MagazineSelection.HasRequestedSize);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetAcceptMagazineCommand(MagazineSelection.HasRequestedSize)), Times.Exactly(1));
        }

        [TestMethod()]
        public void AcceptMagazine2Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            StockerPlc target = new StockerPlc(plcComm.Object);

            target.Open();
            target.AcceptMagazine(MagazineSelection.DoesNotHaveRequestedSize);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(GetAcceptMagazineCommand(MagazineSelection.DoesNotHaveRequestedSize)), Times.Exactly(1));
        }

        #endregion

        #region WriteBarcodeError method tests

        [TestMethod()]
        public void WriteBarcodeError1Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            StockerPlc privateTarget = new StockerPlc(plcComm.Object);
            StockerPlc_Accessor target = new StockerPlc_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerPlc))));

            target.Open();
            target.WriteBarcodeError(false);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(false, 0x121)), Times.Exactly(1));
        }

        [TestMethod()]
        public void WriteBarcodeError2Test()
        {
            Mock<ICommunication> plcComm = PlcHelper.GetPlcCommunicationMock();
            StockerPlc privateTarget = new StockerPlc(plcComm.Object);
            StockerPlc_Accessor target = new StockerPlc_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerPlc))));

            target.Open();
            target.WriteBarcodeError(true);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(PlcHelper.GetBoolWriteCommand(true, 0x121)), Times.Exactly(1));
        }

        #endregion
    }
}