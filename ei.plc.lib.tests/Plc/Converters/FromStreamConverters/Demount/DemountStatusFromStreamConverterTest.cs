using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class DemountStatusFromStreamConverterTest
    {
        #region ParseAreCassettes method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ParseAreCassettesThrowsPlcException1Test()
        {
            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            target.ParseAreCassettes("0X00010001000100");
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ParseAreCassettesThrowsPlcException2Test()
        {
            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            target.ParseAreCassettes("0100010001000X00");
        }

        [TestMethod()]
        public void ParseAreCassettes1Test()
        {
            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            Assert.AreEqual<int>(4, target.ParseAreCassettes("0001000100010001").Count);
        }

        [TestMethod()]
        public void ParseAreCassettes2Test()
        {
            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            Assert.AreEqual<int>(4, target.ParseAreCassettes("0001000000000001").Count);
        }

        #endregion

        #region ParseDemountState method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ParseDemountStateThrowsPlcException1Test()
        {
            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            target.ParseDemountState("0000");
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ParseDemountStateThrowsPlcException2Test()
        {
            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            target.ParseDemountState("0005");
        }

        [TestMethod()]
        public void ParseDemountState1Test()
        {
            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            Assert.AreEqual<DemountState>(DemountState.AutoDemount, target.ParseDemountState("0001"));
        }

        [TestMethod()]
        public void ParseDemountState2Test()
        {
            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            Assert.AreEqual<DemountState>(DemountState.Stop, target.ParseDemountState("0003"));
        }

        #endregion

        #region ParseCassetteHopper method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ParseCassetteHopperThrowsPlcExceptionTest()
        {
            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            target.ParseCassetteHopper("0005");
        }

        [TestMethod()]
        public void ParseCassetteHopper1Test()
        {
            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            Assert.AreEqual<DemountCassetteHopper>(DemountCassetteHopper.Hopper1, target.ParseCassetteHopper("0001"));
        }

        [TestMethod()]
        public void ParseCassetteHopper2Test()
        {
            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            Assert.AreEqual<DemountCassetteHopper>(DemountCassetteHopper.Hopper3, target.ParseCassetteHopper("0003"));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterShortStreamThrowsPclExceptionTest()
        {
            string stream = "".PadLeft(151, '0');

            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            target.CheckParameter(stream);
        }

        #endregion

        #region GetObject method tests

        [TestMethod()]
        public void GetObjectIsCarrierPlateArrivedThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetDemountStatusStream(0).Substring(4, 152);
            stream = "0003" + stream.Substring(4);

            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            target.CheckParameter(stream);
        }

        [TestMethod()]
        public void GetObjectIsCarrierPlateDemountStartedThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetDemountStatusStream(0).Substring(4, 152);
            stream = stream.Substring(0, 12) + "0003" + stream.Substring(16);

            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            target.CheckParameter(stream);
        }

        [TestMethod()]
        public void GetObjectDemountedWaferCountThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetDemountStatusStream(0).Substring(4, 152);
            stream = stream.Substring(0, 28) + "XXXX" + stream.Substring(32);

            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            target.CheckParameter(stream);
        }

        [TestMethod()]
        public void GetObjectCompletedThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetDemountStatusStream(0).Substring(4, 152);
            stream = stream.Substring(0, 32) + "0003" + stream.Substring(36);

            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            target.CheckParameter(stream);
        }

        [TestMethod()]
        public void GetObjectCassetteHopperThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetDemountStatusStream(0).Substring(4, 152);
            stream = stream.Substring(0, 76) + "0005" + stream.Substring(80);

            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            target.CheckParameter(stream);
        }

        [TestMethod()]
        public void GetObjectAreCassettesThrowsPlcException1Test()
        {
            string stream = PlcHelper.GetDemountStatusStream(0).Substring(4, 152);
            stream = stream.Substring(0, 132) + "0003000100010001" + stream.Substring(148);

            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            target.CheckParameter(stream);
        }

        [TestMethod()]
        public void GetObjectAreCassettesThrowsPlcException2Test()
        {
            string stream = PlcHelper.GetDemountStatusStream(0).Substring(4, 152);
            stream = stream.Substring(0, 132) + "0001000100010003" + stream.Substring(148);

            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            target.CheckParameter(stream);
        }

        [TestMethod()]
        public void GetObjectDemountStateThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetDemountStatusStream(0).Substring(4, 152);
            stream = stream.Substring(0, 148) + "0005";

            DemountStatusFromStreamConverter privateTarget = new DemountStatusFromStreamConverter();
            DemountStatusFromStreamConverter_Accessor target = new DemountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(DemountStatusFromStreamConverter))));
            target.CheckParameter(stream);
        }

        #endregion

        #region TryConvert method tests

        [TestMethod()]
        public void TryConvert1Test()
        {
            DemountStatusFromStreamConverter target = new DemountStatusFromStreamConverter();
            DemountStatus result = target.TryConvert(PlcHelper.GetDemountStatusStream(0).Substring(4, 152));

            Assert.AreEqual<bool>(false, result.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(false, result.IsCarrierPlateDemountStarted);
            Assert.AreEqual<int>(2, result.DemountInfo.DemountedWaferCount);
            Assert.AreEqual<bool>(false, result.DemountInfo.Completed);
            Assert.AreEqual<DemountCassetteHopper>(DemountCassetteHopper.Hopper4, result.CanReadCassetteBarcode);
            Assert.AreEqual<bool>(true, result.AreCassettes[0]);
            Assert.AreEqual<bool>(false, result.AreCassettes[1]);
            Assert.AreEqual<bool>(true, result.AreCassettes[2]);
            Assert.AreEqual<bool>(false, result.AreCassettes[3]);
            Assert.AreEqual<DemountState>(DemountState.AutoDemount, result.State);
        }

        [TestMethod()]
        public void TryConvert2Test()
        {
            DemountStatusFromStreamConverter target = new DemountStatusFromStreamConverter();
            DemountStatus result = target.TryConvert(PlcHelper.GetDemountStatusStream(1).Substring(4, 152));

            Assert.AreEqual<bool>(true, result.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(true, result.IsCarrierPlateDemountStarted);
            Assert.AreEqual<int>(3, result.DemountInfo.DemountedWaferCount);
            Assert.AreEqual<bool>(true, result.DemountInfo.Completed);
            Assert.AreEqual<DemountCassetteHopper>(DemountCassetteHopper.Hopper3, result.CanReadCassetteBarcode);
            Assert.AreEqual<bool>(false, result.AreCassettes[0]);
            Assert.AreEqual<bool>(true, result.AreCassettes[1]);
            Assert.AreEqual<bool>(false, result.AreCassettes[2]);
            Assert.AreEqual<bool>(false, result.AreCassettes[3]);
            Assert.AreEqual<DemountState>(DemountState.Standby, result.State);
        }

        [TestMethod()]
        public void TryConvert3Test()
        {
            DemountStatusFromStreamConverter target = new DemountStatusFromStreamConverter();
            DemountStatus result = target.TryConvert(PlcHelper.GetDemountStatusStream(2).Substring(4, 152));

            Assert.AreEqual<bool>(false, result.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(false, result.IsCarrierPlateDemountStarted);
            Assert.AreEqual<int>(1, result.DemountInfo.DemountedWaferCount);
            Assert.AreEqual<bool>(true, result.DemountInfo.Completed);
            Assert.AreEqual<DemountCassetteHopper>(DemountCassetteHopper.Hopper2, result.CanReadCassetteBarcode);
            Assert.AreEqual<bool>(true, result.AreCassettes[0]);
            Assert.AreEqual<bool>(false, result.AreCassettes[1]);
            Assert.AreEqual<bool>(true, result.AreCassettes[2]);
            Assert.AreEqual<bool>(true, result.AreCassettes[3]);
            Assert.AreEqual<DemountState>(DemountState.Stop, result.State);
        }

        #endregion
    }
}