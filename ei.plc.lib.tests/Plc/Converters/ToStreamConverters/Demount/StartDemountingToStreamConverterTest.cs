using Microsoft.VisualStudio.TestTools.UnitTesting;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class StartDemountingToStreamConverterTest
    {
        #region CassetteStationToStream method tests

        [TestMethod()]
        public void CassetteStationToStreamTest()
        {
            StartDemountingToStreamConverter privateTarget = new StartDemountingToStreamConverter();
            StartDemountingToStreamConverter_Accessor target = new StartDemountingToStreamConverter_Accessor(new PrivateObject(privateTarget,
                                                                                               new PrivateType(typeof(StartDemountingToStreamConverter))));

            Assert.AreEqual<string>("0001", target.CassetteStationToStream(DemountCassetteStation.Station1));
            Assert.AreEqual<string>("0002", target.CassetteStationToStream(DemountCassetteStation.Station2));
            Assert.AreEqual<string>("0003", target.CassetteStationToStream(DemountCassetteStation.Station3));
            Assert.AreEqual<string>("0004", target.CassetteStationToStream(DemountCassetteStation.Station4));
        }

        #endregion

        #region WaferSizeToStream method tests

        [TestMethod()]
        public void WaferSizeToStreamTest()
        {
            var privateTarget = new StartDemountingToStreamConverter();
            var target = new StartDemountingToStreamConverter_Accessor(new PrivateObject(privateTarget,
                                                                new PrivateType(typeof(StartDemountingToStreamConverter))));

            Assert.AreEqual<string>("0006", target.WaferSizeToStream(WaferSize.Size6Inches));
            Assert.AreEqual<string>("0008", target.WaferSizeToStream(WaferSize.Size8Inches));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        public void CheckParameterTest()
        {
            StartDemountingToStreamConverter_Accessor target = new StartDemountingToStreamConverter_Accessor();
            StartDemounting_Accessor parameter = new StartDemounting_Accessor(new PrivateObject(new StartDemounting { Size = WaferSize.Size8Inches, Count = 5, Station = DemountCassetteStation.Station1 }, new PrivateType(typeof(StartDemounting))));
            Assert.AreEqual<bool>(true, target.CheckParameter(parameter));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException1Test()
        {
            StartDemountingToStreamConverter_Accessor target = new StartDemountingToStreamConverter_Accessor();
            StartDemounting_Accessor parameter = new StartDemounting_Accessor(new PrivateObject(new StartDemounting { Size = WaferSize.Size8Inches, Count = 5, Station = DemountCassetteStation.Cleared }, new PrivateType(typeof(StartDemounting))));
            target.CheckParameter(parameter);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException2Test()
        {
            StartDemountingToStreamConverter_Accessor target = new StartDemountingToStreamConverter_Accessor();
            StartDemounting_Accessor parameter = new StartDemounting_Accessor(new PrivateObject(new StartDemounting { Size = WaferSize.Size6Inches, Count = 2, Station = DemountCassetteStation.Station1 }, new PrivateType(typeof(StartDemounting))));
            target.CheckParameter(parameter);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException3Test()
        {
            StartDemountingToStreamConverter_Accessor target = new StartDemountingToStreamConverter_Accessor();
            StartDemounting_Accessor parameter = new StartDemounting_Accessor(new PrivateObject(new StartDemounting { Size = WaferSize.Size6Inches, Count = 9, Station = DemountCassetteStation.Station1 }, new PrivateType(typeof(StartDemounting))));
            target.CheckParameter(parameter);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException4Test()
        {
            StartDemountingToStreamConverter_Accessor target = new StartDemountingToStreamConverter_Accessor();
            StartDemounting_Accessor parameter = new StartDemounting_Accessor(new PrivateObject(new StartDemounting { Size = WaferSize.Size8Inches, Count = 2, Station = DemountCassetteStation.Station1 }, new PrivateType(typeof(StartDemounting))));
            target.CheckParameter(parameter);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException5Test()
        {
            StartDemountingToStreamConverter_Accessor target = new StartDemountingToStreamConverter_Accessor();
            StartDemounting_Accessor parameter = new StartDemounting_Accessor(new PrivateObject(new StartDemounting { Size = WaferSize.Size8Inches, Count = 6, Station = DemountCassetteStation.Station1 }, new PrivateType(typeof(StartDemounting))));
            target.CheckParameter(parameter);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowsPlcException6Test()
        {
            StartDemountingToStreamConverter_Accessor target = new StartDemountingToStreamConverter_Accessor();
            StartDemounting_Accessor parameter = new StartDemounting_Accessor(new PrivateObject(new StartDemounting { Size = WaferSize.AnySize, Count = 5, Station = DemountCassetteStation.Station1 }, new PrivateType(typeof(StartDemounting))));
            target.CheckParameter(parameter);
        }

        #endregion

        #region GetLength method tests

        [TestMethod()]
        public void GetLengthTest()
        {
            StartDemountingToStreamConverter_Accessor target = new StartDemountingToStreamConverter_Accessor();
            StartDemounting_Accessor parameter = new StartDemounting_Accessor(new PrivateObject(new StartDemounting { Size = WaferSize.Size8Inches, Count = 5, Station = DemountCassetteStation.Station1 }, new PrivateType(typeof(StartDemounting))));
            Assert.AreEqual<int>(3, target.GetLength(parameter));
        }

        #endregion

        #region GetStream method tests

        [TestMethod()]
        public void GetStreamTest()
        {
            StartDemountingToStreamConverter_Accessor target = new StartDemountingToStreamConverter_Accessor();

            StartDemounting_Accessor parameter1 = new StartDemounting_Accessor(new PrivateObject(new StartDemounting { Size = WaferSize.Size8Inches, Count = 5, Station = DemountCassetteStation.Station1 }, new PrivateType(typeof(StartDemounting))));
            Assert.AreEqual<string>("000800050001", target.GetStream(parameter1));
            StartDemounting_Accessor parameter2 = new StartDemounting_Accessor(new PrivateObject(new StartDemounting { Size = WaferSize.Size6Inches, Count = 4, Station = DemountCassetteStation.Station3 }, new PrivateType(typeof(StartDemounting))));
            Assert.AreEqual<string>("000600040003", target.GetStream(parameter2));
        }

        #endregion
    }
}