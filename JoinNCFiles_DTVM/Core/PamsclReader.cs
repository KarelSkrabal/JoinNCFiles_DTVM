using System;
using System.IO;
using System.Text;

namespace JoinNCFiles_DTVM
{
    public class PamsclReader : IPamsclReader
    {
        private readonly string _pamsclFile;

        public PamsclReader(string pamsclFile)
        {
            _pamsclFile = pamsclFile;
        }

        /// <summary>
        /// Reads pamscl.dat file
        /// </summary>
        /// <returns>Returns last edited pamscl file</returns>
        public string[] Read
        {
            get
            {
                string[] ret;
                char[] sep = { ',' };
                using (StreamReader sr = new StreamReader(_pamsclFile, Encoding.Default))
                {
                    ret = (sr.ReadLine()).Split(sep, StringSplitOptions.None);
                }

                return ret;
            }
        }
    }
}

