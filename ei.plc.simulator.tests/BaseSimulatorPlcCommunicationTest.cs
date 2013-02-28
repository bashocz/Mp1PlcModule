using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class BaseSimulatorPlcCommunicationTest
    {
        #region constructor tests

        [TestMethod()]
        public void CtorTest()
        {
            Mock<BaseSimulatorPlcCommunication> privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.State>(BaseSimulatorPlcCommunication_Accessor.State.READY, target._machineState);
        }

        #endregion

        #region ErrorMethod method tests

        [TestMethod()]
        public void ErrorMethodTest()
        {
            Assert.Inconclusive("Implementation of this method is undefined");
        }

        #endregion

        #region ProcessReadWriteCommand method tests

        [TestMethod()]
        public void ProcessReadWriteCommand1Test()
        {
            Mock<BaseSimulatorPlcCommunication> privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            int errorCode = 0;
            privateTarget.Setup<bool>(x => x.CheckForErrorReadCommand(It.IsAny<string>(), out errorCode)).Returns(true);
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            
            target._memory._memory[0x5] = 0xABCD;
            target._memory._memory[0x6] = 0x1234;

            target.Open();
            target.Write("\u000500FFCR0000050208");
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.State>(BaseSimulatorPlcCommunication_Accessor.State.CR, target._machineState);
            Assert.AreEqual<string>("ABCD1234", PlcMemoryReadData.Create(target.Read()).Data);
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.State>(BaseSimulatorPlcCommunication_Accessor.State.ACK, target._machineState);
            target.Write("\u000600FF");
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.State>(BaseSimulatorPlcCommunication_Accessor.State.READY, target._machineState);
        }

        [TestMethod()]
        public void ProcessReadWriteCommand2Test()
        {
            Mock<BaseSimulatorPlcCommunication> privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            int errorCode = 0;
            privateTarget.Setup<bool>(x => x.CheckForErrorWriteCommand(It.IsAny<string>(), out errorCode)).Returns(true);

            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));

            target.Open();
            target.Write("\u000500FFCW00012002ABCD1234DF");
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.State>(BaseSimulatorPlcCommunication_Accessor.State.CW, target._machineState);
            Assert.AreEqual<string>(new PlcMemoryAcknowledge().CommandToString(), target.Read());
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.State>(BaseSimulatorPlcCommunication_Accessor.State.READY, target._machineState);
        }

        [TestMethod()]
        public void ProcessReadWriteCommand3Test()
        {
            Mock<BaseSimulatorPlcCommunication> privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));

            // invalid command
            target.Open();
            target.Write("\u000500FFXX00012002ABCD1234F5");

            privateTarget.Verify(x => x.ErrorMethod(), Times.Exactly(1));
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.State>(BaseSimulatorPlcCommunication_Accessor.State.CW, target._machineState);
            Assert.AreEqual<string>("\u001500FF06", target.Read());
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.State>(BaseSimulatorPlcCommunication_Accessor.State.READY, target._machineState);
        }

        [TestMethod()]
        public void ProcessReadWriteCommand4Test()
        {
            Mock<BaseSimulatorPlcCommunication> privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            int errorCode = 0;
            privateTarget.Setup<bool>(x => x.CheckForErrorReadCommand(It.IsAny<string>(), out errorCode)).Returns(true);

            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));

            // calling 2x write 
            target.Open();
            target.Write("\u000500FFCR0001200509");
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.State>(BaseSimulatorPlcCommunication_Accessor.State.CR, target._machineState);

            target.Write("\u000500FFCR0001200509");
            privateTarget.Verify(x => x.ErrorMethod(), Times.Exactly(1));
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.State>(BaseSimulatorPlcCommunication_Accessor.State.READY, target._machineState);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void ProcessReadWriteCommandThrowsNotImplementedExceptionTest()
        {
            Mock<BaseSimulatorPlcCommunication> privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));

            // calling read before write
            target.Open();
            target.Read();
        }

        #endregion

        #region CheckReadWriteCommand method tests

        [TestMethod()]
        public void CheckReadWriteCommand1Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.Msg>(BaseSimulatorPlcCommunication_Accessor.Msg.WriteCommand, target.CheckReadWriteCommand("\u000500FFCW00012001ABCD" + target.CalculateCheckSum("00FFCW00012001ABCD")));
        }

        [TestMethod()]
        public void CheckReadWriteCommand2Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.Msg>(BaseSimulatorPlcCommunication_Accessor.Msg.ReadCommand, target.CheckReadWriteCommand("\u000500FFCR00012001" + target.CalculateCheckSum("00FFCR00012001")));
        }

        [TestMethod()]
        public void CheckReadWriteCommand3Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.Msg>(BaseSimulatorPlcCommunication_Accessor.Msg.ProtocolError, target.CheckReadWriteCommand("\u000500FFCR0001200" + target.CalculateCheckSum("00FFCR0001200")));
        }

        [TestMethod()]
        public void CheckReadWriteCommand4Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.Msg>(BaseSimulatorPlcCommunication_Accessor.Msg.SumCheckError, target.CheckReadWriteCommand("\u000500FFCR00018501" + target.CalculateCheckSum("00FFCR00012001")));
        }

        [TestMethod()]
        public void CheckReadWriteCommand5Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.Msg>(BaseSimulatorPlcCommunication_Accessor.Msg.ProtocolError, target.CheckReadWriteCommand("\u000100FFCR00012001" + target.CalculateCheckSum("00FFCR00012001")));
        }

        [TestMethod()]
        public void CheckReadWriteCommand6Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.Msg>(BaseSimulatorPlcCommunication_Accessor.Msg.ProtocolError, target.CheckReadWriteCommand("\u000501FFCR00012001" + target.CalculateCheckSum("01FFCR00012001")));
        }

        [TestMethod()]
        public void CheckReadWriteCommand7Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.Msg>(BaseSimulatorPlcCommunication_Accessor.Msg.CharacterAreaError, target.CheckReadWriteCommand("\u000500FFXX00012001" + target.CalculateCheckSum("00FFXX00012001")));
        }

        [TestMethod()]
        public void CheckReadWriteCommand8Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.Msg>(BaseSimulatorPlcCommunication_Accessor.Msg.CharacterError, target.CheckReadWriteCommand("\u000500FFCR+0012001" + target.CalculateCheckSum("00FFCR+0012001")));
        }

        [TestMethod()]
        public void CheckReadWriteCommand9Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<BaseSimulatorPlcCommunication_Accessor.Msg>(BaseSimulatorPlcCommunication_Accessor.Msg.PCCPUNumberError, target.CheckReadWriteCommand("\u00050041CR00012001" + target.CalculateCheckSum("0041CR00012001")));
        }

        #endregion

        #region CheckAcknowledgeCommand method tests

        [TestMethod()]
        public void CheckAcknowledgeCommand1Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<bool>(true, target.CheckAcknowledgeCommand("\u000600FF"));
        }

        [TestMethod()]
        public void CheckAcknowledgeCommand2Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<bool>(false, target.CheckAcknowledgeCommand("\u010600FF"));
        }

        #endregion

        #region AddCheckSum method tests

        [TestMethod()]
        public void AddCheckSumTest()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<string>("00FFABCDEFGH10", target.AddCheckSum("00FFABCDEFGH"));
        }

        #endregion

        #region CalculateCheckSum method tests

        [TestMethod()]
        public void CalculateCheckSumTest()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<string>("10", target.CalculateCheckSum("00FFABCDEFGH"));
        }

        #endregion

        #region IsMyMessage method tests

        [TestMethod()]
        public void IsMyMessage1Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(true, target.IsMyMessage("\u000500FFCR00012001"));
        }

        [TestMethod()]
        public void IsMyMessage2Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.IsMyMessage("\u000501FFCR00012001"));
        }

        [TestMethod()]
        public void IsMyMessage3Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));

            Assert.AreEqual<bool>(false, target.IsMyMessage("\u000500FECR00012001"));
        }

        #endregion

        #region CharactersCheck method tests

        [TestMethod()]
        public void CharactersCheck1Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<bool>(true, target.CharactersCheck("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_\u0000\u0002\u0003\u0004\u0005\u0006\u000A\u000C\u000D\u0015"));
        }

        [TestMethod()]
        public void CharactersCheck2Test()
        {
            Mock privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));
            Assert.AreEqual<bool>(false, target.CharactersCheck("a0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_\u0000\u0002\u0003\u0004\u0005\u0006\u000A\u000C\u000D\u0015"));
        }

        #endregion

        #region ParseWriteCommand method tests

        [TestMethod()]
        public void ParseWriteCommand1Test()
        {
            Mock<BaseSimulatorPlcCommunication> privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            int errorCode = 0;
            privateTarget.Setup<bool>(x => x.CheckForErrorWriteCommand(It.IsAny<string>(), out errorCode)).Returns(true);

            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));

            Assert.AreEqual<string>("\u000600FF", target.ParseWriteCommand("\u000500FFCW00000502ABCD1234E1"));
            Assert.AreEqual<ushort>(0xABCD, target._memory._memory[5]);
            Assert.AreEqual<ushort>(0x1234, target._memory._memory[6]);
        }

        [TestMethod()]
        public void ParseWriteCommand2Test()
        {
            Mock<BaseSimulatorPlcCommunication> privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            int errorCode = 0;
            privateTarget.Setup<bool>(x => x.CheckForErrorWriteCommand(It.IsAny<string>(), out errorCode)).Returns(true);

            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));

            Assert.AreEqual<string>("\u000600FF", target.ParseWriteCommand("\u000500FFCW00003F015678FA"));
            Assert.AreEqual<ushort>(0x5678, target._memory._memory[63]);
        }

        #endregion

        #region ParseReadCommand method tests

        [TestMethod()]
        public void ParseReadCommand1Test()
        {
            Mock<BaseSimulatorPlcCommunication> privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            int errorCode = 0;
            privateTarget.Setup<bool>(x => x.CheckForErrorReadCommand(It.IsAny<string>(), out errorCode)).Returns(true);
            privateTarget.Setup<bool>(x => x.CheckForErrorWriteCommand(It.IsAny<string>(), out errorCode)).Returns(true);
            
            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));

            Assert.AreEqual<string>("\u000600FF", target.ParseWriteCommand("\u000500FFCW000002015678E3"));
            Assert.AreEqual<ushort>(0x5678, target._memory._memory[2]);
            Assert.AreEqual<string>("\u000200FF5678\u0003C9", target.ParseReadCommand("\u000500FFCR0000020104"));
        }

        [TestMethod()]
        public void ParseReadCommand2Test()
        {
            Mock<BaseSimulatorPlcCommunication> privateTarget = new Mock<BaseSimulatorPlcCommunication>();
            int errorCode = 0;
            privateTarget.Setup<bool>(x => x.CheckForErrorReadCommand(It.IsAny<string>(), out errorCode)).Returns(true);
            privateTarget.Setup<bool>(x => x.CheckForErrorWriteCommand(It.IsAny<string>(), out errorCode)).Returns(true);

            BaseSimulatorPlcCommunication_Accessor target = new BaseSimulatorPlcCommunication_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BaseSimulatorPlcCommunication))));

            Assert.AreEqual<string>("\u000600FF", target.ParseWriteCommand("\u000500FFCW00002302ABCD1234E1"));
            Assert.AreEqual<ushort>(0xABCD, target._memory._memory[35]);
            Assert.AreEqual<ushort>(0x1234, target._memory._memory[36]);
            Assert.AreEqual<string>("\u000200FFABCD1234\u0003C3", target.ParseReadCommand("\u000500FFCR0000230208"));
        }

        #endregion
    }
}
