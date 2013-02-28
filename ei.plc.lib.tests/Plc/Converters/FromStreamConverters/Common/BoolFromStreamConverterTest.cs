using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class BoolFromStreamConverterTest
    {
        #region CheckParameter method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterShortStreamThrowsPclExceptionTest()
        {
            string stream = "".PadLeft(3, '0');

            BoolFromStreamConverter privateTarget = new BoolFromStreamConverter();
            BoolFromStreamConverter_Accessor target = new BoolFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(BoolFromStreamConverter))));
            target.CheckParameter(stream);
        }

        #endregion

        #region GetObject method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectThrowsPclException1Test()
        {
            BoolFromStreamConverter privateTarget = new BoolFromStreamConverter();
            BoolFromStreamConverter_Accessor target = new BoolFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(BoolFromStreamConverter))));
            target.GetObject("XXXX");
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectThrowsPclException2Test()
        {
            BoolFromStreamConverter privateTarget = new BoolFromStreamConverter();
            BoolFromStreamConverter_Accessor target = new BoolFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(BoolFromStreamConverter))));
            target.GetObject("0002");
        }

        #endregion

        #region TryConvert method tests

        [TestMethod()]
        public void TryConvertTest()
        {
            BoolFromStreamConverter target = new BoolFromStreamConverter();

            Assert.IsTrue(target.TryConvert("0001"));
            Assert.IsFalse(target.TryConvert("0000"));
        }

        #endregion
    }
}