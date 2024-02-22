using AcessHome.Data;
using AcessHome.Models;
using Plugin.BluetoothClassic.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AcessHome
{
    public partial class MainPage : ContentPage
    {
        private IBluetoothConnection _bluetoothConnection;
        private BluetoothDeviceModel _device;

        private Usuario _usuario { get; set; }

        public MainPage(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            BindingContext = new ViewModels.MainViewModel(Navigation);
            
        } 

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.MainViewModel(Navigation);
        }
        
      

        private async void GetVisitas()
        {
            DataBaseHelper bd = await DataBaseHelper.instance;
            List<Visita> consulta = new List<Visita>();
            await bd.GetAllVisitasByDate();

            listInfo.ItemsSource = consulta;
        }

        private async void BtnAbrir_Clicked(object sender, EventArgs e)
        {
            try
            {
                var data = Encoding.ASCII.GetBytes("1");
                //await _bluetoothConnection.TransmitAsync(data);
                DataBaseHelper baseHelper = await DataBaseHelper.instance;
                _usuario.idUsuario = await baseHelper.GetUserId(_usuario);
                await baseHelper.SaveVisita(_usuario);                
            }
            catch
            {
                await DisplayAlert("Error", "No se pudo abrir la cerradura", "Cerrar");
            }
        }

        private async void BtnCerrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var data = Encoding.ASCII.GetBytes("0");
                await _bluetoothConnection.TransmitAsync(data);
            }
            catch
            {
                await DisplayAlert("Error", "No se pudo abrir la cerradura", "Cerrar");
            }
        }

        private async void BtnConectar_Clicked(object sender, EventArgs e)
        {            

            var adapter = DependencyService.Resolve<IBluetoothAdapter>();

            adapter.StartDiscovery();
            await Task.Delay(5000);

            _device = adapter.BondedDevices.FirstOrDefault(d => d.Name == "HC-05");
            try
            {
                if (_device != null)
                {
                    _bluetoothConnection = adapter.CreateConnection(_device);
                    await _bluetoothConnection.ConnectAsync();
                    BtnAbrir.IsEnabled = true;
                    BtnCerrar.IsEnabled = false;
                }
                else
                {
                    await DisplayAlert("Aviso", "No se pudo conectar", "Cerrar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", $"Error al intentar conectar {ex.Message}", "Cerrar");
            }
        }
    }
}