using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PolisherStatusFromStreamConverterTest
    {
        #region CheckParameter method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterShortStreamThrowsPlcExceptionTest()
        {
            string stream = "".PadLeft(219, '0');

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.CheckParameter(stream);
        }

        #endregion

        #region GetObject method tests

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectMagazineIdThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = "XXXXXXXXXXXXXXXX" + stream.Substring(16);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectCarrierPlateIdThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 16) + "XXXXXXXXXXXXXXXX" + stream.Substring(32);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectHighPressureDurationThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 84) + "XXXX" + stream.Substring(88);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectPlateRpmThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 172) + "XXXX" + stream.Substring(176);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectPlateLoadCurrentThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 192) + "XXXX" + stream.Substring(196);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectForceThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 88) + "XXXX" + stream.Substring(92);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectPressureThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 140) + "XXXX" + stream.Substring(144);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectBackpressureThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 156) + "XXXX" + stream.Substring(160);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectRpmThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 176) + "XXXX" + stream.Substring(180);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectLoadCurrentThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 196) + "XXXX" + stream.Substring(200);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectPadTempThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 104) + "XXXX" + stream.Substring(108);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectCoolingWaterInTempThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 108) + "XXXX" + stream.Substring(112);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectCoolingWaterOutTempThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 112) + "XXXX" + stream.Substring(116);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectSlurryInTempThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 116) + "XXXX" + stream.Substring(120);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectSlurryOutTempThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 120) + "XXXX" + stream.Substring(124);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectRinseTempThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 124) + "XXXX" + stream.Substring(128);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectCoolingWaterAmountThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 128) + "XXXX" + stream.Substring(132);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectSlurryAmountThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 132) + "XXXX" + stream.Substring(136);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectRinseAmountThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 136) + "XXXX" + stream.Substring(140);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectPadUsedTimeThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 212) + "XXXX" + stream.Substring(216);

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void GetObjectPadUsedCountThrowsPclExceptionTest()
        {
            string stream = PlcHelper.GetPolishingStream(0).Substring(4, 220);
            stream = stream.Substring(0, 216) + "XXXX";

            PolisherStatusFromStreamConverter privateTarget = new PolisherStatusFromStreamConverter();
            PolisherStatusFromStreamConverter_Accessor target = new PolisherStatusFromStreamConverter_Accessor(new PrivateObject(privateTarget, new PrivateType(typeof(PolisherStatusFromStreamConverter))));
            target.GetObject(stream);
        }

        #endregion

        #region TryConvert method tests

        [TestMethod()]
        public void TryConvertTest()
        {
            PolisherStatusFromStreamConverter target = new PolisherStatusFromStreamConverter();
            PolisherFullStatus result = target.TryConvert(PlcHelper.GetPolishingStream(0).Substring(4, 220));

            // Status
            Assert.AreEqual<PolisherState>(PolisherState.AutoProcess, result.State);

            // Magazine
            // Magazine's ID
            Assert.AreEqual<string>("DHFTPR8Q", result.Magazine.Id);

            // Plates's ID
            Assert.AreEqual<string>("NDFH8PLF", result.Magazine.Plates[0].Id);
            Assert.AreEqual<string>("DKFYQ8EL", result.Magazine.Plates[1].Id);
            Assert.AreEqual<string>("WYEJF8F3", result.Magazine.Plates[2].Id);
            Assert.AreEqual<string>("PRYDGHF4", result.Magazine.Plates[3].Id);

            // HighPressure
            Assert.AreEqual<bool>(false, result.HighPressure);

            // HighPressureDuration
            Assert.AreEqual<int>(0x38, result.HighPressureDuration.Milliseconds);

            // PlateRpm
            Assert.AreEqual<int>(0x58F, result.PlateRpm);

            // PlateLoadCurrent
            Assert.AreEqual<double>(4.5, result.PlateLoadCurrent);

            // PolishingHeads
            // Force
            Assert.AreEqual<int>(0xA, result.PolisherHeads[0].Force);
            Assert.AreEqual<int>(0x14, result.PolisherHeads[1].Force);
            Assert.AreEqual<int>(0x1E, result.PolisherHeads[2].Force);
            Assert.AreEqual<int>(0x28, result.PolisherHeads[3].Force);

            // Pressure
            Assert.AreEqual<double>(12.453, result.PolisherHeads[0].Pressure);
            Assert.AreEqual<double>(7.532, result.PolisherHeads[1].Pressure);
            Assert.AreEqual<double>(5, result.PolisherHeads[2].Pressure);
            Assert.AreEqual<double>(0.254, result.PolisherHeads[3].Pressure);

            // Backpressure
            Assert.AreEqual<double>(5.47, result.PolisherHeads[0].Backpressure);
            Assert.AreEqual<double>(0.13, result.PolisherHeads[1].Backpressure);
            Assert.AreEqual<double>(58.74, result.PolisherHeads[2].Backpressure);
            Assert.AreEqual<double>(3.25, result.PolisherHeads[3].Backpressure);

            // Rpm
            Assert.AreEqual<int>(0x4B0, result.PolisherHeads[0].Rpm);
            Assert.AreEqual<int>(0x5DC, result.PolisherHeads[1].Rpm);
            Assert.AreEqual<int>(0x708, result.PolisherHeads[2].Rpm);
            Assert.AreEqual<int>(0x834, result.PolisherHeads[3].Rpm);

            // LoadCurrent
            Assert.AreEqual<double>(0.8, result.PolisherHeads[0].LoadCurrent);
            Assert.AreEqual<double>(6.8, result.PolisherHeads[1].LoadCurrent);
            Assert.AreEqual<double>(13.8, result.PolisherHeads[2].LoadCurrent);
            Assert.AreEqual<double>(94.8, result.PolisherHeads[3].LoadCurrent);

            // PolishingLiquid
            Assert.AreEqual<double>(13.2, result.PolisherLiquid.PadTemp);
            Assert.AreEqual<double>(9.8, result.PolisherLiquid.CoolingWaterInTemp);
            Assert.AreEqual<double>(14.7, result.PolisherLiquid.CoolingWaterOutTemp);
            Assert.AreEqual<double>(15.9, result.PolisherLiquid.SlurryInTemp);
            Assert.AreEqual<double>(35.7, result.PolisherLiquid.SlurryOutTemp);
            Assert.AreEqual<double>(45.6, result.PolisherLiquid.RinseTemp);
            Assert.AreEqual<double>(32.4, result.PolisherLiquid.CoolingWaterAmount);
            Assert.AreEqual<double>(26.7, result.PolisherLiquid.SlurryAmount);
            Assert.AreEqual<double>(65.2, result.PolisherLiquid.RinseAmount);

            // PadUsedTime
            Assert.AreEqual<int>(0x3DE, result.PadUsedTime.Milliseconds);

            // PadUsedCount
            Assert.AreEqual<int>(0x233, result.PadUsedCount);
        }

        #endregion
    }
}