using System.Collections.Generic;
using System.Collections.ObjectModel;
using UserManager.Model;

namespace UserManager.Infrastructure.Interface
{
    public interface IUserService
    {
        ObservableCollection<Person> ReadFromFile();
        short CreateNewPerson(Person person);
        bool DeleteExistingPerson(Person person);
        bool SaveChanges(List<Person> person);
    }
}