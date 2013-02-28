using Microsoft.VisualStudio.TestTools.UnitTesting;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class EmptyCarrierPlateRoutingToStreamConverterTest
    {
        #region CarrierPlateRoutingTypeToStream method tests

        [TestMethod()]
        public void CarrierPlateRoutingTypeToStreamTest()
        {
            EmptyCarrierPlateRoutingToStreamConverter privateTarget = new EmptyCarrierPlateRoutingToStreamConverter();
            EmptyCarrierPlateRoutingToStreamConverter_Accessor target = new EmptyCarrierPlateRoutingToStreamConverter_Accessor(new PrivateObject(privateTarget,
                                                                                           new PrivateType(typeof(EmptyCarrierPlateRoutingToStreamConverter))));

            Assert.AreEqual<string>("0001", target.CarrierPlateRoutingTypeToStream(CarrierPlateRoutingType.BackThroughAwps));
            Assert.AreEqual<string>("0002", target.CarrierPlateRoutingTypeToStream(CarrierPlateRoutingType.InspectionRequired));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        public void CheckParameterTest()
        {
            EmptyCarrierPlateRoutingToStreamConverter_Accessor target1 = new EmptyCarrierPlateRoutingToStreamConverter_Accessor();
            Assert.AreEqual<bool>(true, target1.CheckParameter(CarrierPlateRoutingType.BackThroughAwps));

            EmptyCarrierPlateRoutingToStreamConverter_Accessor target2 = new EmptyCarrierPlateRoutingToStreamConverter_Accessor();
            Assert.AreEqual<bool>(true, target2.CheckParameter(CarrierPlateRoutingType.InspectionRequired));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException1Test()
        {
            EmptyCarrierPlateRoutingToStreamConverter_Accessor target = new EmptyCarrierPlateRoutingToStreamConverter_Accessor();
            target.CheckParameter(CarrierPlateRoutingType.Cleared);
        }

        #endregion

        #region GetLength method tests

        [TestMethod()]
        public void GetLengthTest()
        {
            EmptyCarrierPlateRoutingToStreamConverter_Accessor target = new EmptyCarrierPlateRoutingToStreamConverter_Accessor();
            Assert.AreEqual<int>(1, target.GetLength(CarrierPlateRoutingType.BackThroughAwps));
        }

        #endregion

        #region GetStream method tests

        [TestMethod()]
        public void GetStreamTest()
        {
            EmptyCarrierPlateRoutingToStreamConverter_Accessor target = new EmptyCarrierPlateRoutingToStreamConverter_Accessor();

            Assert.AreEqual<string>("0001", target.GetStream(CarrierPlateRoutingType.BackThroughAwps));
            Assert.AreEqual<string>("0002", target.GetStream(CarrierPlateRoutingType.InspectionRequired));
        }

        #endregion
    }
}