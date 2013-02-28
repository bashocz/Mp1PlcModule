using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class IntHexConverterTest
    {
        #region ToStream method tests

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToStreamThrowsArgumentOutOfRangeException1Test()
        {
            IntHexConverter target = new IntHexConverter();
            target.ToStream(-1);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToStreamThrowsArgumentOutOfRangeException2Test()
        {
            IntHexConverter target = new IntHexConverter();
            target.ToStream(65536);
        }

        [TestMethod()]
        public void ToStreamTest()
        {
            IntHexConverter target = new IntHexConverter();
            Assert.AreEqual<string>("0001", target.ToStream(1));
            Assert.AreEqual<string>("0002", target.ToStream(2));
            Assert.AreEqual<string>("0003", target.ToStream(3));
        }

        #endregion

        #region ToObject method tests

        [TestMethod()]
        public void ToObjectTest()
        {
            IntHexConverter target = new IntHexConverter();
            Assert.AreEqual<int>(1, target.ToObject("0001"));
            Assert.AreEqual<int>(2, target.ToObject("0002"));
            Assert.AreEqual<int>(3, target.ToObject("0003"));
        }

        #endregion
    }
}
