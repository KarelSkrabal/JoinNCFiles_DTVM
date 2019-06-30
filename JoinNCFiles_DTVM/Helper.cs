﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JoinNCFiles_DTVM
{
    public static class Helper
    {
        public static string filePattern(this string path)
        {

            return string.Empty;
        }


        public static string[] filesByPattern(this string str)
        {
            int lastUnderscore = Path.GetFileName(str).LastIndexOf('_');
            //vytvorim si patern pro nacteni vsech souboru v dane slozce, ktere vehovuji podmince, ze ma nazev souboru doplneho o podtrzitko a suffix
            int pozice = Path.GetFileName(str).Length - lastUnderscore;
            int celkem = Path.GetFileName(str).Length;
            string patern = Path.GetFileNameWithoutExtension(str).Substring(0, celkem + 1 - pozice);
            //tmp = Directory.GetFiles(Path.GetDirectoryName(str), patern + "*");


            return Directory.GetFiles(Path.GetDirectoryName(str), patern + "*");
        }
    }
}