using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public string CustomerName { get; set; }
    }
}
