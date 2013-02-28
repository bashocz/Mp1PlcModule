using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PlcMemoryReadDataTest
    {
        #region private members

        private string GetNullMessage()
        {
            return (string)null;
        }

        private string GetShortMessage()
        {
            return "\u000200FF\u0003";
        }

        private string GetWrongChecksumMessage()
        {
            return "\u000200FFData\u0003" + "00";
        }

        private string GetWrongBeginCtrlCharMessage()
        {
            return "\u000300FFData\u0003" + PlcStream.CalculateCheckSum("00FFData\u0003");
        }

        private string GetWrongEndCtrlCharMessage()
        {
            return "\u000200FFData\u0002" + PlcStream.CalculateCheckSum("00FFData\u0002");
        }

        private string GetPass1Message()
        {
            return "\u000200FFData\u0003" + PlcStream.CalculateCheckSum("00FFData\u0003");
        }

        private string GetPass2Message()
        {
            return "\u00021FFEHelloPlc\u0003" + PlcStream.CalculateCheckSum("1FFEHelloPlc\u0003");
        }

        private string GetErrorMessage()
        {
            return "\u001500FF80";
        }

        #endregion

        #region Create method tests

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateThrowsArgumentNullExceptionTest()
        {
            Assert.IsNull(PlcMemoryReadData.Create(GetNullMessage()));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateThrowsArgumentExceptionTest()
        {
            Assert.IsNull(PlcMemoryReadData.Create(GetWrongChecksumMessage()));
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void CreateThrowsFormatException1Test()
        {
            Assert.IsNull(PlcMemoryReadData.Create(GetShortMessage()));
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void CreateThrowsFormatException2Test()
        {
            Assert.IsNull(PlcMemoryReadData.Create(GetWrongBeginCtrlCharMessage()));
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void CreateThrowsFormatException3Test()
        {
            Assert.IsNull(PlcMemoryReadData.Create(GetWrongEndCtrlCharMessage()));
        }

        [TestMethod()]
        public void Create1Test()
        {
            Assert.IsNotNull(PlcMemoryReadData.Create(GetPass1Message()));
        }

        [TestMethod()]
        public void Create2Test()
        {
            Assert.IsNotNull(PlcMemoryReadData.Create(GetPass2Message()));
        }

        [TestMethod()]
        public void Create3Test()
        {
            Assert.IsNotNull(PlcMemoryReadData.Create(GetErrorMessage()));
        }

        #endregion

        #region ParseMessage method tests

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseMessageThrowsArgumentNullExceptionTest()
        {
            PlcMemoryReadData_Accessor target = new PlcMemoryReadData_Accessor();
            target.ParseMessage(GetNullMessage());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseMessageThrowsArgumentExceptionTest()
        {
            PlcMemoryReadData_Accessor target = new PlcMemoryReadData_Accessor();
            target.ParseMessage(GetWrongChecksumMessage());
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void ParseMessageThrowsFormatException1Test()
        {
            PlcMemoryReadData_Accessor target = new PlcMemoryReadData_Accessor();
            target.ParseMessage(GetShortMessage());
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void ParseMessageThrowsFormatException2Test()
        {
            PlcMemoryReadData_Accessor target = new PlcMemoryReadData_Accessor();
            target.ParseMessage(GetWrongBeginCtrlCharMessage());
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void ParseMessageThrowsFormatException3Test()
        {
            PlcMemoryReadData_Accessor target = new PlcMemoryReadData_Accessor();
            target.ParseMessage(GetWrongEndCtrlCharMessage());
        }

        [TestMethod()]
        public void ParseMessagePass1Test()
        {
            PlcMemoryReadData_Accessor target = new PlcMemoryReadData_Accessor();
            target.ParseMessage(GetPass1Message());

            Assert.AreEqual<int>(0, target.StationNo);
            Assert.AreEqual<int>(255, target.PcNo);
            Assert.AreEqual<string>("Data", target.Data);
            Assert.IsFalse(target.IsError);
            Assert.AreEqual<int>(0, target.ErrorCode);
        }

        [TestMethod()]
        public void ParseMessagePass2Test()
        {
            PlcMemoryReadData_Accessor target = new PlcMemoryReadData_Accessor();
            target.ParseMessage(GetPass2Message());

            Assert.AreEqual<int>(31, target.StationNo);
            Assert.AreEqual<int>(254, target.PcNo);
            Assert.AreEqual<string>("HelloPlc", target.Data);
            Assert.IsFalse(target.IsError);
            Assert.AreEqual<int>(0, target.ErrorCode);
        }

        [TestMethod()]
        public void ParseMessageErrorTest()
        {
            PlcMemoryReadData_Accessor target = new PlcMemoryReadData_Accessor();
            target.ParseMessage(GetErrorMessage());

            Assert.AreEqual<int>(0, target.StationNo);
            Assert.AreEqual<int>(255, target.PcNo);
            Assert.IsNull(target.Data);
            Assert.IsTrue(target.IsError);
            Assert.AreEqual<int>(128, target.ErrorCode);
        }

        #endregion

        #region ParseIds method tests

        [TestMethod()]
        public void ParseIds1Test()
        {
            PlcMemoryReadData_Accessor target = new PlcMemoryReadData_Accessor();
            target.ParseIds(GetPass2Message());

            Assert.AreEqual<int>(31, target.StationNo);
            Assert.AreEqual<int>(254, target.PcNo);
        }

        [TestMethod()]
        public void ParseIds2Test()
        {
            PlcMemoryReadData_Accessor target = new PlcMemoryReadData_Accessor();
            target.ParseIds(GetErrorMessage());

            Assert.AreEqual<int>(0, target.StationNo);
            Assert.AreEqual<int>(255, target.PcNo);
        }

        #endregion

        #region ParseData method tests

        [TestMethod()]
        public void ParseData1Test()
        {
            PlcMemoryReadData_Accessor target = new PlcMemoryReadData_Accessor();
            target.ParseData(GetPass1Message());

            Assert.AreEqual<string>("Data", target.Data);
        }

        [TestMethod()]
        public void ParseData2Test()
        {
            PlcMemoryReadData_Accessor target = new PlcMemoryReadData_Accessor();
            target.ParseData(GetPass2Message());

            Assert.AreEqual<string>("HelloPlc", target.Data);
        }

        #endregion

        #region ParseErrorCode method tests

        [TestMethod()]
        public void ParseErrorCodeTest()
        {
            PlcMemoryReadData_Accessor target = new PlcMemoryReadData_Accessor();

            Assert.IsFalse(target.IsError);

            target.ParseErrorCode(GetErrorMessage());

            Assert.IsTrue(target.IsError);
            Assert.AreEqual<int>(128, target.ErrorCode);
        }

        #endregion
    }
}
