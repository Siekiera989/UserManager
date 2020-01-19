using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using UserManager.Infrastructure.Interface;
using UserManager.Infrastructure.Service;

namespace UserManager.ViewModel
{
   public class ViewModelLocator
    {
       
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

          SimpleIoc.Default.Register<IUserService,UserService>();

            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}