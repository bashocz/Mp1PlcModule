using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class BaseFromStreamConverterTest
    {
        #region TryConvert method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void TryConvertThrowsPlcException1Test()
        {
            Mock<BaseFromStreamConverter<string>> target = new Mock<BaseFromStreamConverter<string>>();

            target.Object.TryConvert(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void TryConvertThrowsPlcException2Test()
        {
            Mock<BaseFromStreamConverter<int>> target = new Mock<BaseFromStreamConverter<int>>();
            target.Protected().Setup<bool>("CheckParameter", ItExpr.IsAny<string>()).Returns(false);

            target.Object.TryConvert("0000");
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void TryConvertThrowsPlcException3Test()
        {
            Mock<BaseFromStreamConverter<int>> target = new Mock<BaseFromStreamConverter<int>>();
            target.Protected().Setup<bool>("CheckParameter", ItExpr.IsAny<string>()).Throws(new PlcException());

            target.Object.TryConvert("0000");
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void TryConvertThrowsPlcException4Test()
        {
            Mock<BaseFromStreamConverter<int>> target = new Mock<BaseFromStreamConverter<int>>();
            target.Protected().Setup<bool>("CheckParameter", ItExpr.IsAny<string>()).Returns(true);
            target.Protected().Setup<int>("GetObject", ItExpr.IsAny<string>()).Throws(new PlcException());

            target.Object.TryConvert("0000");
        }

        [TestMethod()]
        public void TryConvertTest()
        {
            Mock<BaseFromStreamConverter<int>> target = new Mock<BaseFromStreamConverter<int>>();
            target.Protected().Setup<bool>("CheckParameter", ItExpr.IsAny<string>()).Returns(true);
            target.Protected().Setup<int>("GetObject", ItExpr.IsAny<string>()).Returns(1);

            Assert.AreEqual<int>(1, target.Object.TryConvert("0000"));
        }

        #endregion
    }
}
