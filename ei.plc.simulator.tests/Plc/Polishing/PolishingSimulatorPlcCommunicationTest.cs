using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PolishingSimulatorPlcCommunicationTest
    {
        #region private members

        private int errorCode;

        #endregion

        #region CreateObjects method tests

        [TestMethod()]
        public void CreateObjects1Test()
        {
            PolishingSimulatorPlcCommunication target = PolishingSimulatorPlcCommunication.Create();

            Assert.IsNotNull(target.MagazineArrived);
            Assert.IsNotNull(target.BarcodeError);
            Assert.IsNotNull(target.MagazineId);
            Assert.IsNotNull(target.Plates);     
            Assert.IsNotNull(target.Polishers);                 
        }

        #endregion

        #region CheckWriteCommand method tests

        [TestMethod()]
        public void CheckWriteCommand1Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW00011F01ABCD29", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand2Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW00013A02ABCD1234F1", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand3Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW00013F01ABCD2B", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand4Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW00014601ABCD1C", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand5Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW00014F01ABCD2C", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand6Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW00018701ABCD21", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand7Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW00018F01ABCD30", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand8Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW0001C701ABCD2C", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand9Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW0001CF01ABCD3B", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand10Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW00020701ABCD1A", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand11Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(true, target.CheckForErrorWriteCommand("\u000500FFCW00014303ABCDEFGH1234FF", out errorCode));
            Assert.AreEqual<int>(-1, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand12Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW00014304ABCDEFGH12345678DA", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        #endregion

        #region CheckReadCommand method tests

        [TestMethod()]
        public void CheckReadCommand1Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(true, target.CheckForErrorReadCommand("\u000500FFCR0001C0051A", out errorCode));
            Assert.AreEqual<int>(-1, errorCode);
        }

        [TestMethod()]
        public void CheckReadCommand2Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorReadCommand("\u000500FFCR00014602OE", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckReadCommand3Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorReadCommand("\u000500FFCR0001860212", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckReadCommand4Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorReadCommand("\u000500FFCR00011F011A", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        #endregion

        #region InitializeMemory method tests

        [TestMethod()]
        public void InitializeMemory1Test()
        {
            PolishingSimulatorPlcCommunication privateTarget = PolishingSimulatorPlcCommunication.Create();         
            PolishingSimulatorPlcCommunication_Accessor target = new PolishingSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingSimulatorPlcCommunication))));

            // default reciper number
            Assert.AreEqual<ushort>(1, target._memory._memory[0x136]);
            Assert.AreEqual<ushort>(1, target._memory._memory[0x137]);
            Assert.AreEqual<ushort>(1, target._memory._memory[0x138]);
            Assert.AreEqual<ushort>(1, target._memory._memory[0x139]);

            // default machine status
            Assert.AreEqual<ushort>(5, target._memory._memory[0x143]);
            Assert.AreEqual<ushort>(5, target._memory._memory[0x144]);
            Assert.AreEqual<ushort>(5, target._memory._memory[0x145]);

            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x120, 0x135, 0));
            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x140, 0x142, 0));
            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x150, 0x186, 0));
            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x190, 0x1C6, 0));
            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x1D0, 0x206, 0));
        }

        #endregion
    }
}
