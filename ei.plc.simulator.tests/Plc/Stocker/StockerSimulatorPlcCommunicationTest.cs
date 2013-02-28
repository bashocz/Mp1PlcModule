using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class StockerSimulatorPlcCommunicationTest
    {
        #region private members

        private int errorCode;

        #endregion

        #region CreateObjects method tests

        [TestMethod()]
        public void CreateObjects1Test()
        {
            StockerSimulatorPlcCommunication target = StockerSimulatorPlcCommunication.Create();

            Assert.IsNotNull(target.IsCarrierPlateArrived);
            Assert.IsNotNull(target.BarcodeError);
            Assert.IsNotNull(target.CarrierPlateRouting);
            Assert.IsNotNull(target.IsMagazineFull);
            Assert.IsNotNull(target.IsOperatorChangeRequest);
            Assert.IsNotNull(target.IsMagazineChange);
            Assert.IsNotNull(target.IsMagazineChangeStarted);
            Assert.IsNotNull(target.IsInputMagazineBarcodeOK);
            Assert.IsNotNull(target.WaferSize);
            Assert.IsNotNull(target.IsMagazineRequested);
            Assert.IsNotNull(target.StockerInventory);
            Assert.IsNotNull(target.IsMagazineArrived);
            Assert.IsNotNull(target.Selection);
        }

        #endregion

        #region CheckWriteCommand method tests

        [TestMethod()]
        public void CheckWriteCommand1Test()
        {
            StockerSimulatorPlcCommunication privateTarget = StockerSimulatorPlcCommunication.Create();
            StockerSimulatorPlcCommunication_Accessor target = new StockerSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(true, target.CheckForErrorWriteCommand("\u000500FFCW000120151111222233334444555566667777888899990000AAAABBBBCCCCDDDDEEEE7F", out errorCode));
            Assert.AreEqual<int>(-1, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand2Test()
        {
            StockerSimulatorPlcCommunication privateTarget = StockerSimulatorPlcCommunication.Create();
            StockerSimulatorPlcCommunication_Accessor target = new StockerSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW00011F01ABCD29", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand3Test()
        {
            StockerSimulatorPlcCommunication privateTarget = StockerSimulatorPlcCommunication.Create();
            StockerSimulatorPlcCommunication_Accessor target = new StockerSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW00013501ABCD1A", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        #endregion

        #region CheckReadCommand method tests

        [TestMethod()]
        public void CheckReadCommand1Test()
        {
            StockerSimulatorPlcCommunication privateTarget = StockerSimulatorPlcCommunication.Create();
            StockerSimulatorPlcCommunication_Accessor target = new StockerSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(true, target.CheckForErrorReadCommand("\u000500FFCR0001210409", out errorCode));
            Assert.AreEqual<int>(-1, errorCode);
        }

        [TestMethod()]
        public void CheckReadCommand2Test()
        {
            StockerSimulatorPlcCommunication privateTarget = StockerSimulatorPlcCommunication.Create();
            StockerSimulatorPlcCommunication_Accessor target = new StockerSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(true, target.CheckForErrorReadCommand("\u000500FFCR0001201409", out errorCode));
            Assert.AreEqual<int>(-1, errorCode);
        }

        [TestMethod()]
        public void CheckReadCommand3Test()
        {
            StockerSimulatorPlcCommunication privateTarget = StockerSimulatorPlcCommunication.Create();
            StockerSimulatorPlcCommunication_Accessor target = new StockerSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorReadCommand("\u000500FFCR00011F011A", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckReadCommand4Test()
        {
            StockerSimulatorPlcCommunication privateTarget = StockerSimulatorPlcCommunication.Create();
            StockerSimulatorPlcCommunication_Accessor target = new StockerSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorReadCommand("\u000500FFCR000135010B", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }  

        #endregion

        #region InitializeMemory method tests

        [TestMethod()]
        public void InitializeMemory1Test()
        {
            StockerSimulatorPlcCommunication privateTarget = StockerSimulatorPlcCommunication.Create();
            StockerSimulatorPlcCommunication_Accessor target = new StockerSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x120, 0x134, 0));
        }

        #endregion
    }
}
