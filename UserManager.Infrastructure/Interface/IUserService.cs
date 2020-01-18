using System.Collections.Generic;
using System.Collections.ObjectModel;
using UserManager.Model;

namespace UserManager.Infrastructure.Interface
{
    public interface IUserService
    {
        ObservableCollection<Person> ReadFromFile();
        void CreateNewPerson(Person person, bool deleteExisting);
        void DeleteExistingPerson(Person person);
        void SaveChanges(Person person);
    }
}