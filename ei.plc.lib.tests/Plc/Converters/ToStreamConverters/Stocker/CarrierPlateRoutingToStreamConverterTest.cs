using Microsoft.VisualStudio.TestTools.UnitTesting;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class CarrierPlateRoutingToStreamConverterTest
    {
        #region CarrierPlateRoutingToStream method tests

        [TestMethod()]
        public void CarrierPlateRoutingToStreamTest()
        {
            CarrierPlateRoutingToStreamConverter_Accessor target = new CarrierPlateRoutingToStreamConverter_Accessor();

            Assert.AreEqual<string>("0001", target.CarrierPlateRoutingToStream(CarrierPlateRouting.InsertIntoMagazine));
            Assert.AreEqual<string>("0002", target.CarrierPlateRoutingToStream(CarrierPlateRouting.Reject));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        public void CheckParameterTest()
        {
            CarrierPlateRoutingToStreamConverter_Accessor target = new CarrierPlateRoutingToStreamConverter_Accessor();

            Assert.AreEqual<bool>(true, target.CheckParameter(CarrierPlateRouting.InsertIntoMagazine));
            Assert.AreEqual<bool>(true, target.CheckParameter(CarrierPlateRouting.Reject));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException1Test()
        {
            CarrierPlateRoutingToStreamConverter_Accessor target = new CarrierPlateRoutingToStreamConverter_Accessor();
            target.CheckParameter(CarrierPlateRouting.Cleared);
        }

        #endregion

        #region GetLength method tests

        [TestMethod()]
        public void GetLengthTest()
        {
            CarrierPlateRoutingToStreamConverter_Accessor target = new CarrierPlateRoutingToStreamConverter_Accessor();
            Assert.AreEqual<int>(1, target.GetLength(CarrierPlateRouting.Reject));
        }

        #endregion

        #region GetStream method tests

        [TestMethod()]
        public void GetStreamTest()
        {
            CarrierPlateRoutingToStreamConverter_Accessor target = new CarrierPlateRoutingToStreamConverter_Accessor();

            Assert.AreEqual<string>("0001", target.GetStream(CarrierPlateRouting.InsertIntoMagazine));
            Assert.AreEqual<string>("0002", target.GetStream(CarrierPlateRouting.Reject));
        }

        #endregion
    }
}