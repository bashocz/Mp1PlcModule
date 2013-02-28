using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class LotCassetteInfoToStreamConverterTest
    {
        #region LotDataToStream method tests

        [TestMethod()]
        public void LotDataToStream1Test()
        {
            string lotId = "NCHFYRT";
            List<ICassette> cassettes = PlcHelper.GetCassetteList(2);
            LotCassetteInfoToStreamConverter_Accessor target = new LotCassetteInfoToStreamConverter_Accessor();

            StringConverter stringConverter = new StringConverter();
            IntHexConverter intConverter = new IntHexConverter();

            string expected = stringConverter.ToStream(lotId).PadRight(28, '0');
            expected += stringConverter.ToStream(cassettes[0].CassetteId);
            expected = expected.PadRight(44, '0');
            expected += stringConverter.ToStream(cassettes[1].CassetteId);
            expected = expected.PadRight(60, '0');
            expected = expected.PadRight(220, '0') + intConverter.ToStream(cassettes.Count);

            Assert.AreEqual<string>(expected, target.LotDataToStream(PlcHelper.GetLotData(lotId, cassettes, PlcHelper.GetWaferList(50, 2))));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        public void CheckParameterTest()
        {
            LotCassetteInfoToStreamConverter_Accessor target = new LotCassetteInfoToStreamConverter_Accessor();
            Assert.AreEqual<bool>(true, target.CheckParameter(PlcHelper.GetLotData()));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException1Test()
        {
            string lotId = "ABCDEFGHIJKLMN";
            LotCassetteInfoToStreamConverter_Accessor target = new LotCassetteInfoToStreamConverter_Accessor();
            target.CheckParameter(PlcHelper.GetLotData(lotId, PlcHelper.GetCassetteList(13), PlcHelper.GetWaferList(325, 13)));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException2Test()
        {
            string lotId = "";
            List<ICassette> cassettes = PlcHelper.GetCassetteList(2);
            List<IWafer> wafers = PlcHelper.GetWaferList(50, 2);

            LotCassetteInfoToStreamConverter_Accessor target = new LotCassetteInfoToStreamConverter_Accessor();
            target.CheckParameter(PlcHelper.GetLotData(lotId, cassettes, wafers));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException3Test()
        {
            string lotId = "NCHFYRT";
            Mock<ICassette> cassette = new Mock<ICassette>();
            cassette.Setup(x => x.CassetteId).Returns("");

            List<ICassette> cassettes = PlcHelper.GetCassetteList(2);
            cassettes.Add(cassette.Object);

            List<IWafer> wafers = PlcHelper.GetWaferList(60, 3);

            LotCassetteInfoToStreamConverter_Accessor target = new LotCassetteInfoToStreamConverter_Accessor();
            target.CheckParameter(PlcHelper.GetLotData(lotId, cassettes, wafers));
        }


        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException4Test()
        {
            string lotId = "KDJFHRTE";
            List<ICassette> cassettes = PlcHelper.GetCassetteList(0);
            List<IWafer> wafers = PlcHelper.GetWaferList(0, 0);

            LotCassetteInfoToStreamConverter_Accessor target = new LotCassetteInfoToStreamConverter_Accessor();
            target.CheckParameter(PlcHelper.GetLotData(lotId, cassettes, wafers));
        }

        #endregion

        #region GetLength method tests

        [TestMethod()]
        public void GetLengthTest()
        {
            LotCassetteInfoToStreamConverter_Accessor target = new LotCassetteInfoToStreamConverter_Accessor();
            Assert.AreEqual<int>(56, target.GetLength(PlcHelper.GetLotData()));
        }

        #endregion

        #region GetStream method tests

        [TestMethod()]
        public void GetStream1Test()
        {
            string lotId = "NCHFYRT";
            List<ICassette> cassettes = PlcHelper.GetCassetteList(2);
            LotCassetteInfoToStreamConverter_Accessor target = new LotCassetteInfoToStreamConverter_Accessor();

            StringConverter stringConverter = new StringConverter();
            IntConverter intConverter = new IntConverter();

            string expected = stringConverter.ToStream(lotId).PadRight(28, '0');
            expected += stringConverter.ToStream(cassettes[0].CassetteId.PadRight(8));
            expected = expected.PadRight(44, '0');
            expected += stringConverter.ToStream(cassettes[1].CassetteId.PadRight(8));
            expected = expected.PadRight(60, '0');
            expected = expected.PadRight(220, '0') + intConverter.ToStream(cassettes.Count);

            Assert.AreEqual<string>(expected, target.GetStream(PlcHelper.GetLotData(lotId, cassettes, PlcHelper.GetWaferList(50, 2))));
        }

        [TestMethod()]
        public void GetStream2Test()
        {
            string lotId = "NCHFYRT";
            List<ICassette> cassettes = PlcHelper.GetCassetteList(12);
            LotCassetteInfoToStreamConverter_Accessor target = new LotCassetteInfoToStreamConverter_Accessor();

            StringConverter stringConverter = new StringConverter();
            IntConverter intConverter = new IntConverter();

            string expected = stringConverter.ToStream(lotId).PadRight(28, '0');
            expected += stringConverter.ToStream(cassettes[0].CassetteId.PadRight(8));
            expected = expected.PadRight(44, '0');
            expected += stringConverter.ToStream(cassettes[1].CassetteId.PadRight(8));
            expected = expected.PadRight(60, '0');
            expected += stringConverter.ToStream(cassettes[2].CassetteId.PadRight(8));
            expected = expected.PadRight(76, '0');
            expected += stringConverter.ToStream(cassettes[3].CassetteId.PadRight(8));
            expected = expected.PadRight(92, '0');
            expected += stringConverter.ToStream(cassettes[4].CassetteId.PadRight(8));
            expected = expected.PadRight(108, '0');
            expected += stringConverter.ToStream(cassettes[5].CassetteId.PadRight(8));
            expected = expected.PadRight(124, '0');
            expected += stringConverter.ToStream(cassettes[6].CassetteId.PadRight(8));
            expected = expected.PadRight(140, '0');
            expected += stringConverter.ToStream(cassettes[7].CassetteId.PadRight(8));
            expected = expected.PadRight(156, '0');
            expected += stringConverter.ToStream(cassettes[8].CassetteId.PadRight(8));
            expected = expected.PadRight(172, '0');
            expected += stringConverter.ToStream(cassettes[9].CassetteId.PadRight(8));
            expected = expected.PadRight(188, '0');
            expected += stringConverter.ToStream(cassettes[10].CassetteId.PadRight(8));
            expected = expected.PadRight(204, '0');
            expected += stringConverter.ToStream(cassettes[11].CassetteId.PadRight(8));
            expected = expected.PadRight(220, '0');

            expected = expected.PadRight(220, '0') + intConverter.ToStream(cassettes.Count);
            Assert.AreEqual<string>(expected, target.GetStream(PlcHelper.GetLotData(lotId, cassettes, PlcHelper.GetWaferList(300, 12))));
        }

        [TestMethod()]
        public void GetStream3Test()
        {
            string lotId = "ABCDEF";
            List<ICassette> cassettes = PlcHelper.GetCassetteList(5);
            LotCassetteInfoToStreamConverter_Accessor target = new LotCassetteInfoToStreamConverter_Accessor();

            StringConverter stringConverter = new StringConverter();
            IntConverter intConverter = new IntConverter();

            string expected = stringConverter.ToStream(lotId).PadRight(28, '0');
            expected += stringConverter.ToStream(cassettes[0].CassetteId.PadRight(8));
            expected = expected.PadRight(44, '0');
            expected += stringConverter.ToStream(cassettes[1].CassetteId.PadRight(8));
            expected = expected.PadRight(60, '0');
            expected += stringConverter.ToStream(cassettes[2].CassetteId.PadRight(8));
            expected = expected.PadRight(76, '0');
            expected += stringConverter.ToStream(cassettes[3].CassetteId.PadRight(8));
            expected = expected.PadRight(92, '0');
            expected += stringConverter.ToStream(cassettes[4].CassetteId.PadRight(8));
            expected = expected.PadRight(108, '0');

            expected = expected.PadRight(220, '0') + intConverter.ToStream(cassettes.Count);
            Assert.AreEqual<string>(expected, target.GetStream(PlcHelper.GetLotData(lotId, cassettes, PlcHelper.GetWaferList(125, 5))));
        }

        [TestMethod()]
        public void GetStream4Test()
        {
            string lotId = "AA";

            List<ICassette> cassettes = new List<ICassette>() { new Cassette() { CassetteId = "222" } };
            LotCassetteInfoToStreamConverter_Accessor target = new LotCassetteInfoToStreamConverter_Accessor();

            StringConverter stringConverter = new StringConverter();
            IntConverter intConverter = new IntConverter();

            string expected = stringConverter.ToStream(lotId).PadRight(28, '0');
            expected += "3232322020202020";

            expected = expected.PadRight(220, '0') + intConverter.ToStream(cassettes.Count);
            Assert.AreEqual<string>(expected, target.GetStream(PlcHelper.GetLotData(lotId, cassettes, PlcHelper.GetWaferList(125, 5))));
        }

        #endregion
    }
}