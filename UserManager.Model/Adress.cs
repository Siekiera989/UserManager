using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserManager.Model
{
    [XmlRoot(nameof(Adress))]
    public class Adress
    {
        [XmlElement(nameof(StreetName))]
        public string StreetName { get; set; }
        [XmlElement(nameof(HouseNumber))]
        public string HouseNumber { get; set; }
        [XmlElement(nameof(ApartmentNumber))]
        public short? ApartmentNumber { get; set; }
        [XmlElement(nameof(PostalCode))]
        public string PostalCode { get; set; }
        [XmlElement(nameof(Town))]
        public string Town { get; set; }
    }
}
