using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PlcAddressSpaceTest
    {
        #region constructor tests

        [TestMethod()]
        public void CtorTest()
        {
            PlcAddressSpace target = new PlcAddressSpace(new PlcAddressRange[] { new PlcAddressRange(0x130, 0x140), 
                                                                                 new PlcAddressRange(0x160, 0x170) });
            bool testPassed = true;

            if(target == null)
                testPassed = false;

            Assert.AreEqual<bool>(true, testPassed);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorThrowsArgumentNullExceptionTest()
        {
            PlcAddressSpace target = new PlcAddressSpace(null);
        }

        #endregion

        #region CheckAddress method tests

        [TestMethod()]
        public void CheckAddressReturnFalseTest()
        {
            PlcAddressSpace target1 = new PlcAddressSpace(new PlcAddressRange[] { new PlcAddressRange(0x130, 0x140), 
                                                                                  new PlcAddressRange(0x160, 0x170) });
            Assert.AreEqual<bool>(false, target1.CheckAddress(0x129));
            Assert.AreEqual<bool>(false, target1.CheckAddress(0x141));
            Assert.AreEqual<bool>(false, target1.CheckAddress(0x159));
            Assert.AreEqual<bool>(false, target1.CheckAddress(0x171));

            PlcAddressSpace target2 = new PlcAddressSpace(new PlcAddressRange[] { new PlcAddressRange(0x150, 0x300), 
                                                                                  new PlcAddressRange(0x600, 0x750),
                                                                                  new PlcAddressRange(0x800, 0x900) });
            Assert.AreEqual<bool>(false, target2.CheckAddress(0x149));
            Assert.AreEqual<bool>(false, target2.CheckAddress(0x301));
            Assert.AreEqual<bool>(false, target2.CheckAddress(0x599));
            Assert.AreEqual<bool>(false, target2.CheckAddress(0x751));
            Assert.AreEqual<bool>(false, target2.CheckAddress(0x799));
            Assert.AreEqual<bool>(false, target2.CheckAddress(0x901));

            PlcAddressSpace target3 = new PlcAddressSpace(new PlcAddressRange[] { new PlcAddressRange(0x130, 0x140), 
                                                                                  new PlcAddressRange(0x160, 0x170) });
            Assert.AreEqual<bool>(false, target3.CheckAddress(0x130, 18));
            Assert.AreEqual<bool>(false, target3.CheckAddress(0x165, 13));

            PlcAddressSpace target4 = new PlcAddressSpace(new PlcAddressRange[] { new PlcAddressRange(0x150, 0x300), 
                                                                                  new PlcAddressRange(0x600, 0x750),
                                                                                  new PlcAddressRange(0x800, 0x900) });
            Assert.AreEqual<bool>(false, target4.CheckAddress(0x155, 429));
            Assert.AreEqual<bool>(false, target4.CheckAddress(0x745, 13));
            Assert.AreEqual<bool>(false, target4.CheckAddress(0x900, 2));

        }

        [TestMethod()]
        public void CheckAddressReturnTrueTest()
        {
            PlcAddressSpace target1 = new PlcAddressSpace(new PlcAddressRange[] { new PlcAddressRange(0x130, 0x140), 
                                                                                  new PlcAddressRange(0x160, 0x170) });
            Assert.AreEqual<bool>(true, target1.CheckAddress(0x165));

            PlcAddressSpace target2 = new PlcAddressSpace(new PlcAddressRange[] { new PlcAddressRange(0x150, 0x300), 
                                                                                  new PlcAddressRange(0x600, 0x750),
                                                                                  new PlcAddressRange(0x800, 0x900) });
            Assert.AreEqual<bool>(true, target2.CheckAddress(0x880));

            PlcAddressSpace target3 = new PlcAddressSpace(new PlcAddressRange[] { new PlcAddressRange(0x130, 0x140), 
                                                                                  new PlcAddressRange(0x160, 0x170) });
            Assert.AreEqual<bool>(true, target3.CheckAddress(0x165, 12));

            PlcAddressSpace target4 = new PlcAddressSpace(new PlcAddressRange[] { new PlcAddressRange(0x150, 0x300), 
                                                                                  new PlcAddressRange(0x600, 0x750),
                                                                                  new PlcAddressRange(0x800, 0x900) });
            Assert.AreEqual<bool>(true, target4.CheckAddress(0x880, 129));
        }

        #endregion
    }
}
