using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JoinNCFiles_DTVM;
using System.IO;
using System.Reflection;
using JoinNCFiles_DTVM.Core;
using JoinNCFiles_DTVM.Joinners.MC3000.Abstraction;
using JoinNCFiles_DTVM.Joinners.MC3000;
using System.Collections.Generic;
using System.Linq;

namespace JoinNCFiles_DTVM.UnitTests
{
    [TestClass]
    public class JoinNCFilesTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();

            var ahoj = thisAssembly.GetManifestResourceNames();

            //var ec = ECsettings.Instance;
            Assert.AreEqual(string.Empty, string.Empty);
        }

        [TestMethod]
        public void PamsclFilePathProvider_GetPamsclFilePath()
        {
            var testingResult = @"C:\Users\k.skrabal\AppData\Local\Temp\Planit\pamscl.dat";
            PamsclFilePathProvider provider = new PamsclFilePathProvider(@"\Temp\Planit");

            Assert.AreEqual(testingResult, provider.GetPamsclFilePath());
        }

    }
}
