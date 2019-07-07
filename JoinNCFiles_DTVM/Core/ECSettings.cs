using System;

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
        /// Reader of pamscl.dat file
        /// </summary>
        private readonly IPamsclReader _reader;

        private ECsettings(IPamsclReader reader)
        {
            _reader = reader;
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
            string[] matches = _reader.Read();
            Postprocesor = matches[(int)PAMSCL_STRUCTURE.postprocesor];
            NCfile = matches[(int)PAMSCL_STRUCTURE.ncFile];
            Ppffile = matches[(int)PAMSCL_STRUCTURE.ppfFile];
        }
    }
}
