using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace UserManager.Model
{
    [XmlRoot(nameof(Person))]
    public class Person
    {
        [XmlIgnore] public short ID { get; set; }
        [XmlElement(nameof(FirstName))]
        public string FirstName { get; set; }
        [XmlElement(nameof(LastName))] public string LastName { get; set; }
        [XmlElement(nameof(BirthDate))] public DateTime BirthDate { get; set; } = DateTime.Today;
        [XmlIgnore] public short Age => GetAge();
        [XmlElement(nameof(PhoneNumber))] public List<PhoneNumber> PhoneNumbers { get; set; }
        [XmlElement(nameof(AdressesList))] public List<Adress> AdressesList { get; set; }
        [XmlIgnore] public bool IsEdited { get; set; }

        public Person()
        {
            AdressesList = new List<Adress>();
            PhoneNumbers = new List<PhoneNumber>();
        }

        private short GetAge() => Convert.ToInt16(DateTime.Today.Year - BirthDate.Year);
    }
}
