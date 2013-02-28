using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class IntToStreamConverterTest
    {
        #region CheckParameter method tests

        [TestMethod()]
        public void CheckParameter1Test()
        {
            IntToStreamConverter_Accessor target = new IntToStreamConverter_Accessor();
            Assert.AreEqual<bool>(true, target.CheckParameter(5));
        }

        #endregion

        #region GetLength method tests

        [TestMethod()]
        public void GetLengthTest()
        {
            IntToStreamConverter_Accessor target = new IntToStreamConverter_Accessor();
            Assert.AreEqual<int>(1, target.GetLength(5));
        }

        #endregion

        #region GetStream method tests

        [TestMethod()]
        public void GetStream1Test()
        {
            IntToStreamConverter_Accessor target = new IntToStreamConverter_Accessor();
            Assert.AreEqual<string>("0005", target.GetStream(5));
        }

        [TestMethod()]
        public void GetStream2Test()
        {
            IntToStreamConverter_Accessor target = new IntToStreamConverter_Accessor();
            Assert.AreEqual<string>("000A", target.GetStream(10));
        }

        #endregion
    }
}