using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class MountSimulatorPlcCommunicationTest
    {
        #region private members

        private int errorCode;

        #endregion

        #region CreateObjects method tests

        [TestMethod()]
        public void CreateObjects1Test()
        {
            MountSimulatorPlcCommunication target = MountSimulatorPlcCommunication.Create();

            Assert.IsNotNull(target.CassetteId);
            Assert.IsNotNull(target.IsCassetteId);
            Assert.IsNotNull(target.IsCassetteIdError);
            Assert.IsNotNull(target.IsLotDataTimeout);
            Assert.IsNotNull(target.NewLotId);
            Assert.IsNotNull(target.Line);
            Assert.IsNotNull(target.IsLotStarted);
            Assert.IsNotNull(target.IsCarrierPlateArrived);
            Assert.IsNotNull(target.IsBarcodeReadedOk);
            Assert.IsNotNull(target.IsCarrierPlateMountingReady);
            Assert.IsNotNull(target.WaferBreakNumber);
            Assert.IsNotNull(target.IsWaferBreakInfoOk);
            Assert.IsNotNull(target.IsMountingCarrierPlateError);
            Assert.IsNotNull(target.IsLotEnded);
            Assert.IsNotNull(target.IsReservationLotCanceled);
            Assert.IsNotNull(target.State);
            Assert.IsNotNull(target.InformationSystemError);
            Assert.IsNotNull(target.LotId);
            Assert.IsNotNull(target.Cassettes);
            Assert.IsNotNull(target.CassetteQuantityInLot);
            Assert.IsNotNull(target.LotDataTransmission);
            Assert.IsNotNull(target.WaferQuantity);
            Assert.IsNotNull(target.NotGoodWaferQuantity);
            Assert.IsNotNull(target.Size);
            Assert.IsNotNull(target.Type);
            Assert.IsNotNull(target.Division);
            Assert.IsNotNull(target.CarrierPlateQuantity1);
            Assert.IsNotNull(target.WaferQuantity1);
            Assert.IsNotNull(target.CarrierPlateQuantity2);
            Assert.IsNotNull(target.WaferQuantity2);
            Assert.IsNotNull(target.Wafers);
        }

        #endregion

        #region CheckWriteCommand method tests

        [TestMethod()]
        public void CheckWriteCommand1Test()
        {
            MountSimulatorPlcCommunication privateTarget = MountSimulatorPlcCommunication.Create();
            MountSimulatorPlcCommunication_Accessor target = new MountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW00014F01ABCD2C", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand2Test()
        {
            MountSimulatorPlcCommunication privateTarget = MountSimulatorPlcCommunication.Create();
            MountSimulatorPlcCommunication_Accessor target = new MountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW00019201AABB19", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand3Test()
        {
            MountSimulatorPlcCommunication privateTarget = MountSimulatorPlcCommunication.Create();
            MountSimulatorPlcCommunication_Accessor target = new MountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW0001FF01555508", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckWriteCommand4Test()
        {
            MountSimulatorPlcCommunication privateTarget = MountSimulatorPlcCommunication.Create();
            MountSimulatorPlcCommunication_Accessor target = new MountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorWriteCommand("\u000500FFCW00045801ABCD22", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        #endregion
        
        #region CheckReadCommand method tests

        [TestMethod()]
        public void CheckReadCommand1Test()
        {
            MountSimulatorPlcCommunication privateTarget = MountSimulatorPlcCommunication.Create();
            MountSimulatorPlcCommunication_Accessor target = new MountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(true, target.CheckForErrorReadCommand("\u000500FFCR0001202208", out errorCode));
            Assert.AreEqual<int>(-1, errorCode);
        }

        [TestMethod()]
        public void CheckReadCommand2Test()
        {
            MountSimulatorPlcCommunication privateTarget = MountSimulatorPlcCommunication.Create();
            MountSimulatorPlcCommunication_Accessor target = new MountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(true, target.CheckForErrorReadCommand("\u000500FFCR0001502811", out errorCode));
            Assert.AreEqual<int>(-1, errorCode);
        }

        [TestMethod()]
        public void CheckReadCommand3Test()
        {
            MountSimulatorPlcCommunication privateTarget = MountSimulatorPlcCommunication.Create();
            MountSimulatorPlcCommunication_Accessor target = new MountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorReadCommand("\u000500FFCR00011F011A", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        [TestMethod()]
        public void CheckReadCommand4Test()
        {
            MountSimulatorPlcCommunication privateTarget = MountSimulatorPlcCommunication.Create();
            MountSimulatorPlcCommunication_Accessor target = new MountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.CheckForErrorReadCommand("\u000500FFCR0001420109", out errorCode));
            Assert.AreEqual<int>(0x6, errorCode);
        }

        #endregion

        #region InitializeMemory method tests

        [TestMethod()]
        public void InitializeMemory1Test()
        {
            MountSimulatorPlcCommunication privateTarget = MountSimulatorPlcCommunication.Create();
            MountSimulatorPlcCommunication_Accessor target = new MountSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountSimulatorPlcCommunication))));

            // default new lot start line
            Assert.AreEqual<ushort>(3, target._memory._memory[0x12E]);
            // default machine state
            Assert.AreEqual<ushort>(2, target._memory._memory[0x140]);
            // default wafer quantity
            Assert.AreEqual<ushort>(3, target._memory._memory[0x18F]);
            Assert.AreEqual<ushort>(3, target._memory._memory[0x191]);

            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x120, 0x12D, 0));
            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x12F, 0x137, 0));
            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x141, 0x141, 0));
            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x150, 0x18E, 0));
            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x190, 0x190, 0));
            Assert.AreEqual<bool>(true, SimulatorHelper.CheckValuesInArray(target._memory._memory, 0x200, 0x457, 0));
        }

        #endregion
    }
}
