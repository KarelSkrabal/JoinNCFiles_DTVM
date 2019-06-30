using System;
using System.Xml.Serialization;

namespace JoinNCFiles_DTVM
{
    [Serializable]
    [XmlRootAttribute("SETTINGS")]

    public class MC3000Settings : BaseData
    {

        [XmlElement("SETTINGROW")]
        public SETTINGROW[] settingrows { get; set; }
    }

    public class SETTINGROW
    {
        public string lineNo { get; set; }

        public string firstLineNo { get; set; }

        public string lastLineNo { get; set; }

        public string insertToolingList { get; set; }
    }
}
