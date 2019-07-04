using System.IO;

namespace JoinNCFiles_DTVM
{
    public static class Helper
    {
        public static string FilePattern(this string path)
        {

            return string.Empty;
        }


        public static string[] FilesByPattern(this string str)
        {
            int lastUnderscore = Path.GetFileName(str).LastIndexOf('_');
            //vytvorim si patern pro nacteni vsech souboru v dane slozce, ktere vehovuji podmince, ze ma nazev souboru doplneho o podtrzitko a suffix
            int pozice = Path.GetFileName(str).Length - lastUnderscore;
            int celkem = Path.GetFileName(str).Length;
            string patern = Path.GetFileNameWithoutExtension(str).Substring(0, celkem + 1 - pozice);

            return Directory.GetFiles(Path.GetDirectoryName(str), patern + "*");
        }
    }
}
