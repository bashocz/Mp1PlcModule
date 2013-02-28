using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class BasePlcTest
    {
        #region private members

        class TestToStreamConverter : BaseToStreamConverter<int>
        {
            #region BaseToStreamConverter members

            protected override bool CheckParameter(int parameter)
            {
                if (parameter < 0)
                    throw new PlcException();
                return true;
            }

            protected override int GetLength(int parameter)
            {
                return 2;
            }

            protected override string GetStream(int parameter)
            {
                return "11112222";
            }

            #endregion
        }

        private Mock<ICommunication> GetPlcCommunicationMock()
        {
            Mock<ICommunication> plcComm = new Mock<ICommunication>(MockBehavior.Strict);

            bool connected = false;
            plcComm.Setup(c => c.Connected).Returns(() => { return connected; });

            plcComm.Setup(c => c.Open()).Callback(() => { connected = true; }).Returns(true);
            plcComm.Setup(c => c.Close()).Callback(() => { connected = false; });

            return plcComm;
        }

        private Mock<ICommunication> GetReadPlcCommunicationMock(string read)
        {
            Mock<ICommunication> plcComm = GetPlcCommunicationMock();
            plcComm.Setup(x => x.Write(It.IsAny<string>()));
            plcComm.Setup(x => x.Read()).Returns(read);
            return plcComm;
        }

        private PlcAddressRange[] GetAddressRanges()
        {
            return new PlcAddressRange[] { new PlcAddressRange(int.MinValue, int.MaxValue) };
        }

        private static PlcWriteStream GetPlcWriteStream()
        {
            return new PlcWriteStream { Length = 2, Stream = "11112222" };
        }

        private Mock<IPlcAddressSpace> GetAddressSpaceMock()
        {
            Mock<IPlcAddressSpace> addressSpace = new Mock<IPlcAddressSpace>();
            addressSpace.Setup(x => x.CheckAddress(It.IsAny<int>())).Returns(true);
            addressSpace.Setup(x => x.CheckAddress(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            return addressSpace;
        }

        private PlcMemoryWriteCommand GetPlcMemoryWriteCommand()
        {
            return new PlcMemoryWriteCommand(GetAddressSpaceMock().Object, 0, GetPlcWriteStream());
        }

        #endregion

        #region constructor tests

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorThrowsArgumentNullExceptionTest()
        {
            Mock<BasePlc> target = new Mock<BasePlc>(null);
            try
            {
                var obj = target.Object;
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        #endregion

        #region Open method tests

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void OpenThrowsInvalidOperationExceptionTest()
        {
            var target = new Mock<BasePlc>(GetPlcCommunicationMock().Object).Object;

            target.Open();
            target.Open();
        }

        [TestMethod()]
        public void OpenTest()
        {
            var plcComm = GetPlcCommunicationMock().Object;
            var target = new Mock<BasePlc>(plcComm).Object;

            Assert.AreEqual(false, plcComm.Connected);

            target.Open();

            Assert.AreEqual(true, plcComm.Connected);
        }

        #endregion

        #region Close method tests

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CloseThrowsInvalidOperationException1Test()
        {
            var target = new Mock<BasePlc>(GetPlcCommunicationMock().Object).Object;

            target.Close();
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CloseThrowsInvalidOperationException2Test()
        {
            var target = new Mock<BasePlc>(GetPlcCommunicationMock().Object).Object;

            target.Open();
            target.Close();
            target.Close();
        }

        [TestMethod()]
        public void CloseTest()
        {
            var plcComm = GetPlcCommunicationMock().Object;
            var target = new Mock<BasePlc>(plcComm).Object;

            target.Open();
            Assert.AreEqual(true, plcComm.Connected);

            target.Close();

            Assert.AreEqual(false, plcComm.Connected);
        }

        #endregion

        #region EnterOperation method tests

        [TestMethod()]
        public void EnterOperationTest()
        {
            Assert.IsTrue(true, "It isn't tested alone, but in concurency tests.");
        }

        #endregion

        #region ExitOperation method tests

        [TestMethod()]
        public void ExitOperationTest()
        {
            Assert.IsTrue(true, "It isn't tested alone, but in concurency tests.");
        }

        #endregion

        #region Write method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void WriteThrowsInvalidOperationException1Test()
        {
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(GetPlcCommunicationMock().Object);
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.Write(GetPlcMemoryWriteCommand().CommandToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void WriteThrowsInvalidOperationException2Test()
        {
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(GetPlcCommunicationMock().Object);
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.Open();
            target.Close();
            target.Write(GetPlcMemoryWriteCommand().CommandToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void WriteThrowsArgumentNullExceptionTest()
        {
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(GetPlcCommunicationMock().Object);
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.Open();
            target.Write(null);
        }

        [TestMethod()]
        public void WriteTest()
        {
            Mock<ICommunication> plcComm = GetReadPlcCommunicationMock("");
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(plcComm.Object);
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));
            string writer = GetPlcMemoryWriteCommand().CommandToString();

            target.Open();
            target.Write(writer);

            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(1));
            plcComm.Verify(x => x.Write(writer), Times.Exactly(1));
        }

        #endregion

        #region Read method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ReadThrowsInvalidOperationException1Test()
        {
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(GetPlcCommunicationMock().Object);
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.Read();
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ReadThrowsInvalidOperationException2Test()
        {
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(GetPlcCommunicationMock().Object);
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.Open();
            target.Close();
            target.Read();
        }

        [TestMethod()]
        public void ReadTest()
        {
            Mock<ICommunication> plcComm = GetReadPlcCommunicationMock("ABCD");
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(plcComm.Object);
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.Open();

            Assert.AreEqual("ABCD", target.Read());
            plcComm.Verify<string>(x => x.Read(), Times.Exactly(1));
        }

        #endregion

        #region WriteMemory tests

        [TestMethod()]
        public void WriteMemoryPassTest()
        {
            Mock<ICommunication> plcComm = GetReadPlcCommunicationMock("\u000600FF");
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(plcComm.Object);
            privateTarget.Protected().Setup<PlcAddressRange[]>("GetAddressRanges").Returns(GetAddressRanges());
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.Open();
            Assert.IsTrue(target.WriteMemory<TestToStreamConverter, int>(0, 0));
        }

        [TestMethod()]
        public void WriteMemoryFail1Test()
        {
            Mock<ICommunication> plcComm = GetReadPlcCommunicationMock("\u001500FF01");
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(plcComm.Object);
            privateTarget.Protected().Setup<PlcAddressRange[]>("GetAddressRanges").Returns(GetAddressRanges());
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.Open();
            Assert.IsFalse(target.WriteMemory<TestToStreamConverter, int>(0, 0));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void WriteMemoryThrowsInvalidOperationExceptionTest()
        {
            Mock<ICommunication> plcComm = GetPlcCommunicationMock();
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(plcComm.Object);
            privateTarget.Protected().Setup<PlcAddressRange[]>("GetAddressRanges").Returns(GetAddressRanges());
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.WriteMemory<TestToStreamConverter, int>(0, -1);
        }

        #endregion

        #region ReadMemory tests

        [TestMethod()]
        public void ReadMemoryPassTest()
        {
            Mock<ICommunication> plcComm = GetReadPlcCommunicationMock("\u000200FF0001\u0003" + PlcStream.CalculateCheckSum("00FF0001\u0003"));
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(plcComm.Object);
            privateTarget.Protected().Setup<PlcAddressRange[]>("GetAddressRanges").Returns(GetAddressRanges());
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.Open();
            bool data = target.ReadMemory<BoolFromStreamConverter, bool>(0, 1);

            Assert.IsTrue(data);
            plcComm.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(2));
            plcComm.Verify(x => x.Write("\u000500FFCR0000000102"), Times.Exactly(1));
            plcComm.Verify(x => x.Write("\u000600FF"), Times.Exactly(1));
            plcComm.Verify(x => x.Read(), Times.Exactly(1));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ReadMemoryFail1Test()
        {
            Mock<ICommunication> plcComm = GetReadPlcCommunicationMock("\u001500FF80");
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(plcComm.Object);
            privateTarget.Protected().Setup<PlcAddressRange[]>("GetAddressRanges").Returns(GetAddressRanges());
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.Open();
            target.ReadMemory<BoolFromStreamConverter, bool>(0, 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ReadMemoryFail2Test()
        {
            Mock<ICommunication> plcComm = GetReadPlcCommunicationMock("\u000200FFData\u0003" + "00");
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(plcComm.Object);
            privateTarget.Protected().Setup<PlcAddressRange[]>("GetAddressRanges").Returns(GetAddressRanges());
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.Open();
            target.ReadMemory<BoolFromStreamConverter, bool>(0, 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ReadMemoryThrowsInvalidOperationExceptionTest()
        {
            Mock<ICommunication> plcComm = GetPlcCommunicationMock();
            plcComm.Setup(x => x.Read()).Returns("Data");
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(plcComm.Object);
            privateTarget.Protected().Setup<PlcAddressRange[]>("GetAddressRanges").Returns(GetAddressRanges());
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.ReadMemory<BoolFromStreamConverter, bool>(0, 10);
        }

        #endregion

        #region GetPlcCommunicationStatus members

        [TestMethod()]
        public void GetPlcCommunicationStatusUnknownTest()
        {
            var plcComm = GetPlcCommunicationMock().Object;
            var target = new Mock<BasePlc>(plcComm).Object;

            Assert.AreEqual<PlcCommunicationStatus>(PlcCommunicationStatus.Unknown, target.GetCommunicationStatus());

            target.Open();
            target.Close();

            Assert.AreEqual<PlcCommunicationStatus>(PlcCommunicationStatus.Unknown, target.GetCommunicationStatus());
        }

        [TestMethod()]
        public void GetPlcCommunicationStatusReadyTest()
        {
            Mock<ICommunication> plcComm = GetReadPlcCommunicationMock("\u000600FF");
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(plcComm.Object);
            privateTarget.Protected().Setup<PlcAddressRange[]>("GetAddressRanges").Returns(GetAddressRanges());
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.Open();

            Assert.AreEqual<PlcCommunicationStatus>(PlcCommunicationStatus.Ready, target.GetCommunicationStatus());

            target.WriteMemory<TestToStreamConverter, int>(0, 0);

            Assert.AreEqual<PlcCommunicationStatus>(PlcCommunicationStatus.Ready, target.GetCommunicationStatus());
        }

        [TestMethod()]
        public void GetPlcCommunicationStatusCommunicateTest()
        {
            Mock<ICommunication> plcComm = GetPlcCommunicationMock();
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback(() => { Thread.Sleep(100); });
            plcComm.Setup(x => x.Read()).Returns("");
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(plcComm.Object);
            privateTarget.Protected().Setup<PlcAddressRange[]>("GetAddressRanges").Returns(GetAddressRanges());
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.Open();

            Assert.AreEqual<PlcCommunicationStatus>(PlcCommunicationStatus.Ready, target.GetCommunicationStatus());

            Task task = Task.Factory.StartNew(() => { target.WriteMemory<TestToStreamConverter, int>(0, 0); });

            Thread.Sleep(50);
            Assert.AreEqual<PlcCommunicationStatus>(PlcCommunicationStatus.Communicate, target.GetCommunicationStatus());

            Task.WaitAll(task);
        }

        [TestMethod()]
        public void GetPlcCommunicationStatusTimeoutTest()
        {
            Mock<ICommunication> plcComm = GetPlcCommunicationMock();
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback(() => { Thread.Sleep(200); });
            plcComm.Setup(x => x.Read()).Returns("");
            Mock<BasePlc> privateTarget = new Mock<BasePlc>(plcComm.Object);
            privateTarget.Protected().Setup<PlcAddressRange[]>("GetAddressRanges").Returns(GetAddressRanges());
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));

            target.Open();

            Assert.AreEqual<PlcCommunicationStatus>(PlcCommunicationStatus.Ready, target.GetCommunicationStatus());

            Task task = Task.Factory.StartNew(() => { target.WriteMemory<TestToStreamConverter, int>(0, 0); });

            Thread.Sleep(150);
            Assert.AreEqual<PlcCommunicationStatus>(PlcCommunicationStatus.Timeout, target.GetCommunicationStatus());

            Task.WaitAll(task);
        }

        #endregion

        #region concurency tests

        //[TestMethod()]
        public void WriteMemoryConcurencyTest()
        {
            Assert.Inconclusive("to-do", "after decision of distributed/");
            Mock<ICommunication> plcComm = GetPlcCommunicationMock();
            int callIdx = 0, write1 = 0, write2 = 0, read1 = 0, read2 = 0;
            plcComm.Setup(x => x.Write(It.IsAny<string>())).Callback(() => { if (write1 == 0) write1 = ++callIdx; else write2 = ++callIdx; Thread.Sleep(10); });
            plcComm.Setup(x => x.Read()).Returns("").Callback(() => { if (read1 == 0) read1 = ++callIdx; else read2 = ++callIdx; }); ;

            Mock<BasePlc> privateTarget = new Mock<BasePlc>(plcComm.Object);
            privateTarget.Protected().Setup<PlcAddressRange[]>("GetAddressRanges").Returns(GetAddressRanges());
            BasePlc_Accessor target = new BasePlc_Accessor(new PrivateObject(privateTarget.Object, new PrivateType(typeof(BasePlc))));


            target.Open();
            Task task1 = Task.Factory.StartNew(() => { target.WriteMemory<TestToStreamConverter, int>(0, 0); });
            Task task2 = Task.Factory.StartNew(() => { target.WriteMemory<TestToStreamConverter, int>(0, 0); });
            Task.WaitAll(task1, task2);

            Assert.AreEqual<int>(1, read1 - write1, "Concurency error of first command.");
            Assert.AreEqual<int>(1, read2 - write2, "Concurency error of second command.");
        }

        //[TestMethod()]
        public void ReadMemoryConcurencyTest()
        {
        }

        //[TestMethod()]
        public void WriteReadMemoryConcurencyTest()
        {
        }

        //[TestMethod()]
        public void WriteMemoryCloseConcurencyTest()
        {
        }

        //[TestMethod()]
        public void ReadMemoryCloseConcurencyTest()
        {
        }

        #endregion
    }
}
