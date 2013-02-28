using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class ProcessRecipeToStreamConverterTest
    {
        #region MagazineDataToStream method tests

        [TestMethod()]
        public void MagazineDataToStreamTest()
        {
            List<ICarrierPlate> plates = PlcHelper.GetPlateList(1, "NCG", "KFHRTE", "V", "YETSEAFQ");

            Mock<IMagazine> magazine = new Mock<IMagazine>();
            magazine.Setup(x => x.Id).Returns("KFYI7W");
            magazine.Setup(x => x.Plates).Returns(plates);

            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();
            StringConverter stringConverter = new StringConverter();

            string expected = stringConverter.ToStream(magazine.Object.Id).PadRight(16, '0');
            expected += stringConverter.ToStream(magazine.Object.Plates[0].Id);
            expected = expected.PadRight(32, '0');
            expected += stringConverter.ToStream(magazine.Object.Plates[1].Id);
            expected = expected.PadRight(48, '0');
            expected += stringConverter.ToStream(magazine.Object.Plates[2].Id);
            expected = expected.PadRight(64, '0');
            expected += stringConverter.ToStream(magazine.Object.Plates[3].Id);
            expected = expected.PadRight(80, '0');
            // recipes
            expected += "0001000100010001";
            
            Assert.AreEqual<string>(expected, target.MagazineDataToStream(magazine.Object));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        public void CheckParameter1Test()
        {
            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(PlcHelper.GetMagazine("FHT5E",
                                                                                           PlcHelper.GetPlateList(1, "UFH4E", "ERT7A", "CP7F6", "QAKGT"))));
        }

        [TestMethod()]
        public void CheckParameter2Test()
        {
            // value of recipe = 1
            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(PlcHelper.GetMagazine("KCHF1EUS",
                                                                                           PlcHelper.GetPlateList(1, "SDW", "MSHFE", "PSHD7SE6", "JDHFYR"))));
        }

        [TestMethod()]
        public void CheckParameter3Test()
        {
            // value of recipe = 25
            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(PlcHelper.GetMagazine("KCHF1EUS",
                                                                                           PlcHelper.GetPlateList(25, "SDW", "MSHFE", "PSHD7SE6", "JDHFYR"))));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowsPlcException1Test()
        {
            // invalid length of magazine ID < 1
            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(PlcHelper.GetMagazine("",
                                                                                           PlcHelper.GetPlateList(1, "UFH4E", "ERT7A", "CP7F6", "QAKGT"))));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowsPlcException2Test()
        {
            // invalid length of magazine ID > 8
            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(PlcHelper.GetMagazine("KCHF1EUS6",
                                                                                           PlcHelper.GetPlateList(1, "UFH4E", "ERT7A", "CP7F6", "QAKGT"))));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowsPlcException3Test()
        {
            // invalid length of carrier plate ID < 1
            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(PlcHelper.GetMagazine("KCHF1EUS",
                                                                                           PlcHelper.GetPlateList(1, "", "ERT7A", "CP7F6", "QAKGT"))));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowsPlcException4Test()
        {
            // invalid length of carrier plate ID > 8
            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(PlcHelper.GetMagazine("KCHF1EUS",
                                                                                           PlcHelper.GetPlateList(1, "SDW", "MSHFE", "EIR", "PSHD7SE6I"))));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowsPlcException5Test()
        {
            // invalid number of plates < 1
            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(PlcHelper.GetMagazine("KCHF1EUS",
                                                                                           PlcHelper.GetPlateList(1))));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowsPlcException6Test()
        {
            // invalid number of plates > 4
            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(PlcHelper.GetMagazine("KCHF1EUS",
                                                                                           PlcHelper.GetPlateList(1, "DHFYRE", "SRWEQ", "KFHE", "IFUR", "PE"))));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowsPlcException7Test()
        {
            // invalid value of recipe < 1
            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(PlcHelper.GetMagazine("KCHF1EUS",
                                                                                           PlcHelper.GetPlateList(0x00, "SDW", "MSHFE", "PSHD7SE6", "JDHFYR"))));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowsPlcException8Test()
        {
            // invalid value of recipe > 25
            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(PlcHelper.GetMagazine("KCHF1EUS",
                                                                                           PlcHelper.GetPlateList(0x26, "SDW", "MSHFE", "PSHD7SE6", "JDHFYR"))));
        }

        #endregion

        #region GetLength method tests

        [TestMethod()]
        public void GetLengthTest()
        {
            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();

            Assert.AreEqual<int>(24, target.GetLength(PlcHelper.GetMagazine("FHT5E",
                                                                                           PlcHelper.GetPlateList(1, "UFH4E", "ERT7A", "CP7F6", "QAKGT"))));
        }

        #endregion

        #region GetStream method tests

        [TestMethod()]
        public void GetStream1Test()
        {
            List<ICarrierPlate> plates = PlcHelper.GetPlateList(1, "NCG", "KFHRTE", "V", "YETSEAFQ");

            Mock<IMagazine> magazine = new Mock<IMagazine>();
            magazine.Setup(x => x.Id).Returns("KFYI7W");
            magazine.Setup(x => x.Plates).Returns(plates);

            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();
            StringConverter stringConverter = new StringConverter();

            string expected = stringConverter.ToStream(magazine.Object.Id).PadRight(16, '0');
            expected += stringConverter.ToStream(magazine.Object.Plates[0].Id);
            expected = expected.PadRight(32, '0');
            expected += stringConverter.ToStream(magazine.Object.Plates[1].Id);
            expected = expected.PadRight(48, '0');
            expected += stringConverter.ToStream(magazine.Object.Plates[2].Id);
            expected = expected.PadRight(64, '0');
            expected += stringConverter.ToStream(magazine.Object.Plates[3].Id);
            expected = expected.PadRight(80, '0');
            // recipes
            expected += "0001000100010001";

            Assert.AreEqual<string>(expected, target.GetStream(magazine.Object));
        }

        [TestMethod()]
        public void GetStream25Test()
        {
            List<ICarrierPlate> plates = PlcHelper.GetPlateList(25, "NCG", "KFHRTE", "V", "YETSEAFQ");

            Mock<IMagazine> magazine = new Mock<IMagazine>();
            magazine.Setup(x => x.Id).Returns("KFYI7W");
            magazine.Setup(x => x.Plates).Returns(plates);

            ProcessRecipeToStreamConverter_Accessor target = new ProcessRecipeToStreamConverter_Accessor();
            StringConverter stringConverter = new StringConverter();

            string expected = stringConverter.ToStream(magazine.Object.Id).PadRight(16, '0');
            expected += stringConverter.ToStream(magazine.Object.Plates[0].Id);
            expected = expected.PadRight(32, '0');
            expected += stringConverter.ToStream(magazine.Object.Plates[1].Id);
            expected = expected.PadRight(48, '0');
            expected += stringConverter.ToStream(magazine.Object.Plates[2].Id);
            expected = expected.PadRight(64, '0');
            expected += stringConverter.ToStream(magazine.Object.Plates[3].Id);
            expected = expected.PadRight(80, '0');
            // recipes
            expected += "0025002500250025";

            Assert.AreEqual<string>(expected, target.GetStream(magazine.Object));
        }

        #endregion
    }
}