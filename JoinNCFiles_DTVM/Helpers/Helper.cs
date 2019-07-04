using System.IO;

namespace JoinNCFiles_DTVM
{
    public static class Helper
    {
        /// <summary>
        /// Gets array of files with the same pattern xxx
        /// </summary>
        /// <param name="str">File name path</param>
        /// <returns>Array of file names</returns>
        public static string[] FilesByPattern(this string str)
        {
            int lastUnderscore = Path.GetFileName(str).LastIndexOf('_');
            int pozice = Path.GetFileName(str).Length - lastUnderscore;
            int celkem = Path.GetFileName(str).Length;
            string patern = Path.GetFileNameWithoutExtension(str).Substring(0, celkem + 1 - pozice);

            return Directory.GetFiles(Path.GetDirectoryName(str), patern + "*");
        }
    }
}
