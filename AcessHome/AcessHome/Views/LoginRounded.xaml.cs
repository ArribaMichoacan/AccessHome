using AcessHome.Data;
using AcessHome.Models;
using AcessHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AcessHome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginRounded : ContentPage
    {
        public LoginRounded()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }

        private async void BtnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            try
            {
                Application.Current.MainPage = new AppShell();

                //Usuario usuario = new Usuario();

                //usuario.nombreUsuario = EntryUserName.Text;

                //usuario.passWord = EntryPassword .Text;

                //DataBaseHelper baseHelper = await DataBaseHelper.instance;                

                //if (await baseHelper.CheckCredentials(usuario) == 1)
                //{
                //    await Navigation.PushAsync(new MainPage(usuario));
                //}
                //else
                //{
                //    await DisplayAlert("Aviso", "Credenciales incorrectas", "Cerrar");
                //}

            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", $"{ex.Message}", "cerrar");
            }
        }
    }
}