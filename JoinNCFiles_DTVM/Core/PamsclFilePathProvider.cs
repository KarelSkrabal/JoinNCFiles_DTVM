using JoinNCFiles_DTVM.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JoinNCFiles_DTVM.Core
{
    
    public class PamsclFilePathProvider : IPamsclFilePathProviders
    {
        private readonly string _planitFolder = string.Empty;
        private readonly string _pamsclFileName = "pamscl.dat";

        public PamsclFilePathProvider(string planitFolder)
        {
            _planitFolder = planitFolder;
        }
        /// <summary>
        /// Gets all pamscl.dat files that exist in subfolders of Planit folder
        /// </summary>
        /// <returns>Last edited pamscl.dat file</returns>
        public string GetPamsclFilePath()
        {
            const Environment.SpecialFolder LOCAL_APPLICATION_DATA = Environment.SpecialFolder.LocalApplicationData;
            string folderPath = Environment.GetFolderPath(LOCAL_APPLICATION_DATA);
            string pathToPlantSubdirectory = folderPath + _planitFolder;
            DirectoryInfo ecFolders = new DirectoryInfo(folderPath + _planitFolder);
            FileInfo[] ncFiles = ecFolders.GetFiles(_pamsclFileName, SearchOption.AllDirectories);
            String foundFile = String.Empty;
            IOrderedEnumerable<FileInfo> fileonFosDescending = from fileInfo in ncFiles
                                                               orderby fileInfo.LastWriteTime descending
                                                               select fileInfo;
            if (fileonFosDescending.Any())
            {
                FileInfo firstFileInfo = fileonFosDescending.First();
                DateTime date = firstFileInfo.LastWriteTime;
                foundFile = firstFileInfo.FullName;
            }
            return foundFile;
        }
    }
}
