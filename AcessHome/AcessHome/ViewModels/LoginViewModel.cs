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
        string _Texto;
        
        private string urlImage = "https://cdn4.iconfinder.com/data/icons/glyphs/24/icons_user2-256.png";
        private FireBaseSettings _settings;

        string userName;
        string userPass;

        #endregion
        #region CONSTRUCTOR
        public LoginViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion
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

        #endregion
        #region PROCESOS
        public async Task LogIn()
        {
            _settings = new FireBaseSettings();

            var user = await _settings.GetUserKey(UserName, UserPass);

            if(user == null)
            {
                return;
            }else if (user.Admin == "1")
            {
                Application.Current.MainPage = new AppShell();
                //await Navigation.PushModalAsync(new AppShell());
            }else if(user.Admin == "0")
            {
                await Navigation.PushModalAsync(new MainPage());
            }
                                      
        }
        public void LogOff()
        {

        }
        #endregion
        #region COMANDOS
        public ICommand LogInCommand => new Command(async () => await LogIn());
        public ICommand ProcesoSimpcommand => new Command(LogOff);
        #endregion
    }
}
