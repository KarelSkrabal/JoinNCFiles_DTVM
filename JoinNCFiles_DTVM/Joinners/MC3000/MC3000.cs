﻿using JoinNCFiles_DTVM.Joinners.MC3000.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JoinNCFiles_DTVM
{
    /// <summary>
    /// Joining NC files generated by the postprocesor MC3000
    /// </summary>
    public class MC3000 : IJoinNCfile
    {

        /// <summary>
        /// List of all NC files to process
        /// </summary>
        public List<string> NCfiles = new List<string>();
        /// <summary>
        /// List of tool sheets from all NC files
        /// </summary>
        private List<string> ToolSheets = new List<string>();
        /// <summary>
        /// Name of tool sheet file
        /// </summary>
        private string ToolSheetResult = string.Empty;
        /// <summary>
        /// Name of NC file
        /// </summary>
        public string NCfileResult = string.Empty;
        /// <summary>
        /// xxxx todo - remove
        /// </summary>
        private int lineNo = 10;
        /// <summary>
        /// xxx todo - remove
        /// </summary>
        private readonly string firstLineNo = " @714";
        /// <summary>
        /// xxx todo - remove
        /// </summary>
        private readonly string lastLineNo = " M30";

        /// <summary>
        /// Setting file for the actual postprocesor
        /// </summary>
        private readonly MC3000Settings settings;
        private readonly IMC3000FileNamesManipulator _fileNameManipulator;
        private readonly string resultNCfileName = string.Empty;

        public string LastLineNo => lastLineNo;

        /// <summary>
        /// Gets the name of the final NCfile by replacing suffix number with a underscore character
        /// </summary>
        /// <param name="path">Last generated NC output, get it from pamscl.dat file. The name has to follow naming convention</param>
        private void GetResultfileNames(string str)
        {



            //if (Regex.Match(str, @"_{1}\d+").Success)
            //{
            //    //todo - change it to the method string GetNCFilePattern(string str)
            //    string ncFilePattern = str.FileByPattern();

            //    //todo - change it to the method string GetToolSheetPattern(string str)
            //    string toolSheetPattern = string.Empty;
            //    int lastUnderscore = -1;
            //    foreach (string item in str.FilesByPattern())
            //    {
            //        if (lastUnderscore < item.LastIndexOf('-'))
            //        {
            //            lastUnderscore = item.LastIndexOf('-');
            //            toolSheetPattern = item.Substring(lastUnderscore);
            //        }
            //    }
            //    //todo- change it to the method string[] GetToolSheets(string toolSheetPattern) 
            //    //todo- change it to the method string[] GetNCFiles(string ncFilePattern) 
            //    foreach (string item in str.FilesByPattern())
            //    {
            //        if (item.EndsWith(toolSheetPattern))
            //            ToolSheets.Add(item);
            //        else
            //            NCfiles.Add(item);
            //    }

            //    if (NCfiles.Count > 1)
            //    {
            //        //todo-change it to the method GetNCfileResult(string ncFilePattern,string[] NCfiles)
            //        NCfileResult = Path.GetDirectoryName(NCfiles[0].ToString()) + @"\" + ncFilePattern.Remove(ncFilePattern.Length - 1) + Path.GetExtension(NCfiles[0].ToString());
            //        //todo-change it to the method GetToolSheetFileResult(string toolSheetPattern,string[] ToolSheets)
            //        ToolSheetResult = Path.GetDirectoryName(NCfiles[0].ToString()) + @"\" + ncFilePattern.Remove(ncFilePattern.Length - 1) + "-TOOL" + Path.GetExtension(NCfiles[0].ToString());
            //    }
            //}
            //else
            //{
            //    NCfiles.Add("Chyba výběru souboru");
            //    Console.WriteLine("Chybný výběr souboru");
            //    Console.ReadKey();
            //}
        }

        /// <summary>
        /// Joinner for machine MC3000
        /// </summary>
        /// <param name="settings">Settings file inherited from BaseData</param>
        public MC3000(BaseData settings, IMC3000FileNamesManipulator fileNameManipulator)
        {
            this.settings = (MC3000Settings)settings;
            _fileNameManipulator = fileNameManipulator;
        }

        /// <summary>
        /// Joins NC files generated in the same folder with the same name and incremented suffix
        /// </summary>
        /// <param name="lastGeneratedNCfile">Last generated NC file</param>
        public void JoinFiles(string lastGeneratedNCfile)
        {
                var toolSheetPattern = _fileNameManipulator.GetToolSheetPattern(lastGeneratedNCfile);
                var ncFilePattern = _fileNameManipulator.GetNCFilePattern(lastGeneratedNCfile);
                var ncFiles = _fileNameManipulator.GetNCFiles(lastGeneratedNCfile, ncFilePattern);
                var toolSheetFiles = _fileNameManipulator.GetToolSheets(lastGeneratedNCfile, toolSheetPattern);
                var ncFileResult = _fileNameManipulator.GetNCfileResult(ncFilePattern, ncFiles);
                var toolSheetResult = _fileNameManipulator.GetToolSheetResult(ncFileResult, toolSheetPattern);
                //Join(false, ToolSheetResult, ToolSheets);
                Join(false, toolSheetResult, toolSheetFiles);
                //joining tool sheets
                //Join(true, NCfileResult, NCfiles);
                Join(true, ncFileResult, ncFiles);
                //Adding tool sheets into the resulting NC file
                AddToolSheetToNCfile();
        }

        /// <summary>
        /// Joins files generated with the pattern xxx_N.nc
        /// xxx -> constatnt name of the file
        /// N -> suffix number, orders files existing in the same folder
        /// </summary>
        /// <param name="insertLine">flag if insert line numbers</param>
        /// <param name="result">name of the resulting file</param>
        /// <param name="paths">list of files to join</param>
        private void Join(bool insertLine, string result, List<string> paths)
        {
            string str;
            Encoding defaultEncoding = Encoding.GetEncoding(Encoding.Default.CodePage, new EncoderExceptionFallback(), new DecoderExceptionFallback());

            //delete if any result file exists
            this.Delete(result);

            foreach (String file in paths)
            {
                using (TextWriter tw = new StreamWriter(Path.GetFullPath(result), true, defaultEncoding))
                {
                    using (StreamReader tr = new StreamReader(file, defaultEncoding))
                    {
                        while (!tr.EndOfStream)
                        {
                            str = tr.ReadLine();
                            if (insertLine)
                                tw.WriteLine(Renumber(str, ref this.lineNo));
                            else
                                tw.WriteLine(str);
                        }
                        tr.Close();
                    }
                    tw.Close();
                }
            }
        }

        /// <summary>
        /// Reads all toolsheets lines
        /// </summary>
        /// <returns>List of toolsheets lines</returns>
        private List<string> GetToolSheet()
        {
            var allLines = File.ReadAllLines(ToolSheetResult, Encoding.Default);
            List<string> lines = new List<string>(allLines);
            return lines;
        }

        /// <summary>
        /// Inserts toolsheets lines right after the string  "(OSAZENI ZASOBNIKU)"
        /// </summary>
        private void AddToolSheetToNCfile()
        {
            var allLines = File.ReadAllLines(this.NCfileResult, Encoding.Default);
            List<string> lines = new List<string>(allLines);
            int index = lines.IndexOf(this.settings.Settingrows[0].InsertToolingList);
            lines.InsertRange(index + 1, GetToolSheet());
            File.WriteAllLines(this.NCfileResult, lines.ToArray());
        }


        /// <summary>
        /// Deletes a resulting file in case that exists from the previous run
        /// </summary>
        /// <param name="file">File to delete</param>
        private void Delete(string file)
        {
            try
            {
                FileInfo NCfile = new FileInfo(file);
                if (NCfile.Exists)
                    NCfile.Delete();
                NCfile = null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Chyba ... " + e.StackTrace +
                    Environment.NewLine + Environment.NewLine +
                    e.Message + Environment.NewLine + Environment.NewLine +
                    e.TargetSite);
                Console.WriteLine("Program bude ukončen libovolnou klávesou.");
                Console.ReadKey();
            }
        }
        /// <summary>
        /// Adds line numbers
        /// </summary>
        /// <param name="input">Actual line for prefixing via a line No.</param>
        /// <param name="num">Actual line No.</param>
        /// <returns>Nacteny radek doplneny o cislo radku</returns>
        private string Renumber(string input, ref int num)
        {
            string ret = input;

            if (input.StartsWith(")"))
            {
                ret = ("(" + num.ToString()).PadRight(1, ' ') + input;
                num += 10;
            }

            if (input.StartsWith(" N"))
            {
                input = input.Replace("N", "N" + num.ToString()).PadRight(1, ' ');
                ret = input.Replace("R800=", " R800=" + num.ToString()).PadRight(1, ' ');
                num += 10;
            }
            return ret;
        }
    }
}
