using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JoinNCFiles_DTVM
{
    class PamsclReader : IPamsclReader
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
        public string[] Read()
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
