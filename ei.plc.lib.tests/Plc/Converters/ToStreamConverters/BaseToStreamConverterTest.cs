using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class BaseToStreamConverterTest
    {
        #region TryConvert method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void TryConvertThrowsPlcException1Test()
        {
            Mock<BaseToStreamConverter<string>> target = new Mock<BaseToStreamConverter<string>>();

            target.Object.TryConvert(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void TryConvertThrowsPlcException2Test()
        {
            Mock<BaseToStreamConverter<int>> target = new Mock<BaseToStreamConverter<int>>();
            target.Protected().Setup<bool>("CheckParameter", ItExpr.IsAny<int>()).Returns(false);

            target.Object.TryConvert(0);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void TryConvertThrowsPlcException3Test()
        {
            Mock<BaseToStreamConverter<int>> target = new Mock<BaseToStreamConverter<int>>();
            target.Protected().Setup<bool>("CheckParameter", ItExpr.IsAny<int>()).Throws(new PlcException());

            target.Object.TryConvert(0);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void TryConvertThrowsPlcException4Test()
        {
            Mock<BaseToStreamConverter<int>> target = new Mock<BaseToStreamConverter<int>>();
            target.Protected().Setup<bool>("CheckParameter", ItExpr.IsAny<int>()).Returns(true);
            target.Protected().Setup<int>("GetLength", ItExpr.IsAny<int>()).Throws(new PlcException());
            target.Protected().Setup<string>("GetStream", ItExpr.IsAny<int>()).Returns("0000");

            target.Object.TryConvert(0);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void TryConvertThrowsPlcException5Test()
        {
            Mock<BaseToStreamConverter<int>> target = new Mock<BaseToStreamConverter<int>>();
            target.Protected().Setup<bool>("CheckParameter", ItExpr.IsAny<int>()).Returns(true);
            target.Protected().Setup<int>("GetLength", ItExpr.IsAny<int>()).Returns(1);
            target.Protected().Setup<string>("GetStream", ItExpr.IsAny<int>()).Throws(new PlcException());

            target.Object.TryConvert(0);
        }

        [TestMethod()]
        public void TryConvertTest()
        {
            Mock<BaseToStreamConverter<int>> target = new Mock<BaseToStreamConverter<int>>();
            target.Protected().Setup<bool>("CheckParameter", ItExpr.IsAny<int>()).Returns(true);
            target.Protected().Setup<int>("GetLength", ItExpr.IsAny<int>()).Returns(1);
            target.Protected().Setup<string>("GetStream", ItExpr.IsAny<int>()).Returns("0000");

            PlcWriteStream result = target.Object.TryConvert(0);
            Assert.AreEqual<int>(1, result.Length);
            Assert.AreEqual<string>("0000", result.Stream);
        }

        #endregion
    }
}
