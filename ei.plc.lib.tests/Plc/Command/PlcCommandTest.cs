using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PlcCommandTest
    {
        #region constructor tests

        [TestMethod()]
        public void CtorTest()
        {
            Mock<PlcCommand> target1 = new Mock<PlcCommand>(PlcControlChar.ENQ);
            Assert.AreEqual<PlcControlChar>(PlcControlChar.ENQ, target1.Object.BeginChar);

            Mock<PlcCommand> target2 = new Mock<PlcCommand>(PlcControlChar.ACK);
            Assert.AreEqual<PlcControlChar>(PlcControlChar.ACK, target2.Object.BeginChar);
        }

        #endregion

        #region BeginCharToString method tests

        [TestMethod()]
        public void BeginCharToStringTest()
        {
            Mock<PlcCommand> privateTarget1 = new Mock<PlcCommand>(PlcControlChar.ENQ);
            PlcCommand_Accessor target1 = new PlcCommand_Accessor(new PrivateObject(privateTarget1.Object,
                                                                  new PrivateType(typeof(PlcCommand))));
            Assert.AreEqual<string>("\u0005", target1.BeginCharToString());

            Mock<PlcCommand> privateTarget2 = new Mock<PlcCommand>(PlcControlChar.ACK);
            PlcCommand_Accessor target2 = new PlcCommand_Accessor(new PrivateObject(privateTarget2.Object,
                                                                  new PrivateType(typeof(PlcCommand))));
            Assert.AreEqual<string>("\u0006", target2.BeginCharToString());
        }

        #endregion

        #region IdsToString method tests

        [TestMethod()]
        public void IdsToStringTest()
        {
            Mock<PlcCommand> privateTarget = new Mock<PlcCommand>(PlcControlChar.ENQ);
            PlcCommand_Accessor target = new PlcCommand_Accessor(new PrivateObject(privateTarget.Object,
                                                                 new PrivateType(typeof(PlcCommand))));

            Assert.AreEqual<string>("00FF", target.IdsToString());
        }

        #endregion

        #region MessageWaitTimeToString method tests

        [TestMethod()]
        public void MessageWaitTimeStringTest()
        {
            Mock<PlcCommand> privateTarget = new Mock<PlcCommand>(PlcControlChar.ENQ);
            PlcCommand_Accessor target = new PlcCommand_Accessor(new PrivateObject(privateTarget.Object,
                                                                 new PrivateType(typeof(PlcCommand))));

            Assert.AreEqual<string>("0", target.MessageWaitTimeToString());
        }

        #endregion
    }
}
