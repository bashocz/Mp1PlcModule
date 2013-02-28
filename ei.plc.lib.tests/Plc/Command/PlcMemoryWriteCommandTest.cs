using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace EI.Plc.Tests
{    
    [TestClass()]
    public class PlcMemoryWriteCommandTest
    {
        #region constructor test

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorThrowsArgumentNullExceptionTest()
        {
            PlcMemoryWriteCommand target = new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(0x110, 0x130), 0x120, (PlcWriteStream)null);
        }

        [TestMethod()]
        public void CtorTest()
        {
            PlcMemoryWriteCommand target = new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(0x110, 0x130), 0x120, new PlcWriteStream());

            Assert.IsNotNull(target);
        }

        #endregion

        #region ToCommandString method tests

        [TestMethod()]
        public void ToCommandStringTest()
        {
            PlcMemoryWriteCommand target1 = new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(0x110, 0x130), 0x110, new PlcWriteStream { Length = 4, Stream = "0000111122223333" });

            Assert.AreEqual<string>("\u000500FFCW000110040000111122223333" + "24", target1.CommandToString());

            PlcMemoryWriteCommand target2 = new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(0x110, 0x130), 0x120, new PlcWriteStream { Length = 2, Stream = "00001111" });

            Assert.AreEqual<string>("\u000500FFCW0001200200001111" + "8F", target2.CommandToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToProtocolStringThrowsInvalidOperationExceptionTest()
        {
            PlcMemoryWriteCommand target = new PlcMemoryWriteCommand(PlcHelper.GetAddressSpace(0x110, 0x111), 0x110, new PlcWriteStream { Length = 4 });

            target.CommandToString();
        }

        #endregion
    }
}