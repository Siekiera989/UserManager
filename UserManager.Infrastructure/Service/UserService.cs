using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using UserManager.Infrastructure.Interface;
using UserManager.Model;

namespace UserManager.Infrastructure.Service
{
    class UserService:IUserService
    {
        private readonly string MainPath = Path.Combine(Directory.GetCurrentDirectory(), "//UserProfiles");
        public List<Person> ReadFromFile()
        {
            var personList = new List<Person>();
            
            if (!Directory.Exists(MainPath)) return personList;

            var files = Directory.GetFiles(MainPath, "*.xml", SearchOption.TopDirectoryOnly);
            personList.AddRange(files.Select(GetPerson));

            return personList;
        }

        private static Person GetPerson(string fileName)
        {
            Person person;
            if (!fileName.Contains(".xml")) return null;

            var streamReader = new StreamReader(fileName);
            var documentXml = new XmlDocument();
            documentXml.Load(streamReader);

            var serializer = new XmlSerializer(typeof(Person));
            using (TextReader text = new StringReader(documentXml.OuterXml))
                person = (Person)serializer.Deserialize(text);

            person.ID = Convert.ToInt16(fileName);
            return person;
        }

        public void CreateNewPerson(Person person, bool deleteExisting)
        {
            var xmlSerializer = new XmlSerializer(typeof(Person));

            var fileCount = Directory.GetFiles(MainPath, "*.xml", SearchOption.TopDirectoryOnly).Length+1;

            if (deleteExisting)
            {
                DeleteExistingPerson(person);
                fileCount = person.ID;
            }
            var sw = new StreamWriter($"{MainPath}//{fileCount}.xml");
            
            xmlSerializer.Serialize(sw, person);
        }

        public void DeleteExistingPerson(Person person)
        {
            var file = Directory.GetFiles(MainPath,$"{person.ID}.xml",SearchOption.TopDirectoryOnly).ToString();
            if (File.Exists(file))
                File.Delete(file);
        }

        public void SaveChanges(Person person) => CreateNewPerson(person,true);
    }
}
