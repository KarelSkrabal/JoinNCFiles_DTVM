using System;
using System.IO;
using System.Linq;

namespace JoinNCFiles_DTVM
{
    /// <summary>
    /// Structure of the very first line of the pamscl.dat file edited by Edgecam every time that some NC file generated
    /// </summary>
    enum PAMSCL_STRUCTURE
    {
        postprocesor,
        index_1_NotUsed,
        index_2_NotUsed,
        index_3_NotUsed,
        ncFile,
        ppfFile
    }
    public class ECsettings
    {
        /// <summary>
        /// Last postprocesor used for generating NC file
        /// </summary>
        private string _postprocesor;
        public string Postprocesor
        {
            get { return _postprocesor; }
            set { _postprocesor = value; }
        }

        /// <summary>
        /// last NC file generated
        /// </summary>
        private string _NCfile;
        public string NCfile
        {
            get { return _NCfile; }
            set { _NCfile = value; }
        }

        /// <summary>
        /// ppf machining file
        /// </summary>
        private string _ppffile;
        public string Ppffile
        {
            get { return _ppffile; }
            set { _ppffile = value; }
        }

        /// <summary>
        /// back up of the last NC file generated
        /// </summary>
        private string _backUpFile;
        public string BackUpFile
        {
            get { return _backUpFile; }
            set { _backUpFile = value; }
        }
        /// <summary>
        /// Reader of pamscl.dat file
        /// </summary>
        private readonly IPamsclReader reader;

        /// <summary>
        /// Path to the pamscl.dat file
        /// </summary>
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
        /// <summary>
        /// Reads and splits the first line in the pamscl.dat file
        /// </summary>
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
        /// Backs up NC file
        /// </summary>
        public void BackUpNCfile()
        {
            string temp = Path.GetDirectoryName(this.NCfile) + "\\" + Path.GetFileNameWithoutExtension(this.NCfile) + ".tmp";
            File.Copy(this.NCfile, Path.GetDirectoryName(this.NCfile) + "\\" + Path.GetFileNameWithoutExtension(this.NCfile) + ".tmp", true);
        }
    }
}
