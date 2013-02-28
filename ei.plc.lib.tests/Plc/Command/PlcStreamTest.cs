using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PlcStreamTest
    {
        #region ControlCharToString method tests

        [TestMethod()]
        public void ControlCharToStringTest()
        {
            Assert.AreEqual<string>("\u0005", PlcStream_Accessor.ControlCharToString(PlcControlChar_Accessor.ENQ));
            Assert.AreEqual<string>("\u0006", PlcStream_Accessor.ControlCharToString(PlcControlChar_Accessor.ACK));
            Assert.AreEqual<string>("\u0003", PlcStream_Accessor.ControlCharToString(PlcControlChar_Accessor.ETX));
            Assert.AreEqual<string>("\u0015", PlcStream_Accessor.ControlCharToString(PlcControlChar_Accessor.NAK));
            Assert.AreEqual<string>("\u0002", PlcStream_Accessor.ControlCharToString(PlcControlChar_Accessor.STX));
        }

        [TestMethod()]
        public void StringToControlCharTest()
        {
            Assert.AreEqual<PlcControlChar_Accessor>(PlcControlChar_Accessor.ENQ, PlcStream_Accessor.StringToControlChar('\u0005'));
            Assert.AreEqual<PlcControlChar_Accessor>(PlcControlChar_Accessor.ACK, PlcStream_Accessor.StringToControlChar('\u0006'));
            Assert.AreEqual<PlcControlChar_Accessor>(PlcControlChar_Accessor.ETX, PlcStream_Accessor.StringToControlChar('\u0003'));
            Assert.AreEqual<PlcControlChar_Accessor>(PlcControlChar_Accessor.NAK, PlcStream_Accessor.StringToControlChar('\u0015'));
            Assert.AreEqual<PlcControlChar_Accessor>(PlcControlChar_Accessor.STX, PlcStream_Accessor.StringToControlChar('\u0002'));
            Assert.AreEqual<PlcControlChar_Accessor>(PlcControlChar_Accessor.Unknown, PlcStream_Accessor.StringToControlChar('\u0000'));
        }

        #endregion

        #region IdsToString method tests

        [TestMethod()]
        public void IdsToString1Test()
        {
            Assert.AreEqual<string>("00FF", PlcStream.IdsToString(0, 255));
        }

        [TestMethod()]
        public void IdsToString4Test()
        {
            Assert.AreEqual<string>("1F40", PlcStream.IdsToString(31, 0x40));
        }

        [TestMethod()]
        public void IdsToString3Test()
        {
            Assert.AreEqual<string>("FF00", PlcStream.IdsToString(255, 0));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IdsToStringThrowsArgumentOutOfRangeException1Test()
        {
            PlcStream.IdsToString(-1, 255);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IdsToStringThrowsArgumentOutOfRangeException2Test()
        {
            PlcStream.IdsToString(0x20, 255);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IdsToStringThrowsArgumentOutOfRangeException3Test()
        {
            PlcStream.IdsToString(255, -1);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IdsToStringThrowsArgumentOutOfRangeException4Test()
        {
            PlcStream.IdsToString(255, 0x41);
        }

        [TestMethod()]
        public void StringToIdTest()
        {
            Assert.AreEqual<int>(0, PlcStream.StringToId("00"));
            Assert.AreEqual<int>(127, PlcStream.StringToId("7F"));
            Assert.AreEqual<int>(255, PlcStream.StringToId("FF"));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StringToIdThrowsArgumentNullExceptionTest()
        {
            PlcStream.StringToId(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void StringToIdThrowsFormatException1Test()
        {
            PlcStream.StringToId("0");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void StringToIdThrowsFormatException2Test()
        {
            PlcStream.StringToId("000");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void StringToIdThrowsFormatException3Test()
        {
            PlcStream.StringToId("XX");
        }

        #endregion

        #region WaitMsToString method tests

        [TestMethod()]
        public void ReservedString1Test()
        {
            Assert.AreEqual<string>("0", PlcStream.WaitMsToString(0));
        }

        [TestMethod()]
        public void ReservedString2Test()
        {
            Assert.AreEqual<string>("3", PlcStream.WaitMsToString(33));
        }

        [TestMethod()]
        public void ReservedString3Test()
        {
            Assert.AreEqual<string>("9", PlcStream.WaitMsToString(99));
        }

        [TestMethod()]
        public void ReservedString4Test()
        {
            Assert.AreEqual<string>("F", PlcStream.WaitMsToString(150));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReservedStringThrowsArgumentOutOfRangeException1Test()
        {
            PlcStream.WaitMsToString(-1);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReservedStringThrowsArgumentOutOfRangeException2Test()
        {
            PlcStream.WaitMsToString(151);
        }

        #endregion

        #region AddressToString method tests

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddressToStringThrowsArgumentOutOfRangeException1Test()
        {
            PlcStream.AddressToString(-1);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddressToStringThrowsArgumentOutOfRangeException2Test()
        {
            PlcStream.AddressToString(0x100000);
        }

        [TestMethod()]
        public void AddressToStringTest()
        {
            Assert.AreEqual<string>("00000", PlcStream.AddressToString(0));
            Assert.AreEqual<string>("FFFFF", PlcStream.AddressToString(0xfffff));
            Assert.AreEqual<string>("B38AC", PlcStream.AddressToString(0xb38ac));
        }

        #endregion

        #region AddCheckSum method tests

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddCheckSumThrowsArgumentNullExceptionTest()
        {
            PlcStream.AddCheckSum(null);
        }

        [TestMethod()]
        public void AddCheckSumTest()
        {
            Assert.AreEqual<string>("Hello world!" + "5D", PlcStream.AddCheckSum("Hello world!"));
            Assert.AreEqual<string>("00FF0010001234FEDC9876" + "C7", PlcStream.AddCheckSum("00FF0010001234FEDC9876"));
        }

        [TestMethod()]
        public void CalculateCheckSumTest()
        {
            Assert.AreEqual<string>("5D", PlcStream.CalculateCheckSum("Hello world!"));
            Assert.AreEqual<string>("C7", PlcStream.CalculateCheckSum("00FF0010001234FEDC9876"));
        }

        #endregion
    }
}
