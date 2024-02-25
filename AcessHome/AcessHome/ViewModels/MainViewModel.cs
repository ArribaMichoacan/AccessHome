using AcessHome.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AcessHome.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region VARIABLES
        string _Texto;

        BtService _btService;

        #endregion
        #region CONSTRUCTOR
        public MainViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _btService = new BtService();
        }
        #endregion
        #region OBJETOS
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }
        #endregion
        #region PROCESOS
        public async Task AbrirCerradura()
        {
            try
            {
                await _btService.Abrir();
            }catch(Exception ex)
            {
                await DisplayAlert("Error",$"Ocurrio un error al intentar abrir la cerradura..{ex.Message}","Cerrar");
            }
            
        }

        public async Task CerrarCerradura()
        {
            try
            {
                await _btService.Cerrar();
            }catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrio un error al intentar cerrar la cerradura..{ex.Message}", "Cerrar");
            }
        }

        public async  Task Conectar()
        {
            try
            {
                await _btService.Conectar();
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
