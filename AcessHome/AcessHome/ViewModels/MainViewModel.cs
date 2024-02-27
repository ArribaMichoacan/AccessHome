using AcessHome.Services;
using AcessHome.Services.Firebase;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AcessHome.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region VARIABLES

        FireBaseSettings _settings;

        string _Texto;

        bool _btnEnabled;

        BtService _btService;

        #endregion
        #region CONSTRUCTOR
        public MainViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _settings = new FireBaseSettings();
            _btService = new BtService();
            BtnEnabled = false;
        }
        #endregion
        #region OBJETOS

        public bool BtnEnabled
        {
            get { return _btnEnabled; }

            set { SetValue(ref _btnEnabled, value); }
        }

        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }
        #endregion
        #region PROCESOS
        public async Task AbrirCerradura()
        {
            if(IsBusy) 
                return;

            try
            {
                IsBusy = true;
                await _btService.Abrir();
                await _settings.RegistrarVisita();
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error",$"Ocurrio un error al intentar abrir la cerradura..{ex.Message}","Cerrar");
            }
            finally
            {
                IsBusy = false;
            }
            
        }

        public async Task CerrarCerradura()
        {
            if (IsBusy)
                return;

            try
            {

                IsBusy = true;

                await _btService.Cerrar();
            }catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrio un error al intentar cerrar la cerradura..{ex.Message}", "Cerrar");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async  Task Conectar()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await _btService.Conectar();
                BtnEnabled = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrio un error al intentar Conectar con la cerradura..{ex.Message}", "Cerrar");
            }
            finally
            {
                IsBusy = false;
            }
        }

        //public void ProcesoSimple()
        //{

        //}
        #endregion
        #region COMANDOS
        public ICommand AbrirCommand => new Command(async () => await AbrirCerradura());

        public ICommand CerrarCommand => new Command(async () => await CerrarCerradura());

        public ICommand ConectarCommand => new Command(async () => await Conectar());
        //public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion


    }
}
