using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AcessHome.Services.Firebase;
using AcessHome.Views;
using Xamarin.Forms;

namespace AcessHome.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region VARIABLES

        private string _Texto;

        private string urlImage = "https://cdn4.iconfinder.com/data/icons/glyphs/24/icons_user2-256.png";
        private FireBaseSettings _settings;

        private string userName;
        private string userPass;

        private bool isVisible = false;
        private bool showIndicator = false;

        private bool _btnEnabled;

        #endregion VARIABLES

        #region CONSTRUCTOR

        public LoginViewModel(INavigation navigation)
        {
            Navigation = navigation;
            ShowIndicator = false;
        }

        #endregion CONSTRUCTOR

        #region OBJETOS

        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }

        public string UrlImage { get => urlImage; set => urlImage = value; }

        public string UserName
        {
            get { return userName; }

            set { SetValue(ref userName, value); }
        }

        public string UserPass
        {
            get { return userPass; }

            set { SetValue(ref userPass, value); }
        }

        public bool BtnEnabled
        {
            get { return _btnEnabled; }
            set
            {
                SetValue(ref _btnEnabled, value);
            }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set { SetValue(ref isVisible, value); }
        }

        public bool ShowIndicator
        {
            get { return showIndicator; }

            set { SetValue(ref showIndicator, value); }

        }

        #endregion OBJETOS

        #region PROCESOS

        public async Task LogIn()
        {
            if (IsBusy)
                return;

            _settings = new FireBaseSettings();
            try
            {

                await _settings.ObtenerVisitas("2024-02-24");

                IsBusy = true; // evitar que se lance 2 veces el proceso
                ShowIndicator = true; //show activity indicator

                if(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserPass))
                {
                    await DisplayAlert("Aviso", "El usuario o contraseña no pueden estar vacios", "Cerrar");
                    return;
                }

                var user = await _settings.GetUserKey(UserName, UserPass);
                if (user == null)
                {
                    IsVisible = true; // si no hay usuario registrado, se muestra el span
                    return;
                }
                else
                {
                    if (user.Admin == "1")
                    {
                       
                        Application.Current.MainPage = new AppShell(); //usuario admin, se carga la vista de admin
                                                                       //await Navigation.PushModalAsync(new AppShell());
                    }
                    else if (user.Admin == "0")
                    {
                        await Navigation.PushModalAsync(new MainPage()); //se carga la vista de no admin
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Cerrar", $"Ocurrio un error al intentar iniciar sesion...{ex.Message} ", "Cerrar");
            }
            finally
            {
                IsBusy = false;
                ShowIndicator = false;
            }
        }


        public async Task LoadRegisterView()
        {
            if(IsBusy) 
                return;
            try
            {
                await Navigation.PushAsync(new RegisterView());
            }catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrio lo siguiente: {ex.Message}", "Cerrar");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void LogOff()
        {
        }

        #endregion PROCESOS

        #region COMANDOS

        public ICommand LogInCommand => new Command(async () => await LogIn());

        public ICommand LoadRegisterViewCommand => new Command(async () => await LoadRegisterView());

        public ICommand ProcesoSimpcommand => new Command(LogOff);

        #endregion COMANDOS
    }
}