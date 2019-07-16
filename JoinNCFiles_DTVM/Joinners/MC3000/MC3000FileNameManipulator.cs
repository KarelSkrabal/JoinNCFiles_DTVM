using JoinNCFiles_DTVM.Joinners.MC3000.Abstraction;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace JoinNCFiles_DTVM.Joinners.MC3000
{
    public class MC3000FileNameManipulator : IMC3000FileNamesManipulator
    {
        /// <summary>
        /// Returns file name without a suffix and file's extention
        /// </summary>
        /// <param name="filenNameWithSuffix">Name of the file including suffix</param>
        /// <returns>String corresponding with a file name</returns>
        public string GetNCFilePattern(string filenNameWithSuffix)
        {
            return filenNameWithSuffix.FileByPattern();
        }

        /// <summary>
        /// Returns all files in the same folder , their names correspond to the NC file pattern
        /// </summary>
        /// <param name="fileNameWithSuffix">Name of the file including suffix</param>
        /// <param name="ncFilePattern">file name without a suffix and file's extention</param>
        /// <returns>List of file names</returns>
        public List<string> GetNCFiles(string fileNameWithSuffix, string ncFilePattern)
        {

            List<string> NCfiles = new List<string>();
            foreach (string file in fileNameWithSuffix.FilesByPattern())
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                if (Regex.Match(fileName, @"_{1}(\d+)$").Success)
                    NCfiles.Add(file);
            }
            return NCfiles;
        }
        /// <summary>
        /// Returns tool sheet file name without a suffix and file's extention
        /// </summary>
        /// <param name="toolSheetFileNameWithSuffix">Name of the tool sheet file including suffix</param>
        /// <returns>String corresponding with a tool sheet file name</returns>
        public string GetToolSheetPattern(string toolSheetFileNameWithSuffix)
        {
            string toolSheetPattern = string.Empty;
            int lastUnderscore = -1;
            foreach (string file in toolSheetFileNameWithSuffix.FilesByPattern())
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

        /// <summary>
        /// Returns all tool sheet files in the same folder , their names correspond to the NC file pattern
        /// </summary>
        /// <param name="toolSheetFileNameWithSuffix">Name of the tool sheet file including suffix</param>
        /// <param name="toolSheetPattern">file name without a suffix and file's extention</param>
        /// <returns>List of file names</returns>
        public List<string> GetToolSheets(string toolSheetFileNameWithSuffix, string toolSheetPattern)
        {
            List<string> ToolSheets = new List<string>();
            foreach (string item in toolSheetFileNameWithSuffix.FilesByPattern())
            {
                if (item.EndsWith(toolSheetPattern))
                    ToolSheets.Add(item);
            }
            return ToolSheets;
        }

        /// <summary>
        /// Result NC file
        /// </summary>
        /// <param name="ncFilePattern">file name without a suffix and file's extention</param>
        /// <param name="NCfiles">Returns all files in the same folder , their names correspond to the NC file pattern</param>
        /// <returns>File name</returns>
        public string GetNCfileResult(string ncFilePattern, List<string> NCfiles)
        {
            var folder = Path.GetDirectoryName(NCfiles[0]);
            var extension = Path.GetExtension(NCfiles[0]);
            var fileName = ncFilePattern.Substring(0, ncFilePattern.Length - 1);
            return folder + @"\" + fileName + extension;
        }

        /// <summary>
        /// Result tool sheet file
        /// </summary>
        /// <param name="toolSheetPattern">tool sheet file name without a suffix and file's extention</param>
        /// <param name="NCfiles">Returns all files in the same folder , their names correspond to the NC file pattern</param>
        /// <returns>File name</returns>
        public string GetToolSheetResult(string ncFileResult, string toolSheetPattern)
        {
            var folder = Path.GetDirectoryName(ncFileResult);
            var fileName = Path.GetFileNameWithoutExtension(ncFileResult) + toolSheetPattern;
            return folder + @"\" + fileName;
        }
    }
}
