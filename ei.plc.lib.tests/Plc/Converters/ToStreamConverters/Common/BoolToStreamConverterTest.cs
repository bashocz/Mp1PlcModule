using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class BoolToStreamConverterTest
    {
        #region CheckParameter method tests

        [TestMethod()]
        public void CheckParameter1Test()
        {
            BoolToStreamConverter_Accessor target = new BoolToStreamConverter_Accessor();
            Assert.AreEqual<bool>(true, target.CheckParameter(true));
        }

        [TestMethod()]
        public void CheckParameter2Test()
        {
            BoolToStreamConverter_Accessor target = new BoolToStreamConverter_Accessor();
            Assert.AreEqual<bool>(true, target.CheckParameter(false));
        }

        #endregion

        #region GetLength method tests

        [TestMethod()]
        public void GetLength1Test()
        {
            BoolToStreamConverter_Accessor target = new BoolToStreamConverter_Accessor();
            Assert.AreEqual<int>(1, target.GetLength(true));
        }

        [TestMethod()]
        public void GetLength2Test()
        {
            BoolToStreamConverter_Accessor target = new BoolToStreamConverter_Accessor();
            Assert.AreEqual<int>(1, target.GetLength(false));
        }

        #endregion

        #region GetStream method tests

        [TestMethod()]
        public void GetStream1Test()
        {
            BoolToStreamConverter_Accessor target = new BoolToStreamConverter_Accessor();
            Assert.AreEqual<string>("0000", target.GetStream(false));
        }

        [TestMethod()]
        public void GetStream2Test()
        {
            BoolToStreamConverter_Accessor target = new BoolToStreamConverter_Accessor();
            Assert.AreEqual<string>("0001", target.GetStream(true));
        }

        #endregion
    }
}