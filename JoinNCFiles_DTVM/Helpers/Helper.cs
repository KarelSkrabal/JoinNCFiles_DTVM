using System.IO;

namespace JoinNCFiles_DTVM
{
    public static class Helper
    {
        /// <summary>
        /// Gets array of files with the same pattern fileName_ that exist in the same folder
        /// </summary>
        /// <param name="str">File name path</param>
        /// <returns>Array of file names</returns>
        public static string[] FilesByPattern(this string fileNameIncludingPath)
        {
            return Directory.GetFiles(Path.GetDirectoryName(fileNameIncludingPath), FileByPattern(fileNameIncludingPath) + "*");
        }
        /// <summary>
        /// Gets the name of the file without suffix, eg. fileName_2 => fileName_
        /// </summary>
        /// <param name="fileNameIncludingPath"></param>
        /// <returns></returns>
        public static string FileByPattern(this string fileNameIncludingPath)
        {
            string fileNameWithoutSuffix = Path.GetFileName(fileNameIncludingPath);
            int lastUnderscore = fileNameWithoutSuffix.LastIndexOf('_');
            int pozice = fileNameWithoutSuffix.Length - lastUnderscore;
            int celkem = fileNameWithoutSuffix.Length;
            return Path.GetFileNameWithoutExtension(fileNameWithoutSuffix).Substring(0, celkem + 1 - pozice);
        }
    }
}
