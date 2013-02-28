using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PlcMemoryReadCommandTest
    {
        #region constructor tests

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CtorThrowsArgumentOutOfRangeExceptionTest()
        {
            PlcMemoryReadCommand target = new PlcMemoryReadCommand(PlcHelper.GetAddressSpace(0x110, 0x130), 0x125, 0x10);
        }

        [TestMethod()]
        public void CtorTest()
        {
            PlcMemoryReadCommand target = new PlcMemoryReadCommand(PlcHelper.GetAddressSpace(0x110, 0x130), 0x125, 0x5);
            Assert.AreEqual<int>(5, target.Length);
            Assert.IsNull(target.Data);
        }

        #endregion

        #region ToCommandString method tests

        [TestMethod()]
        public void ToCommandStringTest()
        {
            PlcMemoryReadCommand target1 = new PlcMemoryReadCommand(PlcHelper.GetAddressSpace(0x110, 0x130), 0x120, 0x5);
            Assert.AreEqual<string>("\u000500FFCR00012005" + "09", target1.CommandToString());

            PlcMemoryReadCommand target2 = new PlcMemoryReadCommand(PlcHelper.GetAddressSpace(0x130, 0x150), 0x140, 0x5);
            Assert.AreEqual<string>("\u000500FFCR00014005" + "0B", target2.CommandToString());
        }

        #endregion
    }
}
