using AcessHome.Services;
using AcessHome.Services.Firebase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AcessHome.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        private bool _btnAbrir;
        private bool _btnCerrar;

        private BtService _btService;
        private FireBaseSettings _settings;

        #endregion
        #region CONSTRUCTOR
        public AdminViewModel()
        {
           // Navigation = navigation;
            _btService = new BtService();
            _settings = new FireBaseSettings();
            BtnAbrir = false;
            BtnCerrar = false;
        }
        #endregion
        #region OBJETOS

        public bool BtnAbrir
        {
            get { return _btnAbrir; }

            set { SetValue(ref _btnAbrir, value); }
        }

        public bool BtnCerrar
        {
            get { return _btnCerrar; }
            set { SetValue(ref _btnCerrar, value); }
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
            if (BtnAbrir) //Btnabrir sera true cuando el usuario se haya conectado
            {
                try
                {
                    //await _btService.Abrir();
                    await _settings.RegistrarVisita();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Ocurrio un error al intentar abrir la cerradura..{ex.Message}", "Cerrar");
                }
            }
            else
            {
                await DisplayAlert("Aviso", "Necesitas Conectarte primero al dispositivo", "Cerrar");
            }

        }

        public async Task CerrarCerradura()
        {
            try
            {
                await _btService.Cerrar();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrio un error al intentar cerrar la cerradura..{ex.Message}", "Cerrar");
            }
        }

        public async Task Conectar()
        {
            try
            {
               
                await _btService.Conectar();        
                BtnAbrir = true;
                BtnCerrar = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrio un error al intentar Conectar con la cerradura..{ex.Message}", "Cerrar");
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
