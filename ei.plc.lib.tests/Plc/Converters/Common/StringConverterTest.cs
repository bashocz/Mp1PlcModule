using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class StringConverterTest
    {
        #region TextToPlcStream method tests

        [TestMethod()]
        public void TextToPlcStreamTest()
        {
            StringConverter privateTarget = new StringConverter();
            StringConverter_Accessor target = new StringConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StringConverter))));
            Assert.AreEqual<string>("333239383400", target.TextToPlcStream("333239383400"));
            Assert.AreEqual<string>("393143333400", target.TextToPlcStream("3931433334"));
            Assert.AreEqual<string>("3348384741583536", target.TextToPlcStream("3348384741583536"));
        }

        #endregion

        #region PlcStreamToText method tests

        [TestMethod()]
        public void PlcStreamToTextTest()
        {
            StringConverter privateTarget = new StringConverter();
            StringConverter_Accessor target = new StringConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StringConverter))));
            Assert.AreEqual<string>("323338393400", target.PlcStreamToText("323338393400"));
            Assert.AreEqual<string>("41424B4C", target.PlcStreamToText("41424B4C"));
            Assert.AreEqual<string>("53525958", target.PlcStreamToText("53525958"));
        }

        #endregion

        #region TextToHexaDigit method tests

        [TestMethod()]
        public void TextToHexaDigitTest()
        {
            StringConverter privateTarget = new StringConverter();
            StringConverter_Accessor target = new StringConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StringConverter))));
            Assert.AreEqual<string>("48656C6C6F5F776F726C64", target.TextToHexaDigit("Hello_world"));
            Assert.AreEqual<string>("3132333435", target.TextToHexaDigit("12345"));
            Assert.AreEqual<string>("41583646473845525439", target.TextToHexaDigit("AX6FG8ERT9"));
        }

        #endregion

        #region HexaDigitToText method tests

        [TestMethod()]
        public void HexaDigitToTextTest()
        {
            StringConverter privateTarget = new StringConverter();
            StringConverter_Accessor target = new StringConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StringConverter))));
            Assert.AreEqual<string>("HELLO", target.HexaDigitToText("48454C4C4F"));
            Assert.AreEqual<string>("35769", target.HexaDigitToText("3335373639"));
            Assert.AreEqual<string>("2E68T3", target.HexaDigitToText("324536385433"));
        }

        #endregion

        #region ToStream method tests

        [TestMethod()]
        public void ToStreamTest()
        {
            StringConverter target = new StringConverter();
            Assert.AreEqual<string>("313233343500", target.ToStream("12345"));
            Assert.AreEqual<string>("5835444A3700", target.ToStream("X5DJ7"));
            Assert.AreEqual<string>("504736535130", target.ToStream("PG6SQ0"));
        }

        #endregion

        #region ToObject method tests

        [TestMethod()]
        public void ToObjectTest()
        {
            StringConverter target = new StringConverter();
            Assert.AreEqual<string>("12345", target.ToObject("313233343500"));
            Assert.AreEqual<string>("XH5WP", target.ToObject("584835575000"));
            Assert.AreEqual<string>("ZW43M", target.ToObject("5A5734334D00"));
            Assert.AreEqual<string>("P6JFQM", target.ToObject("50364A46514D"));
        }

        #endregion
    }
}
