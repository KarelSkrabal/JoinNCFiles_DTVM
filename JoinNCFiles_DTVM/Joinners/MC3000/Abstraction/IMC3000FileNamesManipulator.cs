using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoinNCFiles_DTVM.Joinners.MC3000.Abstraction
{
    public interface IMC3000FileNamesManipulator
    {
        string GetNCFilePattern(string str);
        string GetToolSheetPattern(string str);
        List<string> GetToolSheets(string str, string toolSheetPattern);
        List<string> GetNCFiles(string str, string ncFilePattern);
    }
}
