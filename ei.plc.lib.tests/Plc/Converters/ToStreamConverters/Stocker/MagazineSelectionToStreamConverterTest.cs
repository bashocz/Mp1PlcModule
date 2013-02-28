using Microsoft.VisualStudio.TestTools.UnitTesting;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class MagazineSelectionToStreamConverterTest
    {
        #region MagazineSelectionToStream method tests

        [TestMethod()]
        public void MagazineSelectionToStreamTest()
        {
            MagazineSelectionToStreamConverter_Accessor target = new MagazineSelectionToStreamConverter_Accessor();

            Assert.AreEqual<string>("0001", target.MagazineSelectionToStream(MagazineSelection.HasRequestedSize));
            Assert.AreEqual<string>("0002", target.MagazineSelectionToStream(MagazineSelection.DoesNotHaveRequestedSize));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        public void CheckParameterTest()
        {
            MagazineSelectionToStreamConverter_Accessor target = new MagazineSelectionToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(MagazineSelection.HasRequestedSize));
            Assert.AreEqual<bool>(true, target.CheckParameter(MagazineSelection.DoesNotHaveRequestedSize));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException1Test()
        {
            MagazineSelectionToStreamConverter_Accessor target = new MagazineSelectionToStreamConverter_Accessor();
            target.CheckParameter(MagazineSelection.Cleared);
        }

        #endregion

        #region GetLength method tests

        [TestMethod()]
        public void GetLengthTest()
        {
            MagazineSelectionToStreamConverter_Accessor target = new MagazineSelectionToStreamConverter_Accessor();
            Assert.AreEqual<int>(1, target.GetLength(MagazineSelection.HasRequestedSize));
        }

        #endregion

        #region GetStream method tests

        [TestMethod()]
        public void GetStreamTest()
        {
            MagazineSelectionToStreamConverter_Accessor target = new MagazineSelectionToStreamConverter_Accessor();

            Assert.AreEqual<string>("0001", target.GetStream(MagazineSelection.HasRequestedSize));
            Assert.AreEqual<string>("0002", target.GetStream(MagazineSelection.DoesNotHaveRequestedSize));
        }

        #endregion
    }
}