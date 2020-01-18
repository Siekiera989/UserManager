using System.Collections.Generic;
using UserManager.Model;

namespace UserManager.Infrastructure.Interface
{
    public interface IUserService
    {
        List<Person> ReadFromFile();
        void CreateNewPerson(Person person, bool deleteExisting);
        void DeleteExistingPerson(Person person);
        void SaveChanges(Person person);
    }
}