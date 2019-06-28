using System;
using System.IO;
using System.Linq;

namespace JoinNCFiles_DTVM
{
    class ECsettings
    {
        /// <summary>
        /// Posledni editovany pamscl.dat soubor
        /// </summary>
        private string _pamsclFile;
        public string pamsclFile
        {
            get { return _pamsclFile; }
            set { _pamsclFile = value; }
        }

        /// <summary>
        /// Posledni pouzity postprocesor
        /// </summary>
        private string _post;
        public string post
        {
            get { return _post; }
            set { _post = value; }
        }

        /// <summary>
        /// Posledni vytvoreny NC soubor.Vyhodnocuji se vsechny vsechny verze Edgecam
        /// </summary>
        private string _NCfile;
        public string NCfile
        {
            get { return _NCfile; }
            set { _NCfile = value; }
        }

        /// <summary>
        /// Obrabeci postup ze ktereho byl naposledy vygenerovany NC soubor
        /// </summary>
        private string _ppffile;
        public string ppffile
        {
            get { return _ppffile; }
            set { ppffile = value; }
        }

        /// <summary>
        /// Zaloha posledniho editovaneho Nc souboru
        /// </summary>
        private string _backUpFile;
        public string backUpFile
        {
            get { return _backUpFile; }
            set { backUpFile = value; }
        }

        public ECsettings()
        {
            this.pamsclFile = GetPamscl();
            GetDetails();
        }

        /// <summary>
        /// Procedura pro ziskani souboru pamscl.dat ktery byl naposledy editovan.
        /// </summary>
        /// <returns>Kompletni cesta k poslednimu editovanemu soubor pamscl.</returns>
        private string GetPamscl()
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

        private void GetDetails()
        {
            char[] sep = { ',' };
            StreamReader sr = new StreamReader(this.pamsclFile, System.Text.Encoding.Default);
            string[] matches = (sr.ReadLine()).Split(sep, StringSplitOptions.None);
            this.post = matches[0];
            this.NCfile = matches[3];
            _ppffile = matches[4];
            _backUpFile = Path.GetDirectoryName(matches[3]) + "\\" + Path.GetFileNameWithoutExtension(matches[3]) + ".tmp";
        }

        /// <summary>
        /// Public void backUpNCfile
        /// Metoda pro vytvoreni zalohy editovaneho NC souboru. V pripade, ze zalohovany soubor jiz existuje, dojde k jeho prepsani.
        /// Vytvoreny soubor bude mit stejne umisteni i nazev jako puvodni soubor, koncovka souboru se zmeni na *.tmp
        /// </summary>
        public void backUpNCfile()
        {
            string temp = Path.GetDirectoryName(this.NCfile) + "\\" + Path.GetFileNameWithoutExtension(this.NCfile) + ".tmp";
            File.Copy(this.NCfile, Path.GetDirectoryName(this.NCfile) + "\\" + Path.GetFileNameWithoutExtension(this.NCfile) + ".tmp", true);
        }
    }
}
