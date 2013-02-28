using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class ChangeCassetteToStreamConverterTest
    {
        #region CassetteHopperToStream method tests

        [TestMethod()]
        public void CassetteHopperToStreamTest()
        {
            ChangeCassetteToStreamConverter privateTarget = new ChangeCassetteToStreamConverter();
            ChangeCassetteToStreamConverter_Accessor target = new ChangeCassetteToStreamConverter_Accessor(new PrivateObject(privateTarget,
                                                                                             new PrivateType(typeof(ChangeCassetteToStreamConverter))));

            Assert.AreEqual<string>("0001", target.CassetteHopperToStream(DemountCassetteHopper.Hopper1));
            Assert.AreEqual<string>("0002", target.CassetteHopperToStream(DemountCassetteHopper.Hopper2));
            Assert.AreEqual<string>("0003", target.CassetteHopperToStream(DemountCassetteHopper.Hopper3));
            Assert.AreEqual<string>("0004", target.CassetteHopperToStream(DemountCassetteHopper.Hopper4));
        }

        #endregion

        #region CassetteStationToStream method tests

        [TestMethod()]
        public void CassetteStationToStreamTest()
        {
            ChangeCassetteToStreamConverter privateTarget = new ChangeCassetteToStreamConverter();
            ChangeCassetteToStreamConverter_Accessor target = new ChangeCassetteToStreamConverter_Accessor(new PrivateObject(privateTarget,
                                                                                             new PrivateType(typeof(ChangeCassetteToStreamConverter))));

            Assert.AreEqual<string>("0001", target.DemountCassetteStationToStream(DemountCassetteStation.Station1));
            Assert.AreEqual<string>("0002", target.DemountCassetteStationToStream(DemountCassetteStation.Station2));
            Assert.AreEqual<string>("0003", target.DemountCassetteStationToStream(DemountCassetteStation.Station3));
            Assert.AreEqual<string>("0004", target.DemountCassetteStationToStream(DemountCassetteStation.Station4));
        }

        #endregion

        #region WaferSizeToStream method tests

        [TestMethod()]
        public void WaferSizeToStreamTest()
        {
            ChangeCassetteToStreamConverter privateTarget = new ChangeCassetteToStreamConverter();
            ChangeCassetteToStreamConverter_Accessor target = new ChangeCassetteToStreamConverter_Accessor(new PrivateObject(privateTarget,
                                                                                             new PrivateType(typeof(ChangeCassetteToStreamConverter))));

            Assert.AreEqual<string>("0006", target.WaferSizeToStream(WaferSize.Size6Inches));
            Assert.AreEqual<string>("0008", target.WaferSizeToStream(WaferSize.Size8Inches));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        public void CheckParameterTest()
        {
            ChangeCassetteToStreamConverter_Accessor target = new ChangeCassetteToStreamConverter_Accessor();
            ChangeCassette_Accessor parameter = new ChangeCassette_Accessor(new PrivateObject(new ChangeCassette { Source = DemountCassetteStation.Station1, WaferSize = WaferSize.Size8Inches, Destination = DemountCassetteHopper.Hopper1 }, new PrivateType(typeof(ChangeCassette))));
            Assert.AreEqual<bool>(true, target.CheckParameter(parameter));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException1Test()
        {
            ChangeCassetteToStreamConverter_Accessor target = new ChangeCassetteToStreamConverter_Accessor();
            ChangeCassette_Accessor parameter = new ChangeCassette_Accessor(new PrivateObject(new ChangeCassette { Source = DemountCassetteStation.Cleared, WaferSize = WaferSize.Size8Inches, Destination = DemountCassetteHopper.Hopper1 }, new PrivateType(typeof(ChangeCassette))));
            target.CheckParameter(parameter);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException2Test()
        {
            ChangeCassetteToStreamConverter_Accessor target = new ChangeCassetteToStreamConverter_Accessor();
            ChangeCassette_Accessor parameter = new ChangeCassette_Accessor(new PrivateObject(new ChangeCassette { Source = DemountCassetteStation.Station1, WaferSize = WaferSize.Size8Inches, Destination = DemountCassetteHopper.Cleared }, new PrivateType(typeof(ChangeCassette))));
            target.CheckParameter(parameter);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException3Test()
        {
            ChangeCassetteToStreamConverter_Accessor target = new ChangeCassetteToStreamConverter_Accessor();
            ChangeCassette_Accessor parameter = new ChangeCassette_Accessor(new PrivateObject(new ChangeCassette { Source = DemountCassetteStation.Station1, WaferSize = WaferSize.AnySize, Destination = DemountCassetteHopper.Hopper1 }, new PrivateType(typeof(ChangeCassette))));
            target.CheckParameter(parameter);
        }

        #endregion

        #region GetLength method tests

        [TestMethod()]
        public void GetLengthTest()
        {
            ChangeCassetteToStreamConverter_Accessor target = new ChangeCassetteToStreamConverter_Accessor();
            ChangeCassette_Accessor parameter = new ChangeCassette_Accessor(new PrivateObject(new ChangeCassette { Source = DemountCassetteStation.Station1, WaferSize = WaferSize.Size8Inches, Destination = DemountCassetteHopper.Hopper1 }, new PrivateType(typeof(ChangeCassette))));
            Assert.AreEqual<int>(3, target.GetLength(parameter));
        }

        #endregion

        #region GetStream method tests

        [TestMethod()]
        public void GetStreamTest()
        {
            ChangeCassetteToStreamConverter_Accessor target = new ChangeCassetteToStreamConverter_Accessor();

            ChangeCassette_Accessor parameter1 = new ChangeCassette_Accessor(new PrivateObject(new ChangeCassette { Source = DemountCassetteStation.Station1, WaferSize = WaferSize.Size8Inches, Destination = DemountCassetteHopper.Hopper1 }, new PrivateType(typeof(ChangeCassette))));
            Assert.AreEqual<string>("000100080001", target.GetStream(parameter1));
            ChangeCassette_Accessor parameter2 = new ChangeCassette_Accessor(new PrivateObject(new ChangeCassette { Source = DemountCassetteStation.Station3, WaferSize = WaferSize.Size6Inches, Destination = DemountCassetteHopper.Hopper4 }, new PrivateType(typeof(ChangeCassette))));
            Assert.AreEqual<string>("000300060004", target.GetStream(parameter2));
        }

        #endregion
    }
}