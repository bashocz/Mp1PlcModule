using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class DemountSimulatorPlcCommunicationTest
    {
        #region private members

        private int errorCode;

        #endregion

        #region CreateObjects method tests

        [TestMethod()]
        public void CreateObjects1Test()
        {
            DemountSimulatorPlcCommunication target = DemountSimulatorPlcCommunication.Create();

            Assert.IsNotNull(target.IsCarrierPlateArrived);
            Assert.IsNotNull(target.IsInformationSystemError);
            Assert.IsNotNull(target.IsCarrierPlateBarcodeReadedOk);
            Assert.IsNotNull(target.IsCarrierPlateDemountStarted);
            Assert.IsNotNull(target.CarrierPlateWaferSize);
            Assert.IsNotNull(target.CarrierPlateWaferCount);
            Assert.IsNotNull(target.DemountCassetteStation);
            Assert.IsNotNull(target.WaferDemountCounter);
            Assert.IsNotNull(target.IsCarrierPlateDemounted);
            Assert.IsNotNull(target.CarrierPlateRoutingType);
            Assert.IsNotNull(target.RemoveCassetteFromDemountStation);
            Assert.IsNotNull(target.CassetteWaferSize);
            Assert.IsNotNull(target.DestinationStation);
            Assert.IsNotNull(target.CanReadCassetteBarcode);
            Assert.IsNotNull(target.IsCassetteBarcodeReadedOk);
            Assert.IsNotNull(target.ShouldInspectSpatula);
            Assert.IsNotNull(target.IsCassette);
            Assert.IsNotNull(target.State);
        }

        #endregion

        #region CheckWriteCommand method tests

        [TestMethod()]
        public void CheckWriteCommand1Test()
        {
            DemountSimulatorPlcCommunication privateTarget = DemountSimulatorPlcCommunication.Create();
            DemountSimulatorPlcCommunication_Accessor target = new DemountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(true, target.CheckForErrorWriteCommand("\u000500FFCR00012901ABCD18", out errorCode));
            Assert.AreEqual<int>(-1, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand2Test()
        {
            DemountSimulatorPlcCommunication privateTarget = DemountSimulatorPlcCommunication.Create();
            DemountSimulatorPlcCommunication_Accessor target = new DemountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(true, target.CheckForErrorWriteCommand("\u000500FFCR000140010001C8", out errorCode));
            Assert.AreEqual<int>(-1, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand3Test()
        {
            DemountSimulatorPlcCommunication privateTarget = DemountSimulatorPlcCommunication.Create();
            DemountSimulatorPlcCommunication_Accessor target = new DemountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCR00011F012222E2", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand4Test()
        {
            DemountSimulatorPlcCommunication privateTarget = DemountSimulatorPlcCommunication.Create();
            DemountSimulatorPlcCommunication_Accessor target = new DemountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCR000146015555E1", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        #endregion

        #region CheckReadCommand method tests

        [TestMethod()]
        public void CheckReadCommand1Test()
        {
            DemountSimulatorPlcCommunication privateTarget = DemountSimulatorPlcCommunication.Create();
            DemountSimulatorPlcCommunication_Accessor target = new DemountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(true, target.CheckForErrorReadCommand("\u000500FFCR000120260C", out errorCode));
            Assert.AreEqual<int>(-1, errorCode);
        }

        [TestMethod()]
        public void CheckReadCommand2Test()
        {
            DemountSimulatorPlcCommunication privateTarget = DemountSimulatorPlcCommunication.Create();
            DemountSimulatorPlcCommunication_Accessor target = new DemountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorReadCommand("\u000500FFCR00011F011A", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckReadCommand3Test()
        {
            DemountSimulatorPlcCommunication privateTarget = DemountSimulatorPlcCommunication.Create();
            DemountSimulatorPlcCommunication_Accessor target = new DemountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorReadCommand("\u000500FFCR000146010D", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        #endregion
        
        #region InitializeMemory method tests

        [TestMethod()]
        public void InitializeMemory1Test()
        {
            DemountSimulatorPlcCommunication privateTarget = DemountSimulatorPlcCommunication.Create();
            DemountSimulatorPlcCommunication_Accessor target = new DemountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountSimulatorPlcCommunication))));

            Assert.AreEqual<ushort>(3, target._memory._memory[0x145]);

            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x120, 0x129, 0));
            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x130, 0x134, 0));
            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x140, 0x144, 0));
        }

        #endregion
    }
}
