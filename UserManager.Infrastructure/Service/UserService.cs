using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using UserManager.Infrastructure.Interface;
using UserManager.Model;

namespace UserManager.Infrastructure.Service
{
    public class UserService : IUserService
    {
        private static readonly string _mainPath = Path.Combine(Directory.GetParent(Process.GetCurrentProcess().MainModule?.FileName).ToString(), "UserProfiles");
        public ObservableCollection<Person> ReadFromFile()
        {
            var personList = new ObservableCollection<Person>();

            if (!Directory.Exists(_mainPath)) return personList;

            var files = Directory.GetFiles(_mainPath, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var file in files)
                personList.Add(GetPerson(file));
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

            var lastId = fileName.Substring(_mainPath.Length+1).Substring(0,fileName.Substring(_mainPath.Length+1).Length-4);
            person.ID = Convert.ToInt16(lastId);
            person.SetAge();
            return person;
            
        }

        public void CreateNewPerson(Person person, bool deleteExisting)
        {
            var xmlSerializer = new XmlSerializer(typeof(Person));

            var fileCount = Directory.GetFiles(_mainPath, "*.xml", SearchOption.TopDirectoryOnly).Length + 1;

            if (deleteExisting)
            {
                DeleteExistingPerson(person);
                using (var sw = new StreamWriter($"{_mainPath}//{person.ID}.xml"))
                    xmlSerializer.Serialize(sw, person);
            }
            else
            {
                using (var sw = new StreamWriter($"{_mainPath}//{fileCount}.xml"))
                    xmlSerializer.Serialize(sw, person);
            }
        }

        public void DeleteExistingPerson(Person person)
        {
            var file = Directory.GetFiles(_mainPath, $"{person.ID}.xml", SearchOption.TopDirectoryOnly).ToString();
            if (File.Exists(file))
                File.Delete(file);
        }

        public void SaveChanges(Person person) => CreateNewPerson(person, true);
    }
}
