﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoinNCFiles_DTVM.Joinners.MC3000.Abstraction
{
    public interface IMC3000FileNamesManipulator
    {
        string ToolSheetResult { get; }
        string NCfileResult { get; }
        List<string> ncFileResults { get; }
        List<string> toolSheetResults { get; }
        void ManipulateFileNames(string lastGeneratedNCfile);
        string GetNCFilePattern(string str);
        string GetToolSheetPattern(string str);
        List<string> GetToolSheets(string str, string toolSheetPattern);
        List<string> GetNCFiles(string str, string ncFilePattern);
        string GetNCfileResult(string ncFilePattern, List<string> NCfiles);
        string GetToolSheetResult(string ncFileResult, string toolSheetPattern);
    }
}
