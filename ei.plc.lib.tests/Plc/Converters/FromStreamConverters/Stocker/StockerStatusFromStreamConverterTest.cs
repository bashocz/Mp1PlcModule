using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class StockerStatusFromStreamConverterTest
    {
        #region ParseWaferSize method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ParseWaferSizeThrowsPlcException1Test()
        {
            StockerStatusFromStreamConverter privateTarget = new StockerStatusFromStreamConverter();
            StockerStatusFromStreamConverter_Accessor target = new StockerStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerStatusFromStreamConverter))));
            target.ParseWaferSize("0005");
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ParseWaferSizeThrowsPlcException2Test()
        {
            StockerStatusFromStreamConverter privateTarget = new StockerStatusFromStreamConverter();
            StockerStatusFromStreamConverter_Accessor target = new StockerStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerStatusFromStreamConverter))));
            target.ParseWaferSize("0007");
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ParseWaferSizeThrowsPlcException3Test()
        {
            StockerStatusFromStreamConverter privateTarget = new StockerStatusFromStreamConverter();
            StockerStatusFromStreamConverter_Accessor target = new StockerStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerStatusFromStreamConverter))));
            target.ParseWaferSize("0009");
        }

        [TestMethod()]
        public void ParseWaferSize1Test()
        {
            StockerStatusFromStreamConverter privateTarget = new StockerStatusFromStreamConverter();
            StockerStatusFromStreamConverter_Accessor target = new StockerStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerStatusFromStreamConverter))));
            Assert.AreEqual<WaferSize>(WaferSize.AnySize, target.ParseWaferSize("0000"));
        }

        [TestMethod()]
        public void ParseWaferSize2Test()
        {
            StockerStatusFromStreamConverter privateTarget = new StockerStatusFromStreamConverter();
            StockerStatusFromStreamConverter_Accessor target = new StockerStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerStatusFromStreamConverter))));
            Assert.AreEqual<WaferSize>(WaferSize.Size6Inches, target.ParseWaferSize("0006"));
        }

        [TestMethod()]
        public void ParseWaferSize3Test()
        {
            StockerStatusFromStreamConverter privateTarget = new StockerStatusFromStreamConverter();
            StockerStatusFromStreamConverter_Accessor target = new StockerStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerStatusFromStreamConverter))));
            Assert.AreEqual<WaferSize>(WaferSize.Size8Inches, target.ParseWaferSize("0008"));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterShortStreamThrowsPclExceptionTest()
        {
            string stream = "".PadLeft(87, '0');

            StockerStatusFromStreamConverter privateTarget = new StockerStatusFromStreamConverter();
            StockerStatusFromStreamConverter_Accessor target = new StockerStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerStatusFromStreamConverter))));
            target.CheckParameter(stream);
        }

        #endregion

        #region GetObject method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectCarrierPlateArrivedThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetStockerStatusStream(0).Substring(4, 88);
            stream = "0002" + stream.Substring(4);

            StockerStatusFromStreamConverter privateTarget = new StockerStatusFromStreamConverter();
            StockerStatusFromStreamConverter_Accessor target = new StockerStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectIsMagazineFullThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetStockerStatusStream(0).Substring(4, 88);
            stream = stream.Substring(0, 12) + "0002" + stream.Substring(16);

            StockerStatusFromStreamConverter privateTarget = new StockerStatusFromStreamConverter();
            StockerStatusFromStreamConverter_Accessor target = new StockerStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectIsOperatorChangeRequestThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetStockerStatusStream(0).Substring(4, 88);
            stream = stream.Substring(0, 16) + "0002" + stream.Substring(20);

            StockerStatusFromStreamConverter privateTarget = new StockerStatusFromStreamConverter();
            StockerStatusFromStreamConverter_Accessor target = new StockerStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectIsMagazineChangeStartedThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetStockerStatusStream(0).Substring(4, 88);
            stream = stream.Substring(0, 24) + "0002" + stream.Substring(28);

            StockerStatusFromStreamConverter privateTarget = new StockerStatusFromStreamConverter();
            StockerStatusFromStreamConverter_Accessor target = new StockerStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectIsMagazineRequestedThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetStockerStatusStream(0).Substring(4, 88);
            stream = stream.Substring(0, 68) + "0002" + stream.Substring(72);

            StockerStatusFromStreamConverter privateTarget = new StockerStatusFromStreamConverter();
            StockerStatusFromStreamConverter_Accessor target = new StockerStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectRequestedWaferSizeThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetStockerStatusStream(0).Substring(4, 88);
            stream = stream.Substring(0, 64) + "00070001" + stream.Substring(72);

            StockerStatusFromStreamConverter privateTarget = new StockerStatusFromStreamConverter();
            StockerStatusFromStreamConverter_Accessor target = new StockerStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectIsMagazineArrivedThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetStockerStatusStream(0).Substring(4, 88);
            stream = stream.Substring(0, 76) + "0002" + stream.Substring(80);

            StockerStatusFromStreamConverter privateTarget = new StockerStatusFromStreamConverter();
            StockerStatusFromStreamConverter_Accessor target = new StockerStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(StockerStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        #endregion

        #region TryConvert method tests

        [TestMethod()]
        public void TryConvert1Test()
        {
            StockerStatusFromStreamConverter target = new StockerStatusFromStreamConverter();
            StockerStatus result = target.TryConvert(PlcHelper.GetStockerStatusStream(0).Substring(4, 88));

            Assert.AreEqual<bool>(true, result.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(false, result.MagazineChangeRequest.IsMagazineFull);
            Assert.AreEqual<bool>(false, result.MagazineChangeRequest.IsOperatorChangeRequest);
            Assert.AreEqual<bool>(true, result.IsMagazineChangeStarted);
            Assert.AreEqual<WaferSize>(WaferSize.Size6Inches, result.MagazineRequest.WaferSize);
            Assert.AreEqual<bool>(true, result.MagazineRequest.IsRequested);
            Assert.AreEqual<int>(1, result.MagazineRequest.PolishLineNumber);
            Assert.AreEqual<bool>(true, result.IsMagazineArrived);
            Assert.AreEqual<MagazineSelection>(MagazineSelection.Cleared, result.MagazineSelection);
        }

        [TestMethod()]
        public void TryConvert2Test()
        {
            StockerStatusFromStreamConverter target = new StockerStatusFromStreamConverter();
            StockerStatus result = target.TryConvert(PlcHelper.GetStockerStatusStream(1).Substring(4, 88));

            Assert.AreEqual<bool>(false, result.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(true, result.MagazineChangeRequest.IsMagazineFull);
            Assert.AreEqual<bool>(false, result.MagazineChangeRequest.IsOperatorChangeRequest);
            Assert.AreEqual<bool>(false, result.IsMagazineChangeStarted);
            Assert.AreEqual<WaferSize>(WaferSize.Size8Inches, result.MagazineRequest.WaferSize);
            Assert.AreEqual<bool>(true, result.MagazineRequest.IsRequested);
            Assert.AreEqual<int>(2, result.MagazineRequest.PolishLineNumber);
            Assert.AreEqual<bool>(false, result.IsMagazineArrived);
            Assert.AreEqual<MagazineSelection>(MagazineSelection.HasRequestedSize, result.MagazineSelection);
        }

        [TestMethod()]
        public void TryConvert3Test()
        {
            StockerStatusFromStreamConverter target = new StockerStatusFromStreamConverter();
            StockerStatus result = target.TryConvert(PlcHelper.GetStockerStatusStream(2).Substring(4, 88));

            Assert.AreEqual<bool>(true, result.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(false, result.MagazineChangeRequest.IsMagazineFull);
            Assert.AreEqual<bool>(true, result.MagazineChangeRequest.IsOperatorChangeRequest);
            Assert.AreEqual<bool>(true, result.IsMagazineChangeStarted);
            Assert.AreEqual<WaferSize>(WaferSize.Size6Inches, result.MagazineRequest.WaferSize);
            Assert.AreEqual<bool>(false, result.MagazineRequest.IsRequested);
            Assert.AreEqual<int>(3, result.MagazineRequest.PolishLineNumber);
            Assert.AreEqual<bool>(false, result.IsMagazineArrived);
            Assert.AreEqual<MagazineSelection>(MagazineSelection.DoesNotHaveRequestedSize, result.MagazineSelection);
        }

        #endregion
    }
}