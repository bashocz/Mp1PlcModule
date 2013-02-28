using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PlcAddressRangeTest
    {
        #region Constructor tests

        [TestMethod()]
        public void CtorTest()
        {
            PlcAddressRange target1 = new PlcAddressRange(0x120, 0x130);
            Assert.AreEqual<int>(0x120,target1.Item1);
            Assert.AreEqual<int>(0x130, target1.Item2);

            PlcAddressRange target2 = new PlcAddressRange(0x150, 0x155);
            Assert.AreEqual<int>(0x150, target2.Item1);
            Assert.AreEqual<int>(0x155, target2.Item2);
        }

        #endregion

        #region CheckAddress method tests

        [TestMethod()]
        public void CheckAddressReturnFalseTest()
        {
            PlcAddressRange target1 = new PlcAddressRange(0x100, 0x200);
            Assert.AreEqual<bool>(false, target1.CheckAddress(0x99));
            Assert.AreEqual<bool>(false, target1.CheckAddress(0x201));

            PlcAddressRange target2 = new PlcAddressRange(0x450, 0x516);
            Assert.AreEqual<bool>(false, target2.CheckAddress(0x449));
            Assert.AreEqual<bool>(false, target2.CheckAddress(0x517));

            PlcAddressRange target3 = new PlcAddressRange(0x1, 0x9);
            Assert.AreEqual<bool>(false, target3.CheckAddress(0x1, 10));
            Assert.AreEqual<bool>(false, target3.CheckAddress(0x5, -1));

            PlcAddressRange target4 = new PlcAddressRange(0x120, 0x121);
            Assert.AreEqual<bool>(false, target4.CheckAddress(0x120, 3));
        }
        
        [TestMethod()]
        public void CheckAddressReturnTrueTest()
        {
            PlcAddressRange target1 = new PlcAddressRange(0x100, 0x200);
            Assert.AreEqual<bool>(true, target1.CheckAddress(0x150));

            PlcAddressRange target2 = new PlcAddressRange(0x450, 0x516);
            Assert.AreEqual<bool>(true, target2.CheckAddress(0x486));

            PlcAddressRange target3 = new PlcAddressRange(0x847, 0xA0E);
            Assert.AreEqual<bool>(true, target3.CheckAddress(0x9FC));

            PlcAddressRange target4 = new PlcAddressRange(0x1, 0x9);
            Assert.AreEqual<bool>(true, target4.CheckAddress(0x1, 9));

            PlcAddressRange target5 = new PlcAddressRange(0x120, 0x121);
            Assert.AreEqual<bool>(true, target5.CheckAddress(0x120, 2));

            PlcAddressRange target6 = new PlcAddressRange(0x250, 0x260);
            Assert.AreEqual<bool>(true, target6.CheckAddress(0x255, 12));
        }

        #endregion
    }
}
