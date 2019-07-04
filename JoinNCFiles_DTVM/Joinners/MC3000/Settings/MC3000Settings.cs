using System;
using System.Xml.Serialization;

namespace JoinNCFiles_DTVM
{
    [Serializable]
    [XmlRootAttribute("SETTINGS")]

    
    public class MC3000Settings : BaseData
    {

        [XmlElement("SETTINGROW")]
        public SETTINGROW[] Settingrows { get; set; }
    }

    public class SETTINGROW
    {
        public string LineNo { get; set; }

        public string FirstLineNo { get; set; }

        public string LastLineNo { get; set; }

        public string InsertToolingList { get; set; }
    }
}
