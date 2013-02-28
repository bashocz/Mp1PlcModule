using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EI.Plc.Tests
{   
    [TestClass()]
    public class PlcMemoryAcknowledgeTest
    {
        #region ToCommandString method tests

        [TestMethod()]
        public void ToCommandStringTest()
        {
            PlcMemoryAcknowledge target = new PlcMemoryAcknowledge();
            Assert.AreEqual<string>("\u000600FF", target.CommandToString());
        }

        #endregion
    }
}
