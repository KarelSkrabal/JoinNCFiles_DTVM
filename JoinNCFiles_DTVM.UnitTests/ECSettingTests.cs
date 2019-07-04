using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JoinNCFiles_DTVM;


namespace JoinNCFiles_DTVM.UnitTests
{
    [TestClass]
    public class JoinNCFilesTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ec = ECsettings.Instance;
            Assert.AreEqual(string.Empty, string.Empty);
        }
    }
}
