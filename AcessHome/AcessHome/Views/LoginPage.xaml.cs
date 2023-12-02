using AcessHome.Data;
using AcessHome.Models;
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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

     
        private async void BtnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();

                usuario.nombreUsuario = txtUserName.Text;

                usuario.passWord = txtUserPassword.Text;

                DataBaseHelper baseHelper = await DataBaseHelper.instance;


                //await baseHelper.saveUsuarioInfo(usuario);

                if(await baseHelper.CheckCredentials(usuario) ==1)
                {
                    await Navigation.PushAsync(new MainPage(usuario));
                }
                else
                {
                    await DisplayAlert("Aviso", "Credenciales incorrectas", "Cerrar");
                }
                    

                


            }catch (Exception ex)
            {
                await DisplayAlert("Aviso", $"{ex.Message}", "cerrar");
            }
        }
    }
}