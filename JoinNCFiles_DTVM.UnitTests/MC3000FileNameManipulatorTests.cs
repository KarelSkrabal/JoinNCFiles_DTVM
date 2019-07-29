using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoinNCFiles_DTVM.Core;
using JoinNCFiles_DTVM.Joinners.MC3000.Abstraction;
using JoinNCFiles_DTVM.Joinners.MC3000;


namespace JoinNCFiles_DTVM.UnitTests
{
    [TestClass]
    public class MC3000FileNameManipulatorTests
    {
        [TestMethod]
        public void MC3000FileNameManipulator_GetNCFilePattern()
        {           
            IMC3000FileNamesManipulator strManipulator = new MC3000FileNameManipulator();

            var result = strManipulator.GetNCFilePattern(@"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_5.nc");

            Assert.AreEqual("T1_", result);
        }

        [TestMethod]
        public void MC3000FileNameManipulator_GetNCFiles()
        {
            IMC3000FileNamesManipulator strManipulator = new MC3000FileNameManipulator();

            var result = strManipulator.GetNCFiles(@"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_5.nc", "T1_");

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

        [TestMethod]
        public void MC3000FileNameManipulator_GetNCfileResultName()
        {
            var result = @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1.nc";
            List<string> testList = new List<string> {
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_2.nc",
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_3.nc",
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_4.nc",
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_5.nc",
                @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1_1.nc"
                };
            IMC3000FileNamesManipulator strManipulator = new MC3000FileNameManipulator();

            Assert.AreEqual(result, strManipulator.GetNCfileResult("T1_", testList));
        }

        [TestMethod]
        public void MC3000FileNameManipulator_GetToolSheetResultName()
        {
            var result = @"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1-TOOL.nc";
            IMC3000FileNamesManipulator strManipulator = new MC3000FileNameManipulator();

            Assert.AreEqual(result, strManipulator.GetToolSheetResult(@"d:\Nexnet\Visual studio projects\JoinNCFiles_DTVM\JoinNCFiles_DTVM\TestingData\T1.nc", "-TOOL.nc"));
        }
    }
}
