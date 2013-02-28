using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EI.Business;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class WafersToStreamConverterTest
    {
        #region WafersToStream method tests

        [TestMethod()]
        public void WafersToStreamTest()
        {
            IList<IWafer> wafers = PlcHelper.GetWaferList(30, 2);
            WafersToStreamConverter_Accessor target = new WafersToStreamConverter_Accessor();

            IntConverter converter = new IntConverter();

            string excepted = string.Empty;
            foreach (IWafer wafer in wafers)
            {
                excepted += converter.ToStream(wafer.CassetteNumber) + converter.ToStream(wafer.SlotNumber);
            }
            Assert.AreEqual<string>(excepted, target.WafersToStream(wafers));
        }

        #endregion

        #region CheckParameter method tests

        [TestMethod()]
        public void CheckParameterTest()
        {
            WafersToStreamConverter_Accessor target = new WafersToStreamConverter_Accessor();
            Assert.AreEqual<bool>(true, target.CheckParameter(PlcHelper.GetWaferList(5, 1)));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException1Test()
        {
            WafersToStreamConverter_Accessor target = new WafersToStreamConverter_Accessor();
            target.CheckParameter(PlcHelper.GetWaferList(33, 2));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException2Test()
        {
            WafersToStreamConverter_Accessor target = new WafersToStreamConverter_Accessor();
            target.CheckParameter(PlcHelper.GetWaferList(0, 1));
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException3Test()
        {
            Mock<IWafer> wafer1 = new Mock<IWafer>();
            wafer1.Setup(x => x.CassetteNumber).Returns(5);
            wafer1.Setup(x => x.SlotNumber).Returns(20);

            // invalid cassete number - lower limit
            Mock<IWafer> wafer2 = new Mock<IWafer>();
            wafer2.Setup(x => x.CassetteNumber).Returns(0);
            wafer2.Setup(x => x.SlotNumber).Returns(21);

            List<IWafer> wafers = new List<IWafer>();
            wafers.Add(wafer1.Object);
            wafers.Add(wafer2.Object);

            WafersToStreamConverter_Accessor target = new WafersToStreamConverter_Accessor();
            target.CheckParameter(wafers);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException4Test()
        {
            Mock<IWafer> wafer1 = new Mock<IWafer>();
            wafer1.Setup(x => x.CassetteNumber).Returns(11);
            wafer1.Setup(x => x.SlotNumber).Returns(3);

            // invalid cassete number - upper limit
            Mock<IWafer> wafer2 = new Mock<IWafer>();
            wafer2.Setup(x => x.CassetteNumber).Returns(13);
            wafer2.Setup(x => x.SlotNumber).Returns(4);

            List<IWafer> wafers = new List<IWafer>();
            wafers.Add(wafer1.Object);
            wafers.Add(wafer2.Object);

            WafersToStreamConverter_Accessor target = new WafersToStreamConverter_Accessor();
            target.CheckParameter(wafers);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException5Test()
        {
            Mock<IWafer> wafer1 = new Mock<IWafer>();
            wafer1.Setup(x => x.CassetteNumber).Returns(11);
            wafer1.Setup(x => x.SlotNumber).Returns(3);

            // invalid slot number - lower limit
            Mock<IWafer> wafer2 = new Mock<IWafer>();
            wafer2.Setup(x => x.CassetteNumber).Returns(3);
            wafer2.Setup(x => x.SlotNumber).Returns(0);

            List<IWafer> wafers = new List<IWafer>();
            wafers.Add(wafer1.Object);
            wafers.Add(wafer2.Object);

            WafersToStreamConverter_Accessor target = new WafersToStreamConverter_Accessor();
            target.CheckParameter(wafers);
        }

        [TestMethod()]
        [ExpectedException(typeof(PlcException))]
        public void CheckParameterThrowPlcException6Test()
        {
            Mock<IWafer> wafer1 = new Mock<IWafer>();
            wafer1.Setup(x => x.CassetteNumber).Returns(11);
            wafer1.Setup(x => x.SlotNumber).Returns(3);

            // invalid slot number - upper limit
            Mock<IWafer> wafer2 = new Mock<IWafer>();
            wafer2.Setup(x => x.CassetteNumber).Returns(3);
            wafer2.Setup(x => x.SlotNumber).Returns(26);

            List<IWafer> wafers = new List<IWafer>();
            wafers.Add(wafer1.Object);
            wafers.Add(wafer2.Object);

            WafersToStreamConverter_Accessor target = new WafersToStreamConverter_Accessor();
            target.CheckParameter(wafers);
        }

        #endregion

        #region GetLength method tests

        [TestMethod()]
        public void GetLengthTest()
        {
            WafersToStreamConverter_Accessor target = new WafersToStreamConverter_Accessor();

            Assert.AreEqual<int>(10, target.GetLength(PlcHelper.GetWaferList(5, 1)));
            Assert.AreEqual<int>(30, target.GetLength(PlcHelper.GetWaferList(15, 1)));
        }

        #endregion

        #region GetStream method tests

        [TestMethod()]
        public void GetStream1Test()
        {
            IList<IWafer> wafers = PlcHelper.GetWaferList(15, 1);
            WafersToStreamConverter_Accessor target = new WafersToStreamConverter_Accessor();

            IntConverter converter = new IntConverter();

            string excepted = string.Empty;
            foreach (IWafer wafer in wafers)
            {
                excepted += converter.ToStream(wafer.CassetteNumber) + converter.ToStream(wafer.SlotNumber);
            }
            Assert.AreEqual<string>(excepted, target.GetStream(wafers));
        }

        [TestMethod()]
        public void GetStream2Test()
        {
            IList<IWafer> wafers = PlcHelper.GetWaferList(30, 2);
            WafersToStreamConverter_Accessor target = new WafersToStreamConverter_Accessor();

            IntConverter converter = new IntConverter();

            string excepted = string.Empty;
            foreach (IWafer wafer in wafers)
            {
                excepted += converter.ToStream(wafer.CassetteNumber) + converter.ToStream(wafer.SlotNumber);
            }
            Assert.AreEqual<string>(excepted, target.GetStream(wafers));
        }

        #endregion
    }
}