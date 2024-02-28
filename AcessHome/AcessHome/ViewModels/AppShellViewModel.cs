using AcessHome.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AcessHome.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {






        public async Task LogOut()
        {
            await Shell.Current.GoToAsync("LoginPage");
            // Navigation.PopAsync();
            //await Shell.Current.Navigation.PopAsync();

        }

        public ICommand LogOutCommand => new Command(async () => await LogOut());
    }
}
