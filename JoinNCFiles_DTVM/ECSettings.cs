using System;
using System.IO;
using System.Linq;

namespace JoinNCFiles_DTVM
{
    enum PAMSCL_STRUCTURE
    {
        postprocesor,
        index_1_NotUsed,
        index_2_NotUsed,
        notUsed,
        ncFile,
        ppfFile
    }
    public class ECsettings
    {
        /// <summary>
        /// Posledni pouzity postprocesor
        /// </summary>
        private string _postprocesor;
        public string Postprocesor
        {
            get { return _postprocesor; }
            set { _postprocesor = value; }
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
        public string Ppffile
        {
            get { return _ppffile; }
            set { _ppffile = value; }
        }

        /// <summary>
        /// Zaloha posledniho editovaneho Nc souboru
        /// </summary>
        private string _backUpFile;
        public string BackUpFile
        {
            get { return _backUpFile; }
            set { _backUpFile = value; }
        }

        private readonly IPamsclReader reader;
        private readonly string pamsclFilePath;

        private ECsettings(IPamsclReader reader)
        {
            this.reader = reader;
            this.pamsclFilePath = reader.Read();
        }

        private static Lazy<ECsettings> instance = null;
        public static ECsettings CreateInstance(IPamsclReader reader)
        {
            if(instance == null)
            {
                instance = new Lazy<ECsettings>(() => new ECsettings(reader));
            }
            return instance.Value;
        }



        //TODO - https://www.codeproject.com/Tips/1033646/SOLID-Principle-with-Csharp-Example

       // for GetPamscl() implement interface like interface segregation principle!!!!!

        public void GetDetails()
        {
            char[] sep = { ',' };
            StreamReader sr = new StreamReader(this.pamsclFilePath, System.Text.Encoding.Default);
            string[] matches = (sr.ReadLine()).Split(sep, StringSplitOptions.None);
            this.Postprocesor = matches[(int)PAMSCL_STRUCTURE.postprocesor];
            this.NCfile = matches[(int)PAMSCL_STRUCTURE.ncFile];
            this.Ppffile = matches[(int)PAMSCL_STRUCTURE.ppfFile];
            this.BackUpFile = Path.GetDirectoryName(matches[(int)PAMSCL_STRUCTURE.ncFile]) + "\\" + Path.GetFileNameWithoutExtension(matches[(int)PAMSCL_STRUCTURE.ncFile]) + ".tmp";
        }

        /// <summary>
        /// Public void backUpNCfile
        /// Metoda pro vytvoreni zalohy editovaneho NC souboru. V pripade, ze zalohovany soubor jiz existuje, dojde k jeho prepsani.
        /// Vytvoreny soubor bude mit stejne umisteni i nazev jako puvodni soubor, koncovka souboru se zmeni na *.tmp
        /// </summary>
        public void BackUpNCfile()
        {
            string temp = Path.GetDirectoryName(this.NCfile) + "\\" + Path.GetFileNameWithoutExtension(this.NCfile) + ".tmp";
            File.Copy(this.NCfile, Path.GetDirectoryName(this.NCfile) + "\\" + Path.GetFileNameWithoutExtension(this.NCfile) + ".tmp", true);
        }
    }
}
