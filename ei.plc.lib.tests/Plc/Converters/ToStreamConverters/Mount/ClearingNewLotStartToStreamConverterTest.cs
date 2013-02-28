using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class ClearingNewLotStartToStreamConverterTest
    {
        #region CheckParameter method tests

        [TestMethod()]
        public void CheckParameterTest()
        {
            ClearingNewLotStartToStreamConverter_Accessor target = new ClearingNewLotStartToStreamConverter_Accessor();
            Assert.AreEqual<bool>(true, target.CheckParameter(null));
        }

        #endregion

        #region GetLength method tests

        [TestMethod()]
        public void GetLengthTest()
        {
            ClearingNewLotStartToStreamConverter_Accessor target = new ClearingNewLotStartToStreamConverter_Accessor();
            Assert.AreEqual<int>(9, target.GetLength(null));
        }

        #endregion

        #region GetStream method tests

        [TestMethod()]
        public void GetStreamTest()
        {
            ClearingNewLotStartToStreamConverter_Accessor target = new ClearingNewLotStartToStreamConverter_Accessor();
            Assert.AreEqual<string>("000000000000000000000000000000000000", target.GetStream(null));
        }

        #endregion
    }
}