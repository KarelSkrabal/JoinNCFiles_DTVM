using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JoinNCFiles_DTVM
{
    public class JoinFactory
    {
        public static IJoinNCfile createJoinner (string postprocesor)
        {
            if (!postprocesor.Contains("_"))
                new ArgumentException();

            Func<String, String> untilUnderscore = (s) => { return (new Regex("^(.*?)_")).Match(s).Groups[1].ToString(); };

            if(untilUnderscore(postprocesor).Equals("MC3001"))
            {

                string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(appPath);

                var reader = MC3000SettingReader.Instance;
                BaseData setting = (MC3000Settings)reader.ReadData(Path.Combine(directory, @"MC3000SETTINGS.xml"));
                return new MC3000("path", setting);
            }
                    
            return null;
        }
    }


}
