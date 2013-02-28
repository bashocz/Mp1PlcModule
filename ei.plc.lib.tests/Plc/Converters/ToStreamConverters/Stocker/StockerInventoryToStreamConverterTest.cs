using Microsoft.VisualStudio.TestTools.UnitTesting;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class StockerInventoryToStreamConverterTest
    {
        #region StockerInventoryToStream method tests

        [TestMethod()]
        public void StockerInventoryToStreamTest()
        {
            StockerInventoryToStreamConverter_Accessor target = new StockerInventoryToStreamConverter_Accessor();

            Assert.AreEqual<string>("0001", target.StockerInventoryToStream(StockerInventory.SizeAvailable));
            Assert.AreEqual<string>("0002", target.StockerInventoryToStream(StockerInventory.SizeNotInStocker));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        public void CheckParameterTest()
        {
            StockerInventoryToStreamConverter_Accessor target = new StockerInventoryToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(StockerInventory.SizeAvailable));
            Assert.AreEqual<bool>(true, target.CheckParameter(StockerInventory.SizeNotInStocker));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException1Test()
        {
            StockerInventoryToStreamConverter_Accessor target = new StockerInventoryToStreamConverter_Accessor();
            target.CheckParameter(StockerInventory.Cleared);
        }

        #endregion

        #region GetLength method tests

        [TestMethod()]
        public void GetLengthTest()
        {
            StockerInventoryToStreamConverter_Accessor target = new StockerInventoryToStreamConverter_Accessor();
            Assert.AreEqual<int>(1, target.GetLength(StockerInventory.SizeAvailable));
        }

        #endregion

        #region GetStream method tests

        [TestMethod()]
        public void GetStreamTest()
        {
            StockerInventoryToStreamConverter_Accessor target = new StockerInventoryToStreamConverter_Accessor();

            Assert.AreEqual<string>("0001", target.GetStream(StockerInventory.SizeAvailable));
            Assert.AreEqual<string>("0002", target.GetStream(StockerInventory.SizeNotInStocker));
        }

        #endregion
    }
}