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
        public void TestMethod2()
        {
            var testingResult = @"C:\Users\k.skrabal\AppData\Local\Temp\Planit\pamscl.dat";
            PamsclFilePathProvider provider = new PamsclFilePathProvider(@"\Temp\Planit");

            Assert.AreEqual(testingResult, provider.GetPamsclFilePath());
        }

        [TestMethod]
        public void TestMethod3()
        {
            //mc3000_sin_13r1_dt_v1-1-1.mcp,,,D:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\Data\T1_1.nc,D:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\Data\v1-8702_2.ppf
            IMC3000FileNamesManipulator strManipulator = new MC3000FileNameManipulator();

            var result = strManipulator.GetNCFilePattern(@"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_5.nc");
           
            Assert.AreEqual("T1_", result);
        }

        [TestMethod]
        public void MC3000FileNameManipulator_GetNCFiles()
        {
            IMC3000FileNamesManipulator strManipulator = new MC3000FileNameManipulator();

            var result = strManipulator.GetNCFiles(@"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_5.nc","T1_");

            List<string> testList = new List<string> {
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_2.nc",
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_3.nc",
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_4.nc",
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_5.nc",
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_1.nc"
                };

            List<string> resultExpected = result.Intersect(testList).ToList(); 

            Assert.AreEqual(resultExpected.Count, result.Count);
        }

        [TestMethod]
        public void MC3000FileNameManipulator_GetToolSheetsPattern()
        {
            IMC3000FileNamesManipulator strManipulator = new MC3000FileNameManipulator();

            var result = strManipulator.GetToolSheetPattern(@"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_5.nc");
           
            Assert.AreEqual("-TOOL.nc", result);

        }

        [TestMethod]
        public void MC3000FileNameManipulator_GetToolSheets()
        {
            IMC3000FileNamesManipulator strManipulator = new MC3000FileNameManipulator();

            var result = strManipulator.GetToolSheets(@"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_5.nc", "-TOOL.nc");

            List<string> testList = new List<string> {
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_2-TOOL.nc",
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_3-TOOL.nc",
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_4-TOOL.nc",
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_5-TOOL.nc",
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_1-TOOL.nc"
                };

            List<string> resultExpected = result.Intersect(testList).ToList();

            Assert.AreEqual(resultExpected.Count, result.Count);
        }
    }
}
