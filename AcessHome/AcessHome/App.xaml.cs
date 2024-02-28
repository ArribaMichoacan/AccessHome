using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AcessHome.Views;
namespace AcessHome
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

             MainPage = new NavigationPage(new LoginRounded());

            //MainPage = new AppShell();

            

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
