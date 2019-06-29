using System;
using System.Collections.Generic;
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
                    return new MC3000("path");
            return null;
        }
    }


}
