using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JoinNCFiles_DTVM
{

    public abstract class BaseDataReader<T> where T : class
    {
        public BaseDataReader()
        {
            //_IxmlDataReader = xmlDataReader;
        }

        public virtual T ReadData(string path)
        {
            T data;

            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BaseData));
                data = (T)serializer.Deserialize(reader);
            }

            return data;
        }
    }
}
