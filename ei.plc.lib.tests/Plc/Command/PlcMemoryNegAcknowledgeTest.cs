using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{
    [TestClass()]
    public class PlcMemoryNegAcknowledgeTest
    {
        #region ToCommandString method tests

        [TestMethod()]
        public void ToCommandStringTest()
        {
            PlcMemoryNegAcknowledge target = new PlcMemoryNegAcknowledge();
            Assert.AreEqual<string>("\u001500FF", target.CommandToString());
        }

        #endregion
    }
}
