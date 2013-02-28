using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Reflection;
using System;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PlcMemoryCommandTest
    {
        #region constructor tests

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorThrowsArgumentNullExceptionTest()
        {
            Mock<PlcMemoryCommand> target = new Mock<PlcMemoryCommand>(null, PlcControlChar.ENQ, 0);
            try
            {
                var obj = target.Object;
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CtorThrowsArgumentOutOfRangeExceptionTest()
        {
            Mock<PlcMemoryCommand> target = new Mock<PlcMemoryCommand>(PlcHelper.GetAddressSpace(10, 100), PlcControlChar.ENQ, 0);
            try
            {
                var obj = target.Object;
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod()]
        public void CtorTest()
        {
            IPlcAddressSpace addressSpace = PlcHelper.GetAddressSpace(10, 100);

            Mock<PlcMemoryCommand> privateTarget1 = new Mock<PlcMemoryCommand>(addressSpace, PlcControlChar.ENQ, 55);
            PlcMemoryCommand_Accessor target1 = new PlcMemoryCommand_Accessor(new PrivateObject(privateTarget1.Object,
                                                                              new PrivateType(typeof(PlcMemoryCommand))));
            Assert.IsNotNull(target1.AddressSpace);
            Assert.AreEqual<int>(55, target1.Address);

            Mock<PlcMemoryCommand> privateTarget2 = new Mock<PlcMemoryCommand>(addressSpace, PlcControlChar.ENQ, 100);
            PlcMemoryCommand_Accessor target2 = new PlcMemoryCommand_Accessor(new PrivateObject(privateTarget2.Object,
                                                                              new PrivateType(typeof(PlcMemoryCommand))));
            Assert.IsNotNull(target2.AddressSpace);
            Assert.AreEqual<int>(100, target2.Address);
        }

        #endregion

        #region AddressToString method tests

        [TestMethod()]
        public void AddressToStringTest()
        {
            Mock<PlcMemoryCommand> privateTarget1 = new Mock<PlcMemoryCommand>(PlcHelper.GetAddressSpace(), PlcControlChar.ENQ, 0xb38ac);
            PlcMemoryCommand_Accessor target1 = new PlcMemoryCommand_Accessor(new PrivateObject(privateTarget1.Object,
                                                                              new PrivateType(typeof(PlcMemoryCommand))));

            Assert.AreEqual<string>("B38AC", target1.AddressToString());

            Mock<PlcMemoryCommand> privateTarget2 = new Mock<PlcMemoryCommand>(PlcHelper.GetAddressSpace(), PlcControlChar.ENQ, 0x5b92f);
            PlcMemoryCommand_Accessor target2 = new PlcMemoryCommand_Accessor(new PrivateObject(privateTarget2.Object,
                                                                              new PrivateType(typeof(PlcMemoryCommand))));

            Assert.AreEqual<string>("5B92F", target2.AddressToString());
        }

        #endregion
    }
}
