using System.Collections.Generic;
using System.Xml.Serialization;

namespace GazPromImport.Core.Models
{
    /// <summary>
    /// Список инфы по ФЛ
    /// </summary>
    //[XmlRoot(ElementName = "FLInfoList")]
    public class FLInfoList
    {
        /// <summary>
        /// Список ФЛ
        /// </summary>
        [XmlElement(ElementName = "FLInfo")]
        public List<FLInfo> FLInfos { get; set; }
    }
}
