using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class MountStatusFromStreamConverterTest
    {
        #region ParseMountState method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ParseMountStateThrowsPlcParsingException1Test()
        {
            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.ParseMountState("0000");
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ParseMountStateThrowsPlcParsingException2Test()
        {
            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.ParseMountState("0005");
        }

        [TestMethod()]
        public void ParseMountState1Test()
        {
            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            Assert.AreEqual<MountState>(MountState.AutoMount, target.ParseMountState("0001"));
        }

        [TestMethod()]
        public void ParseMountState2Test()
        {
            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            Assert.AreEqual<MountState>(MountState.Stop, target.ParseMountState("0002"));
        }

        [TestMethod()]
        public void ParseMountState3Test()
        {
            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            Assert.AreEqual<MountState>(MountState.AutoMountAlarm, target.ParseMountState("0003"));
        }

        [TestMethod()]
        public void ParseMountState4Test()
        {
            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            Assert.AreEqual<MountState>(MountState.StopAlarm, target.ParseMountState("0004"));
        }

        #endregion

        #region ParseMountLine method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void ParseMountLineThrowsPlcParsingException2Test()
        {
            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.ParseMountLine("0004");
        }

        [TestMethod()]
        public void ParseMountLine1Test()
        {
            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            Assert.AreEqual<MountLine>(MountLine.Cleared, target.ParseMountLine("0000"));
        }

        [TestMethod()]
        public void ParseMountLine2Test()
        {
            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            Assert.AreEqual<MountLine>(MountLine.Right, target.ParseMountLine("0001"));
        }

        [TestMethod()]
        public void ParseMountLine3Test()
        {
            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            Assert.AreEqual<MountLine>(MountLine.Left, target.ParseMountLine("0002"));
        }

        [TestMethod()]
        public void ParseMountLine4Test()
        {
            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            Assert.AreEqual<MountLine>(MountLine.Both, target.ParseMountLine("0003"));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterShortStreamThrowsPclExceptionTest()
        {
            string stream = "".PadLeft(131, '0');

            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.CheckParameter(stream);
        }

        #endregion

        #region GetObject method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectNewLotCassetteIdThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetMountStatusStream(0).Substring(4, 132);
            stream = "XXXXXXXXXXXXXXXX" + stream.Substring(16);

            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.TryConvert(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectNewLotIsCassetteIdThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetMountStatusStream(0).Substring(4, 132);
            stream = stream.Substring(0, 16) + "0003" + stream.Substring(20);

            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.TryConvert(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectIsLotDataTimeoutThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetMountStatusStream(0).Substring(4, 132);
            stream = stream.Substring(0, 24) + "0003" + stream.Substring(28);

            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.TryConvert(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectNewLotStartLotIdThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetMountStatusStream(0).Substring(4, 132);
            stream = stream.Substring(0, 28) + "XXXXXXXXXXXXXXXXXXXXXXXXXXXX" + stream.Substring(56);

            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.TryConvert(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectNewLotStartedLineThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetMountStatusStream(0).Substring(4, 132);
            stream = stream.Substring(0, 56) + "0004" + stream.Substring(60);

            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.TryConvert(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectNewLotStartedIsLotStartedThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetMountStatusStream(0).Substring(4, 132);
            stream = stream.Substring(0, 60) + "0003" + stream.Substring(64);

            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.TryConvert(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectIsCarrierPlateArrivedThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetMountStatusStream(0).Substring(4, 132);
            stream = stream.Substring(0, 64) + "0003" + stream.Substring(68);

            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.TryConvert(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectIsCarrierPlateMountingReadyThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetMountStatusStream(0).Substring(4, 132);
            stream = stream.Substring(0, 72) + "0003" + stream.Substring(76);

            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.TryConvert(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectWaferBreakNumberThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetMountStatusStream(0).Substring(4, 132);
            stream = stream.Substring(0, 76) + "XXXX" + stream.Substring(80);

            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.TryConvert(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectIsMountingErrorCarrierPlateThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetMountStatusStream(0).Substring(4, 132);
            stream = stream.Substring(0, 84) + "0003" + stream.Substring(88);

            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.TryConvert(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectIsEndLotThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetMountStatusStream(0).Substring(4, 132);
            stream = stream.Substring(0, 88) + "0003" + stream.Substring(92);

            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.TryConvert(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectIsReservationLotCanceledThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetMountStatusStream(0).Substring(4, 132);
            stream = stream.Substring(0, 92) + "0003" + stream.Substring(96);

            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.TryConvert(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectStateThrowsPlcExceptionTest()
        {
            string stream = PlcHelper.GetMountStatusStream(0).Substring(4, 132);
            stream = stream.Substring(0, 128) + "0005";

            MountStatusFromStreamConverter privateTarget = new MountStatusFromStreamConverter();
            MountStatusFromStreamConverter_Accessor target = new MountStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(MountStatusFromStreamConverter))));
            target.TryConvert(stream);
        }

        #endregion

        #region TryConvert method tests

        [TestMethod()]
        public void TryConvert1Test()
        {
            MountStatusFromStreamConverter target = new MountStatusFromStreamConverter();
            MountStatus result = target.TryConvert(PlcHelper.GetMountStatusStream(0).Substring(4, 132));

            Assert.AreEqual<MountState>(MountState.AutoMount, result.State);
            Assert.AreEqual<string>("ABCDEFGH", result.NewLotCassette.CassetteId);
            Assert.AreEqual<bool>(true, result.NewLotCassette.IsCassetteId);
            Assert.AreEqual<bool>(false, result.IsLotDataTimeout);
            Assert.AreEqual<string>("ABCDEFGHIJKLMN", result.NewLotStarted.LotId);
            Assert.AreEqual<MountLine>(MountLine.Both, result.NewLotStarted.Line);
            Assert.AreEqual<bool>(false, result.NewLotStarted.IsLotStarted);
            Assert.AreEqual<bool>(true, result.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(false, result.IsCarrierPlateMountingReady);
            Assert.AreEqual<int>(10, result.WaferBreakNumber);
            Assert.AreEqual<bool>(false, result.IsMountingErrorCarrierPlate);
            Assert.AreEqual<bool>(false, result.IsEndLot);
            Assert.AreEqual<bool>(false, result.IsReservationLotCanceled);
        }

        [TestMethod()]
        public void TryConvert2Test()
        {
            MountStatusFromStreamConverter target = new MountStatusFromStreamConverter();
            MountStatus result = target.TryConvert(PlcHelper.GetMountStatusStream(1).Substring(4, 132));

            Assert.AreEqual<MountState>(MountState.AutoMountAlarm, result.State);
            Assert.AreEqual<string>("IJKLMNOP", result.NewLotCassette.CassetteId);
            Assert.AreEqual<bool>(false, result.NewLotCassette.IsCassetteId);
            Assert.AreEqual<bool>(true, result.IsLotDataTimeout);
            Assert.AreEqual<string>("KCGE8PAQ1HC4HF", result.NewLotStarted.LotId);
            Assert.AreEqual<MountLine>(MountLine.Left, result.NewLotStarted.Line);
            Assert.AreEqual<bool>(true, result.NewLotStarted.IsLotStarted);
            Assert.AreEqual<bool>(false, result.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(true, result.IsCarrierPlateMountingReady);
            Assert.AreEqual<int>(15, result.WaferBreakNumber);
            Assert.AreEqual<bool>(true, result.IsMountingErrorCarrierPlate);
            Assert.AreEqual<bool>(true, result.IsEndLot);
            Assert.AreEqual<bool>(true, result.IsReservationLotCanceled);
        }

        [TestMethod()]
        public void TryConvert3Test()
        {
            MountStatusFromStreamConverter target = new MountStatusFromStreamConverter();
            MountStatus result = target.TryConvert(PlcHelper.GetMountStatusStream(2).Substring(4, 132));

            Assert.AreEqual<MountState>(MountState.StopAlarm, result.State);
            Assert.AreEqual<string>("LDPW8X2D", result.NewLotCassette.CassetteId);
            Assert.AreEqual<bool>(true, result.NewLotCassette.IsCassetteId);
            Assert.AreEqual<bool>(false, result.IsLotDataTimeout);
            Assert.AreEqual<string>("MZQPALJFIR2JCS", result.NewLotStarted.LotId);
            Assert.AreEqual<MountLine>(MountLine.Right, result.NewLotStarted.Line);
            Assert.AreEqual<bool>(false, result.NewLotStarted.IsLotStarted);
            Assert.AreEqual<bool>(true, result.IsCarrierPlateArrived);
            Assert.AreEqual<bool>(true, result.IsCarrierPlateMountingReady);
            Assert.AreEqual<int>(0, result.WaferBreakNumber);
            Assert.AreEqual<bool>(false, result.IsMountingErrorCarrierPlate);
            Assert.AreEqual<bool>(false, result.IsEndLot);
            Assert.AreEqual<bool>(true, result.IsReservationLotCanceled);
        }

        #endregion
    }
}