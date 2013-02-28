using Microsoft.VisualStudio.TestTools.UnitTesting;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class LotDataTransmissionToStreamConverterTest
    {
        #region ParseLotDataTransmissionToStream method tests

        [TestMethod()]
        public void ParseLotDataTransmissionToStream1Test()
        {
            LotDataTransmissionToStreamConverter_Accessor target = new LotDataTransmissionToStreamConverter_Accessor();
            Assert.AreEqual<string>("0000", target.LotDataTransmissionToStream(LotDataTransmission.Cleared));
        }

        [TestMethod()]
        public void ParseLotDataTransmissionToStream2Test()
        {
            LotDataTransmissionToStreamConverter_Accessor target = new LotDataTransmissionToStreamConverter_Accessor();
            Assert.AreEqual<string>("0001", target.LotDataTransmissionToStream(LotDataTransmission.BeforeWritingCassetteInfo));
        }

        [TestMethod()]
        public void ParseLotDataTransmissionToStream3Test()
        {
            LotDataTransmissionToStreamConverter_Accessor target = new LotDataTransmissionToStreamConverter_Accessor();
            Assert.AreEqual<string>("0002", target.LotDataTransmissionToStream(LotDataTransmission.BeforeWritingWaferInfo));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        public void CheckParameterTest()
        {
            LotDataTransmissionToStreamConverter_Accessor target = new LotDataTransmissionToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(LotDataTransmission.Cleared));
            Assert.AreEqual<bool>(true, target.CheckParameter(LotDataTransmission.BeforeWritingCassetteInfo));
            Assert.AreEqual<bool>(true, target.CheckParameter(LotDataTransmission.BeforeWritingWaferInfo));
        }

        #endregion

        #region GetLength method tests

        [TestMethod()]
        public void GetLengthTest()
        {
            LotDataTransmissionToStreamConverter_Accessor target = new LotDataTransmissionToStreamConverter_Accessor();
            Assert.AreEqual<int>(1, target.GetLength(LotDataTransmission.BeforeWritingCassetteInfo));
        }

        #endregion

        #region GetStream method tests

        [TestMethod()]
        public void GetStreamTest()
        {
            LotDataTransmissionToStreamConverter_Accessor target = new LotDataTransmissionToStreamConverter_Accessor();

            Assert.AreEqual<string>("0000", target.GetStream(LotDataTransmission.Cleared));
            Assert.AreEqual<string>("0001", target.GetStream(LotDataTransmission.BeforeWritingCassetteInfo));
            Assert.AreEqual<string>("0002", target.GetStream(LotDataTransmission.BeforeWritingWaferInfo));
        }

        #endregion
    }
}