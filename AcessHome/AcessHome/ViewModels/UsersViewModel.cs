using AcessHome.Models.Firebase;
using AcessHome.Services.Firebase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AcessHome.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        #region VARIABLES
        bool isTaskRunning;
        bool isVisible;
        string _Texto;

        private FireBaseSettings _settings;

        private ObservableCollection<User> _usersList;

        #endregion
        #region CONSTRUCTOR
        public UsersViewModel()
        {
            IsTaskRunning = false;
            IsVisble = false;
            _settings = new FireBaseSettings();
            _usersList = new ObservableCollection<User>();

            GetRequests();

        }
        #endregion
        #region OBJETOS

        public bool IsVisble
        {
            get { return isVisible; }

            set { SetValue(ref isVisible, value); }
        }

        public bool IsTaskRunning
        {
            get { return isTaskRunning; }

            set { SetValue(ref isTaskRunning, value); }

        }

        public ObservableCollection<User> UserList
        {
            get { return _usersList; }

            set { SetValue(ref _usersList, value); }
        }



        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }
        #endregion
        #region PROCESOS


        public async void GetRequests()
        {
            try
            {
                //IsTaskRunning = true;
                UserList = await _settings.ObtenerUsuarios();

                if (UserList.Count == 0)
                {
                    IsVisble = true;
                }
                else
                {
                    IsVisble = false;
                }

                IsTaskRunning = false;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", $"Ocurrio lo siguiente: {ex.Message}", "Cerrar");
            }
            finally
            {

            }
        }

        public async Task RefreshRequests()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                IsTaskRunning = true;
                UserList = await _settings.ObtenerUsuarios();

                if (UserList.Count == 0)
                {
                    IsVisble = true;
                }
                else
                {
                    IsVisble = false;
                }

                IsTaskRunning = false;


            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }



        public async Task EliminarRegistro(object sender)
        {

            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var persona = sender as User;

                await _settings.EliminarRegistro(persona);

                UserList.Remove(persona);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", $"Ocurrio un error al eliminar el usuario: {ex.Message}", "Cerrar");
            }
            finally
            {
                IsBusy = false;
            }






        }

        public async Task AceptarRegistro(object sender)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var persona = sender as User;

                await _settings.RegistrarUsuario(persona.UserName, persona.UserPass);
                await _settings.EliminarRegistro(persona);

                UserList.Remove(persona);


            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }


        #endregion
        #region COMANDOS

        public ICommand EliminarRegistroCommand => new Command<object>(async (object obj) => await EliminarRegistro(obj));

        public ICommand AceptarCommand => new Command<object>(async (object obj) => await AceptarRegistro(obj));

        public ICommand ObtenerRegistrosCommand => new Command(async () => await RefreshRequests());

        //public ICommand EliminarCommand => new Command(async () => await EliminarRegistro());
        ///
        ///public ICommand CerrarCommand => new Command(async () => await CerrarCerradura());
        ///
        ///public ICommand ConectarCommand => new Command(async () => await Conectar());
        //public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion


    }
}
