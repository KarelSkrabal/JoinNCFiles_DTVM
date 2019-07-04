using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JoinNCFiles_DTVM
{
    class PamsclReader : IPamsclReader
    {
        private const string PAMSCL_FILE_NAME = "pamscl.dat";

        private readonly string planitFolder;
        /// <summary>
        /// Ctor reader of pamscl.dat file
        /// </summary>
        /// <param name="planitFolder">Path to the pamscl.dat file - depends on the Edgecam version</param>
        public PamsclReader(string planitFolder)
        {
            this.planitFolder = planitFolder;
        }
        /// <summary>
        /// Reads pamscl.dat file
        /// </summary>
        /// <returns>Returns last edited pamscl file</returns>
        public string Read()
        {
            const Environment.SpecialFolder LOCAL_APPLICATION_DATA = Environment.SpecialFolder.LocalApplicationData;
            string folderPath = Environment.GetFolderPath(LOCAL_APPLICATION_DATA);
            const string PLAN_IT_SUBDIRECTORY = "Temp\\Planit";
            string pathToPlantSubdirectory = Path.Combine(folderPath, PLAN_IT_SUBDIRECTORY);
            DirectoryInfo ecFolders = new DirectoryInfo(pathToPlantSubdirectory);
            FileInfo[] ncFiles = ecFolders.GetFiles("pamscl.dat", SearchOption.AllDirectories);
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
