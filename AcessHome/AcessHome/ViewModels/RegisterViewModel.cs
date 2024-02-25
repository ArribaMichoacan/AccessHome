using AcessHome.Services.Firebase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AcessHome.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region VARIABLES

        private FireBaseSettings _settings;

        string _Texto;
        string _MensajeUserName;
        string _MensajePassword;

        string userName;
        string userPass;
        string userPass2;
        #endregion
        #region CONSTRUCTOR
        public RegisterViewModel()
        {
            _settings = new FireBaseSettings();
        }
        #endregion
        #region OBJETOS
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }

        public string MensajeUserName
        {
            get { return _MensajeUserName; }

            set { SetValue(ref _MensajeUserName, value); }
        }


        public string MensajeUserPass
        {
            get { return _MensajePassword; }

            set { SetValue(ref _MensajePassword, value); }
        }

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

        public string UserPass2
        {
            get { return userPass2; }

            set { SetValue(ref userPass2, value); }
        }




        #endregion
        #region PROCESOS
        public async Task Registrarse()
        {
            if(IsBusy) 
                return;

            try
            {
                IsBusy = true;

                if(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserPass))
                {
                    await DisplayAlert("Aviso", "Ingresa un usuario y contraseña valido", "Cerrar");
                    return;
                }

                if(UserPass != UserPass2)
                {
                    MensajeUserPass = "Las contraseñas deben ser iguales";
                    return;
                }

                bool resultado = await _settings.VerificarUsuario(userName, UserPass);

              if(resultado == false) // si es false, ya hay un usuario registrado con ese nombre de usuario
                {
                    MensajeUserName = "Ya hay un usuario registrado con ese nombre";
                    return;
                }
                else
                {
                    await _settings.RegistrarUser(UserName, UserPass);
                    await DisplayAlert("Solicitud de registro hecha", "Tu registro lo tiene que aceptar un administrador", "Cerrar");
                    UserName = string.Empty; 
                    UserPass = string.Empty;
                    UserPass2 = string.Empty;
                
                }
               
            }catch(Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo realizar el registro: {ex.Message}", "Cerrar");
            }
            finally
            {
                IsBusy = false;
            }
        }
        public void ProcesoSimple()
        {

        }
        #endregion
        #region COMANDOS
        public ICommand RegistrarseCommand => new Command(async () => await Registrarse());
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion


    }
}
