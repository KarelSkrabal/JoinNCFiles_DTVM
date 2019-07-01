using System;
using System.IO;
using System.Text.RegularExpressions;

namespace JoinNCFiles_DTVM
{
    public class JoinFactory
    {
        public static IJoinNCfile createJoinner (string postprocesor)
        {
            if (!postprocesor.Contains("_"))
                new ArgumentException();
            
            //find where settings.xml file is and read it
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(appPath);

            Func<String, String> untilUnderscore = (s) => { return (new Regex("^(.*?)_")).Match(s).Groups[1].ToString(); };

            if(untilUnderscore(postprocesor).Equals("MC3001"))
            {
                var reader = MC3000SettingReader.Instance;
                BaseData setting = (MC3000Settings)reader.ReadData(Path.Combine(directory, @"MC3000SETTINGS.xml"));            
                //create and return joinner for particular postprocesor
                return new MC3000(setting);
            } else if (untilUnderscore(postprocesor).Equals("WACO3"))
            {
                //not ready eyt
                throw new NotImplementedException();
            }

            throw new ArgumentException();
        }
    }


}
