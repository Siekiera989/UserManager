using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UserManager.Infrastructure.Interface;
using UserManager.Model;

namespace UserManager.Infrastructure.Service
{
    public class UserService : IUserService
    {
        private static readonly string MainPath = Path.Combine(Directory.GetParent(Process.GetCurrentProcess().MainModule?.FileName).ToString(), "UserProfiles");
        public ObservableCollection<Person> ReadFromFile()
        {
            var personList = new ObservableCollection<Person>();

            if (!Directory.Exists(MainPath))
            {
                Directory.CreateDirectory(MainPath);
                return null;
            }

            var files = Directory.GetFiles(MainPath, "*.xml", SearchOption.TopDirectoryOnly);

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

            var lastId = fileName.Substring(MainPath.Length+1).Substring(0,fileName.Substring(MainPath.Length+1).Length-4);
            person.ID = Convert.ToInt16(lastId);
            return person;
            
        }

        public short CreateNewPerson(Person person)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(Person));

                using (var sw = new StreamWriter($"{MainPath}//{GetNewId()+1}.xml"))
                   xmlSerializer.Serialize(sw, person);

                return GetNewId();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool DeleteExistingPerson(Person person)
        {
            var file = Path.Combine(MainPath, $"{person.ID}.xml");
            if (!File.Exists(file)) return false;

            File.Delete(file);
            return true;
        }

        public bool SaveChanges(List<Person> persons)
        {
            foreach (var person in persons)
            {
                DeleteExistingPerson(person);
                var xmlSerializer=new XmlSerializer(typeof(Person));
                if (person.ID == 0)
                    CreateNewPerson(person);
                else
                {
                    using (var sw = new StreamWriter($"{MainPath}//{person.ID}.xml"))
                        xmlSerializer.Serialize(sw, person);
                }
                
            }
            return true;
        }

        private short GetNewId()
        {
            var files = Directory.GetFiles(MainPath, "*.xml", SearchOption.TopDirectoryOnly);
            if (files.Length <= 0) return 0;
            var lastFile = files[files.Length - 1];
            var lastId=lastFile.Substring(MainPath.Length+1).Substring(0,lastFile.Substring(MainPath.Length+1).Length-4);
            return Convert.ToInt16(lastId);

        }
    }
}
