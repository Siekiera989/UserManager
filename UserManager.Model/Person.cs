using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace UserManager.Model
{
    [XmlRoot(nameof(Person))]
    public class Person
    {
        public short ID { get; set; }
        [XmlElement(nameof(FirstName))]
        public string FirstName { get; set; }
        [XmlElement(nameof(LastName))]
        public string LastName { get; set; }
        [XmlElement(nameof(PhoneNumber))]
        public List<PhoneNumber> PhoneNumbers { get; set; }
        [XmlElement(nameof(BirthDate))]
        public DateTime BirthDate { get; set; }
        [XmlElement(nameof(Age))]
        public short Age { get; }
        [XmlElement(nameof(AdressesList))]
        public List<Adress> AdressesList { get; set; }

        public Person()
        {
            Age = Convert.ToInt16(DateTime.Today - BirthDate);
            AdressesList=new List<Adress>();
            PhoneNumbers=new List<PhoneNumber>();
        }
    }
}
