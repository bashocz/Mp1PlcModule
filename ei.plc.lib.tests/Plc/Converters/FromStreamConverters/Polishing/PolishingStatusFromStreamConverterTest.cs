using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PolishingStatusFromStreamConverterTest
    {
        #region ParsePolisherState method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ParsePolisherStateThrowsPlcException1Test()
        {
            PolishingStatusFromStreamConverter privateTarget = new PolishingStatusFromStreamConverter();
            PolishingStatusFromStreamConverter_Accessor target = new PolishingStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingStatusFromStreamConverter))));
            target.ParsePolisherState("0000");
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ParsePolisherStateThrowsPlcException2Test()
        {
            PolishingStatusFromStreamConverter privateTarget = new PolishingStatusFromStreamConverter();
            PolishingStatusFromStreamConverter_Accessor target = new PolishingStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingStatusFromStreamConverter))));
            target.ParsePolisherState("0008");
        }

        [TestMethod()]
        public void ParsePolisherState1Test()
        {
            PolishingStatusFromStreamConverter privateTarget = new PolishingStatusFromStreamConverter();
            PolishingStatusFromStreamConverter_Accessor target = new PolishingStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingStatusFromStreamConverter))));
            Assert.AreEqual<PolisherState>(PolisherState.AutoProcess, target.ParsePolisherState("0001"));
        }

        [TestMethod()]
        public void ParsePolisherState2Test()
        {
            PolishingStatusFromStreamConverter privateTarget = new PolishingStatusFromStreamConverter();
            PolishingStatusFromStreamConverter_Accessor target = new PolishingStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingStatusFromStreamConverter))));
            Assert.AreEqual<PolisherState>(PolisherState.Pause, target.ParsePolisherState("0005"));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterShortStreamThrowsPclExceptionTest()
        {
            string stream = "".PadLeft(23, '0');

            PolishingStatusFromStreamConverter privateTarget = new PolishingStatusFromStreamConverter();
            PolishingStatusFromStreamConverter_Accessor target = new PolishingStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingStatusFromStreamConverter))));
            target.CheckParameter(stream);
        }

        #endregion

        #region GetObject method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectState1ThrowsPclExceptionTest()
        {
            PolishingStatusFromStreamConverter privateTarget = new PolishingStatusFromStreamConverter();
            PolishingStatusFromStreamConverter_Accessor target = new PolishingStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingStatusFromStreamConverter))));
            target.GetObject("000100000001000000050003");
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectState2ThrowsPclExceptionTest()
        {
            PolishingStatusFromStreamConverter privateTarget = new PolishingStatusFromStreamConverter();
            PolishingStatusFromStreamConverter_Accessor target = new PolishingStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingStatusFromStreamConverter))));
            target.GetObject("000100000001000100050008");
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectPressure1ThrowsPclExceptionTest()
        {
            PolishingStatusFromStreamConverter privateTarget = new PolishingStatusFromStreamConverter();
            PolishingStatusFromStreamConverter_Accessor target = new PolishingStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingStatusFromStreamConverter))));
            target.GetObject("000200000001000100050003");
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectPressure2ThrowsPclExceptionTest()
        {
            PolishingStatusFromStreamConverter privateTarget = new PolishingStatusFromStreamConverter();
            PolishingStatusFromStreamConverter_Accessor target = new PolishingStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolishingStatusFromStreamConverter))));
            target.GetObject("000100000002000100050003");
        }

        #endregion

        #region TryConvert method tests

        [TestMethod()]
        public void TryConvert1Test()
        {
            PolishingStatusFromStreamConverter target = new PolishingStatusFromStreamConverter();
            PolishingShortStatus result = target.TryConvert("000100000001000100050003");

            Assert.AreEqual<PolisherState>(PolisherState.AutoProcess, result.Status[0].State);
            Assert.AreEqual<PolisherState>(PolisherState.Pause, result.Status[1].State);
            Assert.AreEqual<PolisherState>(PolisherState.AutoLoad, result.Status[2].State);
            Assert.AreEqual<bool>(true, result.Status[0].HighPressure);
            Assert.AreEqual<bool>(false, result.Status[1].HighPressure);
            Assert.AreEqual<bool>(true, result.Status[2].HighPressure);
        }

        [TestMethod()]
        public void TryConvert2Test()
        {
            PolishingStatusFromStreamConverter target = new PolishingStatusFromStreamConverter();
            PolishingShortStatus result = target.TryConvert("000000010001000700020006");

            Assert.AreEqual<PolisherState>(PolisherState.EmergencyStop, result.Status[0].State);
            Assert.AreEqual<PolisherState>(PolisherState.AutoWait, result.Status[1].State);
            Assert.AreEqual<PolisherState>(PolisherState.Alarm, result.Status[2].State);
            Assert.AreEqual<bool>(false, result.Status[0].HighPressure);
            Assert.AreEqual<bool>(true, result.Status[1].HighPressure);
            Assert.AreEqual<bool>(true, result.Status[2].HighPressure);
        }

        #endregion
    }
}