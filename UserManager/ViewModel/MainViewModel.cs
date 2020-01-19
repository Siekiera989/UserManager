using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        private bool _wasEdited;

        public RelayCommand Loading { get; set; }
        public RelayCommand Save { get; set; }
        public RelayCommand<Person> DeletePersonCommand { get; set; }
        public RelayCommand<DataGridCellEditEndingEventArgs> EditMode { get; set; }
        public RelayCommand<object> DateChanged { get; set; }
        public ObservableCollection<Person> PersonsList { get => _personsList; set => Set(ref _personsList, value); }
        public Person SelectedPerson { get => _selectedPerson; set => Set(ref _selectedPerson, value); }

        public bool WasEdited{get => _wasEdited;set =>Set(ref _wasEdited,  value);}

        public MainViewModel(IUserService userService)
        {
            _userService = userService;
            _personsList = new ObservableCollection<Person>();
            _selectedPerson = new Person();

            Loading = new RelayCommand(ApplicationLoading);
            Save = new RelayCommand(SaveChanges);
            DeletePersonCommand = new RelayCommand<Person>(DeletePerson);
            DateChanged=new RelayCommand<object>(DateChangeEvent);
            EditMode = new RelayCommand<DataGridCellEditEndingEventArgs>(SetEditMode);
        }

        private void SetEditMode(DataGridCellEditEndingEventArgs obj)
        {
            SetEditedProperty();
        }

        private void SetEditedProperty()
        {
            SelectedPerson.IsEdited = true;
            if (!WasEdited)
                WasEdited = true;
        }

        private void DateChangeEvent(object sender)
        {
            var date = (DatePicker)sender;
            if (date.SelectedDate != null) SelectedPerson.BirthDate = (DateTime) date.SelectedDate;
            SetEditedProperty();
        }

        private void DeletePerson(Person person)
        {
            if (_userService.DeleteExistingPerson(SelectedPerson))
                LoadAgain("User deleted successfully");
        }

        private void SaveChanges()
        {
            if (_userService.SaveChanges(PersonsList.Where(x => x.IsEdited).ToList()))
                LoadAgain("User information has been modified");
            WasEdited = false;
        }

        private void ApplicationLoading()
        {
            PersonsList?.Clear();
            PersonsList = _userService.ReadFromFile();
            WasEdited = false;
        }

        private void LoadAgain(string message)
        {
            ApplicationLoading();
            MessageBox.Show(message, "Success",MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}