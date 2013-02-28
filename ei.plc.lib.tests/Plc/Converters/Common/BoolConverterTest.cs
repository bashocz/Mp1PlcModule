using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class BoolConverterTest
    {
        #region ToStream method tests

        [TestMethod()]
        public void ToStreamTest()
        {
            BoolConverter target = new BoolConverter();
            Assert.AreEqual<string>("0000", target.ToStream(false));
            Assert.AreEqual<string>("0001", target.ToStream(true));
        }

        #endregion

        #region ToObject method tests

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void ToObjectThrowsFormatExceptionTest()
        {
            BoolConverter target = new BoolConverter();
            target.ToObject("0002");
        }

        [TestMethod()]
        public void ToObjectTest()
        {
            BoolConverter target = new BoolConverter();
            Assert.AreEqual<bool>(false, target.ToObject("0000"));
            Assert.AreEqual<bool>(true, target.ToObject("0001"));
        }

        #endregion
    }
}
