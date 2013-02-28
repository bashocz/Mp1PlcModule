using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class LotWaferInfoToStreamConverterTest
    {
        #region WaferSizeToStream method tests

        [TestMethod()]
        public void WaferSizeToStreamTest()
        {
            LotWaferInfoToStreamConverter privateTarget = new LotWaferInfoToStreamConverter();
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor(new PrivateObject(privateTarget,
                                                                                         new PrivateType(typeof(LotWaferInfoToStreamConverter))));

            Assert.AreEqual<string>("0006", target.WaferSizeToStream(WaferSize.Size6Inches));
            Assert.AreEqual<string>("0008", target.WaferSizeToStream(WaferSize.Size8Inches));
        }

        #endregion

        #region OfTypeToStream method tests

        [TestMethod()]
        public void OFTypeToStreamTest()
        {
            LotWaferInfoToStreamConverter privateTarget = new LotWaferInfoToStreamConverter();
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor(new PrivateObject(privateTarget,
                                                                                         new PrivateType(typeof(LotWaferInfoToStreamConverter))));

            Assert.AreEqual<string>("0001", target.OfTypeToStream(OfType.OF));
            Assert.AreEqual<string>("0002", target.OfTypeToStream(OfType.VNotch));
        }

        #endregion

        #region PolishDivisionToStream method tests

        [TestMethod()]
        public void PolishDivisionToStreamTest()
        {
            LotWaferInfoToStreamConverter privateTarget = new LotWaferInfoToStreamConverter();
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor(new PrivateObject(privateTarget,
                                                                                         new PrivateType(typeof(LotWaferInfoToStreamConverter))));

            Assert.AreEqual<string>("0001", target.PolishDivisionToStream(PolishDivision.New));
            Assert.AreEqual<string>("0002", target.PolishDivisionToStream(PolishDivision.Repolish));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        public void CheckParameterTest()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();
            Assert.AreEqual<bool>(true, target.CheckParameter(PlcHelper.GetLotData()));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException1Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of waferCount out of range value
            List<IWafer> listOfWafers = PlcHelper.GetWaferList(2, 1);
            List<ICassette> listOfCassettes = PlcHelper.GetCassetteList(1);

            target.CheckParameter(PlcHelper.GetLotData("ABCDEFGHIJKLMN", listOfCassettes, listOfWafers));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException2Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of waferCount out of range value
            List<IWafer> listOfWafers = PlcHelper.GetWaferList(301, 13);
            List<ICassette> listOfCassettes = PlcHelper.GetCassetteList(13);

            target.CheckParameter(PlcHelper.GetLotData("ABCDEFGHIJKLMN", listOfCassettes, listOfWafers));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException3Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of nGWaferCount out of range value
            target.CheckParameter(PlcHelper.GetLotData(-1));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException4Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of nGWaferCount out of range value
            target.CheckParameter(PlcHelper.GetLotData(298));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException5Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of waferSize invalid value
            target.CheckParameter(PlcHelper.GetLotData("BCGFTSFQ", PlcHelper.GetCassetteList(2), 4, WaferSize.AnySize, OfType.OF,
                                                              PolishDivision.New, 5, 6, 5, 6, PlcHelper.GetWaferList(50, 2)));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException6Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of ofType invalid value
            target.CheckParameter(PlcHelper.GetLotData("BCGFTSFQ", PlcHelper.GetCassetteList(2), 4, WaferSize.Size6Inches, OfType.Cleared,
                                                              PolishDivision.New, 5, 6, 5, 6, PlcHelper.GetWaferList(50, 2)));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException7Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of polishDivision invalid value
            target.CheckParameter(PlcHelper.GetLotData("BCGFTSFQ", PlcHelper.GetCassetteList(2), 4, WaferSize.Size6Inches, OfType.OF,
                                                              PolishDivision.Cleared, 5, 6, 5, 6, PlcHelper.GetWaferList(50, 2)));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException50Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of assembly1CarrierPlateCount out of range value
            target.CheckParameter(PlcHelper.GetLotData(-1, 3, 26, 8));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException60Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of assembly1CarrierPlateCount out of range value
            target.CheckParameter(PlcHelper.GetLotData(100, 3, 26, 8));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException70Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of assembly1WaferCount out of range value
            target.CheckParameter(PlcHelper.GetLotData(50, 2, 26, 8));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException80Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of assembly1WaferCount out of range value
            target.CheckParameter(PlcHelper.GetLotData(50, 9, 26, 8));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException90Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of assembly2CarrierPlateCount out of range value
            target.CheckParameter(PlcHelper.GetLotData(50, 8, -1, 8));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException100Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of assembly2CarrierPlateCount out of range value
            target.CheckParameter(PlcHelper.GetLotData(50, 8, 100, 8));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException110Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of assembly2WaferCount out of range value
            target.CheckParameter(PlcHelper.GetLotData(50, 8, 50, 2));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException120Test()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            // test of assembly2WaferCount out of range value
            target.CheckParameter(PlcHelper.GetLotData(50, 8, 50, 9));
        }


        #endregion

        #region GetLength method tests

        [TestMethod()]
        public void GetLengthTest()
        {
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();
            Assert.AreEqual<int>(9, target.GetLength(PlcHelper.GetLotData()));
        }

        #endregion

        #region GetStream method tests

        [TestMethod()]
        public void GetStreamTest()
        {
            ILotData lotData = PlcHelper.GetLotData();
            LotWaferInfoToStreamConverter_Accessor target = new LotWaferInfoToStreamConverter_Accessor();

            IntConverter converter = new IntConverter();

            string stream = converter.ToStream(lotData.Wafers.Count)
                            + converter.ToStream(lotData.NGWaferCount)
                            + "0008"
                            + "0002"
                            + "0001"
                            + converter.ToStream(lotData.Assembly1.CarrierPlateCount)
                            + converter.ToStream(lotData.Assembly1.WaferCount)
                            + converter.ToStream(lotData.Assembly2.CarrierPlateCount)
                            + converter.ToStream(lotData.Assembly2.WaferCount);

            Assert.AreEqual<string>(stream, target.GetStream(lotData));
        }

        #endregion
    }
}