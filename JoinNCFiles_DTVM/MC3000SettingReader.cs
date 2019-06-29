using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JoinNCFiles_DTVM
{
    class MC3000SettingReader : BaseDataReader<MC3000Settings>
    {


        private static Lazy<MC3000SettingReader> instance = new Lazy<MC3000SettingReader>(() => new MC3000SettingReader());
        public static MC3000SettingReader Instance => instance.Value;

        private MC3000SettingReader()
        {

        }
        public override MC3000Settings ReadData(string path)
        {
            MC3000Settings data;

            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(MC3000Settings));
                data = (MC3000Settings)serializer.Deserialize(reader);
            }

            return data;
        }
    }
}
