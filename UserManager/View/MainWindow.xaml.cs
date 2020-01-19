using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UserManager.Model;
using UserManager.ViewModel;

namespace UserManager.View
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var vw = (MainViewModel)DataContext;
            vw.EditMode.Execute(e);
        }

        private void DeleteWholePerson(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete) return;
            var vm = (MainViewModel)DataContext;

            var dg = (DataGrid)sender;
            vm.DeletePersonCommand.Execute((Person)dg.SelectedItem);
        }

        private void DatePicker_OnCalendarClosed(object sender, RoutedEventArgs e)
        {
            var vw = (MainViewModel)DataContext;
            vw.DateChanged.Execute(sender);
        }
    }
}
