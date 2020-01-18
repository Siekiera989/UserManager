using System.Xml.Serialization;

namespace UserManager.Model
{
    [XmlRoot(nameof(PhoneNumber))]
    public class PhoneNumber
    {
        [XmlElement(nameof(TelephoneNumber))]
        public string TelephoneNumber { get; set; }
    }
}
