using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using JoinNCFiles_DTVM.Joinners.MC3000.Abstraction;

namespace JoinNCFiles_DTVM.Joinners.MC3000
{
    public class MC3000FileNameManipulator : IMC3000FileNamesManipulator
    {
        public string GetNCFilePattern(string str)
        {
            return str.FileByPattern();
        }

        public List<string> GetNCFiles(string str, string ncFilePattern)
        {

            List<string> NCfiles = new List<string>();
            foreach (string file in str.FilesByPattern())
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                if(Regex.Match(fileName, @"_{1}(\d+)$").Success)
                    NCfiles.Add(file);
            }
            return NCfiles;
        }

        public string GetToolSheetPattern(string str)
        {
            string toolSheetPattern = string.Empty;
            int lastUnderscore = -1;
            foreach (string file in str.FilesByPattern())
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                if (lastUnderscore < file.LastIndexOf('-'))
                {
                    lastUnderscore = file.LastIndexOf('-');
                    toolSheetPattern = file.Substring(lastUnderscore);
                    break;
                }
            }
            return toolSheetPattern;
        }

        public List<string> GetToolSheets(string str, string toolSheetPattern)
        {
            List<string> ToolSheets = new List<string>();
            foreach (string item in str.FilesByPattern())
            {
                if (item.EndsWith(toolSheetPattern))
                    ToolSheets.Add(item);
            }
            return ToolSheets;
        }
    }
}
