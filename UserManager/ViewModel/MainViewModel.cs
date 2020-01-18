using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using UserManager.Infrastructure.Interface;
using UserManager.Model;

namespace UserManager.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        private readonly IUserService _userService;

        private ObservableCollection<Person> _personsList;
        private Person _selectedPerson;
        public RelayCommand Loading { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand SetNewID { get; set; }
        public ObservableCollection<Person> PersonsList { get=>_personsList; set=>Set(ref _personsList,value); }
        public Person SelectedPerson { get=>_selectedPerson; set=>Set(ref _selectedPerson,value); }
        
        public MainViewModel(IUserService userService)
        {
            _userService = userService;
            _personsList=new ObservableCollection<Person>();

            Loading=new RelayCommand(ApplicationLoading);
            Add=new RelayCommand(AddNewUser);
            SetNewID=new RelayCommand(CreateNewId);
        }

        private void ApplicationLoading()
        {
            PersonsList?.Clear();
            PersonsList=_userService.ReadFromFile();
        }

        private void AddNewUser()
        {
            _userService.CreateNewPerson(SelectedPerson,false);
            ApplicationLoading();
            MessageBox.Show("New user has been added");
        }

        private void CreateNewId()
        {
            SelectedPerson.ID = (short)(PersonsList.Count + 1);
        }
    }
}